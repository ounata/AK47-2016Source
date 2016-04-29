using MCS.Library.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Data.Builder
{
    /// <summary>
    /// 生成一组带值和常量的SELECT子句。其子句类型为'Hello', 10 AS AGE...
    /// </summary>
    public class SelectSqlClauseBuilder : SqlClauseBuilderUW
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataField"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public override SqlClauseBuilderIUW AppendItem<T>(string dataField, T data)
        {
            return base.AppendItem<T>(dataField, data, string.Empty);
        }

        /// <summary>
        /// 直接添加值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public SqlClauseBuilderIUW AppendValue<T>(T data)
        {
            return base.AppendItem<T>(string.Empty, data, string.Empty);
        }

        /// <summary>
        /// 返回字段名的SQL语句
        /// </summary>
        /// <param name="sqlBuilder"></param>
        /// <returns></returns>
        public string ToFieldsSqlString(ISqlBuilder sqlBuilder)
        {
            StringBuilder strB = new StringBuilder(256);

            foreach (SqlClauseBuilderItemUW item in List)
            {
                if (item.DataField.IsNotEmpty())
                {
                    if (strB.Length > 0)
                        strB.Append(", ");

                    strB.Append(item.DataField);
                }
            }

            return strB.ToString();
        }

        /// <summary>
        /// 转换成SQL子句
        /// </summary>
        /// <param name="sqlBuilder"></param>
        /// <returns></returns>
        public override string ToSqlString(ISqlBuilder sqlBuilder)
        {
            sqlBuilder.NullCheck("sqlBuilder");

            StringBuilder strB = new StringBuilder(256);

            foreach (SqlClauseBuilderItemUW item in List)
            {
                if (strB.Length > 0)
                    strB.Append(", ");

                item.ToSqlString(strB, sqlBuilder);
            }

            return strB.ToString();
        }

        /// <summary>
        /// 创建新的item
        /// </summary>
        /// <returns></returns>
        protected override SqlClauseBuilderItemBase CreateBuilderItem()
        {
            return new SqlClauseBuilderItemSelect();
        }
    }
}
