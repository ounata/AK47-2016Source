using MCS.Library.Data.Adapters;
using MCS.Library.Data.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Customers.Adapters
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TCollection"></typeparam>
    public abstract class VersionedCustomerAdapterBase<T, TCollection> : VersionedObjectAdapterBase<T, TCollection>
        where T: IVersionDataObjectWithoutID
        where TCollection: IList<T>, new()
    {
        protected override string GetConnectionName()
        {
            return ConnectionDefine.PPTSCustomerConnectionName;
        }
    }
}
