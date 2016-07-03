using System.Collections.Generic;
using MCS.Library.Data;
using PPTS.Data.Customers.DataSources;
using PPTS.WebAPI.Customers.ViewModels.CustomerMeetings;
using MCS.Library.SOA.DataObjects;
using MCS.Web.MVC.Library.Models;
using PPTS.Data.Customers.Adapters;
using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Builder;
using PPTS.Data.Customers.Entities;

namespace PPTS.WebAPI.Customers.DataSources
{
    /// <summary>
    /// 教学服务会DataSource
    /// </summary>
    public class CustomerMeetingDataSource : GenericCustomerDataSource<CustomerMeetingQueryModel, CustomerMeetingQueryCollection>
    {

        public static readonly new CustomerMeetingDataSource Instance = new CustomerMeetingDataSource();
        private CustomerMeetingDataSource() { }

        /// <summary>
        /// 教学服务会分页查询
        /// </summary>
        /// <param name="prp">分页参数</param>
        /// <param name="condition">查询条件</param>
        /// <param name="orderByBuilder">排序条件</param>
        /// <returns></returns>
        public PagedQueryResult<CustomerMeetingQueryModel, CustomerMeetingQueryCollection> GetCustomerMeetingsList(IPageRequestParams prp, object condition, IEnumerable<IOrderByRequestItem> orderByBuilder)
        {
            string select = @"cMeetings.MeetingID,cMeetings.MeetingTime,cMeetings.CampusName,customer.CustomerId,customer.CustomerName,customer.CustomerCode,customer.Grade,cMeetings.CampusID,
                             Parent.ParentName,CMeetings.MeetingType,CMeetings.OrganizerName,
                             CMeetings.Satisficing";
            string from = @" CM.CustomerMeetings cMeetings
                           LEFT JOIN CM.Customers_Current customer on cMeetings.CustomerId=customer.CustomerId
                           LEFT JOIN (Select CustomerId,ParentId From CM.CustomerParentRelations_Current Where IsPrimary=1) CusParentRelation on CusParentRelation.CustomerId=customer.CustomerId
                           LEFT JOIN CM.Parents_Current parent on parent.parentId=CusParentRelation.parentId";

            PagedQueryResult<CustomerMeetingQueryModel, CustomerMeetingQueryCollection> result = Query(prp, select, from, condition, orderByBuilder);
            foreach (var m in result.PagedData)
            {
                m.Materials = MaterialModelHelper.GetInstance(CustomerMeetingAdapter.Instance.ConnectionName).LoadByResourceID(m.MeetingID);
            }
            return result;
        }
        protected override void OnBuildQueryCondition(QueryCondition qc)
        {
            qc.SelectFields = @" Z.* ";
            qc.FromClause = @" (select cMeetings.MeetingID,cMeetings.MeetingTime,cMeetings.CampusName,customer.CustomerId,customer.CustomerName,customer.CustomerCode,customer.Grade,cMeetings.CampusID,
                             Parent.ParentName,CMeetings.MeetingType,CMeetings.OrganizerName,
                             CMeetings.Satisficing
       from     CM.CustomerMeetings cMeetings
                           LEFT JOIN CM.Customers_Current customer on cMeetings.CustomerId=customer.CustomerId
                           LEFT JOIN (Select CustomerId,ParentId From CM.CustomerParentRelations_Current Where IsPrimary=1) CusParentRelation on CusParentRelation.CustomerId=customer.CustomerId
                           LEFT JOIN CM.Parents_Current parent on parent.parentId=CusParentRelation.parentId)Z";

            #region 数据权限加工
            qc.WhereClause = PPTS.Data.Common.Authorization.ScopeAuthorization<CustomerMeeting>
                .GetInstance(ConnectionDefine.DBConnectionName)
                .ReadAuthExistsBuilder("z", qc.WhereClause).ToSqlString(TSqlBuilder.Instance);
            #endregion

            base.OnBuildQueryCondition(qc);
        }
    }
}