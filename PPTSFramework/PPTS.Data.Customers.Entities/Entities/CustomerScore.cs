using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using PPTS.Data.Common.Entities;
using System;
using System.Runtime.Serialization;

namespace PPTS.Data.Customers.Entities
{
    /// <summary>
    /// This object represents the properties and methods of a CustomerScore.
    /// 学员成绩表
    /// </summary>
    [Serializable]
    [ORTableMapping("CM.CustomerScores")]
    [DataContract]
    public class CustomerScore : IEntityWithCreator, IEntityWithModifier
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
        [ConstantCategory("C_Code_Abbr_BO_Customer_GradeTypeExt")]
        public string ScoreType
        {
            get;
            set;
        }

        /// <summary>
        /// 其它考试类型名称
        /// </summary>
        [ORFieldMapping("OtherScoreTypeName")]
        [DataMember]
        public string OtherScoreTypeName
        {
            get;
            set;
        }

        /// <summary>
        /// 考试年级
        /// </summary>
        [ORFieldMapping("ScoreGrade")]
        [DataMember]
        [ConstantCategory("C_CODE_ABBR_CUSTOMER_GRADE")]
        public string ScoreGrade
        {
            get;
            set;
        }

        /// <summary>
        /// 学年（例如：2015-2016）
        /// </summary>
        [ORFieldMapping("StudyYear")]
        [DataMember]
        [ConstantCategory("C_CODE_ABBR_Customer_StudyYear")]
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
        [ConstantCategory("C_CODE_ABBR_Customer_StudyTerm")]
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
        [ConstantCategory("C_CODE_ABBR_Customer_StudyStage")]
        public string StudyStage
        {
            get;
            set;
        }

        /// <summary>
        /// 班级人数
        /// </summary>
        [ORFieldMapping("ClassPeoples")]
        [DataMember]
        public int ClassPeoples
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
        [ConstantCategory("C_CODE_ABBR_Exam_Customer_Type")]
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
        [ConstantCategory("C_CODE_ABBR_Customer_AdmissionType")]
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
        [ConstantCategory("C_CODE_ABBR_Exam_Month")]
        public int ExamineMonth
        {
            get;
            set;
        }

        /// <summary>
        /// 科目成绩是否已全部添加
        /// </summary>
        [ORFieldMapping("IsAllAdded")]
        [DataMember]
        public int IsAllAdded
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
        [ORFieldMapping("CreateTime", UtcTimeToLocal = true)]
        [DataMember]
        [SqlBehavior(BindingFlags = ClauseBindingFlags.Select | ClauseBindingFlags.Where)]
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
        [ORFieldMapping("ModifyTime", UtcTimeToLocal = true)]
        [SqlBehavior(BindingFlags = ClauseBindingFlags.All & ~ClauseBindingFlags.Update, DefaultExpression = "GETUTCDATE()", ForceUseDefaultExpression = true)]
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