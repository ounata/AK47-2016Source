using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using System.Runtime.Serialization;

namespace PPTS.Data.Orders.Entities
{
    [Serializable]
    [ORTableMapping("CustomerSearch")]
    [DataContract]
    public class CustomerSearch
    {
        public CustomerSearch()
        { }

        /// <summary>
		/// 学员ID
		/// </summary>
		[ORFieldMapping("CustomerID", PrimaryKey = true)]
        [DataMember]
        public string CustomerID
        {
            get;
            set;
        }

        [ORFieldMapping("CustomerCode")]
        [DataMember]
        public string CustomerCode
        {
            get;
            set;
        }

        [ORFieldMapping("CustomerName")]
        [DataMember]
        public string CustomerName
        {
            get;
            set;
        }

        /// <summary>
        /// 剩余课时数
        /// </summary>
        [ORFieldMapping("RemainAmount")]
        [DataMember]
        public decimal RemainAmount
        {
            get;
            set;
        }

    }


    [Serializable]
    [DataContract]
    public class CustomerSearchCollection : EditableDataObjectCollectionBase<CustomerSearch>
    {

    }
}
