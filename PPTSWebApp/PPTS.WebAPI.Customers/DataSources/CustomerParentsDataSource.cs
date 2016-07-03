using System.Collections.Generic;
using MCS.Library.Data;
using MCS.Library.Data.DataObjects;
using PPTS.Data.Customers.DataSources;
using PPTS.WebAPI.Customers.ViewModels.PotentialCustomers;
using PPTS.Data.Customers.Adapters;
using PPTS.Data.Customers.Entities;

namespace PPTS.WebAPI.Customers.DataSources
{
    public class CustomerParentsDataSource : GenericCustomerDataSource<CustomerParentsResultModel, CustomerParentsResultModelCollection>
    {
        public static readonly new CustomerParentsDataSource Instance = new CustomerParentsDataSource();

        private CustomerParentsDataSource()
        {
        }

        public CustomerParentsResultModelCollection LoadCustomerParents(IPageRequestParams prp, string customerID, IEnumerable<IOrderByRequestItem> orderByBuilder)
        {
            string sqlBuilder = BuildQueryCondition(customerID);

            sqlBuilder = string.Format(" 1=1 {0}", sqlBuilder);

            var result = Query(prp, sqlBuilder, orderByBuilder);

            return result.PagedData;
        }

        protected override void OnBuildQueryCondition(QueryCondition qc)
        {
            qc.SelectFields = @" CustomerParentRelations.CustomerID,CustomerParentRelations.CustomerRole,CustomerParentRelations.ParentRole,CustomerParentRelations.IsPrimary, Parents.* ";
            qc.FromClause = @" CM.CustomerParentRelations_Current CustomerParentRelations inner join CM.Parents_Current Parents on CustomerParentRelations.ParentID=Parents.ParentID ";

            base.OnBuildQueryCondition(qc);
        }

        protected override void OnAfterQuery(CustomerParentsResultModelCollection result)
        {
            result.ForEach(item =>
                {
                    PhoneAdapter.Instance.LoadByOwnerIDInContext(item.ParentID, phone => item.FillFromPhones(phone));
                    PhoneAdapter.Instance.GetDbContext().DoAction(context => context.ExecuteDataSetSqlInContext());
                });
        }

        private string BuildQueryCondition(string customerID)
        {
            string sqlBuilder = string.Empty;
            if (!string.IsNullOrEmpty(customerID))
            {
                sqlBuilder = string.Format(
                    @" and CustomerParentRelations.CustomerID='{0}' ", customerID);
            }

            return sqlBuilder;
        }

    }
}