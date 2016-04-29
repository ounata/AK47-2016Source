using MCS.Library.Data.Adapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Orders.Adapters
{
    public class ClassGroupAdapterBase<T, TCollection> : UpdatableAndLoadableAdapterBase<T, TCollection>
        where TCollection : IList<T>, new()
    {
        protected override string GetConnectionName()
        {
            return ConnectionDefine.PPTSOrderConnectionName;
        }
    }
}
