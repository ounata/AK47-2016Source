using MCS.Library.Core;
using MCS.Library.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace MCS.Library.SOA.DataObjects.AsyncTransactional
{
    public class TxProcessExecutor
    {
        private string _ConnectionName = string.Empty;
        private TxProcess _Process = null;

        private TxProcessExecutor(string processID, string connectionName)
        {
            this._Process = TxProcessAdapter.DefaultInstance.Load(processID);
            this._Process.NullCheck<ArgumentNullException>("不能找到ID为{0}的交易流程", processID);

            this._ConnectionName = connectionName;
        }

        public TxProcess Process
        {
            get
            {
                return this._Process;
            }
        }
 
        /// <summary>
        /// 得到执行器的实例
        /// </summary>
        /// <param name="processID"></param>
        /// <param name="connectionName"></param>
        /// <returns></returns>
        public static TxProcessExecutor GetExecutor(string processID, string connectionName = "")
        {
            return new TxProcessExecutor(processID, connectionName);
        }

        public TxProcessExecutor PrepareData(Action<TxProcess> action)
        {
            if (action != null)
                action(this._Process);

            return this;
        }

        public TxProcessExecutor ExecuteMoveTo(Action<TxProcess> action)
        {
            using (TransactionScope scope = TransactionScopeFactory.Create())
            {
                if (action != null)
                    action(this._Process);

                TxProcess process = this._Process;

                string connectionName = this.GetConnectionName();

                //本地流程变更
                process.MoveToNextActivity();
                TxProcessAdapter.GetInstance(connectionName).Update(process);

                //发送流转流程的任务
                InvokeServiceTaskAdapter.Instance.Push(process.ToSyncAndExecuteActivityTask());

                //发送流转操作
                scope.Complete();
            }

            return this;
        }

        public TxProcessExecutor ExecuteRollback(Action<TxProcess> action)
        {
            using (TransactionScope scope = TransactionScopeFactory.Create())
            {
                if (action != null)
                    action(this._Process);

                TxProcess process = this._Process;

                string connectionName = this.GetConnectionName();

                process.RollbackToPreviousActivity();
                TxProcessAdapter.GetInstance(connectionName).Update(process);

                //发送流转流程的任务
                InvokeServiceTaskAdapter.Instance.Push(process.ToSyncAndRollbackActivityTask());

                //发送流转操作
                scope.Complete();
            }

            return this;
        }

        private string GetConnectionName()
        {
            string result = this._ConnectionName;

            if (result.IsNullOrEmpty() && this._Process.CurrentActivity != null)
                result = this._Process.CurrentActivity.ConnectionName;

            return result;
        }
    }
}
