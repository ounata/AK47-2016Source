using MCS.Library.Core;
using MCS.Library.Data;
using MCS.Library.OGUPermission;
using MCS.Library.Principal;
using PPTS.Data.Common.Adapters;
using PPTS.Data.Common.Security;
using PPTS.Data.Orders;
using PPTS.Data.Orders.DataSources;
using PPTS.Data.Orders.Entities;
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
            if (criteriaQCM.AssignStatus == null || criteriaQCM.AssignStatus.Length == 0)
                criteriaQCM.AssignStatus = new int[] { (int)AssignStatusDefine.Exception, (int)AssignStatusDefine.Finished };
            else if (criteriaQCM.AssignStatus[0] == (int)AssignStatusDefine.Assigned
                || criteriaQCM.AssignStatus[0] == (int)AssignStatusDefine.Invalid
                )
            {
                criteriaQCM.AssignStatus = new int[] { (int)AssignStatusDefine.Exception, (int)AssignStatusDefine.Finished };
            }

            return new AssignQCR()
            {
                QueryResult = GenericOrderDataSource<Data.Orders.Entities.Assign, AssignCollection>.Instance.Query(criteriaQCM.PageParams, criteriaQCM, criteriaQCM.OrderBy),
                Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(Data.Orders.Entities.Assign))
            };
        }
        ///学员视图  上课记录查询列表翻页事件
        [HttpPost]
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
            if (criteriaQCM.AssignStatus == null || criteriaQCM.AssignStatus.Length == 0)
                criteriaQCM.AssignStatus = new int[] { (int)AssignStatusDefine.Exception, (int)AssignStatusDefine.Finished };
            else if (criteriaQCM.AssignStatus[0] == (int)AssignStatusDefine.Assigned
                || criteriaQCM.AssignStatus[0] == (int)AssignStatusDefine.Invalid
                )
            {
                criteriaQCM.AssignStatus = new int[] { (int)AssignStatusDefine.Exception, (int)AssignStatusDefine.Finished };
            }
            return GenericOrderDataSource<Data.Orders.Entities.Assign, AssignCollection>.Instance.Query(criteriaQCM.PageParams, criteriaQCM, criteriaQCM.OrderBy);
        }

        #endregion

    }
}