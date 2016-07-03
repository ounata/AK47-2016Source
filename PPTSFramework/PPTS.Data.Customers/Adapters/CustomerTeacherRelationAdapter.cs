using System;
using System.Linq;
using MCS.Library.Core;
using MCS.Library.Data;
using MCS.Library.Data.Adapters;
using PPTS.Data.Common.Security;
using PPTS.Data.Customers.Entities;

namespace PPTS.Data.Customers.Adapters
{
    public class CustomerTeacherRelationAdapter : VersionedCustomerAdapterBase<CustomerTeacherRelation, CustomerTeacherRelationCollection>
    {
        public static CustomerTeacherRelationAdapter Instance = new CustomerTeacherRelationAdapter();

        private CustomerTeacherRelationAdapter()
        {
        }

        public void InsertCollection(CustomerTeacherRelationCollection ctrc)
        {
            using (DbContext context = GetDbContext())
            {

                foreach (var item in ctrc)
                {
                    item.ID = UuidHelper.NewUuidString();
                    item.FillCreator();
                    UpdateInContext(item);
                }

                context.ExecuteTimePointSqlInContext();
            }
        }

        public void LoadByCustomerIDInContext(string customerID, Action<CustomerTeacherRelationCollection> action)
        {
            this.LoadInContext(new WhereLoadingCondition(builder => builder.AppendItem("CustomerID", customerID)), action, DateTime.MinValue);
        }
    }
}
