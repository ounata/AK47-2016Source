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
    /// This object represents the properties and methods of a CustomerScore.
    /// 学员成绩表
    /// </summary>
    [Serializable]
    [ORTableMapping("CustomerScores")]
    [DataContract]
    public class CustomerScore
    {
        public CustomerScore()
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
        /// 校区名称
        /// </summary>
        [ORFieldMapping("CampusName")]
        [DataMember]
        public string CampusName
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
        /// 成绩ID
        /// </summary>
        [ORFieldMapping("ScoreID", PrimaryKey = true)]
        [DataMember]
        public string ScoreID
        {
            get;
            set;
        }

        /// <summary>
        /// 成绩类型（部分枚举）
        /// </summary>
        [ORFieldMapping("ScoreType")]
        [DataMember]
        public string ScoreType
        {
            get;
            set;
        }

        /// <summary>
        /// 学年（例如：2015-2016）
        /// </summary>
        [ORFieldMapping("StudyYear")]
        [DataMember]
        public string StudyYear
        {
            get;
            set;
        }

        /// <summary>
        /// 学期
        /// </summary>
        [ORFieldMapping("StudyTerm")]
        [DataMember]
        public string StudyTerm
        {
            get;
            set;
        }

        /// <summary>
        /// 学段
        /// </summary>
        [ORFieldMapping("StudyStage")]
        [DataMember]
        public string StudyStage
        {
            get;
            set;
        }

        /// <summary>
        /// 卷面总分
        /// </summary>
        [ORFieldMapping("PaperScore")]
        [DataMember]
        public decimal PaperScore
        {
            get;
            set;
        }

        /// <summary>
        /// 实际总得分
        /// </summary>
        [ORFieldMapping("RealScore")]
        [DataMember]
        public decimal RealScore
        {
            get;
            set;
        }

        /// <summary>
        /// 总分年级名次
        /// </summary>
        [ORFieldMapping("GradeRank")]
        [DataMember]
        public int GradeRank
        {
            get;
            set;
        }

        /// <summary>
        /// 总分班级名次
        /// </summary>
        [ORFieldMapping("ClassRank")]
        [DataMember]
        public int ClassRank
        {
            get;
            set;
        }

        /// <summary>
        /// 家长满意度
        /// </summary>
        [ORFieldMapping("Satisficing")]
        [DataMember]
        public string Satisficing
        {
            get;
            set;
        }

        /// <summary>
        /// 学生类型代码（高考用）
        /// </summary>
        [ORFieldMapping("StudentType")]
        [DataMember]
        public string StudentType
        {
            get;
            set;
        }

        /// <summary>
        /// 录取类型代码（升学考试）
        /// </summary>
        [ORFieldMapping("AdmissionType")]
        [DataMember]
        public string AdmissionType
        {
            get;
            set;
        }

        /// <summary>
        /// 录取学校（升学考试）
        /// </summary>
        [ORFieldMapping("AdmissionSchool")]
        [DataMember]
        public string AdmissionSchool
        {
            get;
            set;
        }

        /// <summary>
        /// 是否985/211院校（高考用）
        /// </summary>
        [ORFieldMapping("IsKeyCollege")]
        [DataMember]
        public int IsKeyCollege
        {
            get;
            set;
        }

        /// <summary>
        /// 月份（月考用）
        /// </summary>
        [ORFieldMapping("ExamineMonth")]
        [DataMember]
        public int ExamineMonth
        {
            get;
            set;
        }

        /// <summary>
        /// 创建人ID
        /// </summary>
        [ORFieldMapping("CreatorID")]
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
        [DataMember]
        public DateTime CreateTime
        {
            get;
            set;
        }

        /// <summary>
        /// 最后修改人ID
        /// </summary>
        [ORFieldMapping("ModifierID")]
        [DataMember]
        public string ModifierID
        {
            get;
            set;
        }

        /// <summary>
        /// 最后修改人姓名
        /// </summary>
        [ORFieldMapping("ModifierName")]
        [DataMember]
        public string ModifierName
        {
            get;
            set;
        }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        [ORFieldMapping("ModifyTime")]
        [DataMember]
        public DateTime ModifyTime
        {
            get;
            set;
        }
    }

    [Serializable]
    [DataContract]
    public class CustomerScoreCollection : EditableDataObjectCollectionBase<CustomerScore>
    {
    }
}