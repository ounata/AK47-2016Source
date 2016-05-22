using MCS.Library.Data;
using System.Collections.Generic;
using PPTS.Data.Common.Entities;
using PPTS.Data.Customers.Entities;

namespace PPTS.WebAPI.Customers.ViewModels.PotentialCustomers
{
    public class CustomerParentsQueryResult
    {
        public ParentModelCollection Parents
        {
            get;
            set;
        }

        public CustomerParentRelationCollection Relations
        {
            get;
            set;
        }

        public IDictionary<string, IEnumerable<BaseConstantEntity>> Dictionaries
        {
            get;
            set;
        }
    }
}