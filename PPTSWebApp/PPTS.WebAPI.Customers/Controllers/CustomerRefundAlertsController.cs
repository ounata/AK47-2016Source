using MCS.Library.Data;
using MCS.Library.Principal;
using MCS.Web.MVC.Library.Filters;
using PPTS.Data.Common.Adapters;
using PPTS.Data.Common.Security;
using PPTS.Data.Customers.Entities;
using PPTS.WebAPI.Customers.DataSources;
using PPTS.WebAPI.Customers.Executors;
using PPTS.WebAPI.Customers.ViewModels.RefundAlerts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace PPTS.WebAPI.Customers.Controllers
{
    [ApiPassportAuthentication]
    public class CustomerRefundAlertsController : ApiController
    {
        #region api/customer/RefundAlerts/getAllRefundAlerts

        /// <summary>
        /// 退费预警记录查询，第一次。第一页，下载字典
        /// </summary>
        /// <param name="criteria">查询条件</param>
        /// <returns>返回带字典的跟进记录列表</returns>
        [HttpPost]
        public RefundAlertQueryResult GetAllRefundAlerts(RefundAlertCrieriaModel criteria)
        {
            return new RefundAlertQueryResult
            {
                QueryResult = CustomerRefundAlertDataSource.Instance.LoadCustomerStopAlerts(criteria.PageParams, criteria, criteria.OrderBy),
                Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(CustomerRefundAlerts), typeof(RefundAlertCreateModel))
            };
        }

        /// <s6ummary>
        /// 退费预警记录查询，翻页或排序。不下载字典
        /// </summary>
        /// <param name="criteria">查询条件</param>
        /// <returns>返回不带字典的跟进记录列表</returns>
        [HttpPost]
        public PagedQueryResult<RefundAlertQueryModel, RefundAlertQueryCollection> GetPagedRefundAlerts(RefundAlertCrieriaModel criteria)
        {
            return CustomerRefundAlertDataSource.Instance.LoadCustomerStopAlerts(criteria.PageParams, criteria, criteria.OrderBy);
        }

        #endregion

        #region api/customer/RefundAlerts/createRefundAlert

        /// <summary>
        /// 新建退费预警记录
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public RefundAlertCreateModel CreateRefundAlert()
        {
            return RefundAlertCreateModel.CreateRefundAlert();
        }

        /// <summary>
        /// 新增退费预警记录
        /// </summary>
        /// <param name="model"></param>
        [HttpPost]
        public void CreateRefundAlert(RefundAlertCreateModel model)
        {
            model.RefundAlert.OperatorID = DeluxeIdentity.CurrentUser.ID;
            model.RefundAlert.OperatorJobID = DeluxeIdentity.CurrentUser.GetCurrentJob().ID;
            model.RefundAlert.OperatorJobName = DeluxeIdentity.CurrentUser.GetCurrentJob().Name;
            model.RefundAlert.OperatorName = DeluxeIdentity.CurrentUser.DisplayName;

            AddCustomerRefundAlertExecutor executor = new AddCustomerRefundAlertExecutor(model);
            executor.Execute();
        }

        #endregion

        #region api/customer/RefundAlerts/updateRefundAlert

        [HttpGet]
        public RefundAlertEditModel UpdateRefundAlert(string id)
        {
            return RefundAlertEditModel.Load(id);
        }

        /// <summary>
        /// 可编辑项只在结账日前可编辑，结账日后不可编辑；（解释：每月1日晚上24前可编辑上个月至当天的，每月1日晚上24后，只能编辑当月的）
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public RefundAlertEditModel IsCurrentMonthAlert(string id)
        {
            RefundAlertEditModel editModel = new RefundAlertEditModel();
            editModel.IsEditor = true;
            return editModel;
        }

        /// <summary>
        /// 编辑退费预警记录
        /// </summary>
        /// <param name="model"></param>
        [HttpPost]
        public void UpdateRefundAlert(RefundAlertEditModel model)
        {
            model.RefundAlert.OperatorID = DeluxeIdentity.CurrentUser.ID;
            model.RefundAlert.OperatorJobID = DeluxeIdentity.CurrentUser.GetCurrentJob().ID;
            model.RefundAlert.OperatorJobName = DeluxeIdentity.CurrentUser.GetCurrentJob().Name;
            model.RefundAlert.OperatorName = DeluxeIdentity.CurrentUser.DisplayName;

            EditCustomerRefundAlertExecutor executor = new EditCustomerRefundAlertExecutor(model);
            executor.Execute();
        }

        #endregion
    }
}
