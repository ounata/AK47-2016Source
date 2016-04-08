using MCS.Library.Data.Builder;
using MCS.Library.Data.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Data.Test.DataObjects
{
    [ORTableMapping("VersionedOrderItem")]
    public class VersionedOrderItem : IVersionDataObjectWithoutID
    {
        public string OrderID
        {
            get;
            set;
        }

        public int ItemID
        {
            get;
            set;
        }

        public int ItemName
        {
            get;
            set;
        }

        public DateTime VersionEndTime
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        [SqlBehavior(BindingFlags = ClauseBindingFlags.All & ~ClauseBindingFlags.Update)]
        public DateTime VersionStartTime
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}
