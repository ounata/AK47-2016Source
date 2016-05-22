using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCS.Library.Data;
using MCS.Library.Data.Adapters;
using MCS.Library.Data.DataObjects;
using PPTS.Data.Orders.Entities;
using MCS.Library.Data.Builder;
using MCS.Library.Data.Mapping;

namespace PPTS.Data.Orders.Adapters
{
    /// <summary>
    /// 购物车 相关的Adapter的基类
    /// </summary>
    public class ShoppingCartAdapter : UpdatableAndLoadableAdapterBase<ShoppingCart, ShoppingCartCollection>
    {
        public static readonly ShoppingCartAdapter Instance = new ShoppingCartAdapter();
        protected override string GetConnectionName()
        {
            return ConnectionDefine.PPTSOrderConnectionName;
        }

        public ShoppingCartCollection Load(params string [] cartIds)
        {
            return LoadByInBuilder(new InLoadingCondition(w => w.AppendItem(cartIds), "CartID"));
        }

        protected override void BeforeInnerUpdateInContext(ShoppingCart data, SqlContextItem sqlContext, Dictionary<string, object> context)
        {
            base.BeforeInnerUpdateInContext(data, sqlContext, context);
            var wcondition = new WhereLoadingCondition(w => w.AppendItem("CustomerID", data.CustomerID).AppendItem("ProductID", data.ProductID).AppendItem("OrderType", data.OrderType));
            var builder = new WhereSqlClauseBuilder(LogicOperatorDefine.And);
            wcondition.BuilderAction(builder);
            var sql = string.Format("if exists( select 1 from {0} where {1} ) begin select -1; end", this.GetTableName(), builder.ToSqlString(TSqlBuilder.Instance));

            sqlContext.AppendSqlInContext(TSqlBuilder.Instance, TSqlBuilder.Instance.DBStatementSeperator);
            sqlContext.AppendSqlInContext(TSqlBuilder.Instance, sql);
            sqlContext.AppendSqlInContext(TSqlBuilder.Instance, TSqlBuilder.Instance.DBStatementSeperator);
        }

        protected override void AfterInnerUpdateInContext(ShoppingCart data, SqlContextItem sqlContext, Dictionary<string, object> context)
        {
            sqlContext.AppendSqlInContext(TSqlBuilder.Instance, TSqlBuilder.Instance.DBStatementSeperator);
            sqlContext.AppendSqlInContext(TSqlBuilder.Instance, "select 1;");
            sqlContext.AppendSqlInContext(TSqlBuilder.Instance, TSqlBuilder.Instance.DBStatementSeperator);
            base.AfterInnerUpdateInContext(data, sqlContext, context);
        }

    }
}
