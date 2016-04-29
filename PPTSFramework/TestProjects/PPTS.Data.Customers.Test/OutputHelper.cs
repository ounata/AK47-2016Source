using PPTS.Data.Customers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Customers.Test
{
    public static class OutputHelper
    {
        public static void Output(this PotentialCustomer potentialCustomer)
        {
            if (potentialCustomer != null)
            {
                Console.WriteLine("Customer ID: {0}", potentialCustomer.CustomerID);
                Console.WriteLine("Name: {0}", potentialCustomer.CustomerName);
                Console.WriteLine("CustomerCode: {0}", potentialCustomer.CustomerCode);
                Console.WriteLine("VS: {0:yyyy-MM-dd HH:mm:ss}", potentialCustomer.VersionStartTime);
                Console.WriteLine("VE: {0:yyyy-MM-dd HH:mm:ss}", potentialCustomer.VersionEndTime);
            }
        } 
    }
}
