using MCS.Library.Data;
using MCS.Library.Data.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPTS.WebAPI.Customers.ViewModels.Feedback
{
    public class CustomerRepliesCriteriaModel
    {
        [InConditionMapping("cReplies.CampusID")]
        public string[] OrgIds { get; set; }

        /// <summary>
        /// 学员编号
        /// </summary>
        public string CustomerCode { set; get; }

        /// <summary>
        /// 学员姓名
        /// </summary>
        [ConditionMapping("customer.CustomerName", EscapeLikeString = true, Prefix = "%", Postfix = "%", Operation = "LIKE")]
        public string CustomerName { set; get; }

        /// <summary>
        /// 员工姓名
        /// </summary>
        [ConditionMapping("ReplierName", EscapeLikeString = true, Prefix = "%", Postfix = "%", Operation = "LIKE")]
        public string ReplierName { set; get; }

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
        //[InConditionMapping("cReplies.ReplyObject")]
        //public string ReplyType
        //{
        //    get;
        //    set;
        //}

        [InConditionMapping("cReplies.ReplyObject")]
        public string[] ReplyObjects
        {
            get;
            set;
        }

        [ConditionMapping("cReplies.Poster")]
        public string Poster
        {
            set;get;
        }
        [ConditionMapping("customer.Grade")]
        public string Grades
        {
            get;
            set;
        }
        [ConditionMapping("ReplyTime", Operation = ">=")]
        public string ReplyTimeStart {
            set;get;
        }

        [ConditionMapping("ReplyTime", Operation = "<")]
        public string ReplyTimeEnd
        {
            set; get;
        }

    }
}