using MCS.Library.OGUPermission;
using MCS.Library.Principal;
using PPTS.Contracts.Customers.Models;
using PPTS.Contracts.Proxies;
using PPTS.Data.Common.Security;
using PPTS.Data.Customers.Entities;
using PPTS.Data.Products.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPTS.WebAPI.Orders.ViewModels.ClassGroup
{
    /// <summary>
    /// 班组-班级  新增  模型
    /// </summary>
    public class CreatableClassModel
    {
        #region 机构：全部提供(当前操作人的校区)
        private string _campusID = null;

        /// <summary>
        /// 校区ID
        /// </summary>
        public string CampusID
        {
            get {
                if (String.IsNullOrEmpty(_campusID))
                {
                    _campusID = CreateJob.GetParentOrganizationByType(DepartmentType.Campus).ID;                    
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
                    _campusName = CreateJob.GetParentOrganizationByType(DepartmentType.Campus).Name;                    
                }                
                return _campusName;
            }
        }
        #endregion

        #region 产品：仅提供ID其他信息通过查询获取
        /// <summary>
        /// 产品ID
        /// </summary>
        public string ProductID
        {
            get;
            set;
        }

        private ProductView _product = null;

        public ProductView Product { get {
                if (_product == null && !String.IsNullOrEmpty(ProductID))
                {
                    string[] productids = new string[] { ProductID };
                    ProductViewCollection _pvc = PPTSProductQueryServiceProxy.Instance.QueryProductViewsByIDs(productids);
                    _product = _pvc == null ? null : _pvc[0];
                   
                }                
                return _product;
            } }
        #endregion

        #region 上课时间：需要计算的基本信息（包含产品信息）
        /// <summary>
        /// 周几上课
        /// </summary>
        public List<int> DayOfWeeks {  get;set; }

        /// <summary>
        /// 上课开始时间
        /// </summary>
        public DateTime StartTime
        {
            get;
            set;
        }

        private List<DateTime> _startTimeList = null;

        /// <summary>
        /// 开始时间集合
        /// </summary>
        public List<DateTime> StartTimeList { get {
                if (_startTimeList == null && Product.LessonCount > 0)
                {
                    _startTimeList = new List<DateTime>();
                    DateTime st = StartTime;
                    bool flag = true;
                    int c = 0;
                    while (flag)
                    {
                        if (Product.LessonCount <= 0 || c >= Product.LessonCount)
                            break;
                        if (DayOfWeeks.Contains((int)st.DayOfWeek))
                        {
                            _startTimeList.Add(st);
                            st = st.AddDays(1);
                            c = c + 1;
                        }
                        else
                            st = st.AddDays(1);
                    }
                }
                
               return _startTimeList;
            } }
        #endregion

        #region 学生 通过查询获取

        private CustomerCollectionQueryResult _customerCollection = null;

        /// <summary>
        /// 学生信息列表
        /// </summary>
        public CustomerCollectionQueryResult CustomerCollection { get {
                if (_customerCollection == null && Assets != null && Assets.Length > 0)
                {                    
                    string[] customers = new string[Assets.Length];
                    for (int i = 0; i < Assets.Length; i++)
                    {
                        customers[i] = Assets[i].CustomerID;
                    }
                    _customerCollection = PPTSCustomerQueryServiceProxy.Instance.QueryCustomerCollectionByCustomerIDs(customers);
                }
                return _customerCollection;
            } }

        #endregion

        #region 教师：全部提供
        /// <summary>
        /// 教师ID
        /// </summary>
        public string TeacherID
        {
            get;
            set;
        }

        /// <summary>
        /// 教师编号
        /// </summary>
        public string TeacherCode
        {
            get;
            set;
        }

        /// <summary>
        /// 教师名
        /// </summary>
        public string TeacherName
        {
            get;
            set;
        }
        #endregion

        #region  科目：全部提供
        /// <summary>
        /// 科目代码
        /// </summary>
        public string Subject
        {
            get;
            set;
        }


        /// <summary>
        /// 科目名
        /// </summary>
        public string SubjectName
        {
            get;
            set;
        }
        #endregion

        #region 年级：全部提供
        /// <summary>
        /// 年级代码
        /// </summary>
        public string Grade
        {
            get;
            set;
        }

        /// <summary>
        /// 年级名
        /// </summary>
        public string GradeName
        {
            get;
            set;
        }
        #endregion

        #region 创建人：通过当前用户信息获取
        PPTSJob CreateJob = DeluxeIdentity.CurrentUser.GetCurrentJob();

        IUser CreateUser = DeluxeIdentity.CurrentUser;
        #endregion

        #region 资产学生 全部提供

        public Asset[] Assets { get; set; }
        #endregion

        #region 行为处理
        /// <summary>
        /// 新增班级检查
        /// </summary>
        /// <returns></returns>
        public bool CheckCreatClass() {
            return true;
        }
        #endregion
    }

    /// <summary>
    /// 班组-学生 模型
    /// </summary>
    public class CustomerModel {
        /// <summary>
        /// 学员ID
        /// </summary>
        public string CustomerID
        {
            get;
            set;
        }

        /// <summary>
        /// 学员编号
        /// </summary>
        public string CustomerCode
        {
            get;
            set;
        }

        /// <summary>
        /// 学员姓名
        /// </summary>
        public string CustomerName
        {
            get;
            set;
        }

        /// <summary>
        /// 学员校区ID
        /// </summary>
        public string CustomerCampusID
        {
            get;
            set;
        }

        /// <summary>
        /// 学员校区名
        /// </summary>
        public string CustomerCampusName
        {
            get;
            set;
        }

        /// <summary>
        /// 咨询师ID
        /// </summary>
        public string ConsultantID
        {
            get;
            set;
        }

        /// <summary>
        /// 咨询师名
        /// </summary>
        public string ConsultantName
        {
            get;
            set;
        }

        /// <summary>
        /// 咨询师岗位ID
        /// </summary>
        public string ConsultantJobID
        {
            get;
            set;
        }

        /// <summary>
        /// 咨询师岗位名
        /// </summary>
        public string ConsultantJobName
        {
            get;
            set;
        }

        /// <summary>
        /// 学管师ID
        /// </summary>
        public string EducatorID
        {
            get;
            set;
        }

        /// <summary>
        /// 学管师名
        /// </summary>
        public string EducatorName
        {
            get;
            set;
        }

        /// <summary>
        /// 学管师岗位ID
        /// </summary>
        public string EducatorJobID
        {
            get;
            set;
        }

        /// <summary>
        /// 学管师岗位名
        /// </summary>
        public string EducatorJobName
        {
            get;
            set;
        }
    }

    public class Asset {
        /// <summary>
        /// 学生ID
        /// </summary>
        public string CustomerID { get; set; }

        /// <summary>
        /// 资产ID
        /// </summary>
        public string AssetID
        {
            get;
            set;
        }

        /// <summary>
        /// 资产编号
        /// </summary>
        public string AssetCode
        {
            get;
            set;
        }

        /// <summary>
        /// 单价
        /// </summary>
        public decimal Price { get; set; }
    }
}