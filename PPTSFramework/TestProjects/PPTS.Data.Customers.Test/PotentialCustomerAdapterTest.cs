using MCS.Library.Core;
using MCS.Library.Data;
using MCS.Library.SOA.DataObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PPTS.Data.Common;
using PPTS.Data.Customers.Adapters;
using PPTS.Data.Customers.Entities;
using System;
using System.Collections.Generic;

namespace PPTS.Data.Customers.Test
{
    [TestClass]
    public class PotentialCustomerAdapterTest
    {
        [TestMethod]
        public void UpdatePotentialCustomer()
        {
            PotentialCustomer customer = DataHelper.PreparePotentialCustomerData();

            Console.WriteLine("ID: {0}, Code: {1}", customer.CustomerID, customer.CustomerCode);

            PotentialCustomer loaded = UpdateInContext(customer);

            Assert.IsNotNull(loaded);
            loaded.Output();
            customer.AreEqual(loaded);
        }

        [TestMethod]
        public void UpdatePotentialCustomerTwice()
        {
            PotentialCustomer customer = DataHelper.PreparePotentialCustomerData();

            Console.WriteLine("ID: {0}, Code: {1}", customer.CustomerID, customer.CustomerCode);

            PotentialCustomer loaded = UpdateInContext(customer);

            loaded.CustomerName = string.Format("测试修改潜在用户{0:yyyy-MM-dd HH:mm:ss}", DateTime.Now);

            PotentialCustomer loadedAgain = UpdateInContext(loaded);

            Assert.IsNotNull(loadedAgain);
            loaded.Output();
            loaded.AreEqual(loadedAgain);
        }

        [TestMethod]
        public void LoadInheritedPotentialCustomer()
        {
            InheritedPotentialCustomer customer = DataHelper.PreparePotentialCustomerData();

            using (DbContext context = PotentialCustomerAdapter.Instance.GetDbContext())
            {
                PotentialCustomerAdapter.Instance.UpdateInContext(customer);

                context.ExecuteTimePointSqlInContext();
            }

            Console.WriteLine(customer.CustomerCode);

            InheritedPotentialCustomer loaded = GenericPotentialCustomerAdapter<InheritedPotentialCustomer, List<InheritedPotentialCustomer>>.Instance.Load(customer.CustomerID);

            Assert.IsNotNull(loaded);

            loaded.Output();
            customer.AreEqual(loaded);
        }

        private static PotentialCustomer UpdateInContext(PotentialCustomer customer)
        {
            PotentialCustomerAdapter.Instance.UpdateInContext(customer);
            PotentialCustomerAdapter.Instance.GetDbContext().ExecuteTimePointSqlInContext();

            DBTimePointActionContext.Current.TimePoint = DateTime.MinValue;

            PotentialCustomer loaded = PotentialCustomerAdapter.Instance.Load(customer.CustomerID);

            return loaded;
        }
    }
}
