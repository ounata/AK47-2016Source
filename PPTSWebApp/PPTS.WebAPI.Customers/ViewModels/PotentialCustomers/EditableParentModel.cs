using System.Collections.Generic;
using MCS.Library.Data.Mapping;
using PPTS.Data.Common.Entities;
using PPTS.Data.Customers.Entities;

namespace PPTS.WebAPI.Customers.ViewModels.PotentialCustomers
{
    public class EditableParentModel
    {
        [NoMapping]
        public bool isCustomer { get { return true; } }

        public PotentialCustomerModel Customer { get; set; }

        public ParentModel Parent { get; set; }

        public CustomerParentRelation CustomerParentRelation { get; set; }

        public IDictionary<string, IEnumerable<BaseConstantEntity>> Dictionaries { get; set; }

        public EditableParentModel()
        {
            this.Dictionaries = new Dictionary<string, IEnumerable<BaseConstantEntity>>();
        }
    }
}