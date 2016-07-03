using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MCS.Library.Data;
using PPTS.Data.Orders.Entities;
using PPTS.Data.Orders;
using PPTS.Data.Orders.DataSources;
using PPTS.Data.Orders.Adapters;
using PPTS.Data.Common.Adapters;
using PPTS.WebAPI.Orders.ViewModels.CustomerSearchNS;
using PPTS.WebAPI.Orders.ViewModels.Assignment;
using PPTS.WebAPI.Orders.Executors;
using MCS.Web.MVC.Library.Filters;
using PPTS.Data.Common.Security;
using MCS.Library.Principal;
using MCS.Library.OGUPermission;
using PPTS.Data.Common.Entities;
using MCS.Library.Core;
using PPTS.Data.Products;
using PPTS.Web.MVC.Library.Filters;
using PPTS.WebAPI.Orders.Service;

namespace PPTS.WebAPI.Orders.Controllers
{
    [ApiPassportAuthentication]
    public class StudentAssignmentController : ApiController
    {
        #region api/studentassignment/getAllStudentAssignment

        /// 获取学员待排课列表
        [HttpPost]
        [PPTSJobFunctionAuthorize("PPTS:按学员排课-本校区")]
        public CustomerSearchQCR GetAllStudentAssignment(CustomerSearchQCM qCM)
        {
            CustomerSearchQCR result = new CustomerSearchQCR();
            result.LoadData(qCM);
            return result;
        }

        /// 获取学员待排课列表，分页获取
        [HttpPost]
        [PPTSJobFunctionAuthorize("PPTS:按学员排课-本校区")]
        public PagedQueryResult<CustomerSearch, CustomerSearchCollection> GetPagedStudentAssignment(CustomerSearchQCM qCM)
        {
            IOrganization org = DeluxeIdentity.CurrentUser.GetCurrentJob().GetParentOrganizationByType(DepartmentType.Campus);
            if (org == null)
                return new PagedQueryResult<CustomerSearch, CustomerSearchCollection>();
            qCM.CampusID = org.ID;
            return GenericSearchDataSource<CustomerSearch, CustomerSearchCollection>.Instance.Query(qCM.PageParams, qCM, qCM.OrderBy);
        }

        #endregion

        #region api/studentassignment/getStudentWeekCourse

        /// 按照学员排课，初始化学员周视图页面数据，并有查询
        [HttpPost]
        [PPTSJobFunctionAuthorize("PPTS:按学员排课-本校区,")]
        public AssignWeekViewModel GetStudentWeekCourse(StudentAssignQM qm)
        {
            AssignWeekViewModel result = new AssignWeekViewModel();
            result.LoadDataByStudent(qm);
            return result;
        }

        /// 按照学员排课，为周视图页面，获取指定学员当前月排课统计数据
        [HttpPost]
        [PPTSJobFunctionAuthorize("PPTS:按学员排课-本校区")]
        public dynamic GetCurMonthStat(StudentAssignQM acQM)
        {
            DateTime sdate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime edate = new DateTime(DateTime.Now.Year, DateTime.Now.Month + 1, 1);
            AssignCollection ac = AssignsAdapter.Instance.LoadCollection(AssignTypeDefine.ByStudent, acQM.CustomerID, sdate, edate, false);
            string customerName = string.Empty;
            if (ac.Count > 0)
                customerName = ac[0].CustomerName;
            else
            {
                Data.Customers.Entities.Customer customer = PPTS.WebAPI.Orders.Service.CustomerService.GetCustomerByCustomerId(acQM.CustomerID);
                if (customer != null)
                    customerName = customer.CustomerName;
            }
            var c1 = ac.Where(p => p.AssignStatus == AssignStatusDefine.Assigned).Count();
            var c2 = ac.Where(p => p.AssignStatus == AssignStatusDefine.Finished).Count();
            var result = new { CustomerName = customerName, AssignedCourse = c1, FinishedCourse = c2 };
            return result;
        }

        #endregion

        #region api/studentassignment/createAssign

