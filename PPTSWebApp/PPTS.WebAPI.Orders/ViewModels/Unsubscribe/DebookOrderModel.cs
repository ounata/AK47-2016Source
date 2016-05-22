using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PPTS.WebAPI.Orders.ViewModels.Unsubscribe
{
    public class DebookOrderModel
    {
        
        public Data.Orders.Entities.DebookOrder Order { set; get; }

        public Data.Orders.Entities.DebookOrderItem Item { set; get; }
        


        public DebookOrderModel FillOrder()
        {
            
            Order.CustomerID = OrderItemView.CustomerID;
            Order.CustomerCode = OrderItemView.CustomerCode;
            Order.CustomerName = OrderItemView.CustomerName;
            Order.CampusID = OrderItemView.CampusID;
            Order.CampusName = OrderItemView.CampusName;
            Order.DebookStatus = "1";
            Order.ProcessStatus = "1";
            return this;
        }

        public DebookOrderModel FillOrderItem() {

            var currentAmount = OrderItemView.RealAmount - OrderItemView.DebookedAmount;
            Item.DebookAmount = Item.DebookAmount < currentAmount ? Item.DebookAmount : currentAmount;

            Item.AccountCode = OrderItemView.AccountCode;
            Item.AccountID = OrderItemView.AccountID;
            Item.AssetID = OrderItemView.AssetID;
            Item.SortNo = 1;

            //买赠退订 全部退
            if(OrderItemView.PresentAmount>0)
            {
                //买赠退订 全部退
                Item.DebookAmount = currentAmount- OrderItemView.UsedOrderAmount - OrderItemView.UsedPresentAmount;

                //订购数量=已上购买数量+剩余购买数量
                var orderAmount = OrderItemView.UsedOrderAmount + (OrderItemView.OrderAmount - OrderItemView.UsedOrderAmount);

                //赠送数量=已上赠送数量+剩余赠送数量
                var presetAmount = OrderItemView.UsedPresentAmount + (OrderItemView.PresentAmount - OrderItemView.UsedPresentAmount);

                //退订前的平均单价=（订购数量*实际单价）/（订购数量+赠送数量）
                var unsubscribeBeforeAveragePrice = orderAmount * OrderItemView.RealPrice / orderAmount + presetAmount;

                //退订后的赠送数量
                var unsubscribeAfterCount = 0;

                if( OrderItemView.UsedOrderAmount == OrderItemView.OrderAmount )
                {
                    unsubscribeAfterCount = (int)( OrderItemView.PresentAmount - Item.DebookAmount );
                }else if(OrderItemView.OrderAmount> OrderItemView.UsedOrderAmount)
                {
                    unsubscribeAfterCount = (int)(OrderItemView.PresentAmount - Item.DebookAmount - OrderItemView.OrderAmount - OrderItemView.UsedOrderAmount);
                }
                
                
                //退订后的平均单价=（退订后的订购数量*实际单价）/（退订后的订购数量+退订后的赠送数量）
                var unsubscribeAfterAveragePrice = (OrderItemView.OrderAmount - Item.DebookAmount) * OrderItemView.RealPrice / ((OrderItemView.OrderAmount - Item.DebookAmount) +  unsubscribeAfterCount);

                //总消耗的数量*（退订后的平均单价-退订前平均单价）
                Item.ReturnMoney = (OrderItemView.UsedOrderAmount + OrderItemView.UsedPresentAmount) * (unsubscribeAfterAveragePrice - unsubscribeBeforeAveragePrice);
            }

            Item.DebookMoney = Item.DebookAmount * OrderItemView.RealPrice;


            Item.AssetID = "test";

            return this;
        }

        private Data.Orders.Entities.OrderItemView _orderItemView = null;
        private Data.Orders.Entities.OrderItemView OrderItemView
        {
            get{
                if(_orderItemView == null)
                {
                    _orderItemView = Data.Orders.Adapters.OrderItemViewAdapter.Instance.Load(OrderItemID);
                }
                return _orderItemView;
            }
        }

        /// <summary>
        /// 订单明细ID
        /// </summary>
        public string OrderItemID { set; get; }
    }

}