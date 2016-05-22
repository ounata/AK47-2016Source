using System.Linq;
using PPTS.Data.Customers.Entities;

namespace PPTS.Data.Customers.Adapters
{
    public class CustomerScoreAdapter : CustomerAdapterBase<CustomerScore, CustomerScoreCollection>
    {
        public static readonly CustomerScoreAdapter Instance = new CustomerScoreAdapter();

        private CustomerScoreAdapter()
        {
        }

        public CustomerScore Load(string scoreID)
        {
            return this.Load(builder => builder.AppendItem("ScoreID", scoreID)).FirstOrDefault();
        }

        public CustomerScoreCollection LoadByCustomerID(string customerID)
        {
            return this.Load(builder => builder.AppendItem("CustomerID", customerID));
        }
    }
}
