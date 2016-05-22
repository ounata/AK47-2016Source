using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Contracts.Orders.Models
{
    /// <summary>
    /// 订单资产信息查询条件(服务于退费)
    /// </summary>
    [DataContract]
    public class OrderInfoForRefundQueryModel
    {
        /// <summary>
        /// 账户ID
        /// </summary>
        [DataMember]
        public string AccountID
        { get; set; }

        /// <summary>
        /// 新折扣率
        /// </summary>
        public decimal NewDiscountRate
        { get; set; }

        /// <summary>
        /// 最后一次充值收款时间点
        /// </summary>
        [DataMember]
        public DateTime LastestChargePayDate
        { get; set; }

        /// <summary>
        /// 倒数第二次充值收款时间点
        /// </summary>
        [DataMember]
        public DateTime LastChargePayDate
        { get; set; }

        /// <summary>
        /// 最后一次退费最终确认时间
        /// </summary>
        [DataMember]
        public DateTime LastestRefundVerifyDate
        { get; set; }
    }
}
