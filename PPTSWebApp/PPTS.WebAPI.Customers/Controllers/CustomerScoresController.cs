using MCS.Library.Core;
using MCS.Library.Data;
using MCS.Library.Data.Adapters;
using MCS.Library.Principal;
using MCS.Web.MVC.Library.Filters;
using PPTS.Data.Common;
using PPTS.Data.Common.Adapters;
using PPTS.Data.Common.Entities;
using PPTS.Data.Common.Security;
using PPTS.Data.Customers.Adapters;
using PPTS.Data.Customers.Entities;
using PPTS.WebAPI.Customers.DataSources;
using PPTS.WebAPI.Customers.Executors;
using PPTS.WebAPI.Customers.ViewModels.CustomerScores;
using PPTS.WebAPI.Customers.ViewModels.PotentialCustomers;
using System;
using System.Web.Http;

namespace PPTS.WebAPI.Customers.Controllers
{
    [ApiPassportAuthentication]
    public class CustomerScoresController : ApiController
    {
        #region api/customerscores/getallscores

        /// <summary>
        /// 成绩查询，第一次。第一页，下载字典
        /// </summary>
        /// <param name="criteria">查询条件</param>
        /// <returns>返回带字典的成绩数据列表</returns>
        [HttpPost]
        public CustomerScoresQueryResult GetAllScores(CustomerScoresQueryCriteriaModel criteria)
        {
            return new CustomerScoresQueryResult
            {
                isLastDayOfMonth = DateTime.Today.AddDays(1).Day == 1,
                QueryResult = CustomerScoresDataSource.Instance.LoadCustomerScores(criteria.PageParams, criteria, criteria.OrderBy),
                Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(CustomerScore), typeof(CustomerScoreItem))
            };
        }

        /// <summary>
        /// 成绩查询，翻页或排序。不下载字典
        /// </summary>
        /// <param name="criteria">查询条件</param>
        /// <returns>返回不带字典的成绩数据列表</returns>
        [HttpPost]
        public PagedQueryResult<CustomerScoresSearchModel, CustomerScoresSearchModelCollection> GetPagedScores(CustomerScoresQueryCriteriaModel criteria)
        {
            return CustomerScoresDataSource.Instance.LoadCustomerScores(criteria.PageParams, criteria, criteria.OrderBy);
        }

        #endregion

        #region api/customerscores/addscores

