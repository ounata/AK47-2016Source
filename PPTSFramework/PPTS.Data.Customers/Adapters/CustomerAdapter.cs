using System.Collections.Generic;
using System.Linq;
using PPTS.Data.Customers.Entities;
using PPTS.Contracts.Search.Models;
using MCS.Library.Data;

namespace PPTS.Data.Customers.Adapters
{
    public class CustomerAdapter : GenericCustomerAdapter<Customer, CustomerCollection>
    {
        public new static readonly CustomerAdapter Instance = new CustomerAdapter();

        protected override void AfterInnerUpdate(Customer data, Dictionary<string, object> context)
        {
            base.AfterInnerUpdate(data, context);
            UpdateCustomerSearchByCustomerTask.Instance.UpdateByCustomerInfoByTask(new CustomerSearchUpdateModel()
            {
                CustomerID = data.CustomerID,
                ObjectID = data.CustomerID,
                Type = CustomerSearchUpdateType.Customer
            });
        }

        protected override void BeforeInnerUpdateInContext(Customer data, SqlContextItem sqlContext, Dictionary<string, object> context)
        {
            this.GetSqlContext().AfterActions.Add(() => UpdateCustomerSearchByCustomerTask.Instance.UpdateByCustomerInfoByTask(new CustomerSearchUpdateModel()
            {
                CustomerID = data.CustomerID,
                ObjectID = data.CustomerID,
                Type = CustomerSearchUpdateType.Customer
            }));
            base.BeforeInnerUpdateInContext(data, sqlContext, context);
        }

        protected override void BeforeInnerUpdateCollectionInContext(IEnumerable<Customer> data, SqlContextItem sqlContext, Dictionary<string, object> context)
        {
            IEnumerable<CustomerSearchUpdateModel> modelCollection = data.Select(customer => new CustomerSearchUpdateModel() { CustomerID = customer.CustomerID, Type = CustomerSearchUpdateType.Customer });
            this.GetSqlContext().AfterActions.Add(() => UpdateCustomerSearchByCustomerTask.Instance.UpdateByCustomerCollectionInfoByTask(modelCollection.ToList()));
            base.BeforeInnerUpdateCollectionInContext(data, sqlContext, context);
        }

        protected override void AfterInnerUpdateCollection(IEnumerable<Customer> data, Dictionary<string, object> context)
        {
            base.AfterInnerUpdateCollection(data, context);
            IEnumerable<CustomerSearchUpdateModel> modelCollection = data.Select(customer => new CustomerSearchUpdateModel() { CustomerID = customer.CustomerID, Type = CustomerSearchUpdateType.Customer });
            UpdateCustomerSearchByCustomerTask.Instance.UpdateByCustomerCollectionInfoByTask(modelCollection.ToList());
        }

        private CustomerAdapter()
        {
        }
    }
}