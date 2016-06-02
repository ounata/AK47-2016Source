using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Customers.Entities
{
    [Serializable]
    [ORTableMapping("CM.POSRecords")]
    [DataContract]
    public class POSRecord
    {
        public POSRecord()
        { }
        /// <summary>
		/// 交易参考号
		/// </summary>
		[ORFieldMapping("TransactionID", PrimaryKey = true)]
        [DataMember]
        public string TransactionID
        {
            get;
            set;
        }

        /// <summary>
		/// 校区ID（商户号）
		/// </summary>
		[ORFieldMapping("MerchantID", PrimaryKey = true)]
        [DataMember]
        public string MerchantID
        {
            get;
            set;
        }

        /// <summary>
		/// 终端号
		/// </summary>
		[ORFieldMapping("POSID", PrimaryKey = true)]
        [DataMember]
        public string POSID
        {
            get;
            set;
        }

        /// <summary>
        /// 交易日期
        /// </summary>
        [ORFieldMapping("TransactionDate", UtcTimeToLocal = true)]
        [DataMember]
        public DateTime TransactionDate
        { get; set; }

        /// <summary>
        /// 清算日期
        /// </summary>
        [ORFieldMapping("SettlementDate", UtcTimeToLocal = true)]
        [DataMember]
        public DateTime SettlementDate
        { get; set; }

        /// <summary>
        /// 刷卡交易时间
        /// </summary>
        [ORFieldMapping("TransactionTime", UtcTimeToLocal = true)]
        [DataMember]
        public DateTime TransactionTime
        { get; set; }

        /// <summary>
        /// 银行卡号
        /// </summary>
        [ORFieldMapping("CardNum")]
        [DataMember]
        public string CardNum
        { get; set; }

        /// <summary>
        /// 刷卡金额
        /// </summary>
        [ORFieldMapping("Money")]
        [DataMember]
        public decimal Money
        { get; set; }

        /// <summary>
        /// 来源类型(1--接口(实时接口)来源、2--对账(异步接口)来源)
        /// </summary>
        [ORFieldMapping("FromType")]
        [DataMember]
        public string FromType
        { get; set; }

        /// <summary>
        /// 是否核销
        /// </summary>
        [ORFieldMapping("IsUsered")]
        [DataMember]
        public bool IsUsered
        { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [ORFieldMapping("CreateTime", UtcTimeToLocal = true)]
        [SqlBehavior(BindingFlags = ClauseBindingFlags.Select | ClauseBindingFlags.Where)]
        [DataMember]
        public DateTime CreateTime
        {
            get;
            set;
        }

        [ORFieldMapping("TransactionTimeValue")]
        [DataMember]
        public string TransactionTimeValue
        {
            get;
            set;
        }
    }
    [Serializable]
    [DataContract]
    public class POSRecordCollection : EditableDataObjectCollectionBase<POSRecord>
    {

    }
}
