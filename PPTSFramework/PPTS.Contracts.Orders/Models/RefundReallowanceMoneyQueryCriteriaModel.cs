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
    public class RefundReallowanceMoneyQueryCriteriaModel
    {
        /// <summary>
        /// 账户ID
        /// </summary>
        [DataMember]
        public string AccountID
        { get; set; }

        /// <summary>
        /// 退费后折扣率
        /// </summary>
        [DataMember]
        public decimal RefundDiscountRate
        { get; set; }

        /// <summary>
        /// 折扣返还计算的时间点
        /// </summary>
        [DataMember]
        public DateTime ReallowanceStartTime
        { get; set; }
    }
}
