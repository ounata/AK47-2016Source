using MCS.Library.SOA.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Customers.Adapters
{
    /// <summary>
    /// 客户子系统日志操作类
    /// </summary>
    public class CustomerUserOperationLogAdapter : UserOperationLogAdapter
    {
        public new static CustomerUserOperationLogAdapter Instance = new CustomerUserOperationLogAdapter();

        /// <summary>
        /// 
        /// </summary>
        private CustomerUserOperationLogAdapter()
        {
        }

        protected override string GetConnectionName()
        {
            return ConnectionDefine.PPTSCustomerConnectionName;
        }
    }
}
