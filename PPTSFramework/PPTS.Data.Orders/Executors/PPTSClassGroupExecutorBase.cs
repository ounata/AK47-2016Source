using MCS.Library.Data.Executors;
using MCS.Library.SOA.DataObjects;
using PPTS.Data.Common.Executors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Orders.Executors
{
    public abstract class PPTSClassGroupExecutorBase: PPTSExecutorBase
    {
        /// <summary>
        /// 操作类型
        /// </summary>
        /// <param name="opType"></param>
        public PPTSClassGroupExecutorBase(string opType) :
            base(opType)
        {
        }


        protected override void PersistOperationLogInContext(DataExecutionContext<UserOperationLogCollection> context)
        {
            
        }
    }
}
