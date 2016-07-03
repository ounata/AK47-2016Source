using MCS.Library.Core;
using MCS.Library.Data;
using MCS.Library.Office.OpenXml.Excel;
using MCS.Library.Principal;
using MCS.Web.MVC.Library.ApiCore;
using MCS.Web.MVC.Library.Filters;
using MCS.Web.MVC.Library.ModelBinder;
using PPTS.Data.Common;
using PPTS.Data.Common.Adapters;
using PPTS.Data.Common.Entities;
using PPTS.Data.Common.Security;
using PPTS.Data.Customers.Adapters;
using PPTS.Data.Customers.Entities;
using PPTS.Web.MVC.Library.Filters;
using PPTS.WebAPI.Customers.DataSources;
using PPTS.WebAPI.Customers.Executors;
using PPTS.WebAPI.Customers.ViewModels.CustomerScores;
using PPTS.WebAPI.Customers.ViewModels.PotentialCustomers;
using System;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;

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
        [PPTSJobFunctionAuthorize("PPTS:成绩管理（学员视图-成绩）,成绩管理（学员视图-成绩、成绩详情）-本部门,成绩管理（学员视图-成绩、成绩详情）-本校区,成绩管理（学员视图-成绩、成绩详情）-本分公司,成绩管理（学员视图-成绩、成绩详情）-全国")]
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
        [PPTSJobFunctionAuthorize("PPTS:成绩管理（学员视图-成绩）,成绩管理（学员视图-成绩、成绩详情）-本部门,成绩管理（学员视图-成绩、成绩详情）-本校区,成绩管理（学员视图-成绩、成绩详情）-本分公司,成绩管理（学员视图-成绩、成绩详情）-全国")]
        public PagedQueryResult<CustomerScoresSearchModel, CustomerScoresSearchModelCollection> GetPagedScores(CustomerScoresQueryCriteriaModel criteria)
        {
            return CustomerScoresDataSource.Instance.LoadCustomerScores(criteria.PageParams, criteria, criteria.OrderBy);
        }

        #endregion

        #region api/customerscores/addscores

        [HttpGet]
        [PPTSJobFunctionAuthorize("PPTS:录入/批量录入/编辑成绩")]
        public CustomerScoresModel AddScores(string id)
        {
            Customer customer = CustomerAdapter.Instance.Load(id);
            TeacherSearchCollection teachers = null;
            bool isTeacher = DeluxeIdentity.CurrentUser.GetCurrentJob().JobType == Data.Common.JobTypeDefine.Teacher;
            if (isTeacher)
            {
                teachers = TeacherSearchAdapter.Instance.Load(customer.CustomerID, DeluxeIdentity.CurrentUser.ID);
            }
            else
            {
                teachers = TeacherSearchAdapter.Instance.Load(customer.CustomerID);
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
        [PPTSJobFunctionAuthorize("PPTS:录入/批量录入/编辑成绩")]
        public void AddScores(CustomerScoresModel model)
        {
            AddCustomerScoresExecutor executor = new AddCustomerScoresExecutor(model);
            executor.Execute();
        }

        #endregion

        #region api/customerscores/getScoresInfo

        [HttpGet]
        [PPTSJobFunctionAuthorize("PPTS:成绩管理（学员视图-成绩）,成绩管理（学员视图-成绩、成绩详情）-本部门,成绩管理（学员视图-成绩、成绩详情）-本校区,成绩管理（学员视图-成绩、成绩详情）-本分公司,成绩管理（学员视图-成绩、成绩详情）-全国")]
        public CustomerScoresModel GetScoresInfo(string id)
        {
            CustomerScore score = CustomerScoreAdapter.Instance.Load(id);
            Customer customer = CustomerAdapter.Instance.Load(score.CustomerID);
            TeacherSearchCollection teachers = TeacherSearchAdapter.Instance.Load(score.CustomerID);
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
        [PPTSJobFunctionAuthorize("PPTS:录入/批量录入/编辑成绩")]
        public CustomerScoresModel EditScores(string id)
        {
            CustomerScore score = CustomerScoreAdapter.Instance.Load(id);
            Customer customer = CustomerAdapter.Instance.Load(score.CustomerID);
            TeacherSearchCollection teachers = null;
            string uid = DeluxeIdentity.CurrentUser.ID;
            // bool isTeacher = DeluxeIdentity.CurrentUser.GetCurrentJob().JobType == Data.Common.JobTypeDefine.Teacher;
            bool isTeacher = DeluxeIdentity.CurrentUser.GetCurrentJob() ==null ? false : DeluxeIdentity.CurrentUser.GetCurrentJob().JobType == JobTypeDefine.Teacher;
            if (isTeacher)
            {
                teachers = TeacherSearchAdapter.Instance.Load(customer.CustomerID, DeluxeIdentity.CurrentUser.ID);
            }
            else
            {
                teachers = TeacherSearchAdapter.Instance.Load(customer.CustomerID);
            }

            // ConfigArgs args = ConfigsCache.GetArgs(DeluxeIdentity.CurrentUser.GetCurrentJob().GetParentOrganizationByType(DepartmentType.Campus).ID);
            // DateTime closingAccountDate = args.GetCurrentClosingAccountDate();

            return new CustomerScoresModel
            {
                IsTeacher = isTeacher,
                // ClosingAccountDate = closingAccountDate,
                Customer = customer,
                Teachers = teachers,
                Score = score,
                ScoreItems = CustomerScoreItemAdapter.Instance.Load(id),
                Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(CustomerScore), typeof(CustomerScoreItem))
            };
        }

        [HttpPost]
        [PPTSJobFunctionAuthorize("PPTS:录入/批量录入/编辑成绩")]
        public void EditScores(CustomerScoresModel model)
        {
            EditCustomerScoresExecutor executor = new EditCustomerScoresExecutor(model);
            executor.Execute();
        }

        #endregion

        #region api/customerscores/getscoreforbatchadd

        [HttpGet]
        [PPTSJobFunctionAuthorize("PPTS:录入/批量录入/编辑成绩")]
        public CustomerScoresModel GetScoreForBatchAdd()
        {
            return new CustomerScoresModel()
            {
                Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(CustomerScore), typeof(CustomerScoreItem))
            };
        }

        [HttpPost]
        [PPTSJobFunctionAuthorize("PPTS:录入/批量录入/编辑成绩")]
        public CustomerScoresBatchQueryResult GetScoreForBatchAdd(CustomerScoresQueryCriteriaModel criteria)
        {
            return new CustomerScoresBatchQueryResult()
            {
                isTeacher = DeluxeIdentity.CurrentUser.GetCurrentJob().JobType == JobTypeDefine.Teacher,
                QueryResult = CustomerScoresBatchDataSource.Instance.LoadCustomerScores_Batch(criteria.PageParams, criteria, criteria.OrderBy),
                Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(CustomerScore), typeof(CustomerScoreItem))
            };
        }

        [HttpPost]
        [PPTSJobFunctionAuthorize("PPTS:录入/批量录入/编辑成绩")]
        public void AddBatchScores(CustomerScoresBatchSearchModelCollection model)
        {
            AddBatchScoresExecutor executor = new AddBatchScoresExecutor(model);
            executor.Execute();
        }

        #endregion

        #region api/customerscores/getscoresforstudent

        [HttpPost]
        [PPTSJobFunctionAuthorize("PPTS:成绩管理（学员视图-成绩）,成绩管理（学员视图-成绩、成绩详情）-本部门,成绩管理（学员视图-成绩、成绩详情）-本校区,成绩管理（学员视图-成绩、成绩详情）-本分公司,成绩管理（学员视图-成绩、成绩详情）-全国")]
        public CustomerScoresQueryResult GetScoresForStudent(CustomerScoresQueryCriteriaModel criteria)
        {
            return new CustomerScoresQueryResult
            {
                QueryResult = CustomerScoresDataSource.Instance.LoadCustomerScores(criteria.PageParams, criteria, criteria.OrderBy),
                Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(CustomerScore), typeof(CustomerScoreItem))
            };
        }

        [HttpPost]
        [PPTSJobFunctionAuthorize("PPTS:成绩管理（学员视图-成绩）,成绩管理（学员视图-成绩、成绩详情）-本部门,成绩管理（学员视图-成绩、成绩详情）-本校区,成绩管理（学员视图-成绩、成绩详情）-本分公司,成绩管理（学员视图-成绩、成绩详情）-全国")]
        public PagedQueryResult<CustomerScoresSearchModel, CustomerScoresSearchModelCollection> GetPagedScoresForStudent(CustomerScoresQueryCriteriaModel criteria)
        {
            return CustomerScoresDataSource.Instance.LoadCustomerScores(criteria.PageParams, criteria, criteria.OrderBy);
        }

        #endregion

        #region api/customerscores/exportallScores

        [HttpPost]
        [PPTSJobFunctionAuthorize("PPTS:导出成绩")]
        public HttpResponseMessage ExportAllScores([ModelBinder(typeof(FormBinder))] CustomerScoresQueryCriteriaModel criteria)
        {
            criteria.PageParams.PageIndex = 0;
            criteria.PageParams.PageSize = 5000;
            PagedQueryResult<CustomerScoresSearchModel, CustomerScoresSearchModelCollection> queryResult = CustomerScoresDataSource.Instance.LoadCustomerScores(criteria.PageParams, criteria, criteria.OrderBy);

            WorkBook wb = WorkBook.CreateNew();
            WorkSheet sheet = wb.Sheets["sheet1"];
            TableDescription tableDesp = new TableDescription("成绩管理");
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("校区", typeof(string))) { PropertyName = "CampusName" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("学年度", typeof(string))) { PropertyName = "StudyYear" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("学员编号", typeof(string))) { PropertyName = "CustomerCode" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("学员姓名", typeof(string))) { PropertyName = "CustomerName" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("考试年级", typeof(string))) { PropertyName = "ScoreGrade" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("学期", typeof(double))) { PropertyName = "StudyTerm" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("考试类型", typeof(string))) { PropertyName = "ScoreType" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("考试月份", typeof(string))) { PropertyName = "ExamineMonth" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("科目", typeof(string))) { PropertyName = "Subject" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("得分", typeof(decimal))) { PropertyName = "RealScore" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("卷面分", typeof(decimal))) { PropertyName = "PaperScore" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("班级人数", typeof(int))) { PropertyName = "ClassPeoples" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("班级名次", typeof(int))) { PropertyName = "ClassRank" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("年级名次", typeof(int))) { PropertyName = "GradeRank" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("任课教师", typeof(string))) { PropertyName = "TeacherName" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("教师OA", typeof(string))) { PropertyName = "TeacherOACode" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("是否在学大辅导", typeof(string))) { PropertyName = "IsStudyHere" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("家长满意度", typeof(string))) { PropertyName = "Satisficing" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("学管师", typeof(string))) { PropertyName = "EducatorName" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("咨询师", typeof(string))) { PropertyName = "ConstantStaffName" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("当前状态", typeof(string))) { PropertyName = "StudentType" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("学员类型", typeof(string))) { PropertyName = "StudentType" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("录取院校类别", typeof(string))) { PropertyName = "AdmissionType" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("属985或211院校", typeof(string))) { PropertyName = "IsKeyCollege" });

            var dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(CustomerScore), typeof(CustomerScoreItem));

            sheet.LoadFromCollection(queryResult.PagedData, tableDesp, (cell, param) =>
            {
                if (param.PropertyValue != null && !string.IsNullOrEmpty(param.PropertyValue.ToString()))
                {
                    switch (param.ColumnDescription.PropertyName)
                    {
                        case "StudyYear":
                            var studyYear = dictionaries["C_CODE_ABBR_Customer_StudyYear"].Where(c => c.Key == Convert.ToString(param.PropertyValue));
                            cell.Value = null != studyYear ? (null == studyYear.FirstOrDefault() ? null : studyYear.FirstOrDefault().Value) : null;
                            break;
                        case "ScoreGrade":
                            var scoreGrade = dictionaries["C_CODE_ABBR_CUSTOMER_GRADE"].Where(c => c.Key == Convert.ToString(param.PropertyValue));
                            cell.Value = null != scoreGrade ? (null == scoreGrade.FirstOrDefault() ? null : scoreGrade.FirstOrDefault().Value) : null;
                            break;
                        case "StudyTerm":
                            var studyTerm = dictionaries["C_CODE_ABBR_Customer_StudyTerm"].Where(c => c.Key == Convert.ToString(param.PropertyValue));
                            cell.Value = null != studyTerm ? (null == studyTerm.FirstOrDefault() ? null : studyTerm.FirstOrDefault().Value) : null;
                            break;
                        case "ScoreType":
                            var scoreType = dictionaries["C_Code_Abbr_BO_Customer_GradeTypeExt"].Where(c => c.Key == Convert.ToString(param.PropertyValue));
                            cell.Value = null != scoreType ? (null == scoreType.FirstOrDefault() ? null : scoreType.FirstOrDefault().Value) : null;
                            break;
                        case "ExamineMonth":
                            var examineMonth = dictionaries["C_CODE_ABBR_Exam_Month"].Where(c => c.Key == Convert.ToString(param.PropertyValue));
                            cell.Value = null != examineMonth ? (null == examineMonth.FirstOrDefault() ? null : examineMonth.FirstOrDefault().Value) : null;
                            break;
                        case "Subject":
                            var subject = dictionaries["C_CODE_ABBR_Customer_Exam_Subject"].Where(c => c.Key == Convert.ToString(param.PropertyValue));
                            cell.Value = null != subject ? (null == subject.FirstOrDefault() ? null : subject.FirstOrDefault().Value) : null;
                            break;
                        case "IsStudyHere":
                            cell.Value = Convert.ToBoolean(param.PropertyValue) ? "是" : "否";
                            break;
                        case "Satisficing":
                            cell.Value = param.PropertyValue.ToString() == "1" ? "是" : "否";
                            break;
                        case "StudentType":
                            var studentType = dictionaries["C_CODE_ABBR_Exam_Customer_Type"].Where(c => c.Key == Convert.ToString(param.PropertyValue));
                            cell.Value = null != studentType ? (null == studentType.FirstOrDefault() ? null : studentType.FirstOrDefault().Value) : null;
                            break;
                        case "AdmissionType":
                            var admissionType = dictionaries["C_CODE_ABBR_Exam_Customer_Type"].Where(c => c.Key == Convert.ToString(param.PropertyValue));
                            cell.Value = null != admissionType ? (null == admissionType.FirstOrDefault() ? null : admissionType.FirstOrDefault().Value) : null;
                            break;
                        case "IsKeyCollege":
                            cell.Value = param.PropertyValue.ToString() == "1" ? "是" : "否";
                            break;
                        default:
                            cell.Value = param.PropertyValue;
                            break;
                    }
                }
            });

            return wb.ToResponseMessage(string.Format("成绩管理_{0}.xlsx", DateTime.Now.ToString("yyyyMMddHHmmss")));
        }

        #endregion
    }
}