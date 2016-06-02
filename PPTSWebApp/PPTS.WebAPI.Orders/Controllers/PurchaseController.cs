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

namespace PPTS.WebAPI.Orders.Controllers
{
    [ApiPassportAuthentication]
    public class PurchaseController : ApiController
    {
        #region 订购列表

        /// <summary>
        /// 查询，第一次。第一页，下载字典
        /// </summary>
        /// <param name="criteria">查询条件</param>
        /// <returns>返回带字典的潜客数据列表</returns>
        [HttpPost]
        public OrderItemViewQueryResult GetAllOrderItems(OrderItemViewCriteriaModel criteria)
        {
            return new OrderItemViewQueryResult
            {
                QueryResult = GenericPurchaseSource<OrderItemView, OrderItemViewCollection>.Instance.Query(criteria.PageParams, criteria, criteria.OrderBy),
                Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(OrderItemView),typeof(Product)),
            };
        }

        /// <summary>
        /// 查询，翻页或排序。不下载字典
        /// </summary>
        /// <param name="criteria">查询条件</param>
        /// <returns>返回不带字典的潜客数据列表</returns>
        [HttpPost]
        public PagedQueryResult<OrderItemView, OrderItemViewCollection> GetPagedProducts(OrderItemViewCriteriaModel criteria)
        {
            return GenericPurchaseSource<OrderItemView, OrderItemViewCollection>.Instance.Query(criteria.PageParams, criteria, criteria.OrderBy);
        }

        [HttpPost]
        public dynamic GetOrderItemView(dynamic data)
        {
            string id = data.id;
            return new
            {
                Entity = OrderItemViewAdapter.Instance.Load(id),
                Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(OrderItemView)),
            };
        }

        #endregion

        #region 常规订购


        #endregion

        #region 插班订购

        /// <summary>
        /// 查询，翻页或排序。不下载字典
        /// </summary>
        /// <param name="criteria">查询条件</param>
        /// <returns>返回不带字典的潜客数据列表</returns>
        [HttpPost]
        public PagedQueryResult<ClassModel, ClassCollectionModel> GetPagedClasses(ClassCriteriaModel criteria)
        {
            var result = GenericPurchaseSource<ClassModel, ClassCollectionModel>.Instance.Query(criteria.PageParams, criteria, criteria.OrderBy);
            //result.PagedData.FillOrderAmount();
            return result;
        }

        #endregion

        #region 用户扣除服务费

        [HttpPost]
        public dynamic GetServiceChargeByUserId(dynamic data)
        {
            string customerId = data.customerId;
            string campusId = data.campusId;

            return new
            {
                DeductList = Service.CustomerService.GetWhetherToDeductServiceChargeByCustomerId(customerId),
                ServiceCharge = Service.ProductService.GetServiceChargeByCampusId(campusId)
            };
        }

        #endregion

        #region 查看订单

        [HttpPost]
        public OrderModel GetOrder(dynamic data)
        {
            string orderId = data.orderId;
            return OrderModel.FillOrderByOrderId(orderId).FillAccount().FillChargePayment();

        }

        #endregion

        /// <summary>
        /// 获取购物车
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
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
        [HttpPost]
        public void SubmitShoppingCart(SubmitOrderModel model)
        {
            model.NullCheck("model");
            model.CustomerID.CheckStringIsNullOrEmpty("model.CustomerID");
            model.AccountID.CheckStringIsNullOrEmpty("model.AccountId");

            model.CurrentUser = DeluxeIdentity.CurrentUser;
            new AddOrderExecutor(model) { NeedValidation = true }.Execute();

        }


        #region 资产兑换
        /// <summary>
        /// 资产兑换 获取订单 及 要兑换的产品信息
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public ExchangeAmountModel GetExchangeInfo(dynamic data)
        {
            string itemId = data.itemId;
            string productId = data.productId;
            return new ExchangeAmountModel(itemId, productId);

        }

        [HttpPost]
        public void ExchangeOrder(ExchangeOrderModel model)
        {
            new ExchangeExecutor(model).Execute();
        }

        #endregion

        #region 编辑缴费单

        [HttpPost]
        public OrderModel GetEditPayment(dynamic data)
        {
            string orderId = data.orderid;
            return OrderModel.FillOrderByOrderId(orderId).FillPayments();
        }

        [HttpPost]
        public void EditPayment(dynamic data)
        {
            string orderId = data.orderId;
            string chargeApplyID = data.chargeApplyId;

            var param = new Dictionary<string, object>() {
                { "ChargeApplyID", chargeApplyID },
                { "ModifierID", DeluxeIdentity.CurrentUser.ID },
                { "ModifierName", DeluxeIdentity.CurrentUser.DisplayName },
            };

            new PPTSOrderExecutor("EditPayment") { OrderId = orderId, EditPaymentParams = param }.Execute();
        }

        #endregion

        #region 导出

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
    }
}
