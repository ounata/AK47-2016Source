using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Customers.Entities
{
    /// <summary>
    /// 学员、家长和电话的视图对象，对应视图为CustomerParentPhone_Current，PotentialCustomerParentPhone_Current
    /// </summary>
    [Serializable]
    [DataContract]
    [ORTableMapping("CM.PotentialCustomerParentPhone_Current")]
    public class PotentialCustomerParentPhone
    {
        [DataMember]
        public string CustomerID
        {
            get;
            set;
        }

        [DataMember]
        public string CustomerCode
        {
            get;
            set;
        }

        [DataMember]
        public string CustomerName
        {
            get;
            set;
        }

        [DataMember]
        public string Grade
        {
            get;
            set;
        }

        [DataMember]
        public string SchoolID
        {
            get;
            set;
        }

        [DataMember]
        public string ParentID
        {
            get;
            set;
        }

        [DataMember]
        public string ParentCode
        {
            get;
            set;
        }

        [DataMember]
        public string ParentName
        {
            get;
            set;
        }

        [DataMember]
        public string CustomerRole
        {
            get;
            set;
        }

        [DataMember]
        public string ParentRole
        {
            get;
            set;
        }

        [DataMember]
        public string PhoneNumber
        {
            get;
            set;
        }
    }

    [Serializable]
    [DataContract]
    public class PotentialCustomerParentPhoneCollection : EditableDataObjectCollectionBase<PotentialCustomerParentPhone>
    {
    }
}
