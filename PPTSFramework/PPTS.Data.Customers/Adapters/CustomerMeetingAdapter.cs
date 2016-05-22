using MCS.Library.Data.Adapters;
using PPTS.Data.Customers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Customers.Adapters
{
    public class CustomerMeetingAdapter : CustomerMeetingAdapterBase<CustomerMeeting, CustomerMeetingCollection>
    {
        public new static CustomerMeetingAdapter Instance = new CustomerMeetingAdapter();

    }
}
