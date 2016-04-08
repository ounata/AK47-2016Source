using MCS.Library.Data.Executors;
using MCS.Library.SOA.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Common.Executors
{
    /// <summary>
    /// PPTS的操作执行类的基类
    /// </summary>
    public abstract class PPTSExecutorBase : DataExecutorBase<UserOperationLogCollection>
    {
        /// <summary>
        /// 操作类型
        /// </summary>
        /// <param name="opType"></param>
        public PPTSExecutorBase(string opType) :
            base(opType)
        {
        }
    }
}
