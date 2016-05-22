using MCS.Library.Core;
using MCS.Library.Data;
using MCS.Library.SOA.DataObjects;
using MCS.Library.SOA.DataObjects.AsyncTransactional;
using MCS.Library.SOA.DataObjects.AsyncTransactional.Contracts;
using MCS.Library.WcfExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using System.Transactions;

namespace WfPlatformServices.Services
{
    /// <summary>
    /// 这里面的都是从目标数据库复制流程到全局数据库
    /// </summary>
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class TxProcessService : ITxProcessService
    {
        [WfJsonFormatter]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public void StartProcess(string srcConnectionName, string processID)
        {
            processID.CheckStringIsNullOrEmpty("processID");

            TxProcessAdapter srcAdapter = TxProcessAdapter.GetInstance(srcConnectionName);

            TxProcess process = srcAdapter.Load(processID);

            //如果为空则忽略。说明数据不完整。该流程无效
            if (process != null)
            {
                srcAdapter.CopyTo(process, TxProcessAdapter.DefaultInstance.ConnectionName);

                process.MoveToNextActivity();

                using (TransactionScope scope = TransactionScopeFactory.Create())
                {
                    if (process.Status == TxProcessStatus.Running)
                        InvokeServiceTaskAdapter.Instance.Push(process.ToExecuteCurrentActivityTask());

                    TxProcessAdapter.DefaultInstance.Update(process);

                    scope.Complete();
                }
            }
        }

        [WfJsonFormatter]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public void SyncAndExecuteActivity(string srcConnectionName, string processID)
        {
            processID.CheckStringIsNullOrEmpty("processID");

            TxProcessAdapter srcAdapter = TxProcessAdapter.GetInstance(srcConnectionName);

            TxProcess process = srcAdapter.Load(processID);

            if (process != null)
            {
                using (TransactionScope scope = TransactionScopeFactory.Create())
                {
                    //如果为空则忽略。说明数据不完整。该流程无效
                    srcAdapter.CopyTo(process, TxProcessAdapter.DefaultInstance.ConnectionName);

                    //发送执行动作的任务
                    if (process.Status == TxProcessStatus.Running)
                        InvokeServiceTaskAdapter.Instance.Push(process.ToExecuteCurrentActivityTask());

                    TxProcessAdapter.DefaultInstance.Update(process);

                    scope.Complete();
                }
            }
        }

        [WfJsonFormatter]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public void SyncAndRollbackActivity(string srcConnectionName, string processID)
        {
            processID.CheckStringIsNullOrEmpty("processID");

            TxProcessAdapter srcAdapter = TxProcessAdapter.GetInstance(srcConnectionName);

            TxProcess process = srcAdapter.Load(processID);

            if (process != null)
            {
                using (TransactionScope scope = TransactionScopeFactory.Create())
                {
                    //如果为空则忽略。说明数据不完整。该流程无效
                    srcAdapter.CopyTo(process, TxProcessAdapter.DefaultInstance.ConnectionName);

                    //发送执行动作的任务
                    switch (process.Status)
                    {
                        case TxProcessStatus.RollingBack:
                            InvokeServiceTaskAdapter.Instance.Push(process.ToExecuteRollbackCurrentActivityTask());
                            break;
                        case TxProcessStatus.RolledBack:
                            InvokeServiceTaskAdapter.Instance.Push(process.ToExecuteRollbackProcessTask());
                            break;
                    }

                    TxProcessAdapter.DefaultInstance.Update(process);

                    scope.Complete();
                }
            }
        }
    }
}
