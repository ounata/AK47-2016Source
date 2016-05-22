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
    [DataExecutorDescription("删除课表")]
    public class AssignDeleteExecutor : PPTSEditAssignExecutorBase<AssignCollection>
    {
        public AssignDeleteExecutor(AssignCollection model)
            : base(model, null)
        {
            model.NullCheck("model");
        }

        public string Msg { get; set; }

        protected override void PrepareData(DataExecutionContext<UserOperationLogCollection> context)
        {
            ///只能删除已上状态课时
            ///过结账日后，只能删除当月课表，不能删除上月课表
            ///删除后，已上课时数减去删除的课时数量，剩余课时数加上删除的课时数量
            ///设置排课状态为 无效   设置确认状态为 已删除 
            base.PrepareData(context);

            if (this.Model.Count == 0)
                return;
            this.Model[0].FillModifier();

            //只能删除已上状态课时
            IList<string> aIDs = new List<string>();

            //判断结账日接口调用，过了为true,没有过false
            bool closingDate = ValidateClosingDate(DateTime.Now.Month);
            foreach (var v in this.Model)
            {
                if ((v.AssignStatus != Data.Orders.AssignStatusDefine.Finished || v.StartTime.Month < DateTime.Now.Month && closingDate))
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
                //已上课时数减去删除的课时数量，剩余课时数加上删除的课时数量
                at.ConfirmedAmount -= assignedAmount;
                at.Amount += assignedAmount;
                aIDs = assigns.Select(p => p.AssignID).ToList();
                AssignsAdapter.Instance.UpdateAssignStatusInContext(aIDs, this.Model[0].ModifierID, this.Model[0].ModifierName, Data.Orders.AssignStatusDefine.Invalid);
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