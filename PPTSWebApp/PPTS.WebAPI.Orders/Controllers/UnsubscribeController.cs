using MCS.Library.Core;
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
using PPTS.WebAPI.Orders.ViewModels.Purchase;
using MCS.Web.MVC.Library.ModelBinder;
using System.Web.Http.ModelBinding;
using MCS.Library.Office.OpenXml.Excel;
using System.Data;
using MCS.Web.MVC.Library.ApiCore;

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
                QueryResult = GenericPurchaseSource<DebookOrderItemView, DebookOrderItemViewCollection>.Instance.Query(criteria.PageParams, criteria, criteria.OrderBy),
                Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(DebookOrder), typeof(OrderItemView), typeof(DebookOrderItemView)),
            };
        }

        /// <summary>
        /// 查询，翻页或排序。不下载字典
        /// </summary>
        /// <param name="criteria">查询条件</param>
        /// <returns>返回不带字典的潜客数据列表</returns>
        [HttpPost]
        public PagedQueryResult<DebookOrderItemView, DebookOrderItemViewCollection> GetPagedDebookOrders(DebookOrderCriteriaModel criteria)
        {
            return GenericPurchaseSource<DebookOrderItemView, DebookOrderItemViewCollection>.Instance.Query(criteria.PageParams, criteria, criteria.OrderBy);
        }

        #endregion

        [HttpPost]
        public void Unsubscribe(DebookOrderModel model)
        {
            model.OrderItemID.CheckStringIsNullOrEmpty("OrderItemID");

            model.CurrentUser = DeluxeIdentity.CurrentUser;
            new DebookOrderExecutor(model) { NeedValidation=true }.Execute();
        }


        //

        #region 导出

        [HttpPost]
        public HttpResponseMessage ExportDebookItemList([ModelBinder(typeof(FormBinder))]DebookOrderCriteriaModel criteria)
        {

            var wb = WorkBook.CreateNew();
            var sheet = wb.Sheets["sheet1"];
            var tableDesp = new TableDescription("退订列表");
            criteria.PageParams.PageIndex = 0;
            criteria.PageParams.PageSize = 0;
            var pageData = GenericPurchaseSource<DebookOrderItemView, DebookOrderItemViewCollection>.Instance.Query(criteria.PageParams, criteria, criteria.OrderBy);

            var columns = new Dictionary<string, string>() {
{ "校区", "CampusName"},
{"学生姓名", "CustomerName"},
{"学生编号", "CustomerCode"},
{"家长姓名", "ParentName"},
{"订购单编号", "OrderNo"},
//{"订购日期", "OrderTime"},
//{"产品类型", "CatalogName"},
//{"年级", "Grade"},
//{"科目", "Subject"},
//{"课程级别", "CourseLevel"},
//{"实际单价(元)", "RealPrice"},
//{"客户折扣率", "DiscountRate"},
//{"订购数量", "OrderAmount"},
//{"订购金额(元)", "BookMoney"},
//{"赠送数量", "PresentAmount"},
//{"已退数量", "DebookedAmount"},
//{"已排数量", "AssignedAmount"},
//{"已上数量", "ConfirmedAmount"},
//{"剩余数量", "RemainCount"},
//{"订单状态", "OrderStatus"},
//{"订购操作人", "SubmitterName"},
//{"操作人岗位", "SubmitterJobName"},
 };
            columns.ToList().ForEach(kv =>
            {
                tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn(kv.Key, typeof(string))) { PropertyName = kv.Value });
            });

            var dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(DebookOrderItemView));
            sheet.LoadFromCollection(pageData.PagedData, tableDesp, (cell, param) =>
            {

                switch (param.ColumnDescription.PropertyName)
                {
                    case "CourseLevel":
                        var cl = dictionaries["c_codE_ABBR_Product_CourseLevel"].Where(c => c.Key == Convert.ToString(param.PropertyValue));
                        cell.Value = null != cl ? (null == cl.FirstOrDefault() ? null : cl.FirstOrDefault().Value) : null;
                        break;
                    case "Grade":
                        var g = dictionaries["c_codE_ABBR_CUSTOMER_GRADE"].Where(c => c.Key == Convert.ToString(param.PropertyValue));
                        cell.Value = null != g ? (null == g.FirstOrDefault() ? null : g.FirstOrDefault().Value) : null;
                        break;
                    case "Subject":
                        var rt = dictionaries["C_CODE_ABBR_BO_Product_TeacherSubject"].Where(c => c.Key == Convert.ToString(param.PropertyValue));
                        cell.Value = null != rt ? (null == rt.FirstOrDefault() ? null : rt.FirstOrDefault().Value) : null;
                        break;
                    case "OrderStatus":
                        var ro = dictionaries["c_codE_ABBR_Order_OrderStatus"].Where(c => c.Key == Convert.ToString(param.PropertyValue));
                        cell.Value = null != ro ? (null == ro.FirstOrDefault() ? null : ro.FirstOrDefault().Value) : null;
                        break;
                    default:
                        cell.Value = param.PropertyValue;
                        break;
                }
            });

            return wb.ToResponseMessage("退订列表.xlsx");
        }


        #endregion

    }
}
