using PPTS.Data.Customers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Customers.Adapters
{
    public class CustomerMeetingItemAdapter : CustomerMeetingItemAdapterBase<CustomerMeetingItem, CustomerMeetingItemCollection>
    {
        public new static CustomerMeetingItemAdapter Instance = new CustomerMeetingItemAdapter();
    }
}
