using System.Collections.Generic;
using System.Web.Http;
using MCS.Library.Data;
using PPTS.Data.Common.Adapters;
using PPTS.Data.Customers.Adapters;
using PPTS.Data.Customers.DataSources;
using PPTS.Data.Customers.Entities;
using PPTS.WebAPI.Customers.Executors;
using PPTS.WebAPI.Customers.ViewModels.PotentialCustomers;
using System.Linq;

namespace PPTS.WebAPI.Customers.Controllers
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

            CustomerParentRelation customerParentRelation = CustomerParentRelationAdapter.Instance.Load(action =>
                {
                    action.AppendItem("CustomerID", id).AppendItem("IsPrimary", 1);
                }).FirstOrDefault();

            if (customerParentRelation != null)
            {
                result.Parent = ParentAdapter.Instance.Load(customerParentRelation.ParentID);
            }

            result.CustomerStaffRelation = CustomerStaffRelationAdapter.Instance.Load(action =>
            {
                action.AppendItem("CustomerID", id);
            });

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
            var customerCode = criteria.CustomerCode;
            return PotentialCustomerAdapter.Instance.Load((action) =>
            {
                action.AppendItem("CustomerCode", customerCode);
            }).FirstOrDefault();
        }

        #endregion

        #region api/potentialcustomers/getcustomerinfo

        [HttpPost]
        public CustomerStaffRelationQueryResult GetStaffRelations(CustomerStaffRelationQueryCriteriaModel criteria)
        {
            return new CustomerStaffRelationQueryResult
            {
                QueryResult = GenericCustomerDataSource<CustomerStaffRelation, CustomerStaffRelationCollection>.Instance.Query(criteria.PageParams, criteria, criteria.OrderBy)
            };
        }

        #endregion
    }
}