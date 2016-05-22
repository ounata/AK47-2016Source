using MCS.Library.Data;
using MCS.Library.Data.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPTS.WebAPI.Customers.ViewModels.CustomerMeetings
{
    public class CustomerMeetingCriteriaModel
    {
        /// <summary>
        /// 满意度
        /// </summary>
        [InConditionMapping("Satisficing")]
        public int[] Satisfactions { set; get; }

        [InConditionMapping("Satisficing")]
        public int? Satisfaction { set; get; }
        /// <summary>
        /// 年级
        /// </summary>
        [InConditionMapping("grade")]
        public int[] Grades { set; get; }

        /// <summary>
        /// 会议开始时间
        /// </summary>
        [ConditionMapping("MeetingTime", Operation = ">=")]
        public string MeetingTimeStart { set; get; }

        /// <summary>
        /// 会议结束时间
        /// </summary>
        [ConditionMapping("MeetingTime", Operation = "<")]
        public string MeetingTimeEnd { set; get; }

        /// <summary>
        /// 下次会议开始时间
        /// </summary>
        [ConditionMapping("NextMeetingTime", Operation = ">=")]
        public string NextMeetingTimeStart { set; get; }

        /// <summary>
        /// 下次会议结束时间
        /// </summary>
        [ConditionMapping("NextMeetingTime", Operation = "<")]
        public string NextMeetingTimeEnd { set; get; }

        /// <summary>
        /// 会议类型
        /// </summary>
        [InConditionMapping("MeetingType")]
        public int[] MeetingTypes { set; get; }

        /// <summary>
        /// 会议类型
        /// </summary>
        [InConditionMapping("MeetingType")]
        public int? MeetingType { set; get; }

        [ConditionMapping("customer.CustomerId", Operation = " = ")]
        public string CustomerId { set; get; }

        /// <summary>
        /// 学员姓名
        /// </summary>
        public string CustomerName { set; get; }
        /// <summary>
        /// 学员编号
        /// </summary>
        public string CustomerCode { set; get; }
        /// <summary>
        /// 咨询师姓名
        /// </summary>
        [ConditionMapping("participants", EscapeLikeString = true, Prefix = "%", Postfix = "%", Operation = "LIKE")]
        public string ConsultantName { set; get; }
        /// <summary>
        /// 学管师姓名
        /// </summary>
        [ConditionMapping("participants", EscapeLikeString = true, Prefix = "%", Postfix = "%", Operation = "LIKE")]
        public string LeaveManageName { set; get; }
        /// <summary>
        /// 会议组织人
        /// </summary>
        [ConditionMapping("OrganizerName", EscapeLikeString = true, Prefix = "%", Postfix = "%", Operation = "LIKE")]
        public string OrganizerName { set; get; }

        [InConditionMapping("cMeetings.CampusID")]
        public string[] OrgIds { get; set; }
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