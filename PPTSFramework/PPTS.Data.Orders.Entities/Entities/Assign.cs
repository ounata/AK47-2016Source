using MCS.Library.Core;
using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using PPTS.Data.Common.Authorization;
using PPTS.Data.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace PPTS.Data.Orders.Entities
{
    /// <summary>
    /// This object represents the properties and methods of a Assign.
    /// 排课表
    /// </summary>
    [Serializable]
    [ORTableMapping("OM.Assigns")]
    [DataContract]

    [CustomerRelationScope(Name = "客户课表列表-本人", Functions = "客户课表列表-本人", ActionType = ActionType.Read, RelationType = RelationType.Teacher, RecordType = CustomerRecordType.Customer)]
    [OrgCustomerRelationScope(Name = "客户课表列表-本校区", Functions = "客户课表列表-本校区", OrgType = Common.Authorization.OrgType.Campus, RecordType = CustomerRecordType.Customer)]
    [OrgCustomerRelationScope(Name = "客户课表列表-本分公司", Functions = "客户课表列表-本分公司", OrgType = Common.Authorization.OrgType.Branch, RecordType = CustomerRecordType.Customer)]
    [OrgCustomerRelationScope(Name = "客户课表列表-全国", Functions = "客户课表列表-全国", OrgType = Common.Authorization.OrgType.HQ, RecordType = CustomerRecordType.Customer)]
    public class Assign : IAssignAttr, IAssignShareAttr, IEntityWithCreator, IEntityWithModifier
    {
        public Assign()
        {
        }

        #region IAssignAttr

        /// 排课ID
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

        /// 排课状态（排定，已上，异常，无效）
        [ORFieldMapping("AssignStatus")]    
        [ConstantCategory("C_CODE_ABBR_Course_AssignStatus")]
        [DataMember]
        public AssignStatusDefine AssignStatus
        {
            get;
            set;
        }

        /// 排课来源（自动【班组】，手工【一对一】，补录）
        [ConstantCategory("C_CODE_ABBR_Assign_Source")]
        [ORFieldMapping("AssignSource")]
        [DataMember]
        public AssignSourceDefine AssignSource
        {
            get;
            set;
        }

        /// 异常原因
        [ORFieldMapping("AssignMemo")]
        [DataMember]
        public string AssignMemo
        {
            get;
            set;
        }

        /// 是否允许复制
        [ORFieldMapping("CopyAllowed")]
        [DataMember]
        public bool CopyAllowed
        {
            get;
            set;
        }

        /// 确认状态（0-未确认，1-已确认，3-已删除，4-部分确认）
        [ORFieldMapping("ConfirmStatus")]
        [DataMember]
        public ConfirmStatusDefine ConfirmStatus
        {
            get;
            set;
        }

        /// 确认时间
        [ORFieldMapping("ConfirmTime", UtcTimeToLocal = true)]
        [DataMember]
        public DateTime ConfirmTime
        {
            get;
            set;
        }

        /// 学员编码
        [ORFieldMapping("CustomerCode")]
        [DataMember]
        public string CustomerCode
        {
            get;
            set;
        }

        /// <summary>
        /// 学员姓名
        /// </summary>
        [ORFieldMapping("CustomerName")]
        [DataMember]
        public string CustomerName
        {
            get;
            set;
        }

        /// <summary>
        /// 咨询师ID
        /// </summary>
        [ORFieldMapping("ConsultantID")]
        [DataMember]
        public string ConsultantID
        {
            get;
            set;
        }

        /// <summary>
        /// 咨询师姓名
        /// </summary>
        [ORFieldMapping("ConsultantName")]
        [DataMember]
        public string ConsultantName
        {
            get;
            set;
        }

        /// <summary>
        /// 咨询师岗位ID
        /// </summary>
        [ORFieldMapping("ConsultantJobID")]
        [DataMember]
        public string ConsultantJobID
        {
            get;
            set;
        }

        /// <summary>
        /// 学管师ID
        /// </summary>
        [ORFieldMapping("EducatorID")]
        [DataMember]
        public string EducatorID
        {
            get;
            set;
        }

        /// <summary>
        /// 学管师姓名
        /// </summary>
        [ORFieldMapping("EducatorName")]
        [DataMember]
        public string EducatorName
        {
            get;
            set;
        }

        /// <summary>
        /// 学管师岗位ID
        /// </summary>
        [ORFieldMapping("EducatorJobID")]
        [DataMember]
        public string EducatorJobID
        {
            get;
            set;
        }


        /// 时长（分钟）【班组就是课次时长，一对一就是课时时常】
        [ORFieldMapping("DurationValue")]
        [DataMember]
        public decimal DurationValue
        {
            get;
            set;
        }

        /////订单ID
        //[ORFieldMapping("OrderID")]
        //[DataMember]
        //public string OrderID { get; set; }

        /////订单编号
        //[ORFieldMapping("OrderNo")]
        //[DataMember]
        //public string OrderNo { get; set; }

        /// 排定课时数量（一对一是实际时间除以时长，以0.5为单位向下取整，班组是1）
        [ORFieldMapping("Amount")]
        [DataMember]
        public decimal Amount
        {
            get;
            set;
        }
        
        /////实际排定课时数量（一对一是实际时间除以时长）
        //[ORFieldMapping("RealAmount")]
        //[DataMember]
        //public decimal RealAmount { get; set; }

        /// 排定课程时的单价
        [ORFieldMapping("AssignPrice")]
        [DataMember]
        public decimal AssignPrice
        {
            get;
            set;
        }

        /// 确认时价格
        [ORFieldMapping("ConfirmPrice")]
        [DataMember]
        public decimal ConfirmPrice { get; set; }

        /// 开始时间
        [ORFieldMapping("StartTime", UtcTimeToLocal = true)]
        [DataMember]
        public DateTime StartTime
        {
            get;
            set;
        }

        /// 结束时间
        [ORFieldMapping("EndTime", UtcTimeToLocal = true)]
        [DataMember]
        public DateTime EndTime
        {
            get;
            set;
        }

        [ORFieldMapping("ConfirmID")]
        [DataMember]
        public string ConfirmID { get; set; }
        #endregion

        #region IAssignShareAttr

        /// 学员ID
        [ORFieldMapping("CustomerID")]
        [DataMember]
        public string CustomerID
        {
            get;
            set;
        }

        /// 资产ID
        [ORFieldMapping("AssetID")]
        [DataMember]
        public string AssetID
        {
            get;
            set;
        }

        /// 资产编码
        [ORFieldMapping("AssetCode")]
        [DataMember]
        public string AssetCode
        {
            get;
            set;
        }

        /// 产品ID
        [ORFieldMapping("ProductID")]
        [DataMember]
        public string ProductID
        {
            get;
            set;
        }

        /// 产品编码
        [ORFieldMapping("ProductCode")]
        [DataMember]
        public string ProductCode
        {
            get;
            set;
        }

        /// 产品名称
        [ORFieldMapping("ProductName")]
        [DataMember]
        public string ProductName
        {
            get;
            set;
        }

        /// 年级代码
        [ORFieldMapping("Grade")]
        [ConstantCategory("C_CODE_ABBR_CUSTOMER_GRADE")]
        [DataMember]
        public string Grade
        {
            get;
            set;
        }

        /// 年级名称
        [ORFieldMapping("GradeName")]
        [DataMember]
        public string GradeName
        {
            get;
            set;
        }

        /// 科目代码
        [ORFieldMapping("Subject")]
        [DataMember]
        [ConstantCategory("C_CODE_ABBR_BO_Product_TeacherSubject")]
        public string Subject
        {
            get;
            set;
        }

        /// 科目名称
        [ORFieldMapping("SubjectName")]
        [DataMember]
        public string SubjectName
        {
            get;
            set;
        }

        /// 教室ID
        [ORFieldMapping("RoomID")]
        [DataMember]
        public string RoomID
        {
            get;
            set;
        }

        /// 教室编码
        [ORFieldMapping("RoomCode")]
        [DataMember]
        public string RoomCode
        {
            get;
            set;
        }

        /// 教室名称
        [ORFieldMapping("RoomName")]
        [DataMember]
        public string RoomName
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
        { get; set; }


        /// 创建时间
        [ORFieldMapping("CreateTime", UtcTimeToLocal = true)]
        [SqlBehavior(BindingFlags = ClauseBindingFlags.Select | ClauseBindingFlags.Where, DefaultExpression = "GETUTCDATE()")]
        [DataMember]
        public DateTime CreateTime
        {
            get;
            set;
        }

        /// 最后修改时间
        [ORFieldMapping("ModifyTime", UtcTimeToLocal = true)]
        [SqlBehavior(DefaultExpression = "GETUTCDATE()", ForceUseDefaultExpression = true)]
        [DataMember]
        public DateTime ModifyTime
        {
            get;
            set;
        }

        /// 
        [ORFieldMapping("TenantCode")]
        [DataMember]
        public string TenantCode
        {
            get;
            set;
        }

        [ORFieldMapping("CampusID")]
        [DataMember]
        public string CampusID
        { get; set; }

        [ORFieldMapping("CampusName")]
        [DataMember]
        public string CampusName { get; set; }

        ///教师学科组名称
        [ORFieldMapping("TeacherJobOrgName")]
        [DataMember]
        public string TeacherJobOrgName
        {
            get; set;
        }


        ///教师，全职还是兼职
        [ConstantCategory("Common_TeacherType")]
        [ORFieldMapping("IsFullTimeTeacher")]
        [DataMember]
        public int? IsFullTimeTeacher { get; set; }

        ///学员账户ID
        [ORFieldMapping("AccountID")]
        [DataMember]
        public string AccountID { get; set; }

        ///教师学科组ID
        [ORFieldMapping("TeacherJobOrgID")]
        [DataMember]
        public string TeacherJobOrgID
        {
            get; set;
        }

        [ConstantCategory("c_codE_ABBR_Product_CategoryType")]
        [ORFieldMapping("CategoryType")]
        [DataMember]
        public string CategoryType { get; set; }

        [ORFieldMapping("CategoryTypeName")]
        [DataMember]
        public string CategoryTypeName { get; set; }


        #endregion

        #region IEntityWithModifier
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
        #endregion

        #region IEntityWithCreator
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
        #endregion


        //上课时段
        [NoMapping]
        [DataMember]
        public string CourseSE
        {
            get {
                return string.Format("{0}-{1}", this.StartTime.ToString("HH:mm"), this.EndTime.ToString("HH:mm"));
            }
        }
        ///实际小时
        [NoMapping]
        [DataMember]
        public string RealTime
        {
            get {
                return this.EndTime.Subtract(this.StartTime).TotalHours.ToString("0.00");
            }
        }
    }

    [Serializable]
    [DataContract]
    public class AssignCollection : EditableDataObjectCollectionBase<Assign>
    {
    }


    public interface IAssignAttr
    {
        /// 排课ID
        string AssignID
        {
            get;
            set;
        }

        /// 排定时间
        DateTime AssignTime
        {
            get;
            set;
        }

        /// 排课状态（排定，已上，异常，无效）
        AssignStatusDefine AssignStatus
        {
            get;
            set;
        }

        /// 排课来源（自动【班组】，手工【一对一】，补录）
        AssignSourceDefine AssignSource
        {
            get;
            set;
        }

        /// 异常原因
        string AssignMemo
        {
            get;
            set;
        }

        /// 是否允许复制
        bool CopyAllowed
        {
            get;
            set;
        }

        /// 确认状态（0-未确认，1-已确认，3-已删除，4-部分确认）
        ConfirmStatusDefine ConfirmStatus
        {
            get;
            set;
        }

        string ConfirmID { get; set; }

        /// 确认时间
        DateTime ConfirmTime
        {
            get;
            set;
        }

        /// 确认时价格
        decimal ConfirmPrice { get; set; }

        /// 咨询师ID
        string ConsultantID
        {
            get;
            set;
        }

        /// 咨询师姓名
        string ConsultantName
        {
            get;
            set;
        }

        /// 咨询师岗位ID
        string ConsultantJobID
        {
            get;
            set;
        }

        /// 学管师ID
        string EducatorID
        {
            get;
            set;
        }

        /// 学管师姓名
        string EducatorName
        {
            get;
            set;
        }

        /// 学管师岗位ID
        string EducatorJobID
        {
            get;
            set;
        }

        /// 时长（分钟）【班组就是课次时长，一对一就是课时时常】
        decimal DurationValue
        {
            get;
            set;
        }
        
        /// 排定课时数量（一对一是实际时间除以时长，以0.5为单位向下取整，班组是1）
        decimal Amount
        {
            get;
            set;
        }

        /// 课程排定时的单价
        decimal AssignPrice
        {
            get;
            set;
        }

        /// 开始时间
        DateTime StartTime
        {
            get;
            set;
        }

        /// 结束时间
        DateTime EndTime
        {
            get;
            set;
        }

        ///校区ID
        string CampusID { get; set; }
        ///校区名称
        string CampusName { get; set; }
    }
}