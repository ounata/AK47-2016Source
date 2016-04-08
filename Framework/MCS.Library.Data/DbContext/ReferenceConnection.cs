using MCS.Library.Core;
using MCS.Library.Data.Builder;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Data
{
    internal class ReferenceConnection
    {
        private DbConnection connection = null;
        private int referenceCount = 0;
        private string name = string.Empty;

        ///// <summary>
        ///// 保存在上下文中的SQL
        ///// </summary>
        //private StringBuilder sqlInContext = new StringBuilder(256);

        ///// <summary>
        ///// 在上下文中的注册的返回结果处理操作
        ///// </summary>
        //private TableActionCollection tableActions = new TableActionCollection();

        /// <summary>
        /// 引用连接
        /// </summary>
        /// <param name="connName">连接名称</param>
        /// <param name="conn">数据库连接对象</param>
        public ReferenceConnection(string connName, DbConnection conn)
        {
            this.name = connName;
            this.connection = conn;
            this.referenceCount++;
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public DbConnection Connection
        {
            get { return this.connection; }
            set { this.connection = value; }
        }

        public int ReferenceCount
        {
            get { return this.referenceCount; }
            set { this.referenceCount = value; }
        }

        //public TableActionCollection TableActions
        //{
        //    get { return this.tableActions; }
        //}

        //public void AppendSqlInContext(SqlBuilderBase sqlBuilder, bool withSeperator, string format, params object[] args)
        //{
        //    sqlBuilder.NullCheck("sqlBuilder");

        //    if (this.sqlInContext.Length > 0 && withSeperator)
        //        this.sqlInContext.Append(sqlBuilder.DBStatementSeperator);

        //    this.sqlInContext.AppendFormat(format, args);
        //}

        //public void RegisterTableAction(string tableName, Action<DataTable> action)
        //{
        //    tableName.CheckStringIsNullOrEmpty("tableName");

        //    this.tableActions.Add(new TableAction(tableName, action));
        //}

        //public void ClearSqlInContext()
        //{
        //    this.sqlInContext.Clear();
        //    this.tableActions.Clear();
        //}

        //public string GetSqlInContext()
        //{
        //    return this.sqlInContext.ToString();
        //}
    }
}
