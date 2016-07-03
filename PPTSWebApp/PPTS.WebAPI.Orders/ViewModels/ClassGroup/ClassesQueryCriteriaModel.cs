using MCS.Library.Data;
using MCS.Library.Data.Mapping;
using PPTS.Data.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPTS.WebAPI.Orders.ViewModels.ClassGroup
{
    /// <summary>
    /// 班组-班级 查询  条件  模型
    /// </summary>
    public class ClassesQueryCriteriaModel
    {
        #region Classes 条件
        [InConditionMapping("CampusID")]
        public string[] CampusID{ get; set; }


        [ConditionMapping("ProductCode")]
        public string ProductCode
        {
            get;
            set;
        }

        [ConditionMapping("ProductName")]
        public string ProductName
        {
            get;
            set;
        }

        [ConditionMapping("ClassName")]
        public string ClassName
        {
            get;
            set;
        }

        [InConditionMapping("Subject")]
        public string[] Subjects
        {
            get;
            set;
        }

        [InConditionMapping("Grade")]
        public string[] Grades
        {
            get;
            set;
        }

        [ConditionMapping("StartTime",Operation = ">=")]
        public DateTime StartTime
        {
            get;
            set;
        }        

        [ConditionMapping("StartTime", Operation = "<",AdjustDays =1)]
        public DateTime EndTime
        {
            get;
            set;
        }

        [InConditionMapping("ClassStatus")]
        public string[] ClassStatuses
        {
            get;
            set;
        }

        public static object ClassesAdjustConditionValueDelegate(string propertyName, object propertyValue, ref bool ignored)
        {
            object returnValue = propertyValue;

            switch (propertyName)
            {
                case "CampusID":
                case "ProductCode":
                case "ProductName":
                case "ClassName":
                case "Subjects":
                case "Grades":
                case "StartTime":
                case "EndTime":
                case "ClassStatuses":
                    break;
                default:
                    ignored = true;
                    break;
            }
            return returnValue;
        }

        public bool CheckClassesAdjustCondition() {
            return true;
        }
        #endregion

        #region ClassLessons 条件
        [ConditionMapping("TeacherName")]
        public string TeacherName { get; set; }

        [ConditionMapping("LessonStatus",op:"!=")]
        public LessonStatus LessonStatus {
            get { return LessonStatus.Deleted; } }

        public static object ClassLessonsAdjustConditionValueDelegate(string propertyName, object propertyValue, ref bool ignored)
        {
            object returnValue = propertyValue;

            switch (propertyName)
            {
                case "TeacherName":
                    break;
                default:
                    ignored = true;
                    break;
            }
            return returnValue;
        }

        public bool CheckClassLessonsAdjustCondition() {
            return !String.IsNullOrEmpty(TeacherName);
        }
        #endregion

        #region ClassLessonItems条件
        [ConditionMapping("CustomerID")]
        public string CustomerID { get; set; }

        [ConditionMapping("CustomerName")]
        public string CustomerName { get; set; }

        [ConditionMapping("CustomerCode")]
        public string CustomerCode { get; set; }

        public static object ClassLessonItemsAdjustConditionValueDelegate(string propertyName, object propertyValue, ref bool ignored)
        {
            object returnValue = propertyValue;

            switch (propertyName)
            {
                case "CustomerID":
                case "CustomerName":
                case "CustomerCode":
                    break;
                default:
                    ignored = true;
                    break;
            }
            return returnValue;
        }

        public bool CheckClassLessonItemsAdjustCondition() {
            return !String.IsNullOrEmpty(CustomerName) || !string.IsNullOrEmpty(CustomerCode)||!string.IsNullOrEmpty(CustomerID);
        }
        #endregion


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
    }
}