using MCS.Library.Core;
using MCS.Library.Data;
using MCS.Library.OGUPermission;
using MCS.Library.Principal;
using PPTS.Data.Common.Adapters;
using PPTS.Data.Common.Entities;
using PPTS.Data.Common.Security;
using PPTS.Data.Orders;
using PPTS.Data.Orders.DataSources;
using PPTS.Data.Orders.Entities;
using PPTS.Data.Products;
using PPTS.Web.MVC.Library.Filters;
using PPTS.WebAPI.Orders.ViewModels.Assignment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace PPTS.WebAPI.Orders.Controllers
{
    public partial class StudentCourseController
    {
        #region api/studentcourse/getStuClassRecord
        ///学员视图  上课记录查询列表
        [HttpPost]
        [PPTSJobFunctionAuthorize("PPTS:学员视图-排课条件/课表/上课记录/班组班级")]
        public AssignQCR GetStuClassRecord(AssignQCM criteriaQCM)
        {
            criteriaQCM.CustomerID.NullCheck("学员ID不能为空");
            //当前操作人所属校区ID
            IOrganization org = DeluxeIdentity.CurrentUser.GetCurrentJob().GetParentOrganizationByType(DepartmentType.Campus);
            if (org == null)
            {
                return new AssignQCR
                {
                    QueryResult = new PagedQueryResult<Assign, AssignCollection>(),
                    Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(Data.Orders.Entities.Assign))
                };
            }
            criteriaQCM.CampusID = new string[] { org.ID };
            var och = new OrderCommonHelper();
            och.GetCourseCondition(criteriaQCM);
            Dictionary<string, IEnumerable<BaseConstantEntity>> dic = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(Data.Orders.Entities.Assign));
           
            och.GetCourseAssignStatus(dic);
            och.GetProductCategoryType(dic);

            return new AssignQCR()
            {
                QueryResult = GenericOrderDataSource<Data.Orders.Entities.Assign, AssignCollection>.Instance.Query(criteriaQCM.PageParams, criteriaQCM, criteriaQCM.OrderBy),
                Dictionaries = dic
            };
        }

       
        ///学员视图  上课记录查询列表翻页事件
        [HttpPost]
        [PPTSJobFunctionAuthorize("PPTS:学员视图-排课条件/课表/上课记录/班组班级")]
        public PagedQueryResult<Data.Orders.Entities.Assign, AssignCollection> GetStuClassRecordPaged(AssignQCM criteriaQCM)
        {
            criteriaQCM.CustomerID.NullCheck("学员ID不能为空");
            //当前操作人所属校区ID
            IOrganization org = DeluxeIdentity.CurrentUser.GetCurrentJob().GetParentOrganizationByType(DepartmentType.Campus);
            if (org == null)
            {
                return new PagedQueryResult<Data.Orders.Entities.Assign, AssignCollection>();
            }
            criteriaQCM.CampusID = new string[] { org.ID };

            var och = new OrderCommonHelper();
            och.GetCourseCondition(criteriaQCM);

            return GenericOrderDataSource<Data.Orders.Entities.Assign, AssignCollection>.Instance.Query(criteriaQCM.PageParams, criteriaQCM, criteriaQCM.OrderBy);
        }

        #endregion

    }
}