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
using PPTS.Contracts.Customers.Models;
using PPTS.Data.Customers;
using PPTS.Data.Common.Adapters;

namespace PPTS.WebAPI.Orders.ViewModels.Purchase
{

    public class SubmitOrderModel
    {
        /// <summary>
        /// 提交清单类型 1:常规 2:买赠 3:插班
        /// </summary>
        public OrderType ListType { set; get; }

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
        //public string CustomerCampusID { set; get; }

        public List<OrderItemViewModel> item { set; get; }

        private List<OrderItemViewModel> Oparams { get { return item; } }

        #region private fields

        private Data.Customers.Entities.Account Account { set; get; }
        private List<Data.Products.Entities.ProductView> ProductViews { set; get; }

        //扣除服务费集合
        private Data.Customers.Entities.CustomerExpenseRelationCollection ExpenseRelations { set; get; }

        private decimal FrozenMoney { set; get; }


        private Customer Customer { set; get; }
        private CustomerStaffRelation Consultant { set; get; }
        private CustomerStaffRelation Educator { set; get; }



        private Dictionary<string, IEnumerable<BaseConstantEntity>> dictionary = null;
        private Dictionary<string, IEnumerable<BaseConstantEntity>> Dictionary
        {
            get
            {
                dictionary.IsNull(() => { dictionary = new Dictionary<string, IEnumerable<BaseConstantEntity>>(); dictionary.PrepareTypes(typeof(ProductView)); });
                return dictionary;
            }
        }

        #endregion


        [ObjectValidator]
        public Order Order { private set; get; }

        [ObjectValidator]
        public OrderItemCollection OrderItems { private set; get; }

        [ObjectValidator]
        public AssetCollection Assets { set; get; }


        /// <summary>
        /// 插班 排课信息
        /// </summary>
        [ObjectValidator]
        public AssignCollection Assigns { set; get; }

        /// <summary>
        /// 插班 排课信息
        /// </summary>
        [ObjectValidator]
        public ClassLessonItemCollection ClassLessonItems { set; get; }

        /// <summary>
        /// 是否需要审批
        /// </summary>
        public bool IsApprove { private set; get; }

        public SubmitOrderModel FillOrder()
        {
            if (Order == null)
            {

                FillCustomerInfo();
                FillAccount();

                var parentInfo = Service.CustomerService.GetPrimaryParentByCustomerId(CustomerID);
                //var customerInfo = Service.CustomerService.GetCustomerByCustomerId(CustomerID);

                Order = new Order()
                {
                    OrderID = UuidHelper.NewUuidString(),
                    OrderNo = PPTS.Data.Orders.Helper.GetOrderCode("NOD"),
                    CustomerID = CustomerID,
                    ChargeApplyID = ChargeApplyID,
                    SpecialType = SpecialType,
                    SpecialMemo = SpecialMemo,
                    AccountID = AccountID,
                    AccountCode = Account.AccountCode,
                    CustomerCode = Customer.CustomerCode,
                    CustomerGrade = Customer.Grade,
                    CustomerGradeName = Dictionary.GetCategoryName("c_codE_ABBR_CUSTOMER_GRADE", Customer.Grade),
                    CustomerName = Customer.CustomerName,
                    ParentID = parentInfo != null ? parentInfo.ParentID : "",
                    ParentName = parentInfo != null ? parentInfo.ParentName : "",
                    OrderType = ListType,

                    OrderStatus = OrderStatus.PendingApproval,
                    ProcessStatus = (int)ProcessStatusDefine.Processing,
                    CampusName = Customer.CampusName,
                    CampusID = Customer.CampusID,
                };

                if (Consultant != null)
                {
                    Order.ConsultantID = Consultant.StaffID;
                    Order.ConsultantJobID = Consultant.StaffJobID;
                    Order.ConsultantName = Consultant.StaffName;
                }
                if (Educator != null)
                {
                    Order.EducatorID = Educator.StaffID;
                    Order.EducatorJobID = Educator.StaffJobID;
                    Order.EducatorName = Educator.StaffName;
                }


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

                MCS.Library.OGUPermission.OguObjectCollection<MCS.Library.OGUPermission.IOrganization> oguCollection = null;


                if (ListType == OrderType.Freebie)
                {
                    preset = Service.ProductService.GetPresentByOrgId(Customer.CampusID);
                }

                var campusIds = carts.Select(m => m.ProductCampusID).Distinct().ToArray();
                oguCollection = OGUExtensions.GetOrganizationByIDs(campusIds);


                OrderItems = new OrderItemCollection();
                var i = 1;
                foreach (var product in ProductViews)
                {
                    var cart = carts.Single(s => s.ProductID == product.ProductID);
                    var submitItem = item.Single(m => m.ProductID == product.ProductID);
                    var oitem = mapper.Map<OrderItem>(product);

                    oitem.ItemNo = Order.OrderNo + (i++);

                    oitem.CategoryTypeName = product.CategoryName;
                    oitem.OrderPrice = product.ProductPrice;
                    oitem.OrderAmount = 1;
                    oitem.CategoryType = ((int)product.CategoryType).ToString();
                    oitem.DiscountType = ((int)DiscountTypeDefine.None).ToString();
                    oitem.DiscountRate = 1;

                    oitem.JoinedClassID = cart.ClassID;

                    product.Process(oitem, submitItem, preset, Account);

                    oitem.CourseLevelName = Dictionary.GetCategoryName("CourseLevel", oitem.CourseLevel);
                    oitem.ProductUnitName = Dictionary.GetCategoryName("ProductUnit", oitem.ProductUnit);
                    oitem.GradeName = Dictionary.GetCategoryName("c_codE_ABBR_CUSTOMER_GRADE", oitem.Grade);
                    oitem.SubjectName = Dictionary.GetCategoryName("c_codE_ABBR_BO_Product_TeacherSubject", oitem.Subject);


                    oitem.ProductCampusID = cart.ProductCampusID;
                    oguCollection.SingleOrDefault(m => m.ID == oitem.ProductCampusID).IsNotNull(m => { oitem.ProductCampusName = m.Name; });

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
                    asset.AssetRefPID = item.OrderID;

                    asset.Amount = item.RealAmount;
                    asset.Price = item.RealPrice;

                    asset.AssetType = ProductViews.SingleOrDefault(m => m.ProductID == item.ProductID).HasCourse == 1 ? AssetTypeDefine.Course : AssetTypeDefine.NonCourse;
                    asset.AssetRefType = AssetRefTypeDefine.Order;

                    asset.CustomerID = CustomerID;
                    asset.AssetCode = item.ItemNo;
                    asset.CustomerCode = Order.CustomerCode;
                    asset.CustomerName = Order.CustomerName;

                    FillUser(asset);

                    Assets.Add(asset);
                }
            }
            return this;
        }



