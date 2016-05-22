using MCS.Library.Data;
using MCS.Web.MVC.Library.Filters;
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
using System.Web.Mvc;

namespace PPTS.WebAPI.Orders.Controllers
{
    [ApiPassportAuthentication]
    public class ClassGroupController : ApiController
    {
        #region api/classGroup/getAllClasses
        [System.Web.Http.HttpPost]
        public ClassesQueryResultModel GetAllClasses(ClassesQueryCriteriaModel criteria)
        {
            return new ClassesQueryResultModel
            {
                QueryResult = ClassGroupDataSource.Instance.LoadClasses(criteria.PageParams, criteria, criteria.OrderBy),
                Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(ClassSearchModel))
            };
        }

        [System.Web.Http.HttpPost]
        public PagedQueryResult<ClassSearchModel, ClassSearchModelCollection> GetPageClasses(ClassesQueryCriteriaModel criteria)
        {
            return ClassGroupDataSource.Instance.LoadClasses(criteria.PageParams, criteria, criteria.OrderBy);
        }
        #endregion

        #region api/classGroup/CreateClass
        //[ApiPassportAuthentication]
        //[PPTSJobFunctionAuthorize("PPTS:新增潜在客户")]
        [System.Web.Http.HttpPost]
        public void CreateClass(CreatableClassModel model)
        {
            AddClassExecutor executor = new AddClassExecutor(model);
            executor.Execute();
        }
        #endregion

        #region api/classGroup/getAllAssets
        [System.Web.Http.HttpPost]
        public AssetsQueryResultModel getAllAssets(AssetsQueryCriteriaModel criteria)
        {
            return new AssetsQueryResultModel
            {
                QueryResult = GenericOrderDataSource<AssetView, AssetViewCollection>.Instance.Query(criteria.PageParams, criteria, criteria.OrderBy)
            };
        }

        [System.Web.Http.HttpPost]
        public PagedQueryResult<AssetView, AssetViewCollection> getPageAssets(AssetsQueryCriteriaModel criteria)
        {
            return GenericOrderDataSource<AssetView, AssetViewCollection>.Instance.Query(criteria.PageParams, criteria, criteria.OrderBy);
        }
        #endregion

        #region api/classGroup/getAllTeacherJobs
        [System.Web.Http.HttpPost]
        public TeacherJobsQueryResultModel getAllTeacherJobs(TeacherJobsCriteriaModel criteria)
        {
            return new TeacherJobsQueryResultModel
            {
                QueryResult = GenericCommonDataSource<TeacherJobView, TeacherJobViewCollection>.Instance.Query(criteria.PageParams, criteria, criteria.OrderBy),
                Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(TeacherJobView))
            };
        }

        [System.Web.Http.HttpPost]
        public PagedQueryResult<TeacherJobView, TeacherJobViewCollection> getPageTeacherJobs(TeacherJobsCriteriaModel criteria)
        {
            return GenericCommonDataSource<TeacherJobView, TeacherJobViewCollection>.Instance.Query(criteria.PageParams, criteria, criteria.OrderBy);
        }
        #endregion

        #region api/classGroup/deleteClass
        [System.Web.Http.HttpPost]
        public void DeleteClass(dynamic data)
        {
            string classID = data.classID;
            DeleteClassExecutor executor = new DeleteClassExecutor(classID);
            executor.Execute();
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
        [System.Web.Http.HttpPost]
        public void deleteCustomer(DeleteCustomerModel model)
        {
            DeleteCustomerExecutor executor = new DeleteCustomerExecutor(model);
            executor.Execute();
        }
        #endregion

        #region api/classGroup/getClassDetail
        [System.Web.Http.HttpPost]
        public ClassDetailModel getClassDetail(ClassLessonQueryCriteriaModel criteria)
        {
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
        public void editClassLessones(EditClassLessonesModel model)
        {
            EditClassLessonesExecutor executor = new EditClassLessonesExecutor(model);
            executor.Execute();
        }
        #endregion

        #region api/classGroup/editTeacher
        public void editTeacher(EditTeacherModel model)
        {
            EditTeacherExecutor executor = new EditTeacherExecutor(model);
            executor.Execute();
        }
        #endregion

        #region api/classGroup/addCustomer
        public void addCustomer(AddCustomerModel model)
        {
            AddCustomerExecutor executor = new AddCustomerExecutor(model);
            executor.Execute();
        }
        #endregion

        #region api/classGroup/CheckCreateClass_Product
        //[ApiPassportAuthentication]
        //[PPTSJobFunctionAuthorize("PPTS:新建班级")]
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