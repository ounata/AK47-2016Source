using MCS.Library.Core;
using MCS.Library.Data;
using MCS.Library.Data.Executors;
using MCS.Library.SOA.DataObjects;
using PPTS.Data.Common.Security;
using PPTS.Data.Orders.Adapters;
using PPTS.Data.Orders.Entities;
using PPTS.Data.Orders.Executors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPTS.WebAPI.Orders.Executors
{
    [DataExecutorDescription("确认课表")]
    public class AssignConfirmExecutor : PPTSEditAssignExecutorBase<AssignCollection>
    {
        public AssignConfirmExecutor(AssignCollection model)
            : base(model, null)
        {
            model.NullCheck("model");
        }

        public string Msg { get; set; }

        protected override void PrepareData(DataExecutionContext<UserOperationLogCollection> context)
        {
            ///异常状态的课时可以确认
            ///未过结账日可以确认上月以及本月异常课时，否则只能确认本月异常课时
            ///确认后，资产表中已排课时数量减对应数量，已上数量增加对应数量  剩余课时数量减对应数量
            base.PrepareData(context);

            if (this.Model.Count == 0)
                return;
            this.Model[0].FillModifier();

            //只能确认异常状态的课时
            IList<string> aIDs = new List<string>();

            //判断结账日接口调用，过了为true,没有过false
            bool closingDate = ValidateClosingDate(DateTime.Now.Month);
            foreach (var v in this.Model)
            {
                if ((v.AssignStatus != Data.Orders.AssignStatusDefine.Exception || v.StartTime.Month < DateTime.Now.Month && closingDate))
                    continue;
                aIDs.Add(v.AssignID);
            }
            if (aIDs.Count() == 0)
                return;
            AssignCollection acCollection = AssignsAdapter.Instance.LoadCollection(aIDs);
            ///排课可能来自相同的资产，所以要分组处理
            IEnumerable<IGrouping<string, Assign>> result = acCollection.GroupBy(p => p.AssetID);
            foreach (IGrouping<string, Assign> g in result)
            {
                IEnumerable<Assign> assigns = g.ToList<Assign>();
                decimal assignedAmount = assigns.Sum(p => p.Amount);
                Asset at = GenericAssetAdapter<Asset, AssetCollection>.Instance.Load(g.Key);
                if (at == null)
                    continue;
                //资产表中已排课时数量/剩余课时数量 减对应数量，已上数量增加对应数量
                at.AssignedAmount -= assignedAmount;
                at.Amount -= assignedAmount;
                at.ConfirmedAmount += assignedAmount;
                aIDs = assigns.Select(p => p.AssignID).ToList();
                AssignsAdapter.Instance.UpdateAssignStatusInContext(aIDs, this.Model[0].ModifierID, this.Model[0].ModifierName,Data.Orders.AssignStatusDefine.Finished);
                GenericAssetAdapter<Asset, AssetCollection>.Instance.UpdateInContext(at);
            }
        }

        private bool ValidateClosingDate(int month)
        {
            return false;
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