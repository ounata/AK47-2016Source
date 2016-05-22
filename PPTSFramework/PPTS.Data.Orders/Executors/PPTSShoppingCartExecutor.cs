using MCS.Library.Data.Executors;
using MCS.Library.SOA.DataObjects;
using PPTS.Data.Common.Executors;
using PPTS.Data.Orders.Adapters;
using MCS.Library.Core;

namespace PPTS.Data.Orders.Executors
{
    public class PPTSShoppingCartExecutor : PPTSExecutorBase
    {
        /// <summary>
        /// 操作类型
        /// </summary>
        /// <param name="opType"></param>
        public PPTSShoppingCartExecutor(string opType) :
            base(opType)
        {
        }

        public string CustomerId { set; get; }

        /// <summary>
        /// 1-常规订购 2-买赠订购 3-插班订购 4-补差兑换 5-不补差兑换
        /// </summary>
        public int OrderType { set; get; }

        public string[] CartIds { set; get; }

        protected override object DoOperation(DataExecutionContext<UserOperationLogCollection> context)
        {
            if (OperationType == "GetShoppingCart") {
                CustomerId.CheckStringIsNullOrEmpty("CustomerId");
                return ShoppingCartAdapter.Instance.Load(builder => builder.AppendItem("CustomerId", CustomerId).AppendItem("OrderType", OrderType));
            }
            if (OperationType == "DelShoppingCart") {
                CartIds.NullCheck("CartIds");
                CartIds.ForEach(s => { ShoppingCartAdapter.Instance.DeleteInContext(w => w.AppendItem("CartID", s)); });

                using(var dbContext = ShoppingCartAdapter.Instance.GetDbContext())
                {
                    return dbContext.ExecuteNonQuerySqlInContext()>0;
                }

            }
            return false;
        }

        protected override string GetOperationDescription()
        {
            var descrption= base.GetOperationDescription();
            if (string.IsNullOrWhiteSpace(descrption)) { return OperationType; }
            return descrption;
        }

        protected override void PersistOperationLogInContext(DataExecutionContext<UserOperationLogCollection> context)
        {
            context.Logs.ForEach(log => UserOperationLogAdapter.Instance.InsertDataInContext(log));
        }
    }
}
