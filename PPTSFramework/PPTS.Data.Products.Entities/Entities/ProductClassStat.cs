using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Products.Entities
{
    [Serializable]
    [ORTableMapping("PM.ProductClassStats")]
    [DataContract]
    public class ProductClassStat
    {
        [ORFieldMapping("CampusID", PrimaryKey = true)]
        [DataMember]
        public string CampusID { set; get; }

        [ORFieldMapping("ProductID", PrimaryKey = true)]
        [DataMember]
        public string ProductID { set; get; }

        [ORFieldMapping("ClassCount")]
        [DataMember]
        public int ClassCount { set; get; }

        [ORFieldMapping("ValidClasses")]
        [DataMember]
        public int ValidClasses { set; get; }

        [ORFieldMapping("TenantCode")]
        [DataMember]
        public string TenantCode { set; get; }
    }

    public class ProductClassStatCollection : EditableDataObjectCollectionBase<ProductClassStat>
    {

    }
}
