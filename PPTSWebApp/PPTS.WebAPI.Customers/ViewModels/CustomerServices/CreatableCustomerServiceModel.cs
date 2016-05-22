using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PPTS.Data.Customers.Entities;
using PPTS.Data.Common.Entities;
using MCS.Library.Validation;
using PPTS.Data.Common.Security;
using MCS.Library.OGUPermission;
using MCS.Library.Principal;

namespace PPTS.WebAPI.Customers.ViewModels.CustomerServices
{
    public class CreatableCustomerServiceModel
    {
        [ObjectValidator]
        public CustomerServiceModel Customer
        {
            get;
            set;
        }

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

        #region 机构：全部提供(当前操作人的校区)
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
                    _campusID = CreateJob.GetParentOrganizationByType(DepartmentType.Campus) == null ? "" : CreateJob.GetParentOrganizationByType(DepartmentType.Campus).ID;
                }
                return _campusID;
            }
        }

        private string _campusName = null;

        /// <summary>
        /// 校区名
        /// </summary>
        public string CampusName
        {
            get
            {
                if (String.IsNullOrEmpty(_campusName))
                {
                    _campusName = CreateJob.GetParentOrganizationByType(DepartmentType.Campus) == null ? "" : CreateJob.GetParentOrganizationByType(DepartmentType.Campus).Name;
                }
                return _campusName;
            }
        }

        private string _shortCampusName = null;

        public string ShortCampusName
        {
            get
            {
                if (String.IsNullOrEmpty(_shortCampusName))
                {
                    _shortCampusName = CreateJob.GetDataScopeAbbr();
                }
                return _shortCampusName;
            }
        }
        #endregion

        public IDictionary<string, IEnumerable<BaseConstantEntity>> Dictionaries
        {
            get;
            set;
        }
        public object CustomerCollection { get; internal set; }

        public CreatableCustomerServiceModel()
        {
        }
    }
}