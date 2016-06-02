using MCS.Library.Core;
using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace PPTS.Data.Products.Entities
{
    /// <summary>
    /// This object represents the properties and methods of a DiscountItem.
    /// 折扣明细表
    /// </summary>
    [Serializable]
    [ORTableMapping("[PM].DiscountItems")]
    [DataContract]
    public class DiscountItem
    {
        public DiscountItem()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        [ORFieldMapping("ItemID", PrimaryKey = true)]
        [DataMember]
        public string ItemID
        {
            get;
            set;
        }

        /// <summary>
        /// 折扣ID
        /// </summary>
        [ORFieldMapping("DiscountID")]
        [DataMember]
        public string DiscountID
        {
            get;
            set;
        }

        /// <summary>
        /// 折扣率
        /// </summary>
        [ORFieldMapping("DiscountValue")]
        [DataMember]
        public decimal DiscountValue
        {
            get;
            set;
        }

        /// <summary>
        /// 折扣标准
        /// </summary>
        [ORFieldMapping("DiscountStandard")]
        [DataMember]
        public decimal DiscountStandard
        {
            get;
            set;
        }
    }

    [Serializable]
    [DataContract]
    public class DiscountItemCollection : EditableDataObjectCollectionBase<DiscountItem>
    {
    }
}