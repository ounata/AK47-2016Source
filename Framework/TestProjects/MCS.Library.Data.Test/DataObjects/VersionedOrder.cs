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
    [ORTableMapping("VersionedOrder", "VersionedOrder_Current")]
    public class VersionedOrder : IVersionDataObjectWithoutID
    {
        [ORFieldMapping("OrderID", PrimaryKey = true)]
        public string OrderID
        {
            get;
            set;
        }

        public string OrderName
        {
            get;
            set;
        }

        public Decimal Amount
        {
            get;
            set;
        }

        [SqlBehavior(BindingFlags = ClauseBindingFlags.Select | ClauseBindingFlags.Where)]
        public DateTime VersionEndTime
        {
            get;
            set;
        }

        [ORFieldMapping("VersionStartTime", PrimaryKey = true)]
        [SqlBehavior(BindingFlags = ClauseBindingFlags.All & ~ClauseBindingFlags.Update)]
        public DateTime VersionStartTime
        {
            get;
            set;
        }
    }

    public class VersionedOrderCollection : EditableDataObjectCollectionBase<VersionedOrder>
    {
    }
}
