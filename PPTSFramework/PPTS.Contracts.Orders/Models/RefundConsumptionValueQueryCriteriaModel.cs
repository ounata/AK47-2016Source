using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Contracts.Orders.Models
{
    /// <summary>
    /// 退费课时消耗价值查询条件
    /// </summary>
    [DataContract]
    public class RefundConsumptionValueQueryCriteriaModel
    {
        /// <summary>
        /// 账户ID
        /// </summary>
        [DataMember]
        public string AccountID
        { get; set; }

        /// <summary>
        /// 倒数第二次折扣率变化的充值收款时间点
        /// </summary>
        [DataMember]
        public DateTime LastChargeDate
        { get; set; }

        /// <summary>
        /// 最后一次折扣率变化的充值收款时间点
        /// </summary>
        [DataMember]
        public DateTime LastestChargeDate
        { get; set; }

        /// <summary>
        /// 最后一次退费最终确认时间
        /// </summary>
        [DataMember]
        public DateTime LastestRefundDate
        { get; set; }
    }
}
