using MCS.Library.Data.DataObjects;
using PPTS.Data.Common.Entities;
using PPTS.Data.Customers.Entities;
using System;
using System.Runtime.Serialization;

namespace PPTS.WebAPI.Customers.ViewModels.CustomerVerifies
{
    [Serializable]
    public class CustomerVerifyQueryModel: CustomerVerify
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
        [ConstantCategory("C_CODE_ABBR_CUSTOMER_GRADE")]
        [DataMember]
        public string Grade { get; set; }

        /// <summary>
        /// 计划上门时间
        /// </summary>
        [DataMember]
        public DateTime PlanTime { get; set; }

        /// <summary>
        /// 咨询师
        /// </summary>
        [DataMember]
        public string StaffName { get; set; }

    }

    public class CustomerVerifiesQueryCollection : EditableDataObjectCollectionBase<CustomerVerifyQueryModel>
    {
        public CustomerVerifiesQueryCollection()
        {

        }
    }
}
