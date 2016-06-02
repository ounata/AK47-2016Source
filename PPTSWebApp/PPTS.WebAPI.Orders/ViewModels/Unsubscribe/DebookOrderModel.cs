using MCS.Library.Validation;
using PPTS.Data.Orders.Adapters;
using PPTS.Data.Orders.Entities;
using MCS.Library.Core;
using PPTS.Data.Common.Security;
using MCS.Library.Principal;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PPTS.WebAPI.Orders.ViewModels.Unsubscribe
{
    public class DebookOrderModel
    {
        /// <summary>
        /// 订单明细ID
        /// </summary>
        public string OrderItemID { set; get; }

        [ObjectValidator]
        public DebookOrder Order { set; get; }

        [ObjectValidator]
        public DebookOrderItem Item { set; get; }

        [ObjectValidator]
        public Asset Asset { set; get; }

        public MCS.Library.OGUPermission.IUser CurrentUser { set; get; }

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

        public DebookOrderModel FillOrderItem()
        {

            var currentAmount = OrderItemView.RealAmount - OrderItemView.DebookedAmount - OrderItemView.ConfirmedAmount;
            Item.DebookAmount = Item.DebookAmount < currentAmount ? Item.DebookAmount : currentAmount;

            Item.AccountCode = OrderItemView.AccountCode;
            Item.AccountID = OrderItemView.AccountID;
            Item.AssetID = OrderItemView.AssetID;
            Item.SortNo = 1;

            //买赠退订
            if (OrderItemView.PresentAmount > 0)
            {
                //课时单价
                var lessonPricce = OrderItemView.OrderPrice * OrderItemView.OrderAmount / OrderItemView.RealAmount;

                Item.ReturnMoney = OrderItemView.ConfirmedAmount * (OrderItemView.OrderPrice - lessonPricce) - OrderItemView.ReturnedMoney;
            }

            Item.DebookMoney = Item.DebookAmount * OrderItemView.RealPrice;

            Item.AssetID = "test";

            return this;
        }

        public DebookOrderModel FillUser()
        {
            if (CurrentUser != CurrentUser)
            {
                CurrentUser.FillCreatorInfo(Order);
                CurrentUser.FillModifierInfo(Order);
            }
            return this;
        }

        public Asset ToAsset()
        {
            if (null != Asset) { return Asset; }
            Asset = GenericAssetAdapter<Asset, AssetCollection>.Instance.LoadByItemId(Item.ItemID);
            Asset.NullCheck("Asset");

            Asset.ReturnedMoney = Item.ReturnMoney;
            Asset.DebookedAmount += Item.DebookAmount;
            return Asset;
        }

        private OrderItemView _orderItemView = null;
        private OrderItemView OrderItemView
        {
            get
            {
                if (_orderItemView == null)
                {
                    _orderItemView = Data.Orders.Adapters.OrderItemViewAdapter.Instance.Load(OrderItemID);
                }
                return _orderItemView;
            }
        }



        public void Validate()
        {
            Order.NullCheck("Order");
            Item.NullCheck("Item");
            (Asset.ConfirmedAmount == Asset.RealAmount).TrueThrow("无剩余数量可退");
            
        }

    }

}