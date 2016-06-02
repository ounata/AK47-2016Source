using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PPTS.Data.Orders.Entities;
using PPTS.Data.Common.Entities;
using PPTS.Data.Orders;
using System.Runtime.Serialization;

namespace PPTS.WebAPI.Orders.ViewModels.Assignment
{
    [Serializable]
    [DataContract]
    public class AssignSuperModel : IAssignShareAttr,IAssignAttr,IAssignConditionAttr,IEntityWithCreator,IEntityWithModifier
    {
        #region IAssignShareAttr
        
        /// 学员ID     
        [DataMember]
        public string CustomerID
        {
            get;
            set;
        }

        /// 学员编码
        [DataMember]
        public string CustomerCode
        {
            get;
            set;
        }

        /// 学员姓名
        [DataMember]
        public string CustomerName
        {
            get;
            set;
        }

        /// 资产ID
        [DataMember]
        public string AssetID
        {
            get;
            set;
        }

        /// 资产编码 
        [DataMember]
        public string AssetCode
        {
            get;
            set;
        }

        /// 产品ID
        [DataMember]
        public string ProductID
        {
            get;
            set;
        }

        /// 产品编码
        [DataMember]
        public string ProductCode
        {
            get;
            set;
        }
       
        /// 产品名称
        [DataMember]
        public string ProductName
        {
            get;
            set;
        }
    
        /// 年级代码
        [DataMember]
        public string Grade
        {
            get;
            set;
        }

        /// 年级名称
        [DataMember]
        public string GradeName
        {
            get;
            set;
        }

        /// 科目代码
        [DataMember]
        public string Subject
        {
            get;
            set;
        }

        /// 科目名称
        [DataMember]
        public string SubjectName
        {
            get;
            set;
        }

        /// 教室ID
        [DataMember]
        public string RoomID
        {
            get;
            set;
        }

        /// 教室编码
        [DataMember]
        public string RoomCode
        {
            get;
            set;
        }

        /// 教室名称
        [DataMember]
        public string RoomName
        {
            get;
            set;
        }

        /// 教师ID
        [DataMember]
        public string TeacherID
        {
            get;
            set;
        }

        /// 教师姓名
        [DataMember]
        public string TeacherName
        {
            get;
            set;
        }

        /// 教师岗位ID
        [DataMember]
        public string TeacherJobID
        { get; set; }

        /// 创建时间
        [DataMember]
        public DateTime CreateTime
        {
            get;
            set;
        }

        /// 最后修改时间
        [DataMember]
        public DateTime ModifyTime
        {
            get;
            set;
        }

        [DataMember]
        public string TenantCode
        {
            get;
            set;
        }

        ///教师学科组名称
        [DataMember]
        public string TeacherJobOrgName
        {
            get; set;
        }

        ///教师学科组ID
        [DataMember]
        public string TeacherJobOrgID
        {
            get; set;
        }

        ///教师，全职还是兼职
        [DataMember]
        public int IsFullTimeTeacher { get; set; }

        ///学员账户ID
        [DataMember]
        public string AccountID { get; set; }


        #endregion

        #region IAssignAttr

        /// 排课ID
        [DataMember]
        public string AssignID
        {
            get;
            set;
        }

        /// 排定时间
        [DataMember]
        public DateTime AssignTime
        {
            get;
            set;
        }

        /// 排课状态（排定，已上，异常，无效）
        [DataMember]
        public AssignStatusDefine AssignStatus
        {
            get;
            set;
        }

        /// 排课来源（自动【班组】，手工【一对一】，补录）
        [DataMember]
        public AssignSourceDefine AssignSource
        {
            get;
            set;
        }

        /// 异常原因
        [DataMember]
        public string AssignMemo
        {
            get;
            set;
        }

        /// 是否允许复制
        [DataMember]
        public bool CopyAllowed
        {
            get;
            set;
        }

        /// 确认状态（0-未确认，1-已确认，3-已删除，4-部分确认）
        [DataMember]
        public ConfirmStatusDefine ConfirmStatus
        {
            get;
            set;
        }

        /// 确认时间    
        [DataMember]
        public DateTime ConfirmTime
        {
            get;
            set;
        }

        /// 当前操作人校区ID
        [DataMember]
        public string CampusID
        {
            get; set;
        }

        /// 当前操作人校区名称
        [DataMember]
        public string CampusName
        {
            get; set;
        }

        /// 咨询师ID
        [DataMember]
        public string ConsultantID
        {
            get;
            set;
        }

        /// 咨询师姓名
        [DataMember]
        public string ConsultantName
        {
            get;
            set;
        }

        /// 咨询师岗位ID
        [DataMember]
        public string ConsultantJobID
        {
            get;
            set;
        }

        /// 学管师ID   
        [DataMember]
        public string EducatorID
        {
            get;
            set;
        }

        /// 学管师姓名
        [DataMember]
        public string EducatorName
        {
            get;
            set;
        }

        /// 学管师岗位ID
        [DataMember]
        public string EducatorJobID
        {
            get;
            set;
        }

        /// 时长（分钟）【课次时长】
        [DataMember]
        public decimal DurationValue
        {
            get;
            set;
        }

        /// 排定课时数量（一对一是实际时间除以时长，以0.5为单位向下取整，班组是1）
        [DataMember]
        public decimal Amount
        {
            get;
            set;
        }

        /// 排定单价      
        [DataMember]
        public decimal AssignPrice
        {
            get;
            set;
        }

        /// 确认单价    
        [DataMember]
        public decimal ConfirmPrice
        {
            get;
            set;
        }

        /// 开始时间  
        [DataMember]
        public DateTime StartTime
        {
            get;
            set;
        }

        /// 结束时间 
        [DataMember]
        public DateTime EndTime
        {
            get;
            set;
        }
        #endregion

        #region  IAssignConditionAttr

        /// 排课条件ID
        [DataMember]
        public string ConditionID
        {
            get;
            set;
        }

        /// 排课条件名称（学员视图排课条件名称（资产编码+科目+老师+年级））
        [DataMember]
        public string ConditionName4Customer
        {
            get;
            set;
        }

        /// 排课条件名称（教师视图排课条件名称（资产编码+科目+学员+年级））
        [DataMember]
        public string ConditionName4Teacher
        {
            get;
            set;
        }

		/// 课程级别代码
        [DataMember]
        public string CourseLevel
        {
            get;
            set;
        }

		/// 课程级别名称
        [DataMember]
        public string CourseLevelName
        {
            get;
            set;
        }

        /// 课次时长代码
        [DataMember]
        public string LessonDuration
        {
            get;
            set;
        }

        /// 课次时长名称
        [DataMember]
        public decimal LessonDurationValue
        {
            get;
            set;
        }

        #endregion

        #region IEntityWithModifier

        /// 最后修改人ID
        [DataMember]
        public string ModifierID
        {
            get;
            set;
        }

        /// 最后修改人姓名
        [DataMember]
        public string ModifierName
        {
            get;
            set;
        }
        #endregion

        #region IEntityWithCreator
        
        /// 创建人ID
        [DataMember]
        public string CreatorID
        {
            get;
            set;
        }

        /// 创建人姓名
        [DataMember]
        public string CreatorName
        {
            get;
            set;
        }
        #endregion
    }
}