using MCS.Library.Data.DataObjects;
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
    public class CustomerRepliesQueryModel: CustomerReply
    {
        /// <summary>
        /// 年级
        /// </summary>
        [DataMember]
        [ConstantCategory("c_codE_ABBR_CUSTOMER_GRADE")]
        public string Grade { set; get; }

        [DataMember]
        public string CustomerId { set; get; }

      

        /// <summary>
        /// 学员编号
        /// </summary>
        [DataMember]
        public string CustomerCode { get; set; }

      
        [DataMember]
        [ConstantCategory("c_codE_ABBR_Customer_ReplyType")]
        public string ReplyType
        {
            get;
            set;
        }

        /// <summary>
        /// 互动岗位呈现
        /// </summary>
        [DataMember]
        public string ReplyObject1 { set; get; }
    }

    [Serializable]
    public class CustomerRepliesQueryCollection : EditableDataObjectCollectionBase<CustomerRepliesQueryModel>
    {

    }
}