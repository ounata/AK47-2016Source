using MCS.Library.Core;
using MCS.Library.Data.Builder;
using MCS.Library.Data.Mapping;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Data.Adapters
{
    public abstract partial class VersionedObjectAdapterBase<T, TCollection>
    {
        #region Public
        /// <summary>
        /// 
        /// </summary>
        /// <param name="inCondition"></param>
        /// <param name="action"></param>
        /// <param name="timePoint"></param>
        /// <param name="tableName"></param>
        /// <param name="mappings"></param>
        public void LoadByInBuilderInContext(InLoadingCondition inCondition, Action<TCollection> action, DateTime timePoint, string tableName = null, ORMappingItemCollection mappings = null)
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

                this.RegisterLoadByBuilderInContext(condition, orderByBuilder, action, timePoint, tableName, mappings);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectiveCondition"></param>
        /// <param name="action"></param>
        /// <param name="timePoint">时间点</param>
        /// <param name="tableName"></param>
        /// <param name="mappings"></param>
        public void LoadByBuilderInContext(ConnectiveLoadingCondition connectiveCondition, Action<TCollection> action, DateTime timePoint, string tableName = null, ORMappingItemCollection mappings = null)
        {
            connectiveCondition.NullCheck("connectiveCondition");
            connectiveCondition.ConnectiveBuilder.NullCheck("ConnectiveBuilder");

            this.RegisterLoadByBuilderInContext(
                connectiveCondition.ConnectiveBuilder.ToSqlString(TSqlBuilder.Instance),
                connectiveCondition.OrderByBuilder,
                action,
                timePoint,
                tableName,
                mappings);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="loadingCondition"></param>
        /// <param name="action"></param>
        /// <param name="timePoint">时间点</param>
        /// <param name="tableName"></param>
        /// <param name="mappings"></param>
        public void LoadInContext(WhereLoadingCondition loadingCondition, Action<TCollection> action, DateTime timePoint, string tableName = null, ORMappingItemCollection mappings = null)
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

            this.RegisterLoadByBuilderInContext(builder.ToSqlString(TSqlBuilder.Instance), orderByBuilder, action, timePoint, tableName, mappings);
        }
        #endregion Public

        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="orderByBuilder"></param>
        /// <param name="action"></param>
        /// <param name="timePoint"></param>
        /// <param name="tableName"></param>
        /// <param name="mappings"></param>
        protected void RegisterLoadByBuilderInContext(string condition, OrderBySqlClauseBuilder orderByBuilder, Action<TCollection> action, DateTime timePoint, string tableName, ORMappingItemCollection mappings)
        {
            VersionedQueryInContextBuilder<T, TCollection>.Instance.RegisterLoadByBuilderInContext(
                this.GetSqlContext(),
                condition,
                orderByBuilder,
                (collection) => this.AfterLoad(collection),
                (collection) => action(collection),
                (row) => this.CreateNewData(row),
                timePoint,
                tableName,
                mappings);
        }

        /// <summary>
        /// 在上下文中注册查询返回的结果
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="mapping"></param>
        /// <param name="sql"></param>
        /// <param name="action"></param>
        protected void RegisterQueryData(string tableName, ORMappingItemCollection mapping, string sql, Action<TCollection> action)
        {
            VersionedQueryInContextBuilder<T, TCollection>.Instance.RegisterQueryData(
                this.GetSqlContext(),
                tableName,
                mapping,
                sql,
                action,
                (row) => this.CreateNewData(row));
        }
    }
}
