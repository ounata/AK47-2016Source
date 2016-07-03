using MCS.Web.MVC.Library.Filters;
using PPTS.Data.Common.Entities;
using PPTS.WebAPI.Orders.ViewModels.Assignment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using MCS.Library.Data;
using PPTS.Data.Orders.DataSources;
using PPTS.Data.Common.Adapters;
using PPTS.Data.Orders.Entities;
using PPTS.Data.Orders.Adapters;
using PPTS.Data.Orders;
using PPTS.WebAPI.Orders.Executors;
using MCS.Library.OGUPermission;
using MCS.Library.Principal;
using PPTS.Data.Common.Security;
using PPTS.Contracts.Customers.Models;
using PPTS.Data.Products;
using PPTS.Web.MVC.Library.Filters;
using PPTS.WebAPI.Orders.Service;

namespace PPTS.WebAPI.Orders.Controllers
{
    [ApiPassportAuthentication]
    public class TeacherAssignmentController : ApiController
    {

        #region api/teacherassignment/getTeacherList

        /// 按教师排课，获取选择教师的列表数据
        [HttpPost]
        [PPTSJobFunctionAuthorize("PPTS:按教师排课-本校区")]
        public TeacherQCR GetTeacherList(TeacherQCM qcm)
        {
            TeacherQCR result = new TeacherQCR();
            result.LoadData(qcm);
            return result;
        }

        /// 按教师排课，获取选择教师的列表数据（支持翻页事件）
        [HttpPost]
        [PPTSJobFunctionAuthorize("PPTS:按教师排课-本校区")]
        public PagedQueryResult<TeacherJobView, TeacherJobViewCollection> GetTeacherListPaged(TeacherQCM qcm)
        {
            TeacherQCR result = new TeacherQCR();
            result.LoadDataPaged(qcm);
            return result.QueryResult;
        }
        
        #endregion

        #region api/teacherassignment/getTeacherWeekCourse
        ///按照教师排课，初始化教师周视图页面数据
        [HttpPost]
        [PPTSJobFunctionAuthorize("PPTS:按教师排课-本校区")]
        public AssignWeekViewModel GetTeacherWeekCourse(TeacherAssignQM qm)
        {
            AssignWeekViewModel result = new AssignWeekViewModel();
            result.LoadDataByTeacher(qm);
            return result;
        }
        ///按照教师排课，为周视图页面获取指定教师当前月排课统计数据
        [HttpPost]
        [PPTSJobFunctionAuthorize("PPTS:按教师排课-本校区")]
        public dynamic GetCurMonthStat(TeacherAssignQM qm)
        {
            DateTime sdate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime edate = new DateTime(DateTime.Now.Year, DateTime.Now.Month + 1, 1);
            AssignCollection ac = AssignsAdapter.Instance.LoadCollection(AssignTypeDefine.ByTeacher, qm.TeacherJobID, sdate, edate, false);
            string teacherName = string.Empty;
            if (ac.Count > 0)
                teacherName = ac[0].TeacherName;
               
            var c1 = ac.Where(p => p.AssignStatus == AssignStatusDefine.Assigned).Count();
            var c2 = ac.Where(p => p.AssignStatus == AssignStatusDefine.Finished).Count();
            var result = new { TeacherName = teacherName, AssignedCourse = c1, FinishedCourse = c2 };
            return result;
        }
        #endregion

        #region api/teacherassignment/createAssign
        ///按教师排课，新增排课时，初始化排课页面数据
        [HttpPost]
        [PPTSJobFunctionAuthorize("PPTS:按钮-新增/取消/复制/调动课表-本校区")]
        public AssignByTeacherModel InitCreateAssign(TeacherAssignQM qm)
        {
            AssignByTeacherModel result = new AssignByTeacherModel();
            result.LoadData(qm);
            return result;
        }
     
        ///按教师排课，当学员下拉框发生改变时，加载对应资产
        ///1. 指定了科目和年级的资产 2. 通用资产，没有具体指定科目或者年级
        [HttpPost]
        [PPTSJobFunctionAuthorize("PPTS:按钮-新增/取消/复制/调动课表-本校区")]
        public SimpleAssetViewCollection GetAssetByCustomerID(CrumbsQM qm)
        {
            string customerID = qm.CustomerID;
            AssetViewCollection avm = AssetViewAdapter.Instance.LoadCollection(customerID);
            return new SimpleAssetViewCollection { Result = avm };
        }

