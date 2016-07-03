using System.Collections.Generic;
using MCS.Library.Data;
using PPTS.Data.Customers.DataSources;
using PPTS.Data.Customers.Entities;
using PPTS.WebAPI.Customers.ViewModels.PotentialCustomers;
using PPTS.Data.Customers.Adapters;
using PPTS.Data.Customers;
using MCS.Library.Data.DataObjects;
using System.Text;
using MCS.Library.Data.Builder;
using MCS.Library.Data.Mapping;

namespace PPTS.WebAPI.Customers.DataSources
{
    public class CustomerStaffRelationDataSource : GenericCustomerDataSource<CustomerStaffRelation, CustomerStaffRelationCollection>
    {
        public static readonly new CustomerStaffRelationDataSource Instance = new CustomerStaffRelationDataSource();

        private CustomerStaffRelationDataSource()
        {
        }

        public PagedQueryResult<CustomerStaffRelation, CustomerStaffRelationCollection> LoadCustomerStaffRelations(IPageRequestParams prp, CustomerStaffRelationQueryCriteriaModel condition, IEnumerable<IOrderByRequestItem> orderByBuilder)
        {
            ConnectiveSqlClauseCollection sqlCollection = new ConnectiveSqlClauseCollection();
            sqlCollection.Add(ConditionMapping.GetConnectiveClauseBuilder(condition));

            string staffRelationSqlBuilder = sqlCollection.ToSqlString(TSqlBuilder.Instance);

            var result = Query(prp, staffRelationSqlBuilder, orderByBuilder);

            return result;
        }

        protected override void OnBuildQueryCondition(QueryCondition qc)
        {
            qc.SelectFields = @" CustomerStaffRelations.* ";
            qc.FromClause = @" CM.CustomerStaffRelations CustomerStaffRelations ";

            #region 数据权限加工

            #endregion

            base.OnBuildQueryCondition(qc);
        }

        protected override void OnAfterQuery(CustomerStaffRelationCollection result)
        {
            result.ForEach(item=> {

            });
        }
        
    }
}