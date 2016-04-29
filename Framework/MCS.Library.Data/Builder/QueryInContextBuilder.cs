using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCS.Library.Core;
using MCS.Library.Data.Mapping;
using System.Data;
using MCS.Library.Data.DataObjects;

namespace MCS.Library.Data.Builder
{
    /// <summary>
    /// 在上下文中进行查询的语句构造器
    /// </summary>
    public class QueryInContextBuilder<T, TCollection> : QueryInContextBuilderBase<T, TCollection> where TCollection : IList<T>, new()
    {
        /// <summary>
        /// 获取实例
        /// </summary>
        public static readonly QueryInContextBuilder<T, TCollection> Instance = new QueryInContextBuilder<T, TCollection>();

        private QueryInContextBuilder()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sqlContext">Sql语句上下文</param>
        /// <param name="condition"></param>
        /// <param name="orderByBuilder"></param>
        /// <param name="afterLoadAction"></param>
        /// <param name="action"></param>
        /// <param name="createNewAction"></param>
        /// <param name="tableName"></param>
        /// <param name="mappings"></param>
        public void RegisterLoadByBuilderInContext(SqlContextItem sqlContext, string condition, OrderBySqlClauseBuilder orderByBuilder, Action<TCollection> afterLoadAction, Action<TCollection> action, Func<DataRow, T> createNewAction, string tableName, ORMappingItemCollection mappings)
        {
            if (mappings == null)
                mappings = this.GetQueryMappingInfo();

            string queryTableName = mappings.GetQueryTableName();

            if (tableName.IsNullOrEmpty())
                tableName = queryTableName;

            string sql = GetLoadSqlByBuilder(condition, orderByBuilder, queryTableName);

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
        /// 根据条件和排序得到查询语句
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="orderByBuilder"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public string GetLoadSqlByBuilder(string condition, OrderBySqlClauseBuilder orderByBuilder, string tableName)
        {
            string sql = string.Format("SELECT * FROM {0}", tableName);

            if (condition.IsNotEmpty())
                sql = sql + string.Format(" WHERE {0}", condition);

            if (orderByBuilder != null)
                sql = sql + string.Format(" ORDER BY {0}", orderByBuilder.ToSqlString(TSqlBuilder.Instance));

            return sql;
        }
    }
}
