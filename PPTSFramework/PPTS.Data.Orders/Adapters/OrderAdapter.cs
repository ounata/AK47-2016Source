using MCS.Library.Caching;
using MCS.Library.Core;
using MCS.Library.Data.Adapters;
using MCS.Library.Data.Builder;
using MCS.Library.Data.Mapping;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PPTS.Data.Orders.Entities;
using PPTS.Data.Orders;
using MCS.Library.Data;

namespace PPTS.Data.Orders.Adapters
{
    public class OrdersAdapter : OrderAdapterBase<Order,OrderCollection>
    {
        public static readonly OrdersAdapter Instance = new OrdersAdapter();

        private OrdersAdapter()
        {
        }

        /// <summary>
        /// 插入操作
        /// </summary>
        /// <param name="orders"></param>
        /*
		public void Insert(Order orders)
		{
			this.InnerInsert(order, new Dictionary<string, object>());
		}
		*/

        /// <summary>
        /// 加载操作
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        public Order Load(string orderid)
        {
            return this.Load(builder => builder.AppendItem("OrderID", orderid)).SingleOrDefault();
        }

        public OrderCollection LoadCollection(IList<string> orderID)
        {
            return this.LoadByInBuilder(new InLoadingCondition(builder => builder.AppendItem(orderID.ToArray<string>()), dataField: "OrderID"));

        }


        protected override void BeforeInnerUpdate(Order data, Dictionary<string, object> context)
        {
            if (data.OrderNo.IsNullOrWhiteSpace()) { data.OrderNo = Helper.GetOrderCode("NOD"); }
        }

        protected override void BeforeInnerUpdateInContext(Order data, SqlContextItem sqlContext, Dictionary<string, object> context)
        {
            if (data.OrderNo.IsNullOrWhiteSpace()) { data.OrderNo = Helper.GetOrderCode("NOD"); }
        }

        public void ExistsPendingApprovalInContext(string customerId) {
            
            var whereCustomerId = new WhereSqlClauseBuilder().AppendItem("CustomerID", customerId).ToSqlString(TSqlBuilder.Instance);
            var sql = string.Format(@"if exists (
select * from {0} ROWLOCK where {1} and OrderStatus='1' 
)
begin 
select -1;return;
end", this.GetTableName(), whereCustomerId);
            
            var sqlContext = GetSqlContext();
            sqlContext.AppendSqlInContext(TSqlBuilder.Instance, TSqlBuilder.Instance.DBStatementSeperator);
            sqlContext.AppendSqlInContext(TSqlBuilder.Instance, sql);
            sqlContext.AppendSqlInContext(TSqlBuilder.Instance, TSqlBuilder.Instance.DBStatementSeperator);
        }

        public void ExecSuccessInContext()
        {
            GetSqlContext().AppendSqlWithSperatorInContext(TSqlBuilder.Instance, "select 1");
        }
        
        public void Update(string orderId,Dictionary<string,object> param)
        {
            var updateBuilder = new UpdateSqlClauseBuilder();
            var whereBuilder = new WhereSqlClauseBuilder();

            whereBuilder.AppendItem("orderId", orderId);
            param.ForEach(kv => { updateBuilder.AppendItem(kv.Key, kv.Value); });
            updateBuilder.AppendItem("ModifyTime", "GETUTCDATE()", "=", true);

            string sql = string.Format("update {0} set {1} where {2}",
                GetTableName(),
                updateBuilder.ToSqlString(TSqlBuilder.Instance),
                whereBuilder.ToSqlString(TSqlBuilder.Instance));
            DbHelper.RunSql(sql, GetConnectionName());
        }
    }
}