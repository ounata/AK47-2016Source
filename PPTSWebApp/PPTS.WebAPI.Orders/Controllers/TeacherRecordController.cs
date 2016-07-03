using MCS.Library.Core;
using MCS.Library.Data;
using MCS.Library.OGUPermission;
using MCS.Library.Principal;
using MCS.Web.MVC.Library.Filters;
using PPTS.Data.Common.Adapters;
using PPTS.Data.Common.Entities;
using PPTS.Data.Common.Security;
using PPTS.Data.Orders;
using PPTS.Data.Orders.Adapters;
using PPTS.Data.Orders.DataSources;
using PPTS.Data.Orders.Entities;
using PPTS.Data.Products;
using PPTS.Web.MVC.Library.Filters;
using PPTS.WebAPI.Orders.Executors;
using PPTS.WebAPI.Orders.Service;
using PPTS.WebAPI.Orders.ViewModels.Assignment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace PPTS.WebAPI.Orders.Controllers
{
    public partial class TeacherCourseController
    {

        #region api/teachercourse/getTchClassRecord
        ///教师  上课记录查询列表
        [HttpPost]
        [ApiPassportAuthentication]
        [PPTSJobFunctionAuthorize("PPTS:教师上课记录（打印上课记录）,教师上课记录（打印上课记录）-本部门,教师上课记录（打印上课记录）-本校区,教师上课记录（打印上课记录）-本分公司,教师上课记录（打印）-全国")]
        public ClassRecordTeacherModel GetTchClassRecord(AssignQCM qcm)
        {
            ClassRecordTeacherModel result = new ClassRecordTeacherModel();
            result.LoadData(qcm);
            return result;
        }

        ///教师  上课记录查询列表翻页事件
        [HttpPost]
        [ApiPassportAuthentication]
        [PPTSJobFunctionAuthorize("PPTS:教师上课记录（打印上课记录）,教师上课记录（打印上课记录）-本部门,教师上课记录（打印上课记录）-本校区,教师上课记录（打印上课记录）-本分公司,教师上课记录（打印）-全国")]
        public PagedQueryResult<AssignView, AssignViewCollection> GetTchClassRecordPaged(AssignQCM qcm)
        {
            //如果当前操作人
            if (DeluxeIdentity.CurrentUser.GetCurrentJob().JobType == Data.Common.JobTypeDefine.Teacher)
            {
                qcm.TeacherJobID = DeluxeIdentity.CurrentUser.GetCurrentJob().ID;
                IOrganization org = DeluxeIdentity.CurrentUser.GetCurrentJob().GetParentOrganizationByType(DepartmentType.Campus);
                qcm.CampusID = new string[] { org.ID };
            }
            var och = new OrderCommonHelper();
            och.GetCourseCondition(qcm);
            return GenericOrderDataSource<Data.Orders.Entities.AssignView, AssignViewCollection>.Instance.Query(qcm.PageParams, qcm, qcm.OrderBy);
        }

        #endregion

        #region api/teachercourse/addAccompanion

        /*初始化录入陪读课时记录界面数据*/
        [HttpPost]
        [ApiPassportAuthentication]
        [PPTSJobFunctionAuthorize("PPTS:按钮-录入/删除/确认课时-本校区")]
        public dynamic InitAccompanion()
        {
            IOrganization organ = DeluxeIdentity.CurrentUser.GetCurrentJob().GetParentOrganizationByType(DepartmentType.Campus);
            if (organ == null)
            {
                return null;
            }
            return new
            {
                CampusID = organ.ID,
                CampusName = organ.Name,
                Accompanion = new AccompanyAssign() { CampusID = organ.ID, CampusName = organ.Name }
            };
        }

        //添加陪读记录
        [HttpPost]
        [ApiPassportAuthentication]
        [PPTSJobFunctionAuthorize("PPTS:按钮-录入/删除/确认课时-本校区")]
        public dynamic AddAccompanion(AccompanyAssign aa)
        {
            aa.NullCheck("AccompanyAssign");

            aa.AssignID = UuidHelper.NewUuidString();
            aa.AssignStatus = Data.Orders.AssignStatusDefine.Finished;

            aa.FillCreator();
            aa.FillModifier();

            string msg = "ok";

            try
            {
                AccompanyAssignsAdapter.Instance.Update(aa);
            }
            catch (Exception error)
            {
                msg = error.StackTrace.ToString();
            }

            return new { Msg = msg };
        }

        #endregion

        #region api/teachercourse/markupAssign

        ///教师上课记录 录入学科课时
        [HttpPost]
        [ApiPassportAuthentication]
        [PPTSJobFunctionAuthorize("PPTS:按钮-录入/删除/确认课时-本校区")]
        public dynamic MarkupAssign(AssignSuperModel asm)
        {
            AssignMarkupExecutor ac = new AssignMarkupExecutor(asm);
            ac.Execute();
            AssignTaskService.UpdateCustomerSearchInfo(asm.CustomerID);
            return new { Msg = ac.Msg };
        }

        #endregion

        #region api/teachercourse/deleteAssign

        /// 教师上课记录，删除课表
        [HttpPost]
        [ApiPassportAuthentication]
        [PPTSJobFunctionAuthorize("PPTS:按钮-录入/删除/确认课时-本校区")]
        public void DeleteAssign(AssignCollection model)
        {
            AssignDeleteExecutor executor = new AssignDeleteExecutor(model);
            executor.Execute();
            AssignTaskService.UpdateCustomerSearchInfo(executor.CustomerIDTask);
        }
        #endregion

        #region api/teachercourse/cancelAssign

        /// 教师上课记录，取消排课
        [HttpPost]
        [ApiPassportAuthentication]
        [PPTSJobFunctionAuthorize("PPTS:按钮-新增/取消/复制/调动课表-本校区")]
        public void CancelAssign(AssignCollection model)
        {
            AssignCancelExecutor executor = new AssignCancelExecutor(model);
            executor.Execute();
            AssignTaskService.UpdateCustomerSearchInfo(executor.CustomerIDTask);
        }
        #endregion

        #region api/teachercourse/getCurrUserCampus
        ///获取当前用户所属校区
        [HttpPost]
        [ApiPassportAuthentication]
        public dynamic GetCurrUserCampus()
        {
            IOrganization organ = DeluxeIdentity.CurrentUser.GetCurrentJob().GetParentOrganizationByType(DepartmentType.Campus);
            if (organ == null)
                return null;
            return new { CampusID = organ.ID, CampusName = organ.Name };
        }
        #endregion

        #region api/teachercourse/getTeacher
        /*录入学科课时、陪读课时，获取智能提示搜索框下拉数据*/
        [HttpPost]
        [ApiPassportAuthentication]
        public SimpleTeacherJobViewCollection GetTeacher(dynamic para)
        {
            IOrganization organ = DeluxeIdentity.CurrentUser.GetCurrentJob().GetParentOrganizationByType(DepartmentType.Campus);
            TeacherQCM qcm = new TeacherQCM() { TeacherName = para.searchTerm };
            
            if (organ == null)
                return new SimpleTeacherJobViewCollection()
                {
                    Result = new TeacherJobViewCollection()
                };
            qcm.CampusID = organ.ID;
            var r = TeacherJobViewAdapter.Instance.LoadCollection(qcm.CampusID, qcm.TeacherName);
            return new SimpleTeacherJobViewCollection() { Result = r };
        }
        #endregion 

    }
}