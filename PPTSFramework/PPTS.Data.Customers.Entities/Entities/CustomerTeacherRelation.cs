using MCS.Library.Core;
using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using PPTS.Data.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace PPTS.Data.Customers.Entities
{
    /// <summary>
    /// This object represents the properties and methods of a CustomerTeacherRelation.
    /// 学员教师关系表
    /// </summary>
    [Serializable]
    [ORTableMapping("CM.CustomerTeacherRelations", "CM.CustomerTeacherRelations_Current")]
    [DataContract]
    public class CustomerTeacherRelation : IVersionDataObjectWithoutID, IEntityWithCreator
    {
        public CustomerTeacherRelation()
        {
        }

        [ORFieldMapping("ID", PrimaryKey = true)]
        [DataMember]
        public string ID { get; set; }


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
        /// 教师OA编码
        /// </summary>
        [ORFieldMapping("TeacherOACode")]
        [DataMember]
        public string TeacherOACode
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
        /// 教师岗位学科组ID
        /// </summary>
        [ORFieldMapping("TeacherJobOrgID")]
        [DataMember]
        public string TeacherJobOrgID
        {
            get;
            set;
        }

        /// <summary>
        /// 教师岗位学科组姓名
        /// </summary>
        [ORFieldMapping("TeacherJobOrgName")]
        [DataMember]
        public string TeacherJobOrgName
        {
            get;
            set;
        }

        /// <summary>
        /// 是否全职教师
        /// </summary>
        [ORFieldMapping("IsFullTimeTeacher")]
        [DataMember]
        public bool IsFullTimeTeacher
        {
            get;
            set;
        }

        /// <summary>
        /// 创建人ID
        /// </summary>
        [ORFieldMapping("CreatorID")]
        [SqlBehavior(BindingFlags = ClauseBindingFlags.All & ~ClauseBindingFlags.Update)]
        [DataMember]
        public string CreatorID
        {
            get;
            set;
        }

        /// <summary>
        /// 创建人姓名
        /// </summary>
        [ORFieldMapping("CreatorName")]
        [SqlBehavior(BindingFlags = ClauseBindingFlags.All & ~ClauseBindingFlags.Update)]
        [DataMember]
        public string CreatorName
        {
            get;
            set;
        }

        /// <summary>
        /// 创建时间
        /// </summary>
        [ORFieldMapping("CreateTime")]
        [SqlBehavior(BindingFlags = ClauseBindingFlags.Select | ClauseBindingFlags.Where)]
        [DataMember]
        public DateTime CreateTime
        {
            get;
            set;
        }

        /// <summary>
        /// 版本开始时间
        /// </summary>
        [DataMember]
        [ORFieldMapping("VersionStartTime", PrimaryKey = true, UtcTimeToLocal = true)]
        [SqlBehavior(BindingFlags = ClauseBindingFlags.All & ~ClauseBindingFlags.Update)]
        public DateTime VersionStartTime
        {
            get; set;
        }

        [DataMember]
        [ORFieldMapping("VersionEndTime", UtcTimeToLocal = true)]
        [SqlBehavior(BindingFlags = ClauseBindingFlags.Select | ClauseBindingFlags.Where)]
        public DateTime VersionEndTime
        {
            get; set;
        }

        [ConstantCategory(Category = "C_CODE_ABBR_BO_Customer_ChangeTeacherReason")]
        [DataMember]
        [NoMapping]
        public string ChangeTeacherReason { get; set; }
    }

    [Serializable]
    [DataContract]
    public class CustomerTeacherRelationCollection : EditableDataObjectCollectionBase<CustomerTeacherRelation>
    {
    }
}