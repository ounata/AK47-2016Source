using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Data.Executors
{
    /// <summary>
    /// 数据操作执行器的上下文
    /// </summary>
    public class DataExecutionContext<TLogs> where TLogs : new()
    {
        private string _OperationType = string.Empty;
        private DataExecutorBase<TLogs> _Executor = null;
        private Dictionary<string, object> _Parameters = new Dictionary<string, object>();

        private TLogs _Logs = default(TLogs);

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="opType"></param>
        /// <param name="executor"></param>
        public DataExecutionContext(string opType, DataExecutorBase<TLogs> executor)
        {
            this._OperationType = opType;
            this._Executor = executor;
        }

        /// <summary>
        /// 获取执行器
        /// </summary>
        public DataExecutorBase<TLogs> Executor
        {
            get
            {
                return this._Executor;
            }
        }

        /// <summary>
        /// 获取表示操作类型描述。
        /// </summary>
        public string OperationType
        {
            get
            {
                return this._OperationType;
            }
        }

        /// <summary>
        /// 在操作中需要传递的参数
        /// </summary>
        public Dictionary<string, object> Parameters
        {
            get
            {
                return this._Parameters;
            }
        }

        /// <summary>
        /// 获取操作日志的集合
        /// </summary>
        public TLogs Logs
        {
            get
            {
                if (this._Logs == null)
                    this._Logs = new TLogs();

                return this._Logs;
            }
        }
    }
}
