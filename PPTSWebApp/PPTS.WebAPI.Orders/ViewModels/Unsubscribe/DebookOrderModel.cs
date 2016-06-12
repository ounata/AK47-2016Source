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
using PPTS.Data.Common;
using PPTS.Data.Orders;
using PPTS.Data.Common.Entities;

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
            Order.ParentID = OrderItemView.ParentID;
            Order.ParentName = OrderItemView.ParentName;

            Order.DebookStatus = ((int)OrderStatus.PendingApproval).ToString();
            Order.ProcessStatus = ((int)ProcessStatusDefine.Processing).ToString();

            FillUser(Order);

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
            
            Item.AssetID = OrderItemView.AssetID;

            FillUser(Item);

            return this;
        }

        private void FillUser(object info)
        {
            if (CurrentUser != null)
            {
                if (info is IEntityWithCreator)
                {
                    CurrentUser.FillCreatorInfo(info as IEntityWithCreator);
                }
                if (info is IEntityWithModifier)
                {
                    CurrentUser.FillModifierInfo(info as IEntityWithModifier);
                }
                if (info is DebookOrder)
                {
                    var model = info as DebookOrder;
                    model.SubmitterID = model.CreatorID;
                    model.SubmitterName = model.CreatorName;
                    model.SubmitterJobID = CurrentUser.GetCurrentJob().ID;
                    model.SubmitterJobName = CurrentUser.GetCurrentJob().Name;
                }
            }
        }



        public Asset ToAsset()
        {
            if (null != Asset) { return Asset; }
            Asset = GenericAssetAdapter<Asset, AssetCollection>.Instance.LoadByItemId(OrderItemView.ItemID);
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

            (
                Item.DebookAmount == 0 ||
                Asset.RealAmount - Item.DebookAmount- Asset.AssignedAmount - Asset.ConfirmedAmount- Asset.DebookedAmount < 0
            ).TrueThrow("退订数据有误！");

            (
                Asset.ConfirmedAmount == Asset.RealAmount || 
                Asset.DebookedAmount + Asset.AssignedAmount == Asset.RealAmount
             ).TrueThrow("无剩余数量可退");
            
        }

    }

}