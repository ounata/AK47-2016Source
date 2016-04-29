using PPTS.Data.Common.Executors;
using MCS.Library.Data.Executors;
using MCS.Library.SOA.DataObjects;
using PPTS.Data.Orders.Entities;
using System.Collections.Generic;
using MCS.Library.Data;
using PPTS.Data.Orders.Adapters;
using MCS.Library.Data.Builder;

namespace PPTS.Data.Orders.Executors
{
    public class PPTSEditAssignExecutor : PPTSExecutorBase
    {
        public IList<Assign> AssignCollection { get; set; }

        /// <summary>
        /// 操作类型
        /// </summary>
        /// <param name="opType"></param>
        public PPTSEditAssignExecutor(string opType) :
            base(opType)
        {
        }

        protected override object DoOperation(DataExecutionContext<UserOperationLogCollection> context)
        {
            //if (OperationType == "ResetAssign") //调整课表
            //{
            //    foreach (var m in this.AssignCollection)
            //        AssignsAdapter.Instance.UpdateAssignTime(m);

            //    using (DbContext db = AssignsAdapter.Instance.GetDbContext())
            //    {
            //        db.ExecuteNonQuerySqlInContext();
            //    }
            //}
            //if (OperationType == "CancelAssign")
            //{

            //}
            return true;
        }
      
    }
}
