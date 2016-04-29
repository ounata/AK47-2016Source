using System.Web.Http;
using PPTS.Data.Common.Adapters;
using PPTS.Data.Customers.DataSources;
using PPTS.Data.Customers.Entities;
using PPTS.WebAPI.Customers.ViewModels.CustomerFollows;
using MCS.Library.Data;
using PPTS.WebAPI.Customers.Executors;
using PPTS.WebAPI.Customers.DataSources;

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
        public FollowQueryResult GetAllFollows(FollowQueryCriteriaModel criteria)
        {
            return new FollowQueryResult
            {
                QueryResult = CustomersFollowDataSource.Instance.LoadCustomerFollow(criteria.PageParams, criteria, criteria.OrderBy),
                Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(CustomerFollow))
            };
        }

        /// <s6ummary>
        /// 跟进记录查询，翻页或排序。不下载字典
        /// </summary>
        /// <param name="criteria">查询条件</param>
        /// <returns>返回不带字典的跟进记录列表</returns>
        [HttpPost]
        public PagedQueryResult<FollowQueryModel, CustomerFollowQueryCollection> GetPagedFollows(FollowQueryCriteriaModel criteria)
        {
            return CustomersFollowDataSource.Instance.LoadCustomerFollow(criteria.PageParams, criteria, criteria.OrderBy);
        }

        #endregion

        #region api/customerfollows/createfollow
        /// <summary>
        /// 新建跟踪记录
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="isPotential"></param>
        /// <returns></returns>
        [HttpGet]
        public CreatableFollowModel CreateFollow(string customerId, bool isPotential)
        {
            return new CreatableFollowModel(customerId, isPotential)
            {
                Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(CustomerFollow), typeof(CreatableFollowModel))
            };
        }

        [HttpPost]
        public void CreateFollow(CreatableFollowModel model)
        {
            AddCustomerFollowExecutor executor = new AddCustomerFollowExecutor(model);

            executor.Execute();
        }

        #endregion

        #region api/customerfollows/updatefollow

        [HttpGet]
        public EditableFollowModel UpdateFollow(string id)
        {
            EditableFollowModel result = new EditableFollowModel()
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
        public void UpdateFollow(EditableFollowModel model)
        {
            EditCustomerFollowExecutor executor = new EditCustomerFollowExecutor(model);

            executor.Execute();
        }
        #endregion
    }
}