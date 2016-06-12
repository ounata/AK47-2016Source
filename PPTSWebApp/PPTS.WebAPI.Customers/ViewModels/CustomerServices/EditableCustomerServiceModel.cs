using PPTS.Data.Common.Adapters;
using PPTS.Data.Common.Entities;
using PPTS.Data.Customers.Adapters;
using PPTS.Data.Customers.Entities;
using System.Collections.Generic;
using PPTS.Data.Products.Entities;
using PPTS.WebAPI.Customers.ViewModels.Students;
using MCS.Library.Principal;
using PPTS.Data.Common.Security;
using PPTS.WebAPI.Customers.ViewModels.PotentialCustomers;

namespace PPTS.WebAPI.Customers.ViewModels.CustomerServices
{
    public class EditableCustomerServiceModel
    {
        public CustomerService CustomerService
        {
            get;
            set;
        }

        public StudentModel Customer
        {
            get;
            set;
        }

        public PotentialCustomerModel PCustomer
        {
            get;
            set;
        }

        public CustomerServiceItem CustomerServiceItem
        {
            get;
            set;
        }

        public string CurrJobName
        {
            get;
            set;
        }

        public string CurrJobID
        {
            get;
            set;
        }

        public IDictionary<string, IEnumerable<BaseConstantEntity>> Dictionaries
        {
            get;
            set;
        }

        public EditableCustomerServiceModel()
        {
            this.Dictionaries = new Dictionary<string, IEnumerable<BaseConstantEntity>>();
        }

        public static EditableCustomerServiceModel Load(string id)
        {
            EditableCustomerServiceModel result = new EditableCustomerServiceModel();

            result.Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(CustomerService));
            
            CustomerServiceAdapter.Instance.LoadInContext(id, collection =>
            {
                result.CustomerService = collection.FirstOrNull();
            });

            CustomerServiceAdapter.Instance.GetDbContext().DoAction(context => context.ExecuteDataSetSqlInContext());

            GenericCustomerAdapter<StudentModel, List<StudentModel>>.Instance.LoadInContext(result.CustomerService.CustomerID, customer => result.Customer = customer);

            GenericPotentialCustomerAdapter<PotentialCustomerModel, List<PotentialCustomerModel>>.Instance.LoadInContext(result.CustomerService.CustomerID, customer => result.PCustomer = customer);

            CustomerServiceAdapter.Instance.GetDbContext().DoAction(context => context.ExecuteDataSetSqlInContext());

            result.CurrJobName = DeluxeIdentity.CurrentUser.GetCurrentJob().Name;

            result.CurrJobID = DeluxeIdentity.CurrentUser.GetCurrentJob().ID;

            return result;
        }
    }
}