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
    [DataExecutorDescription("更换教师")]
    public class EditTeacherExecutor : PPTSEditClassGroupExecutorBase<EditTeacherModel>
    {
        public EditTeacherExecutor(EditTeacherModel Model)
            : base(Model, null)
        {

        }

        protected override void PrepareData(DataExecutionContext<UserOperationLogCollection> context)
        {
            base.PrepareData(context);
            Class c = ClassesAdapter.Instance.LoadByClassID(Model.ClassID);
            IList<ClassLesson> clc = ClassLessonsAdapter.Instance.LoadCollectionByClassID(Model.ClassID).OrderBy(cl => cl.StartTime).SkipWhile((cl, index) => index < Model.StartLessonNum - 1).TakeWhile((cl, index) => index < Model.EndLessonNum - Model.StartLessonNum + 1).ToList();
            ClassLessonItemCollection clic = ClassLessonItemsAdapter.Instance.LoadCollection(clc);
            IList<string> assigs = new List<string>();
            foreach (var item in clic)
            {
                assigs.Add(item.AssignID);
            }
            AssignCollection ac = assigs.Count > 0 ? AssignsAdapter.Instance.LoadCollection(assigs) : new AssignCollection();
            ac = ac == null ? new AssignCollection() : ac;
            for (int i = 0; i < clc.Count; i++)
            {
                var item = clc[i];
                item.TeacherID = Model.TeacherID;
                item.TenantCode = Model.TeacherCode;
                item.TeacherName = Model.TeacherName;
                ClassLessonsAdapter.Instance.UpdateInContext(item);
            }
            for (int i = 0; i < ac.Count; i++)
            {
                var item = ac[i];
                item.TeacherID = Model.TeacherID;
                item.TenantCode = Model.TeacherCode;
                item.TeacherName = Model.TeacherName;
                AssignsAdapter.Instance.UpdateInContext(item);
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