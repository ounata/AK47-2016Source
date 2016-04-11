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
    /// This object represents the properties and methods of a CustomerMeeting.
    /// 学情会信息表
    /// </summary>
    [Serializable]
    [ORTableMapping("CustomerMeetings")]
    [DataContract]
    public class CustomerMeeting
    {
        public CustomerMeeting()
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
        /// 学情会ID
        /// </summary>
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
        [ORFieldMapping("MeetingTime")]
        [DataMember]
        public DateTime MeetingTime
        {
            get;
            set;
        }

        /// <summary>
        /// 开会时长（分钟）
        /// </summary>
        [ORFieldMapping("MeetingDuration")]
        [DataMember]
        public decimal MeetingDuration
        {
            get;
            set;
        }

        /// <summary>
        /// 会议类型
        /// </summary>
        [ORFieldMapping("MeetingType")]
        [DataMember]
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
        [ORFieldMapping("NextMeetingTime")]
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
    public class CustomerMeetingCollection : EditableDataObjectCollectionBase<CustomerMeeting>
    {
    }
}