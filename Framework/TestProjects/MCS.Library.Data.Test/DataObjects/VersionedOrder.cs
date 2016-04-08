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
    [ORTableMapping("VersionedOrder")]
    public class VersionedOrder : IVersionDataObjectWithoutID
    {
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

        [SqlBehavior(BindingFlags = ClauseBindingFlags.All & ~ClauseBindingFlags.Update)]
        public DateTime VersionEndTime
        {
            get;
            set;
        }

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
