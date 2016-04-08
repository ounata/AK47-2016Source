using MCS.Library.Caching;
using MCS.Library.Core;
using MCS.Library.Data.Builder;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Data
{
    /// <summary>
    /// SQL语句的上下文缓存
    /// </summary>
    public class SqlContextItem
    {
        private StringBuilder sqlInContext = new StringBuilder(256);

        /// <summary>
        /// 在上下文中的注册的返回结果处理操作
        /// </summary>
        private TableActionCollection tableActions = new TableActionCollection();

        /// <summary>
        /// DataTable的回调操作
        /// </summary>
        internal TableActionCollection TableActions
        {
            get { return this.tableActions; }
        }

        /// <summary>
        /// 在上下文中添加待执行的SQL语句，自动添加语句分隔符
        /// </summary>
        /// <param name="sqlBuilder"></param>
        /// <param name="withSeperator"></param>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public void AppendSqlInContext(SqlBuilderBase sqlBuilder, bool withSeperator, string format, params object[] args)
        {
            sqlBuilder.NullCheck("sqlBuilder");

            if (this.sqlInContext.Length > 0 && withSeperator)
                this.sqlInContext.Append(sqlBuilder.DBStatementSeperator);

            this.sqlInContext.AppendFormat(format, args);
        }

        /// <summary>
        /// 在上下文中添加待执行的SQL语句
        /// </summary>
        /// <param name="sqlBuilder"></param>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public void AppendSqlInContext(SqlBuilderBase sqlBuilder, string format, params object[] args)
        {
            AppendSqlInContext(sqlBuilder, false, format, args);
        }

        /// <summary>
        /// 在上下文中添加待执行的SQL语句，自动添加语句分隔符
        /// </summary>
        /// <param name="sqlBuilder"></param>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public void AppendSqlWithSperatorInContext(SqlBuilderBase sqlBuilder, string format, params object[] args)
        {
            AppendSqlInContext(sqlBuilder, true, format, args);
        }

        /// <summary>
        /// 清除上下文中的SQL语句
        /// </summary>
        public void ClearSqlInContext()
        {
            this.sqlInContext.Clear();
            this.tableActions.Clear();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetSqlInContext()
        {
            return this.sqlInContext.ToString();
        }

        /// <summary>
        /// 注册查询结果返回的DataTable的处理类
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="action"></param>
        public void RegisterTableAction(string tableName, Action<DataTable> action)
        {
            tableName.CheckStringIsNullOrEmpty("tableName");

            this.tableActions.Add(new TableAction(tableName, action));
        }
    }

    internal class SqlContext : Dictionary<string, SqlContextItem>
    {
        private const string NamePostfixSqlContext = ".SqlContext";

        /// <summary>
        /// 得到
        /// </summary>
        /// <param name="connectionName"></param>
        /// <returns></returns>
        public static SqlContextItem GetContext(string connectionName)
        {
            connectionName.CheckStringIsNullOrEmpty("connectionName");

            SqlContextItem result = null;

            if (Context.TryGetValue(connectionName, out result) == false)
            {
                result = new SqlContextItem();

                Context.Add(connectionName, result);
            }

            return result;
        }

        private static SqlContext Context
        {
            get
            {
                const string itemKey = "DeluxeWorks" + NamePostfixSqlContext;

                return (SqlContext)ObjectContextCache.Instance.GetOrAddNewValue(itemKey, (cache, key) =>
                {
                    SqlContext sc = new SqlContext();

                    cache.Add(key, sc);

                    return sc;
                });
            }
        }
    }

}
