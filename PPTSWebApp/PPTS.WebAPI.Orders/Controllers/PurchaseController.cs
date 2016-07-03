using MCS.Library.Data;
using MCS.Library.Core;
using PPTS.Data.Common.Adapters;
using PPTS.Data.Orders.DataSources;
using PPTS.Data.Orders.Entities;
using PPTS.Data.Orders.Executors;
using PPTS.Data.Products.Entities;
using PPTS.WebAPI.Orders.Executors;
using PPTS.WebAPI.Orders.ViewModels.Purchase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PPTS.Data.Orders.Adapters;
using MCS.Web.MVC.Library.Filters;
using MCS.Library.Principal;
using PPTS.Data.Common.Security;

using PPTS.WebAPI.Orders.ViewModels.Exchange;
using System.Web.Http.ModelBinding;
using MCS.Web.MVC.Library.ModelBinder;
using MCS.Library.Office.OpenXml.Excel;
using System.Data;
using MCS.Web.MVC.Library.ApiCore;
using PPTS.WebAPI.Orders.DataSources;
using PPTS.Web.MVC.Library.Filters;
using MCS.Web.MVC.Library.Models.Workflow;

namespace PPTS.WebAPI.Orders.Controllers
{
    [ApiPassportAuthentication]
    public class PurchaseController : ApiController
    {
        #region 订购列表

        //
        [PPTSJobFunctionAuthorize("PPTS:订购管理列表（订购单详情）,订购管理列表（订购单详情）-本部门,订购管理列表（订购单详情）-本校区,订购管理列表（订购单详情）-本分公司,订购管理列表（订购单详情）-全国,学员视图-充值记录/订购历史/转让记录,学员视图-充值记录/订购历史/转让记录-本部门,学员视图-充值记录/订购历史/转让记录-本校区,学员视图-充值记录/订购历史/转让记录-本分公司")]
        [HttpPost]
        public OrderItemViewQueryResult GetAllOrderItems(OrderItemViewCriteriaModel criteria)
        {
            return new OrderItemViewQueryResult
            {
                QueryResult = GenericPurchaseSource<OrderItemView, OrderItemViewCollection>.Instance.Query(criteria.PageParams, criteria, criteria.OrderBy),
                Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(OrderItemView),typeof(Product)),
            };
        }


        [PPTSJobFunctionAuthorize("PPTS:订购管理列表（订购单详情）,订购管理列表（订购单详情）-本部门,订购管理列表（订购单详情）-本校区,订购管理列表（订购单详情）-本分公司,订购管理列表（订购单详情）-全国")]
        [HttpPost]
        public PagedQueryResult<OrderItemView, OrderItemViewCollection> GetPagedProducts(OrderItemViewCriteriaModel criteria)
        {
            return GenericPurchaseSource<OrderItemView, OrderItemViewCollection>.Instance.Query(criteria.PageParams, criteria, criteria.OrderBy);
        }

