using MCS.Web.MVC.Library.Filters;
using PPTS.Data.Common.Entities;
using PPTS.WebAPI.Orders.ViewModels.Assignment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using MCS.Library.Data;
using PPTS.Data.Orders.DataSources;
using PPTS.Data.Common.Adapters;
using PPTS.Data.Orders.Entities;
using PPTS.Data.Orders.Adapters;
using PPTS.Data.Orders;
using PPTS.WebAPI.Orders.Executors;
using MCS.Library.OGUPermission;
using MCS.Library.Principal;
using PPTS.Data.Common.Security;
using PPTS.Contracts.Customers.Models;

namespace PPTS.WebAPI.Orders.Controllers
{
    [ApiPassportAuthentication]
    public class TeacherAssignmentController : ApiController
    {

        #region api/teacherassignment/getTeacherList

        /// 按教师排课，获取选择教师的列表数据
        [HttpPost]
        public TeacherQCR GetTeacherList(TeacherQCM criteria)
        {
            IOrganization organ = DeluxeIdentity.CurrentUser.GetCurrentJob().GetParentOrganizationByType(DepartmentType.Campus);
            if (organ == null)
            {
                return new TeacherQCR()
                {
                    QueryResult = new PagedQueryResult<TeacherJobView, TeacherJobViewCollection>(),
                    Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(TeacherJobView))
                };
            }
            criteria.CampusID = organ.ID;
            if (!string.IsNullOrEmpty(criteria.GradeMemo) && criteria.GradeMemo.Contains("41"))
                criteria.GradeMemo = string.Empty;

