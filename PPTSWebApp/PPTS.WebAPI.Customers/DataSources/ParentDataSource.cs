using System.Collections.Generic;
using MCS.Library.Data;
using PPTS.Data.Customers.Adapters;
using PPTS.Data.Customers.DataSources;
using PPTS.Data.Customers.Entities;
using PPTS.WebAPI.Customers.ViewModels.PotentialCustomers;

namespace PPTS.WebAPI.Customers.DataSources
{
    public class ParentDataSource : GenericCustomerDataSource<ParentModel, ParentModelCollection>
    {
        public static readonly new ParentDataSource Instance = new ParentDataSource();

        private ParentDataSource()
        {
        }

        protected override void OnAfterQuery(ParentModelCollection result)
        {
            result.ForEach(parent =>
            {
                PhoneAdapter.Instance.LoadByOwnerIDInContext(parent.ParentID, phone => parent.FillFromPhones(phone));
                PhoneAdapter.Instance.GetDbContext().DoAction(context => context.ExecuteDataSetSqlInContext());
            });
        }

        public PagedQueryResult<ParentModel, ParentModelCollection> LoadParents(IPageRequestParams prp, object condition, IEnumerable<IOrderByRequestItem> orderByBuilder)
        {
            string select = " a.ParentID, a.ParentName, a.CreateTime, a.Gender ";
            string from = " Parents_Current a left join ParentsFulltext b on a.ParentID = b.OwnerID ";
            PagedQueryResult<ParentModel, ParentModelCollection> result = Query(prp, select, from, condition, orderByBuilder);
            return result;
        }
    }
}