using System.Linq;
using PPTS.Data.Customers.Entities;

namespace PPTS.Data.Customers.Adapters
{
    public class CustomerAdapter : CustomerAdapterBase<Customer, CustomerCollection>
    {
        public static readonly CustomerAdapter Instance = new CustomerAdapter();

        private CustomerAdapter()
        {
        }

        /// <summary>
        /// 加载操作
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public Customer LoadByCustomerID(string customerID)
        {
            return this.Load(builder => builder.AppendItem("AccountID", customerID)).SingleOrDefault();
        }
    }
}