        ///按照学员排课，新增排课时，初始化排课页面数据
        [HttpPost]
        [PPTSJobFunctionAuthorize("PPTS:按钮-新增/取消/复制/调动课表-本校区")]
        public AssignByStudentModel GetAssignCondition(StudentAssignQM qm)
        {
            AssignByStudentModel result = new AssignByStudentModel();
            result.LoadData(qm);
            return result;
        }

        ///保存排课
        [HttpPost]
        [PPTSJobFunctionAuthorize("PPTS:按钮-新增/取消/复制/调动课表-本校区")]
        public dynamic CreateAssignCondition(AssignSuperModel asm)
        {
            AssignAddExecutor ac = new AssignAddExecutor(asm);
            var result = ac.Execute();
            AssignTaskService.UpdateCustomerSearchInfo(ac.Model.CustomerID);
            return new { assignID = ac.Model.AssignID };
        }
        #endregion

        #region api/studentassignment/cancelAssign
        /// 按学员排课，取消排课
        [HttpPost]
        [PPTSJobFunctionAuthorize("PPTS:按钮-新增/取消/复制/调动课表-本校区")]
        public void CancelAssign(AssignCollection model)
        {
            AssignCancelExecutor executor = new AssignCancelExecutor(model);
            executor.Execute();
            AssignTaskService.UpdateCustomerSearchInfo(executor.CustomerIDTask);
        }
        #endregion

        #region api/studentassignment/copyAssign
        /// 按学员排课,复制排课
        [HttpPost]
        [PPTSJobFunctionAuthorize("PPTS:按钮-新增/取消/复制/调动课表-本校区")]
        public void CopyAssign(AssignCopyQM queryModel)
        {
            queryModel.TeacherID = string.Empty;
            queryModel.SrcDateStart = queryModel.SrcDateStart.Date;
            queryModel.SrcDateEnd = queryModel.SrcDateEnd.Date;
            queryModel.DestDateStart = queryModel.DestDateStart.Date;
            queryModel.DestDateEnd = queryModel.DestDateEnd.Date;
            AssignCopyExecutor ace = new AssignCopyExecutor(queryModel);
            ace.Execute();
            AssignTaskService.UpdateCustomerSearchInfo(ace.CustomerIDTask);
        }
        #endregion

        #region api/studentassignment/resetAssign

        /// 按学员排课 调整课表获取初始化数据
        [HttpPost]
        [PPTSJobFunctionAuthorize("PPTS:按钮-新增/取消/复制/调动课表-本校区")]
        public dynamic ResetAssignInit()
        {
            var allowDateTime = DateTime.Now.Date.AddDays(10);
            var result = new
            {
                AllowDateTime = allowDateTime
            };
            return result;
        }

        /// 按学员排课, 调整课表
        [HttpPost]
        [PPTSJobFunctionAuthorize("PPTS:按钮-新增/取消/复制/调动课表-本校区")]
        public void ResetAssign(IList<AssignResetQM> queryModel)
        {
            AssignResetExecutor executor = new AssignResetExecutor(queryModel);
            executor.Execute();
        }
        #endregion

        #region api/studentassignment/getSCLV

        /// 按学员排课，列表视图， 获取排课列表视图数据
        [HttpPost]
        [PPTSJobFunctionAuthorize("PPTS:按学员排课-本校区")]
        public AssignQCR GetSCLV(AssignQCM criteria)
        {
            criteria.TeacherID = string.Empty;
            criteria.TeacherJobID = string.Empty;
            criteria.CustomerName = string.Empty;

            OrderCommonHelper och = new OrderCommonHelper();
            och.GetCourseConditionAssign(criteria);

            Dictionary<string, IEnumerable<BaseConstantEntity>> dic = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(Data.Orders.Entities.Assign));
            ///处理查询条件
            och.GetProductCategoryType(dic);

            AssignQCR result = new AssignQCR
            {
                QueryResult = GenericOrderDataSource<Data.Orders.Entities.Assign, AssignCollection>.Instance.Query(criteria.PageParams, criteria, criteria.OrderBy),
                Dictionaries = dic
            };
            return result;
        }
        /// 按学员排课，列表视图， 获取排课列表视图数据(分页事件)
        [HttpPost]
        [PPTSJobFunctionAuthorize("PPTS:按学员排课-本校区")]
        public PagedQueryResult<Data.Orders.Entities.Assign, AssignCollection> GetPagedSCLV(AssignQCM criteria)
        {
            criteria.TeacherID = string.Empty;
            criteria.TeacherJobID = string.Empty;
            criteria.CustomerName = string.Empty;
            new OrderCommonHelper().GetCourseConditionAssign(criteria);
            return GenericOrderDataSource<Data.Orders.Entities.Assign, AssignCollection>.Instance.Query(criteria.PageParams, criteria, criteria.OrderBy);
        }

