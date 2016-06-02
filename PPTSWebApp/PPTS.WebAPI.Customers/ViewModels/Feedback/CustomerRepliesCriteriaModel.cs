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
        [InConditionMapping("CampusID")]
        public string[] OrgIds { get; set; }

        [ConditionMapping("CustomerId")]
        public string CustomerId { set; get; }
        /// <summary>
        /// 学员编号
        /// </summary>
        public string CustomerCode { set; get; }

        /// <summary>
        /// 学员姓名
        /// </summary>
        [ConditionMapping("CustomerName", EscapeLikeString = true, Prefix = "%", Postfix = "%", Operation = "LIKE")]
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

        [InConditionMapping("ReplyObject1")]
        public string[] ReplyObjects
        {
            get;
            set;
        }

        [ConditionMapping("Poster")]
        public string Poster
        {
            set;get;
        }
        [ConditionMapping("Grade")]
        public string Grades
        {
            get;
            set;
        }
        [ConditionMapping("ReplyTime", Operation = ">=")]
        public DateTime ReplyTimeStart {
            set;get;
        }

        [ConditionMapping("ReplyTime", Operation = "<=")]
        public DateTime ReplyTimeEnd
        {
            set; get;
        }
        [NoMapping]
        public string ReplyID { set; get; }
    }
}