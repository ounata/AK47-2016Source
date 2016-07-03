using MCS.Library.Core;
using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using PPTS.Data.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace PPTS.Data.Orders.Entities
{
    /// <summary>
    /// This object represents the properties and methods of a AccompanyAssign.
    /// 
    /// </summary>
    [Serializable]
    [ORTableMapping("OM.AccompanyAssigns")]
    [DataContract]
    public class AccompanyAssign : IEntityWithCreator, IEntityWithModifier
    {
        public AccompanyAssign()
        {
        }

        /// 校区ID
        [ORFieldMapping("CampusID")]
        [DataMember]
        public string CampusID
        {
            get;
            set;
        }

        /// 校区名称
        [ORFieldMapping("CampusName")]
        [DataMember]
        public string CampusName
        {
            get;
            set;
        }

        /// 排定 ID
        [ORFieldMapping("AssignID", PrimaryKey = true)]
        [DataMember]
        public string AssignID
        {
            get;
            set;
        }

        /// 排定时间
        [ORFieldMapping("AssignTime", UtcTimeToLocal = true)]
        [SqlBehavior(DefaultExpression = "GETUTCDATE()")]
        [DataMember]
        public DateTime AssignTime
        {
            get;
            set;
        }

        /// 排定状态
        [ORFieldMapping("AssignStatus")]
        [DataMember]
        public AssignStatusDefine AssignStatus
        {
            get;
            set;
        }

        /// 数量
        [ORFieldMapping("Amount")]
        [DataMember]
        public decimal Amount
        {
            get;
            set;
        }

        [ORFieldMapping("StartTime", UtcTimeToLocal = true)]
        [DataMember]
        public DateTime StartTime
        {
            get;
            set;
        }

        [ORFieldMapping("EndTime", UtcTimeToLocal = true)]
        [DataMember]
        public DateTime EndTime
        {
            get;
            set;
        }

        /// 教师ID
        [ORFieldMapping("TeacherID")]
        [DataMember]
        public string TeacherID
        {
            get;
            set;
        }

        /// 教师姓名
        [ORFieldMapping("TeacherName")]
        [DataMember]
        public string TeacherName
        {
            get;
            set;
        }

        /// 教师岗位ID
        [ORFieldMapping("TeacherJobID")]
        [DataMember]
        public string TeacherJobID
        {
            get;
            set;
        }

        [ORFieldMapping("TeacherJobOrgID")]
        [DataMember]
        public string TeacherJobOrgID { get; set; }


        [ORFieldMapping("TeacherJobOrgName")]
        [DataMember]
        public string TeacherJobOrgName { get; set; }

        [ORFieldMapping("ISFullTimeTeacher")]
        [DataMember]
        public int? ISFullTimeTeacher { get; set; }


        /// 创建人ID
        [ORFieldMapping("CreatorID")]
        [DataMember]
        public string CreatorID
        {
            get;
            set;
        }

        /// 创建人姓名
        [ORFieldMapping("CreatorName")]
        [DataMember]
        public string CreatorName
        {
            get;
            set;
        }

        /// 创建时间  
        [ORFieldMapping("CreateTime", UtcTimeToLocal = true)]
        [SqlBehavior(BindingFlags = ClauseBindingFlags.Select | ClauseBindingFlags.Where, DefaultExpression = "GETUTCDATE()")]
        [DataMember]
        public DateTime CreateTime
        {
            get;
            set;
        }

        [ORFieldMapping("ModifierID")]
        [DataMember]
        public string ModifierID
        {
            get;
            set;
        }

        [ORFieldMapping("ModifierName")]
        [DataMember]
        public string ModifierName
        {
            get;
            set;
        }

        [ORFieldMapping("ModifyTime", UtcTimeToLocal = true)]
        [SqlBehavior(DefaultExpression = "GETUTCDATE()", ForceUseDefaultExpression = true)]
        [DataMember]
        public DateTime ModifyTime
        {
            get;set;
        }
    }

    [Serializable]
    [DataContract]
    public class AccompanyAssignCollection : EditableDataObjectCollectionBase<AccompanyAssign>
    {
    }
}