        [PPTSJobFunctionAuthorize("PPTS:资产兑换,打印订单凭证,打印订单凭证-本校区,打印订单凭证-本分公司")]
        [HttpPost]
        public dynamic GetOrderItemView(dynamic data)
        {
            string id = data.id;
            return new
            {
                Entity = OrderItemViewAdapter.Instance.Load(id),
                Expense = Service.CustomerService.GetCustomerExpenseByOrderId(id),
                Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(OrderItemView)),
            };
        }

        #endregion

        #region 常规订购


        #endregion

        #region 插班订购

        [PPTSJobFunctionAuthorize("PPTS:订购")]
        [HttpPost]
        public PagedQueryResult<Class, ClassCollection> GetPagedClasses(ClassCriteriaModel criteria)
        {
            var result = GenericPurchaseSource<Class, ClassCollection>.Instance.Query(criteria.PageParams, criteria, criteria.OrderBy);
            return result;
        }

        #endregion

        #region 用户扣除服务费

        [PPTSJobFunctionAuthorize("PPTS:订购")]
        [HttpPost]
        public dynamic GetServiceChargeByUserId(dynamic data)
        {
            string customerId = data.customerId;
            var customer = Service.CustomerService.GetCustomerByCustomerId(customerId);
            (customerId == null).TrueThrow(customerId+"该学员不存在！");
            string campusId = customer.CampusID;

            //return new
            //{
            //    DeductList = Service.CustomerService.GetWhetherToDeductServiceChargeByCustomerId(customerId),
            //    ServiceCharge = Service.ProductService.GetServiceChargeByCampusId(campusId)
            //};

            var kfdic = Service.CustomerService.GetWhetherToDeductServiceChargeByCustomerId(customerId);
            var serviceCharges = Service.ProductService.GetServiceChargeByCampusId(campusId);

            var result = new Dictionary<int,dynamic>();
            //kfdic.ForEach(kv => { result.Add(new { kv.Key,kv.Value,SC= serviceCharges.SingleOrDefault(m=>m.ExpenseType==kv.Key.ToString()) }); });
            kfdic.ForEach(kv => { result.Add(kv.Key, new { kv.Value, SC = serviceCharges.SingleOrDefault(m => m.ExpenseType == kv.Key.ToString()) }); });

            return new { DeductList= result };
        }

        #endregion

        #region 查看订单

        [PPTSJobFunctionAuthorize("PPTS:订购管理列表（订购单详情）,订购管理列表（订购单详情）-本部门,订购管理列表（订购单详情）-本校区,订购管理列表（订购单详情）-本分公司,订购管理列表（订购单详情）-全国")]
        [HttpPost]
        public OrderModel GetOrder(dynamic data)
        {
            string orderId = data.orderId;
            
            return OrderModel.FillOrderAndItemViewByOrderId(orderId, null).FillAccount().FillChargePayment();

        }

        #endregion

        /// <summary>
        /// 获取购物车
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [PPTSJobFunctionAuthorize("PPTS:订购")]
        [HttpPost]
        public ShoppingCartModel GetShoppingCart(dynamic data)
        {
            string orgId = data.campusId;
            string customerId = data.customerId;
            //查看清单类型 1：普通 2：买赠 3：插班
            int listType = data.listType;

            return ShoppingCartModel.FillCart(customerId, orgId, listType)
                            .FillAccount()
                            .FillPreset()
                            .FillClassGroupAmount()
                            .FillChargePays()
                            .SetDiscount();

        }

        /// <summary>
        /// 删除已选购产品
        /// </summary>
        /// <param name="cartIds"></param>
        /// <returns></returns>
        [PPTSJobFunctionAuthorize("PPTS:订购")]
        [HttpPost]
        public bool DeleteShoppingCart(params string[] cartIds)
        {
            return (bool)new PPTSShoppingCartExecutor("DelShoppingCart") { CartIds = cartIds }.Execute();
        }

        /// <summary>
        /// 加入购物车
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        [PPTSJobFunctionAuthorize("PPTS:订购")]
        [HttpPost]
        public void AddShoppingCart(ShoppingCartCollection collection)
        {
            collection.NullCheck("提交数据有误！");

            collection.ForEach(m =>
            {
                m.Amount = 1;
                DeluxeIdentity.CurrentUser.FillCreatorInfo(m);
            });
            new AddShoppingCartExecutor(collection).Execute();

        }

        /// <summary>
        /// 提交订单
        /// </summary>
        /// <param name="model"></param>
        [PPTSJobFunctionAuthorize("PPTS:订购")]
        [HttpPost]
        public void SubmitShoppingCart(SubmitOrderModel model)
        {
            model.NullCheck("model");
            model.CustomerID.CheckStringIsNullOrEmpty("model.CustomerID");
            model.AccountID.CheckStringIsNullOrEmpty("model.AccountId");

            model.CurrentUser = DeluxeIdentity.CurrentUser;
            new AddOrderExecutor(model) { NeedValidation = true }.Execute();

        }

        #region 订购历史


        /// <summary>
        /// 查看历史确认
        /// </summary>
        /// <param name="itemID"></param>
        /// <returns></returns>
        [PPTSJobFunctionAuthorize("PPTS:学员视图-充值记录/订购历史/转让记录,学员视图-充值记录/订购历史/转让记录-本部门,学员视图-充值记录/订购历史/转让记录-本校区,学员视图-充值记录/订购历史/转让记录-本分公司")]
        public dynamic GetRecognizingIncomeList(string itemID)
        {
            return new { List= AssetConfirmAdapter.Instance.Load(itemID, null) } ;
        }

        /// <summary>
        /// 确认非上课类收入
        /// </summary>
        /// <param name="model"></param>
        [HttpPost]
        public void AddRecognizingIncome(AssetConfirmModel model)
        {
            model.CurrentUser = DeluxeIdentity.CurrentUser;
            new AddAssetConfirmExecutor(model) { NeedValidation=true }.Execute();
        }

        #region 资产兑换

        /// <summary>
        /// 资产兑换 获取订单 及 要兑换的产品信息
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [PPTSJobFunctionAuthorize("PPTS:资产兑换")]
        [HttpPost]
        public ExchangeAmountModel GetExchangeInfo(dynamic data)
        {
            string itemId = data.itemId;
            string productId = data.productId;
            return new ExchangeAmountModel(itemId, productId);

        }

        [PPTSJobFunctionAuthorize("PPTS:资产兑换")]
        [HttpPost]
        public void ExchangeOrder(ExchangeOrderModel model)
        {
            new ExchangeExecutor(model).Execute();
        }

        /// <summary>
        /// 获取产品信息
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        [HttpPost]
        public ProductView GetProductByID(dynamic param)
        {
            string productId = param.productId;
            return Service.ProductService.GetProductsByIds(productId).FirstOrDefault();
        }

        #endregion

        #endregion



        #region 编辑缴费单

        [PPTSJobFunctionAuthorize("PPTS:编辑关联缴费单-本分公司")]
        [HttpPost]
        public OrderModel GetEditPayment(dynamic data)
        {
            string orderId = data.orderid;
            return OrderModel.FillOrderAndItemViewByOrderId(orderId, null).FillPayments();
        }


        [PPTSJobFunctionAuthorize("PPTS:编辑关联缴费单-本分公司")]
        [HttpPost]
        public void EditPayment(EditPaymentModel model)
        {
            model.CurrentUser = DeluxeIdentity.CurrentUser;
            new PPTSOrderExecutor("EditPayment") {  Order= model.FillOrder().Order, CampusIDs= null }.Execute();
        }

        #endregion

        #region 导出

        [PPTSJobFunctionAuthorize("PPTS:订购列表导出")]
        [HttpPost]
        public HttpResponseMessage ExportOrderItemList([ModelBinder(typeof(FormBinder))]OrderItemViewCriteriaModel criteria)
        {

            var wb = WorkBook.CreateNew();
            var sheet = wb.Sheets["sheet1"];
            var tableDesp = new TableDescription("订购列表");
            criteria.PageParams.PageIndex = 0;
            criteria.PageParams.PageSize = 0;
            var pageData = GenericPurchaseSource<OrderItemView, OrderItemViewCollection>.Instance.Query(criteria.PageParams, criteria, criteria.OrderBy);

            var columns = new Dictionary<string, string>() {
{ "校区", "CampusName"},
{"学生姓名", "CustomerName"},
{"学生编号", "CustomerCode"},
{"家长姓名", "ParentName"},
{"订购单编号", "OrderNo"},
{"订购日期", "OrderTime"},
{"产品类型", "CatalogName"},
{"年级", "Grade"},
{"科目", "Subject"},
{"课程级别", "CourseLevel"},
{"实际单价(元)", "RealPrice"},
{"客户折扣率", "DiscountRate"},
{"订购数量", "OrderAmount"},
{"订购金额(元)", "BookMoney"},
{"赠送数量", "PresentAmount"},
{"已退数量", "DebookedAmount"},
{"已排数量", "AssignedAmount"},
{"已上数量", "ConfirmedAmount"},
{"剩余数量", "RemainCount"},
{"订单状态", "OrderStatus"},
{"订购操作人", "SubmitterName"},
{"操作人岗位", "SubmitterJobName"},
 };
            columns.ToList().ForEach(kv =>
            {
                tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn(kv.Key, typeof(string))) { PropertyName = kv.Value });
            });
            
            var dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(OrderItemView));
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

            return wb.ToResponseMessage("订购列表.xlsx");
        }


        #endregion

        #region api/purchase/getAssetConsumeViews
        [HttpPost]
        public AssetConsumeViewResultModel getAssetConsumeViews(AssetConsumeViewCriteriaModel criteria) {
            AssetConsumeViewResultModel result = new AssetConsumeViewResultModel
            {
                QueryResult = AssetConsumeDataSource.Instance.Load(criteria.PageParams, criteria, criteria.OrderBy),
                Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(AssetConsumeViewModel))
            };
            return result;
        }

        [HttpPost]
        public PagedQueryResult<AssetConsumeViewModel, AssetConsumeViewCollectionModel> getPageAssetConsumeViews(AssetConsumeViewCriteriaModel criteria) {
            PagedQueryResult<AssetConsumeViewModel, AssetConsumeViewCollectionModel> result = AssetConsumeDataSource.Instance.Load(criteria.PageParams, criteria, criteria.OrderBy);
            return result;
        }
        #endregion



        [HttpPost]
        public dynamic GetOrderByWorkflow(dynamic wfParams)
        {
            WfClientSearchParameters p = new WfClientSearchParameters();

            p.ResourceID = wfParams.resourceID;
            p.ActivityID = wfParams.activityID;
            p.ProcessID = wfParams.processID;
            
            var model= OrderModel.FillOrderAndItemByOrderId(p.ResourceID, null).FillAccount();
            return new { ClientProcess = WfClientProxy.GetClientProcess(p),Model= model };
        }

    }
}
