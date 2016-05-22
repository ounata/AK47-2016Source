using Microsoft.VisualStudio.TestTools.UnitTesting;
using PPTS.WebAPI.Orders.Executors;
using PPTS.WebAPI.Orders.ViewModels.ClassGroup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.WebAPI.Orders.Executors.Tests
{
    [TestClass()]
    public class AddCustomerExecutorTests
    {
        [TestMethod()]
        public void AddCustomerExecutorTest()
        {
            AddCustomerModel model = new AddCustomerModel() {
                ClassID = "f383d522-3599-804c-49e5-f482b94b8ea5",
                Assets = new Asset[] {
                    new Asset() { AssetID="625546",AssetCode="OD2013122900727", CustomerID="3886647", Price=(decimal)2000.0000 },
                    new Asset() { AssetID="687939",AssetCode="OD2016022200003", CustomerID="3989980", Price=(decimal)139.5000 }
                } };


            AddCustomerExecutor executor = new AddCustomerExecutor(model);
            executor.Execute();
        }
    }
}