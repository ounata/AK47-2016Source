using MCS.Library.Data.Adapters;
using MCS.Library.Data.Builder;
using PPTS.Data.Orders.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCS.Library.Data;
using PPTS.Data.Orders;

namespace PPTS.Data.Orders.Adapters
{
    /// <summary>
    /// 退订 相关的Adapter的基类
    /// </summary>
    public class DebookOrderAdapter : DebookOrderAdapterBase<DebookOrder, DebookOrderCollection>
    {
        public static readonly DebookOrderAdapter Instance = new DebookOrderAdapter();
        protected override string GetConnectionName()
        {
            return ConnectionDefine.PPTSOrderConnectionName;
        }

        public DebookOrder Load(string debookID)
        {
            return Load(b => b.AppendItem("DebookID", debookID)).FirstOrDefault();
        }


        public void ExistsPendingApprovalInContext(string customerId)
        {

            var whereSqlBuilder = new WhereSqlClauseBuilder().AppendItem("CustomerID", customerId).ToSqlString(TSqlBuilder.Instance);
            var sql = string.Format(@"if exists (
select * from {0} ROWLOCK where {1} and DebookStatus='1' 
)
begin 
RAISERROR ('有未完成的退费操作不允许订购！', 16, 1) WITH NOWAIT;
end", this.GetTableName(), whereSqlBuilder);

            var sqlContext = GetSqlContext();
            sqlContext.AppendSqlInContext(TSqlBuilder.Instance, TSqlBuilder.Instance.DBStatementSeperator);
            sqlContext.AppendSqlInContext(TSqlBuilder.Instance, sql);
            sqlContext.AppendSqlInContext(TSqlBuilder.Instance, TSqlBuilder.Instance.DBStatementSeperator);
        }

        protected override void BeforeInnerUpdateInContext(DebookOrder data, SqlContextItem sqlContext, Dictionary<string, object> context)
        {
            base.BeforeInnerUpdateInContext(data, sqlContext, context);

            if (string.IsNullOrWhiteSpace(data.DebookNo))
            {
                data.DebookNo = Helper.GetDebookOrderCode("DEB");
            }

            //OrdersAdapter.Instance.ExistsPendingApprovalInContext(data.CustomerID);
            
        }

        

    }
}
