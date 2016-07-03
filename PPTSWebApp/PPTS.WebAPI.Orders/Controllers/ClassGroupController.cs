using MCS.Library.Data;
using MCS.Web.MVC.Library.Filters;
using PPTS.Contracts.Proxies;
using PPTS.Data.Common.Adapters;
using PPTS.Data.Common.DataSources;
using PPTS.Data.Common.Entities;
using PPTS.Data.Orders.Adapters;
using PPTS.Data.Orders.DataSources;
using PPTS.Data.Orders.Entities;
using PPTS.Web.MVC.Library.Filters;
using PPTS.WebAPI.Orders.DataSources;
using PPTS.WebAPI.Orders.Executors;
using PPTS.WebAPI.Orders.ViewModels.ClassGroup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace PPTS.WebAPI.Orders.Controllers
{
    [ApiPassportAuthentication]
    public class ClassGroupController : ApiController
    {
        private void CheckEditAuth(string classID) {
            PPTS.Data.Common.Authorization.ScopeAuthorization<Class>
                .GetInstance(Data.Orders.ConnectionDefine.PPTSOrderConnectionName).CheckEditAuth(classID);
        }

        private void CheckReadAuth(string classID) {
            PPTS.Data.Common.Authorization.ScopeAuthorization<Class>
               .GetInstance(Data.Orders.ConnectionDefine.PPTSOrderConnectionName).CheckReadAuth(classID);
        }

        #region api/classGroup/getAllClasses
        [PPTSJobFunctionAuthorize("PPTS:班级管理（按钮查看班级、按钮查看学生）-本校区,班级管理（按钮查看班级、按钮查看学生）-本分公司,班级管理（按钮查看班级、按钮查看学生）-全国")]
        [System.Web.Http.HttpPost]
        public ClassesQueryResultModel GetAllClasses(ClassesQueryCriteriaModel criteria)
        {
            return new ClassesQueryResultModel
            {
                QueryResult = ClassGroupDataSource.Instance.LoadClasses(criteria.PageParams, criteria, criteria.OrderBy),
                Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(ClassSearchModel))
            };
        }

        [PPTSJobFunctionAuthorize("PPTS:班级管理（按钮查看班级、按钮查看学生）-本校区,班级管理（按钮查看班级、按钮查看学生）-本分公司,班级管理（按钮查看班级、按钮查看学生）-全国")]
        [System.Web.Http.HttpPost]
        public PagedQueryResult<ClassSearchModel, ClassSearchModelCollection> GetPageClasses(ClassesQueryCriteriaModel criteria)
        {
            return ClassGroupDataSource.Instance.LoadClasses(criteria.PageParams, criteria, criteria.OrderBy);
        }
        #endregion

        #region api/classGroup/CreateClass
        [PPTSJobFunctionAuthorize("PPTS:按钮新增班级（编辑课表/更换教师/增加学生/删除班级/移出班级）-本校区")]
        [System.Web.Http.HttpPost]
        public void CreateClass(CreatableClassModel model)
        {
            AddClassExecutor executor = new AddClassExecutor(model);
            executor.Execute();

            PPTSClassServiceProxy.Instance.SyncClassCountToProduct(model.ProductID);
        }
        #endregion

        #region api/classGroup/getAllAssets

        [System.Web.Http.HttpPost]
        public AssetsQueryResultModel getAllAssets(AssetsQueryCriteriaModel criteria)
        {
            AssetsQueryResultModel result = new AssetsQueryResultModel
            {
                QueryResult = GenericOrderDataSource<OrderItemView, OrderItemViewCollection>.Instance.Query(criteria.PageParams, criteria, criteria.OrderBy)
            };
            return result;
        }


        [System.Web.Http.HttpPost]
        public PagedQueryResult<OrderItemView, OrderItemViewCollection> getPageAssets(AssetsQueryCriteriaModel criteria)
        {
            return GenericOrderDataSource<OrderItemView, OrderItemViewCollection>.Instance.Query(criteria.PageParams, criteria, criteria.OrderBy);
        }
        #endregion

        #region api/classGroup/getAllTeacherJobs

        [System.Web.Http.HttpPost]
        public TeacherJobsQueryResultModel getAllTeacherJobs(TeacherJobsCriteriaModel criteria)
        {
            return new TeacherJobsQueryResultModel
            {
                QueryResult = TeacherJobsQueryResultModel.Trim(GenericCommonDataSource<TeacherJobView, TeacherJobViewCollection>.Instance.Query(criteria.PageParams, criteria, criteria.OrderBy)),
                Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(TeacherJobView))
            };
        }


        [System.Web.Http.HttpPost]
        public PagedQueryResult<TeacherJobView, TeacherJobViewCollection> getPageTeacherJobs(TeacherJobsCriteriaModel criteria)
        {
            return TeacherJobsQueryResultModel.Trim(GenericCommonDataSource<TeacherJobView, TeacherJobViewCollection>.Instance.Query(criteria.PageParams, criteria, criteria.OrderBy));
        }
        #endregion

        #region api/classGroup/deleteClass
        [PPTSJobFunctionAuthorize("PPTS:按钮新增班级（编辑课表/更换教师/增加学生/删除班级/移出班级）-本校区")]
        [System.Web.Http.HttpPost]
        public void DeleteClass(dynamic data)
        {
            string classID = data.classID;

            #region 写入权限验证

            #endregion

            DeleteClassExecutor executor = new DeleteClassExecutor(classID);
            executor.Execute();

            SyncClassCountToProductExecutor exe = new SyncClassCountToProductExecutor(classID);
            exe.Execute();
        }
        #endregion

        #region api/classGroup/getAllCustomers

        [System.Web.Http.HttpPost]
        public ClassCustomerQueryResultModel getAllCustomers(ClassCustomerQueryCriteriaModel criteria)
        {
            return new ClassCustomerQueryResultModel
            {
                QueryResult = ClassCustomerDataSource.Instance.Load(criteria.PageParams, criteria, criteria.OrderBy),
                Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(ClassLessonItem))
            };
        }


        [System.Web.Http.HttpPost]
        public PagedQueryResult<ClassLessonItem, ClassLessonItemCollection> GetPageCustomers(ClassCustomerQueryCriteriaModel criteria)
        {
            return ClassCustomerDataSource.Instance.Load(criteria.PageParams, criteria, criteria.OrderBy);
        }
        #endregion

        #region api/classGroup/deleteCustomer
        [PPTSJobFunctionAuthorize("PPTS:按钮新增班级（编辑课表/更换教师/增加学生/删除班级/移出班级）-本校区")]
        [System.Web.Http.HttpPost]
        public void deleteCustomer(DeleteCustomerModel model)
        {
            #region 写入权限验证
            CheckEditAuth(model.ClassID);
            #endregion

            DeleteCustomerExecutor executor = new DeleteCustomerExecutor(model);
            executor.Execute();
        }
        #endregion

        #region api/classGroup/getClassDetail
        [PPTSJobFunctionAuthorize("PPTS:班级管理（按钮查看班级、按钮查看学生）-本校区,班级管理（按钮查看班级、按钮查看学生）-本分公司,班级管理（按钮查看班级、按钮查看学生）-全国")]
        [System.Web.Http.HttpPost]
        public ClassDetailModel getClassDetail(ClassLessonQueryCriteriaModel criteria)
        {
            CheckReadAuth(criteria.ClassID);

            return new ClassDetailModel()
            {
                Class = GenericClassGroupAdapter<ClassModel, ClassModelCollection>.Instance.Load(builder => builder.AppendItem("ClassID", criteria.ClassID)).SingleOrDefault(),
                Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(ClassModel), typeof(ClassLessonModel)),
                ClassLessones = new ClassLessonQueryResultModel() {                    
                    QueryResult = GenericOrderDataSource<ClassLessonModel, ClassLessonModelCollection>.Instance.Query(criteria.PageParams, criteria, criteria.OrderBy)
                }
            };
        }

        [System.Web.Http.HttpPost]
        public PagedQueryResult<ClassLessonModel, ClassLessonModelCollection> GetPageClassLessons(ClassLessonQueryCriteriaModel criteria) {
            return GenericOrderDataSource<ClassLessonModel, ClassLessonModelCollection>.Instance.Query(criteria.PageParams, criteria, criteria.OrderBy);
        }
        #endregion

        #region api/classGroup/editClassLessones
        [PPTSJobFunctionAuthorize("PPTS:按钮新增班级（编辑课表/更换教师/增加学生/删除班级/移出班级）-本校区")]
        public void editClassLessones(EditClassLessonesModel model)
        {
            #region 写入权限验证
            CheckEditAuth(model.ClassID);
            #endregion

            EditClassLessonesExecutor executor = new EditClassLessonesExecutor(model);
            executor.Execute();
        }
        #endregion

        #region api/classGroup/editTeacher
        [PPTSJobFunctionAuthorize("PPTS:按钮新增班级（编辑课表/更换教师/增加学生/删除班级/移出班级）-本校区")]
        public void editTeacher(EditTeacherModel model)
        {
            #region 写入权限验证
            CheckEditAuth(model.ClassID);
            #endregion

            EditTeacherExecutor executor = new EditTeacherExecutor(model);
            executor.Execute();
        }
        #endregion

        #region api/classGroup/addCustomer
        [PPTSJobFunctionAuthorize("PPTS:按钮新增班级（编辑课表/更换教师/增加学生/删除班级/移出班级）-本校区")]
        public void addCustomer(AddCustomerModel model)
        {
            #region 写入权限验证
            CheckEditAuth(model.ClassID);
            #endregion

            AddCustomerExecutor executor = new AddCustomerExecutor(model);
            executor.Execute();
        }
        #endregion

        #region api/classGroup/CheckCreateClass_Product
        [PPTSJobFunctionAuthorize("PPTS:按钮新增班级（编辑课表/更换教师/增加学生/删除班级/移出班级）-本校区")]
        [System.Web.Http.HttpPost]
        public CheckResultModel checkCreateClass_Product(dynamic data)
        {
            CreatableClassModel model = new CreatableClassModel() { ProductID = data.productID };
            return model.CheckProduct();
        }
        #endregion

        #region api/classGroup/checkDeleteCustomer
        #endregion

        #region api/classGroup/getCustomerAllClasses
        [System.Web.Http.HttpPost]
        public CustomerClassQueryResultModel getCustomerAllClasses(CustomerClassQueryCriteriaModel criteria) {
            return new CustomerClassQueryResultModel
            {
                QueryResult = CustomerClassesDataSource.Instance.Load(criteria.PageParams, criteria, criteria.OrderBy)
            };
        }
        #endregion
    }
}