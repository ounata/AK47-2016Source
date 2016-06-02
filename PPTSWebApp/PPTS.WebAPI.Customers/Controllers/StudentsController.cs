using MCS.Library.Data;
using MCS.Library.Data.Adapters;
using MCS.Library.Net.SNTP;
using MCS.Library.OGUPermission;
using MCS.Library.Principal;
using MCS.Web.MVC.Library.Filters;
using PPTS.Data.Common;
using PPTS.Data.Common.Adapters;
using PPTS.Data.Common.Entities;
using PPTS.Data.Common.Security;
using PPTS.Data.Customers;
using PPTS.Data.Customers.Adapters;
using PPTS.Data.Customers.DataSources;
using PPTS.Data.Customers.Entities;
using PPTS.Data.Customers.Executors;
using PPTS.WebAPI.Customers.Executors;
using PPTS.WebAPI.Customers.ViewModels.Accounts;
using PPTS.WebAPI.Customers.ViewModels.PotentialCustomers;
using PPTS.WebAPI.Customers.ViewModels.Students;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace PPTS.WebAPI.Customers.Controllers
{
    [ApiPassportAuthentication]
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

            result.Relations = CustomerParentRelationAdapter.Instance.Load(id);

            if (result.Relations != null && result.Relations.Count > 0)
            {
                var builder = new InLoadingCondition((condition) =>
                {
                    result.Relations.ForEach((relation) => { condition.AppendItem(relation.ParentID); });
                }, "ParentID");

                result.Parents = GenericParentAdapter<ParentModel, ParentModelCollection>.Instance.LoadByInBuilder(builder, DateTime.MinValue);

                result.Parents.ForEach(parent =>
                {
                    PhoneAdapter.Instance.LoadByOwnerIDInContext(parent.ParentID, phone => parent.FillFromPhones(phone));
                    PhoneAdapter.Instance.GetDbContext().DoAction(context => context.ExecuteDataSetSqlInContext());
                });
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

        #region 判断当前学员是否可以充值缴费

        [HttpGet]
        public AssertResult AssertAccountCharge(string customerID)
        {
            return ChargeApplyResult.Validate(customerID, DeluxeIdentity.CurrentUser);
        }

        #endregion

        #region 转学

        /// <summary>
        /// 根据学员ID获取当前转学信息
        /// </summary>
        /// <param name="id">学员ID</param>
        /// <returns></returns>
        [HttpGet]
        public StudentTransferApplyResult GetStudentTransferApplyByCustomerID(string id)
        {
            IUser user = DeluxeIdentity.CurrentUser;
            StudentTransferApplyResult result = StudentTransferApplyResult.LoadByCustomerID(id, user);
            if (result != null)
            {
                result.Apply.InitApplier(DeluxeIdentity.CurrentUser); //初始当前申请人信息
            }
            return result;
        }

        /// <summary>
        /// 根据申请单ID获取转学信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public StudentTransferApplyResult GetStudentTransferApplyByApplyID(string id)
        {
            IUser user = DeluxeIdentity.CurrentUser;
            StudentTransferApplyResult result = StudentTransferApplyResult.LoadByApplyID(id, user);
            return result;
        }

        /// <summary>
        /// 保存转学申请单。
        /// </summary>
        /// <param name="apply">申请信息</param>
        [HttpPost]
        public void SaveStudentTransferApply(StudentTransferApplyModel apply)
        {
            apply.Prepare(DeluxeIdentity.CurrentUser);
            new StudentEditTransferApplyExecutor(apply).Execute();
        }

        /// <summary>
        /// 审批转学申请单
        /// </summary>
        /// <param name="apply"></param>
        /// <returns></returns>
        [HttpPost]
        public StudentTransferApplyModel ApproveStudentTransferApply(dynamic apply)
        {
            string opinion = apply.opinion;
            string applyID = apply.applyID;

            IUser user = DeluxeIdentity.CurrentUser;
            StudentApproveTransferModel model = new StudentApproveTransferModel();
            model.BillID = applyID;
            model.ApproverID = user.ID;
            model.ApproverName = user.Name;
            model.ApproverJobID = user.GetCurrentJob().ID;
            model.ApproverJobName = user.GetCurrentJob().Name;
            model.ApproveTime = SNTPClient.AdjustedTime;
            model.IsFinalApprove = true;
            model.IsRefused = opinion == "refuse";
            model.PrepareApprove();
            if (model.Apply.ApplyStatus == ApplyStatusDefine.Approving)
            {
                object result = new StudentApproveTransferApplyExecutor(model).Execute();
                return AutoMapper.Mapper.DynamicMap<StudentTransferApplyModel>(result);
            }
            return null;
        }
        #endregion

        #region api/students/createparent

        [HttpGet]
        public CreatableParentModel CreateParent(string id)
        {
            CreatableParentModel result = new CreatableParentModel();
            result.Customer = GenericCustomerAdapter<StudentModel, List<StudentModel>>.Instance.Load(id);
            result.Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(CreatableParentModel), typeof(Parent));
            return result;
        }

        [HttpPost]
        public void CreateParent(CreatableParentModel model)
        {
            CreateStudentParentExecutor executor = new CreateStudentParentExecutor(model);
            executor.Execute();
        }

        #endregion

        #region api/students/assignTeacher
        [HttpPost]
        public void assignTeacher(CustomerTeacherRelationCollection ctrc)
        {
            AssignTeacherExecutor executor = new AssignTeacherExecutor(ctrc);
            executor.Execute();
        }
        #endregion

        #region api/students/getTeachers
        [HttpGet]
        public TeacherJobViewCollection getTeachers()
        {
            TeachersQueryCriteriaModel criteria = new TeachersQueryCriteriaModel();
            var result = TeacherJobViewAdapter.Instance.Load(builder => builder.AppendItem("CampusID", criteria.CampusID).AppendItem("JobOrgType", (int)criteria.JobOrgType));
            return result;
        }
        #endregion

        #region api/students/calloutTeacher
        [HttpPost]
        public void calloutTeacher(CustomerTeacherAssignApply cta)
        {
            CalloutTeacherExecutor executor = new CalloutTeacherExecutor(cta);
            executor.Execute();
        }
        #endregion

        #region api/students/changeTeacher
        [HttpPost]
        public void changeTeacher(CustomerTeacherAssignApply cta)
        {
            ChangeTeacherExecutor executor = new ChangeTeacherExecutor(cta);
            executor.Execute();
        }
        #endregion

        #region api/students/getAllCustomerTeacherRelations
        [HttpPost]
        public CustomerTeacherRelationQueryResultModel getAllCustomerTeacherRelations(CustomerTeacherRelationQueryCriteriaModel criteria)
        {
            return new CustomerTeacherRelationQueryResultModel
            {
                CustomerTeachers = CustomerTeacherRelationAdapter.Instance.Load(builder => builder.AppendItem("CustomerID", criteria.CustomerID), DateTime.MinValue),
                Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(CustomerTeacherRelation))
            }; 
        }
        #endregion 
    }
}
