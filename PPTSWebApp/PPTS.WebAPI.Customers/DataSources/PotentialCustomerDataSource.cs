using System.Collections.Generic;
using MCS.Library.Data;
using PPTS.Data.Customers.DataSources;
using PPTS.Data.Customers.Entities;
using PPTS.WebAPI.Customers.ViewModels.PotentialCustomers;
using PPTS.Data.Customers.Adapters;
using PPTS.Data.Customers;
using MCS.Library.Data.DataObjects;
using System.Text;
using MCS.Library.Data.Builder;
using MCS.Library.Data.Mapping;

namespace PPTS.WebAPI.Customers.DataSources
{
    public class PotentialCustomerDataSource : GenericCustomerDataSource<PotentialCustomerSearchModel, PotentialCustomerSearchModelCollection>
    {
        public static readonly new PotentialCustomerDataSource Instance = new PotentialCustomerDataSource();

        private PotentialCustomerDataSource()
        {
        }

        public PagedQueryResult<PotentialCustomerSearchModel, PotentialCustomerSearchModelCollection> LoadPotentialCustomers(IPageRequestParams prp, PotentialCustomerQueryCriteriaModel condition, IEnumerable<IOrderByRequestItem> orderByBuilder)
        {
            ConnectiveSqlClauseCollection sqlCollection = new ConnectiveSqlClauseCollection();
            sqlCollection.Add(ConditionMapping.GetConnectiveClauseBuilder(condition));

            string customerSqlBuilder = sqlCollection.ToSqlString(TSqlBuilder.Instance);
            string sqlBuilder = BuildQueryCondition(condition);

            sqlBuilder = string.IsNullOrEmpty(customerSqlBuilder) ? string.Format(" 1=1 {0}", sqlBuilder) : string.Format("{0}{1}", customerSqlBuilder, sqlBuilder);

            var result = Query(prp, sqlBuilder, orderByBuilder);

            return result;
        }

        protected override void OnBuildQueryCondition(QueryCondition qc)
        {
            qc.SelectFields = @" PotentialCustomers.* ";
            qc.FromClause = @" CM.PotentialCustomers_Current PotentialCustomers ";

            // qc.WhereClause=PPTS.Data.Customers.Authorization.ScopeAuthorization<PotentialCustomer>.Instance.ReadAuthExistsBuider("pcc", qc.WhereClause).ToSqlString(MCS.Library.Data.Builder.TSqlBuilder.Instance);
            base.OnBuildQueryCondition(qc);
        }

        protected override void OnAfterQuery(PotentialCustomerSearchModelCollection result)
        {
            result.ForEach(customer =>
            {
                var relations = CustomerStaffRelationAdapter.Instance.LoadByCustomerID(customer.CustomerID);
                customer.ConsultantStaff = relations.GetStaffName(CustomerRelationType.Consultant);
                customer.Consultant = relations.GetStaff(CustomerRelationType.Consultant);
                customer.Market = relations.GetStaff(CustomerRelationType.Market);
                customer.MarketStaff = relations.GetStaffName(CustomerRelationType.Market);

                ParentModel parent = GenericParentAdapter<ParentModel, List<ParentModel>>.Instance.LoadPrimaryParentInContext(customer.CustomerID);
                customer.ParentName = parent == null ? "" : parent.ParentName;
            });
        }

        private string BuildQueryCondition(PotentialCustomerQueryCriteriaModel condition)
        {
            string sqlBuilder = string.Empty;

            #region 在读学校
            string schoolNameBuilder = string.Empty;
            if (!string.IsNullOrEmpty(condition.SchoolName))
            {
                schoolNameBuilder = string.Format(
                    @" and exists (select SchoolID from CM.Schools AS Schools where PotentialCustomers.SchoolID=Schools.SchoolID and SchoolName like N'%{0}%') ", condition.SchoolName);
            }
            #endregion

            #region 家庭住址
            string addressBuilder = string.Empty;
            if (!string.IsNullOrEmpty(condition.AddressDetail))
            {
                addressBuilder = string.Format(
                    @" and exists(select CustomerParentRelations.CustomerID 
					from CM.CustomerParentRelations_Current AS CustomerParentRelations inner join CM.Parents_Current AS Parents on CustomerParentRelations.ParentID = Parents.ParentID
					where PotentialCustomers.CustomerID = CustomerParentRelations.CustomerID and Parents.AddressDetail like N'%{0}%')", condition.AddressDetail);
            }
            #endregion

            #region 归属关系
            string staffRelationBuilder = string.Empty;
            StringBuilder staffBuilder = new StringBuilder();
            StringBuilder isAssignBuilder = new StringBuilder();

            string sqlString = @" and exists(select CustomerStaffRelations.CustomerID 
                       from CM.CustomerStaffRelations_Current AS CustomerStaffRelations 
				       where PotentialCustomers.CustomerID = CustomerStaffRelations.CustomerID {0})";
            // 归属坐席
            if (!string.IsNullOrEmpty(condition.CallcenterName))
            {
                staffBuilder.AppendFormat(sqlString, string.Format(@" and (CustomerStaffRelations.StaffName=N'{0}' and CustomerStaffRelations.RelationType={1})", condition.CallcenterName, (byte)CustomerRelationType.Callcenter));
            }
            else if (!string.IsNullOrEmpty(condition.IsAssignCallcenterStatus))
            {
                if (condition.IsAssignCallcenterStatus == ((byte)StaffRelationIsAssigned.Yes).ToString())
                {
                    staffBuilder.AppendFormat(sqlString, string.Format(@" and (CustomerStaffRelations.RelationType={0}) ", (byte)CustomerRelationType.Callcenter));
                }
                else
                {
                    isAssignBuilder.AppendFormat(
                        @" and not exists(select CustomerStaffRelations.CustomerID 
                           from CM.CustomerStaffRelations_Current AS CustomerStaffRelations 
				           where PotentialCustomers.CustomerID = CustomerStaffRelations.CustomerID and CustomerStaffRelations.RelationType={0})", (byte)CustomerRelationType.Callcenter);
                }
            }

