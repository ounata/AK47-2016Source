using System.Linq;
using PPTS.Data.Customers.Entities;

namespace PPTS.Data.Customers.Adapters
{
    public class CustomerAdapter : GenericCustomerAdapter<Customer, CustomerCollection>
    {
        public new static readonly CustomerAdapter Instance = new CustomerAdapter();

        private CustomerAdapter()
        {
        }
    }
}