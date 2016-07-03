using MCS.Library.Configuration;
using MCS.Library.Core;
using MCS.Library.Data;
using MCS.Library.SOA.DataObjects;
using MCS.Library.SOA.DataObjects.AsyncTransactional;
using MCS.Library.WcfExtensions;
using PPTS.Contracts.Customers.Operations;
using PPTS.Contracts.Orders.Operations;
using PPTS.Data.Common;
using PPTS.Data.Customers;
using PPTS.Data.Customers.Entities;
using PPTS.Data.Orders;
using PPTS.Data.Orders.Adapters;
using PPTS.Data.Orders.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace PPTS.Services.Orders.Services
{

    public class WorkflowService : PPTS.Contracts.Orders.Operations.IWorkflowService
    {

        [WfJsonFormatter]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public void DebookOrderProcessCompleting(string CurrentResourceID, string CurrentUserID, string CurrentUserName)
        {
            var debookOrder = DebookOrderAdapter.Instance.Load(CurrentResourceID);
            debookOrder.NullCheck(CurrentResourceID + ":不存在！");
            var debookorderItemView = DebookOrderItemViewAdapter.Instance.LoadByDebookId(CurrentResourceID);
            debookorderItemView.NullCheck("debookorderItemView:不存在！");
            var accountRecord = FillAccountRecord(debookOrder, debookorderItemView);


            MutexReleaser mutex = new MutexReleaser(
                        new MutexReleaseParameter()
                        {
                            BillID = CurrentResourceID
                        });
            mutex.Release(() =>
            {


                var TxProcess = new TxProcess();

                TxProcess.ProcessName = "提交退订";
                TxProcess.Category = "提交退订";


                TxActivity activity1 = TxProcess.Activities.AddActivity("帐户退钱");
                activity1.AddActionService<IAccountTransactionService>(
                    UriSettings.GetConfig().CheckAndGet("pptsServices", "accountTransactionService").ToString(),
                    proxy => proxy.DebookAccount(TxProcess.ProcessID, accountRecord.AccountID, debookorderItemView.DebookMoney, accountRecord));

                activity1.AddCompensationService<IAccountTransactionService>(
                    UriSettings.GetConfig().CheckAndGet("pptsServices", "accountTransactionService").ToString(),
                    proxy => proxy.RollbackDebookAccount(TxProcess.ProcessID, accountRecord.AccountID, debookorderItemView.DebookMoney, accountRecord));


                TxActivity activity2 = TxProcess.Activities.AddActivity("重置退订订单状态");
                activity2.AddActionService<IOrderTransactionService>(
                    UriSettings.GetConfig().CheckAndGet("pptsServices", "orderTransactionService").ToString(),
                    proxy => proxy.ModifyDebookOrderStatus(TxProcess.ProcessID, debookOrder.DebookID, OrderStatus.ApprovalPass, ProcessStatusDefine.Processed, debookOrder.CreatorID, debookOrder.CreatorName));
                activity2.AddCompensationService<IOrderTransactionService>(
                    UriSettings.GetConfig().CheckAndGet("pptsServices", "orderTransactionService").ToString(),
                    proxy => proxy.ModifyDebookOrderStatus(TxProcess.ProcessID, debookOrder.DebookID, OrderStatus.ApprovalPass, ProcessStatusDefine.Error, debookOrder.CreatorID, debookOrder.CreatorName));


                using (System.Transactions.TransactionScope scope = TransactionScopeFactory.Create())
                {

                    TxProcess process = TxProcess;
                    TxProcessAdapter.GetInstance(MCS.Library.SOA.DataObjects.ConnectionDefine.DBConnectionName).Update(process);
                    InvokeServiceTaskAdapter.Instance.Push(process.ToStartWorkflowTask());

                    scope.Complete();
                }



            });

        }



        [WfJsonFormatter]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public void DebookOrderProcessCancelling(string CurrentResourceID, string CurrentUserID, string CurrentUserName)
        {
            var model = DebookOrderAdapter.Instance.Load(CurrentResourceID);
            model.NullCheck(CurrentResourceID + ":不存在！");

            MutexReleaser mutex = new MutexReleaser(
                        new MutexReleaseParameter()
                        {
                            BillID = CurrentResourceID
                        });
            mutex.Release(() =>
            {
                model.DebookStatus = ((int)OrderStatus.Reject).ToString();
                model.ProcessStatus = ((int)ProcessStatusDefine.Processed).ToString();
                model.ModifierID = CurrentUserID;
                model.ModifierName = CurrentUserName;

                DebookOrderAdapter.Instance.Update(model);
            });
        }

        private AccountRecord FillAccountRecord(DebookOrder Order,DebookOrderItemView item)
        {


            var tempOrderItem = OrderItemAdapter.Instance.Load(item.OrderItemID);
            var tempOrder = OrdersAdapter.Instance.Load(item.OrderID);

            var Record = new AccountRecord()
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

            //FillUser(Record);

            return Record;
        }


        [WfJsonFormatter]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public void SubmitOrderProcessCancelling(string CurrentResourceID, string CurrentUserID, string CurrentUserName)
        {
            OrdersAdapter.Instance.Load(CurrentResourceID).NullCheck(CurrentResourceID + ":不存在！");

            MutexReleaser mutex = new MutexReleaser(
                        new MutexReleaseParameter()
                        {
                            BillID = CurrentResourceID
                        });
            mutex.Release(() =>
            {
                OrdersAdapter.Instance.ModifyStatusInContext(CurrentResourceID, (int)ProcessStatusDefine.Processed, (int)OrderStatus.Reject, CurrentUserID, CurrentUserName);
            });
        }


        [WfJsonFormatter]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public void SubmitOrderProcessCompleting(string CurrentResourceID, string CurrentUserID, string CurrentUserName)
        {
            OrdersAdapter.Instance.Load(CurrentResourceID).NullCheck(CurrentResourceID + ":不存在！");

            Order order = null;
            OrderItemCollection orderItems = null;
            AssetCollection assets = null;
            AssignCollection Assigns = null;
            ClassLessonItemCollection ClassLessonItems = null;
            Data.Customers.Entities.AccountRecord accountRecord = null;

            OrdersAdapter.Instance.LoadInContext(CurrentResourceID, null, s => { order = s.FirstOrDefault(); });
            OrderItemAdapter.Instance.LoadInContext(CurrentResourceID, null, s => { orderItems = s; });

            PPTS.Data.Orders.ConnectionDefine.GetDbContext().DoAction(db => db.ExecuteDataSetSqlInContext());

            (order == null || orderItems == null).TrueThrow(CurrentResourceID + ":order or orderItems 为null");

            assets = FillAssets(order, orderItems);
            FillAssignAndClassLessonItem(order, orderItems, assets, Assigns, ClassLessonItems);
            accountRecord = FillAccountRecord(order);

            MutexReleaser mutex = new MutexReleaser(
                        new MutexReleaseParameter()
                        {
                            BillID = CurrentResourceID
                        });

            mutex.Release(() =>
            {

                using (System.Transactions.TransactionScope scope = TransactionScopeFactory.Create())
                {

                    TxProcess process = PrepareProcess(order, orderItems, 
                        accountRecord,
                        assets == null ? null : assets.ToList(), 
                        Assigns == null ? null : Assigns.ToList(), 
                        ClassLessonItems == null ? null : ClassLessonItems.ToList());
                    TxProcessAdapter.GetInstance(MCS.Library.SOA.DataObjects.ConnectionDefine.DBConnectionName).Update(process);
                    InvokeServiceTaskAdapter.Instance.Push(process.ToStartWorkflowTask());

                    scope.Complete();
                }

            });


        }

        private TxProcess PrepareProcess(Order order,
            OrderItemCollection OrderItems,
            Data.Customers.Entities.AccountRecord Record,
            List<Asset> Assets,
            List<Assign> Assigns,
            List<ClassLessonItem> ClassLessonItems
            )
        {


            var TxProcess = new TxProcess();

            TxProcess.ProcessName = "提交订单";
            TxProcess.Category = "提交订单";


            var debitMoney = Record.BillMoney = OrderItems.Sum(m => m.RealPrice * m.RealAmount);


            TxActivity activity1 = TxProcess.Activities.AddActivity("帐户扣钱");
            activity1.AddActionService<IAccountTransactionService>(
                UriSettings.GetConfig().CheckAndGet("pptsServices", "accountTransactionService").ToString(),
                proxy => proxy.DebitAccount(TxProcess.ProcessID, order.AccountID, debitMoney, Record));

            activity1.AddCompensationService<IAccountTransactionService>(
                UriSettings.GetConfig().CheckAndGet("pptsServices", "accountTransactionService").ToString(),
                proxy => proxy.RollbackDebitAccount(TxProcess.ProcessID, order.AccountID, debitMoney, Record));



            TxActivity activity2 = TxProcess.Activities.AddActivity("订单处理");
            activity2.AddActionService<IOrderTransactionService>(
                UriSettings.GetConfig().CheckAndGet("pptsServices", "orderTransactionService").ToString(),
                proxy => proxy.OrderProcess(TxProcess.ProcessID, order.OrderID, ProcessStatusDefine.Processed, order, OrderItems.ToList(), Assets, Assigns, ClassLessonItems));

            activity2.AddCompensationService<IOrderTransactionService>(
                UriSettings.GetConfig().CheckAndGet("pptsServices", "orderTransactionService").ToString(),
                proxy => proxy.OrderProcess(TxProcess.ProcessID, order.OrderID, ProcessStatusDefine.Error, null, null, null, null, null));



            var CustomerExpenseRelations = new List<Data.Customers.Entities.CustomerExpenseRelation>();

            OrderItems.ForEach(m =>
            {
                if (m.CategoryType == ((int)CategoryType.OneToOne).ToString() || m.CategoryType == ((int)CategoryType.CalssGroup).ToString())
                {
                    CustomerExpenseRelations.Add(new Data.Customers.Entities.CustomerExpenseRelation() { CustomerID = order.CustomerID, ExpenseType = m.CategoryType, OrderID = order.OrderID });
                }
            });

            (CustomerExpenseRelations.Count == 2).TrueAction(() => {
                CustomerExpenseRelations.Add(new Data.Customers.Entities.CustomerExpenseRelation() { CustomerID = order.CustomerID, ExpenseType = "3", OrderID = order.OrderID });
            });

            TxActivity activity6 = TxProcess.Activities.AddActivity("同步扣除综合服务费订单ID");
            activity6.AddActionService<IAccountTransactionService>(
                UriSettings.GetConfig().CheckAndGet("pptsServices", "accountTransactionService").ToString(),
                proxy => proxy.SyncExpense(TxProcess.ProcessID, CustomerExpenseRelations));

            return TxProcess;
        }

        private void FillUser(object info, Order order)
        {
            if (order != null)
            {
                if (info is Data.Common.Entities.IEntityWithCreator)
                {
                    var obj = (info as Data.Common.Entities.IEntityWithCreator);
                    obj.CreatorID = order.CreatorID;
                    obj.CreatorName = order.CreatorName;
                }
                if (info is Data.Common.Entities.IEntityWithModifier)
                {
                    var obj = (info as Data.Common.Entities.IEntityWithModifier);
                    obj.ModifierID = order.ModifierID;
                    obj.ModifierName = order.ModifierName;
                }

                if (info is Data.Customers.Entities.AccountRecord)
                {
                    var record = info as Data.Customers.Entities.AccountRecord;
                    record.BillerID = order.CreatorID;
                    record.BillerName = order.CreatorName;
                    record.BillerJobID = order.SubmitterJobID;
                    record.BillerJobName = order.SubmitterJobName;
                }
            }

        }

        private Data.Customers.Entities.AccountRecord FillAccountRecord(Order order)
        {
            Data.Customers.Entities.AccountRecord Record = null;

            Record = new Data.Customers.Entities.AccountRecord()
            {
                CampusID = order.CampusID,
                CustomerID = order.CustomerID,
                AccountID = order.AccountID,
                RecordID = UuidHelper.NewUuidString(),
                RecordType = AccountRecordType.Order,
                RecordFlag = -1,
                BillID = order.OrderID,
                BillNo = order.OrderNo,
                BillRelateID = order.OrderID,
                BillType = ((int)order.OrderType).ToString(),
                BillTypeName = EnumItemDescriptionAttribute.GetDescription(order.OrderType),
                BillTime = order.OrderTime
            };

            FillUser(Record, order);


            return Record;
        }

        private void FillAssignAndClassLessonItem(Order order, OrderItemCollection orderItems, AssetCollection Assets, AssignCollection Assigns, ClassLessonItemCollection ClassLessonItems)
        {

            //插班订购
            if (order.OrderType == OrderType.Transfer && null == Assigns)
            {
                var customerStaffRelation = Service.CustomerService.GetCustomerStaffRelationByCustomerId(order.CustomerID);
                Data.Customers.Entities.CustomerStaffRelation Consultant = null;
                Data.Customers.Entities.CustomerStaffRelation Educator = null;
                Data.Customers.Entities.Customer Customer = null;
                customerStaffRelation.IsNotNull(m =>
                {
                    Consultant = customerStaffRelation.CustomerStaffRelationCollection.Find(r => r.RelationType == CustomerRelationType.Consultant);
                    Educator = customerStaffRelation.CustomerStaffRelationCollection.Find(r => r.RelationType == CustomerRelationType.Educator);
                    Customer = customerStaffRelation.Customer;
                });

                Assigns = new AssignCollection();
                ClassLessonItems = new ClassLessonItemCollection();

                orderItems.ForEach(orderItem =>
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

                        FillUser(a, order);

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

                        FillUser(cli, order);

                        ClassLessonItems.Add(cli);

                        #endregion

                    });
                    assetItem.AssignedAmount += orderItem.RealAmount;

                });

            }

        }

        private AssetCollection FillAssets(Order order, OrderItemCollection orderItems)
        {
            var Assets = new AssetCollection();
            var mapper = new AutoMapper.MapperConfiguration(c => { c.CreateMap<OrderItem, Asset>(); }).CreateMapper();
            foreach (var item in orderItems)
            {
                var asset = mapper.Map<Asset>(item);
                asset.AssetID = UuidHelper.NewUuidString();
                asset.AccountID = order.AccountID;
                asset.AssetRefID = item.ItemID;
                asset.AssetRefPID = item.OrderID;
                asset.Amount = item.RealAmount;
                asset.AssetType = (item.CategoryType == ((int)CategoryType.OneToOne).ToString() || item.CategoryType == ((int)CategoryType.CalssGroup).ToString()) ? AssetTypeDefine.Course : AssetTypeDefine.NonCourse;
                asset.AssetRefType = AssetRefTypeDefine.Order;
                asset.Price = item.RealPrice;
                asset.CustomerID = order.CustomerID;
                asset.AssetCode = item.ItemNo;
                asset.CustomerCode = order.CustomerCode;
                asset.CustomerName = order.CustomerName;

                FillUser(asset, order);

                Assets.Add(asset);
            }

            return Assets;
        }

        
    }
}
