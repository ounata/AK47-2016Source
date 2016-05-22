using MCS.Library.Core;
using MCS.Library.Test.Data.Entities;
using MCS.Library.WcfExtensions.Inspectors;
using MCS.Services.Test.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.ServiceModel;

namespace MCS.Library.SOA.SysTasks.Test
{
    [TestClass]
    public class WfClientInspectorTest
    {
        [TestMethod]
        public void InspectParametersTest()
        {
            WfClientOperationInfo opInfo = WfClientInspector.InspectParameters<IOrderTransactionService>(proxy=> proxy.SetOrderStatus("1", "2", OrderStatus.Normal));

            Console.WriteLine("OP Name: {0}", opInfo.Name);

            foreach(WfClientParameter parameter in opInfo.Parameters)
            {
                Console.WriteLine("Param Name: {0}, Type: {1}, Value: {2}",
                    parameter.Name, parameter.Type, parameter.Value);
            }

            Assert.AreEqual("SetOrderStatus", opInfo.Name);
            Assert.AreEqual(3, opInfo.Parameters.Count);
        }
    }
}
