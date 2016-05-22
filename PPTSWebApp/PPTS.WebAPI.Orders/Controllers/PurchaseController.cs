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
                Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(OrderItemView)),
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
                IsDeduct = Service.CustomerService.GetWhetherToDeductServiceChargeByCustomerId(customerId),
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

            var sccollection = (ShoppingCartCollection)new PPTSShoppingCartExecutor("GetShoppingCart") { CustomerId = customerId, OrderType = listType }.Execute();
            var productIds = sccollection.Select(m => m.ProductID).ToArray();

            var scm = new ShoppingCartModel() { ListType = listType };

            scm.FillAccount(Service.CustomerService.GetAccountbyCustomerId(customerId))
                .FillCart(sccollection)
                .FillCartProduct(Service.ProductService.GetProductsByIds(productIds))
                .FillPreset(Service.ProductService.GetPresentByOrgId(orgId))
                .SetDiscount()
                .FillClassGroupAmount()
                .ChargePayments = Service.CustomerService.GetChargePaymentsByCustomerId(customerId);

            scm.Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(ProductView), typeof(Order));

            return scm;


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
            collection.ForEach(m =>
            {
                m.Amount = 1;
                m.CreatorID = DeluxeIdentity.CurrentUser.ID;
                m.CreatorName = DeluxeIdentity.CurrentUser.DisplayName;
            });
            new AddShoppingCartExecutor(collection).Execute();

        }

        /// <summary>
        /// 提交订单
        /// </summary>
        /// <param name="model"></param>
        [HttpPost]
        public string SubmitShoppingCart(SubmitOrderModel model)
        {
            model.NullCheck("model");
            model.CustomerID.CheckStringIsNullOrEmpty("model.CustomerID");
            model.AccountId.CheckStringIsNullOrEmpty("model.AccountId");


            model.ProductViews = Service.ProductService.GetProductsByIds(model.item.Select(m => m.ProductID).ToArray());
            model.Account = Service.CustomerService.GetAccountbyCustomerId(model.CustomerID).SingleOrDefault(m => m.AccountID == model.AccountId);

            model.CreatorID = DeluxeIdentity.CurrentUser.ID;
            model.CreatorName = DeluxeIdentity.CurrentUser.DisplayName;
            model.JobId = DeluxeIdentity.CurrentUser.GetCurrentJob().ID;
            model.JobName = DeluxeIdentity.CurrentUser.GetCurrentJob().Name;
            model.JobType = ((int)DeluxeIdentity.CurrentUser.GetCurrentJob().JobType).ToString();

            //常规订购
            if (model.ListType == 1)
            {
                if (
                    model.ProductViews.Any(m => m.CategoryType == Data.Products.CategoryType.YouXue)
                    && model.ProductViews.Any(m => m.CategoryType != Data.Products.CategoryType.YouXue)
                    )
                {
                    return "游学类产品不能与其他类产品同时订购";
                }
            }


            var totalMoney = model.ToOrderItemCollection().Sum(m => m.RealPrice);
            ////是否扣除过综合服务费
            //var kfdict = Service.CustomerService.GetWhetherToDeductServiceChargeByCustomerId(model.CustomerID);
            //if (model.ProductViews.Select(m => (int)m.CategoryType).Any(w => kfdict[w] == false))
            //{
            //    //获取该校区综合服务费
            //    var expense = Service.ProductService.GetServiceChargeByCampusId(model.CustomerCampusID);
            //    if (expense == null) { return "该校区未创建综合服务费"; }
            //    totalMoney += expense.ExpenseValue;
            //}
            if (totalMoney > model.Account.AccountMoney)
            {
                return "该学员帐户余额不足";
            }


            //买赠订购
            //有未完成的退费操作不允许订购
            //提交订单后，扣减对应账户的可用金额，订购资金余额增加对应金额。

            //常规订购
            //有未完成的退费操作不允许订购
            //游学类产品不能与其他类产品同时订购

            var status = (int)new AddOrderExecutor(model).Execute();
            switch (status)
            {
                case -1: return "有未完成的退费操作不允许订购";
            }
            return "";
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

    }
}
