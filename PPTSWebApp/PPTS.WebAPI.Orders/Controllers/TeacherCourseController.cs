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
using PPTS.WebAPI.Orders.Executors;
using PPTS.WebAPI.Orders.ViewModels.Assignment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace PPTS.WebAPI.Orders.Controllers
{
    [ApiPassportAuthentication]
    public partial class TeacherCourseController : ApiController
    {
        ///获取当前用户所属校区
        [HttpPost]
        public dynamic GetCurrUserCampus()
        {
            IOrganization organ = DeluxeIdentity.CurrentUser.GetCurrentJob().GetParentOrganizationByType(DepartmentType.Campus);
            if (organ == null)
            {
                return null;
            }
            return new { CampusID = organ.ID, CampusName = organ.Name  };
        }

        /*录入学科课时、陪读课时，获取智能提示搜索框下拉数据*/
        [HttpPost]
        public SimpleTeacherJobViewCollection GetTeacher(TeacherQCM criteria)
        {
            IOrganization organ = DeluxeIdentity.CurrentUser.GetCurrentJob().GetParentOrganizationByType(DepartmentType.Campus);
            if (organ == null)
            {
                return new SimpleTeacherJobViewCollection()
                {
                    Result = new TeacherJobViewCollection()
                };
            }
            criteria.CampusID = organ.ID;
          
            var r = TeacherJobViewAdapter.Instance.LoadCollection(criteria.CampusID, criteria.TeacherName);
            return new SimpleTeacherJobViewCollection() { Result = r };
        }

        #region api/teachercourse/addAccompanion

        /*初始化录入陪读课时记录界面数据*/
        [HttpPost]
        public dynamic InitAccompanion()
        {
            IOrganization organ = DeluxeIdentity.CurrentUser.GetCurrentJob().GetParentOrganizationByType(DepartmentType.Campus);
            if (organ == null)
            {
                return null;
            }
            var dic = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(Data.Orders.Entities.Assign));
            return new
            {
                CampusID = organ.ID,
                CampusName = organ.Name,
                Dictionaries = dic,
                Accompanion = new AccompanyAssign() { CampusID = organ.ID, CampusName = organ.Name }
            };
        }

        //添加陪读记录
        [HttpPost]
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

        #region api/teachercourse/deleteAssign

        /// 学员课表，删除课表
        [HttpPost]
        public void DeleteAssign(AssignCollection model)
        {
            AssignDeleteExecutor executor = new AssignDeleteExecutor(model);
            executor.Execute();
        }
        #endregion

        #region api/teachercourse/cancelAssign

        /// 教师上课记录，取消排课
        [HttpPost]
        public void CancelAssign(AssignCollection model)
        {
            AssignCancelExecutor executor = new AssignCancelExecutor(model);
            executor.Execute();
        }
        #endregion


        #region api/teachercourse/markupAssign

        ///保存补录的课时
        [HttpPost]
        public dynamic MarkupAssign(AssignSuperModel asm)
        {
            AssignMarkupExecutor ac = new AssignMarkupExecutor(asm);
            var result = ac.Execute();
            if (string.IsNullOrEmpty(ac.Msg))
                ac.Msg = "ok";
            return new { Msg = ac.Msg };
        }

        #endregion 

    }
}