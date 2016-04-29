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
    /// This object represents the properties and methods of a PresentItem.
    /// 买赠明细表
    /// </summary>
    [Serializable]
    [ORTableMapping("PM.PresentItems")]
    [DataContract]
    public class PresentItem
    {
        public PresentItem()
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
        /// 买赠ID
        /// </summary>
        [ORFieldMapping("PresentID")]
        [DataMember]
        public string PresentID
        {
            get;
            set;
        }

        /// <summary>
        /// 买赠率
        /// </summary>
        [ORFieldMapping("PresentValue")]
        [DataMember]
        public decimal PresentValue
        {
            get;
            set;
        }

        /// <summary>
        /// 买赠标准
        /// </summary>
        [ORFieldMapping("PresentStandard")]
        [DataMember]
        public decimal PresentStandard
        {
            get;
            set;
        }
    }

    [Serializable]
    [DataContract]
    public class PresentItemCollection : EditableDataObjectCollectionBase<PresentItem>
    {
    }
}