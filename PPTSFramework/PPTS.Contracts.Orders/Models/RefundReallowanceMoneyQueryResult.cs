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
    public class RefundReallowanceMoneyQueryResult
    {
        /// <summary>
        /// 账户ID
        /// </summary>
        [DataMember]
        public string AccountID { get; set; }

        /// <summary>
        /// 折扣返还金额(指定时间段后的某一账号的sum(确认课时价值/原折扣率*(新折扣率-原折扣率)))
        /// </summary>
        [DataMember]
        public decimal ReallowanceMoney { get; set; }
    }
}
