using PPTS.Data.Common.Adapters;
using PPTS.Data.Common.Entities;
using PPTS.Data.Customers.Adapters;
using PPTS.Data.Customers.Entities;
using PPTS.WebAPI.Customers.ViewModels.PotentialCustomers;
using System.Collections.Generic;

namespace PPTS.WebAPI.Customers.ViewModels.Students
{
    public class EditableStudentModel
    {
        public StudentModel Customer
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

        public EditableStudentModel()
        {
            this.Dictionaries = new Dictionary<string, IEnumerable<BaseConstantEntity>>();
        }

        public static EditableStudentModel Load(string id)
        {
            EditableStudentModel result = new EditableStudentModel();

            result.Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(Customer));

            GenericCustomerAdapter<StudentModel, List<StudentModel>>.Instance.LoadInContext(id, customer => result.Customer = customer);

            GenericParentAdapter<ParentModel, List<ParentModel>>.Instance.LoadPrimaryParentInContext(id, parent => result.Parent = parent);

            CustomerStaffRelationAdapter.Instance.LoadByCustomerIDInContext(id, relations => result.CustomerStaffRelations = relations);

            PhoneAdapter.Instance.LoadByOwnerIDInContext(id, phones => result.Customer.FillFromPhones(phones));

            PhoneAdapter.Instance.GetDbContext().DoAction(context => context.ExecuteDataSetSqlInContext());

            return result;
        }
    }

    public class EditableStudentParentModel
    {
        public Customer Customer { get; set; }

        public ParentModel Parent { get; set; }

        public CustomerParentRelation CustomerParentRelation { get; set; }

        public IDictionary<string, IEnumerable<BaseConstantEntity>> Dictionaries { get; set; }

        public EditableStudentParentModel()
        {
            this.Dictionaries = new Dictionary<string, IEnumerable<BaseConstantEntity>>();
        }
    }
}