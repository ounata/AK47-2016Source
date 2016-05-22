using PPTS.Data.Common.Entities;
using PPTS.Data.Customers.Adapters;
using PPTS.Data.Customers.Entities;
using System;
using System.Linq;
using System.Collections.Generic;
using MCS.Library.Data;
using MCS.Library.Data.Adapters;
using PPTS.Data.Products.Entities;
using PPTS.WebAPI.Customers.ViewModels.Students;
using MCS.Library.Principal;
using PPTS.Data.Common.Security;
using PPTS.Data.Common.Adapters;

namespace PPTS.WebAPI.Customers.ViewModels.CustomerVisits
{
    public class EditableCustomerVisitModel
    {
        public CustomerVisit CustomerVisit
        {
            get;
            set;
        }

        public StudentModel Customer
        {
            get;
            set;
        }

        public IDictionary<string, IEnumerable<BaseConstantEntity>> Dictionaries
        {
            get;
            set;
        }

        public EditableCustomerVisitModel()
        {
            this.Dictionaries = new Dictionary<string, IEnumerable<BaseConstantEntity>>();
        }

        public static EditableCustomerVisitModel Load(string id)
        {
            EditableCustomerVisitModel result = new EditableCustomerVisitModel();

            result.Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(CustomerVisit));

            CustomerVisitAdapter.Instance.LoadInContext(id, collection =>
            {
                result.CustomerVisit = collection.FirstOrNull();
            });

            CustomerVisitAdapter.Instance.GetDbContext().DoAction(context => context.ExecuteDataSetSqlInContext());

            GenericCustomerAdapter<StudentModel, List<StudentModel>>.Instance.LoadInContext(result.CustomerVisit.CustomerID, customer => result.Customer = customer);

            CustomerServiceAdapter.Instance.GetDbContext().DoAction(context => context.ExecuteDataSetSqlInContext());

            return result;
        }
    }
}