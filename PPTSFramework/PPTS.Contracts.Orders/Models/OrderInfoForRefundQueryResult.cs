using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Contracts.Orders.Models
{
    /// <summary>
    /// 订单资产信息返回信息(服务于退费)
    /// </summary>
    [DataContract]
    public class OrderInfoForRefundQueryResult
    {
        /// <summary>
        /// 账户ID
        /// </summary>
        public string AccountID { get; set; }

        /// <summary>
        /// 命中时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 订购资金余额(未排+未上)
        /// </summary>
        public decimal AssetMoney { get; set; }

        /// <summary>
        /// 消耗课时价值(指定时间段后的某一账号的sum(确认课时价值))
        /// </summary>
        public decimal ConsumptionValue { get; set; }

        /// <summary>
        /// 折扣返还金额(指定时间段后的某一账号的sum(确认课时价值/原折扣率*(新折扣率-原折扣率)))
        /// </summary>
        public decimal ReallowanceMoney { get; set; }
    }
}
