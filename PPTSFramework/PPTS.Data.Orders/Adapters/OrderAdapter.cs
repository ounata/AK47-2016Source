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

        public string TableName { get { return GetTableName(); } }

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

        public void LoadInContext(string orderId, string[] campusIds,Action<OrderCollection> action)
        {
            var where = new WhereSqlClauseBuilder();
            where.AppendItem("OrderID", orderId);
            if (campusIds != null)
            {
                where.AppendItem("CampusID", campusIds, "in", true);
            }
            LoadByBuilderInContext(new ConnectiveLoadingCondition(where) , action);
        }

        public string IsExistsCampusIDSQL(string orderId, string[] campusIds)
        {
            var where = new WhereSqlClauseBuilder();
            where.AppendItem("OrderID", orderId);
            if (campusIds != null)
            {
                where.AppendItem("CampusID", campusIds, "in", true);
            }
            return string.Format(" ( select 1 from {0} where {1} )",GetQueryTableName(),where.ToSqlString(TSqlBuilder.Instance));
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
RAISERROR ('有未完成订购操作的，不能退订！', 16, 1) WITH NOWAIT;
end", this.GetTableName(), whereCustomerId);
            
            var sqlContext = GetSqlContext();
            sqlContext.AppendSqlInContext(TSqlBuilder.Instance, TSqlBuilder.Instance.DBStatementSeperator);
            sqlContext.AppendSqlInContext(TSqlBuilder.Instance, sql);
            sqlContext.AppendSqlInContext(TSqlBuilder.Instance, TSqlBuilder.Instance.DBStatementSeperator);
        }

        //public void ExecSuccessInContext()
        //{
        //    GetSqlContext().AppendSqlWithSperatorInContext(TSqlBuilder.Instance, "select 1");
        //}
        
        private void UpdateInContext(string orderId,Dictionary<string,object> param)
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

            GetSqlContext().AppendSqlWithSperatorInContext(TSqlBuilder.Instance, sql);
        }

        public void ModifyProcessStatusInContext(string orderId,int processStatus)
        {
            UpdateInContext(orderId, new Dictionary<string, object>() { { "ProcessStatus", processStatus } });
        }

        public void ModifyStatusInContext(string orderId, int processStatus,int orderStatus,string modifyUserId=null,string modifyUserName=null)
        {
            var updateParam = new Dictionary<string, object>() { { "ProcessStatus", processStatus }, { "OrderStatus", orderStatus } };
            if (!string.IsNullOrWhiteSpace(modifyUserId)) { updateParam.Add("ModifierID", modifyUserId); }
            if (!string.IsNullOrWhiteSpace(modifyUserName)) { updateParam.Add("ModifierName", modifyUserName); }

            UpdateInContext(orderId, updateParam);
        }

        public void ModifyChargeApply(Order model,string[] campusIds)
        {
            var where = new WhereSqlClauseBuilder();
            where.AppendItem("OrderID", model.OrderID);
            if (campusIds != null)
            {
                where.AppendItem("CampusID", campusIds, "in", true);
            }
            var sql = string.Format("if not exists(select 1 from {0} where {1}) begin \n return; \n",GetQueryTableName(), where.ToSqlString(TSqlBuilder.Instance));

            UpdateInContext(model.OrderID, new Dictionary<string, object>() {
                { "ChargeApplyID", model.ChargeApplyID },
                { "ModifierID", model.ModifierID },
                { "ModifierName", model.ModifierName },
            });

            GetDbContext().DoAction(db => db.ExecuteNonQuerySqlInContext());
        }

    }
}