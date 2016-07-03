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
using PPTS.Data.Products.Entities;
using PPTS.Data.Orders.Adapters;
using PPTS.Web.MVC.Library.Filters;
using MCS.Web.MVC.Library.Models.Workflow;

namespace PPTS.WebAPI.Orders.Controllers
{
    [ApiPassportAuthentication]
    public class UnsubscribeController : ApiController
    {

        #region 退订列表


        [PPTSJobFunctionAuthorize("PPTS:退订管理列表,退订管理列表-本部门,退订管理列表-本校区,退订管理列表-本分公司,退订管理列表-全国")]
        [HttpPost]
        public DebookOrderQueryResult GetAllDebookOrders(DebookOrderCriteriaModel criteria)
        {

            return new DebookOrderQueryResult
            {
                QueryResult = GenericPurchaseSource<DebookOrderItemView, DebookOrderItemViewCollection>.Instance.Query(criteria.PageParams, criteria, criteria.OrderBy),
                Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(Product), typeof(DebookOrder), typeof(OrderItemView), typeof(DebookOrderItemView)),
            };
        }


        [PPTSJobFunctionAuthorize("PPTS:退订管理列表,退订管理列表-本部门,退订管理列表-本校区,退订管理列表-本分公司,退订管理列表-全国")]
        [HttpPost]
        public PagedQueryResult<DebookOrderItemView, DebookOrderItemViewCollection> GetPagedDebookOrders(DebookOrderCriteriaModel criteria)
        {
            return GenericPurchaseSource<DebookOrderItemView, DebookOrderItemViewCollection>.Instance.Query(criteria.PageParams, criteria, criteria.OrderBy);
        }

        #endregion

        [PPTSJobFunctionAuthorize("PPTS:退订")]
        [HttpPost]
        public void Unsubscribe(DebookOrderModel model)
        {
            model.OrderItemID.CheckStringIsNullOrEmpty("OrderItemID");

            model.CurrentUser = DeluxeIdentity.CurrentUser;
            new DebookOrderExecutor(model) { NeedValidation=true }.Execute();
        }


        //

        #region 导出

        [PPTSJobFunctionAuthorize("PPTS:退订列表导出")]
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
//{"学生编号", "CustomerCode"},
{"家长姓名", "ParentName"},
{"订购单编号", "OrderNo"},
{"产品类型", "CategoryType"},
{"实际单价", "RealPrice"},
{"订购数量", "OrderAmount"},
{"订购金额", "BookMoney"},
{"已上数量", "ConfirmedAmount"},
{"已使用金额", "ConfirmedMoney"},
{"退订数量(赠送)", "DebookAmountAndPreset"},
{"退订金额", "DebookMoney"},
{"退订日期", "DebookTime"},
{"退订申请人", "SubmitterName"},
 };
            columns.ToList().ForEach(kv =>
            {
                tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn(kv.Key, typeof(string))) { PropertyName = kv.Value });
            });

            var dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(DebookOrderItemView), typeof(Product));
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
                    case "CategoryType":
                        var ct = dictionaries["c_codE_ABBR_Product_CategoryType"].Where(c => c.Key == Convert.ToString(param.PropertyValue));
                        cell.Value = null != ct ? (null == ct.FirstOrDefault() ? null : ct.FirstOrDefault().Value) : null;
                        break;
                    default:
                        cell.Value = param.PropertyValue;
                        break;
                }
            });

            return wb.ToResponseMessage("退订列表.xlsx");
        }


        #endregion

        #region api/unsubscribe/getDebookOrderDetial
        [HttpGet]
        public DebookOrderItemView GetDebookOrderDetial(string id) {
            DebookOrderItemView result = DebookOrderItemViewAdapter.Instance.Load(builder => builder.AppendItem("DebookID", id)).SingleOrDefault();
            return result;
        }
        #endregion

        [HttpPost]
        public dynamic GetDebookOrderByWorkflow(dynamic wfParams)
        {
            WfClientSearchParameters p = new WfClientSearchParameters();

            p.ResourceID = wfParams.resourceID;
            p.ActivityID = wfParams.activityID;
            p.ProcessID = wfParams.processID;

            var model = DebookOrderItemViewAdapter.Instance.Load(builder => builder.AppendItem("DebookID", p.ResourceID)).SingleOrDefault();
            return new { ClientProcess = WfClientProxy.GetClientProcess(p), Model = model };
        }

    }
}
