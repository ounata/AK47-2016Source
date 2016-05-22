using System.Collections.Generic;
using MCS.Library.Data;
using PPTS.Data.Customers.DataSources;
using PPTS.WebAPI.Customers.ViewModels.CustomerMeetings;

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
            string from = @"CustomerMeetings cMeetings
                           LEFT JOIN Customers_Current customer on cMeetings.CustomerId=customer.CustomerId
                           LEFT JOIN (Select CustomerId,ParentId From CustomerParentRelations Where IsPrimary=1) CusParentRelation on CusParentRelation.CustomerId=customer.CustomerId
                           LEFT JOIN Parents parent on parent.parentId=CusParentRelation.parentId";

            PagedQueryResult<CustomerMeetingQueryModel, CustomerMeetingQueryCollection> result = Query(prp, select, from, condition, orderByBuilder);
            return result;
        }
       
    }
}