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
    [DataExecutorDescription("取消排课")]
    public class AssignCancelExecutor : PPTSEditAssignExecutorBase<AssignCollection>
    {
        public AssignCancelExecutor(AssignCollection model)
            : base(model, null)
        {
            model.NullCheck("model");
        }

        protected override void PrepareData(DataExecutionContext<UserOperationLogCollection> context)
        {
            ///需要执行的判断
            ///1. 排定状态可以取消
            ///2. 取消后，需要将资产表中的已排课时数量减去取消掉的课时量
            base.PrepareData(context);

            IList<string> assignID = this.Model.Select(p => p.AssetID).ToList();
            AssignCollection ac = AssignsAdapter.Instance.LoadCollection(assignID);
            if (ac == null)
                return;
            ///排课可能来自不同的资产，所以要分组处理
            IEnumerable<IGrouping<string, Assign>> result = ac.GroupBy(p => p.AssetID);
            foreach (IGrouping<string, Assign> g in result)
            {
                IEnumerable<Assign> assigns = g.ToList<Assign>();

                assigns = ac.Where(p => p.AssignStatus == Data.Orders.AssignStatusDefine.Assigned);
                if (assigns == null)
                    continue;

                decimal assignedAmount = assigns.Sum(p => p.Amount);
                assignID = assigns.Select(p => p.AssignID).ToList();

                AssignsAdapter.Instance.CancelAssignInContext(assignID, string.Empty, string.Empty);
                AssetAdapter.Instance.DecreaseAssignedAmountInContext(g.Key, assignedAmount, string.Empty, string.Empty);
            }
        }
        protected override void PrepareOperationLog(DataExecutionContext<UserOperationLogCollection> context)
        {
            base.PrepareOperationLog(context);
            //context.Logs.ForEach(log => log.ResourceID = this.Model);
        }
    }
}