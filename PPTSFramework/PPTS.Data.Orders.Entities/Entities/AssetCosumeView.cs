using System;
using System.Runtime.Serialization;
using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using PPTS.Data.Common.Entities;

namespace PPTS.Data.Orders.Entities
{
	[Serializable]
    [ORTableMapping("OM.v_AssetConsumes")]
    [DataContract]
	public class AssetConsumeView : AssetConsume
    {
        /// <summary>
        /// 教师ID
        /// </summary>
        [ORFieldMapping("TeacherID")]
        [DataMember]
        public string TeacherID
        {
            get;
            set;
        }

        /// <summary>
        /// 教师姓名
        /// </summary>
        [ORFieldMapping("TeacherName")]
        [DataMember]
        public string TeacherName
        {
            get;
            set;
        }

        /// <summary>
        /// 教师岗位ID
        /// </summary>
        [ORFieldMapping("TeacherJobID")]
        [DataMember]
        public string TeacherJobID
        {
            get;
            set;
        }

        /// <summary>
        /// 上课开始时间
        /// </summary>
        [ORFieldMapping("StartTime", UtcTimeToLocal = true)]
        [DataMember]
        public DateTime StartTime
        {
            get;
            set;
        }

        /// <summary>
        /// 上课结束时间
        /// </summary>
        [ORFieldMapping("EndTime", UtcTimeToLocal = true)]
        [DataMember]
        public DateTime EndTime
        {
            get;
            set;
        }

        /// <summary>
        /// 课次时长
        /// </summary>
        [ORFieldMapping("DurationValue")]
        [DataMember]
        public decimal DurationValue
        {
            get;
            set;
        }
    }

    [Serializable]
    [DataContract]
    public class AssetConsumeViewCollection : EditableDataObjectCollectionBase<AssetConsumeView>
    {
    }
}