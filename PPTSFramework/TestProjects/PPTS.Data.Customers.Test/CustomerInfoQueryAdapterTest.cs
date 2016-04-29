using MCS.Library.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PPTS.Data.Customers.Adapters;
using PPTS.Data.Customers.Entities;
using System;

namespace PPTS.Data.Customers.Test
{
    [TestClass]
    public class CustomerInfoQueryAdapterTest
    {
        [TestMethod]
        public void QueryBasicInfoByID()
        {
            PotentialCustomer pcustomer = DataHelper.PreparePotentialCustomerData();

            UpdateInContext(pcustomer);

            Customer customer = DataHelper.PrepareCustomerData();

            UpdateInContext(customer);

            BasicCustomerInfoCollection loaded = CustomerInfoQueryAdapter.Instance.LoadCustomersBasicInfoByIDs(customer.CustomerID, pcustomer.CustomerID);

            Assert.AreEqual(2, loaded.Count);
            customer.AreEqual(loaded[customer.CustomerID]);
            pcustomer.AreEqual(loaded[pcustomer.CustomerID]);
        }

        [TestMethod]
        public void QueryBasicInfoByIDInContext()
        {
            PotentialCustomer pcustomer = DataHelper.PreparePotentialCustomerData();

            UpdateInContext(pcustomer);

            Customer customer = DataHelper.PrepareCustomerData();

            UpdateInContext(customer);

            CustomerInfoQueryAdapter.Instance.LoadCustomersBasicInfoByIDsInContext((loaded) =>
            {
                Assert.AreEqual(2, loaded.Count);
                customer.AreEqual(loaded[customer.CustomerID]);
                pcustomer.AreEqual(loaded[pcustomer.CustomerID]);
            },
            customer.CustomerID,
            pcustomer.CustomerID);

            CustomerInfoQueryAdapter.Instance.GetDbContext().DoAction(context => context.ExecuteDataSetSqlInContext());
        }

        private static PotentialCustomer UpdateInContext(PotentialCustomer customer)
        {
            PotentialCustomerAdapter.Instance.UpdateInContext(customer);
            PotentialCustomerAdapter.Instance.GetDbContext().ExecuteTimePointSqlInContext();

            DBTimePointActionContext.Current.TimePoint = DateTime.MinValue;

            return PotentialCustomerAdapter.Instance.Load(customer.CustomerID);

        }

        private static Customer UpdateInContext(Customer customer)
        {
            CustomerAdapter.Instance.UpdateInContext(customer);
            CustomerAdapter.Instance.GetDbContext().ExecuteTimePointSqlInContext();

            DBTimePointActionContext.Current.TimePoint = DateTime.MinValue;

            return CustomerAdapter.Instance.Load(customer.CustomerID);
        }
    }
}
