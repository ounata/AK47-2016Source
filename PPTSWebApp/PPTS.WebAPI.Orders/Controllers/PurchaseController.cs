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

namespace PPTS.WebAPI.Orders.Controllers
{
    public class PurchaseController : ApiController
    {

        #region 常规订购

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
            result.PagedData.FillOrderAmount();
            return result;
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
            string orgId = "8";
            string customerId = data.customerId;
            //查看清单类型 1：普通 2：买赠
            int listType = data.listType;

            var sccollection = (ShoppingCartCollection)new PPTSShoppingCartExecutor("GetShoppingCart") { CustomerId = customerId }.Execute();
            var productIds = sccollection.Select(m => m.ProductID).ToArray();
            var scm = new ShoppingCartModel();

            scm.FillAccount(Service.AccountService.GetAccountbyCustomerId(customerId));
            scm.FillCart(sccollection);
            scm.FillCartProduct(Service.ProductService.GetProductsByIds(productIds));
            scm.ChargePayments = Service.AccountService.GetChargePaymentsByCustomerId(customerId);
            scm.SetDiscount();

            scm.Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(ProductView),typeof(Order));

            if (listType == 2)
            {
                scm.Present = Service.ProductService.GetPresentByOrgId(orgId);
            }

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
        public dynamic AddShoppingCart(ShoppingCartCollection collection)
        {
            collection.ForEach(m => m.Amount = 1);
            var products = (ShoppingCartCollection)new AddShoppingCartExecutor(collection).Execute();
            return new { productIds = products.Select(m => m.ProductID) };
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
            

            var totalMoney = model.ToOrderItemCollection().Sum(m => m.RealPrice);
            model.Account = Service.AccountService.GetAccountbyCustomerId(model.CustomerID).SingleOrDefault(m => m.AccountID == model.AccountId);
            totalMoney += Service.AccountService.GetServiceMoneyByCustomerId(model.CustomerID);

            //if(totalMoney>= model.Account.AccountMoney){}

            model.OrgID = "8";

            //默认数据列表区域显示学员当前年级的产品

            //买赠订购
            //有未完成的退费操作不允许订购
            //提交订单后，扣减对应账户的可用金额，订购资金余额增加对应金额。

            //常规订购
            //有未完成的退费操作不允许订购
            //游学类产品不能与其他类产品同时订购


            model.ProductViews = null;

            new AddOrderExecutor(model).Execute();
            return "";
        }


    }
}
