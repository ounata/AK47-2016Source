using MCS.Library.Core;
using MCS.Library.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PPTS.Data.Customers.Adapters;
using PPTS.Data.Customers.Entities;
using System;

namespace PPTS.Data.Customers.Test
{
    [TestClass]
    public class CustomerParentRelationAdapterTest
    {
        [TestMethod]
        public void UpdateCustomerRelation()
        {
            CustomerParentRelation relation = DataHelper.PrepareCustomerRelation(UuidHelper.NewUuidString(), UuidHelper.NewUuidString());

            using (DbContext context = CustomerParentRelationAdapter.Instance.GetDbContext())
            {
                CustomerParentRelationAdapter.Instance.UpdateInContext(relation);

                context.ExecuteTimePointSqlInContext();
            }

            CustomerParentRelation loaded = CustomerParentRelationAdapter.Instance.Load(relation.CustomerID, relation.ParentID);

            relation.AreEqual(loaded);
        }
    }
}
