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
        /// <summary>
        /// 学员ID
        /// </summary>
        [DataMember]
        public string CustomerID
        {
            get;
            set;
        }

        /// <summary>
        /// 资产ID
        /// </summary>
        [DataMember]
        public string AssetID
        {
            get;
            set;
        }

        /// <summary>
        /// 资产编码
        /// </summary>
        [DataMember]
        public string AssetCode
        {
            get;
            set;
        }

        /// <summary>
        /// 产品ID
        /// </summary>
        [DataMember]
        public string ProductID
        {
            get;
            set;
        }

        /// <summary>
        /// 产品编码
        /// </summary>
        [DataMember]
        public string ProductCode
        {
            get;
            set;
        }

        /// <summary>
        /// 产品名称
        /// </summary>
        [DataMember]
        public string ProductName
        {
            get;
            set;
        }

        /// <summary>
        /// 年级代码
        /// </summary>
        [DataMember]
        public string Grade
        {
            get;
            set;
        }

        /// <summary>
        /// 年级名称
        /// </summary>
        [DataMember]
        public string GradeName
        {
            get;
            set;
        }

        /// <summary>
        /// 科目代码
        /// </summary>
        [DataMember]
        public string Subject
        {
            get;
            set;
        }

        /// <summary>
        /// 科目名称
        /// </summary>
        [DataMember]
        public string SubjectName
        {
            get;
            set;
        }

        /// <summary>
        /// 教室ID
        /// </summary>
        [DataMember]
        public string RoomID
        {
            get;
            set;
        }

        /// <summary>
        /// 教室编码
        /// </summary>
        [DataMember]
        public string RoomCode
        {
            get;
            set;
        }

        /// <summary>
        /// 教室名称
        /// </summary>
        [DataMember]
        public string RoomName
        {
            get;
            set;
        }

        /// <summary>
        /// 教师ID
        /// </summary>
        [DataMember]
        public string TeacherID
        {
            get;
            set;
        }

        /// <summary>
        /// 教师姓名
        /// </summary>
        [DataMember]
        public string TeacherName
        {
            get;
            set;
        }

        /// <summary>
        /// 教师岗位ID
        /// </summary>
        [DataMember]
        public string TeacherJobID
        { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [DataMember]
        public DateTime CreateTime
        {
            get;
            set;
        }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        [DataMember]
        public DateTime ModifyTime
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string TenantCode
        {
            get;
            set;
        }
        #endregion

        #region IAssignAttr
        /// <summary>
        /// 排课ID
        /// </summary>
        [DataMember]
        public string AssignID
        {
            get;
            set;
        }

        /// <summary>
        /// 排定时间
        /// </summary>
        [DataMember]
        public DateTime AssignTime
        {
            get;
            set;
        }

        /// <summary>
        /// 排课状态（排定，已上，异常，无效）
        /// </summary>
        [DataMember]
        public AssignStatusDefine AssignStatus
        {
            get;
            set;
        }

        /// <summary>
        /// 排课来源（自动【班组】，手工【一对一】，补录）
        /// </summary>
        [DataMember]
        public AssignSourceDefine AssignSource
        {
            get;
            set;
        }

        /// <summary>
        /// 异常原因
        /// </summary>
        [DataMember]
        public string AssignMemo
        {
            get;
            set;
        }

        /// <summary>
        /// 是否允许复制
        /// </summary>
        [DataMember]
        public bool CopyAllowed
        {
            get;
            set;
        }

        /// <summary>
        /// 确认状态（0-未确认，1-已确认，3-已删除，4-部分确认）
        /// </summary>
        [DataMember]
        public ConfirmStatusDefine ConfirmStatus
        {
            get;
            set;
        }

        /// <summary>
        /// 确认时间
        /// </summary>
        [DataMember]
        public DateTime ConfirmTime
        {
            get;
            set;
        }

        /// <summary>
        /// 当前操作人校区ID
        /// </summary>
        [DataMember]
        public string CampusID
        {
            get; set;
        }

        /// <summary>
        /// 当前操作人校区名称
        /// </summary>
        [DataMember]
        public string CampusName
        {
            get; set;
        }

        /// <summary>
        /// 学员编码
        /// </summary>
        [DataMember]
        public string CustomerCode
        {
            get;
            set;
        }

        /// <summary>
        /// 学员姓名
        /// </summary>
        [DataMember]
        public string CustomerName
        {
            get;
            set;
        }

        /// <summary>
        /// 咨询师ID
        /// </summary>
        [DataMember]
        public string ConsultantID
        {
            get;
            set;
        }

        /// <summary>
        /// 咨询师姓名
        /// </summary>
        [DataMember]
        public string ConsultantName
        {
            get;
            set;
        }

        /// <summary>
        /// 咨询师岗位ID
        /// </summary>
        [DataMember]
        public string ConsultantJobID
        {
            get;
            set;
        }

        /// <summary>
        /// 学管师ID
        /// </summary>
        [DataMember]
        public string EducatorID
        {
            get;
            set;
        }

        /// <summary>
        /// 学管师姓名
        /// </summary>
        [DataMember]
        public string EducatorName
        {
            get;
            set;
        }

        /// <summary>
        /// 学管师岗位ID
        /// </summary>
        [DataMember]
        public string EducatorJobID
        {
            get;
            set;
        }

        ///订单ID
        [DataMember]
        public string OrderID { get; set; }
        ///订单编码
        [DataMember]
        public string OrderNo { get; set; }

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
        ///实际排定课时数量（一对一是实际时间除以时长）
        [DataMember]
        public decimal RealAmount { get; set; }

        /// <summary>
        /// 排定单价
        /// </summary>
        [DataMember]
        public decimal AssignPrice
        {
            get;
            set;
        }

        /// <summary>
        /// 确认单价
        /// </summary>
        [DataMember]
        public decimal ConfirmPrice
        {
            get;
            set;
        }

        /// <summary>
        /// 开始时间
        /// </summary>
        [DataMember]
        public DateTime StartTime
        {
            get;
            set;
        }

        /// <summary>
        /// 结束时间
        /// </summary>
        [DataMember]
        public DateTime EndTime
        {
            get;
            set;
        }
        #endregion

        #region  IAssignConditionAttr
        /// <summary>
        /// 排课条件ID
        /// </summary>
        [DataMember]
        public string ConditionID
        {
            get;
            set;
        }

        ///// <summary>
        ///// 排课条件名称（资产编码+科目+老师+年级）
        ///// </summary>
        //[DataMember]
        //public string ConditionName
        //{
        //    get;
        //    set;
        //}

        /// <summary>
        /// 排课条件名称（学员视图排课条件名称（资产编码+科目+老师+年级））
        /// </summary>
        [DataMember]
        public string ConditionName4Customer
        {
            get;
            set;
        }

        /// <summary>
        /// 排课条件名称（教师视图排课条件名称（资产编码+科目+学员+年级））
        /// </summary>
        [DataMember]
        public string ConditionName4Teacher
        {
            get;
            set;
        }



      

        /// <summary>
		/// 课程级别代码
		/// </summary>
        [DataMember]
        public string CourseLevel
        {
            get;
            set;
        }

        /// <summary>
		/// 课程级别名称
		/// </summary>
        [DataMember]
        public string CourseLevelName
        {
            get;
            set;
        }

        /// <summary>
        /// 课次时长代码
        /// </summary>
        [DataMember]
        public string LessonDuration
        {
            get;
            set;
        }

        /// <summary>
        /// 课次时长名称
        /// </summary>
        [DataMember]
        public decimal LessonDurationValue
        {
            get;
            set;
        }

        /// <summary>
        /// 每周课次（预留）
        /// </summary>
        [DataMember]
        public decimal LessonsOfWeek
        {
            get;
            set;
        }

        /// <summary>
        /// 首次上课时间（预留）
        /// </summary>
        [DataMember]
        public DateTime FirstLessonTime
        {
            get;
            set;
        }
        #endregion

        #region IEntityWithModifier
        /// <summary>
        /// 最后修改人ID
        /// </summary>
        [DataMember]
        public string ModifierID
        {
            get;
            set;
        }

        /// <summary>
        /// 最后修改人姓名
        /// </summary>
        [DataMember]
        public string ModifierName
        {
            get;
            set;
        }
        #endregion

        #region IEntityWithCreator
        /// <summary>
        /// 创建人ID
        /// </summary>
        [DataMember]
        public string CreatorID
        {
            get;
            set;
        }

        /// <summary>
        /// 创建人姓名
        /// </summary>
        [DataMember]
        public string CreatorName
        {
            get;
            set;
        }
        #endregion
    }
}