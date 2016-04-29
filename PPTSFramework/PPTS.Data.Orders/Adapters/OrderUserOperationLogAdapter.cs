using MCS.Library.SOA.DataObjects;

namespace PPTS.Data.Orders.Adapters
{
    /// <summary>
    /// 客户子系统日志操作类
    /// </summary>
    public class OrderUserOperationLogAdapter : UserOperationLogAdapter
    {
        public new static OrderUserOperationLogAdapter Instance = new OrderUserOperationLogAdapter();

        /// <summary>
        /// 
        /// </summary>
        private OrderUserOperationLogAdapter()
        {
        }

        protected override string GetConnectionName()
        {
            return ConnectionDefine.PPTSOrderConnectionName;
        }
    }
}
