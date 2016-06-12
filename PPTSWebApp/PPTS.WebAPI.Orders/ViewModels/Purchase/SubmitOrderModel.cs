using MCS.Library.Validation;
using MCS.Library.Core;
using PPTS.Data.Common.Security;
using PPTS.Data.Orders;
using PPTS.Data.Orders.Entities;
using PPTS.Data.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using MCS.Library.SOA.DataObjects.AsyncTransactional;
using MCS.Library.Configuration;
using PPTS.Data.Orders.Adapters;
using PPTS.Data.Common;
using PPTS.Contracts.Customers.Operations;
using PPTS.Contracts.Orders.Operations;
using PPTS.Data.Common.Entities;
using PPTS.Data.Products.Entities;
using PPTS.Data.Customers.Entities;
using PPTS.WebAPI.Orders.Service;

namespace PPTS.WebAPI.Orders.ViewModels.Purchase
{

    public class SubmitOrderModel
    {
        /// <summary>
        /// 提交清单类型 1:常规 2:买赠 3:插班
        /// </summary>
        public int ListType { set; get; }

        /// <summary>
        /// 关联缴费申请单ID
        /// </summary>
        public string ChargeApplyID { set; get; }

        public string CustomerID { set; get; }
        public string AccountID { set; get; }

        /// <summary>
        /// 特殊折扣类型代码
        /// </summary>
        public string SpecialType { set; get; }

        /// <summary>
        /// 特殊折扣说明
        /// </summary>
        public string SpecialMemo { set; get; }
        public string CustomerCampusID { set; get; }

        public List<OrderItemViewModel> item { set; get; }


        #region private fields

        private Data.Customers.Entities.Account Account { set; get; }
        private List<Data.Products.Entities.ProductView> ProductViews { set; get; }

        //扣除服务费集合
        private Data.Customers.Entities.CustomerExpenseRelationCollection ExpenseRelations { set; get; }

        private decimal FrozenMoney { set; get; }

        #endregion


        [ObjectValidator]
        public Order Order { private set; get; }

        [ObjectValidator]
        public OrderItemCollection OrderItems { private set; get; }

        [ObjectValidator]
        public AssetCollection Assets { set; get; }


        public SubmitOrderModel FillOrder()
        {
            if (Order == null)
            {

                FillAccount();

                var parentInfo = Service.CustomerService.GetPrimaryParentByCustomerId(CustomerID);
                var customerInfo = Service.CustomerService.GetCustomerByCustomerId(CustomerID);

                Order = new Order()
                {
                    OrderID = UuidHelper.NewUuidString(),

                    CustomerID = CustomerID,
                    ChargeApplyID = ChargeApplyID,
                    SpecialType = SpecialType,
                    SpecialMemo = SpecialMemo,
                    AccountID = AccountID,
                    AccountCode = Account.AccountCode,
                    CustomerCode = customerInfo.CustomerCode,
                    CustomerGrade = customerInfo.Grade,
                    CustomerName = customerInfo.CustomerName,
                    ParentID = parentInfo != null ? parentInfo.ParentID : "",
                    ParentName = parentInfo != null ? parentInfo.ParentName : "",
                    OrderType = (OrderType)ListType,
                    //OrderStatus = OrderStatus.PendingApproval,
                    ProcessStatus = (int)ProcessStatusDefine.Processing,
                    CampusName = customerInfo.CampusName,
                    CampusID = customerInfo.CampusID
                };


                FillUser(Order);
            }
            return this;
        }

