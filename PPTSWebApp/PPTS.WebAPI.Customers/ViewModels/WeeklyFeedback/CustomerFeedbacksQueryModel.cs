using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using PPTS.Data.Common.Entities;
using PPTS.Data.Customers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PPTS.WebAPI.Customers.ViewModels.Feedback
{
    /// <summary>
    /// 学大反馈QueryModel
    /// </summary>
    [Serializable]
    public class CustomerFeedbacksQueryModel : CustomerReply
    {
        ///// <summary>
        ///// 年级
        ///// </summary>
        //[DataMember]
        //[ConstantCategory("c_codE_ABBR_CUSTOMER_GRADE")]
        //public new string Grade { set; get; }

        [DataMember]
        public string CustomerId { set; get; }

        ///// <summary>
        ///// 学员姓名
        ///// </summary>
        //[DataMember]
        //public string CustomerName { get; set; }

        /// <summary>
        /// 学员编号
        /// </summary>
        [DataMember]
        public string CustomerCode { get; set; }

        ///// <summary>
        ///// 家长姓名
        ///// </summary>
        //[DataMember]
        //public new string CampusName { get; set; }

        /// <summary>
        /// 反馈对象
        /// </summary>
       
        //[DataMember]
        //[ConstantCategory("c_codE_ABBR_Customer_ReplyObject")]
        //public string ReplyObject
        //{
        //    get;
        //    set;
        //}
        [InConditionMapping("OrgID")]
        public string[] OrgIds { get; set; }
    }

    [Serializable]
    public class CustomerFeedbacksQueryCollection : EditableDataObjectCollectionBase<CustomerFeedbacksQueryModel>
    {

    }
}