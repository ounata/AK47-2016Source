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
    /// This object represents the properties and methods of a Discount.
    /// 折扣表
    /// </summary>
    [Serializable]
    [ORTableMapping("v_Discounts")]
    [DataContract]
    public class DiscountPermissionView : Discount
    {
        public DiscountPermissionView()
        {
        }

        /// <summary>
        /// 使用者组织ID
        /// </summary>
        [ORFieldMapping("UseOrgID")]
        [DataMember]
        public string UseOrgID
        {
            get;
            set;
        }
    }

    [Serializable]
    [DataContract]
    public class DiscountPermissionViewCollection : EditableDataObjectCollectionBase<DiscountPermissionView>
    {
    }
}