        public SubmitOrderModel FillOrderItemCollection()
        {
            if (OrderItems == null)
            {
                var cartids = item.Select(m => m.CartID).ToArray();
                var carts = ShoppingCartAdapter.Instance.Load(cartids);
                (cartids.Length != carts.Count).TrueThrow("提交数据有误，请重新提交！");

                FillProductViews();
                
                var mapper = new AutoMapper.MapperConfiguration(c =>
                {
                    c.CreateMap<Data.Products.Entities.ProductView, OrderItem>();
                }).CreateMapper();

                Service.Present preset = null;

                //买赠
                if (ListType == 2)
                {
                    preset = Service.ProductService.GetPresentByOrgId(CustomerCampusID);
                }

                OrderItems = new OrderItemCollection();

                foreach (var product in ProductViews)
                {
                    var submitItem = item.Single(m => m.ProductID == product.ProductID);
                    var oitem = mapper.Map<OrderItem>(product);

                    oitem.OrderPrice = product.ProductPrice;
                    oitem.OrderAmount = 1;
                    oitem.CategoryType = ((int)product.CategoryType).ToString();
                    oitem.DiscountType = ((int)DiscountTypeDefine.None).ToString();
                    oitem.DiscountRate = 1;

                    oitem.ProductCampusID = carts.Single(s => s.ProductID == product.ProductID).ProductCampusID;

                    product.Process(oitem, submitItem, preset, Account);
                    
                    oitem.ItemID = UuidHelper.NewUuidString();
                    OrderItems.Add(oitem);
                }
            }
            return this;
        }

        public SubmitOrderModel FillAssets()
        {
            if (Assets == null)
            {
                Assets = new AssetCollection();
                var mapper = new AutoMapper.MapperConfiguration(c => { c.CreateMap<OrderItem, Asset>(); }).CreateMapper();
                foreach (var item in OrderItems)
                {
                    var asset = mapper.Map<Asset>(item);
                    asset.AssetID = UuidHelper.NewUuidString();
                    asset.AccountID = AccountID;
                    asset.AssetRefID = item.ItemID;
                    asset.Amount = item.RealAmount;
                    asset.AssetType = "0";
                    asset.AssetRefType = "0";
                    asset.Price = item.RealPrice;
                    asset.CustomerID = CustomerID;

                    Assets.Add(asset);
                }
            }
            return this;
        }

