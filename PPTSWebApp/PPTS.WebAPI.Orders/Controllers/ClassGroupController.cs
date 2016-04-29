using MCS.Library.Data;
using MCS.Web.MVC.Library.Filters;
using PPTS.Data.Common.Adapters;
using PPTS.Data.Common.DataSources;
using PPTS.Data.Common.Entities;
using PPTS.Data.Orders.DataSources;
using PPTS.Data.Orders.Entities;
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
        [System.Web.Http.HttpPost]
        public void CreateClass(CreatableClassModel model) {
            AddClassExecutor executor = new AddClassExecutor(model);
            executor.Execute();
        }
        #endregion

        #region api/classGroup/getAllAssets
        [System.Web.Http.HttpPost]
        public AssetsQueryResultModel getAllAssets(AssetsQueryCriteriaModel criteria) {
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
        public TeacherJobsQueryResultModel getAllTeacherJobs(TeacherJobsCriteriaModel criteria) {
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
    }
}