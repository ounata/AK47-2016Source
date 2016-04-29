using MCS.Library.Data.Builder;
using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Data.Test.DataObjects
{
    [ORTableMapping("VersionedOrderItem", "VersionedOrderItem_Current")]
    public class VersionedOrderItem : IVersionDataObjectWithoutID
    {
        [ORFieldMapping("OrderID", PrimaryKey = true)]
        public string OrderID
        {
            get;
            set;
        }

        [ORFieldMapping("ItemID", PrimaryKey = true)]
        public int ItemID
        {
            get;
            set;
        }

        public string ItemName
        {
            get;
            set;
        }

        [ORFieldMapping("ModifierID")]
        [SqlBehavior(BindingFlags = ClauseBindingFlags.All & ~ClauseBindingFlags.Update)]
        public string ModifierID
        {
            get;
            set;
        }

        [ORFieldMapping("ModifyTime", UtcTimeToLocal = true)]
        [SqlBehavior(BindingFlags = ClauseBindingFlags.All & ~ClauseBindingFlags.Update, DefaultExpression = "GETUTCDATE()", ForceUseDefaultExpression = true)]
        public DateTime ModifyTime
        {
            get;
            set;
        }

        [ORFieldMapping("VersionEndTime", UtcTimeToLocal = true)]
        [SqlBehavior(BindingFlags = ClauseBindingFlags.Select | ClauseBindingFlags.Where)]
        public DateTime VersionEndTime
        {
            get;
            set;
        }

        [ORFieldMapping("VersionStartTime", PrimaryKey = true, UtcTimeToLocal = true)]
        [SqlBehavior(BindingFlags = ClauseBindingFlags.All & ~ClauseBindingFlags.Update)]
        public DateTime VersionStartTime
        {
            get;
            set;
        }
    }

    public class VersionedOrderItemCollection : EditableDataObjectCollectionBase<VersionedOrderItem>
    {
    }
}
