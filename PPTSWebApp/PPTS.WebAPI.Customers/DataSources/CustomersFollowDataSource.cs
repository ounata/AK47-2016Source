using System.Collections.Generic;
using MCS.Library.Data;
using PPTS.Data.Customers.DataSources;
using PPTS.WebAPI.Customers.ViewModels.CustomerFollows;

namespace PPTS.WebAPI.Customers.DataSources
{
    /// <summary>
    /// 用户跟进数据源类
    /// </summary>
    public class CustomersFollowDataSource : GenericCustomerDataSource<FollowQueryModel, CustomerFollowQueryCollection>
    {
        public static readonly new CustomersFollowDataSource Instance = new CustomersFollowDataSource();

        private CustomersFollowDataSource()
        {
        }

        protected override void OnAfterQuery(CustomerFollowQueryCollection result)
        {
         //   result.ForEach(parent => {
               // PhoneAdapter.Instance.LoadByOwnerIDInContext(parent.CustomerID, phone => parent.FollowPhone(phone));
           //     PhoneAdapter.Instance.GetDbContext().DoAction(context => context.ExecuteDataSetSqlInContext());
            //});
            // PhoneAdapter.Instance.GetDbContext().DoAction(context => context.ExecuteDataSetSqlInContext());
        }

        public PagedQueryResult<FollowQueryModel, CustomerFollowQueryCollection> LoadCustomerFollow(IPageRequestParams prp, object condition, IEnumerable<IOrderByRequestItem> orderByBuilder)
        {
            string select = " c.CustomerSearchContent,c.ParentSearchContent,b.CustomerName,b.CustomerCode,";
            select += "e.ParentName,a.FollowTime,a.FollowType,a.FollowObject,a.PlanVerifyTime,a.FollowStage,";
            select += "a.PurchaseIntension,a.CustomerLevel,a.IntensionSubjects,a.followerName,";
            select += "a.IsStudyThere,a.PlanSignDate,b.CampusName,a.CreateTime ";
            string from = "";
            from += "[CustomerFollows] a inner join [Customers] b on a.CustomerID = b.CustomerID"
                    +" inner join[CustomersFulltext] c on a.CustomerID = c.OwnerID"
                     + " inner join[CustomerParentRelations] d on a.CustomerID = d.CustomerID"
                    +" inner join[Parents] e on d.ParentID = e.ParentID";
            PagedQueryResult<FollowQueryModel, CustomerFollowQueryCollection> result = Query(prp, select, from, condition, orderByBuilder);
            return result;
        }
    }
}
