using MCS.Library.Data.Adapters;
using MCS.Library.Data.Builder;
using MCS.Library.Data.Executors;
using MCS.Library.Data.Mapping;
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
    [DataExecutorDescription("删除班级")]
    public class DeleteClassExecutor : PPTSEditClassGroupExecutorBase<string>
    {        /// <summary>
             /// 
             /// </summary>
             /// <param name="model"></param>
             /// <param name="dataAction"></param>
        public DeleteClassExecutor(string  ID)
            : base(ID, null)
        {
            
        }

        protected override void PrepareData(DataExecutionContext<UserOperationLogCollection> context)
        {
            base.PrepareData(context);
            Class c = ClassesAdapter.Instance.LoadByClassID(Model);
            if (CheckDeleteClass(c))
            {
                ClassLessonCollection clc = ClassLessonsAdapter.Instance.LoadCollectionByClassID(Model);
                ClassLessonItemCollection clic = ClassLessonItemsAdapter.Instance.LoadCollection(clc);
                IList<string> assigs = new List<string>();
                foreach (var item in clic)
                {
                    assigs.Add(item.AssignID);
                }
                AssignCollection ac = assigs.Count > 0 ? AssignsAdapter.Instance.LoadCollection(assigs) : new AssignCollection();
                ac = ac == null ? new AssignCollection() : ac;

                c.ClassStatus = ClassStatusDefine.Deleted;
                ClassesAdapter.Instance.UpdateInContext(c);
                foreach (var cl in clc)
                {
                    cl.LessonStatus = LessonStatus.Deleted;
                    ClassLessonsAdapter.Instance.UpdateInContext(cl);
                }
                foreach (var cli in clic)
                {
                    cli.AssignStatus = AssignStatusDefine.Invalid;
                    ClassLessonItemsAdapter.Instance.UpdateInContext(cli);
                }
                foreach (var a in ac)
                {
                    a.AssignStatus = AssignStatusDefine.Invalid;
                    AssignsAdapter.Instance.UpdateInContext(a);
                    AssetAdapter.Instance.IncreaseAssignedAmountInContext(a.AssetID, 1, a.CreatorID, a.CreatorName);
                }

            }
            else {
                throw new ApplicationException("所有课次为“排定”的班级才可以删除");
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

        private bool CheckDeleteClass(Class c) {
            bool result = true;
            //所有课次为“排定”的班级才可以删除
            result = c!=null && c.ClassStatus == ClassStatusDefine.Createed;
            return result;
        }
    }

}