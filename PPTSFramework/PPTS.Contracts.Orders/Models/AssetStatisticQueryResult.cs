using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Contracts.Orders.Models
{
    /// <summary>
    /// 资产统计信息查询结果
    /// </summary>
    [DataContract]
    public class AssetStatisticQueryResult
    {
        /// <summary>
        /// 订购资金余额
        /// </summary>
        [DataMember]
        public decimal AssetMoney
        {
            get;
            set;
        }

        /// <summary>
        /// 当前已排定课时数量
        /// </summary>
        [DataMember]
        public decimal AssignedAmount
        {
            get;
            set;
        }

        /// <summary>
        /// 当前已确认课时数量
        /// </summary>
        [DataMember]
        public decimal ConfirmedAmount
        {
            get;
            set;
        }
    }
}
