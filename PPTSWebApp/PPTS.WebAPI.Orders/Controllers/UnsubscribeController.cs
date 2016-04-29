using MCS.Library.Data;
using PPTS.Data.Common.Adapters;
using PPTS.Data.Orders.DataSources;
using PPTS.Data.Orders.Entities;
using PPTS.WebAPI.Orders.ViewModels.Unsubscribe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PPTS.WebAPI.Orders.Controllers
{
    public class UnsubscribeController : ApiController
    {

        #region 退订

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
                Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(DebookOrder)),
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




        
    }
}