        /// <summary>
        /// 插班订购 填充排课信息
        /// </summary>
        /// <returns></returns>
        public SubmitOrderModel FillAssignAndClassLessonItem()
        {

            //插班订购
            if (Order.OrderType == OrderType.Transfer && null == Assigns)
            {
                FillCustomerInfo();

                Assigns = new AssignCollection();
                ClassLessonItems = new ClassLessonItemCollection();

                OrderItems.ForEach(orderItem =>
                {

                    Class c = ClassesAdapter.Instance.LoadByClassID(orderItem.JoinedClassID);
                    ClassLessonCollection clc = ClassLessonsAdapter.Instance.LoadCollectionByClassID(orderItem.JoinedClassID);
                    List<ClassLesson> clc_ = clc.OrderBy(c_ => c_.StartTime).Skip(clc.Count() - Convert.ToInt32(orderItem.RealAmount)).ToList();
                    Asset assetItem = Assets.Find(a => a.AssetRefID == orderItem.ItemID);
                    clc_.ForEach(cl =>
                    {

                        ClassLessonItem cli = new ClassLessonItem();
                        Assign a = new Assign();

                        a.AssignID = UuidHelper.NewUuidString();
                        a.AssignStatus = AssignStatusDefine.Assigned;
                        a.AssignSource = AssignSourceDefine.Automatic;
                        a.ConfirmStatus = ConfirmStatusDefine.Unconfirmed;
                        a.AssetID = assetItem.AssetID;
                        a.AssetCode = assetItem.AssetCode;
                        a.CustomerID = assetItem.CustomerID;
                        a.CustomerName = Customer.CustomerName;
                        a.CustomerCode = Customer.CustomerCode;

                        if (Consultant != null)
                        {
                            a.ConsultantID = Consultant.StaffID;
                            a.ConsultantJobID = Consultant.StaffJobID;
                            a.ConsultantName = Consultant.StaffName;
                        }
                        if (Educator != null)
                        {
                            a.EducatorID = Educator.StaffID;
                            a.EducatorJobID = Educator.StaffJobID;
                            cli.EducatorName = Educator.StaffName;
                        }

                        a.ProductID = orderItem.ProductID;
                        a.ProductName = orderItem.ProductName;
                        a.ProductCode = orderItem.ProductCode;
                        //教室信息暂不处理
                        a.TeacherID = cl.TeacherID;
                        a.TeacherName = cl.TeacherName;
                        a.Grade = orderItem.Grade;
                        a.GradeName = orderItem.GradeName;
                        a.Subject = orderItem.Subject;
                        a.SubjectName = orderItem.SubjectName;
                        a.DurationValue = orderItem.LessonDurationValue;
                        a.AssignPrice = assetItem.Price;
                        a.StartTime = cl.StartTime;
                        a.EndTime = cl.EndTime;
                        FillUser(a);

                        Assigns.Add(a);

                        #region ClassLessonItem

                        cli.LessonID = cl.LessonID;
                        //cli.SortNo = i;
                        cli.AssignID = a.AssignID;
                        cli.AssignStatus = AssignStatusDefine.Assigned;
                        cli.ConfirmStatus = ConfirmStatusDefine.Unconfirmed;
                        cli.AssetID = assetItem.AssetID;
                        cli.AssetCode = assetItem.AssetCode;
                        cli.CustomerID = assetItem.CustomerID;
                        cli.CustomerCode = Customer.CustomerCode;
                        cli.CustomerName = Customer.CustomerName;
                        cli.CustomerCampusID = Customer.CampusID;
                        cli.CustomerCampusName = Customer.CampusName;
                        cli.CustomerGrade = orderItem.Grade;
                        cli.CustomerGradeName = orderItem.GradeName;
                        if (Consultant != null)
                        {
                            cli.ConsultantID = Consultant.StaffID;
                            cli.ConsultantJobID = Consultant.StaffJobID;
                            cli.ConsultantName = Consultant.StaffName;
                        }
                        if (Educator != null)
                        {
                            cli.EducatorID = Educator.StaffID;
                            cli.EducatorJobID = Educator.StaffJobID;
                            cli.EducatorName = Educator.StaffName;
                        }

                        FillUser(cli);

                        ClassLessonItems.Add(cli);
                        #endregion

                    });
                    assetItem.AssignedAmount += orderItem.RealAmount;

                });

            }
            return this;
        }

