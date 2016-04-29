using MCS.Library.Core;
using MCS.Library.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PPTS.Data.Customers.Adapters;
using PPTS.Data.Customers.Entities;
using System;

namespace PPTS.Data.Customers.Test
{
    [TestClass]
    public class CustomerFulltextInfoAdapterTest
    {
        [TestMethod]
        public void UpdateCustomerFulltextInfo()
        {
            CustomerFulltextInfo info = PrepareData();

            CustomerFulltextInfoAdapter.Instance.UpdateInContext(info);

            CustomerFulltextInfoAdapter.Instance.GetDbContext().DoAction(context => context.ExecuteScalarSqlInContext());

            CustomerFulltextInfo loaded = CustomerFulltextInfoAdapter.Instance.LoadByOwnerID(info.OwnerID);

            Assert.IsNotNull(loaded);

            Assert.AreEqual(info.OwnerID, loaded.OwnerID);
            Assert.AreEqual(info.OwnerType, loaded.OwnerType);
            Assert.AreEqual(info.CustomerSearchContent, loaded.CustomerSearchContent);
            Assert.AreEqual(info.ParentSearchContent, loaded.ParentSearchContent);
        }

        [TestMethod]
        public void OnlyUpdateCustomerFulltextInfo()
        {
            CustomerFulltextInfo info = PrepareData();

            CustomerFulltextInfoAdapter.Instance.UpdateCustomerSearchContentInContext(info);

            CustomerFulltextInfoAdapter.Instance.GetDbContext().DoAction(context => context.ExecuteScalarSqlInContext());

            CustomerFulltextInfo loaded = CustomerFulltextInfoAdapter.Instance.LoadByOwnerID(info.OwnerID);

            Assert.IsNotNull(loaded);

            Assert.AreEqual(info.OwnerID, loaded.OwnerID);
            Assert.AreEqual(info.OwnerType, loaded.OwnerType);
            Assert.AreEqual(info.CustomerSearchContent, loaded.CustomerSearchContent);
            Assert.AreNotEqual(info.ParentSearchContent, loaded.ParentSearchContent);
        }

        [TestMethod]
        public void OnlyUpdateParentFulltextInfo()
        {
            CustomerFulltextInfo info = PrepareData();

            CustomerFulltextInfoAdapter.Instance.UpdateParentSearchContentInContext(info);

            CustomerFulltextInfoAdapter.Instance.GetDbContext().DoAction(context => context.ExecuteScalarSqlInContext());

            CustomerFulltextInfo loaded = CustomerFulltextInfoAdapter.Instance.LoadByOwnerID(info.OwnerID);

            Assert.IsNotNull(loaded);

            Assert.AreEqual(info.OwnerID, loaded.OwnerID);
            Assert.AreEqual(info.OwnerType, loaded.OwnerType);
            Assert.AreNotEqual(info.CustomerSearchContent, loaded.CustomerSearchContent);
            Assert.AreEqual(info.ParentSearchContent, loaded.ParentSearchContent);
        }

        private static CustomerFulltextInfo PrepareData()
        {
            CustomerFulltextInfo info = new CustomerFulltextInfo() { OwnerID = UuidHelper.NewUuidString() };

            info.CustomerSearchContent = "何明宇 010-59178008";
            info.ParentSearchContent = "沈峥 18901026477";
            info.OwnerType = CustomerFulltextInfo.PotentialCustomersType;

            return info;
        }
    }
}
