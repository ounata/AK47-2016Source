using MCS.Library.Core;
using MCS.Library.Data;
using MCS.Library.Data.Adapters;
using MCS.Library.Data.Builder;
using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using PPTS.Data.Customers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Customers.Adapters
{
    public class GenericCustomerServiceAdapter<T, TCollection> 
        where T : CustomerService
        where TCollection : IList<T>, new()
    {
        public static readonly GenericCustomerServiceAdapter<T, TCollection> Instance = new GenericCustomerServiceAdapter<T, TCollection>();

        protected GenericCustomerServiceAdapter()
        {
        }

    }
}
