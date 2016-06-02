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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix">分公司大写字母缩写</param>
        /// <returns></returns>
        public static string GetDiscountCode(string prefix) {
            string pattern = string.Format("{0}{1}","ZK", prefix);

            int counterValue = Counter.NewCountValue(pattern);

            return string.Format("{0}{1:yyMMdd}{2:0000}", pattern, DateTime.UtcNow, counterValue);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix">分公司名</param>
        /// <returns></returns>
        public static string GetDiscountName(string prefix)
        {
            return string.Format("{0}{1}",prefix,"折扣表");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix">分公司大写字母缩写</param>
        /// <returns></returns>
        public static string GetPresentCode(string prefix)
        {
            string pattern = string.Format("{0}{1}", "MZ", prefix);

            int counterValue = Counter.NewCountValue(pattern);

            return string.Format("{0}{1:yyMMdd}{2:0000}", pattern, DateTime.UtcNow, counterValue);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix">分公司名</param>
        /// <returns></returns>
        public static string GetPresentName(string prefix)
        {
            return string.Format("{0}{1}", prefix, "买赠表");
        }
    }
}
