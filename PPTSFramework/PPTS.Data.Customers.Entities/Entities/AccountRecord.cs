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
    /// This object represents the properties and methods of a AccountRecord.
    /// 账户流水记录表
    /// </summary>
    [Serializable]
    [ORTableMapping("CM.AccountRecords")]
    [DataContract]
    public class AccountRecord
    {
        public AccountRecord()
        {
        }

        /// <summary>
        /// 校区ID
        /// </summary>
        [ORFieldMapping("CampusID")]
        [DataMember]
        public string CampusID
        {
            get;
            set;
        }

        /// <summary>
        /// 学员ID
        /// </summary>
        [ORFieldMapping("CustomerID")]
        [DataMember]
        public string CustomerID
        {
            get;
            set;
        }

        /// <summary>
        /// 账户ID
        /// </summary>
        [ORFieldMapping("AccountID")]
        [DataMember]
        public string AccountID
        {
            get;
            set;
        }

        /// <summary>
        /// 流水ID
        /// </summary>
        [ORFieldMapping("RecordID", PrimaryKey = true)]
        [DataMember]
        public string RecordID
        {
            get;
            set;
        }

        /// <summary>
        /// 流水时间
        /// </summary>
        [ORFieldMapping("RecordTime", UtcTimeToLocal = true)]
        [DataMember]
        public DateTime RecordTime
        {
            get;
            set;
        }

        /// <summary>
        /// 流水类型（收入，支出）
        /// </summary>
        [ORFieldMapping("RecordType")]
        [DataMember]
        public string RecordType
        {
            get;
            set;
        }

        /// <summary>
        /// 流水方向（1是入，-1是出）
        /// </summary>
        [ORFieldMapping("RecordFlag")]
        [DataMember]
        public int RecordFlag
        {
            get;
            set;
        }

        /// <summary>
        /// 业务单ID
        /// </summary>
        [ORFieldMapping("BillID")]
        [DataMember]
        public string BillID
        {
            get;
            set;
        }

        /// <summary>
        /// 业务单号
        /// </summary>
        [ORFieldMapping("BillNo")]
        [DataMember]
        public string BillNo
        {
            get;
            set;
        }

        /// <summary>
        /// 业务操作时间
        /// </summary>
        [ORFieldMapping("BillTime", UtcTimeToLocal = true)]
        [DataMember]
        public DateTime BillTime
        {
            get;
            set;
        }

        /// <summary>
        /// 业务类型
        /// </summary>
        [ORFieldMapping("BillType")]
        [DataMember]
        public string BillType
        {
            get;
            set;
        }

        /// <summary>
        /// 业务类型描述
        /// </summary>
        [ORFieldMapping("BillTypeName")]
        [DataMember]
        public string BillTypeName
        {
            get;
            set;
        }

        /// <summary>
        /// 业务金额
        /// </summary>
        [ORFieldMapping("BillMoney")]
        [DataMember]
        public decimal BillMoney
        {
            get;
            set;
        }

        /// <summary>
        /// 业务说明
        /// </summary>
        [ORFieldMapping("BillMemo")]
        [DataMember]
        public string BillMemo
        {
            get;
            set;
        }

        /// <summary>
        /// 业务操作人ID
        /// </summary>
        [ORFieldMapping("BillerID")]
        [DataMember]
        public string BillerID
        {
            get;
            set;
        }

        /// <summary>
        /// 业务操作人姓名
        /// </summary>
        [ORFieldMapping("BillerName")]
        [DataMember]
        public string BillerName
        {
            get;
            set;
        }

        /// <summary>
        /// 业务操作人岗位ID
        /// </summary>
        [ORFieldMapping("BillerJobID")]
        [DataMember]
        public string BillerJobID
        {
            get;
            set;
        }

        /// <summary>
        /// 业务操作人岗位名称
        /// </summary>
        [ORFieldMapping("BillerJobName")]
        [DataMember]
        public string BillerJobName
        {
            get;
            set;
        }
    }

    [Serializable]
    [DataContract]
    public class AccountRecordCollection : EditableDataObjectCollectionBase<AccountRecord>
    {
    }
}