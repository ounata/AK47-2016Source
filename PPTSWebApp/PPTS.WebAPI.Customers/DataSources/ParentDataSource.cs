using System.Collections.Generic;
using MCS.Library.Data;
using PPTS.Data.Customers.Adapters;
using PPTS.Data.Customers.DataSources;
using PPTS.Data.Customers.Entities;
using PPTS.WebAPI.Customers.ViewModels.PotentialCustomers;
using MCS.Library.Data.DataObjects;
using PPTS.WebAPI.Customers.ViewModels.Parents;
using PPTS.Data.Customers;

namespace PPTS.WebAPI.Customers.DataSources
{
    public class ParentDataSource : GenericCustomerDataSource<ParentModel, ParentModelCollection>
    {
        public static readonly new ParentDataSource Instance = new ParentDataSource();

        private ParentDataSource()
        {
        }

        public PagedQueryResult<ParentModel, ParentModelCollection> LoadParents(IPageRequestParams prp, ParentsSearchQueryCriteriaModel condition, IEnumerable<IOrderByRequestItem> orderByBuilder)
        {
            string sqlBuilder = BuildQueryCondition(condition);

            sqlBuilder = string.Format(" 1=1 {0}", sqlBuilder);

            var result = Query(prp, sqlBuilder, orderByBuilder);

            return result;
        }

        protected override void OnBuildQueryCondition(QueryCondition qc)
        {
            qc.SelectFields = @" Parents.ParentID, Parents.ParentName, Parents.CreateTime, Parents.Gender ";
            qc.FromClause = @" CM.Parents_Current Parents ";

            base.OnBuildQueryCondition(qc);
        }

        protected override void OnAfterQuery(ParentModelCollection result)
        {
            result.ForEach(parent =>
            {
                PhoneAdapter.Instance.LoadByOwnerIDInContext(parent.ParentID, phone => parent.FillFromPhones(phone));
                PhoneAdapter.Instance.GetDbContext().DoAction(context => context.ExecuteDataSetSqlInContext());
            });
        }

        private string BuildQueryCondition(ParentsSearchQueryCriteriaModel condition)
        {
            string sqlBuilder = string.Empty;
            if (!string.IsNullOrEmpty(condition.CustomerID))
            {
                sqlBuilder = string.Format(
                    @" and ParentID not in (select ParentID from CM.CustomerParentRelations_Current where CustomerID = '{0}') ", condition.CustomerID);
            }
            if (!string.IsNullOrEmpty(condition.Keyword))
            {
                sqlBuilder += string.Format(
                    @" and exists(select OwnerID
                       from CM.ParentsFulltext AS ParentsFulltext
                       where Parents.ParentID = ParentsFulltext.OwnerID and CONTAINS(ParentsFulltext.ParentSearchContent, N'""{0}""'))", condition.Keyword.EncodeString());
            }

            return sqlBuilder;
        }

    }
}