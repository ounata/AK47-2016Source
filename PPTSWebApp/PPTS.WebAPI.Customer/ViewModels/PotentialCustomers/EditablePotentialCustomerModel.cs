using PPTS.Data.Common.Entities;
using PPTS.Data.Customers.Entities;
using System.Collections.Generic;

namespace PPTS.WebAPI.Customer.ViewModels.PotentialCustomers
{
    public class EditablePotentialCustomerModel
    {
        public PotentialCustomerModel Customer
        {
            get;
            set;
        }

        public Parent Parent
        {
            get;
            set;
        }

        public CustomerStaffRelationCollection CustomerStaffRelation
        {
            get;
            set;
        }

        public Phone Phone
        {
            get;
            set;
        }

        public IDictionary<string, IEnumerable<BaseConstantEntity>> Dictionaries
        {
            get;
            set;
        }

        public EditablePotentialCustomerModel()
        {
            this.Dictionaries = new Dictionary<string, IEnumerable<BaseConstantEntity>>();
        }
    }
}