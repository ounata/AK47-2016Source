using PPTS.Data.Customers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Customers.Adapters
{
    public class CustomerReplyAdapter : CustomerReplyAdapterBase<CustomerReply, CustomerReplyCollection>
    {
        public new static CustomerReplyAdapter Instance = new CustomerReplyAdapter();
    }
}
