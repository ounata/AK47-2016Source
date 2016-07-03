using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PPTS.Data.Customers.Entities;
using PPTS.Data.Common.Entities;
using MCS.Library.Validation;
using MCS.Library.Principal;
using PPTS.Data.Common.Security;
using MCS.Library.OGUPermission;
using MCS.Web.MVC.Library.Filters;
using PPTS.WebAPI.Customers.ViewModels.Students;
using PPTS.Data.Customers.Adapters;

namespace PPTS.WebAPI.Customers.ViewModels.CustomerVisits
{
    [ApiPassportAuthentication]
    public class CreatableVisitBatchModel
    {

        public CustomerVisitModel CustomerVisit
        {
            get; set;
        }

        public CustomerVisitModelCollection CustomerVisits
        {
            get; set;
        }

        public IDictionary<string, IEnumerable<BaseConstantEntity>> Dictionaries
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


        public string _CurrID = null;

        /// <summary>
        /// 当前登录人ID
        /// </summary>
        public string CurrID
        {
            get
            {
                if (String.IsNullOrEmpty(_CurrID))
                {
                    _CurrID = CreateUser.ID;
                }
                return _CurrID;
            }
        }

        public string _CurrName = null;

        /// <summary>
        /// 当前登录人姓名
        /// </summary>
        public string CurrName
        {
            get
            {
                if (String.IsNullOrEmpty(_CurrName))
                {
                    _CurrName = CreateUser.Name;
                }
                return _CurrName;
            }
        }

        public string _CurrJobID = null;

        /// <summary>
        /// 当前岗位ID
        /// </summary>
        public string CurrJobID
        {
            get
            {
                if (String.IsNullOrEmpty(_CurrJobID))
                {
                    _CurrJobID = CreateJob.ID;
                }
                return _CurrJobID;
            }
        }

        public string _CurrJobName = null;

        /// <summary>
        /// 当前岗位ID
        /// </summary>
        public string CurrJobName
        {
            get
            {
                if (String.IsNullOrEmpty(_CurrJobName))
                {
                    _CurrJobName = CreateJob.Name;
                }
                return _CurrJobName;
            }
        }

        #endregion

        public CreatableVisitBatchModel()
        {
        }

        public static StudentModel GetStudentByID(string studentID)
        {
            StudentModel student = new StudentModel();

            GenericCustomerAdapter<StudentModel, List<StudentModel>>.Instance.LoadInContext(studentID, customer => student = customer);

            CustomerServiceAdapter.Instance.GetDbContext().DoAction(context => context.ExecuteDataSetSqlInContext());

            return student;
        }
    }
}