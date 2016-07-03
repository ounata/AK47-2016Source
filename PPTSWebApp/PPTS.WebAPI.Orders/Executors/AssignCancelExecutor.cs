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
using PPTS.Data.Common.Security;
using MCS.Library.Data;

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

        public List<string> CustomerIDTask { get; private set; }

        protected override void PrepareData(DataExecutionContext<UserOperationLogCollection> context)
        {
            ///需要执行的判断
            ///1. 排定 异常   状态的排课 可以取消
            ///2. 取消后，需要将资产表中的已排课时数量减去取消掉的课时量
            base.PrepareData(context);

            if (this.Model.Count == 0)
                return;
            this.Model[0].FillModifier();

            IList<string> assignID = this.Model.Select(p => p.AssignID).ToList();
            AssignCollection ac = AssignsAdapter.Instance.LoadCollection(assignID);
            if (ac == null)
                return;
            IEnumerable<Assign> retValue = ac.Where(p=>p.AssignStatus == Data.Orders.AssignStatusDefine.Assigned 
            || p.AssignStatus == Data.Orders.AssignStatusDefine.Exception);

            if (retValue == null || retValue.Count() == 0)
                return;

            this.CustomerIDTask = retValue.Select(p => p.CustomerID).Distinct().ToList();

            ///排课可能来自不同的资产，所以要分组处理
            IEnumerable<IGrouping<string, Assign>> result = retValue.GroupBy(p => p.AssetID);
            foreach (IGrouping<string, Assign> g in result)
            {
                IEnumerable<Assign> assigns = g.ToList<Assign>();
                decimal assignedAmount = assigns.Sum(p => p.Amount);
                Asset at = GenericAssetAdapter<Asset, AssetCollection>.Instance.Load(g.Key);
                if (at == null)
                    continue;

                at.AssignedAmount -= assignedAmount;
                assignID = assigns.Select(p => p.AssignID).ToList();

                AssignsAdapter.Instance.UpdateAssignStatusInContext(assignID, this.Model[0].ModifierID, this.Model[0].ModifierName, Data.Orders.AssignStatusDefine.Invalid);
                GenericAssetAdapter<Asset, AssetCollection>.Instance.UpdateInContext(at);
            }
        }

        protected override void ExecuteNonQuerySqlInContext(DbContext dbContext)
        {
            dbContext.ExecuteTimePointSqlInContext();
        }

        protected override void PrepareOperationLog(DataExecutionContext<UserOperationLogCollection> context)
        {
            base.PrepareOperationLog(context);
            //context.Logs.ForEach(log => log.ResourceID = this.Model);
        }
    }
}