using MCS.Library.Data;
using PPTS.Data.Customers.DataSources;
using PPTS.WebAPI.Customers.ViewModels.Feedback;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPTS.WebAPI.Customers.DataSources
{
    public class CustomerFeedbacksDataSource : GenericCustomerDataSource<CustomerFeedbacksQueryModel, CustomerFeedbacksQueryCollection>
    {
        public static readonly new CustomerFeedbacksDataSource Instance = new CustomerFeedbacksDataSource();
        private CustomerFeedbacksDataSource() { }

        /// <summary>
        /// 客户反馈分页查询
        /// </summary>
        /// <param name="prp">分页参数</param>
        /// <param name="condition">查询条件</param>
        /// <param name="orderByBuilder">排序条件</param>
        /// <returns></returns>
        public PagedQueryResult<CustomerFeedbacksQueryModel, CustomerFeedbacksQueryCollection> GetCustomerFeedbackList(IPageRequestParams prp, object condition, IEnumerable<IOrderByRequestItem> orderByBuilder)
        {
            string select = @"feedbacks.FeedbackID,feedbacks.CampusID, feedbacks.FeedbackTime,feedbacks.BranchName,
                            feedbacks.CustomerId,feedbacks.FeedbackContent,
                            ParentName,ParentID,
                            customer.CustomerCode,customer.CustomerName,customer.CampusName,
                            customer.Grade";
            string from = @"CustomerFeedbacks feedbacks LEFT JOIN Customers_Current customer on feedbacks.CustomerId=customer.CustomerId";
            PagedQueryResult<CustomerFeedbacksQueryModel, CustomerFeedbacksQueryCollection> result = Query(prp, select, from, condition, orderByBuilder);
            return result;
        }
    }
}