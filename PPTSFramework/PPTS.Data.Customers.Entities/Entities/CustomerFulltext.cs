using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using PPTS.Data.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Customers.Entities
{
    [Serializable]
    [ORTableMapping("CM.CustomerFulltextInfo")]
    [DataContract]
    public class CustomerFulltextInfo: IEntityWithCreator
    {
        public static readonly string PotentialCustomersType = "PotentialCustomers";
        public static readonly string CustomersType = "Customers";
        public static readonly string ParentsType = "Parents";

        [ORFieldMapping("OwnerID", PrimaryKey = true)]
        [DataMember]
        public string OwnerID
        {
            get;
            set;
        }

        [ORFieldMapping("OwnerType")]
        [DataMember]
        public string OwnerType
        {
            get;
            set;
        }

        [ORFieldMapping("CustomerSearchContent")]
        [DataMember]
        public string CustomerSearchContent
        {
            get;
            set;
        }

        [ORFieldMapping("ParentSearchContent")]
        [DataMember]
        public string ParentSearchContent
        {
            get;
            set;
        }

        [ORFieldMapping("CreatorID")]
        [DataMember]
        public string CreatorID
        {
            get;
            set;
        }

        [ORFieldMapping("CreatorName")]
        [DataMember]
        public string CreatorName
        {
            get;
            set;
        }

        [ORFieldMapping("CreateTime", UtcTimeToLocal = true)]
        [SqlBehavior(BindingFlags = ClauseBindingFlags.Select | ClauseBindingFlags.Where)]
        [DataMember]
        public DateTime CreateTime
        {
            get;
            set;
        }

        [ORFieldMapping("CustomerStatus")]
        public CustomerStatus Status
        {
            get;
            set;
        }

        [ORFieldMapping("TenantCode")]
        [DataMember]
        public string TenantCode
        {
            get;
            set;
        }

        /// <summary>
        /// 创建对象
        /// </summary>
        /// <param name="ownerID"></param>
        /// <param name="ownerType"></param>
        /// <param name="status">潜客或者学员的状态。对于家长无效</param>
        /// <returns></returns>
        public static CustomerFulltextInfo Create(string ownerID, string ownerType, CustomerStatus status = CustomerStatus.NotConfirmed)
        {
            CustomerFulltextInfo result = new CustomerFulltextInfo()
            {
                OwnerID = ownerID,
                OwnerType = ownerType,
                Status = status
            };

            return result;
        }
    }

    [Serializable]
    [DataContract]
    public class CustomerFulltextInfoCollection : EditableDataObjectCollectionBase<CustomerFulltextInfo>
    {

    }
}
