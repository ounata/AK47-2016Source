using MCS.Library.Data.Test.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Data.Test
{
    public static class OutputHelper
    {
        public static void Output(this VersionedOrder order)
        {
            Console.WriteLine("Order");
            Console.WriteLine("ID: {0}", order.OrderID);
            Console.WriteLine("Name: {0}", order.OrderName);
            Console.WriteLine("Amount: {0}", order.Amount);
            Console.WriteLine("VS: {0:yyyy-MM-dd HH:mm:ss.fff}", order.VersionStartTime);
            Console.WriteLine("VE: {0:yyyy-MM-dd HH:mm:ss.fff}", order.VersionEndTime);
        }

        public static void Output(this IEnumerable<VersionedOrderItem> orderItems)
        {
            int i = 0;

            foreach (VersionedOrderItem item in orderItems)
            {
                Console.WriteLine("Item: {0}", i);
                item.Output();
            }
        }

        public static void Output(this VersionedOrderItem orderItem)
        {
            Console.WriteLine("Order Item");
            Console.WriteLine("ID: {0}", orderItem.OrderID);
            Console.WriteLine("Item Name: {0}", orderItem.ItemName);
            Console.WriteLine("VS: {0:yyyy-MM-dd HH:mm:ss.fff}", orderItem.VersionStartTime);
            Console.WriteLine("VE: {0:yyyy-MM-dd HH:mm:ss.fff}", orderItem.VersionEndTime);
        }
    }
}
