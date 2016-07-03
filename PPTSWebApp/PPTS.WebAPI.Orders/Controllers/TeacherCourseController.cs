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
using PPTS.Web.MVC.Library.Filters;
using PPTS.WebAPI.Orders.Executors;
using PPTS.WebAPI.Orders.ViewModels.Assignment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace PPTS.WebAPI.Orders.Controllers
{
    
    public partial class TeacherCourseController : ApiController
    {
        #region api/teachercourse/initTchWeekCourse
        ///教师课表 获取搜索条件
        [HttpPost]
        [ApiPassportAuthentication]
        [PPTSJobFunctionAuthorize("PPTS:教师课表,教师课表-本部门,教师课表-本校区,教师课表-本分公司,教师课表-全国")]
        public dynamic InitTchWeekCourse()
        {
            //校区
            IOrganization campus = DeluxeIdentity.CurrentUser.GetCurrentJob().GetParentOrganizationByType(DepartmentType.Campus);
            if (campus == null)
                return null;
            ///分公司
            IOrganization company = DeluxeIdentity.CurrentUser.GetCurrentJob().GetParentOrganizationByType(DepartmentType.Branch);
            ///学科组
            IList<IOrganization> xk = campus.GetChildOrganizationsByPPTSType(DepartmentType.XueKeZu);
            Dictionary<string, IEnumerable<BaseConstantEntity>> dic = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(Data.Orders.Entities.Assign));
            return new
            {
                Campus = campus,
                Company = company,
                XueKeZu = xk,
                Dictionaries = dic
            };
        }
        #endregion

        #region api/teachercourse/getWeekCourse
        ///教师课表 获取教师课表数据
        [HttpPost]
        [ApiPassportAuthentication]
        [PPTSJobFunctionAuthorize("PPTS:教师课表,教师课表-本部门,教师课表-本校区,教师课表-本分公司,教师课表-全国")]
        public dynamic GetWeekCourse(Assign assingQM)
        {
            if (assingQM.StartTime == DateTime.MinValue || assingQM.EndTime == DateTime.MinValue)
            {
                DateTime dt = DateTime.Now;
                DateTime startWeek = dt.AddDays(1-Convert.ToInt32(dt.DayOfWeek.ToString("d")));
                DateTime endWeek = startWeek.AddDays(6);

                assingQM.StartTime = startWeek;
                assingQM.EndTime = endWeek;
            }

            assingQM.StartTime = assingQM.StartTime.Date;
            assingQM.EndTime = assingQM.EndTime.Date;

            //校区
            IOrganization organ = DeluxeIdentity.CurrentUser.GetCurrentJob().GetParentOrganizationByType(DepartmentType.Campus);
            if (organ == null)
                return null;

            assingQM.CampusID = organ.ID;
            AssignCollection ac = AssignsAdapter.Instance.LoadCollection(assingQM);

            TeacherCourseModel tcm = new TeacherCourseModel();
            tcm.StartTime = assingQM.StartTime;
            tcm.EndTime = assingQM.EndTime;
            IList<TWC> twc = tcm.GetTchWeekCourse(ac);
            return  new { result = twc } ;
        }
        #endregion



        #region api/teachercourse/getTeacherWeekCourse
        ///教师个人课表，初始化教师周视图页面数据
        [HttpPost]
        [ApiPassportAuthentication]
        [PPTSJobFunctionAuthorize("PPTS:教师查看-教师课表,教师查看-教师课表-本部门,教师查看-教师课表-本校区,教师查看-教师课表-本分公司,教师查看-教师课表-全国")]
        public AssignWeekViewModel GetPsnTeacherWeekCourse(TeacherAssignQM qm)
        {
            AssignWeekViewModel result = new AssignWeekViewModel();
            qm.TeacherJobID = DeluxeIdentity.CurrentUser.GetCurrentJob().ID;
            result.LoadDataByTeacher(qm);
            return result;
        }
        ///教师个人课表，为周视图页面获取指定教师当前月排课统计数据
        [HttpPost]
        [ApiPassportAuthentication]
        [PPTSJobFunctionAuthorize("PPTS:教师查看-教师课表,教师查看-教师课表-本部门,教师查看-教师课表-本校区,教师查看-教师课表-本分公司,教师查看-教师课表-全国")]
        public dynamic GetPsnCurMonthStat(TeacherAssignQM qm)
        {
            DateTime sdate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime edate = new DateTime(DateTime.Now.Year, DateTime.Now.Month + 1, 1);
            qm.TeacherJobID = DeluxeIdentity.CurrentUser.GetCurrentJob().ID;
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

        #region api/teachercourse/getSCLV

        /// 按教师排课，列表视图， 获取排课列表视图数据
        [HttpPost]
        [ApiPassportAuthentication]
        [PPTSJobFunctionAuthorize("PPTS:教师查看-教师课表,教师查看-教师课表-本部门,教师查看-教师课表-本校区,教师查看-教师课表-本分公司,教师查看-教师课表-全国")]
        public AssignQCR GetSCLV(AssignQCM qcm)
        {
            qcm.CustomerID = string.Empty;
            qcm.TeacherName = string.Empty;
            var och = new OrderCommonHelper();
            och.GetCourseConditionAssign(qcm);

            Dictionary<string, IEnumerable<BaseConstantEntity>> dic = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(Data.Orders.Entities.Assign));
            ///处理查询条件
            och.GetProductCategoryType(dic);
            qcm.TeacherJobID = DeluxeIdentity.CurrentUser.GetCurrentJob().ID;
            AssignQCR result = new AssignQCR
            {
                QueryResult = GenericOrderDataSource<Data.Orders.Entities.Assign, AssignCollection>.Instance.Query(qcm.PageParams, qcm, qcm.OrderBy),
                Dictionaries = dic
            };
            return result;
        }

        /// 按教师排课，列表视图， 获取排课列表视图数据(分页事件)
        [HttpPost]
        [ApiPassportAuthentication]
        [PPTSJobFunctionAuthorize("PPTS:教师查看-教师课表,教师查看-教师课表-本部门,教师查看-教师课表-本校区,教师查看-教师课表-本分公司,教师查看-教师课表-全国")]
        public PagedQueryResult<Assign, AssignCollection> GetPagedSCLV(AssignQCM qcm)
        {
            qcm.CustomerID = string.Empty;
            qcm.TeacherName = string.Empty;
            var och = new OrderCommonHelper();
            och.GetCourseConditionAssign(qcm);
            qcm.TeacherJobID = DeluxeIdentity.CurrentUser.GetCurrentJob().ID;
            return GenericOrderDataSource<Data.Orders.Entities.Assign, AssignCollection>.Instance.Query(qcm.PageParams, qcm, qcm.OrderBy);
        }

        #endregion
    }
}