        ///按教师排课，当选择已经存在的排课条件时，根据资产ID加载资产
        [HttpPost]
        [PPTSJobFunctionAuthorize("PPTS:按钮-新增/取消/复制/调动课表-本校区")]
        public SimpleAssetView GetAssetByAssetID(CrumbsQM queryModel)
        {
            string customerID = queryModel.CustomerID;
            string assetID = queryModel.AssetID;

            AssetView avm = AssetViewAdapter.Instance.Load(customerID, assetID);
            return new SimpleAssetView { Result = avm };
        }

        ///保存排课条件
        [HttpPost]
        [PPTSJobFunctionAuthorize("PPTS:按钮-新增/取消/复制/调动课表-本校区")]
        public dynamic CreateAssign(AssignSuperModel asm)
        {
            AssignAddExecutor ac = new AssignAddExecutor(asm);
            var result = ac.Execute();
            AssignTaskService.UpdateCustomerSearchInfo(ac.Model.CustomerID);
            return new { assignID = ac.Model.AssignID };
        }
        #endregion

        #region api/teacherassignment/cancelAssign

        /// 按教师排课，取消排课
        [HttpPost]
        [PPTSJobFunctionAuthorize("PPTS:按钮-新增/取消/复制/调动课表-本校区")]
        public void CancelAssign(AssignCollection model)
        {
            AssignCancelExecutor executor = new AssignCancelExecutor(model);
            executor.Execute();
            AssignTaskService.UpdateCustomerSearchInfo(executor.CustomerIDTask);
        }
        
        #endregion

        #region api/teacherassignment/copyAssign
        /// 按教师排课,复制排课
        [HttpPost]
        [PPTSJobFunctionAuthorize("PPTS:按钮-新增/取消/复制/调动课表-本校区")]
        public dynamic CopyAssign(AssignCopyQM qm)
        {
            qm.CustomerID = string.Empty;
            qm.SrcDateStart = qm.SrcDateStart.Date;
            qm.SrcDateEnd = qm.SrcDateEnd.Date;
            qm.DestDateStart = qm.DestDateStart.Date;
            qm.DestDateEnd = qm.DestDateEnd.Date;
            AssignCopyExecutor ace = new AssignCopyExecutor(qm);
            ace.Execute();
            AssignTaskService.UpdateCustomerSearchInfo(ace.CustomerIDTask);
            return new { Msg = ace.Msg };
        }
        #endregion

        #region api/teacherassignment/resetAssign

        ///按教师排课 调整课表获取初始化数据
        [HttpPost]
        [PPTSJobFunctionAuthorize("PPTS:按钮-新增/取消/复制/调动课表-本校区")]
        public dynamic InitResetAssign()
        {
            var allowDateTime = DateTime.Now.Date.AddDays(10);
            var result = new
            {
                AllowDateTime = allowDateTime,
            };
            return result;
        }

        ///按教师排课, 调整课表
        [HttpPost]
        [PPTSJobFunctionAuthorize("PPTS:按钮-新增/取消/复制/调动课表-本校区")]
        public void ResetAssign(IList<AssignResetQM> qm)
        {
            AssignResetExecutor executor = new AssignResetExecutor(qm);
            executor.Execute();
        }
        #endregion

        #region api/studentassignment/getSCLV

        /// 按教师排课，列表视图， 获取排课列表视图数据
        [HttpPost]
        [PPTSJobFunctionAuthorize("PPTS:按教师排课-本校区")]
        public AssignQCR GetSCLV(AssignQCM qcm)
        {
            qcm.CustomerID = string.Empty;
            qcm.TeacherName = string.Empty;
            var och = new OrderCommonHelper();
            och.GetCourseConditionAssign(qcm);

            Dictionary<string, IEnumerable<BaseConstantEntity>> dic = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(Data.Orders.Entities.Assign));
            ///处理查询条件
            och.GetProductCategoryType(dic);

            AssignQCR result = new AssignQCR
            {
                QueryResult = GenericOrderDataSource<Data.Orders.Entities.Assign, AssignCollection>.Instance.Query(qcm.PageParams, qcm, qcm.OrderBy),
                Dictionaries = dic
            };
            return result;
        }

        /// 按教师排课，列表视图， 获取排课列表视图数据(分页事件)
        [HttpPost]
        [PPTSJobFunctionAuthorize("PPTS:按教师排课-本校区")]
        public PagedQueryResult<Data.Orders.Entities.Assign, AssignCollection> GetPagedSCLV(AssignQCM qcm)
        {
            qcm.CustomerID = string.Empty;
            qcm.TeacherName = string.Empty;
            var och = new OrderCommonHelper();
            och.GetCourseConditionAssign(qcm);

            return GenericOrderDataSource<Data.Orders.Entities.Assign, AssignCollection>.Instance.Query(qcm.PageParams, qcm, qcm.OrderBy);
        }

        #endregion

    }
}