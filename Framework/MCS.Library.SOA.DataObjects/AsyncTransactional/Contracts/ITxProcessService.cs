using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.SOA.DataObjects.AsyncTransactional.Contracts
{
    /// <summary>
    /// 事务流程的服务方法
    /// </summary>
    [ServiceContract]
    public interface ITxProcessService
    {
        /// <summary>
        /// 启动流程
        /// </summary>
        /// <param name="srcConnectionName"></param>
        /// <param name="processID"></param>
        [OperationContract]
        void StartProcess(string srcConnectionName, string processID);

        /// <summary>
        /// 同步流程状态，执行流程当前活动
        /// </summary>
        /// <param name="srcConnectionName"></param>
        /// <param name="processID"></param>
        [OperationContract]
        void SyncAndExecuteActivity(string srcConnectionName, string processID);

        /// <summary>
        /// 同步流程状态，回滚流程当前活动
        /// </summary>
        /// <param name="srcConnectionName"></param>
        /// <param name="processID"></param>
        [OperationContract]
        void SyncAndRollbackActivity(string srcConnectionName, string processID);
    }
}
