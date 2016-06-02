using MCS.Library.Data.Adapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Customers.Adapters
{
    public class CustomerTransferResourcesAdapterBase<T, TCollection> : UpdatableAndLoadableAdapterBase<T, TCollection> where TCollection : IList<T>, new()
    {
        public readonly static CustomerTransferResourcesAdapterBase<T, TCollection> Instance = new CustomerTransferResourcesAdapterBase<T, TCollection>();
        protected override string GetConnectionName()
        {
            return ConnectionDefine.PPTSCustomerConnectionName;
        }

        public T Load(string transferID)
        {
            return this.LoadByInBuilder(new InLoadingCondition(builder => builder.AppendItem(transferID), "TransferID")).SingleOrDefault();
        }
    }
}
