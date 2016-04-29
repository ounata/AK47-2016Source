using MCS.Library.SOA.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Products
{
    internal static class Helper
    {
        /// <summary>
        /// 计算产品编码
        /// </summary>
        /// <param name="prefix"></param>
        /// <returns></returns>
        public static string GetProductCode(string prefix)
        {
            string pattern = string.Format("{0}{1:yyMMdd}", prefix, DateTime.UtcNow);

            int counterValue = Counter.NewCountValue(pattern);

            return string.Format("{0}{1:000000}", pattern, counterValue);
        }
    }
}
