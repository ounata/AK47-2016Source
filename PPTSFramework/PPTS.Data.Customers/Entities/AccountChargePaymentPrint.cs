using MCS.Library.Core;
using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace PPTS.Data.Customers.Entities
{
    /// <summary>
    /// This object represents the properties and methods of a AccountChargePaymentPrint.
    /// 账户缴费支付单打印表
    /// </summary>
    [Serializable]
    [ORTableMapping("AccountChargePaymentPrints")]
    [DataContract]
    public class AccountChargePaymentPrint
    {
        public AccountChargePaymentPrint()
        {
        }

        /// <summary>
        /// 支付单ID
        /// </summary>
        [ORFieldMapping("PayID")]
        [DataMember]
        public string PayID
        {
            get;
            set;
        }

        /// <summary>
        /// 打印ID
        /// </summary>
        [ORFieldMapping("PrintID", PrimaryKey = true)]
        [DataMember]
        public string PrintID
        {
            get;
            set;
        }

        /// <summary>
        /// 打印时间
        /// </summary>
        [ORFieldMapping("PrintTime")]
        [DataMember]
        public DateTime PrintTime
        {
            get;
            set;
        }

        /// <summary>
        /// 打印人ID
        /// </summary>
        [ORFieldMapping("PrintorID")]
        [DataMember]
        public string PrintorID
        {
            get;
            set;
        }

        /// <summary>
        /// 打印人姓名
        /// </summary>
        [ORFieldMapping("PrintorName")]
        [DataMember]
        public string PrintorName
        {
            get;
            set;
        }

        /// <summary>
        /// 打印说明
        /// </summary>
        [ORFieldMapping("PrintMemo")]
        [DataMember]
        public string PrintMemo
        {
            get;
            set;
        }
    }

    [Serializable]
    [DataContract]
    public class AccountChargePaymentPrintCollection : EditableDataObjectCollectionBase<AccountChargePaymentPrint>
    {
    }
}