            // 咨询师姓名
            if (!string.IsNullOrEmpty(condition.ConsultantName))
            {
                staffBuilder.AppendFormat(sqlString, string.Format(@" and (CustomerStaffRelations.StaffName=N'{0}' and CustomerStaffRelations.RelationType={1})", condition.ConsultantName, (byte)CustomerRelationType.Consultant));
            }
            else if (!string.IsNullOrEmpty(condition.IsAssignConsultantStatus))
            {
                if (condition.IsAssignConsultantStatus == ((byte)StaffRelationIsAssigned.Yes).ToString())
                {
                    staffBuilder.AppendFormat(sqlString, string.Format(@" and (CustomerStaffRelations.RelationType={0}) ", (byte)CustomerRelationType.Consultant));
                }
                else
                {
                    isAssignBuilder.AppendFormat(
                        @" and not exists(select CustomerStaffRelations.CustomerID 
                           from CM.CustomerStaffRelations_Current AS CustomerStaffRelations 
				           where PotentialCustomers.CustomerID = CustomerStaffRelations.CustomerID and CustomerStaffRelations.RelationType={0})", (byte)CustomerRelationType.Consultant);
                }
            }

            // 市场专员姓名
            if (!string.IsNullOrEmpty(condition.MarketName))
            {
                staffBuilder.AppendFormat(sqlString, string.Format(@" and (CustomerStaffRelations.StaffName=N'{0}' and CustomerStaffRelations.RelationType={1})", condition.MarketName, (byte)CustomerRelationType.Market));
            }
            else if (!string.IsNullOrEmpty(condition.IsAssignMarketStatus))
            {
                if (condition.IsAssignMarketStatus == ((byte)StaffRelationIsAssigned.Yes).ToString())
                {
                    staffBuilder.AppendFormat(sqlString, string.Format(@" and (CustomerStaffRelations.RelationType={0}) ", (byte)CustomerRelationType.Market));
                }
                else
                {
                    isAssignBuilder.AppendFormat(
                        @" and not exists(select CustomerStaffRelations.CustomerID 
                           from CM.CustomerStaffRelations_Current AS CustomerStaffRelations 
				           where PotentialCustomers.CustomerID = CustomerStaffRelations.CustomerID and CustomerStaffRelations.RelationType={0})", (byte)CustomerRelationType.Market);
                }
            }

            staffRelationBuilder = staffBuilder.ToString() + isAssignBuilder.ToString();
            #endregion

            #region 有效无效状态
            string customerStatusBuilder = string.Format(" and CustomerStatus <> {0}", (byte)CustomerStatus.Formal); // 潜客中过滤掉学员
            if (!string.IsNullOrEmpty(condition.CustomerStatus))
            {
                if (condition.CustomerStatus == ((byte)CustomerStatus.Invalid).ToString())
                {
                    customerStatusBuilder = string.Format(" and CustomerStatus = {0}", (byte)CustomerStatus.Invalid);
                }
                else
                {
                    customerStatusBuilder = string.Format(" and CustomerStatus <> {0}", (byte)CustomerStatus.Invalid);
                }
            }

            #endregion 

            #region 学员家长姓名、学员编号、联系方式 全文检索
            string customersFulltextbuilder = string.Empty;
            if (!string.IsNullOrEmpty(condition.Keyword))
            {
                customersFulltextbuilder = string.Format(
                    @" and exists(select OwnerID
                       from CM.PotentialCustomersFulltext AS PotentialCustomersFulltext
                       where PotentialCustomers.CustomerID = PotentialCustomersFulltext.OwnerID and CONTAINS(PotentialCustomersFulltext.*, N'{0}'))", condition.Keyword);
            }
            #endregion

            sqlBuilder = schoolNameBuilder + addressBuilder + staffRelationBuilder + customersFulltextbuilder + customerStatusBuilder;

            return sqlBuilder;
        }
    }
}