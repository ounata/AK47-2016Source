using MCS.Library.Core;
using MCS.Library.Data.Adapters;
using MCS.Library.Data.DataObjects;
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
    /// 在上下文中进行查询的语句构造器的基类
    /// </summary>
    public abstract class QueryInContextBuilderBase<T, TCollection> where TCollection : IList<T>, new()
    {
        /// <summary>
        /// 得到查询数据时的ORMapping信息
        /// </summary>
        /// <returns></returns>
        public virtual ORMappingItemCollection GetQueryMappingInfo()
        {
            return ORMapping.GetMappingInfo(typeof(T));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mapping"></param>
        /// <param name="table"></param>
        /// <param name="createNewAction"></param>
        /// <returns></returns>
        public TCollection DataTableToCollection(ORMappingItemCollection mapping, DataTable table, Func<DataRow, T> createNewAction)
        {
            createNewAction.NullCheck("createNewAction");

            TCollection result = new TCollection();

            foreach (DataRow row in table.Rows)
            {
                T data = default(T);

                if (createNewAction != null)
                    data = createNewAction(row);

                ORMapping.DataRowToObject(row, mapping, data);

                if (data is ILoadableDataEntity)
                    ((ILoadableDataEntity)data).Loaded = true;

                result.Add(data);
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mapping"></param>
        /// <param name="sql"></param>
        /// <param name="connectionName"></param>
        /// <param name="createNewAction"></param>
        /// <returns></returns>
        public TCollection QueryData(ORMappingItemCollection mapping, string sql, string connectionName, Func<DataRow, T> createNewAction)
        {
            DataTable table = DbHelper.RunSqlReturnDS(sql, connectionName).Tables[0];

            return this.DataTableToCollection(mapping, table, createNewAction);
        }

        /// <summary>
        /// 在上下文中注册查询返回的结果
        /// </summary>
        /// <param name="sqlContext">Sql语句上下文</param>
        /// <param name="tableName"></param>
        /// <param name="mapping"></param>
        /// <param name="sql"></param>
        /// <param name="action"></param>
        /// <param name="createNewAction"></param>
        public void RegisterQueryData(SqlContextItem sqlContext, string tableName, ORMappingItemCollection mapping, string sql, Action<TCollection> action, Func<DataRow, T> createNewAction)
        {
            sqlContext.NullCheck("sqlContext");

            sqlContext.AppendSqlWithSperatorInContext(TSqlBuilder.Instance, sql);
            sqlContext.RegisterTableAction(tableName, (table) =>
            {
                TCollection collection = this.DataTableToCollection(mapping, table, createNewAction);

                if (action != null)
                    action(collection);
            });
        }
    }
}
