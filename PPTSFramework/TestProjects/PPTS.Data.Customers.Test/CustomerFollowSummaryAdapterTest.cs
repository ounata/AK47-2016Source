using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MCS.Library.Core;
using PPTS.Data.Customers.Entities;
using PPTS.Data.Customers.Adapters;

namespace PPTS.Data.Customers.Test
{
    [TestClass]
    public class CustomerFollowSummaryAdapterTest
    {
        [TestMethod]
        public void SingleFollowTest()
        {
            PotentialCustomer customer = DataHelper.PreparePotentialCustomerData();

            PotentialCustomerAdapter.Instance.Update(customer);

            DBTimePointActionContext.Current.TimePoint = DateTime.MinValue;

            CustomerFollow follow = DataHelper.PrepareFollow(customer.CustomerID);

            CustomerFollowAdapter.Instance.Update(follow);

            PotentialCustomer summary = PotentialCustomerAdapter.Instance.Load(follow.CustomerID);

            Assert.IsNotNull(summary);

            follow.FillFollowSummary(summary);

            GenericPotentialCustomerAdapter<PotentialCustomer, PotentialCustomerCollection>.Instance.UpdateFollowSummary(summary);

            PotentialCustomer summaryLoaded = PotentialCustomerAdapter.Instance.Load(follow.CustomerID);

            Assert.AreEqual(1, summaryLoaded.FollowedCount);
        }
        [TestMethod]
        public void SingleFollowTestInContext()
        {
            PotentialCustomer customer = DataHelper.PreparePotentialCustomerData();

            PotentialCustomerAdapter.Instance.Update(customer);

            DBTimePointActionContext.Current.TimePoint = DateTime.MinValue;

            CustomerFollow follow = DataHelper.PrepareFollow(customer.CustomerID);

            CustomerFollowAdapter.Instance.UpdateInContext(follow);

            PotentialCustomer summary = PotentialCustomerAdapter.Instance.Load(follow.CustomerID);

            Assert.IsNotNull(summary);

            follow.FillFollowSummary(summary);

            GenericPotentialCustomerAdapter<PotentialCustomer, PotentialCustomerCollection>.Instance.UpdateFollowSummaryInContext(summary);

            GenericPotentialCustomerAdapter<PotentialCustomer, PotentialCustomerCollection>.Instance.GetDbContext().DoAction(
                context => context.ExecuteTimePointSqlInContext());

            PotentialCustomer summaryLoaded = PotentialCustomerAdapter.Instance.Load(follow.CustomerID);

            Assert.AreEqual(1, summaryLoaded.FollowedCount);
        }

    }
}
