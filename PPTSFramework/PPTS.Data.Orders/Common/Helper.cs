using MCS.Library.SOA.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Products
{
    public static class Helper
    {
        /// <summary>
        /// 计算订单编码
        /// </summary>
        /// <param name="prefix"></param>
        /// <returns></returns>
        public static string GetOrderCode(string prefix)
        {
            string pattern = string.Format("{0}{1:yyMMdd}", prefix, DateTime.UtcNow);

            int counterValue = Counter.NewCountValue(pattern);

            return string.Format("{0}{1:000000}", pattern, counterValue);
        }

        /// <summary>
        /// 获取班级名称
        /// </summary>
        /// <param name="productCode"></param>
        /// <param name="orgName"></param>
        /// <returns></returns>
        public static string GetClassName(string productCode, string orgName)
        {
            int counterValue1 = Counter.NewCountValue(productCode);
            int counterValue2 = Counter.NewCountValue(string.Format("{0}-{1}", productCode, orgName));
            return string.Format("{0}-{1}-{2}{3}", productCode, counterValue1, orgName, counterValue2);
        }
    }
}
