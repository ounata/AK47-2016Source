using MCS.Library.SOA.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Common
{
    public static class Helper
    {
        /// <summary>
        /// 金额转中文大写
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static string MoneyToCapital(decimal d)
        {
            return d.ToString();
        }
    }
}