        [HttpGet]
        public CustomerScoresModel AddScores(string id)
        {
            Customer customer = CustomerAdapter.Instance.Load(id);
            TeacherJobViewCollection teachers = null;
            string uid = DeluxeIdentity.CurrentUser.ID;
            bool isTeacher = DeluxeIdentity.CurrentUser.GetCurrentJob().JobType == Data.Common.JobTypeDefine.Teacher;
            if (isTeacher)
            {
                teachers = TeacherJobViewAdapter.Instance.Load(where => where.AppendItem("JobID", DeluxeIdentity.CurrentUser.GetCurrentJob().ID));
            }
            else
            {
                teachers = TeacherJobViewAdapter.Instance.Load(where => where.AppendItem("CampusID", customer.CampusID));
            }
            return new CustomerScoresModel
            {
                IsTeacher = isTeacher,
                Customer = customer,
                Teachers = teachers,
                Score = new CustomerScore { ScoreID = UuidHelper.NewUuidString() },
                Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(CustomerScore), typeof(CustomerScoreItem))
            };
        }

        [HttpPost]
        public void AddScores(CustomerScoresModel model)
        {
            AddCustomerScoresExecutor executor = new AddCustomerScoresExecutor(model);
            executor.Execute();
        }

        #endregion

        #region api/customerscores/getScoresInfo

        [HttpGet]
        public CustomerScoresModel GetScoresInfo(string id)
        {
            CustomerScore score = CustomerScoreAdapter.Instance.Load(id);
            Customer customer = CustomerAdapter.Instance.Load(score.CustomerID);
            TeacherJobViewCollection teachers = TeacherJobViewAdapter.Instance.Load(where => where.AppendItem("CampusID", customer.CampusID));
            return new CustomerScoresModel
            {
                Customer = customer,
                Teachers = teachers,
                Score = score,
                ScoreItems = CustomerScoreItemAdapter.Instance.Load(id),
                Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(CustomerScore), typeof(CustomerScoreItem))
            };
        }

        #endregion

        #region api/customerscores/editscores

        [HttpGet]
        public CustomerScoresModel EditScores(string id)
        {
            CustomerScore score = CustomerScoreAdapter.Instance.Load(id);
            Customer customer = CustomerAdapter.Instance.Load(score.CustomerID);
            TeacherJobViewCollection teachers = null;
            string uid = DeluxeIdentity.CurrentUser.ID;
            bool isTeacher = DeluxeIdentity.CurrentUser.GetCurrentJob().JobType == Data.Common.JobTypeDefine.Teacher;
            if (isTeacher)
            {
                teachers = TeacherJobViewAdapter.Instance.Load(where => where.AppendItem("JobID", uid));
            }
            else
            {
                teachers = TeacherJobViewAdapter.Instance.Load(where => where.AppendItem("CampusID", customer.CampusID));
            }

            ConfigArgs args = ConfigsCache.GetArgs(DeluxeIdentity.CurrentUser.GetCurrentJob().GetParentOrganizationByType(DepartmentType.Campus).ID);
            DateTime closingAccountDate = args.GetCurrentClosingAccountDate();

            return new CustomerScoresModel
            {
                IsTeacher = isTeacher,
                ClosingAccountDate = closingAccountDate,
                Customer = customer,
                Teachers = teachers,
                Score = score,
                ScoreItems = CustomerScoreItemAdapter.Instance.Load(id),
                Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(CustomerScore), typeof(CustomerScoreItem))
            };
        }

        [HttpPost]
        public void EditScores(CustomerScoresModel model)
        {
            EditCustomerScoresExecutor executor = new EditCustomerScoresExecutor(model);
            executor.Execute();
        }

        #endregion

        #region api/customerscores/getscoreforbatchadd

        [HttpGet]
        public CustomerScoresModel GetScoreForBatchAdd()
        {
            return new CustomerScoresModel()
            {
                Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(CustomerScore), typeof(CustomerScoreItem))
            };
        }

        [HttpPost]
        public CustomerScoresBatchQueryResult GetScoreForBatchAdd(CustomerScoresQueryCriteriaModel criteria)
        {
            TeacherJobViewCollection teachers = null;
            string uid = DeluxeIdentity.CurrentUser.ID;
            bool isTeacher = DeluxeIdentity.CurrentUser.GetCurrentJob().JobType == Data.Common.JobTypeDefine.Teacher;
            if (isTeacher)
            {
                teachers = TeacherJobViewAdapter.Instance.Load(where => where.AppendItem("JobID", uid));
            }
            else
            {
                teachers = TeacherJobViewAdapter.Instance.Load(where => where.AppendItem("CampusID", DeluxeIdentity.CurrentUser.GetCurrentJob().GetParentOrganizationByType(DepartmentType.Campus).ID));
            }
            return new CustomerScoresBatchQueryResult()
            {
                isTeacher = isTeacher,
                Teachers = teachers,
                QueryResult = CustomerScoresBatchDataSource.Instance.LoadCustomerScores_Batch(criteria.PageParams, criteria, criteria.OrderBy),
                Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(CustomerScore), typeof(CustomerScoreItem))
            };
        }

        [HttpPost]
        public void AddBatchScores(CustomerScoresBatchSearchModelCollection model)
        {
            AddBatchScoresExecutor executor = new AddBatchScoresExecutor(model);
            executor.Execute();
        }

        #endregion

        #region api/customerscores/getscoresforstudent

        [HttpPost]
        public CustomerScoresQueryResult GetScoresForStudent(CustomerScoresQueryCriteriaModel criteria)
        {
            return new CustomerScoresQueryResult
            {
                QueryResult = CustomerScoresDataSource.Instance.LoadCustomerScores(criteria.PageParams, criteria, criteria.OrderBy),
                Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(CustomerScore), typeof(CustomerScoreItem))
            };
        }

        [HttpPost]
        public PagedQueryResult<CustomerScoresSearchModel, CustomerScoresSearchModelCollection> GetPagedScoresForStudent(CustomerScoresQueryCriteriaModel criteria)
        {
            return CustomerScoresDataSource.Instance.LoadCustomerScores(criteria.PageParams, criteria, criteria.OrderBy);
        }

        #endregion
    }
}