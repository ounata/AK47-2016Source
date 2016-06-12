using System.Collections.Generic;
using MCS.Library.Data;
using PPTS.Data.Customers.DataSources;
using PPTS.WebAPI.Customers.ViewModels.CustomerFollows;
using PPTS.Data.Customers.Entities;
using PPTS.Data.Customers.Adapters;
using MCS.Library.Data.Builder;
using MCS.Library.Data.Mapping;

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
            List<string> customerIDs = new List<string>();
            result.ForEach((model) => customerIDs.Add(model.CustomerID));
            CustomerParentPhoneCollection loaded = CustomerInfoQueryAdapter.Instance.LoadCustomerParentPhoneByIDs(customerIDs.ToArray());
            result.ForEach((model) => { MappingModel(loaded, model); });
        }

        private void MappingModel(CustomerParentPhoneCollection loaded, FollowQueryModel model)
        {
            CustomerParentPhone customerParentPhone = loaded.Find(render => render.CustomerID == model.CustomerID);
            model.CustomerName = customerParentPhone.CustomerName;
            model.CustomerCode = customerParentPhone.CustomerCode;
            model.ParentName = customerParentPhone.ParentName;
        }

        public PagedQueryResult<FollowQueryModel, CustomerFollowQueryCollection> LoadCustomerFollow(IPageRequestParams prp, object condition, IEnumerable<IOrderByRequestItem> orderByBuilder)
        {
            string select = " c.CustomerSearchContent,c.ParentSearchContent,";
            select += "a.OrgID,a.FollowTime,a.FollowType,a.FollowObject,a.PlanVerifyTime,a.FollowStage,";
            select += "a.PurchaseIntention,a.CustomerLevel,a.IntensionSubjects,(a.followerName+'('+a.FollowerJobName+')')FollowerAndJobName,";
            select += "a.IsStudyThere,a.PlanSignDate,a.CreateTime,a.FollowID,a.FollowMemo,a.CustomerID,b.CustomerStatus ";
            string from = "";
            from += " CM.[CustomerFollows] a ";
            from += "inner join cm.PotentialCustomers_Current b on a.CustomerID = b.CustomerID ";
            from += "left join CM.PotentialCustomersFulltext c on a.CustomerID = c.OwnerID ";
            string StaffName = (condition as FollowQueryCriteriaModel).StaffName;
            PagedQueryResult<FollowQueryModel, CustomerFollowQueryCollection> result;
            string where = null;
            if (StaffName != "" && StaffName != null)
            {
                var whereBuilder = ConditionMapping.GetConnectiveClauseBuilder(condition);
                if (whereBuilder != null && TSqlBuilder.Instance != null)
                    where = whereBuilder.ToSqlString(TSqlBuilder.Instance) + (whereBuilder.ToSqlString(TSqlBuilder.Instance) == "" ? "" : " AND ") + "EXISTS(select top 1 1 from CM.[CustomerStaffRelations_Current] aa where aa.CustomerID = a.CustomerID and aa.[RelationType] = 1 and aa.StaffName = '" + StaffName + "')";
                result = Query(prp, select, from, where, orderByBuilder);
            }
            else
                result = Query(prp, select, from, condition, orderByBuilder);

            return result;
        }
    }
}
