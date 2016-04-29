using PPTS.Data.Common.Adapters;
using PPTS.Data.Common.Entities;
using PPTS.Data.Customers.Adapters;
using PPTS.Data.Customers.Entities;
using System;
using System.Linq;
using System.Collections.Generic;
using MCS.Library.Data;
using MCS.Library.Data.Adapters;

namespace PPTS.WebAPI.Customers.ViewModels.PotentialCustomers
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

        public CustomerStaffRelationCollection CustomerStaffRelations
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

        public static EditablePotentialCustomerModel Load(string id)
        {
            EditablePotentialCustomerModel result = new EditablePotentialCustomerModel();

            result.Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(PotentialCustomer));

            GenericPotentialCustomerAdapter<PotentialCustomerModel, List<PotentialCustomerModel>>.Instance.LoadInContext(id, customer => result.Customer = customer);

            GenericParentAdapter<ParentModel, List<ParentModel>>.Instance.LoadPrimaryParentInContext(id, parent => result.Parent = parent);

            CustomerStaffRelationAdapter.Instance.LoadByCustomerIDInContext(id, relations => result.CustomerStaffRelations = relations);

            PhoneAdapter.Instance.LoadByOwnerIDInContext(id, phones => result.Customer.FillFromPhones(phones));

            PhoneAdapter.Instance.GetDbContext().DoAction(context => context.ExecuteDataSetSqlInContext());

            return result;
        }
    }
}