using PPTS.Data.Customers.Entities;

namespace PPTS.Data.Customers.Adapters
{
    public class PotentialCustomerAdapter : GenericPotentialCustomerAdapter<PotentialCustomer, PotentialCustomerCollection>
    {
        public new static PotentialCustomerAdapter Instance = new PotentialCustomerAdapter();

        private PotentialCustomerAdapter()
        {
        }
    }
}
