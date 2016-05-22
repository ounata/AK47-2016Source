using MCS.Library.Data.Executors;
using MCS.Library.SOA.DataObjects;
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
    [DataExecutorDescription("编辑课表")]
    public class EditClassLessonesExecutor : PPTSEditClassGroupExecutorBase<EditClassLessonesModel>
    {
        public EditClassLessonesExecutor(EditClassLessonesModel Model)
            : base(Model, null)
        {

        }

        protected override void PrepareData(DataExecutionContext<UserOperationLogCollection> context)
        {
            base.PrepareData(context);

            Class c = ClassesAdapter.Instance.LoadByClassID(Model.ClassID);
            ClassLessonCollection clc = ClassLessonsAdapter.Instance.LoadCollectionByClassID(Model.ClassID);
            ClassLesson cl = clc.Find(result => result.LessonID == Model.LessonID);
            ClassLessonItemCollection clic = ClassLessonItemsAdapter.Instance.LoadCollection(clc);
            IList<string> assigs = new List<string>();
            foreach (var item in clic)
            {
                assigs.Add(item.AssignID);
            }
            AssignCollection ac = assigs.Count > 0 ? AssignsAdapter.Instance.LoadCollection(assigs) : new AssignCollection();
            ac = ac == null ? new AssignCollection() : ac;
            if (Model.DayOfWeeks.Count > 0)
            {
                //修改选择课次及以后的上课时间
                IList< ClassLesson> clc_ = clc.FindAll(result => result.StartTime >= cl.StartTime);  
                List<DateTime> StartTimeList = Model.StartTimeList(clc_.Count);
                for (int i = 0; i < clc_.Count; i++)
                {
                    var cl_ = clc_[i];
                    var st = StartTimeList[i];
                    var ts = cl_.EndTime - cl_.StartTime;
                    cl_.StartTime = st;
                    cl_.EndTime = st + ts;
                    ClassLessonsAdapter.Instance.UpdateInContext(cl_);

                    var clic_ = clic.FindAll(result =>result.LessonID == cl_.LessonID);
                    foreach (var item in clic_)
                    {
                        var a_ = ac.Find(result => result.AssignID == item.AssignID);
                        a_.StartTime = cl_.StartTime;
                        a_.EndTime = cl_.EndTime;
                        AssignsAdapter.Instance.UpdateInContext(a_);
                    }
                }

            }
            else {
                //修改选择课次的上课时间
                var ts = cl.EndTime - cl.StartTime;
                cl.StartTime = Model.StartTime;
                cl.EndTime = Model.StartTime + ts;
                ClassLessonsAdapter.Instance.UpdateInContext(cl);
                var clic_ = clic.FindAll(result => result.LessonID == cl.LessonID);
                foreach (var item in clic_)
                {
                    var a_ = ac.Find(result => result.AssignID == item.AssignID);
                    a_.StartTime = cl.StartTime;
                    a_.EndTime = cl.EndTime;
                    AssignsAdapter.Instance.UpdateInContext(a_);
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