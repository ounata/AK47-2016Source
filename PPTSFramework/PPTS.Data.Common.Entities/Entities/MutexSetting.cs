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
    [ORTableMapping("MT.MutexSettings")]
    [DataContract]
    public class MutexSetting
    {
        public MutexSetting()
        {
        }

        /// <summary>
        /// 业务操作
        /// </summary>
        [ORFieldMapping("BizAction", PrimaryKey = true)]
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
        /// 互斥的业务操作
        /// </summary>
        [ORFieldMapping("MutexAction", PrimaryKey = true)]
        [DataMember]
        public MutexAction MutexAction
        {
            get;
            set;
        }

        /// <summary>
        /// 互斥的业务操作描述
        /// </summary>
        [ORFieldMapping("MutexActionText")]
        [DataMember]
        public string MutexActionText
        {
            get;
            set;
        }
    }

    [Serializable]
    [DataContract]
    public class MutexSettingCollection : EditableDataObjectCollectionBase<MutexSetting>
    {
    }
}