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


        public void ExistsPendingApprovalInContext(string customerId)
        {

            var whereCustomerId = new WhereSqlClauseBuilder().AppendItem("CustomerID", customerId).ToSqlString(TSqlBuilder.Instance);
            var sql = string.Format(@"if exists (
select * from {0} ROWLOCK where {1} and DebookStatus='1' 
)
begin 
select -1;return;
end", this.GetTableName(), whereCustomerId);

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
