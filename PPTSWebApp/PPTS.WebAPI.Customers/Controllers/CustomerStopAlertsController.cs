using MCS.Library.Data;
using MCS.Library.Principal;
using MCS.Web.MVC.Library.Filters;
using PPTS.Data.Common.Adapters;
using PPTS.Data.Common.Security;
using PPTS.Data.Customers.Entities;
using PPTS.Web.MVC.Library.Filters;
using PPTS.WebAPI.Customers.DataSources;
using PPTS.WebAPI.Customers.Executors;
using PPTS.WebAPI.Customers.ViewModels.StopAlerts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace PPTS.WebAPI.Customers.Controllers
{
    [ApiPassportAuthentication]
    public class CustomerStopAlertsController : ApiController
    {
        #region api/customer/StopAlerts/getAllStopAlerts

        /// <summary>
        /// 停课休学记录查询，第一次。第一页，下载字典
        /// </summary>
        /// <param name="criteria">查询条件</param>
        /// <returns>返回带字典的跟进记录列表</returns>
        [HttpPost]
        [PPTSJobFunctionAuthorize("PPTS:学员视图-停课休学/退费预警,学员视图-停课休学/退费预警-本部门,学员视图-停课休学/退费预警-本校区,学员视图-停课休学/退费预警-本分公司,学员视图-停课休学/退费预警-全国")]
        public StopAlertQueryResult GetAllStopAlerts(StopAlertCriteriaModel criteria)
        {
            return new StopAlertQueryResult
            {
                QueryResult = CustomerStopAlertDataSource.Instance.LoadCustomerStopAlerts(criteria.PageParams, criteria, criteria.OrderBy),
                Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(CustomerStopAlerts), typeof(StopAlertCreateModel))
            };
        }

        /// <s6ummary>
        /// 停课休学记录查询，翻页或排序。不下载字典
        /// </summary>
        /// <param name="criteria">查询条件</param>
        /// <returns>返回不带字典的跟进记录列表</returns>
        [HttpPost]
        [PPTSJobFunctionAuthorize("PPTS:学员视图-停课休学/退费预警,学员视图-停课休学/退费预警-本部门,学员视图-停课休学/退费预警-本校区,学员视图-停课休学/退费预警-本分公司,学员视图-停课休学/退费预警-全国")]
        public PagedQueryResult<StopAlertQueryModel, StopAlertQueryCollection> GetPagedStopAlerts(StopAlertCriteriaModel criteria)
        {
            return CustomerStopAlertDataSource.Instance.LoadCustomerStopAlerts(criteria.PageParams, criteria, criteria.OrderBy);
        }

        #endregion

        #region api/customer/StopAlerts/createStopAlert

        /// <summary>
        /// 新建停课休学记录
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public StopAlertCreateModel CreateStopAlert()
        {
            return StopAlertCreateModel.CreateStopAlert();
        }

        /// <summary>
        /// 新增停课休学记录
        /// </summary>
        /// <param name="model"></param>
        [HttpPost]
        [PPTSJobFunctionAuthorize("PPTS:新增停课/休学")]
        public void CreateStopAlert(StopAlertCreateModel model)
        {
            #region 写入权限验证
            PPTS.Data.Common.Authorization.ScopeAuthorization<CustomerStopAlerts>
               .GetInstance(Data.Customers.ConnectionDefine.PPTSCustomerConnectionName).CheckEditAuth("", model.StopAlert.CustomerID);
            #endregion

            model.StopAlert.OperatorID = DeluxeIdentity.CurrentUser.ID;
            model.StopAlert.OperatorJobID = DeluxeIdentity.CurrentUser.GetCurrentJob().ID;
            model.StopAlert.OperatorJobName = DeluxeIdentity.CurrentUser.GetCurrentJob().Name;
            model.StopAlert.OperatorName = DeluxeIdentity.CurrentUser.DisplayName;

            AddCustomerStopAlertExecutor executor = new AddCustomerStopAlertExecutor(model);
            executor.Execute();
        }

        #endregion
    }
}
