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
    [Serializable]
    [DataContract]
    public class BasicCustomerInfo : IBasicCustomerInfo
    {
        [DataMember]
        [ORFieldMapping("CustomerCode")]
        public string CustomerCode
        {
            get;
            set;
        }

        [DataMember]
        [ORFieldMapping("CustomerID")]
        public string CustomerID
        {
            get;
            set;
        }

        [DataMember]
        [ORFieldMapping("CustomerName")]
        public string CustomerName
        {
            get;
            set;
        }
    }

    /// <summary>
    /// 以CustomerID为Key的基本信息集合类
    /// </summary>
    [Serializable]
    [DataContract]
    public class BasicCustomerInfoCollection : SerializableEditableKeyedDataObjectCollectionBase<string, BasicCustomerInfo>
    {
        public BasicCustomerInfoCollection()
        {
        }

        protected BasicCustomerInfoCollection(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }

        protected override string GetKeyForItem(BasicCustomerInfo item)
        {
            return item.CustomerID;
        }
    }
}
