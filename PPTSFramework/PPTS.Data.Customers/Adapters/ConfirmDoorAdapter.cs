using PPTS.Data.Customers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Customers.Adapters
{
    public class ConfirmDoorAdapter : CustomerAdapterBase<CustomerVerify, CustomerVerifyCollection>
    {
        public static readonly ConfirmDoorAdapter Instance = new ConfirmDoorAdapter();

        public ConfirmDoorAdapter()
        {

        }
    }
}
