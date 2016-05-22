using System.Linq;
using System.Web.Http;
using MCS.Library.Data;
using PPTS.Data.Common.Adapters;
using PPTS.Data.Customers.Entities;
using PPTS.WebAPI.Customers.ViewModels.CustomerMeetings;
using PPTS.WebAPI.Customers.DataSources;
using PPTS.WebAPI.Customers.Executors;
using MCS.Library.Principal;
using System;
using MCS.Web.MVC.Library.Filters;
using PPTS.Data.Customers.Adapters;
using PPTS.Data.Common;

namespace PPTS.WebAPI.Customers.Controllers
{
    /// <summary>
    /// 教学服务会Controller
    /// </summary>
    [ApiPassportAuthentication]
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

        #region api/customermeetings/savecustomermeetings
        /// <summary>
        /// 新增/编辑会议
        /// </summary>
        /// <param name="criteria"></param>
        [HttpPost]
        //[ApiPassportAuthentication]
        public void SaveCustomerMeetings(EditCustomerMeetingModel model)
        {
            CustomerMeetingExecutor cmExcutor = new CustomerMeetingExecutor(model);
            cmExcutor.Execute();
        }

        #endregion


        #region api/customermeetings/loadcustomermeetingsdictionaries
        /// <summary>
        /// 加载数据词典
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        [HttpPost]
        public CustomerMeetingQueryResult LoadCustomerMeetingsDictionaries(CustomerMeetingCriteriaModel criteria)
        {
            return new CustomerMeetingQueryResult
            {
                Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(CustomerMeetingQueryModel),typeof(CustomerMeetingItem), typeof(Parent))
            };
        }
        #endregion

        #region api/customermeetings/getCustomerMeeting
        /// <summary>
        /// 根据会议ID获取获取会议
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public EditCustomerMeetingModel GetCustomerMeeting(string id)
        {
            return new EditCustomerMeetingModel
            {
                CustomerMeeting = CustomerMeetingAdapter.Instance.Load(id),
                Items = CustomerMeetingItemAdapter.Instance.LoadItems(id),
                Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(CustomerMeetingQueryModel), typeof(CustomerMeetingItem), typeof(Parent))
            };
        }
        #endregion
    }
}