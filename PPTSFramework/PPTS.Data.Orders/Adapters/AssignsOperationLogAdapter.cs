using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCS.Library.SOA.DataObjects;

namespace PPTS.Data.Orders.Adapters
{
    public class AssignsOperationLogAdapter : UserOperationLogAdapter
    {
        public new static AssignsOperationLogAdapter Instance = new AssignsOperationLogAdapter();

        /// <summary>
        /// 
        /// </summary>
        private AssignsOperationLogAdapter()
        {
        }

        protected override string GetConnectionName()
        {
            return ConnectionDefine.PPTSOrderConnectionName;
        }
    }
}
