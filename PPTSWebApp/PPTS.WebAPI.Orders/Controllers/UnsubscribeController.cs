using MCS.Library.Data;
using MCS.Web.MVC.Library.Filters;
using PPTS.Data.Common.Adapters;
using PPTS.Data.Orders.DataSources;
using PPTS.Data.Orders.Entities;
using PPTS.WebAPI.Orders.Executors;
using PPTS.WebAPI.Orders.ViewModels.Unsubscribe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PPTS.Data.Common.Security;
using MCS.Library.Principal;
using System.Web.Http.Results;

namespace PPTS.WebAPI.Orders.Controllers
{
    [ApiPassportAuthentication]
    public class UnsubscribeController : ApiController
    {

        #region 退订列表

        /// <summary>
        /// 查询，第一次。第一页，下载字典
        /// </summary>
        /// <param name="criteria">查询条件</param>
        /// <returns>返回带字典的潜客数据列表</returns>
        [HttpPost]
        public DebookOrderQueryResult GetAllDebookOrders(DebookOrderCriteriaModel criteria)
        {

            return new DebookOrderQueryResult
            {
                QueryResult = GenericPurchaseSource<DebookOrder, DebookOrderCollection>.Instance.Query(criteria.PageParams, criteria, criteria.OrderBy),
                Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(DebookOrder), typeof(OrderItemView)),
            };
        }

        /// <summary>
        /// 查询，翻页或排序。不下载字典
        /// </summary>
        /// <param name="criteria">查询条件</param>
        /// <returns>返回不带字典的潜客数据列表</returns>
        [HttpPost]
        public PagedQueryResult<DebookOrder, DebookOrderCollection> GetPagedDebookOrders(DebookOrderCriteriaModel criteria)
        {
            return GenericPurchaseSource<DebookOrder, DebookOrderCollection>.Instance.Query(criteria.PageParams, criteria, criteria.OrderBy);
        }

        #endregion

        [HttpPost]
        public HttpResponseMessage Unsubscribe(DebookOrderModel model)
        {

            model.Order.CreatorID = DeluxeIdentity.CurrentUser.ID;
            model.Order.CreatorName = DeluxeIdentity.CurrentUser.DisplayName;
            model.Order.SubmitterID = DeluxeIdentity.CurrentUser.ID;
            model.Order.SubmitterJobID = DeluxeIdentity.CurrentUser.GetCurrentJob().ID;
            model.Order.SubmitterJobName = DeluxeIdentity.CurrentUser.GetCurrentJob().Name;
            model.Order.SubmitterName = DeluxeIdentity.CurrentUser.DisplayName;

            string message = string.Empty;
            var returnValue = (int)new DebookOrderExecutor(model).Execute();
            switch (returnValue)
            {
                case -1: message = "有未完成订购操作的，不能退订"; break;
            }
            //跨库操作
            //case -2: message = "有未完成退费操作的，不能退订"; break;
            //case -3: message = "有未完成转学操作的，不能退订"; break;

            var statusCode = HttpStatusCode.OK;
            if (!string.IsNullOrWhiteSpace(message))
            {
                statusCode = HttpStatusCode.PreconditionFailed;
            }
            return Request.CreateErrorResponse(statusCode, message);

        }


    }
}
