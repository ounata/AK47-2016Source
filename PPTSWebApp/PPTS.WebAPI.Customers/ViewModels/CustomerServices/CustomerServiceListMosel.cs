using System;
using System.Runtime.Serialization;
using MCS.Library.Data.DataObjects;
using PPTS.Data.Customers.Entities;

namespace PPTS.WebAPI.Customers.ViewModels.CustomerServices
{
    [Serializable]
    public class CustomerServiceModel : CustomerService
    {
        /// <summary>
        /// 学员姓名
        /// </summary>
        [DataMember]
        public string CustomerName { get; set; }

        /// <summary>
        /// 家长姓名
        /// </summary>
        [DataMember]
        public string ParentName { get; set; }

        /// <summary>
        /// 当前年级
        /// </summary>
        [DataMember]
        public string Grade { get; set; }

        /// <summary>
        /// 校区反馈
        /// </summary>
        [DataMember]
        public string SchoolMemo { get; set; }

        /// <summary>
        /// 录音状态
        /// </summary>
        [DataMember]
        public string VoiceStatus { get; set; }
    }

    [Serializable]
    public class CustomerServiceModelCollection : EditableDataObjectCollectionBase<CustomerServiceModel>
    {
        public CustomerServiceModelCollection()
        {

        }
    }
}