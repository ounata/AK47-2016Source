using System.Linq;
using System.Web.Http;
using MCS.Library.Data;
using PPTS.Data.Common.Adapters;
using PPTS.Data.Customers.Entities;
using PPTS.WebAPI.Customers.ViewModels.CustomerMeetings;
using PPTS.WebAPI.Customers.DataSources;

namespace PPTS.WebAPI.Customers.Controllers
{
    /// <summary>
    /// 教学服务会Controller
    /// </summary>
    public class CustomerMeetingsController : ApiController
    {

        #region api/customermeetings/getallcustomermeetings
        /// <summary>
        /// 教学服务会默认查询
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        [HttpPost]
        public CustomerMeetingQueryResult GetAllCustomerMeetings(CustomerMeetingCriteriaModel criteria)
        {
            return new CustomerMeetingQueryResult
            {
                QueryResult = CustomerMeetingDataSource.Instance.GetCustomerMeetingsList(criteria.PageParams, criteria, criteria.OrderBy),
                Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(CustomerMeetingQueryModel), typeof(Parent))
            };
        }
        #endregion

        #region api/customermeetings/getpagedcustomermeetings
        /// <summary>
        /// 教学服务会分页查询
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        [HttpPost]
        public PagedQueryResult<CustomerMeetingQueryModel, CustomerMeetingQueryCollection> GetPagedCustomerMeetings(CustomerMeetingCriteriaModel criteria)
        {
            return CustomerMeetingDataSource.Instance.GetCustomerMeetingsList(criteria.PageParams, criteria, criteria.OrderBy);
        }
        #endregion
    }
}