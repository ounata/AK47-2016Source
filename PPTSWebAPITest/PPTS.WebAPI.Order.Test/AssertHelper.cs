using Microsoft.VisualStudio.TestTools.UnitTesting;
using PPTS.Data.Orders.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.WebAPI.Orders.Test
{
    internal static class AssertHelper
    {
        public static void AreEqual(object expected, object actual)
        {
            Assert.IsNotNull(expected);
            Assert.IsNotNull(actual);

            Assert.AreEqual(expected, actual);
        }

     
    }
}
