using MCS.Library.Data;
using MCS.Library.Data.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPTS.WebAPI.Customers.ViewModels.Feedback
{
    /// <summary>
    /// 客户反馈
    /// </summary>
    public class CustomerFeedbacksCriteriaModel
    {
        
        [InConditionMapping("feedbacks.CampusID")]
        public string[] OrgIds { get; set; }

        /// <summary>
        /// 学员编号
        /// </summary>
        public string CustomerCode { set; get; }

        /// <summary>
        /// 学员姓名
        /// </summary>
        public string CustomerName { set; get; }

        ///// <summary>
        ///// 员工姓名
        ///// </summary>
        //public string ReplierName { set; get; }

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