        public MCS.Library.OGUPermission.IUser CurrentUser { set; get; }

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
                if (info is Order)
                {
                    var model = info as Order;
                    model.SubmitterID = model.CreatorID;
                    model.SubmitterName = model.CreatorName;
                    model.SubmitterJobID = CurrentUser.GetCurrentJob().ID;
                    model.SubmitterJobName = CurrentUser.GetCurrentJob().Name;
                }
            }

        }

        #region Private




        private void FillAccount()
        {
            if (Account == null)
            {
                Account = Service.CustomerService.GetAccountbyCustomerId(CustomerID).SingleOrDefault(m => m.AccountID == AccountID);
                (Account == null).TrueThrow("未找到该学员帐户");
            }
        }

        private void FillProductViews()
        {
            if (ProductViews == null)
            {
                ProductViews = Service.ProductService.GetProductsByIds(item.Select(m => m.ProductID).ToArray());
            }
        }

        private void FillFrozenMoney()
        {
            FrozenMoney = OrderItemAdapter.Instance.GetFrozenMoneyByCustomerId(CustomerID, AccountID);
        }

        #endregion

        public void Validate()
        {
            FillFrozenMoney();

            //常规订购
            (ListType == 1 &&
             ProductViews.Any(m => m.CategoryType == CategoryType.YouXue) &&
             ProductViews.Any(m => m.CategoryType != CategoryType.YouXue)
             ).TrueThrow("游学类产品不能与其他类产品同时订购");

            //扣除服务费集合
            ExpenseRelations = new CustomerExpenseRelationCollection();

            var totalMoney = OrderItems.Sum(m => m.RealPrice * m.RealAmount);

            //是否扣除过综合服务费
            var kfdict = Service.CustomerService.GetWhetherToDeductServiceChargeByCustomerId(CustomerID);
            var categoryTypes = kfdict.Where(item => !item.Value).Select(item => item.Key);
            if (categoryTypes.Count() > 0)
            {
                //获取该校区综合服务费
                var expenses = Service.ProductService.GetServiceChargeByCampusId(CustomerCampusID);
                var extype = new string[] {"1","2" };
                var containsExpenses = expenses.Where(item => extype.Contains(item.ExpenseType) && categoryTypes.Contains(Convert.ToInt32(item.ExpenseType)));
                (expenses == null || containsExpenses.Count() != categoryTypes.Count()).TrueThrow("该校区存在未创建综合服务费");
                containsExpenses.ForEach(item =>
                {
                    totalMoney += item.ExpenseValue;
                    ExpenseRelations.Add(new CustomerExpenseRelation() {
                        ExpenseID = item.ExpenseID,
                        ExpenseType = item.ExpenseType,
                        ExpenseMoney = item.ExpenseValue,
                        AccountID = AccountID,
                        CustomerID = CustomerID });
                });
            }

            (totalMoney + FrozenMoney > Account.AccountMoney).TrueThrow("该学员帐户余额不足");

            if (ExpenseRelations.Count > 0)
            {
                Service.CustomerService.DeductExpenses(ExpenseRelations);
            }

            //买赠订购
            //有未完成的退费操作不允许订购
            //提交订单后，扣减对应账户的可用金额，订购资金余额增加对应金额。

            //常规订购
            //有未完成的退费操作不允许订购
            //游学类产品不能与其他类产品同时订购

        }


        #region 异步使用

        public TxProcess TxProcess { private set; get; }

        public TxProcess PrepareProcess()
        {
            if (TxProcess != null)
            {
                return TxProcess;
            }

            TxProcess = new TxProcess();

            TxProcess.ProcessName = "提交订单";
            TxProcess.Category = "提交订单";

            //process.ConnectionName = MCS.Library.SOA.DataObjects.ConnectionDefine.DBConnectionName;


            var debitMoney = OrderItems.Sum(m => m.RealPrice * m.RealAmount);

            TxActivity activity1 = TxProcess.Activities.AddActivity("帐户扣钱");
            activity1.AddActionService<IAccountTransactionService>(
                UriSettings.GetConfig().CheckAndGet("pptsServices", "accountTransactionService").ToString(),
                proxy => proxy.DebitAccount(TxProcess.ProcessID, AccountID, debitMoney));

            activity1.AddCompensationService<IAccountTransactionService>(
                UriSettings.GetConfig().CheckAndGet("pptsServices", "accountTransactionService").ToString(),
                proxy => proxy.RollbackDebitAccount(TxProcess.ProcessID, AccountID, debitMoney));


            TxActivity activity2 = TxProcess.Activities.AddActivity("更改订单异步处理状态");
            activity2.AddActionService<IOrderTransactionService>(
                UriSettings.GetConfig().CheckAndGet("pptsServices", "orderTransactionService").ToString(),
                proxy => proxy.ResetOrderStatus(TxProcess.ProcessID, Order.OrderID, (int)ProcessStatusDefine.Processed));

            activity2.AddCompensationService<IOrderTransactionService>(
                UriSettings.GetConfig().CheckAndGet("pptsServices", "orderTransactionService").ToString(),
                proxy => proxy.ResetOrderStatus(TxProcess.ProcessID, Order.OrderID, (int)ProcessStatusDefine.Error));

            TxActivity activity5 = TxProcess.Activities.AddActivity("同步资产");
            activity5.AddActionService<IOrderTransactionService>(
                UriSettings.GetConfig().CheckAndGet("pptsServices", "orderTransactionService").ToString(),
                proxy => proxy.SyncAsset(TxProcess.ProcessID, Assets));


            if (ExpenseRelations.Count > 0)
            {
                ExpenseRelations.ForEach(m => m.OrderID = Order.OrderID);

                TxActivity activity6 = TxProcess.Activities.AddActivity("同步扣除综合服务费订单ID");
                activity6.AddActionService<IAccountTransactionService>(
                    UriSettings.GetConfig().CheckAndGet("pptsServices", "accountTransactionService").ToString(),
                    proxy => proxy.SyncExpense(TxProcess.ProcessID, ExpenseRelations));
            }
            

            return TxProcess;
        }

        #endregion

    }








    static class ProductViewExtend
    {

        public static void Process(this ProductView productView, OrderItem item, OrderItemViewModel ovitem, Service.Present preset, Account Account)
        {
            ICalculateStrategy strategy = null;
            switch (productView.CategoryType)
            {

                case CategoryType.OneToOne:
                    strategy = new OneToOneStrategy();
                    break;
                case CategoryType.CalssGroup:
                    strategy = new ClassGroupStrategy();
                    break;
                case CategoryType.YouXue:
                    strategy = new YouXueStrategy();
                    break;
                case CategoryType.Other:
                    strategy = new OtherStrategy();
                    break;
            }

            strategy.Process(item, ovitem, productView, preset, Account);
        }

    }


    interface ICalculateStrategy
    {
        void Process(OrderItem item, OrderItemViewModel ovitem, ProductView product, Service.Present preset, Account Account);
    }

    abstract class CalculateItem : ICalculateStrategy
    {
        protected void Prepare(OrderItem oitem, OrderItemViewModel submitItem, ProductView product, Service.Present preset, Account Account)
        {
            //是否允许修改订购产品数量
            if (product.CanInput == 1)
            {
                oitem.OrderAmount = submitItem.OrderAmount;
            }

            //是否允许使用 客户折扣
            if (product.TunlandAllowed == 1)
            {
                oitem.DiscountRate = oitem.TunlandRate = Account.DiscountRate;
                oitem.DiscountType = ((int)DiscountTypeDefine.Tunland).ToString();
            }

            //是否允许特殊折扣
            if (product.SpecialAllowed == 1)
            {
                if (oitem.DiscountRate > submitItem.SpecialRate)
                {
                    oitem.DiscountRate = oitem.SpecialRate = submitItem.SpecialRate;
                    oitem.DiscountType = ((int)DiscountTypeDefine.Special).ToString();
                }
            }

            oitem.PresentAmount = submitItem.PresentAmount;
            oitem.RealPrice = oitem.OrderPrice * oitem.DiscountRate;
            oitem.RealAmount = oitem.OrderAmount + oitem.PresentAmount;

        }

        public virtual void Process(OrderItem oitem, OrderItemViewModel submitItem, ProductView product, Service.Present preset, Account Account)
        {
            Prepare(oitem, submitItem, product, preset, Account);
        }
    }

    class OneToOneStrategy : CalculateItem
    {
        public override void Process(OrderItem oitem, OrderItemViewModel submitItem, ProductView product, Service.Present preset, Account Account)
        {
            //买赠
            if (preset != null)
            {
                var presetitem = preset.Items.FirstOrDefault(s => oitem.OrderAmount >= s.PresentStandard);
                oitem.PresentID = preset.PresentID;
                oitem.PresentQuato = presetitem == null ? 0 : presetitem.PresentStandard;
            }

            Prepare(oitem, submitItem, product, preset, Account);
        }
    }

    class ClassGroupStrategy : CalculateItem
    {
        public override void Process(OrderItem oitem, OrderItemViewModel submitItem, ProductView product, Service.Present preset, Account Account)
        {
            oitem.OrderAmount = product.LessonCount;
            Prepare(oitem, submitItem, product, preset, Account);
        }
    }

    class YouXueStrategy : CalculateItem
    {
        public override void Process(OrderItem oitem, OrderItemViewModel submitItem, ProductView product, Service.Present preset, Account Account)
        {
            //是否允许修改订购产品数量
            if (product.CanInput == 1)
            {
                oitem.OrderAmount = submitItem.OrderAmount;
            }

            //是否允许特殊折扣
            if (product.SpecialAllowed == 1)
            {
                if (oitem.DiscountRate > submitItem.SpecialRate)
                {
                    oitem.DiscountRate = oitem.SpecialRate = submitItem.SpecialRate;
                    oitem.DiscountType = ((int)DiscountTypeDefine.Special).ToString();
                }
            }

            //是否允许使用 客户折扣
            if (product.TunlandAllowed == 1)
            {
                //只享受客户折扣
                if (product.PromotionAllowed == 1)
                {
                    oitem.DiscountRate = oitem.TunlandRate = Account.DiscountRate;
                    oitem.DiscountType = ((int)DiscountTypeDefine.Tunland).ToString();
                }
                else
                {
                    //只享受订购时特殊优惠
                    if (oitem.OrderPrice * oitem.OrderAmount * (1 - oitem.DiscountRate) >= product.PromotionQuota)
                    {
                        //TODO 需要进行审批
                    }

                }
            }

            oitem.RealPrice = oitem.OrderPrice * oitem.DiscountRate;
            oitem.RealAmount = oitem.OrderAmount;

        }
    }

    class OtherStrategy: CalculateItem
    {

    }

}