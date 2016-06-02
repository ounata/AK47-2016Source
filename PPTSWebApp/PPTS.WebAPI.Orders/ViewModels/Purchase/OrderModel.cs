using PPTS.Data.Common.Adapters;
using PPTS.Data.Common.Entities;
using PPTS.Data.Orders.Adapters;
using PPTS.Data.Orders.Entities;
using PPTS.Data.Products.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPTS.WebAPI.Orders.ViewModels.Purchase
{
    public class OrderModel
    {
        public OrderModel() {
            Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(OrderItemView));
        }

        public Order Order { set; get; }
        public OrderItemCollection Items { set; get; }
        public Data.Customers.Entities.Account Account { set; get; }

        public Data.Customers.Entities.AccountChargeApply ChargePayment { set;get;}

        public List<Data.Customers.Entities.AccountChargeApply> Payments { set; get; }

        public Dictionary<string, IEnumerable<BaseConstantEntity>> Dictionaries { set; get; }

        public static OrderModel FillOrderByOrderId(string orderId)
        {
            var model = new OrderModel();

            OrdersAdapter.Instance.LoadInContext(new MCS.Library.Data.Adapters.WhereLoadingCondition(builder => builder.AppendItem("OrderID", orderId)), collection => { model.Order = collection.SingleOrDefault(); });
            OrderItemAdapter.Instance.LoadInContext(new MCS.Library.Data.Adapters.WhereLoadingCondition(builder => builder.AppendItem("OrderID", orderId)), collection => { model.Items = collection; });

            using (var dbContext = PPTS.Data.Orders.ConnectionDefine.GetDbContext()) { dbContext.ExecuteDataSetSqlInContext(); }

            return model;
        }

        public OrderModel FillAccount()
        {
            Account = Service.CustomerService.GetAccountbyCustomerId(Order.CustomerID)
                .Where(m=>m.AccountID == Order.AccountID)
                .DefaultIfEmpty(new Data.Customers.Entities.Account()).First();
            return this;
        }

        public OrderModel FillChargePayment() {
            ChargePayment = Service.CustomerService.GetChargePayById(Order.CustomerID, Order.ChargeApplyID);
            return this;
        }

        public OrderModel FillPayments() {
            Payments = Service.CustomerService.GetChargePaysByCustomerId(Order.CustomerID);
            return this;
        }

    }
}