        #endregion

        #region api/studentassignment/getACC

        /// 学员视图，排课条件列表  
        [HttpPost]
        [PPTSJobFunctionAuthorize("PPTS:按钮-修改排课条件/删除排课条件-本校区")]
        public AssignConditionQCR GetACC(AssignConditionQCM criteriaQCM)
        {
            return new AssignConditionQCR()
            {
                QueryResult = GenericOrderDataSource<AssignCondition, AssignConditionCollection>.Instance.Query(criteriaQCM.PageParams, criteriaQCM, criteriaQCM.OrderBy)
            };
        }

        [HttpPost]
        [PPTSJobFunctionAuthorize("PPTS:按钮-修改排课条件/删除排课条件-本校区")]
        public PagedQueryResult<AssignCondition, AssignConditionCollection> GetACCPaged(AssignConditionQCM criteriaQCM)
        {
            return GenericOrderDataSource<AssignCondition, AssignConditionCollection>.Instance.Query(criteriaQCM.PageParams, criteriaQCM, criteriaQCM.OrderBy);
        }

        [HttpPost]
        [PPTSJobFunctionAuthorize("PPTS:按钮-修改排课条件/删除排课条件-本校区")]
        public AssignConditionEx InitEditACC(ACCEditQM queryModel)
        {
            queryModel.NullCheck("queryModel");
            ///当前操作人所属校区ID
            IOrganization org = DeluxeIdentity.CurrentUser.GetCurrentJob().GetParentOrganizationByType(DepartmentType.Campus);
            if (org == null)
            {
                return new AssignConditionEx();
            }
            string operaterCampusID = org.ID;
            var acc = AssignConditionAdapter.Instance.Load(queryModel.AccID);
            acc.CustomerName = acc.ConditionName4Teacher.Split("-".ToCharArray())[2];
            AssetViewCollection avm = AssetViewAdapter.Instance.LoadCollection(queryModel.CustomerID);
            var dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(Data.Orders.Entities.Assign));
            ///从服务中获取学员指定的教师列表
            TeacherModel teacher = new OrderCommonHelper().GetTeacher(queryModel.CustomerID, operaterCampusID, dictionaries);

            return new AssignConditionEx()
            {
                CampusName = org.Name,
                ACC = acc,
                AVC = avm,
                Teacher = teacher
            };
        }

        [HttpPost]
        [PPTSJobFunctionAuthorize("PPTS:按钮-修改排课条件/删除排课条件-本校区")]
        public dynamic SaveAssignConditon(AssignCondition acc)
        {
            acc.NullCheck("AssignCondition");
            string msg = "ok";
            try
            {
                acc.ConditionName4Customer = string.Format("{0}-{1}-{2}-{3}", acc.AssetCode, acc.SubjectName, acc.TeacherName, acc.GradeName);
                acc.ConditionName4Teacher = string.Format("{0}-{1}-{2}-{3}", acc.AssetCode, acc.SubjectName, acc.CustomerName, acc.GradeName);
                AssignConditionAdapter.Instance.Update(acc);
            }
            catch (Exception error)
            {
                msg = error.StackTrace.ToString();
            }
            return new { Msg = msg };
        }

        [HttpPost]
        [PPTSJobFunctionAuthorize("PPTS:按钮-修改排课条件/删除排课条件-本校区")]
        public dynamic DeleteAssignCondition(AssignConditionCollection acc)
        {
            acc.NullCheck("AssignConditionCollection");
            string msg = "ok";
            try
            {
                AssignConditionAdapter.Instance.DeleteCollection(acc);
            }
            catch (Exception error)
            {
                msg = error.StackTrace.ToString();
            }
            return new { Msg = msg };
        }


        #endregion

    }
}
