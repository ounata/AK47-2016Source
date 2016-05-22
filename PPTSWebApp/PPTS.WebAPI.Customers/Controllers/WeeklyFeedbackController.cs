using MCS.Library.Data;
using MCS.Web.MVC.Library.Filters;
using PPTS.Data.Common.Adapters;
using PPTS.Data.Customers.Entities;
using PPTS.WebAPI.Customers.DataSources;
using PPTS.WebAPI.Customers.ViewModels.Feedback;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PPTS.WebAPI.Customers.Controllers
{
    /// <summary>
    /// 客户反馈Controller
    /// </summary>
    [ApiPassportAuthentication]
    public class WeeklyFeedbackController : ApiController
    {
        #region api/feedback/getcustomerfeedbackList
        /// <summary>
        /// 客户反馈默认查询
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        [HttpPost]
        public CustomerFeedbacksQueryResult GetCustomerFeedbackList(CustomerFeedbacksCriteriaModel criteria)
        {
            return new CustomerFeedbacksQueryResult
            {
                QueryResult = CustomerFeedbacksDataSource.Instance.GetCustomerFeedbackList(criteria.PageParams, criteria, criteria.OrderBy),
                Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(CustomerFeedbacksQueryModel), typeof(Parent))
            };
        }
        #endregion

        #region api/feedback/getpagedcustomerfeedbacklist
        /// <summary>
        ///客户反馈分页查询
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        [HttpPost]
        public PagedQueryResult<CustomerFeedbacksQueryModel, CustomerFeedbacksQueryCollection> GetPagedCustomerFeedbackList(CustomerRepliesCriteriaModel criteria)
        {
            return CustomerFeedbacksDataSource.Instance.GetCustomerFeedbackList(criteria.PageParams, criteria, criteria.OrderBy);
        }

        #endregion

    }
}
