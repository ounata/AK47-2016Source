using MCS.Library.Data;
using MCS.Library.Data.Adapters;
using PPTS.Data.Common.Adapters;
using PPTS.Data.Common.Security;
using PPTS.Data.Customers.Adapters;
using PPTS.Data.Customers.DataSources;
using PPTS.Data.Customers.Entities;
using PPTS.WebAPI.Customers.Executors;
using PPTS.WebAPI.Customers.ViewModels.PotentialCustomers;
using PPTS.WebAPI.Customers.ViewModels.Students;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace PPTS.WebAPI.Customers.Controllers
{
    public class StudentsController : ApiController
    {
        #region api/students/getallstudents

        /// <summary>
        /// 学员查询，第一次。第一页，下载字典
        /// </summary>
        /// <param name="criteria">查询条件</param>
        /// <returns>返回带字典的潜客数据列表</returns>
        [HttpPost]
        public StudentQueryResult GetAllStudents(StudentQueryCriteriaModel criteria)
        {
            return new StudentQueryResult
            {
                QueryResult = GenericCustomerDataSource<Customer, CustomerCollection>.Instance.Query(criteria.PageParams, criteria, criteria.OrderBy),
                Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(Customer))
            };
        }

        [HttpPost]
        public PagedQueryResult<Customer, CustomerCollection> GetPagedStudents(StudentQueryCriteriaModel criteria)
        {
            return GenericCustomerDataSource<Customer, CustomerCollection>.Instance.Query(criteria.PageParams, criteria, criteria.OrderBy);
        }

        #endregion

        #region api/students/getstudentinfo

        [HttpGet]
        public EditableStudentModel GetStudentInfo(string id)
        {
            return EditableStudentModel.Load(id);
        }

        #endregion

        #region api/students/updatestudent
        [HttpPost]
        public void UpdateStudent(EditableStudentModel model)
        {
            EditableStudentExecutor executor = new EditableStudentExecutor(model);

            executor.Execute();
        }
        #endregion

        #region api/students/GetCustomerParent
        [HttpGet]
        public EditableStudentParentModel GetCustomerParent(string id, string customerId)
        {
            EditableStudentParentModel result = new EditableStudentParentModel()
            {
                Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(CreatablePortentialCustomerModel), typeof(PotentialCustomer), typeof(Parent))
            };

            GenericParentAdapter<ParentModel, List<ParentModel>>.Instance.LoadInContext(
                    id,
                    parent => result.Parent = parent
                );

            result.CustomerParentRelation = CustomerParentRelationAdapter.Instance.Load(customerId, id);

            result.Customer = CustomerAdapter.Instance.Load(result.CustomerParentRelation.CustomerID);

            PhoneAdapter.Instance.LoadByOwnerIDInContext(id, phones => result.Parent.FillFromPhones(phones));

            PhoneAdapter.Instance.GetDbContext().DoAction(context => context.ExecuteDataSetSqlInContext());

            return result;
        }
        #endregion

        #region api/students/updateparent

        [HttpPost]
        public void UpdateParent(EditableStudentParentModel model)
        {
            EditableStudentParentExecutor executor = new EditableStudentParentExecutor(model);

            executor.Execute();
        }

        #endregion

        #region api/students/addparent

        [HttpPost]
        public void AddParent(AddStudentParentModel criteria)
        {
            AddStudentParentExecutor executor = new AddStudentParentExecutor(criteria);
            executor.Execute();
        }

        #endregion

        #region api/students/getstudentparents

        [HttpGet]
        public CustomerParentsQueryResult GetStudentParents(string id)
        {
            CustomerParentsQueryResult result = new CustomerParentsQueryResult();
            result.CustomerParentRelations = CustomerParentRelationAdapter.Instance.Load(id);

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

        #region api/students/GetStudentParent
        [HttpGet]
        public EditableStudentParentModel GetStudentParent(string id, string customerId)
        {
            EditableStudentParentModel result = new EditableStudentParentModel()
            {
                Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(CreatablePortentialCustomerModel), typeof(PotentialCustomer), typeof(Parent))
            };

            GenericParentAdapter<ParentModel, List<ParentModel>>.Instance.LoadInContext(
                    id,
                    parent => result.Parent = parent
                );

            result.CustomerParentRelation = CustomerParentRelationAdapter.Instance.Load(customerId, id);

            result.Customer = CustomerAdapter.Instance.Load(result.CustomerParentRelation.CustomerID);

            PhoneAdapter.Instance.LoadByOwnerIDInContext(id, phones => result.Parent.FillFromPhones(phones));

            PhoneAdapter.Instance.GetDbContext().DoAction(context => context.ExecuteDataSetSqlInContext());

            return result;
        }
        #endregion
    }
}
