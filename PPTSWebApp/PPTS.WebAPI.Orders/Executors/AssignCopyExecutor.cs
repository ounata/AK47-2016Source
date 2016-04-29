using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PPTS.Data.Orders.Executors;
using PPTS.WebAPI.Orders.ViewModels.AssignConditions;
using PPTS.Data.Orders.Adapters;
using MCS.Library.Core;
using MCS.Library.Data.Executors;
using MCS.Library.SOA.DataObjects;
using PPTS.Data.Orders.Entities;

namespace PPTS.WebAPI.Orders.Executors
{
    [DataExecutorDescription("复制排课")]
    public class AssignCopyExecutor : PPTSEditAssignExecutorBase<AssignCopyModel>
    {
        public AssignCopyExecutor(AssignCopyModel model)
            : base(model, null)
        {
            model.NullCheck("model");
        }

        protected override void PrepareData(DataExecutionContext<UserOperationLogCollection> context)
        {
            ///需要执行的判断
            ///1. 检查学员是否被冻结
            ///2. 检查课表是否允许复制（排课是否可以复制标志）（另外班组排课的课表默认不允许排课）
            ///3. 如课表允许复制，则排定、异常、已上状态的课表能被复制
            ///4. 当前排课的数量 不大于 未排课课时数量（课时总数量减去已排课数量）
            ///5. 学员排课及教师排课冲突检查
            ///6. 插入排课记录及更新资产表已排课数量

            base.PrepareData(context);
            ///获取允许复制的排课记录
            IEnumerable<Assign> assignCollection = this.GetAllowCopyAssign();
            if (assignCollection == null || assignCollection.Count() == 0)
                return;
            ///计算复制源与目标间隔天数
            TimeSpan ts = this.Model.destDateCourseStart.Subtract(this.Model.srcDateCourseStart);
            int days = ts.Days;
            //AssignsAdapter.Instance.GetSqlContext().AppendSqlInContext("if ");
            //创建新的记录
            foreach (var m in assignCollection)
            {
                m.AssignID = UuidHelper.NewUuidString();             
                m.AssignTime = DateTime.MinValue;
                m.StartTime.AddDays(days);
                m.EndTime.AddDays(days);
                m.AssignStatus = Data.Orders.AssignStatusDefine.Assigned;
                m.AssignSource = Data.Orders.AssignSourceDefine.Manual;
                m.ConfirmStatus = Data.Orders.ConfirmStatusDefine.Unconfirmed;

                //m.ModifierID = this.Model;
                //m.ModifierName = this.Model;
                //m.CreatorID = this.Model;
                //m.CreatorName = this.Model;
                m.CreateTime = DateTime.MinValue;
                //添加更新
                AssignsAdapter.Instance.UpdateInContext(m);
            }
        }
       

        private IEnumerable<Assign> GetAllowCopyAssign()
        {
            AssignCollection ac = AssignsAdapter.Instance.LoadCollection(this.Model.srcDateCourseStart, this.Model.srcDateCourseEnd, this.Model.CustomerID);
            IEnumerable<Assign> assignCollection = null;
            ///挑选允许复制的记录
            if (ac != null)
            {
                assignCollection = ac.Where(p => p.CopyAllowed
                && (p.AssignStatus == Data.Orders.AssignStatusDefine.Assigned || p.AssignStatus == Data.Orders.AssignStatusDefine.Exception || p.AssignStatus == Data.Orders.AssignStatusDefine.Finished));
            }
            return assignCollection;
        }

        protected override void PrepareOperationLog(DataExecutionContext<UserOperationLogCollection> context)
        {
            base.PrepareOperationLog(context);
            //context.Logs.ForEach(log => log.ResourceID = this.Model.AssetID);
        }
    }
}