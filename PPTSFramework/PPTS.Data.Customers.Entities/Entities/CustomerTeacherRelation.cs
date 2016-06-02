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
    /// ѧԱ��ʦ��ϵ��
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
        /// ѧԱID
        /// </summary>        
        [ORFieldMapping("CustomerID")]
        [DataMember]
        public string CustomerID
        {
            get;
            set;
        }

        /// <summary>
        /// ��ʦID
        /// </summary>
        [ORFieldMapping("TeacherID")]
        [DataMember]
        public string TeacherID
        {
            get;
            set;
        }

        /// <summary>
        /// ��ʦOA����
        /// </summary>
        [ORFieldMapping("TeacherOACode")]
        [DataMember]
        public string TeacherOACode
        {
            get;
            set;
        }

        /// <summary>
        /// ��ʦ����
        /// </summary>
        [ORFieldMapping("TeacherName")]
        [DataMember]
        public string TeacherName
        {
            get;
            set;
        }

        /// <summary>
        /// ��ʦ��λID
        /// </summary>
        [ORFieldMapping("TeacherJobID")]
        [DataMember]
        public string TeacherJobID
        {
            get;
            set;
        }

        /// <summary>
        /// ��ʦ��λѧ����ID
        /// </summary>
        [ORFieldMapping("TeacherJobOrgID")]
        [DataMember]
        public string TeacherJobOrgID
        {
            get;
            set;
        }

        /// <summary>
        /// ��ʦ��λѧ��������
        /// </summary>
        [ORFieldMapping("TeacherJobOrgName")]
        [DataMember]
        public string TeacherJobOrgName
        {
            get;
            set;
        }

        /// <summary>
        /// �Ƿ�ȫְ��ʦ
        /// </summary>
        [ORFieldMapping("IsFullTimeTeacher")]
        [DataMember]
        public bool IsFullTimeTeacher
        {
            get;
            set;
        }

        /// <summary>
        /// ������ID
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
        /// ����������
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
        /// ����ʱ��
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
        /// �汾��ʼʱ��
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