        /// <summary>
        /// 是否需要审批
        /// </summary>
        public SubmitOrderModel PrepareIsApprove()
        {
            if (OrderItems != null)
            {

                IsApprove.FalseAction(() => { IsApprove = OrderItems.Any(m => m.DiscountType == ((int)DiscountTypeDefine.Special).ToString()); });

                IsApprove.FalseAction(() =>
                {

                    IsApprove = OrderItems.Any(m => m.CategoryType == ((int)CategoryType.YouXue).ToString() &&
                                        (
                                            m.DiscountType == ((int)DiscountTypeDefine.Tunland).ToString() ||
                                            m.OrderAmount * m.OrderPrice - m.RealAmount * m.RealPrice > m.PromotionQuota
                                        )
                                    );

                });

                IsApprove.FalseAction(() =>
                {
                    Order.OrderStatus = OrderStatus.ApprovalPass;
                });
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
                else if (info is AccountRecord)
                {
                    var record = info as AccountRecord;
                    record.BillerID = Order.CreatorID;
                    record.BillerName = Order.CreatorName;
                    record.BillerJobID = CurrentUser.GetCurrentJob().ID;
                    record.BillerJobName = CurrentUser.GetCurrentJob().Name;
                }
            }

        }

        private void FillCustomerInfo()
        {
            if (Educator == null || Consultant == null || Customer == null)
            {
                var customerStaffRelation = Service.CustomerService.GetCustomerStaffRelationByCustomerId(CustomerID);

                customerStaffRelation.IsNotNull(m =>
                {
                    Consultant = customerStaffRelation.CustomerStaffRelationCollection.Find(r => r.RelationType == CustomerRelationType.Consultant);
                    Educator = customerStaffRelation.CustomerStaffRelationCollection.Find(r => r.RelationType == CustomerRelationType.Educator);
                    Customer = customerStaffRelation.Customer;
                });
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
            //提交数据是否有误
            Oparams.Any(m => m.OrderAmount < 0 || m.SpecialRate < 0 || m.PresentAmount < 0).TrueThrow("提交数据有误！");

            OrderItems.Any(m => m.CategoryType == ((int)CategoryType.OneToOne).ToString() && (m.OrderAmount < 0 || (double)m.OrderAmount % 0.5 != 0)).TrueThrow("1对1类产品只支持正数，且大于等于0.5，以0.5为最小单位!");

            ProductViews.Any(m => m.SpecialAllowed == 1 &&
            item.Single(s => s.ProductID == m.ProductID).SpecialRate.HasValue &&
            (0 > item.Single(s => s.ProductID == m.ProductID).SpecialRate.Value || (double)item.Single(s => s.ProductID == m.ProductID).SpecialRate.Value > 0.9999)).TrueThrow("提交特殊折扣存在问题！");

            OrderItems.Any(m =>
        (m.CategoryType == ((int)CategoryType.Real).ToString() || m.CategoryType == ((int)CategoryType.Virtual).ToString()) && (double)m.OrderAmount % 10 == 0
        ).TrueThrow("实物、虚拟只支持正整数!");

            //是否含不销售的产品
            var fileterProducts = ProductViews.Where(m => m.ProductStatus != ProductStatus.Enabled);
            (fileterProducts.Count() > 0).TrueAction(() =>
            {
                var message = string.Join(",", fileterProducts.Select(m => m.ProductName));
                (fileterProducts.Count() > 0).TrueThrow(message + " " + " 产品停止销售，请选择其他产品！");
            });

            //插班订购
            (Order.OrderType == OrderType.Transfer).TrueAction(() =>
            {
                var campuIds = OrderItems.Select(item => item.ProductCampusID).ToArray();
                var productIds = OrderItems.Select(item => item.ProductID).ToArray();
                Service.ProductService.IsExistsTransfer(campuIds, productIds).FalseThrow("提交插班信息有误！");

                var classes = new ClassCollection();
                OrderItems.ForEach(m => classes.Add(new Class() { ProductID = m.ProductID, ClassID = m.JoinedClassID })).ToList();
                ClassesAdapter.Instance.IsExistsTransfer(classes).FalseThrow("提交插班信息有误！");

            });

            //常规订购
            (Order.OrderType == OrderType.Ordinary &&
             ProductViews.Any(m => m.CategoryType == CategoryType.YouXue) &&
             ProductViews.Any(m => m.CategoryType != CategoryType.YouXue)
             ).TrueThrow("游学类产品不能与其他类产品同时订购");


            //是否存在 未完成的退费操作
            DebookOrderAdapter.Instance.ExistsPendingApprovalInContext(CustomerID);
            Data.Orders.ConnectionDefine.GetDbContext().DoAction(db => db.ExecuteNonQuerySqlInContext());

            //买赠订购
            //有未完成的退费操作不允许订购
            //提交订单后，扣减对应账户的可用金额，订购资金余额增加对应金额。

            //常规订购
            //有未完成的退费操作不允许订购
            //游学类产品不能与其他类产品同时订购

            //插班订购
            //有未完成的退费操作不允许订购


            //扣除服务费集合
            ExpenseRelations = new CustomerExpenseRelationCollection();

            var totalMoney = OrderItems.Sum(m => m.RealPrice * m.RealAmount);

            #region 综合服务费



            //1对1  班组 
            var categoryTypes = new List<string>() { ((int)CategoryType.OneToOne).ToString(), ((int)CategoryType.CalssGroup).ToString() };
            if (OrderItems.Any(m => categoryTypes.Contains(m.CategoryType)))
            {
                //是否扣除过综合服务费
                var kfdict = Service.CustomerService.GetWhetherToDeductServiceChargeByCustomerId(CustomerID);
                if (!kfdict[3])
                {
                    //获取该校区综合服务费
                    var expenses = Service.ProductService.GetServiceChargeByCampusId(Customer.CampusID);

                    if (expenses.Any(m => m.ExpenseType == "3"))
                    {

                        var item = expenses.First(m => m.ExpenseType == "3");
                        ExpenseRelations.Add(new CustomerExpenseRelation()
                        {
                            ExpenseID = item.ExpenseID,
                            ExpenseType = item.ExpenseType,
                            ExpenseMoney = item.ExpenseValue,
                            AccountID = AccountID,
                            CustomerID = CustomerID
                        });
                        totalMoney += item.ExpenseValue;
                    }
                    else
                    {

                        categoryTypes.Any(mm => !kfdict[Convert.ToInt32(mm)] && OrderItems.Any(m => m.CategoryType == mm) && !expenses.Any(m => m.ExpenseType == mm)).TrueThrow("该校区存在未创建综合服务费");

                        categoryTypes.ForEach(mm =>
                        {
                            if (!kfdict[Convert.ToInt32(mm)] && OrderItems.Any(m => m.CategoryType == mm))
                            {
                                var item = expenses.First(m => m.ExpenseType == mm);
                                ExpenseRelations.Add(new CustomerExpenseRelation()
                                {
                                    ExpenseID = item.ExpenseID,
                                    ExpenseType = item.ExpenseType,
                                    ExpenseMoney = item.ExpenseValue,
                                    AccountID = AccountID,
                                    CustomerID = CustomerID
                                });
                            }
                        });


                    }

                }
            }

            #endregion


            FillFrozenMoney();
            (totalMoney + FrozenMoney > Account.AccountMoney).TrueThrow("该学员帐户余额不足");

            if (ExpenseRelations.Count > 0)
            {
                Service.CustomerService.DeductExpenses(ExpenseRelations);
            }



        }


        #region 异步使用

        private Data.Customers.Entities.AccountRecord Record { set; get; }

        private void FillAccountRecord()
        {
            Record.IsNull(() =>
            {
                Order = OrdersAdapter.Instance.Load(Order.OrderID);

                Record = new AccountRecord()
                {
                    CampusID = Order.CampusID,
                    CustomerID = CustomerID,
                    AccountID = AccountID,
                    RecordID = UuidHelper.NewUuidString(),
                    RecordType = AccountRecordType.Order,
                    RecordFlag = -1,
                    BillID = Order.OrderID,
                    BillNo = Order.OrderNo,
                    BillRelateID = Order.OrderID,
                    BillType = ((int)Order.OrderType).ToString(),
                    BillTypeName = EnumItemDescriptionAttribute.GetDescription(Order.OrderType),
                    BillTime = Order.OrderTime
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

            TxProcess.ProcessName = "提交订单";
            TxProcess.Category = "提交订单";

            //process.ConnectionName = MCS.Library.SOA.DataObjects.ConnectionDefine.DBConnectionName;


            var debitMoney = Record.BillMoney = OrderItems.Sum(m => m.RealPrice * m.RealAmount);




            TxActivity activity1 = TxProcess.Activities.AddActivity("帐户扣钱");
            activity1.AddActionService<IAccountTransactionService>(
                UriSettings.GetConfig().CheckAndGet("pptsServices", "accountTransactionService").ToString(),
                proxy => proxy.DebitAccount(TxProcess.ProcessID, AccountID, debitMoney, Record));

            activity1.AddCompensationService<IAccountTransactionService>(
                UriSettings.GetConfig().CheckAndGet("pptsServices", "accountTransactionService").ToString(),
                proxy => proxy.RollbackDebitAccount(TxProcess.ProcessID, AccountID, debitMoney, Record));



            TxActivity activity2 = TxProcess.Activities.AddActivity("订单处理");
            activity2.AddActionService<IOrderTransactionService>(
                UriSettings.GetConfig().CheckAndGet("pptsServices", "orderTransactionService").ToString(),
                proxy => proxy.OrderProcess(TxProcess.ProcessID, Order.OrderID, ProcessStatusDefine.Processed, Order, OrderItems.ToList(), Assets.ToList(), Assigns == null ? null : Assigns.ToList(), ClassLessonItems == null ? null : ClassLessonItems.ToList()));

            activity2.AddCompensationService<IOrderTransactionService>(
                UriSettings.GetConfig().CheckAndGet("pptsServices", "orderTransactionService").ToString(),
                proxy => proxy.OrderProcess(TxProcess.ProcessID, Order.OrderID, ProcessStatusDefine.Error, null, null, null, null, null));


            if (ExpenseRelations.Count > 0)
            {
                ExpenseRelations.ForEach(m => m.OrderID = Order.OrderID);

                TxActivity activity6 = TxProcess.Activities.AddActivity("同步扣除综合服务费订单ID");
                activity6.AddActionService<IAccountTransactionService>(
                    UriSettings.GetConfig().CheckAndGet("pptsServices", "accountTransactionService").ToString(),
                    proxy => proxy.SyncExpense(TxProcess.ProcessID, ExpenseRelations.ToList()));
            }


            return TxProcess;
        }

        #endregion

        #region 审批

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
                default:
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
            if (product.SpecialAllowed == 1 && submitItem.SpecialRate.HasValue)
            {
                //if (oitem.DiscountRate > submitItem.SpecialRate)
                {
                    oitem.DiscountRate = oitem.SpecialRate = submitItem.SpecialRate.Value;
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
            if (product.SpecialAllowed == 1 && submitItem.SpecialRate.HasValue)
            {
                if (oitem.DiscountRate > submitItem.SpecialRate)
                {
                    oitem.DiscountRate = oitem.SpecialRate = submitItem.SpecialRate.Value;
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

    class OtherStrategy : CalculateItem
    {

    }

}