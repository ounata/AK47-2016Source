using MCS.Library.SOA.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Customers
{
    public static class Helper
    {
        /// <summary>
        /// 计算客户编码
        /// </summary>
        /// <param name="prefix"></param>
        /// <returns></returns>
        public static string GetCustomerCode(string prefix)
        {
            string pattern = string.Format("{0}{1:yyMMdd}", prefix, MCS.Library.Net.SNTP.SNTPClient.AdjustedLocalTime);

            int counterValue = Counter.NewCountValue(pattern);

            return string.Format("{0}{1:000000}", pattern, counterValue);
        }

        /// <summary>
        /// 计算账户编号
        /// </summary>
        /// <returns></returns>
        public static string GetAccountCode()
        {
            string pattern = string.Format("{0}{1:yyMMdd}", "AC", MCS.Library.Net.SNTP.SNTPClient.AdjustedLocalTime);

            int counterValue = Counter.NewCountValue(pattern);

            return string.Format("{0}{1:000}", pattern, counterValue);
        }

        /// <summary>
        /// 计算申请单号
        /// </summary>
        /// <param name="prefix"></param>
        /// <returns></returns>
        public static string GetApplyNo(string prefix)
        {
            string pattern = string.Format("{0}{1:yyMMdd}", prefix, MCS.Library.Net.SNTP.SNTPClient.AdjustedLocalTime);

            int counterValue = Counter.NewCountValue(pattern);

            return string.Format("{0}{1:000000}", pattern, counterValue);
        }

        public static string EncodeString(this string text)
        {
            if (!string.IsNullOrEmpty(text))
                return text.Replace("'", "''");
            return text;
        }
    }
}
