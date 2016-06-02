using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Products.Adapters
{
    public class GenericDiscountAdapter<T, TCollection> : ProductAdapterBase<T, TCollection>
        where TCollection : IList<T>, new()
    {
        public static readonly GenericDiscountAdapter<T, TCollection> Instance = new GenericDiscountAdapter<T, TCollection>();
    }
    }
