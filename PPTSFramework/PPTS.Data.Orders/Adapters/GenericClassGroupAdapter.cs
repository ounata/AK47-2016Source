using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Orders.Adapters
{
    public class GenericClassGroupAdapter<T, TCollection> : ClassGroupAdapterBase<T, TCollection>
        where TCollection : IList<T>, new()
    {
        public static readonly GenericClassGroupAdapter<T, TCollection> Instance = new GenericClassGroupAdapter<T, TCollection>();

        protected GenericClassGroupAdapter()
        {
        }
    }
}
