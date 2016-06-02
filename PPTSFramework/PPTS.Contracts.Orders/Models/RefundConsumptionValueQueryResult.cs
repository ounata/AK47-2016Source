using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Contracts.Orders.Models
{
    /// <summary>
    /// 退费课时消耗价值查询结果
    /// </summary>
    [DataContract]
    public class RefundConsumptionValueQueryResult
    {
        /// <summary>
        /// 账户ID
        /// </summary>
        [DataMember]
        public string AccountID { get; set; }

        /// <summary>
        /// 消耗课时价值(指定时间段后的某一账号的sum(确认课时价值))
        /// </summary>
        [DataMember]
        public decimal ConsumptionValue { get; set; }

        /// <summary>
        /// 折扣返还计算的时间点
        /// </summary>
        [DataMember]
        public DateTime ReallowanceStartTime { get; set; }
    }
}
