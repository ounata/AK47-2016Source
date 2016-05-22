using MCS.Library.Net.SNTP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Common
{
    public struct ConfigArgs
    {
        /// <summary>
        /// 获取当前结账日期
        /// </summary>
        /// <returns></returns>
        public DateTime GetCurrentClosingAccountDate()
        {
            DateTime date = SNTPClient.AdjustedTime.Date;
            return date.AddHours(24).AddMinutes(59).AddSeconds(59);            
        }

        /// <summary>
        /// 使用的折扣方案
        /// </summary>
        public DiscountSchemaDefine DiscountSchema
        {
            set;
            get;
        }

        /// <summary>
        /// 结账日
        /// </summary>
        public int ClosingAccountDay
        {
            set;
            get;
        }

        /// <summary>
        /// 账户首次充值的最小金额(500)
        /// </summary>
        public decimal AccountFirstChargeMinMoney
        {
            set;
            get;
        }

        /// <summary>
        /// 结课与非结课账户价值阈值
        /// </summary>
        public decimal EndingClassMinAccountValue
        {
            set;
            get;
        }

        /// <summary>
        /// 账户充值前期最小天数要求(15)
        /// </summary>
        public int AccountChargeEarlyMinDays
        {
            set;
            get;
        }

        /// <summary>
        /// 账户充值前期结课后多少天配置 (30)
        /// </summary>
        public int AccountChargeEarlyMinDaysX
        {
            set;
            get;
        }

        /// <summary>
        /// 账户退费类型判定的天数（默认7天）
        /// </summary>
        public int AccountRefundTypeJudgeDays
        {
            get;
            set;
        }
    }

    /// <summary>
    /// 折扣方案
    /// </summary>
    public enum DiscountSchemaDefine
    {
        /// <summary>
        /// 折扣方案1
        /// </summary>
        Schema1,

        /// <summary>
        /// 折扣方案2
        /// </summary>
        Schema2
    }
}
