using MCS.Library.Core;
using MCS.Library.Data.Adapters;
using PPTS.Data.Customers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Customers.Adapters
{
    public class GenericCustomerAdapter<T, TCollection> : VersionedCustomerAdapterBase<T, TCollection>
        where T : Customer
        where TCollection : IList<T>, new()
    {
        public static readonly GenericCustomerAdapter<T, TCollection> Instance = new GenericCustomerAdapter<T, TCollection>();

        protected GenericCustomerAdapter()
        {
        }

        public T Load(string customerID)
        {
            return this.LoadByInBuilder(new InLoadingCondition(builder => builder.AppendItem(customerID), "CustomerID"), DateTime.MinValue).SingleOrDefault();
        }

        public void LoadInContext(string customerID, Action<T> action)
        {
            this.LoadByInBuilderInContext(new InLoadingCondition(builder => builder.AppendItem(customerID), "CustomerID"),
                collection => action(collection.SingleOrDefault()), DateTime.MinValue);
        }
    }
}
