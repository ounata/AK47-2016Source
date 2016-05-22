using MCS.Library.Core;
using MCS.Library.Data;
using MCS.Library.Data.Adapters;
using MCS.Library.SOA.DataObjects;
using PPTS.Data.Common.Entities;
using PPTS.Data.Common.Security;
using PPTS.Data.Customers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Customers.Adapters
{
    public class CustomerTeacherRelationAdapter : VersionedCustomerAdapterBase<CustomerTeacherRelation, CustomerTeacherRelationCollection>
    {
        public static CustomerTeacherRelationAdapter Instance = new CustomerTeacherRelationAdapter();

        private CustomerTeacherRelationAdapter()
        {
        }

        public void InsertCollection(CustomerTeacherRelationCollection ctrc) {
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
    }
}
