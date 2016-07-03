using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using PPTS.Data.Common.Entities;
using PPTS.Data.Customers.Adapters;
using PPTS.WebAPI.Customers.DataSources;
using PPTS.Data.Common.Adapters;
using MCS.Library.Data;
using PPTS.Data.Customers.Entities;

namespace PPTS.WebAPI.Customers.ViewModels.PotentialCustomers
{
    public class CustomerParentsQueryResult
    {
        [NoMapping]
        public bool isCustomer { get { return true; } }
        [NoMapping]
        public string CustomerID { get; set; }
        [NoMapping]
        public string CustomerName { get; set; }
        public CustomerParentsResultModelCollection QueryResult { get; set; }
        public IDictionary<string, IEnumerable<BaseConstantEntity>> Dictionaries { get; set; }

        public static CustomerParentsQueryResult GetCustomerParents(string customerID)
        {
            CustomerParentsQueryResult result = new CustomerParentsQueryResult();
            result.CustomerID = customerID;
            result.CustomerName = PotentialCustomerAdapter.Instance.Load(result.CustomerID).CustomerName;
            result.QueryResult = CustomerParentsDataSource.Instance.LoadCustomerParents(new PageRequestParams(), customerID, new OrderByRequestItem[] { new OrderByRequestItem() { DataField = "IsPrimary", SortDirection = FieldSortDirection.Descending }, new OrderByRequestItem() { DataField = "CustomerParentRelations.CreateTime", SortDirection = FieldSortDirection.Descending } });
            result.Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(CreatablePortentialCustomerModel), typeof(Parent));
            return result;
        }
    }

    public class CustomerParentsResultModel : ParentModel
    {
        [DataMember]
        public string CustomerRole { get; set; }
        [DataMember]
        public string ParentRole { get; set; }
        [DataMember]
        public bool IsPrimary { get; set; }
    }

    [Serializable]
    public class CustomerParentsResultModelCollection : EditableDataObjectCollectionBase<CustomerParentsResultModel>
    {
    }
}