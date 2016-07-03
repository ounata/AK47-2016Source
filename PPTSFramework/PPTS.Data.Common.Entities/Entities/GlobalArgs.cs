using MCS.Library.Net.SNTP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Common
{
    [Serializable]
    public class GlobalArgs
    {
        /// <summary>
        /// 获取当前结账日期
        /// </summary>
        /// <returns></returns>
        public DateTime GetCurrentClosingAccountDate()
        {
            DateTime date = SNTPClient.AdjustedTime.Date;
            int days = this.ClosingAccountDay - date.Day;
            return date.AddDays(days).AddMonths(1).AddHours(24).AddMinutes(59).AddSeconds(59);
        }

        /// <summary>
        /// 指定的年月是否已关帐。
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public bool IsClosedToAcccount(int year, int month)
        {
            DateTime date = DateTime.Parse(string.Format("{0}-{1}-1", year, month));
            return IsClosedToAcccount(date);
        }

        /// <summary>
        /// 指定的日期是否已关帐了
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public bool IsClosedToAcccount(DateTime date)
        {
            DateTime endDate = this.GetCurrentClosingAccountDate();
            DateTime startDate = endDate.AddDays(1 - endDate.Day).AddMonths(-1).Date;
            return date.Date < startDate;
        }

        /// <summary>
        /// 结账日
        /// </summary>
        public int ClosingAccountDay
        {
            set;
            get;
        }

        public GlobalArgs(int closingAccountDay)
        {
            this.ClosingAccountDay = closingAccountDay;
        }
    }
}
