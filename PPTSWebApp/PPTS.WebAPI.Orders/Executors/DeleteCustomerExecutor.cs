using MCS.Library.Data.Executors;
using MCS.Library.SOA.DataObjects;
using PPTS.Data.Orders;
using PPTS.Data.Orders.Adapters;
using PPTS.Data.Orders.Entities;
using PPTS.Data.Orders.Executors;
using PPTS.WebAPI.Orders.ViewModels.ClassGroup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPTS.WebAPI.Orders.Executors
{
    [DataExecutorDescription("移除班级班级")]
    public class DeleteCustomerExecutor : PPTSEditClassGroupExecutorBase<DeleteCustomerModel>
    {
        public DeleteCustomerExecutor(DeleteCustomerModel Model)
            : base(Model, null)
        {

        }

        protected override void PrepareData(DataExecutionContext<UserOperationLogCollection> context)
        {
            base.PrepareData(context);
            Class c = ClassesAdapter.Instance.LoadByClassID(Model.ClassID);
            ClassLessonCollection clc = ClassLessonsAdapter.Instance.LoadCollectionByClassID(Model.ClassID);
            ClassLessonItemCollection clic = ClassLessonItemsAdapter.Instance.LoadCollection(clc);
            IList<string> assigs = new List<string>();
            foreach (var item in clic)
            {
                assigs.Add(item.AssignID);
            }
            AssignCollection ac = assigs.Count > 0 ? AssignsAdapter.Instance.LoadCollection(assigs) : new AssignCollection();
            ac = ac == null ? new AssignCollection() : ac;

            //该生未确认过课次，可移出班组，否则系统提示：已确认过课次，无法移出。
            foreach (var cli in clic)
            {
                IList<ClassLesson> list = clc.FindAll(cl => cl.LessonID == cli.LessonID && Model.CustomerIDs.Contains(cli.CustomerID));
                foreach (var item in list)
                {
                    if (item.ConfirmStatus == ConfirmStatusDefine.Confirmed)
                    {
                        throw new ApplicationException( "已确认过课次，无法移出");                        
                    }
                }
            }


            c.ClassPeoples = c.ClassPeoples - Model.CustomerIDs.Length;
            ClassesAdapter.Instance.UpdateInContext(c);
            foreach (var cli in clic)
            {
                if (Model.CustomerIDs.Contains(cli.CustomerID))
                {
                    cli.AssignStatus = AssignStatusDefine.Invalid;
                    ClassLessonItemsAdapter.Instance.UpdateInContext(cli);
                }
            }
            foreach (var a in ac)
            {
                if (Model.CustomerIDs.Contains(a.CustomerID))
                {
                    a.AssignStatus = AssignStatusDefine.Invalid;
                    AssignsAdapter.Instance.UpdateInContext(a);
                    AssetAdapter.Instance.IncreaseAssignedAmountInContext(a.AssetID, 1, a.CreatorID, a.CreatorName);
                }
            }
        }

        /// <summary>
        /// 准备日志信息
        /// </summary>
        /// <param name="context"></param>
        protected override void PrepareOperationLog(DataExecutionContext<UserOperationLogCollection> context)
        {
            base.PrepareOperationLog(context);


        }


    }
}