using MCS.Library.Core;
using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using MCS.Web.MVC.Library.Models;
using PPTS.Data.Common.Authorization;
using PPTS.Data.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace PPTS.Data.Customers.Entities
{
    /// <summary>
    /// This object represents the properties and methods of a CustomerMeeting.
    /// 学情会信息表
    /// </summary>
    [Serializable]
    [ORTableMapping("CM.CustomerMeetings")]
    [DataContract]
    #region 数据范围权限(存入识别)
    [EntityAuth(RecordType = RecordType.CustomerMeeting)]
    #endregion 

    #region 数据范围权限(数据读取权限)
    [CustomerRelationScope(Name = "教学服务会管理（学员视图-教学服务会、详情查看）", Functions = "教学服务会管理（学员视图-教学服务会、详情查看）", RecordType = CustomerRecordType.Customer)]
    [OrgCustomerRelationScope(Name = "教学服务会管理（学员视图-教学服务会、详情查看）-本部门", Functions = "教学服务会管理（学员视图-教学服务会、详情查看）-本部门", RecordType = CustomerRecordType.Customer)]
    [OrgCustomerRelationScope(Name = "教学服务会管理（学员视图-教学服务会、详情查看）-本校区", Functions = "教学服务会管理（学员视图-教学服务会、详情查看）-本校区", RecordType = CustomerRecordType.Customer)]
    [OrgCustomerRelationScope(Name = "教学服务会管理（学员视图-教学服务会、详情查看）-本分公司", Functions = "教学服务会管理（学员视图-教学服务会、详情查看）-本分公司", RecordType = CustomerRecordType.Customer)]
    [OrgCustomerRelationScope(Name = "教学服务会管理（学员视图-教学服务会、详情查看）-全国", Functions = "教学服务会管理（学员视图-教学服务会、详情查看）-全国", RecordType = CustomerRecordType.Customer)]
    #endregion

    #region 数据范围权限(编辑操作权限)
    [CustomerRelationScope(Name = "新增/编辑教学服务会", Functions = "新增/编辑教学服务会", ActionType = ActionType.Edit, RecordType = CustomerRecordType.Customer)]
    #endregion
    public class CustomerMeeting : IEntityWithCreator, IEntityWithModifier
    {
        public CustomerMeeting()
        {
        }
       
        [NoMapping]
        [DataMember]
        public string ResourceId
        {
            set;
            get;
        }

        /// <summary>
        /// 附件列表
        /// </summary>
        [NoMapping]
        [DataMember]
        public MaterialModelCollection Materials
        {
            set;
            get;
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
        [CustomerFieldMapping("CustomerID")]
        [DataMember]
        public string CustomerID
        {
            get;
            set;
        }
        /// <summary>
        /// 学员姓名
        /// </summary>
        [NoMapping]
        [DataMember]
        public string CustomerName
        {
            get;
            set;
        }
        /// <summary>
        /// 学情会ID
        /// </summary>
        [KeyFieldMapping("MeetingID")]
        [ORFieldMapping("MeetingID", PrimaryKey = true)]
        [DataMember]
        public string MeetingID
        {
            get;
            set;
        }

        /// <summary>
        /// 开会时间
        /// </summary>
        [ORFieldMapping("MeetingTime", UtcTimeToLocal = true)]
        [DataMember]
        public DateTime MeetingTime
        {
            get;
            set;
        }

        /// <summary>
        /// 会议结束时间
        /// </summary>
        [ORFieldMapping("MeetingEndTime", UtcTimeToLocal = true)]
        [DataMember]
        public DateTime MeetingEndTime
        {
            get;
            set;
        }
        /// <summary>
        /// 开会时长（分钟）
        /// </summary>
        [ORFieldMapping("MeetingDuration")]
        [DataMember]
        public string MeetingDuration
        {
            get;
            set;
        }

        /// <summary>
        /// 会议类型
        /// </summary>
        [ORFieldMapping("MeetingType")]
        [DataMember]
        [ConstantCategory("C_CODE_ABBR_Customer_CRM_MainServiceMeeting")]
        public string MeetingType
        {
            get;
            set;
        }

        /// <summary>
        /// 会谈事件
        /// </summary>
        [ORFieldMapping("MeetingEvent")]
        [DataMember]
        [ConstantCategory("c_codE_ABBR_MeetingEvent")]
        public string MeetingEvent
        {
            get;
            set;
        }

        /// <summary>
        /// 家长满意度
        /// </summary>
        [ORFieldMapping("Satisficing")]
        [DataMember]
        [ConstantCategory("c_codE_Abbr_BO_Customer_Satisfaction")]
        public string Satisficing
        {
            get;
            set;
        }

        /// <summary>
        /// 会议组织者ID
        /// </summary>
        [ORFieldMapping("OrganizerID")]
        [DataMember]
        public string OrganizerID
        {
            get;
            set;
        }

        /// <summary>
        /// 会议组织者姓名
        /// </summary>
        [ORFieldMapping("OrganizerName")]
        [DataMember]
        public string OrganizerName
        {
            get;
            set;
        }

        /// <summary>
        /// 会议组织者岗位ID
        /// </summary>
        [ORFieldMapping("OrganizerJobID")]
        [DataMember]
        public string OrganizerJobID
        {
            get;
            set;
        }

        /// <summary>
        /// 会议组织者岗位名称
        /// </summary>
        [ORFieldMapping("OrganizerJobName")]
        [DataMember]
        public string OrganizerJobName
        {
            get;
            set;
        }

        /// <summary>
        /// 预计下次会议时间
        /// </summary>
        [ORFieldMapping("NextMeetingTime", UtcTimeToLocal = true)]
        [DataMember]
        public DateTime NextMeetingTime
        {
            get;
            set;
        }

        /// <summary>
        /// 会议参与人列表
        /// </summary>
        [ORFieldMapping("Participants")]
        [DataMember]
        [ConstantCategory("C_CODE_ABBR_Customer_CRM_MeetingObject")]
        public string Participants
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

        /// <summary>
        /// 会议主题代码
        /// </summary>

        [ORFieldMapping("MeetingTitle")]
        [DataMember]
        [ConstantCategory("c_codE_ABBR_MeetingTitle")]
        public string MeetingTitle { set; get; }
    }

    [Serializable]
    [DataContract]
    public class CustomerMeetingCollection : EditableDataObjectCollectionBase<CustomerMeeting>
    {
    }
}