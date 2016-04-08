using MCS.Library.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace MCS.Library.Data.Executors
{
    /// <summary>
    /// 数据操作执行器的基类。
    /// 数据操作一般指的是一组DB操作，在一个事务中。有连续的转换、校验和入库逻辑
    /// </summary>
    public abstract class DataExecutorBase<TLogs> where TLogs : new()
    {
        private bool _AutoStartTransaction = true;

        /// <summary>
        /// 构造方法
        /// </summary>
        protected DataExecutorBase()
        {
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="opType"></param>
        protected DataExecutorBase(string opType)
        {
            this.OperationType = opType;
        }

        /// <summary>
        /// 操作类型
        /// </summary>
        public string OperationType
        {
            get;
            private set;
        }

        /// <summary>
        /// 是否自动启动事务
        /// </summary>
        public bool AutoStartTransaction
        {
            get
            {
                return this._AutoStartTransaction;
            }
            set
            {
                this._AutoStartTransaction = value;
            }
        }

        /// <summary>
        /// 执行操作
        /// </summary>
        /// <returns></returns>
        public object Execute()
        {
            object result = null;

            ExecutionWrapper(this.GetOperationDescription(),
                    () => result = InternalExecute());

            return result;
        }

        /// <summary>
        /// 执行在事务内具体的数据操作，需要重载
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        protected abstract object DoOperation(DataExecutionContext<TLogs> context);

        /// <summary>
        /// 准备数据，包括校验数据。这个操作在事务之外
        /// </summary>
        /// <param name="context"></param>
        protected virtual void PrepareData(DataExecutionContext<TLogs> context)
        {
        }

        /// <summary>
        /// 准备操作日志
        /// </summary>
        /// <param name="context"></param>
        protected virtual void PrepareOperationLog(DataExecutionContext<TLogs> context)
        {
        }

        /// <summary>
        /// 得到操作的名称
        /// </summary>
        protected virtual string GetOperationDescription()
        {
            string result = string.Empty;

            if (this.OperationType.IsNullOrEmpty())
            {
                DataExecutorDescriptionAttribute attribute = AttributeHelper.GetCustomAttribute<DataExecutorDescriptionAttribute>(this.GetType());

                if (attribute != null)
                    result = attribute.Description;
            }

            return result;
        }

        /// <summary>
        /// 需要重载，保存日志
        /// </summary>
        /// <param name="context"></param>
        protected virtual void PersistOperationLog(DataExecutionContext<TLogs> context)
        {
        }

        /// <summary>
        /// 需要重载，在上下文中保存日志
        /// </summary>
        /// <param name="context"></param>
        protected virtual void PersistOperationLogInContext(DataExecutionContext<TLogs> context)
        {
        }

        private object InternalExecute()
        {
            DataExecutionContext<TLogs> context = new DataExecutionContext<TLogs>(this.OperationType, this);

            ExecutionWrapper("PrepareData", () => PrepareData(context));
            ExecutionWrapper("PrepareOperationLog", () => PrepareOperationLog(context));
            ExecutionWrapper("PersistOperationLogInContext", () => PersistOperationLogInContext(context));

            object result = null;

            if (this.AutoStartTransaction)
            {
                using (TransactionScope scope = TransactionScopeFactory.Create())
                {
                    ExecutionWrapper("DoOperation", () => result = DoOperation(context));
                    ExecutionWrapper("PersistOperationLog", () => PersistOperationLog(context));

                    scope.Complete();
                }
            }
            else
            {
                ExecutionWrapper("DoOperation", () => result = DoOperation(context));
                ExecutionWrapper("PersistOperationLog", () => PersistOperationLog(context));
            }

            return result;
        }

        private static void ExecutionWrapper(string operationName, Action action)
        {
            operationName.CheckStringIsNullOrEmpty("operationName");
            action.NullCheck("action");

            DataExecutorLogContextInfo.Writer.WriteLine("\t\t{0}开始：{1:yyyy-MM-dd HH:mm:ss.fff}",
                    operationName, DateTime.Now);

            Stopwatch sw = new Stopwatch();

            sw.Start();
            try
            {
                action();
            }
            finally
            {
                sw.Stop();
                DataExecutorLogContextInfo.Writer.WriteLine("\t\t{0}结束：{1:yyyy-MM-dd HH:mm:ss.fff}；经过时间：{2:#,##0}毫秒",
                    operationName, DateTime.Now, sw.ElapsedMilliseconds);
            }
        }
    }
}
