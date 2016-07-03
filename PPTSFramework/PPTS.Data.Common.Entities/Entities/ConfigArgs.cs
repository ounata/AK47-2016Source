using MCS.Library.Net.SNTP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PPTS.Data.Common
{
    [Serializable]
    public class ConfigArgs
    {
        /// <summary>
        /// 使用拓路折扣2
        /// </summary>
        [XmlIgnore]
        public bool IsTulandDiscountSchema2
        {
            set;
            get;
        }

        /// <summary>
        /// 账户首次充值的最小金额(500)
        /// </summary>
        [XmlIgnore]
        public decimal AccountFirstChargeMinMoney
        {
            set;
            get;
        }

        /// <summary>
        /// 结课与非结课账户价值阈值（默认200）
        /// </summary>
        [XmlIgnore]
        public decimal EndingClassMinAccountValue
        {
            set;
            get;
        }

        /// <summary>
        /// 账户充值前期最小天数要求(默认15天)
        /// </summary>
        [XmlIgnore]
        public int AccountChargeEarlyMinDays
        {
            set;
            get;
        }

        /// <summary>
        /// 账户退费类型判定的天数（默认7天）
        /// </summary>
        [XmlIgnore]
        public int AccountRefundTypeJudgeDays
        {
            get;
            set;
        }
    }
}