            return new TeacherQCR()
            {
                QueryResult = GenericMetaDataSource<TeacherJobView, TeacherJobViewCollection>.Instance.Query(criteria.PageParams, criteria, criteria.OrderBy),
                Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(TeacherJobView))
            };
        }

        /// 按教师排课，获取选择教师的列表数据（支持翻页事件）
        [HttpPost]
        public PagedQueryResult<TeacherJobView, TeacherJobViewCollection> GetTeacherListPaged(TeacherQCM criteria)
        {
            IOrganization organ = DeluxeIdentity.CurrentUser.GetCurrentJob().GetParentOrganizationByType(DepartmentType.Campus);
            if (organ == null)
            {
                return new PagedQueryResult<TeacherJobView, TeacherJobViewCollection>();
            }
            criteria.CampusID = organ.ID;
            if (!string.IsNullOrEmpty(criteria.GradeMemo) && criteria.GradeMemo.Contains("41"))
                criteria.GradeMemo = string.Empty;
            return GenericMetaDataSource<TeacherJobView, TeacherJobViewCollection>.Instance.Query(criteria.PageParams, criteria, criteria.OrderBy);
        }
        #endregion

        #region api/teacherassignment/getTeacherWeekCourse
        ///按照教师排课，初始化教师周视图页面数据
        [HttpPost]
        public dynamic GetTeacherWeekCourse(TeacherAssignQM qConditon)
        {
            qConditon.StartTime = qConditon.StartTime.Date;
            qConditon.EndTime = qConditon.EndTime.Date;

            AssignCollection ac = AssignsAdapter.Instance.LoadCollection(AssignTypeDefine.ByTeacher, qConditon.TeacherJobID, qConditon.StartTime, qConditon.EndTime, false);

            var acLst = from r in ac
                        where (r.AssignStatus == AssignStatusDefine.Assigned || r.AssignStatus == AssignStatusDefine.Finished ||
                        r.AssignStatus == AssignStatusDefine.Exception)
                        && (r.AssignSource == AssignSourceDefine.Automatic || r.AssignSource == AssignSourceDefine.Manual)
                        select r;

            if (acLst != null && !string.IsNullOrEmpty(qConditon.AssignStatus) && qConditon.AssignStatus != "-1")
                acLst = acLst.Where(p => p.AssignStatus == (AssignStatusDefine)Convert.ToInt32(qConditon.AssignStatus));
            if (acLst != null && !string.IsNullOrEmpty(qConditon.AssignSource) && qConditon.AssignSource != "-1")
                acLst = acLst.Where(p => p.AssignSource == (AssignSourceDefine)Convert.ToInt32(qConditon.AssignSource));
            if (acLst != null && !string.IsNullOrEmpty(qConditon.Grade) && qConditon.Grade != "41")
                acLst = acLst.Where(p => p.Grade == qConditon.Grade);
            if (acLst != null && !string.IsNullOrEmpty(qConditon.CustomerName))
                acLst = acLst.Where(p => p.CustomerName.Contains(qConditon.CustomerName));

            Dictionary<string, IEnumerable<BaseConstantEntity>> dic = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(Data.Orders.Entities.Assign));

            return new
            {
                result = acLst,
                Dictionaries = dic
            };
        }
        ///按照教师排课，为周视图页面获取指定教师当前月排课统计数据
        [HttpPost]
        public dynamic GetCurMonthStat(TeacherAssignQM qConditon)
        {
            DateTime sdate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime edate = new DateTime(DateTime.Now.Year, DateTime.Now.Month + 1, 1);
            AssignCollection ac = AssignsAdapter.Instance.LoadCollection(AssignTypeDefine.ByTeacher, qConditon.TeacherJobID, sdate, edate, false);
            string teacherName = string.Empty;
            if (ac.Count > 0)
                teacherName = ac[0].TeacherName;
            var c1 = ac.Where(p => p.AssignStatus == AssignStatusDefine.Assigned).Count();
            var c2 = ac.Where(p => p.AssignStatus == AssignStatusDefine.Finished).Count();
            var result = new { TeacherName = teacherName, AssignedCourse = c1, FinishedCourse = c2 };
            return result;
        }
        #endregion

        #region api/teacherassignment/createAssign
        ///按教师排课，新增排课时，初始化排课页面数据
        [HttpPost]
        public InitDataByTchCAQR InitCreateAssign(TeacherAssignQM queryModel)
        {
            AssignConditionCollection acc = AssignConditionAdapter.Instance.LoadCollection(AssignTypeDefine.ByTeacher, queryModel.TeacherID, queryModel.TeacherJobID);
            acc.Insert(0, new AssignCondition() { ConditionID = "-1", ConditionName4Customer = "新建", ConditionName4Teacher = "新建" });

            IOrganization org = DeluxeIdentity.CurrentUser.GetCurrentJob().GetParentOrganizationByType(DepartmentType.Campus);
            if (org == null)
            {
                return new InitDataByTchCAQR()
                {
                    AssignCondition = acc,
                    Student = new StudentModel(),
                };
            }

            var dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(Data.Orders.Entities.Assign));

            ///从服务中获取教师指定的学员列表
            StudentModel cust = GetCustomerByTchID(queryModel.TeacherJobID, org.ID, dictionaries);
            InitDataByTchCAQR result = new InitDataByTchCAQR()
            {
                AssignCondition = acc,
                Student = cust,
            };

            TeacherJobView tjv = TeacherJobViewAdapter.Instance.Load(queryModel.TeacherJobID);
            if (tjv == null)
                throw new Exception(string.Format("未能查找到岗位ID：{0}的教师信息", queryModel.TeacherJobID));

            result.Assign.TeacherID = tjv.TeacherID;
            result.Assign.TeacherName = tjv.TeacherName;
            result.Assign.TeacherJobID = tjv.JobID;
            result.Assign.TeacherJobOrgID = tjv.JobOrgID;
            result.Assign.TeacherJobOrgName = tjv.JobOrgName;
            result.Assign.IsFullTimeTeacher = tjv.IsFullTime;

            result.Assign.CampusID = org.ID;
            result.Assign.CampusName = org.Name;
            return result;
        }
        ///根据教师ID及教师岗位，获分配给教师的学员信息
        private StudentModel GetCustomerByTchID(string teacherJobID, string campusID, Dictionary<string, IEnumerable<BaseConstantEntity>> dic)
        {
            // CustomerCampusID = "18-Org", CustomerCampusName = "北京分公司方庄校区" 

            var student = new OrderCommonHelper().GetStudent(teacherJobID, campusID, dic);

            //student.TeacherJobOrgID = "6306-Org";
            //student.TeacherJobOrgName = "北京分公司方庄校区校教学部文科组";

            //student.Student.Add(new KeyValue() { Key = "3797053", Value = "李泓辰", Field01 = "S131124000281" });
            //student.Student.Add(new KeyValue() { Key = "1723232", Value = "张启凡", Field01 = "S121014000179" });
            //student.Student.Add(new KeyValue() { Key = "3783587", Value = "孙磊", Field01 = "S131117000755" });

            //student.Grade = new List<KeyValue>();
            //student.Grade.Add(new KeyValue { Key = "21", Value = "初中一年级" });
            //student.Grade.Add(new KeyValue { Key = "22", Value = "初中二年级" });
            //student.Grade.Add(new KeyValue { Key = "16", Value = "小学六年级" });

            //student.GradeSubjectRela = new Dictionary<string, IList<KeyValue>>();
            //student.GradeSubjectRela.Add("21",new List<KeyValue>());
            //student.GradeSubjectRela["21"].Add(new KeyValue() { Key="3",Value= "英语" });

            //student.GradeSubjectRela.Add("22", new List<KeyValue>());
            //student.GradeSubjectRela["22"].Add(new KeyValue() { Key = "3", Value = "英语" });

            //student.GradeSubjectRela.Add("16", new List<KeyValue>());
            //student.GradeSubjectRela["16"].Add(new KeyValue() { Key = "3", Value = "英语" });

            return student;
        }

        ///按教师排课，当学员下拉框发生改变时，加载对应资产
        ///1. 指定了科目和年级的资产
        ///2. 通用资产，没有具体指定科目或者年级
        [HttpPost]
        public SimpleAssetViewCollection GetAssetByCustomerID(CrumbsQM queryModel)
        {
            string customerID = queryModel.CustomerID;
            AssetViewCollection avm = AssetViewAdapter.Instance.LoadCollection(customerID);
            return new SimpleAssetViewCollection { Result = avm };
        }

        ///按教师排课，当选择已经存在的排课条件时，根据资产ID加载资产
        [HttpPost]
        public SimpleAssetView GetAssetByAssetID(CrumbsQM queryModel)
        {
            string customerID = queryModel.CustomerID;
            string assetID = queryModel.AssetID;

            AssetView avm = AssetViewAdapter.Instance.Load(customerID, assetID);
            return new SimpleAssetView { Result = avm };
        }

        ///保存排课条件
        [HttpPost]
        public dynamic CreateAssign(AssignSuperModel asm)
        {
            AssignAddExecutor ac = new AssignAddExecutor(asm);
            var result = ac.Execute();
            return new { assignID = ac.Model.AssignID };
        }
        #endregion

        #region api/teacherassignment/cancelAssign
        /// <summary>
        /// 按教师排课，取消排课
        /// </summary>
        /// <param name="model"></param>
        /// 
        [HttpPost]
        public void CancelAssign(AssignCollection model)
        {
            AssignCancelExecutor executor = new AssignCancelExecutor(model);
            executor.Execute();
        }
        #endregion

        #region api/teacherassignment/copyAssign
        /// <summary>
        /// 按教师排课,复制排课
        /// </summary>
        /// <param name="result"></param>
        /// 
        [HttpPost]
        public void CopyAssign(AssignCopyQM queryModel)
        {
            queryModel.CustomerID = string.Empty;
            queryModel.SrcDateStart = queryModel.SrcDateStart.Date;
            queryModel.SrcDateEnd = queryModel.SrcDateEnd.Date;
            queryModel.DestDateStart = queryModel.DestDateStart.Date;
            queryModel.DestDateEnd = queryModel.DestDateEnd.Date;
            AssignCopyExecutor ace = new AssignCopyExecutor(queryModel);
            ace.Execute();
        }
        #endregion

        #region api/teacherassignment/resetAssign

        ///按教师排课 调整课表获取初始化数据
        [HttpPost]
        public dynamic InitResetAssign()
        {
            Dictionary<string, IEnumerable<BaseConstantEntity>> dic = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(Data.Orders.Entities.Assign));
            var allowDateTime = DateTime.Now.Date.AddDays(10);
            var result = new
            {
                AllowDateTime = allowDateTime,
                Dictionaries = dic
            };
            return result;
        }

        ///按教师排课, 调整课表
        [HttpPost]
        public void ResetAssign(IList<AssignResetQM> queryModel)
        {
            AssignResetExecutor executor = new AssignResetExecutor(queryModel);
            executor.Execute();
        }
        #endregion

        #region api/studentassignment/getSCLV
        /// <summary>
        /// 按教师排课，列表视图， 获取排课列表视图数据
        /// </summary>
        /// <param name="acQM"></param>
        /// <returns></returns>
        /// 
        [HttpPost]
        public AssignQCR GetSCLV(AssignQCM criteria)
        {
            criteria.CustomerID = string.Empty;
            criteria.TeacherName = string.Empty;

            Dictionary<string, IEnumerable<BaseConstantEntity>> dic = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(Data.Orders.Entities.Assign));
            ///处理查询条件   
            //dic = new OrderCommonHelper().GetWCAS(dic);

            criteria.AssignStatus = new[] { (int)AssignStatusDefine.Assigned, (int)AssignStatusDefine.Finished, (int)AssignStatusDefine.Exception };
            AssignQCR result = new AssignQCR
            {
                QueryResult = GenericOrderDataSource<Data.Orders.Entities.Assign, AssignCollection>.Instance.Query(criteria.PageParams, criteria, criteria.OrderBy),
                Dictionaries = dic
            };
            return result;
        }
        /// <summary>
        /// 按教师排课，列表视图， 获取排课列表视图数据(分页事件)
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        /// 
        [HttpPost]
        public PagedQueryResult<Data.Orders.Entities.Assign, AssignCollection> GetPagedSCLV(AssignQCM criteria)
        {
            criteria.CustomerID = string.Empty;
            criteria.TeacherName = string.Empty;

            criteria.AssignStatus = new[] { (int)AssignStatusDefine.Assigned, (int)AssignStatusDefine.Finished, (int)AssignStatusDefine.Exception };
            return GenericOrderDataSource<Data.Orders.Entities.Assign, AssignCollection>.Instance.Query(criteria.PageParams, criteria, criteria.OrderBy);
        }
        #endregion

    }
}