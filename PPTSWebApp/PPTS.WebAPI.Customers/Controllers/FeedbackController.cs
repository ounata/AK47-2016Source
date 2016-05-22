using MCS.Library.Data;
using MCS.Library.Data.Executors;
using MCS.Web.MVC.Library.Filters;
using PPTS.Data.Common.Adapters;
using PPTS.Data.Customers.Entities;
using PPTS.WebAPI.Customers.DataSources;
using PPTS.WebAPI.Customers.Executors;
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
    /// 学大反馈Controller
    /// </summary>
    [ApiPassportAuthentication]
    public class FeedbackController : ApiController
    {
        #region api/feedback/getcustomerrepliesList

        /// <summary>
        /// 只获取数据词典(特殊需要)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public CustomerRepliesQueryResult LoadDictionaries()
        {
            return new CustomerRepliesQueryResult
            {
                Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(CustomerRepliesQueryModel), typeof(Parent))
            };
        }
        /// <summary>
        /// 学大反馈默认查询
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        [HttpPost]
        public CustomerRepliesQueryResult GetCustomerRepliesList(CustomerRepliesCriteriaModel criteria)
        {
            return new CustomerRepliesQueryResult
            {
                QueryResult = CustomerRepliesDataSource.Instance.GetCustomerRepliesList(criteria.PageParams, criteria, criteria.OrderBy),
                Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(CustomerRepliesQueryModel), typeof(Parent))
            };
        }
        #endregion

        #region api/feedback/getpagedcustomerreplieslist
        /// <summary>
        ///学大反馈分页查询
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        [HttpPost]
        public PagedQueryResult<CustomerRepliesQueryModel, CustomerRepliesQueryCollection> GetPagedCustomerRepliesList(CustomerRepliesCriteriaModel criteria)
        {
            return CustomerRepliesDataSource.Instance.GetCustomerRepliesList(criteria.PageParams, criteria, criteria.OrderBy);
        }

        #endregion

        #region api/feedback/createcustomerreplies
        [HttpPost]
        public void CreateCustomerReplies(EditCustomerRepliesModel model)
        {
            CustomerRepliesExecutor crExecutor = new CustomerRepliesExecutor(model);
            crExecutor.Execute();
        }
        #endregion
    }
}
