using System.Collections.Generic;
using System.Web.Http;
using MCS.Library.Data;
using PPTS.Data.Common.Adapters;
using PPTS.Data.Customers.Adapters;
using PPTS.Data.Customers.DataSources;
using PPTS.Data.Customers.Entities;
using PPTS.WebAPI.Customer.Executors;
using PPTS.WebAPI.Customer.ViewModels.PotentialCustomers;

namespace PPTS.WebAPI.Customer.Controllers
{
    public class PotentialCustomersController : ApiController
    {
        #region api/potentialcustomers/getallcustomers

        /// <summary>
        /// 潜客查询，第一次。第一页，下载字典
        /// </summary>
        /// <param name="criteria">查询条件</param>
        /// <returns>返回带字典的潜客数据列表</returns>
        [HttpPost]
        public PotentialCustomerQueryResult GetAllCustomers(PotentialCustomerQueryCriteriaModel criteria)
        {
            return new PotentialCustomerQueryResult
            {
                QueryResult = GenericCustomerDataSource<PotentialCustomer, PotentialCustomerCollection>.Instance.Query(criteria.PageParams, criteria, criteria.OrderBy),
                Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(PotentialCustomer), typeof(Parent))
            };
        }

        /// <summary>
        /// 潜客查询，翻页或排序。不下载字典
        /// </summary>
        /// <param name="criteria">查询条件</param>
        /// <returns>返回不带字典的潜客数据列表</returns>
        [HttpPost]
        public PagedQueryResult<PotentialCustomer, PotentialCustomerCollection> GetPagedCustomers(PotentialCustomerQueryCriteriaModel criteria)
        {
            return GenericCustomerDataSource<PotentialCustomer, PotentialCustomerCollection>.Instance.Query(criteria.PageParams, criteria, criteria.OrderBy);
        }

        #endregion

        #region api/potentialcustomers/createcustomer

        [HttpGet]
        public CreatablePortentialCustomerModel CreateCustomer()
        {
            return new CreatablePortentialCustomerModel
            {
                Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(CreatablePortentialCustomerModel), typeof(PotentialCustomer), typeof(Parent))
            };
        }

        [HttpPost]
        public void CreateCustomer(CreatablePortentialCustomerModel model)
        {
            AddPotentialCustomerExecutor executor = new AddPotentialCustomerExecutor(model);

            executor.Execute();
        }

        #endregion

        #region api/potentialcustomers/updatecustomer

        [HttpGet]
        public EditablePotentialCustomerModel UpdateCustomer(string id)
        {
            EditablePotentialCustomerModel result = new EditablePotentialCustomerModel()
            {
                Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(PotentialCustomer))
            };

            GenericPotentialCustomerAdapter<PotentialCustomerModel, List<PotentialCustomerModel>>.Instance.LoadInContext(
                    id,
                    customer => result.Customer = customer
                );

            PhoneAdapter.Instance.LoadByOwnerIDInContext(
                id,
                phones => result.Customer.FillFromPhones(phones)
            );

            using (DbContext context = PhoneAdapter.Instance.GetDbContext())
            {
                context.ExecuteDataSetSqlInContext();
            }

            return result;
        }

        [HttpPost]
        public void UpdateCustomer(EditablePotentialCustomerModel model)
        {
            EditPotentialCustomerExecutor executor = new EditPotentialCustomerExecutor(model);

            executor.Execute();
        }
        #endregion

        #region api/potentialcustomers/getcustomerinfo

        [HttpPost]
        public PotentialCustomer GetCustomerInfo(PotentialCustomerQueryCriteriaModel criteria)
        {
            string customerCode = criteria.CustomerCode;
            return PotentialCustomerAdapter.Instance.Load((action) =>
            {
                action.AppendItem("CustomerCode", customerCode);
            }).Find(c => c.CustomerCode == customerCode);
        }

        #endregion
    }
}