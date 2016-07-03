using System;
using System.Runtime.Serialization;
using MCS.Library.Data.DataObjects;
using PPTS.Data.Customers.Entities;

namespace PPTS.WebAPI.Customers.ViewModels.PotentialCustomers
{
    [Serializable]
    public class PotentialCustomerSearchModel : PotentialCustomerModel
    {
        /// <summary>
        /// 家长姓名
        /// </summary>
        [DataMember]
        public string ParentName { get; set; }

        /// <summary>
        /// 当前归属咨询师
        /// </summary>
        [DataMember]
        public string ConsultantStaff { get; set; }

        /// <summary>
        /// 当前归属市场专员
        /// </summary>
        [DataMember]
        public string MarketStaff { get; set; }

        /// <summary>
        /// 咨询师
        /// </summary>
        [DataMember]
        public CustomerStaffRelation Consultant { get; set; }

        /// <summary>
        /// 市场专员
        /// </summary>
        [DataMember]
        public CustomerStaffRelation Market { get; set; }

        /// <summary>
        /// 建档人岗位
        /// </summary>
        [DataMember]
        public string CreateJobName { get; set; }

    }

    [Serializable]
    public class PotentialCustomerSearchModelCollection : EditableDataObjectCollectionBase<PotentialCustomerSearchModel>
    {
    }
}