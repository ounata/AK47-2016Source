using MCS.Library.Data;
using MCS.Library.SOA.DataObjects.AsyncTransactional;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace MCS.Library.SOA.DataObjects
{
    /// <summary>
    /// 交易活动调用Service的执行器
    /// </summary>
    public class TxActivityActionServiceTaskExecutor : InvokeServiceTaskExecutor
    {
        protected override SysAccomplishedTask OnError(SysTask task, Exception ex)
        {
            DoCompensationAction(task);

            return base.OnError(task, ex);
        }

        /// <summary>
        /// 执行活动交易补偿操作
        /// </summary>
        /// <param name="task"></param>
        private static void DoCompensationAction(SysTask task)
        {
            TxProcess process = TxProcessAdapter.DefaultInstance.Load(task.ResourceID);

            if (process != null)
            {
                process.RollbackToPreviousActivity();

                using (TransactionScope scope = TransactionScopeFactory.Create())
                {
                    TxProcessAdapter.DefaultInstance.Update(process);
                    InvokeServiceTaskAdapter.Instance.Push(process.ToExecuteRollbackCurrentActivityTask());

                    scope.Complete();
                }
            }
        }
    }
}
