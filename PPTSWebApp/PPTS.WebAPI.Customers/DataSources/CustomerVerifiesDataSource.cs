using MCS.Library.Core;
using MCS.Library.Data;
using MCS.Library.Data.Builder;
using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using PPTS.Data.Customers;
using PPTS.Data.Customers.Adapters;
using PPTS.Data.Customers.DataSources;
using PPTS.Data.Customers.Entities;
using PPTS.WebAPI.Customers.ViewModels.CustomerVerifies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.WebAPI.Customers.DataSources
{
    public class CustomerVerifiesDataSource : GenericCustomerDataSource<CustomerVerifyQueryModel, CustomerVerifiesQueryCollection>
    {
        public static readonly new CustomerVerifiesDataSource Instance = new CustomerVerifiesDataSource();

        public CustomerVerifiesDataSource()
        {

        }

        protected override void OnAfterQuery(CustomerVerifiesQueryCollection result)
        {
            List<string> customerIDs = new List<string>();
            result.ForEach((model) => customerIDs.Add(model.CustomerID));
            CustomerParentPhoneCollection loaded = CustomerInfoQueryAdapter.Instance.LoadCustomerParentPhoneByIDs(customerIDs.ToArray());
            result.ForEach((model) => { MappingModel(loaded, model); });
        }

        protected override void OnBuildQueryCondition(QueryCondition qc)
        {

            qc.SelectFields = @" a.* ";

            qc.FromClause = @" CM.[CustomerVerifies] a ";

            #region 数据权限加工

            qc.WhereClause = PPTS.Data.Common.Authorization.ScopeAuthorization<CustomerFollow>

                .GetInstance(ConnectionDefine.PPTSCustomerConnectionName)

                .ReadAuthExistsBuilder("a", qc.WhereClause).ToSqlString(TSqlBuilder.Instance);

            #endregion

            base.OnBuildQueryCondition(qc);
        }

        private void MappingModel(CustomerParentPhoneCollection loaded, CustomerVerifyQueryModel model)
        {
            CustomerParentPhone customerParentPhone = loaded.Find(render => render.CustomerID == model.CustomerID);
            if (customerParentPhone != null)
            {
                model.CustomerName = customerParentPhone.CustomerName;
                model.CustomerCode = customerParentPhone.CustomerCode;
                model.Grade = customerParentPhone.Grade;
                School schoolModel = SchoolAdapter.Instance.Load(customerParentPhone.SchoolID);
                if (schoolModel != null)
                    model.CampusName = schoolModel.SchoolName;  //  学员所在学校
                model.ParentName = customerParentPhone.ParentName;
            }
        }
        #region 上门列表数据源备份
        //public PagedQueryResult<CustomerVerifyQueryModel, CustomerVerifiesQueryCollection> LoadCustomerVerify(IPageRequestParams prp, object condition, IEnumerable<IOrderByRequestItem> orderByBuilder)
        //{
        //    string select = " a.CustomerID,a.CreatorName,a.CampusName,a.CampusID,a.CreateTime,a.VerifyPeoples,a.VerifyRelations,c.CustomerSearchContent,c.ParentSearchContent,";
        //    select += "(select top 1 q.PlanVerifyTime from CM.[CustomerFollows] q where a.CustomerID = q.CustomerID order by q.CreateTime desc)planTime,";
        //    select += "(select top 1 StaffName from CM.[CustomerStaffRelations_Current] aa where aa.CustomerID = a.CustomerID and aa.[RelationType] = 1 order by aa.CreateTime desc)StaffName";
        //    string from = "";
        //    from += " CM.[CustomerVerifies] a inner join CM.[PotentialCustomersFulltext] c on a.CustomerID = c.OwnerID ";
        //    string StaffName = (condition as CustomerVerifyQueryCriteriaModel).StaffName;
        //    PagedQueryResult<CustomerVerifyQueryModel, CustomerVerifiesQueryCollection> result;
        //    string where = null;
        //    if (StaffName != "" && StaffName != null)
        //    {
        //        var whereBuilder = ConditionMapping.GetConnectiveClauseBuilder(condition);
        //        if (whereBuilder != null && TSqlBuilder.Instance != null)
        //            where = whereBuilder.ToSqlString(TSqlBuilder.Instance) + (whereBuilder.ToSqlString(TSqlBuilder.Instance) == "" ? "" : " AND ") + "EXISTS(select top 1 1 from CM.[CustomerStaffRelations_Current] aa where aa.CustomerID = a.CustomerID and aa.[RelationType] = 1 and aa.StaffName = '" + StaffName + "')";
        //        result = Query(prp, select, from, where, orderByBuilder);
        //    }
        //    else
        //        result = Query(prp, select, from, condition, orderByBuilder);
        //    //  PagedQueryResult<CustomerVerifyQueryModel, CustomerVerifiesQueryCollection> result = Query(prp, select, from, condition, orderByBuilder);
        //    return result;
        //}
        #endregion

        private string EncodeText(string text)
        {
            if (text != null)
                return text.Replace("'", "''");
            return text;
        }

        public PagedQueryResult<CustomerVerifyQueryModel, CustomerVerifiesQueryCollection> LoadCustomerVerify(IPageRequestParams prp, object condition, IEnumerable<IOrderByRequestItem> orderByBuilder)
        {
            var select = @"a.*";
            var from = @" CM.CustomerVerifies a";

            PagedQueryResult<CustomerVerifyQueryModel, CustomerVerifiesQueryCollection> result;
            string where = null;
            var whereBuilder = ConditionMapping.GetConnectiveClauseBuilder(condition);
            if (whereBuilder != null && TSqlBuilder.Instance != null)
                where = whereBuilder.ToSqlString(TSqlBuilder.Instance);
            where = string.IsNullOrEmpty(where) ? "1>0" : where;

            CustomerVerifyQueryCriteriaModel criterial = (condition as CustomerVerifyQueryCriteriaModel);
            #region 全文检索
            if (!string.IsNullOrEmpty(criterial.Keyword))
            {
                string searchText = this.EncodeText(criterial.Keyword);
                where += " and (";
                where += string.Format("EXISTS(select top 1 1 from CM.CustomersFulltext d where d.OwnerID = a.CustomerID and (contains(d.CustomerSearchContent,'{0}') or contains(d.ParentSearchContent,'{0}')))", searchText);
                where += " or ";
                where += string.Format("EXISTS(select top 1 1 from CM.PotentialCustomersFulltext e where e.OwnerID = a.CustomerID and (contains(e.CustomerSearchContent,'{0}') or contains(e.ParentSearchContent,'{0}')))", searchText);
                where += ")";
            }
            #endregion
            #region 建档时间
            if (criterial.CreateTimeStart != DateTime.MinValue && criterial.CreateTimeStart != DateTime.MaxValue)
            {
                string date = TimeZoneContext.Current.ConvertTimeToUtc(criterial.CreateTimeStart).ToString("yyyy-MM-dd HH:mm:ss");
                where += " and (";
                where += string.Format("EXISTS(select top 1 1 from CM.PotentialCustomers e where e.CustomerID = a.CustomerID and e.CreateTime>='{0}')", date);
                where += ")";
            }
            if (criterial.CreateTimeEnd != DateTime.MinValue && criterial.CreateTimeEnd != DateTime.MaxValue)
            {
                string date = TimeZoneContext.Current.ConvertTimeToUtc(criterial.CreateTimeEnd.AddDays(1)).ToString("yyyy-MM-dd HH:mm:ss");
                where += " and (";
                where += string.Format("EXISTS(select top 1 1 from CM.PotentialCustomers e where e.CustomerID = a.CustomerID and e.CreateTime<'{0}')", date);
                where += ")";
            }
            #endregion
            #region 建档关系
            if (criterial.CreatorJobTypes != null && criterial.CreatorJobTypes.Length != 0)
            {
                string inStr = string.Empty;
                foreach (string s in criterial.CreatorJobTypes)
                    inStr += "\'" + s + "\',";
                inStr = inStr.TrimEnd(',');

                where += " and (";
                where += string.Format("EXISTS(select top 1 1 from CM.PotentialCustomers e where e.CustomerID = a.CustomerID and e.CreatorJobType in ({0}))", inStr);
                where += ")";
            }
            #endregion

            result = Query(prp, select, from, where, orderByBuilder);
            return result;
        }
    }
}
