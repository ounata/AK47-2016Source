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
    public class StudentCourseController : ApiController
    {
        #region api/studentcourse/getStuCourse
        /// <summary>
        /// 获取学员课表列表数据
        /// </summary>
        /// <param name="criteriaQCM"></param>
        /// <returns></returns>
        [HttpPost]
        public AssignQCR GetStuCourse(AssignQCM criteriaQCM)
        {
            ///当前操作人所属校区ID
            // IOrganization org = DeluxeIdentity.CurrentUser.GetCurrentJob().GetParentOrganizationByType(DepartmentType.Campus);
            return new AssignQCR()
            {
                QueryResult = GenericOrderDataSource<Data.Orders.Entities.Assign, AssignCollection>.Instance.Query(criteriaQCM.PageParams, criteriaQCM, criteriaQCM.OrderBy),
                Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(Data.Orders.Entities.Assign))
            };
        }

        [HttpPost]
        public PagedQueryResult<Data.Orders.Entities.Assign, AssignCollection> GetStuCoursePaged(AssignQCM criteriaQCM)
        {
            return GenericOrderDataSource<Data.Orders.Entities.Assign, AssignCollection>.Instance.Query(criteriaQCM.PageParams, criteriaQCM, criteriaQCM.OrderBy);
        }
        #endregion

        #region api/studentcourse/deleteAssign
        /// <summary>
        /// 学员课表，删除课表
        /// </summary>
        /// <param name="model"></param>
        /// 
        [HttpPost]
        public void DeleteAssign(AssignCollection model)
        {
            AssignDeleteExecutor executor = new AssignDeleteExecutor(model);
            executor.Execute();
        }
        #endregion

        #region api/studentcourse/confirmAssign
        /// <summary>
        /// 学员课表，确认课表
        /// </summary>
        /// <param name="model"></param>
        /// 
        [HttpPost]
        public void ConfirmAssign(AssignCollection model)
        {
            AssignConfirmExecutor executor = new AssignConfirmExecutor(model);
            executor.Execute();
        }
        #endregion



        #region api/studentcourse/markupAssign
        ///保存排课条件
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