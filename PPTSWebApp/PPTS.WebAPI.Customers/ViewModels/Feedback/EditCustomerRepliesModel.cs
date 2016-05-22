using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PPTS.Data.Customers.Entities;
using MCS.Library.Validation;

namespace PPTS.WebAPI.Customers.ViewModels.Feedback
{
    /// <summary>
    /// 学大反馈【联系家长Model】
    /// </summary>
    public class EditCustomerRepliesModel
    {
        [ObjectValidator]
        public IList<CustomerReply> Items { set; get; }
        [ObjectValidator]
        public string CustomerId { set; get; }


    }
    /// <summary>
    /// 数据来源
    /// </summary>
    public enum ReplyFrom
    {
        /// <summary>
        /// IOS客户端
        /// </summary>
        IOS=1,
        /// <summary>
        /// ANDROID客户端
        /// </summary>
        ANDROID=2,
        /// <summary>
        /// PPTS客户端
        /// </summary>
        PPTSWEB=3
    }
    public enum Poster
    {
        XUEDA=1,
        CUSTOMER=2
    }
}