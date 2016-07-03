using MCS.Library.Data;
using MCS.Library.OGUPermission;
using MCS.Library.Principal;
using PPTS.Data.Common.Adapters;
using PPTS.Data.Common.Entities;
using PPTS.Data.Common.Security;
using PPTS.Data.Orders.DataSources;
using PPTS.Data.Orders.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPTS.WebAPI.Orders.ViewModels.Assignment
{
    public class ClassRecordTeacherModel
    {
        public PagedQueryResult<AssignView, AssignViewCollection> QueryResult { get; set; }
        public IDictionary<string, IEnumerable<BaseConstantEntity>> Dictionaries { get; set; }

        public string Msg { get; private set; }

        public void LoadData(AssignQCM qcm)
        {
            this.Msg = "ok";

            //当前操作人所属校区ID
            if (DeluxeIdentity.CurrentUser.GetCurrentJob().JobType == Data.Common.JobTypeDefine.Teacher)
            {
                qcm.TeacherJobID = DeluxeIdentity.CurrentUser.GetCurrentJob().ID;
                IOrganization org = DeluxeIdentity.CurrentUser.GetCurrentJob().GetParentOrganizationByType(DepartmentType.Campus);
                if (org == null)
                {
                    this.Msg = "未能获取当前用户所属校区，请确认角色是否正确！";
                    return;
                }
                qcm.CampusID = new string[] { org.ID };
            }
            Dictionary<string, IEnumerable<BaseConstantEntity>> dic = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(Data.Orders.Entities.Assign));
            var och = new OrderCommonHelper();
            och.GetCourseAssignStatus(dic);
            och.GetProductCategoryType(dic);
            och.GetCourseCondition(qcm);

            this.Dictionaries = dic;
            this.QueryResult = GenericOrderDataSource<Data.Orders.Entities.AssignView, AssignViewCollection>.Instance.Query(qcm.PageParams, qcm, qcm.OrderBy);
        }


        public void GetCC(AssignQCM criteriaQCM)
        {
            if (criteriaQCM.EndTime != DateTime.MinValue)
            {
                criteriaQCM.EndTime = criteriaQCM.EndTime.AddDays(1);
            }
        }
    }
}