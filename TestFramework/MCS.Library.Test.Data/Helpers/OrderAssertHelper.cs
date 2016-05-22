using MCS.Library.Test.Data.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Test.Data.Helpers
{
    public static class OrderAssertHelper
    {
        public static void AreEqual(this Order expected, Order actual)
        {
            Assert.IsNotNull(expected);
            Assert.IsNotNull(actual);

            Assert.AreEqual(expected.OrderID, actual.OrderID);
            Assert.AreEqual(expected.OrderName, actual.OrderName);
            Assert.AreEqual(expected.ProductID, actual.ProductID);
            Assert.AreEqual(expected.Quantity, actual.Quantity);
            Assert.AreEqual(expected.Status, actual.Status);
        }

        public static void AreEqual(this Repertory expected, Repertory actual)
        {
            Assert.IsNotNull(expected);
            Assert.IsNotNull(actual);

            Assert.AreEqual(expected.ProductID, actual.ProductID);
            Assert.AreEqual(expected.ProductName, actual.ProductName);
            Assert.AreEqual(expected.TotalQuantity, actual.TotalQuantity);
        }
    }
}
