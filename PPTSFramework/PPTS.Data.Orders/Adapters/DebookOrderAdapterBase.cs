using MCS.Library.Data.Adapters;
using MCS.Library.Data.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Orders.Adapters
{
    public abstract class DebookOrderAdapterBase<T, TCollection> : UpdatableAndLoadableAdapterBase<T, TCollection> where TCollection : IList<T>, new()
    {
        protected override string GetConnectionName()
        {
            return ConnectionDefine.PPTSOrderConnectionName;
        }
        

    }
}
