using MCS.Library.Data;
using MCS.Library.Data.Mapping;
using MCS.Library.OGUPermission;
using MCS.Library.Principal;
using PPTS.Data.Common.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPTS.WebAPI.Orders.ViewModels.ClassGroup
{
    public class TeacherJobsCriteriaModel
    {
        private string _campusID = null;

        /// <summary>
        /// 校区ID
        /// </summary>
        [ConditionMapping("CampusID")]
        public string CampusID
        {
            get
            {
                if (String.IsNullOrEmpty(_campusID))
                {
                    _campusID = CreateJob.GetParentOrganizationByType(DepartmentType.Campus).ID;
                }
                return _campusID;
            }
        }

        [ConditionMapping("TeacherName")]
        public string TeacherName { get; set; }

        [ConditionMapping("GradeMemo")]
        public string GradeMemo { get; set; }

        [ConditionMapping("SubjectMemo")]
        public string SubjectMemo { get; set; }

        #region 创建人：通过当前用户信息获取
        private PPTSJob _createJob = null;
        PPTSJob CreateJob
        {
            get
            {
                if (_createJob == null)
                {
                    _createJob = DeluxeIdentity.CurrentUser.GetCurrentJob();
                }
                return _createJob;
            }
        }

        private IUser _createUser;
        IUser CreateUser
        {
            get
            {
                if (_createUser == null)
                {
                    _createUser = DeluxeIdentity.CurrentUser;
                }
                return _createUser;
            }
        }
        #endregion


        [NoMapping]
        public PageRequestParams PageParams
        {
            get;
            set;
        }

        [NoMapping]
        public OrderByRequestItem[] OrderBy
        {
            get;
            set;
        }
    }
}