using MCS.Library.Core;
using MCS.Library.Data.Builder;
using MCS.Library.Data.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Data.Adapters
{
    public abstract partial class UpdatableAndLoadableAdapterBase<T, TCollection>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="inCondition"></param>
        /// <param name="action"></param>
        /// <param name="tableName"></param>
        /// <param name="mappings"></param>
        public void LoadByInBuilderInContext(InLoadingCondition inCondition, Action<TCollection> action, string tableName = null, ORMappingItemCollection mappings = null)
        {
            inCondition.NullCheck("inCondition");
            inCondition.BuilderAction.NullCheck("BuilderAction");

            InSqlClauseBuilder inBuilder = new InSqlClauseBuilder(inCondition.DataField);

            inCondition.BuilderAction(inBuilder);

            string condition = string.Empty;

            if (inBuilder.IsEmpty == false)
            {
                ConnectiveSqlClauseCollection builder = new ConnectiveSqlClauseCollection(LogicOperatorDefine.And,
                    inBuilder, new WhereSqlClauseBuilder().AppendTenantCode(typeof(T)));

                condition = builder.ToSqlString(TSqlBuilder.Instance);

                OrderBySqlClauseBuilder orderByBuilder = null;

                if (inCondition.OrderByAction != null)
                {
                    orderByBuilder = new OrderBySqlClauseBuilder();

                    inCondition.OrderByAction(orderByBuilder);
                }

                this.RegisterLoadByBuilderInContext(condition, orderByBuilder, action, tableName, mappings);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectiveCondition"></param>
        /// <param name="action"></param>
        /// <param name="tableName"></param>
        /// <param name="mappings"></param>
        public void LoadByBuilderInContext(ConnectiveLoadingCondition connectiveCondition, Action<TCollection> action, string tableName = null, ORMappingItemCollection mappings = null)
        {
            connectiveCondition.NullCheck("connectiveCondition");
            connectiveCondition.ConnectiveBuilder.NullCheck("ConnectiveBuilder");

            this.RegisterLoadByBuilderInContext(
                connectiveCondition.ConnectiveBuilder.ToSqlString(TSqlBuilder.Instance),
                connectiveCondition.OrderByBuilder,
                action,
                tableName,
                mappings);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="loadingCondition"></param>
        /// <param name="action"></param>
        /// <param name="tableName"></param>
        /// <param name="mappings"></param>
        public void LoadInContext(WhereLoadingCondition loadingCondition, Action<TCollection> action, string tableName = null, ORMappingItemCollection mappings = null)
        {
            loadingCondition.NullCheck("loadingCondition");
            loadingCondition.BuilderAction.NullCheck("BuilderAction");

            WhereSqlClauseBuilder builder = new WhereSqlClauseBuilder();

            loadingCondition.BuilderAction(builder);

            builder.AppendTenantCode(typeof(T));

            OrderBySqlClauseBuilder orderByBuilder = null;

            if (loadingCondition.OrderByAction != null)
            {
                orderByBuilder = new OrderBySqlClauseBuilder();

                loadingCondition.OrderByAction(orderByBuilder);
            }

            this.RegisterLoadByBuilderInContext(builder.ToSqlString(TSqlBuilder.Instance), orderByBuilder, action, tableName, mappings);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="orderByBuilder"></param>
        /// <param name="mappings"></param>
        /// <param name="tableName"></param>
        /// <param name="action"></param>
        protected void RegisterLoadByBuilderInContext(string condition, OrderBySqlClauseBuilder orderByBuilder, Action<TCollection> action, string tableName, ORMappingItemCollection mappings)
        {
            if (mappings == null)
                mappings = this.GetQueryMappingInfo();

            string sql = GetLoadSqlByBuilder(condition, orderByBuilder, mappings);

            if (tableName.IsNullOrEmpty())
                tableName = this.GetTableName();

            this.RegisterQueryData(tableName, mappings, sql, (collection) =>
            {
                AfterLoad(collection);
                action(collection);
            });
        }
    }
}
