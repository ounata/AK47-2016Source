using MCS.Library.Data.Builder;
using PPTS.Data.Customers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Customers.Adapters
{
    public class CustomerScoreItemAdapter: CustomerAdapterBase<CustomerScoreItem, CustomerScoreItemCollection>
    {
        public static readonly CustomerScoreItemAdapter Instance = new CustomerScoreItemAdapter();

        private CustomerScoreItemAdapter()
        {
        }

        public CustomerScoreItemCollection Load(string scoreID)
        {
            return this.Load(builder => builder.AppendItem("ScoreID", scoreID), order => order.AppendItem("SortNo", MCS.Library.Data.FieldSortDirection.Ascending));
        }
    }
}
