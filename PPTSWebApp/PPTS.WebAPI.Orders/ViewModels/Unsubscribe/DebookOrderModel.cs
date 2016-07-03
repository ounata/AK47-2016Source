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
using MCS.Library.SOA.DataObjects.AsyncTransactional;
using PPTS.Data.Customers.Entities;
using PPTS.Data.Customers;
using PPTS.Contracts.Customers.Operations;
using MCS.Library.Configuration;
using PPTS.Contracts.Orders.Operations;

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

            Order.DebookID =  UuidHelper.NewUuidString();
            Order.DebookNo = Data.Orders.Helper.GetDebookOrderCode("DEB");
            Order.CustomerID = OrderItemView.CustomerID;
            Order.CustomerCode = OrderItemView.CustomerCode;
            Order.CustomerName = OrderItemView.CustomerName;
            Order.CampusID = OrderItemView.CampusID;
            Order.CampusName = OrderItemView.CampusName;
            Order.ParentID = OrderItemView.ParentID;
            Order.ParentName = OrderItemView.ParentName;

            Order.DebookStatus = ((int)OrderStatus.ApprovalPass).ToString();
            Order.ProcessStatus = ((int)ProcessStatusDefine.Processing).ToString();

            FillUser(Order);

            return this;
        }

        public DebookOrderModel FillOrderItem()
        {
            var currentAmount = OrderItemView.RealAmount - OrderItemView.DebookedAmount - OrderItemView.ConfirmedAmount;
            Item.DebookAmount = Item.DebookAmount < currentAmount ? Item.DebookAmount : currentAmount;

            Item.DebookID = Order.DebookID;
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



        public DebookOrderModel FillAsset()
        {
            //if (null != Asset) { return Asset; }
            Asset = GenericAssetAdapter<Asset, AssetCollection>.Instance.LoadByItemId(OrderItemView.ItemID);
            Asset.NullCheck("Asset");

            Asset.ReturnedMoney = Item.ReturnMoney;
            Asset.DebookedAmount += Item.DebookAmount;
            Asset.Amount -= Item.DebookAmount;

            return this;
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
                Asset.Amount < 0
            ).TrueThrow("退订数据有误！");

            //是否存在 未完成订购操作
            OrdersAdapter.Instance.ExistsPendingApprovalInContext(Order.CustomerID);
            Data.Orders.ConnectionDefine.GetDbContext().DoAction(db => db.ExecuteNonQuerySqlInContext());
        }

        #region 异步使用
        private Data.Customers.Entities.AccountRecord Record { set; get; }

        private void FillAccountRecord()
        {
            Record.IsNull(() =>
            {

                Order = DebookOrderAdapter.Instance.Load(Order.DebookID);
                var tempOrderItem = OrderItemAdapter.Instance.Load(OrderItemID);
                var tempOrder = OrdersAdapter.Instance.Load(tempOrderItem.OrderID);

                Record = new AccountRecord()
                {
                    CampusID = Order.CampusID,
                    CustomerID = Order.CustomerID,
                    AccountID = tempOrder.AccountID,
                    RecordID = UuidHelper.NewUuidString(),
                    RecordType = AccountRecordType.Debook,
                    RecordFlag = 1,

                    BillID = Order.DebookID,
                    BillNo = Order.DebookNo,

                    BillRelateID = tempOrder.OrderID,
                    BillRelateNo = tempOrder.OrderNo,

                    BillType = ((int)tempOrder.OrderType).ToString(),
                    BillTypeName = EnumItemDescriptionAttribute.GetDescription(tempOrder.OrderType),
                    BillTime = tempOrder.OrderTime
                };
                FillUser(Record);
            });

        }
        public TxProcess TxProcess { private set; get; }
        public TxProcess PrepareProcess()
        {
            if (TxProcess != null)
            {
                return TxProcess;
            }

            FillAccountRecord();

            TxProcess = new TxProcess();

            TxProcess.ProcessName = "提交退订";
            TxProcess.Category = "提交退订";
            

            TxActivity activity1 = TxProcess.Activities.AddActivity("帐户退钱");
            activity1.AddActionService<IAccountTransactionService>(
                UriSettings.GetConfig().CheckAndGet("pptsServices", "accountTransactionService").ToString(),
                proxy => proxy.DebookAccount(TxProcess.ProcessID, Record.AccountID, Item.DebookMoney, Record));

            activity1.AddCompensationService<IAccountTransactionService>(
                UriSettings.GetConfig().CheckAndGet("pptsServices", "accountTransactionService").ToString(),
                proxy => proxy.RollbackDebookAccount(TxProcess.ProcessID, Record.AccountID, Item.DebookMoney, Record));


            TxActivity activity2 = TxProcess.Activities.AddActivity("重置退订订单状态");
            activity2.AddActionService<IOrderTransactionService>(
                UriSettings.GetConfig().CheckAndGet("pptsServices", "orderTransactionService").ToString(),
                proxy => proxy.ModifyDebookOrderStatus(TxProcess.ProcessID,Order.DebookID, OrderStatus.ApprovalPass, ProcessStatusDefine.Processed,Order.CreatorID,Order.CreatorName));
            activity2.AddCompensationService<IOrderTransactionService>(
                UriSettings.GetConfig().CheckAndGet("pptsServices", "orderTransactionService").ToString(),
                proxy => proxy.ModifyDebookOrderStatus(TxProcess.ProcessID, Order.DebookID, OrderStatus.ApprovalPass, ProcessStatusDefine.Error, Order.CreatorID, Order.CreatorName));

            return TxProcess;

        }

        #endregion


        /// <summary>
        /// 是否需要审批
        /// </summary>
        public bool IsApprove { private set; get; }

        public DebookOrderModel PrepareIsApprove() {
            


            if(OrderItemView != null)
            {
                IsApprove.FalseAction(() => {
                    IsApprove = (OrderItemView.CategoryType == ((int)CategoryType.YouXue).ToString() );
                }); 
            }

            //IsApprove = true;

            IsApprove.TrueAction(() => {
                Order.DebookStatus = ((int)OrderStatus.PendingApproval).ToString();
            });

            return this;
        }

    }

}