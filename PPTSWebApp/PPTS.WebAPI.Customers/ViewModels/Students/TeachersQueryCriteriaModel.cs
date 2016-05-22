using MCS.Library.Data;
using MCS.Library.Data.Mapping;
using MCS.Library.OGUPermission;
using MCS.Library.Principal;
using PPTS.Data.Common.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPTS.WebAPI.Customers.ViewModels.Students
{
    public class TeachersQueryCriteriaModel
    {
        private string _campusID = null;

        /// <summary>
        /// 校区ID
        /// </summary>
        public string CampusID
        {
            get
            {
                if (String.IsNullOrEmpty(_campusID))
                {
                    _campusID = "18-org";//CreateJob.GetParentOrganizationByType(DepartmentType.Campus).ID;
                }
                return _campusID;
            }
        }

        

        [ConditionMapping("JobOrgType")]
        public DepartmentType JobOrgType {
            get {
                return DepartmentType.XueKeZu;
            }
        }


        [NoMapping]
        public PageRequestParams PageParams
        {
            get {
                return new PageRequestParams()
                {
                    PageIndex = 0,
                    PageSize = 0,
                    TotalCount = -1
                };
            }
            
        }

        [NoMapping]
        public OrderByRequestItem[] OrderBy
        {
            get {
                return null;
                    }
        }
    }
}