using System.Collections.Generic;
using MCS.Library.Data;
using PPTS.Data.Customers.DataSources;
using PPTS.WebAPI.Customers.ViewModels.CustomerFollows;
using PPTS.Data.Customers.Entities;
using PPTS.Data.Customers.Adapters;
using MCS.Library.Data.Builder;
using MCS.Library.Data.Mapping;
using System;
using MCS.Library.Core;
using MCS.Library.Data.DataObjects;
using PPTS.Data.Customers;
using MCS.Library.Data.Adapters;
using System.Linq;

namespace PPTS.WebAPI.Customers.DataSources
{
    /// <summary>
    /// 用户跟进数据源类
    /// </summary>
    public class CustomerFollowDataSource : GenericCustomerDataSource<FollowQueryModel, CustomerFollowQueryCollection>
    {
        public static readonly new CustomerFollowDataSource Instance = new CustomerFollowDataSource();

        private CustomerFollowDataSource()
        {
        }

        protected override void OnAfterQuery(CustomerFollowQueryCollection result)
        {
            List<string> customerIDs = result.Select(m => m.CustomerID).Distinct().ToList();
            CustomerParentPhoneCollection loaded = CustomerInfoQueryAdapter.Instance.LoadCustomerParentPhoneByIDs(customerIDs.ToArray());

            InLoadingCondition inBuilder = new InLoadingCondition(condition => result.ForEach(item => condition.AppendItem(item.CustomerID)), "CustomerID");
            PotentialCustomerCollection CustomerLoaded = GenericPotentialCustomerAdapter<PotentialCustomer, PotentialCustomerCollection>.Instance.LoadByInBuilder(inBuilder, DateTime.MinValue);
            
            result.ForEach((model) => { MappingModel(loaded, CustomerLoaded, model); });
        }

        private void MappingModel(CustomerParentPhoneCollection loaded, PotentialCustomerCollection CustomerLoaded, FollowQueryModel model)
        {
            CustomerParentPhone customerParentPhone = loaded.Find(render => render.CustomerID == model.CustomerID);
            if (customerParentPhone != null)
            {
                model.CustomerName = customerParentPhone.CustomerName;
                model.CustomerCode = customerParentPhone.CustomerCode;
                model.ParentName = customerParentPhone.ParentName;
            }

            PotentialCustomer customer = CustomerLoaded.Find(render => render.CustomerID == model.CustomerID);
            if (customer != null)
            {
                model.CustomerStatus = customer.Status;
            }

        }

        protected override void OnBuildQueryCondition(QueryCondition qc)
        {

            qc.SelectFields = @" a.* ";

            qc.FromClause = @" CM.[CustomerFollows] a ";

            #region 数据权限加工

            qc.WhereClause = PPTS.Data.Common.Authorization.ScopeAuthorization<CustomerFollow>

                .GetInstance(ConnectionDefine.PPTSCustomerConnectionName)

                .ReadAuthExistsBuilder("a", qc.WhereClause).ToSqlString(TSqlBuilder.Instance);

            #endregion

            base.OnBuildQueryCondition(qc);
        }


        private string EncodeText(string text)
        {
            if (text != null)
                return text.Replace("'", "''");
            return text;
        }

        public PagedQueryResult<FollowQueryModel, CustomerFollowQueryCollection> LoadCustomerFollow(IPageRequestParams prp, object condition, IEnumerable<IOrderByRequestItem> orderByBuilder)
        {
            string select = " a.OrgID,a.FollowTime,a.FollowType,a.FollowObject,a.PlanVerifyTime,a.FollowStage,";
            select += "a.PurchaseIntention,a.CustomerLevel,a.IntensionSubjects,";
            select += "a.IsStudyThere,a.PlanSignDate,a.CreateTime,a.FollowID,a.FollowMemo,a.CustomerID,a.followerName,a.FollowerJobID,a.FollowerJobName ";
            string from = "";
            from += " CM.[CustomerFollows] a ";
            PagedQueryResult<FollowQueryModel, CustomerFollowQueryCollection> result;

            string where = null;
            var whereBuilder = ConditionMapping.GetConnectiveClauseBuilder(condition);
            if (whereBuilder != null && TSqlBuilder.Instance != null)
                where = whereBuilder.ToSqlString(TSqlBuilder.Instance);
            where = string.IsNullOrEmpty(where) ? "1>0" : where;

            FollowQueryCriteriaModel criterial = (condition as FollowQueryCriteriaModel);
            #region 查询校区或分公司
            if (criterial.CampusIDs != null || criterial.BranchIDs != null)
            {
                if (criterial.CampusIDs != null)
                {
                    where += " and (";
                    where += string.Format("OrgID = '{0}'", criterial.CampusIDs);
                }
                if (criterial.BranchIDs != null)
                {
                    where += " or ";
                    where += string.Format("OrgID = '{0}'", criterial.BranchIDs);
                }
                where += ")";
            }
            #endregion
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
            #region 建档人
            if (!string.IsNullOrEmpty(criterial.CreatorName))
            {
                string customerCreatorName = this.EncodeText(criterial.CreatorName);
                where += " and (";
                where += string.Format("EXISTS(select top 1 1 from CM.PotentialCustomers e where e.CustomerID = a.CustomerID and e.CreatorName='{0}')", customerCreatorName);
                where += ")";
            }
            #endregion
            #region 查询部门
            if (!string.IsNullOrEmpty(criterial.QueryDeptID))
            {
                where += " and (";
                where += string.Format("EXISTS(select top 1 1 from MT.CustomerOrgAuthorizations d where d.OwnerID = a.CustomerID and d.ObjectID='{0}')", criterial.QueryDeptID);
                where += ")";
            }
            #endregion

            result = Query(prp, select, from, where, orderByBuilder);

            return result;
        }
    }
}
