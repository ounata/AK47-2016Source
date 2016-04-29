using MCS.Library.Data.DataObjects;
using PPTS.Data.Common.Entities;
using PPTS.Data.Customers.Entities;
using System;
using System.Runtime.Serialization;

namespace PPTS.WebAPI.Customers.ViewModels.CustomerMeetings
{
    [Serializable]
    public class CustomerMeetingQueryModel : CustomerMeeting
    {
        /// <summary>
        /// 学员姓名
        /// </summary>
        [DataMember]
        public string CustomerName { get; set; }

        /// <summary>
        /// 学员编号
        /// </summary>
        [DataMember]
        public string CustomerCode { get; set; }

        /// <summary>
        /// 家长姓名
        /// </summary>
        [DataMember]
        public string ParentName { get; set; }
        /// <summary>
        /// 年级
        /// </summary>
        [DataMember]
        [ConstantCategory("c_codE_ABBR_CUSTOMER_GRADE")]
        public string Grade { set; get; }

        /// <summary>
        /// 附件
        /// </summary>
        [DataMember]
        public string Attachment { set; get; }
    }

    [Serializable]
    public class CustomerMeetingQueryCollection : EditableDataObjectCollectionBase<CustomerMeetingQueryModel>
    {
    }
}