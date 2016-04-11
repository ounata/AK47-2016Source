using System.Web.Http;
using PPTS.Data.Common.Adapters;
using PPTS.Data.Customers.DataSources;
using PPTS.Data.Customers.Entities;
using PPTS.WebAPI.Customers.ViewModels.CustomerFollows;
using MCS.Library.Data;
using PPTS.WebAPI.Customers.Executors;

namespace PPTS.WebAPI.Customers.Controllers
{
    public class CustomerFollowsController : ApiController
    {
        #region api/customerfollows/getallfollows

        /// <summary>
        /// 跟进记录查询，第一次。第一页，下载字典
        /// </summary>
        /// <param name="criteria">查询条件</param>
        /// <returns>返回带字典的跟进记录列表</returns>
        [HttpPost]
        public CustomerFollowQueryResult GetAllFollows(CustomerFollowQueryCriteriaModel criteria)
        {
            return new CustomerFollowQueryResult
            {
                QueryResult = GenericCustomerDataSource<CustomerFollow, CustomerFollowCollection>.Instance.Query(criteria.PageParams, criteria, criteria.OrderBy),
                Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(CustomerFollow))
            };
        }

        /// <summary>
        /// 跟进记录查询，翻页或排序。不下载字典
        /// </summary>
        /// <param name="criteria">查询条件</param>
        /// <returns>返回不带字典的跟进记录列表</returns>
        [HttpPost]
        public PagedQueryResult<CustomerFollow, CustomerFollowCollection> GetPagedFollows(CustomerFollowQueryCriteriaModel criteria)
        {
            return GenericCustomerDataSource<CustomerFollow, CustomerFollowCollection>.Instance.Query(criteria.PageParams, criteria, criteria.OrderBy);
        }

        #endregion

        #region api/customerfollows/createfollow

        [HttpGet]
        public CreatableCustomerFollowModel CreateFollow()
        {
            return new CreatableCustomerFollowModel
            {
                Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(CustomerFollow))
            };
        }

        [HttpPost]
        public void CreateFollow(CreatableCustomerFollowModel model)
        {
            AddCustomerFollowExecutor executor = new AddCustomerFollowExecutor(model);

            executor.Execute();
        }

        #endregion

        #region api/customerfollows/updatefollow

        [HttpGet]
        public EditableCustomerFollowModel UpdateFollow(string id)
        {
            EditableCustomerFollowModel result = new EditableCustomerFollowModel()
            {
                Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(CustomerFollow))
            };

            //GenericPotentialCustomerAdapter<PotentialCustomerModel, List<PotentialCustomerModel>>.Instance.LoadInContext(
            //        id,
            //        customer => result.Customer = customer
            //    );

            //PhoneAdapter.Instance.LoadByOwnerIDInContext(
            //    id,
            //    phones => result.Customer.FillFromPhones(phones)
            //);

            //using (DbContext context = PhoneAdapter.Instance.GetDbContext())
            //{
            //    context.ExecuteDataSetSqlInContext();
            //}

            return result;
        }

        [HttpPost]
        public void UpdateFollow(EditableCustomerFollowModel model)
        {
            EditCustomerFollowExecutor executor = new EditCustomerFollowExecutor(model);

            executor.Execute();
        }
        #endregion
    }
}