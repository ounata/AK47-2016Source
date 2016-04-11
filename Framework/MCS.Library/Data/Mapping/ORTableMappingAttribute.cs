using System;
using System.Collections.Generic;
using System.Text;

namespace MCS.Library.Data.Mapping
{
    /// <summary>
    /// 加在类定义之前，用于表示表名的Attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class ORTableMappingAttribute : System.Attribute
    {
        private string tableName = string.Empty;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="tblName">表名</param>
        public ORTableMappingAttribute(string tblName)
        {
            this.tableName = tblName;
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="queryTableName">查询时所用的表名</param>
        public ORTableMappingAttribute(string tableName, string queryTableName)
        {
            this.tableName = tableName;
            this.QueryTableName = queryTableName;
        }

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName
        {
            get { return this.tableName; }
            set { this.tableName = value; }
        }

        /// <summary>
        /// 查询时所用的表名
        /// </summary>
        public string QueryTableName
        {
            get;
            set;
        }
    }
}
