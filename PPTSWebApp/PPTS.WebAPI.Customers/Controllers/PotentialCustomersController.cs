using MCS.Library.Data;
using MCS.Library.Data.Adapters;
using MCS.Web.MVC.Library.Filters;
using PPTS.Data.Common.Adapters;
using PPTS.Data.Customers.Adapters;
using PPTS.Data.Customers.DataSources;
using PPTS.Data.Customers.Entities;
using PPTS.Web.MVC.Library.Filters;
using PPTS.WebAPI.Customers.DataSources;
using PPTS.WebAPI.Customers.Executors;
using PPTS.WebAPI.Customers.ViewModels.Parents;
using PPTS.WebAPI.Customers.ViewModels.PotentialCustomers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

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
        //[ApiPassportAuthentication]
        //[PPTSJobFunctionAuthorize("PPTS:潜在客户列表查看,jf2,jf3")]
        public PotentialCustomerQueryResult GetAllCustomers(PotentialCustomerQueryCriteriaModel criteria)
        {
            return new PotentialCustomerQueryResult
            {
                QueryResult = PotentialCustomerDataSource.Instance.LoadPotentialCustomers(criteria.PageParams, criteria, criteria.OrderBy),
                Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(PotentialCustomer), typeof(Parent))
            };
        }

        /// <summary>
        /// 潜客查询，翻页或排序。不下载字典
        /// </summary>
        /// <param name="criteria">查询条件</param>
        /// <returns>返回不带字典的潜客数据列表</returns>
        [HttpPost]
        //[ApiPassportAuthentication]
        //[PPTSFunctionAuthorize("PPTS:f1,f2,f3")]
        //[PPTSJobFunctionAuthorize("PPTS:潜在客户列表查看,jf2,jf3")]
        public PagedQueryResult<PotentialCustomerSearchModel, PotentialCustomerSearchModelCollection> GetPagedCustomers(PotentialCustomerQueryCriteriaModel criteria)
        {
            return PotentialCustomerDataSource.Instance.LoadPotentialCustomers(criteria.PageParams, criteria, criteria.OrderBy);
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
            return EditablePotentialCustomerModel.Load(id);
        }

        [HttpPost]
        public void UpdateCustomer(EditablePotentialCustomerModel model)
        {
            EditPotentialCustomerExecutor executor = new EditPotentialCustomerExecutor(model);

            executor.Execute();
        }
        #endregion

        #region api/potentialcustomers/getcustomerbycode

        [HttpGet]
        public PotentialCustomer GetCustomerByCode(string customerCode)
        {
            return PotentialCustomerAdapter.Instance.Load((action) =>
            {
                action.AppendItem("CustomerCode", customerCode);
            }, DateTime.MinValue).FirstOrDefault();
        }

        #endregion

        #region api/potentialcustomers/getcustomerinfo

        [HttpGet]
        public EditablePotentialCustomerModel GetCustomerInfo(string id)
        {
            return this.UpdateCustomer(id);
        }

        #endregion

        #region api/potentialcustomers/getstaffrelations

        [HttpPost]
        public CustomerStaffRelationQueryResult GetStaffRelations(CustomerStaffRelationQueryCriteriaModel criteria)
        {
            return new CustomerStaffRelationQueryResult
            {
                QueryResult = GenericCustomerDataSource<CustomerStaffRelation, CustomerStaffRelationCollection>.Instance.Query(criteria.PageParams, criteria, criteria.OrderBy)
            };
        }

        [HttpPost]
        public PagedQueryResult<CustomerStaffRelation, CustomerStaffRelationCollection> GetPagedStaffRelations(CustomerStaffRelationQueryCriteriaModel criteria)
        {
            return GenericCustomerDataSource<CustomerStaffRelation, CustomerStaffRelationCollection>.Instance.Query(criteria.PageParams, criteria, criteria.OrderBy);
        }

        #endregion

        #region api/potentialcustomers/getcustomerparents

        [HttpGet]
        public CustomerParentsQueryResult GetCustomerParents(string id)
        {
            CustomerParentsQueryResult result = new CustomerParentsQueryResult();

            result.CustomerParentRelations = CustomerParentRelationAdapter.Instance.Load(action =>
            {
                action.AppendItem("CustomerID", id);
            }, DateTime.MinValue);

            if (result.CustomerParentRelations != null && result.CustomerParentRelations.Count > 0)
            {
                var builder = new InLoadingCondition((p) =>
                {
                    result.CustomerParentRelations.ForEach((relation) =>
                    {
                        p.AppendItem(relation.ParentID);
                    });
                }, "ParentID");
                result.Parents = ParentAdapter.Instance.LoadByInBuilder(builder, DateTime.MinValue);
            }

            result.Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(CreatablePortentialCustomerModel), typeof(Parent));

            return result;
        }
        #endregion

        #region api/potentialcustomers/GetCustomerParent
        [HttpGet]
        public EditableParentModel GetCustomerParent(string id, string customerId)
        {
            EditableParentModel result = new EditableParentModel()
            {
                Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(CreatablePortentialCustomerModel), typeof(PotentialCustomer), typeof(Parent))
            };

            GenericParentAdapter<ParentModel, List<ParentModel>>.Instance.LoadInContext(
                    id,
                    parent => result.Parent = parent
                );

            result.CustomerParentRelation = CustomerParentRelationAdapter.Instance.Load(customerId, id);

            result.Customer = PotentialCustomerAdapter.Instance.Load(result.CustomerParentRelation.CustomerID);

            PhoneAdapter.Instance.LoadByOwnerIDInContext(id, phones => result.Parent.FillFromPhones(phones));

            PhoneAdapter.Instance.GetDbContext().DoAction(context => context.ExecuteDataSetSqlInContext());

            return result;
        }
        #endregion

        #region api/potentialcustomers/getparentinfo
        [HttpGet]
        public EditableParentModel GetParentInfo(string id)
        {
            EditableParentModel result = new EditableParentModel()
            {
                Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(CreatablePortentialCustomerModel), typeof(PotentialCustomer), typeof(Parent))
            };

            GenericParentAdapter<ParentModel, List<ParentModel>>.Instance.LoadInContext(id, parent => result.Parent = parent);

            PhoneAdapter.Instance.LoadByOwnerIDInContext(id, phones => result.Parent.FillFromPhones(phones));

            PhoneAdapter.Instance.GetDbContext().DoAction(context => context.ExecuteDataSetSqlInContext());

            return result;
        }
        #endregion

        #region api/potentialcustomers/updateparent

        [HttpPost]
        public void UpdateParent(EditableParentModel model)
        {
            EditableParentExecutor executor = new EditableParentExecutor(model);

            executor.Execute();
        }

        #endregion

        #region api/potentialcustomers/getallparents

        [HttpPost]
        public ParentsSearchQueryResult GetAllParents(ParentsSearchQueryCriteriaModel criteria)
        {
            return new ParentsSearchQueryResult
            {
                QueryResult = ParentDataSource.Instance.LoadParents(criteria.PageParams, criteria, criteria.OrderBy),
                Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(CreatablePortentialCustomerModel), typeof(PotentialCustomer), typeof(Parent))
            };
        }

        [HttpPost]
        public PagedQueryResult<ParentModel, ParentModelCollection> GetPagedParents(ParentsSearchQueryCriteriaModel criteria)
        {
            return ParentDataSource.Instance.LoadParents(criteria.PageParams, criteria, criteria.OrderBy);
        }

        #endregion

        #region api/potentialcustomers/addparent

        [HttpPost]
        public void AddParent(CreatablePortentialCustomerModel criteria)
        {
            AddParentExecutor executor = new AddParentExecutor(criteria);
            executor.Execute();
        }

        #endregion

        #region 判断当前潜客是否可以充值缴费

        [HttpGet]
        public AssertResult AssertAccountCharge(string customerID)
        {
            //CustomerStaffRelationCollection collection = CustomerStaffRelationAdapter.Instance.LoadByCustomerID(id);
            //CustomerStaffRelation relation = collection.Find(x => x.RelationType == Data.Customers.CustomerRelationType.Consultant);
            //if (relation != null)
            //{

            //}
            //return new AssertResult(false, "当前用户不清楚岗位类型，无法进行充值");
            return new AssertResult();
        }
        #endregion
    }
}