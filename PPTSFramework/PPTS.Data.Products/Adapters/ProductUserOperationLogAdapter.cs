using MCS.Library.SOA.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Products.Adapters
{
    /// <summary>
    /// 客户子系统日志操作类
    /// </summary>
    public class ProductUserOperationLogAdapter : UserOperationLogAdapter
    {
        public new static ProductUserOperationLogAdapter Instance = new ProductUserOperationLogAdapter();

        /// <summary>
        /// 
        /// </summary>
        private ProductUserOperationLogAdapter()
        {
        }

        protected override string GetConnectionName()
        {
            return ConnectionDefine.PPTSProductConnectionName;
        }
    }
}
