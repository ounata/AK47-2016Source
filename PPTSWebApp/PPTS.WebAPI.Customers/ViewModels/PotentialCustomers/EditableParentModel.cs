using PPTS.Data.Common.Entities;
using PPTS.Data.Customers.Entities;
using System.Collections.Generic;

namespace PPTS.WebAPI.Customers.ViewModels.PotentialCustomers
{
    public class EditableParentModel
    {
        public PotentialCustomer Customer
        {
            get;
            set;
        }

        public ParentModel Parent
        {
            get;
            set;
        }

        public CustomerParentRelation CustomerParentRelation
        {
            get;
            set;
        }

        public IDictionary<string, IEnumerable<BaseConstantEntity>> Dictionaries
        {
            get;
            set;
        }

        public EditableParentModel()
        {
            this.Dictionaries = new Dictionary<string, IEnumerable<BaseConstantEntity>>();
        }
    }
}