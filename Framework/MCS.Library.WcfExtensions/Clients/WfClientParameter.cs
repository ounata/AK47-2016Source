using MCS.Library.Data.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.WcfExtensions.Inspectors
{
    public class WfClientParameter
    {
        public string Name
        {
            get;
            internal set;
        }

        public Type Type
        {
            get;
            internal set;
        }

        public object Value
        {
            get;
            internal set;
        }
    }

    public class WfClientParameterCollection : EditableKeyedDataObjectCollectionBase<string, WfClientParameter>
    {
        protected override string GetKeyForItem(WfClientParameter item)
        {
            return item.Name;
        }
    }
}
