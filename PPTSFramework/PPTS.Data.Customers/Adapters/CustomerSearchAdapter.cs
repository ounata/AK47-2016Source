using MCS.Library.Data.Adapters;
using PPTS.Data.Customers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Customers.Adapters
{
    public class CustomerSearchAdapter : SearchAdapterBase<CustomerSearch, CustomerSearchCollection>
    {
        public static readonly CustomerSearchAdapter Instance = new CustomerSearchAdapter();

        private CustomerSearchAdapter()
        {
        }

        /// <summary>
        /// 加载操作
        /// </summary>
        /// <param name="customerid"></param>
        /// <returns></returns>
        public CustomerSearch Load(string customerid)
        {
            return this.Load(builder => builder.AppendItem("CustomerID", customerid)).SingleOrDefault();
        }

        protected override string GetConnectionName()
        {
            return ConnectionDefine.PPTSSearchConnectionName;
        }
    }
}
