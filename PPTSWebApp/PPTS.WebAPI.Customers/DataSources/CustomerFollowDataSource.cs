using System.Collections.Generic;
using MCS.Library.Data;
using PPTS.Data.Customers.DataSources;
using PPTS.WebAPI.Customers.ViewModels.CustomerFollows;
using PPTS.Data.Customers.Entities;
using PPTS.Data.Customers.Adapters;

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
            select += "a.PurchaseIntention,a.CustomerLevel,a.IntensionSubjects,a.followerName,";
            select += "a.IsStudyThere,a.PlanSignDate,a.CreateTime,a.FollowID,a.FollowMemo,a.CustomerID, " +
                      "(select top 1 StaffName from CM.[CustomerStaffRelations_Current] aa where aa.CustomerID = a.CustomerID and aa.[RelationType] = 1 order by aa.CreateTime desc)StaffName ";
            string from = "";
            from += "CM.[CustomerFollows] a inner join CM.[PotentialCustomersFulltext] c on a.CustomerID = c.OwnerID";
            PagedQueryResult<FollowQueryModel, CustomerFollowQueryCollection> result = Query(prp, select, from, condition, orderByBuilder);
            return result;
        }
    }
}
