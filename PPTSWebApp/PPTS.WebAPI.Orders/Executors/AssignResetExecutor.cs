using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using MCS.Library.Data.Executors;
using PPTS.Data.Orders.Entities;
using PPTS.Data.Orders.Executors;
using MCS.Library.Core;
using MCS.Library.SOA.DataObjects;
using PPTS.Data.Orders.Adapters;

namespace PPTS.WebAPI.Orders.Executors
{
    [DataExecutorDescription("调整排课")]
    public class AssignResetExecutor : PPTSEditAssignExecutorBase<AssignCollection>
    {
        public AssignResetExecutor(AssignCollection model)
            : base(model, null)
        {
            model.NullCheck("model");
        }

        protected override void PrepareData(DataExecutionContext<UserOperationLogCollection> context)
        {
            ///需要执行的判断
            ///1. 冻结状态学员不允许调整排课
            ///2. 只有排定状态的排课可以调课
            ///3. 学员排课及教师排课冲突检查
            base.PrepareData(context);
            foreach (var m in this.Model)
            {
                AssignsAdapter.Instance.CheckConflictAssignInContext(m);
            }
            AssignsAdapter.Instance.ResetAssignInContext(this.Model);
        }

        protected override void PrepareOperationLog(DataExecutionContext<UserOperationLogCollection> context)
        {
            base.PrepareOperationLog(context);
            //context.Logs.ForEach(log => log.ResourceID = this.Model);
        }


    }
}