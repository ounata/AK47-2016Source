using MCS.Library.Core;
using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace PPTS.Data.Common.Entities
{
    /// <summary>
    /// This object represents the properties and methods of a Teacher.
    /// 
    /// </summary>
    [Serializable]
    [ORTableMapping("MT.MutexRecords")]
    [DataContract]
    public class MutexRecord
    {
        public MutexRecord()
        {
        }

        /// <summary>
        /// 互斥键
        /// </summary>
        [ORFieldMapping("MutexKey", PrimaryKey = true)]
        [DataMember]
        public string MutexKey
        {
            get;
            set;
        }

        /// <summary>
        /// 业务操作
        /// </summary>
        [ORFieldMapping("BizAction")]
        [DataMember]
        public MutexAction BizAction
        {
            get;
            set;
        }

        /// <summary>
        /// 业务操作描述
        /// </summary>
        [ORFieldMapping("BizActionText")]
        [DataMember]
        public string BizActionText
        {
            get;
            set;
        }

        /// <summary>
        /// 过期时间
        /// </summary>
        [ORFieldMapping("ExpireTime", UtcTimeToLocal = true)]
        [DataMember]
        public DateTime ExpireTime
        {
            get;
            set;
        }

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

        /// <summary>
        /// 业务ID
        /// </summary>
        [ORFieldMapping("BizBillID", PrimaryKey = true)]
        [DataMember]
        public string BizBillID
        {
            get;
            set;
        }

        /// <summary>
        /// 描述
        /// </summary>
        [ORFieldMapping("Description")]
        [DataMember]
        public string Description
        {
            get;
            set;
        }
    }

    [Serializable]
    [DataContract]
    public class MutexRecordCollection : EditableDataObjectCollectionBase<MutexRecord>
    {
    }
}