using MCS.Library.Core;
using MCS.Library.Data.Mapping;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Data.Builder
{
    /// <summary>
    /// 带版本信息的上下文查询的构造器的基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TCollection"></typeparam>
    public class VersionedQueryInContextBuilder<T, TCollection> : QueryInContextBuilderBase<T, TCollection> where TCollection : IList<T>, new()
    {
        /// <summary>
        /// 获取实例
        /// </summary>
        public static readonly VersionedQueryInContextBuilder<T, TCollection> Instance = new VersionedQueryInContextBuilder<T, TCollection>();

        private VersionedQueryInContextBuilder()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sqlContext"></param>
        /// <param name="condition"></param>
        /// <param name="orderByBuilder"></param>
        /// <param name="afterLoadAction"></param>
        /// <param name="action"></param>
        /// <param name="createNewAction"></param>
        /// <param name="timePoint"></param>
        /// <param name="tableName"></param>
        /// <param name="mappings"></param>
        public void RegisterLoadByBuilderInContext(SqlContextItem sqlContext, string condition, OrderBySqlClauseBuilder orderByBuilder, Action<TCollection> afterLoadAction, Action<TCollection> action, Func<DataRow, T> createNewAction, DateTime timePoint, string tableName, ORMappingItemCollection mappings)
        {
            if (mappings == null)
                mappings = this.GetQueryMappingInfo();

            if (tableName.IsNullOrEmpty())
                tableName = GetQueryTableName(mappings, timePoint);

            string sql = GetLoadSqlByBuilder(condition, orderByBuilder, timePoint, tableName);

            this.RegisterQueryData(sqlContext, tableName, mappings, sql, (collection) =>
            {
                if (afterLoadAction != null)
                    afterLoadAction(collection);

                if (action != null)
                    action(collection);
            },
            createNewAction);
        }

        /// <summary>
        /// 构造查询语句
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="orderByBuilder"></param>
        /// <param name="tableName"></param>
        /// <param name="timePoint"></param>
        /// <returns></returns>
        public string GetLoadSqlByBuilder(string condition, OrderBySqlClauseBuilder orderByBuilder, DateTime timePoint, string tableName)
        {
            string sql = string.Format("SELECT * FROM {0} WHERE ", tableName);

            ConnectiveSqlClauseCollection timePointBuilder = VersionStrategyQuerySqlBuilder.Instance.TimePointToBuilder(timePoint);

            if (condition.IsNotEmpty())
                sql = sql + condition;

            sql += " AND " + timePointBuilder.ToSqlString(TSqlBuilder.Instance);

            if (orderByBuilder != null)
                sql = sql + string.Format(" ORDER BY {0}", orderByBuilder.ToSqlString(TSqlBuilder.Instance));

            return sql;
        }

        private static string GetQueryTableName(ORMappingItemCollection mappings, DateTime timePoint)
        {
            string result = mappings.GetQueryTableName();

            if (timePoint != DateTime.MinValue)
                result = mappings.TableName;

            return result;
        }
    }
}
