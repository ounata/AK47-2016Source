using System;
using System.Runtime.Serialization;
using MCS.Library.Data.DataObjects;

namespace PPTS.WebAPI.Customers.ViewModels.PotentialCustomers
{
    [Serializable]
    public class PotentialCustomerSearchModel : PotentialCustomerModel
    {
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
    }

    [Serializable]
    public class PotentialCustomerSearchModelCollection : EditableDataObjectCollectionBase<PotentialCustomerSearchModel>
    {
    }
}