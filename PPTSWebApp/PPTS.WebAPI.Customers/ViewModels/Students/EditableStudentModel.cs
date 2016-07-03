using System.Collections.Generic;
using PPTS.Data.Common.Adapters;
using PPTS.Data.Common.Entities;
using PPTS.Data.Customers.Adapters;
using PPTS.Data.Customers.Entities;
using PPTS.WebAPI.Customers.ViewModels.PotentialCustomers;

namespace PPTS.WebAPI.Customers.ViewModels.Students
{
    public class EditableStudentModel
    {
        public StudentModel Customer { get; set; }

        public Parent Parent { get; set; }

        public CustomerStaffRelationCollection CustomerStaffRelations { get; set; }

        public IDictionary<string, IEnumerable<BaseConstantEntity>> Dictionaries { get; set; }

        public EditableStudentModel()
        {
            this.Dictionaries = new Dictionary<string, IEnumerable<BaseConstantEntity>>();
        }

        public static EditableStudentModel Load(string id)
        {
            EditableStudentModel result = new EditableStudentModel();

            GenericCustomerAdapter<StudentModel, List<StudentModel>>.Instance.LoadInContext(id, customer => result.Customer = customer);

            GenericParentAdapter<ParentModel, List<ParentModel>>.Instance.LoadPrimaryParentInContext(id, parent => result.Parent = parent);

            CustomerStaffRelationAdapter.Instance.LoadByCustomerIDInContext(id, relations => result.CustomerStaffRelations = relations);

            PhoneAdapter.Instance.LoadByOwnerIDInContext(id, phones => result.Customer.FillFromPhones(phones));

            CustomerTeacherRelationAdapter.Instance.LoadByCustomerIDInContext(id,
                relations =>
                {
                    if (relations != null && relations.Count > 0)
                    {
                        relations.ForEach(item => result.Customer.BelongTeacherNames += item.TeacherName + ",");
                        result.Customer.BelongTeacherNames = result.Customer.BelongTeacherNames.TrimEnd(new char[] { ',' });
                    }
                });

            PhoneAdapter.Instance.GetDbContext().DoAction(context => context.ExecuteDataSetSqlInContext());

            School school = SchoolAdapter.Instance.Load(result.Customer.SchoolID);
            result.Customer.SchoolName = school == null ? "" : school.SchoolName;

            result.Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(Customer));

            return result;
        }
    }

}