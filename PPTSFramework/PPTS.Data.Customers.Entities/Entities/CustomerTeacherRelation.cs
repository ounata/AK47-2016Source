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
    /// 
    /// </summary>
    [Serializable]
    [ORTableMapping("CM.CustomerTeacherRelations", "CM.CustomerTeacherRelations_Current")]
    [DataContract]
    public class CustomerTeacherRelation : IVersionDataObjectWithoutID, IEntityWithCreator
    {
        public CustomerTeacherRelation()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        [ORFieldMapping("ID", PrimaryKey = true)]
        [DataMember]
        public string ID
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [ORFieldMapping("CustomerID")]
        [DataMember]
        public string CustomerID
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [ORFieldMapping("OrgID")]
        [DataMember]
        public string OrgID
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [ORFieldMapping("OrgName")]
        [DataMember]
        public string OrgName
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [ORFieldMapping("TeacherID")]
        [DataMember]
        public string TeacherID
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [ORFieldMapping("TeacherCode")]
        [DataMember]
        public string TeacherCode
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [ORFieldMapping("TeacherName")]
        [DataMember]
        public string TeacherName
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [ORFieldMapping("Subject")]
        [DataMember]
        public string Subject
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [ORFieldMapping("Grade")]
        [DataMember]
        public string Grade
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [ORFieldMapping("CreatorID")]
        [DataMember]
        public string CreatorID
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [ORFieldMapping("CreatorName")]
        [DataMember]
        public string CreatorName
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [ORFieldMapping("CreateTime")]
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
        [SqlBehavior(BindingFlags = ClauseBindingFlags.Select | ClauseBindingFlags.Where)]
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
    }

    [Serializable]
    [DataContract]
    public class CustomerTeacherRelationCollection : EditableDataObjectCollectionBase<CustomerTeacherRelation>
    {
    }
}