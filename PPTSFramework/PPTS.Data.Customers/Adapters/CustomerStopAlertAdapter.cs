using PPTS.Data.Customers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Customers.Adapters
{
    public class CustomerStopAlertAdapter : CustomerAdapterBase<CustomerStopAlerts, CustomerStopAlertsCollection>
    {
        public static readonly CustomerStopAlertAdapter Instance = new CustomerStopAlertAdapter();

        public CustomerStopAlertAdapter()
        {

        }
    }
}
