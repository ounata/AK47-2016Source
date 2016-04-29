using MCS.Library.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Data.Builder
{
    /// <summary>
    /// 用于SELECT子句的Item
    /// </summary>
    public class SqlClauseBuilderItemSelect : SqlClauseBuilderItemUW
    {
        /// <summary>
        /// 默认的表达式模板
        /// </summary>
        private const string WithFieldAndDataTemplate = "${Data}$ AS ${DataField}$";
        private const string WithDataTemplate = "${Data}$";

        /// <summary>
        /// 根据模板生成SQL子句
        /// </summary>
        /// <param name="strB"></param>
        /// <param name="builder"></param>
        internal override void ToSqlString(StringBuilder strB, ISqlBuilder builder)
        {
            string defaultTemplate = this.GetTemplate();

            base.ToSqlString(strB, defaultTemplate, builder);
        }

        private string GetTemplate()
        {
            string result = WithDataTemplate;

            if (this.DataField.IsNotEmpty())
                result = WithFieldAndDataTemplate;

            return result;
        }
    }
}
