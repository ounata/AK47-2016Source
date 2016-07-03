using System.Collections.Generic;
using MCS.Library.Data.Mapping;
using PPTS.Data.Common.Adapters;
using PPTS.Data.Common.Entities;
using PPTS.Data.Customers.Adapters;
using PPTS.Data.Customers.Entities;

namespace PPTS.WebAPI.Customers.ViewModels.PotentialCustomers
{
    public class EditablePotentialCustomerModel
    {
        [NoMapping]
        public bool isCustomer { get { return true; } }
        
        public PotentialCustomerModel Customer { get; set; }
        
        public Parent Parent { get; set; }

        public CustomerStaffRelationCollection CustomerStaffRelations { get; set; }

        public IDictionary<string, IEnumerable<BaseConstantEntity>> Dictionaries { get; set; }

        public EditablePotentialCustomerModel()
        {
            this.Dictionaries = new Dictionary<string, IEnumerable<BaseConstantEntity>>();
        }

        public static EditablePotentialCustomerModel Load(string id)
        {
            EditablePotentialCustomerModel result = new EditablePotentialCustomerModel();

            GenericPotentialCustomerAdapter<PotentialCustomerModel, List<PotentialCustomerModel>>.Instance.LoadInContext(id, customer => result.Customer = customer);

            GenericParentAdapter<ParentModel, List<ParentModel>>.Instance.LoadPrimaryParentInContext(id, parent => result.Parent = parent);

            CustomerStaffRelationAdapter.Instance.LoadByCustomerIDInContext(id, relations => result.CustomerStaffRelations = relations);

            PhoneAdapter.Instance.LoadByOwnerIDInContext(id, phones => result.Customer.FillFromPhones(phones));
            
            PhoneAdapter.Instance.GetDbContext().DoAction(context => context.ExecuteDataSetSqlInContext());

            School school = SchoolAdapter.Instance.Load(result.Customer.SchoolID);
            result.Customer.SchoolName = school == null ? "" : school.SchoolName;

            result.Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(PotentialCustomer));

            return result;
        }
    }
}