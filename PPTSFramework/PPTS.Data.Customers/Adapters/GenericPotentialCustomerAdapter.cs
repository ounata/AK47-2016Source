using MCS.Library.Core;
using MCS.Library.Data;
using MCS.Library.Data.Adapters;
using MCS.Library.Data.DataObjects;
using PPTS.Data.Customers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Customers.Adapters
{
    public class GenericPotentialCustomerAdapter<T, TCollection> : CustomerAdapterBase<T, TCollection>
        where T : PotentialCustomer
        where TCollection : IList<T>, new()
    {
        public static readonly GenericPotentialCustomerAdapter<T, TCollection> Instance = new GenericPotentialCustomerAdapter<T, TCollection>();

        protected GenericPotentialCustomerAdapter()
        {
        }

        public T Load(string customerID)
        {
            return this.LoadByInBuilder(new InLoadingCondition(builder => builder.AppendItem(customerID), "CustomerID")).SingleOrDefault();
        }

        public void LoadInContext(string customerID, Action<T> action)
        {
            this.LoadByInBuilderInContext(new InLoadingCondition(builder => builder.AppendItem(customerID), "CustomerID"),
                collection => action(collection.SingleOrDefault()));
        }

        protected override void BeforeInnerUpdateInContext(T data, SqlContextItem sqlContext, Dictionary<string, object> context)
        {
            if (data.CustomerCode.IsNullOrEmpty())
                data.CustomerCode = Helper.GetCustomerCode("S");
        }

        protected override void BeforeInnerUpdate(T data, Dictionary<string, object> context)
        {
            if (data.CustomerCode.IsNullOrEmpty())
                data.CustomerCode = Helper.GetCustomerCode("S");
        }
    }
}
