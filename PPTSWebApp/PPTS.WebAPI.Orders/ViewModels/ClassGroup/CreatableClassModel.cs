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

        private string _shortCampusName = null;

        public string ShortCampusName {
            get {
                if (String.IsNullOrEmpty(_shortCampusName))
                {
                    _shortCampusName = CreateJob.GetDataScopeAbbr();
                }
                return _shortCampusName;
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

        public CheckResultModel CheckProduct() {
            CheckResultModel result = new CheckResultModel() ;
            if (Product == null)
            {
                result.SetErrorMsg("产品不存在！");
                return result;
            }

            if (Product.CategoryType != Data.Products.CategoryType.CalssGroup) {
                result.SetErrorMsg("只有班组产品才可以创建班级！");
                return result;
            }

            if (Product.ProductStatus != Data.Products.ProductStatus.Enabled && Product.ProductStatus != Data.Products.ProductStatus.Disabled) {
                result.SetErrorMsg("只有产品状态是使用中、已停售的才可以创建班级！");
                return result;
            }

            return result;
        }
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
                    for (int i = 0; i < DayOfWeeks.Count; i++)
                    {
                        if (DayOfWeeks[i] == 7)
                            DayOfWeeks[i] = 0;
                    }
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


        public CheckResultModel CheckCustomer() {
            CheckResultModel result = new CheckResultModel();
            //1.判断学员信息是否重复录入
            foreach (var item in CustomerCollection.CustomerCollection)
            {
                if (CustomerCollection.CustomerCollection.Where(c => c.Customer.CustomerID == item.Customer.CustomerID).Count() > 1) {
                    result.SetErrorMsg("学员不能重复！");
                }
            }
            return result;
        }
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

        #region 资产学生 全部提供

        public Asset[] Assets { get; set; }
        #endregion

        #region 行为处理
        /// <summary>
        /// 新增班级检查
        /// </summary>
        /// <returns></returns>
        public CheckResultModel CheckCreatClass() {
            CheckResultModel result = new CheckResultModel();

            //1.必填项验证
            if (Product == null)
            {
                result.SetErrorMsg("产品为空！");
                return result;
            }
            if (DayOfWeeks == null || DayOfWeeks.Count() == 0)
            {
                result.SetErrorMsg("没有选择按周的上课时间！");
                return result;
            }
            if (CustomerCollection == null || CustomerCollection.CustomerCollection == null || CustomerCollection.CustomerCollection.Count() == 0)
            {
                result.SetErrorMsg("没有选择学员！");
                return result;
            }
            if (string.IsNullOrEmpty(TeacherID) || String.IsNullOrEmpty(TeacherCode) || String.IsNullOrEmpty(TeacherName))
            {
                result.SetErrorMsg("没有选择教师！");
                return result;
            }
            if (String.IsNullOrEmpty(Subject) || string.IsNullOrEmpty(SubjectName))
            {
                result.SetErrorMsg("没有选择科目！");
                return result;
            }
            if (string.IsNullOrEmpty(Grade) || string.IsNullOrEmpty(GradeName))
            {
                result.SetErrorMsg("没有选择年级！");
                return result;
            }

            //2.产品验证
            result = CheckProduct();
            if (!result.Sucess)
                return result;

            //3.学员验证
            result = CheckCustomer();
            if (!result.Sucess)
                return result;



            return result;
        }
        #endregion
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