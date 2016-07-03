using System;
using MCS.Library.Data.Mapping;
using MCS.Library.Data;

namespace PPTS.WebAPI.Customers.ViewModels.PotentialCustomers
{
    public class CustomerScoresQueryCriteriaModel
    {
        /// <summary>
        /// 机构ID
        /// </summary>
        [InConditionMapping("CampusID")]
        public string[] OrgIds { get; set; }
        /// <summary>
        /// 学年度
        /// </summary>
        [NoMapping]
        public string StudyYear { get; set; }
        /// <summary>
        /// 学年度
        /// </summary>
        [ConditionMapping("StudyYear")]
        public string _StudyYear { get { return StudyYear == "-1" ? "" : StudyYear; } set { } }

        /// <summary>
        /// 学期
        /// </summary>
        [NoMapping]
        public string StudyTerm { get; set; }
        /// <summary>
        /// 学期
        /// </summary>
        [ConditionMapping("StudyTerm")]
        public string _StudyTerm { get { return StudyTerm == "-1" ? "" : StudyTerm; } set { } }

        /// <summary>
        /// 是否在学大辅导
        /// </summary>
        [NoMapping]
        public string IsStudyHere { get; set; }
        /// <summary>
        /// 是否在学大辅导
        /// </summary>
        [ConditionMapping("IsStudyHere")]
        public string _IsStudyHere { get { return IsStudyHere == "-1" ? "" : IsStudyHere; } set { } }

        /// <summary>
        /// 家长满意度
        /// </summary>
        [NoMapping]
        public string Satisficing { get; set; }
        /// <summary>
        /// 家长满意度
        /// </summary>
        [ConditionMapping("CustomerScoreItems.Satisficing")]
        public string _Satisficing { get { return Satisficing == "-1" ? "" : Satisficing; } set { } }

        /// <summary>
        /// 考试学段
        /// </summary>
        [NoMapping]
        public string StudyStage { get; set; }
        /// <summary>
        /// 考试学段
        /// </summary>
        [ConditionMapping("StudyStage")]
        public string _StudyStage { get { return StudyStage == "-1" ? "" : StudyStage; } set { } }

        /// <summary>
        /// 考试年级
        /// </summary>
        [NoMapping]
        public string ScoreGrade { get; set; }
        /// <summary>
        /// 考试年级
        /// </summary>
        [ConditionMapping("ScoreGrade")]
        public string _ScoreGrade { get { return ScoreGrade == "-1" ? "" : ScoreGrade; } set { } }

        /// <summary>
        /// 考试科目
        /// </summary>
        [NoMapping]
        public string Subject { get; set; }
        /// <summary>
        /// 考试年级
        /// </summary>
        [ConditionMapping("Subject")]
        public string _Subject { get { return Subject == "-1" ? "" : Subject; } set { } }
        
        /// <summary>
        /// 考试类型
        /// </summary>
        [NoMapping]
        public string ScoreType { get; set; }
        /// <summary>
        /// 考试类型
        /// </summary>
        [ConditionMapping("ScoreType")]
        public string _ScoreType { get { return ScoreType == "-1" ? "" : ScoreType; } set { } }

        /// <summary>
        /// 考试类型-其它
        /// </summary>
        [ConditionMapping("OtherScoreTypeName", EscapeLikeString = true, Prefix = "%", Postfix = "%", Operation = "LIKE")]
        public string OtherScoreTypeName { get; set; }

        /// <summary>
        /// 录取院校类型
        /// </summary>
        [NoMapping]
        public string AdmissionType { get; set; }
        /// <summary>
        /// 录取院校类型
        /// </summary>
        [ConditionMapping("AdmissionType")]
        public string _AdmissionType { get { return AdmissionType == "-1" ? "" : AdmissionType; } set { } }

        /// <summary>
        /// 学员类别
        /// </summary>
        [NoMapping]
        public string StudentType { get; set; }
        /// <summary>
        /// 学员类别
        /// </summary>
        [ConditionMapping("StudentType")]
        public string _StudentType { get { return StudentType == "-1" ? "" : StudentType; } set { } }

        /// <summary>
        /// 成绩范围-最低成绩
        /// </summary>
        [ConditionMapping("CustomerScoreItems.PaperScore", Operation = ">=")]
        public decimal MinPaperScore { get; set; }

        /// <summary>
        /// 成绩范围-最高成绩
        /// </summary>
        [ConditionMapping("CustomerScoreItems.PaperScore", Operation = "<=")]
        public decimal MaxPaperScore { get; set; }

        /// <summary>
        /// 学生ID
        /// </summary>
        [ConditionMapping("CustomerID")]
        public string CustomerID { get; set; }

        /// <summary>
        /// 学生姓名
        /// </summary>
        [NoMapping]
        public string CustomerName { get; set; }

        /// <summary>
        /// 教师姓名
        /// </summary>
        [ConditionMapping("TeacherName", EscapeLikeString = true, Prefix = "%", Postfix = "%", Operation = "LIKE")]
        public string TeacherName { get; set; }
        /// <summary>
        /// 教师OA
        /// </summary>
        [NoMapping]
        public string TeacherOA { get; set; }

        /// <summary>
        /// 岗位名称
        /// </summary>
        [NoMapping]
        public string StaffJobType { get; set; }

        /// <summary>
        /// 咨询师姓名
        /// </summary>
        [NoMapping]
        public string ConsultantName { get; set; }
        /// <summary>
        /// 学管师姓名
        /// </summary>
        [NoMapping]
        public string EducatorName { get; set; }

        /// <summary>
        /// 员工OA
        /// </summary>
        [NoMapping]
        public string StaffOA { get; set; }

        [NoMapping]
        public PageRequestParams PageParams
        {
            get;
            set;
        }

        [NoMapping]
        public OrderByRequestItem[] OrderBy
        {
            get;
            set;
        }

        public static object AdjustConditionValueDelegate_CustomerScore(string propertyName, object propertyValue, ref bool ignored)
        {
            object result = propertyValue;
            switch (propertyName)
            {
                case "OrgIds":
                case "StudyYear":
                case "StudyStage":
                case "ScoreGrade":
                case "ScoreType":
                case "AdmissionType":
                case "StudentType":
                case "TeacherName":
                case "OtherScoreTypeName":
                    if (result.ToString() == "-1")
                        ignored = true;
                    break;
                case "CustomerID":
                    break;
                default:
                    ignored = true;
                    break;
            }
            return result;
        }

        public static object AdjustConditionValueDelegate_CustomerScoreItems(string propertyName, object propertyValue, ref bool ignored)
        {
            object result = propertyValue;
            switch (propertyName)
            {
                case "IsStudyHere":
                case "Satisficing":
                case "Subject":
                    if (result.ToString() == "-1")
                        ignored = true;
                    break;
                case "MinPaperScore":
                case "MaxPaperScore":
                    break;
                default:
                    ignored = true;
                    break;
            }
            return result;
        }

        public static object AdjustConditionValueDelegate_StaffRelations(string propertyName, object propertyValue, ref bool ignored)
        {
            object result = propertyValue;
            switch (propertyName)
            {
                case "StaffName":
                    break;
                default:
                    ignored = true;
                    break;
            }
            return result;
        }

        public static object AdjustConditionValueDelegate_Customers(string propertyName, object propertyValue, ref bool ignored)
        {
            object result = propertyValue;
            switch (propertyName)
            {
                case "CustomerName":
                    break;
                default:
                    ignored = true;
                    break;
            }
            return result;
        }

        public bool CheckCondition_Customers()
        {
            return String.IsNullOrEmpty(CustomerName);
        }
    }
}