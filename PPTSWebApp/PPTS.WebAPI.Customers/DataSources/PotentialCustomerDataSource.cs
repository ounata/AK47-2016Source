using System.Text;
using System.Collections.Generic;
using PPTS.Data.Customers.DataSources;
using PPTS.Data.Customers.Entities;
using PPTS.WebAPI.Customers.ViewModels.PotentialCustomers;
using PPTS.Data.Customers.Adapters;
using PPTS.Data.Customers;
using MCS.Library.Data;
using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Builder;
using MCS.Library.Data.Mapping;
using MCS.Library.Principal;
using PPTS.Data.Common.Security;
using PPTS.Data.Common;
using System;

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

            #region 数据权限加工
            qc.WhereClause = PPTS.Data.Common.Authorization.ScopeAuthorization<PotentialCustomer>
                .GetInstance(ConnectionDefine.PPTSCustomerConnectionName)
                .ReadAuthExistsBuilder("PotentialCustomers", qc.WhereClause).ToSqlString(TSqlBuilder.Instance);
            #endregion

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
                var creator = relations.GetStaff(CustomerRelationType.Creator);
                customer.CreateJobName = creator == null ? "" : creator.StaffJobName;

                ParentModel parent = GenericParentAdapter<ParentModel, List<ParentModel>>.Instance.LoadPrimaryParentInContext(customer.CustomerID);
                customer.ParentName = parent == null ? "" : parent.ParentName;
            });
        }

        private string BuildQueryCondition(PotentialCustomerQueryCriteriaModel condition)
        {
            string sqlBuilder = string.Empty;

            #region 在读学校
            if (!string.IsNullOrEmpty(condition.SchoolName))
            {
                sqlBuilder = string.Format(
                    @" and exists (select SchoolID from CM.Schools AS Schools where PotentialCustomers.SchoolID=Schools.SchoolID and SchoolName like N'%{0}%') ", condition.SchoolName.EncodeString());
            }
            #endregion

            #region 家庭住址
            if (!string.IsNullOrEmpty(condition.AddressDetail))
            {
                sqlBuilder += string.Format(
                    @" and exists(select CustomerParentRelations.CustomerID 
					from CM.CustomerParentRelations_Current AS CustomerParentRelations inner join CM.Parents_Current AS Parents on CustomerParentRelations.ParentID = Parents.ParentID
					where PotentialCustomers.CustomerID = CustomerParentRelations.CustomerID and Parents.AddressDetail like N'%{0}%')", condition.AddressDetail.EncodeString());
            }
            #endregion

            #region 归属关系-是否分配
            StringBuilder staffBuilder = new StringBuilder();
            StringBuilder isAssignBuilder = new StringBuilder();

            string staffRelationString = @" and exists(select CustomerStaffRelations.CustomerID 
                       from CM.CustomerStaffRelations_Current AS CustomerStaffRelations 
				       where PotentialCustomers.CustomerID = CustomerStaffRelations.CustomerID {0})";
            // 归属坐席
            if (!string.IsNullOrEmpty(condition.CallcenterName))
            {
                staffBuilder.AppendFormat(staffRelationString, string.Format(@" and (CustomerStaffRelations.StaffName=N'{0}' and CustomerStaffRelations.RelationType={1})", condition.CallcenterName.EncodeString(), (byte)CustomerRelationType.Callcenter));
            }
            else if (!string.IsNullOrEmpty(condition.IsAssignCallcenterStatus))
            {
                if (condition.IsAssignCallcenterStatus == ((byte)StaffRelationIsAssigned.Yes).ToString())
                {
                    staffBuilder.AppendFormat(staffRelationString, string.Format(@" and (CustomerStaffRelations.RelationType={0}) ", (byte)CustomerRelationType.Callcenter));
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
                staffBuilder.AppendFormat(staffRelationString, string.Format(@" and (CustomerStaffRelations.StaffName=N'{0}' and CustomerStaffRelations.RelationType={1})", condition.ConsultantName.EncodeString(), (byte)CustomerRelationType.Consultant));
            }
            else if (!string.IsNullOrEmpty(condition.IsAssignConsultantStatus))
            {
                if (condition.IsAssignConsultantStatus == ((byte)StaffRelationIsAssigned.Yes).ToString())
                {
                    staffBuilder.AppendFormat(staffRelationString, string.Format(@" and (CustomerStaffRelations.RelationType={0}) ", (byte)CustomerRelationType.Consultant));
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
                staffBuilder.AppendFormat(staffRelationString, string.Format(@" and (CustomerStaffRelations.StaffName=N'{0}' and CustomerStaffRelations.RelationType={1})", condition.MarketName.EncodeString(), (byte)CustomerRelationType.Market));
            }
            else if (!string.IsNullOrEmpty(condition.IsAssignMarketStatus))
            {
                if (condition.IsAssignMarketStatus == ((byte)StaffRelationIsAssigned.Yes).ToString())
                {
                    staffBuilder.AppendFormat(staffRelationString, string.Format(@" and (CustomerStaffRelations.RelationType={0}) ", (byte)CustomerRelationType.Market));
                }
                else
                {
                    isAssignBuilder.AppendFormat(
                        @" and not exists(select CustomerStaffRelations.CustomerID 
                           from CM.CustomerStaffRelations_Current AS CustomerStaffRelations 
				           where PotentialCustomers.CustomerID = CustomerStaffRelations.CustomerID and CustomerStaffRelations.RelationType={0})", (byte)CustomerRelationType.Market);
                }
            }

            sqlBuilder += staffBuilder.ToString() + isAssignBuilder.ToString();
            #endregion

            #region 归属关系
            StringBuilder belongsBuilder = new StringBuilder();
            if (!string.IsNullOrEmpty(condition.BelongName))
            {
                belongsBuilder.AppendFormat(string.Format(@" and CustomerStaffRelations.StaffName = N'{0}' ", condition.BelongName.EncodeString()));
            }
            if (condition.Belongs != null && condition.Belongs.Length > 0)
            {
                string ins = string.Empty;
                foreach (string item in condition.Belongs)
                {
                    ins += "\'" + item + "\',";
                }
                ins = ins.TrimEnd(',');
                belongsBuilder.AppendFormat(string.Format(@" and CustomerStaffRelations.RelationType in ({0}) ", ins));
            }
            sqlBuilder = string.IsNullOrEmpty(belongsBuilder.ToString()) ? sqlBuilder : sqlBuilder + string.Format(staffRelationString, belongsBuilder.ToString());
            #endregion

            #region 查询部门
            if (!string.IsNullOrEmpty(condition.DeptParam))
            {
                string deptID = DeluxeIdentity.CurrentUser.GetCurrentJob().Organization().ID;
                sqlBuilder += string.Format(@"
                        and exists(select CustomerStaffRelations.CustomerID 
				        from CM.CustomerStaffRelations_Current AS CustomerStaffRelations
				        where PotentialCustomers.CustomerID = CustomerStaffRelations.CustomerID and CustomerStaffRelations.StaffJobOrgID = '{0}')", deptID);
            }
            #endregion 

            #region 有效无效状态
            if (!string.IsNullOrEmpty(condition.CustomerStatus))
            {
                if (condition.CustomerStatus == ((byte)CustomerStatus.Invalid).ToString())
                {
                    sqlBuilder += string.Format(" and CustomerStatus = {0}", (byte)CustomerStatus.Invalid);
                }
                else
                {
                    sqlBuilder += string.Format(" and CustomerStatus <> {0}", (byte)CustomerStatus.Invalid);
                }
            }

            #endregion 

            #region 学员家长姓名、学员编号、联系方式 全文检索
            if (!string.IsNullOrEmpty(condition.Keyword))
            {
                sqlBuilder += string.Format(
                    @" and exists(select OwnerID
                       from CM.PotentialCustomersFulltext AS PotentialCustomersFulltext
                       where PotentialCustomers.CustomerID = PotentialCustomersFulltext.OwnerID and CONTAINS(PotentialCustomersFulltext.*, N'""{0}""'))", condition.Keyword.EncodeString());
            }
            #endregion

            #region 市场相关-潜客列表过滤掉学员，市场导入的资源不过滤
            // 来源
            if (condition.From == "market")
            {
                sqlBuilder += string.Format(@" and PotentialCustomers.CreatorJobType = " + (byte)JobTypeDefine.Marketing);
            }
            else
            {
                sqlBuilder += string.Format(" and CustomerStatus <> {0}", (byte)CustomerStatus.Formal); // 潜客中过滤掉学员
            }
            #endregion

            #region 市场相关-充值日期
            string chargeBuilder = @" and exists (select AccountChargeApplies.CustomerID 
                                     from CM.AccountChargeApplies AccountChargeApplies 
	                                 where PotentialCustomers.CustomerID=AccountChargeApplies.CustomerID {0})";
            string payTimeBuilder = string.Empty;
            if (condition.PayTimeStartUTC != null && condition.PayTimeStartUTC != DateTime.MinValue)
            {
                payTimeBuilder = string.Format(@" and AccountChargeApplies.PayTime >= '{0}'", condition.PayTimeStartUTC);
            }
            if (condition.PayTimeEndUTC != null && condition.PayTimeEndUTC != DateTime.MinValue)
            {
                payTimeBuilder += string.Format(@" and AccountChargeApplies.PayTime <= '{0}'", condition.PayTimeEndUTC);
            }
            sqlBuilder += string.IsNullOrEmpty(payTimeBuilder) ? "" : string.Format(chargeBuilder, payTimeBuilder);

            #endregion

            #region 市场相关-充值金额
            string payAmountBuilder = string.Empty;
            if (condition.PayAmountMin > 0)
            {
                payAmountBuilder = string.Format(@" and AccountChargeApplies.ChargeMoney >= {0}", condition.PayAmountMin);
            }
            if (condition.PayAmountMax > 0)
            {
                payAmountBuilder += string.Format(@" and AccountChargeApplies.ChargeMoney <= {0}", condition.PayAmountMax);
            }
            sqlBuilder += string.IsNullOrEmpty(payAmountBuilder) ? "" : string.Format(chargeBuilder, payAmountBuilder);

            #endregion

            #region 市场相关-客户分类-有效无效
            if (!string.IsNullOrEmpty(condition.CustomerType))
            {
                if (condition.CustomerType == "0")
                {
                    sqlBuilder += string.Format(@" and PotentialCustomers.CustomerStatus <> {0}",(byte)CustomerStatus.Formal);
                }
                else if (condition.CustomerType == "1")
                {
                    sqlBuilder += string.Format(@" and PotentialCustomers.CustomerStatus = {0}", (byte)CustomerStatus.Formal);
                }
            }
            #endregion 

            return sqlBuilder;
        }

    }
}