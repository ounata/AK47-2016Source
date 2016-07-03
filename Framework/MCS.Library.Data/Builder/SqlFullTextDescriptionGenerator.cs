using MCS.Library.Core;
using MCS.Library.Expression;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Data.Builder
{
    /// <summary>
    /// Sql Server全文检索值的描述生成器
    /// </summary>
    internal class SqlFullTextDescriptionGenerator : DataDescriptionGeneratorBase
    {
        private static readonly char[] KeywordSeparators = new char[] { ' ', '　' };

        /// <summary>
        /// 
        /// </summary>
        public static readonly SqlFullTextDescriptionGenerator Instance = new SqlFullTextDescriptionGenerator();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="builderItem"></param>
        /// <returns></returns>
        protected override bool DecideIsMatched(SqlCaluseBuilderItemWithData builderItem)
        {
            bool result = false;

            /*
             *string pattern2 = @"\b(?i)(constains){1}\s*\({1}.*\){1}";
            foreach (Match match in Regex.Matches(input, pattern2))
            {
                Console.WriteLine(match.Value);
            }
             */
            SqlClauseBuilderItemUW uwItem = builderItem as SqlClauseBuilderItemUW;

            if (uwItem != null)
            {
                if (uwItem.Template.IsNotEmpty())
                {
                    ParseResult pr = ExpressionParser.Parse(uwItem.Template, false);

                    if (pr.Identifiers != null)
                        pr.Identifiers.ScanIdentifiers(id =>
                        {
                            if (string.Compare(id.Identifier, "contains", true) == 0)
                                result = true;

                            return result == false;
                        });
                }
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="builderItem"></param>
        /// <param name="builder"></param>
        /// <returns></returns>
        protected override string GetDescription(SqlCaluseBuilderItemWithData builderItem, ISqlBuilder builder)
        {
            StringBuilder strB = new StringBuilder();

            if (builderItem != null)
            {
                IEnumerable<string> keywords = GetKeywords(builderItem.Data.ToString());

                foreach (string keyword in keywords)
                {
                    if (strB.Length > 0)
                        strB.Append(" OR ");

                    strB.Append('"');
                    strB.Append(keyword.Replace("\"", "\"\""));
                    strB.Append('"');
                }
            }

            return builder.CheckUnicodeQuotationMark(strB.ToString());
        }

        private static string[] GetKeywords(string dataValue)
        {
            return dataValue.Split(KeywordSeparators, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
