﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MCS.Library.Data;
using PPTS.Data.Orders.Entities;
using PPTS.Data.Orders;
using PPTS.Data.Orders.DataSources;
using PPTS.Data.Orders.Adapters;
using PPTS.Data.Common.Adapters;
using PPTS.WebAPI.Orders.ViewModels.CustomerSearchNS;
using PPTS.WebAPI.Orders.ViewModels.Assignment;
using PPTS.WebAPI.Orders.Executors;
using MCS.Web.MVC.Library.Filters;
using PPTS.Data.Common.Security;
using MCS.Library.Principal;
using MCS.Library.OGUPermission;
using PPTS.Data.Common.Entities;
using PPTS.Contracts.Customers.Models;

namespace PPTS.WebAPI.Orders.Controllers
{
    [ApiPassportAuthentication]
    public class StudentAssignmentController : ApiController
    {
        #region api/studentassignment/getAllStudentAssignment
        /// <summary>
        /// 获取学员待排课列表
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        [HttpPost]
        public CustomerSearchQCR GetAllStudentAssignment(CustomerSearchQCM criteria)
        {
            ///当前操作人所属校区ID
            IOrganization org = DeluxeIdentity.CurrentUser.GetCurrentJob().GetParentOrganizationByType(DepartmentType.Campus);
            if (org == null)
            {
                return new CustomerSearchQCR
                {
                    QueryResult = new PagedQueryResult<CustomerSearch, CustomerSearchCollection>(),
                    Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(CustomerSearch))
                };
            }
            criteria.CampusID = org.ID;
            if (criteria.Grade == "41")
                criteria.Grade = string.Empty;
            CustomerSearchQCR result = new CustomerSearchQCR
            {
                QueryResult = GenericSearchDataSource<CustomerSearch, CustomerSearchCollection>.Instance.Query(criteria.PageParams, criteria, criteria.OrderBy),
                Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(CustomerSearch))
            };
            return result;
        }
        /// <summary>
        /// 获取学员待排课列表，分页获取
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        [HttpPost]
        public PagedQueryResult<CustomerSearch, CustomerSearchCollection> GetPagedStudentAssignment(CustomerSearchQCM criteria)
        {
            IOrganization org = DeluxeIdentity.CurrentUser.GetCurrentJob().GetParentOrganizationByType(DepartmentType.Campus);
            if (org == null)
            {
                return new PagedQueryResult<CustomerSearch, CustomerSearchCollection>();
            }
            criteria.CampusID = org.ID;
            return GenericSearchDataSource<CustomerSearch, CustomerSearchCollection>.Instance.Query(criteria.PageParams, criteria, criteria.OrderBy);
        }

        #endregion

        #region api/studentassignment/createAssignCondition

        ///按照学员排课，新增排课时，初始化排课页面数据
        [HttpPost]
        public InitDataByStuCAQR GetAssignCondition(StudentAssignQM queryModel)
        {
            ///学员ID
            string customerID = queryModel.CustomerID;
            ///当前操作人所属校区ID
            IOrganization org = DeluxeIdentity.CurrentUser.GetCurrentJob().GetParentOrganizationByType(DepartmentType.Campus);
            if (org == null)
            {
                return new InitDataByStuCAQR()
                {
                    AssignExtension = new AssetViewCollection(),
                    AssignCondition = new AssignConditionCollection(),
                    Teacher = new TeacherModel(),
                    Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(Data.Orders.Entities.Assign))
                };
            }
            string operaterCampusID = org.ID;
            AssignConditionCollection acc = AssignConditionAdapter.Instance.LoadCollection(AssignTypeDefine.ByStudent, customerID, string.Empty);
            acc.Insert(0, new AssignCondition() { ConditionID = "-1", ConditionName4Customer = "新建", ConditionName4Teacher = "新建" });

            var dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(Data.Orders.Entities.Assign));
            AssetViewCollection avm = AssetViewAdapter.Instance.LoadCollection(operaterCampusID, customerID);
            ///从服务中获取学员指定的教师列表
            TeacherModel teacher = this.GetTearchByStuID(customerID, operaterCampusID, dictionaries);
            ///返回结果
            InitDataByStuCAQR result = new InitDataByStuCAQR()
            {
                AssignExtension = avm,
                AssignCondition = acc,
                Teacher = teacher,
                Dictionaries = dictionaries
            };
            result.Assign.CampusID = org.ID;
            result.Assign.CampusName = org.Name;
            return result;
        }
        ///保存排课条件
        [HttpPost]
        public dynamic CreateAssignCondition(AssignSuperModel asm)
        {
            AssignAddExecutor ac = new AssignAddExecutor(asm);
            var result = ac.Execute();
            return new { assignID = ac.Model.AssignID };
        }
        /// <summary>
        /// 根据学员ID和科目ID获取对应的教师列表
        /// 该列表数据需要从数据服务提供
        /// </summary>
        /// <param name="queryModel"></param>
        /// <returns></returns>
        private TeacherModel GetTearchByStuID(string stuID, string campusID, Dictionary<string, IEnumerable<BaseConstantEntity>> dic)
        {
            TeacherModel tm = new OrderCommonHelper().GetTeacher(stuID, campusID, dic);
            if (tm.Grade.Count > 0)
                return tm;

            tm.Grade = new List<KeyValue>();
            tm.Grade.Add(new KeyValue() { Key = "21", Value = "初中一年级" });
            tm.Grade.Add(new KeyValue() { Key = "22", Value = "初中二年级" });
            tm.Grade.Add(new KeyValue() { Key = "23", Value = "初中三年级" });
            tm.Grade.Add(new KeyValue() { Key = "24", Value = "初中四年级" });


            tm.GradeSubjectRela = new Dictionary<string, IList<KeyValue>>();
            tm.GradeSubjectRela.Add("21", new List<KeyValue>());
            tm.GradeSubjectRela["21"].Add(new KeyValue() { Key = "1", Value = "语文" });
            tm.GradeSubjectRela["21"].Add(new KeyValue() { Key = "2", Value = "数学" });

            tm.GradeSubjectRela.Add("22", new List<KeyValue>());
            tm.GradeSubjectRela["22"].Add(new KeyValue() { Key = "10", Value = "体育" });

            tm.GradeSubjectRela.Add("23", new List<KeyValue>());
            tm.GradeSubjectRela["23"].Add(new KeyValue() { Key = "11", Value = "奥数" });
            tm.GradeSubjectRela["23"].Add(new KeyValue() { Key = "13", Value = "地理" });

            tm.GradeSubjectRela.Add("24", new List<KeyValue>());
            tm.GradeSubjectRela["24"].Add(new KeyValue() { Key = "14", Value = "政治" });


            tm.Tch = new List<TchSubjectGradeRela>();
            var person = new TchSubjectGradeRela() { Grade = "21", Subject = "1", Teachers = new List<KeyValue>() };
            person.Teachers.Add(new KeyValue() { Key = "105", Value = "王华卫", Field01 = "93276-Group"});
            person.Teachers.Add(new KeyValue() { Key = "11176", Value = "张凯", Field01 = "93235-Group"});
            tm.Tch.Add(person);
            return tm;
        }

        #endregion

        #region api/studentassignment/getStudentWeekCourse

        /// 按照学员排课，初始化学员周视图页面数据，并有查询
        [HttpPost]
        public dynamic GetStudentWeekCourse(StudentAssignQM qConditon)
        {
            Dictionary<string, IEnumerable<BaseConstantEntity>> dic = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(Data.Orders.Entities.Assign));
            ///处理查询条件   
            dic = new OrderCommonHelper().GetWCAS(dic);
            qConditon.StartTime = qConditon.StartTime.Date;
            qConditon.EndTime = qConditon.EndTime.Date;
            AssignCollection ac = AssignsAdapter.Instance.LoadCollection(qConditon.CustomerID, true, qConditon.StartTime, qConditon.EndTime, false);
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
            if (acLst != null && !string.IsNullOrEmpty(qConditon.TeacherName))
                acLst = acLst.Where(p => p.TeacherName.Contains(qConditon.TeacherName));
            return new
            {
                result = acLst,
                Dictionaries = dic
            };
        }

        /// 按照学员排课，为周视图页面，获取指定学员当前月排课统计数据
        [HttpPost]
        public dynamic GetCurMonthStat(StudentAssignQM acQM)
        {
            DateTime sdate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime edate = new DateTime(DateTime.Now.Year, DateTime.Now.Month + 1, 1);
            AssignCollection ac = AssignsAdapter.Instance.LoadCollection(acQM.CustomerID, true, sdate, edate, false);
            string customerName = string.Empty;
            if (ac.Count > 0)
                customerName = ac[0].CustomerName;
            var c1 = ac.Where(p => p.AssignStatus == AssignStatusDefine.Assigned).Count();
            var c2 = ac.Where(p => p.AssignStatus == AssignStatusDefine.Finished).Count();
            var result = new { CustomerName = customerName, AssignedCourse = c1, FinishedCourse = c2 };
            return result;
        }
        #endregion

        #region api/studentassignment/cancelAssign
        /// <summary>
        /// 按学员排课，取消排课
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

        #region api/studentassignment/copyAssign
        /// <summary>
        /// 按学员排课,复制排课
        /// </summary>
        /// <param name="result"></param>
        /// 
        [HttpPost]
        public void CopyAssign(AssignCopyQM queryModel)
        {
            queryModel.TeacherID = string.Empty;
            queryModel.SrcDateStart = queryModel.SrcDateStart.Date;
            queryModel.SrcDateEnd = queryModel.SrcDateEnd.Date;
            queryModel.DestDateStart = queryModel.DestDateStart.Date;
            queryModel.DestDateEnd = queryModel.DestDateEnd.Date;
            AssignCopyExecutor ace = new AssignCopyExecutor(queryModel);
            ace.Execute();
        }
        #endregion

        #region api/studentassignment/resetAssign
        /// <summary>
        /// 按学员排课 调整课表获取初始化数据
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpPost]
        public dynamic ResetAssignInit()
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
        /// <summary>
        /// 按学员排课, 调整课表
        /// </summary>
        /// <param name="model"></param>
        /// 
        [HttpPost]
        public void ResetAssign(IList<AssignResetQM> queryModel)
        {
            AssignResetExecutor executor = new AssignResetExecutor(queryModel);
            executor.Execute();
        }
        #endregion

        #region api/studentassignment/getSCLV
        /// <summary>
        /// 按学员排课，列表视图， 获取排课列表视图数据
        /// </summary>
        /// <param name="acQM"></param>
        /// <returns></returns>
        /// 
        [HttpPost]
        public AssignQCR GetSCLV(AssignQCM criteria)
        {
            criteria.TeacherID = string.Empty;
            criteria.TeacherJobID = string.Empty;
            criteria.CustomerName = string.Empty;

            Dictionary<string, IEnumerable<BaseConstantEntity>> dic = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(Data.Orders.Entities.Assign));
            ///处理查询条件   
            dic = new OrderCommonHelper().GetWCAS(dic);
            criteria.AssignStatus = new[] { (int)AssignStatusDefine.Assigned, (int)AssignStatusDefine.Finished, (int)AssignStatusDefine.Exception };
            AssignQCR result = new AssignQCR
            {
                QueryResult = GenericOrderDataSource<Data.Orders.Entities.Assign, AssignCollection>.Instance.Query(criteria.PageParams, criteria, criteria.OrderBy),
                Dictionaries = dic
            };
            return result;
        }
        /// <summary>
        /// 按学员排课，列表视图， 获取排课列表视图数据(分页事件)
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        /// 
        [HttpPost]
        public PagedQueryResult<Data.Orders.Entities.Assign, AssignCollection> GetPagedSCLV(AssignQCM criteria)
        {
            criteria.TeacherID = string.Empty;
            criteria.TeacherJobID = string.Empty;
            criteria.CustomerName = string.Empty;

            criteria.AssignStatus = new[] { (int)AssignStatusDefine.Assigned, (int)AssignStatusDefine.Finished, (int)AssignStatusDefine.Exception };
            return GenericOrderDataSource<Data.Orders.Entities.Assign, AssignCollection>.Instance.Query(criteria.PageParams, criteria, criteria.OrderBy);
        }
        #endregion

        #region   #region api/studentassignment/getACC
        /// <summary>
        /// 学员视图，排课条件列表
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        /// 
        [HttpPost]
        public AssignConditionQCR GetACC(AssignConditionQCM criteriaQCM)
        {
            return new AssignConditionQCR()
            {
                QueryResult = GenericOrderDataSource<AssignCondition, AssignConditionCollection>.Instance.Query(criteriaQCM.PageParams, criteriaQCM, criteriaQCM.OrderBy)
            };
        }

        public PagedQueryResult<AssignCondition, AssignConditionCollection> GetACCPaged(AssignConditionQCM criteriaQCM)
        {
            return GenericOrderDataSource<AssignCondition, AssignConditionCollection>.Instance.Query(criteriaQCM.PageParams, criteriaQCM, criteriaQCM.OrderBy);
        }
        #endregion

    }
}