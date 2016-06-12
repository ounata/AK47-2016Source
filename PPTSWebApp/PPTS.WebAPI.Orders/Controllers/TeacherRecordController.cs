using MCS.Library.Data;
using MCS.Library.OGUPermission;
using MCS.Library.Principal;
using PPTS.Data.Common.Adapters;
using PPTS.Data.Common.Entities;
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
    public partial class TeacherCourseController
    {

        #region api/teachercourse/getTchClassRecord
        ///教师  上课记录查询列表
        [HttpPost]
        public AssignQCR GetTchClassRecord(AssignQCM criteriaQCM)
        {
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
            if (criteriaQCM.EndTime != DateTime.MinValue)
                criteriaQCM.EndTime = criteriaQCM.EndTime.AddDays(1);


            criteriaQCM.CampusID = new string[] { org.ID };
            if (criteriaQCM.AssignStatus == null || criteriaQCM.AssignStatus.Length == 0)
                criteriaQCM.AssignStatus = new int[] { (int)AssignStatusDefine.Exception, (int)AssignStatusDefine.Finished };
            else if (criteriaQCM.AssignStatus[0] == (int)AssignStatusDefine.Assigned
                || criteriaQCM.AssignStatus[0] == (int)AssignStatusDefine.Invalid
                )
            {
                criteriaQCM.AssignStatus = new int[] { (int)AssignStatusDefine.Exception, (int)AssignStatusDefine.Finished };
            }

            Dictionary<string, IEnumerable<BaseConstantEntity>> dic = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(Data.Orders.Entities.Assign));

            string key = "C_CODE_ABBR_Course_AssignStatus";
            foreach (var v in dic)
            {
                if (v.Key.ToLower() == key.ToLower())
                {
                    key = v.Key;
                    System.Collections.Generic.List<BaseConstantEntity> ie = (System.Collections.Generic.List<BaseConstantEntity>)v.Value;
                    var cc = from c in ie
                             where (new[] { "异常", "已上"}).Contains(c.Value)
                             select c;
                    dic.Remove(key);
                    dic.Add(key, cc);
                    break;
                }
            }
            return new AssignQCR()
            {
                QueryResult = GenericOrderDataSource<Data.Orders.Entities.Assign, AssignCollection>.Instance.Query(criteriaQCM.PageParams, criteriaQCM, criteriaQCM.OrderBy),
                Dictionaries = dic
            };
        }
        ///教师  上课记录查询列表翻页事件
        [HttpPost]
        public PagedQueryResult<Data.Orders.Entities.Assign, AssignCollection> GetTchClassRecordPaged(AssignQCM criteriaQCM)
        {
            //当前操作人所属校区ID
            IOrganization org = DeluxeIdentity.CurrentUser.GetCurrentJob().GetParentOrganizationByType(DepartmentType.Campus);
            if (org == null)
            {
                return new PagedQueryResult<Data.Orders.Entities.Assign, AssignCollection>();
            }
            if (criteriaQCM.EndTime != DateTime.MinValue)
                criteriaQCM.EndTime = criteriaQCM.EndTime.AddDays(1);
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