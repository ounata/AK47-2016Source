using MCS.Library.Core;
using MCS.Library.Data;
using MCS.Library.Office.OpenXml.Excel;
using MCS.Library.OGUPermission;
using MCS.Library.Principal;
using MCS.Web.MVC.Library.ApiCore;
using MCS.Web.MVC.Library.Filters;
using MCS.Web.MVC.Library.ModelBinder;
using PPTS.Data.Common;
using PPTS.Data.Common.Adapters;
using PPTS.Data.Common.Entities;
using PPTS.Data.Common.Security;
using PPTS.Data.Orders;
using PPTS.Data.Orders.Adapters;
using PPTS.Data.Orders.DataSources;
using PPTS.Data.Orders.Entities;
using PPTS.Data.Products;
using PPTS.Web.MVC.Library.Filters;
using PPTS.WebAPI.Orders.Executors;
using PPTS.WebAPI.Orders.Service;
using PPTS.WebAPI.Orders.ViewModels.Assignment;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.ModelBinding;

namespace PPTS.WebAPI.Orders.Controllers
{
    [ApiPassportAuthentication]
    public partial class StudentCourseController : ApiController
    {
        #region api/studentcourse/getStuCourse

        /// 获取学员课表列表数据
        [HttpPost]
        [PPTSJobFunctionAuthorize("PPTS:客户课表管理列表（打印课表）,客户课表管理列表（打印课表）-本部门,客户课表管理列表（打印课表）-本校区,客户课表管理列表（打印课表）-本分公司,客户课表管理列表（打印课表）-全国")]
        public AssignQCR GetStuCourse(AssignQCM qcm)
        {
            if (DeluxeIdentity.CurrentUser.GetCurrentJob().JobType == Data.Common.JobTypeDefine.Teacher)
                qcm.TeacherJobID = DeluxeIdentity.CurrentUser.GetCurrentJob().ID;

            this.GetStuCourseCondition(qcm);
            Dictionary<string, IEnumerable<BaseConstantEntity>> dic = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(Assign));
            ///处理查询条件
            new OrderCommonHelper().GetProductCategoryType(dic);

            return new AssignQCR()
            {
                QueryResult = GenericOrderDataSource<Data.Orders.Entities.Assign, AssignCollection>.Instance.Query(qcm.PageParams, qcm, qcm.OrderBy),
                Dictionaries = dic
            };
        }

        [HttpPost]
        [PPTSJobFunctionAuthorize("PPTS:客户课表管理列表（打印课表）,客户课表管理列表（打印课表）-本部门,客户课表管理列表（打印课表）-本校区,客户课表管理列表（打印课表）-本分公司,客户课表管理列表（打印课表）-全国")]
        public PagedQueryResult<Assign, AssignCollection> GetStuCoursePaged(AssignQCM qcm)
        {
            if (DeluxeIdentity.CurrentUser.GetCurrentJob().JobType == Data.Common.JobTypeDefine.Teacher)
                qcm.TeacherJobID = DeluxeIdentity.CurrentUser.GetCurrentJob().ID;
            this.GetStuCourseCondition(qcm);
            return GenericOrderDataSource<Data.Orders.Entities.Assign, AssignCollection>.Instance.Query(qcm.PageParams, qcm, qcm.OrderBy);
        }

        private void GetStuCourseCondition(AssignQCM criteriaQCM)
        {
            if (criteriaQCM.EndTime != DateTime.MinValue)
            {
                criteriaQCM.EndTime = criteriaQCM.EndTime.AddDays(1);
            }
            ///排除无效状态的
            if (criteriaQCM.AssignStatus != null && criteriaQCM.AssignStatus.Length == 0)
            {
                criteriaQCM.AssignStatus = new int[] { (int)AssignStatusDefine.Assigned, (int)AssignStatusDefine.Exception, (int)AssignStatusDefine.Finished };
            }
            ///
            if (criteriaQCM.CategoryType != null && criteriaQCM.CategoryType.Length == 0)
            {
                criteriaQCM.CategoryType = new string[] { ((int)CategoryType.OneToOne).ToString(), ((int)CategoryType.CalssGroup).ToString() };
            }
        }

        [HttpPost]
        [PPTSJobFunctionAuthorize("PPTS:按钮-客户课表导出")]
        public HttpResponseMessage ExportPageStuCourse([ModelBinder(typeof(FormBinder))] AssignQCM qcm)
        {

            if (DeluxeIdentity.CurrentUser.GetCurrentJob().JobType == Data.Common.JobTypeDefine.Teacher)
                qcm.TeacherJobID = DeluxeIdentity.CurrentUser.GetCurrentJob().ID;

            this.GetStuCourseCondition(qcm);
            PagedQueryResult<Data.Orders.Entities.Assign, AssignCollection> dc = GenericOrderDataSource<Assign, AssignCollection>.Instance.Query(qcm.PageParams, qcm, qcm.OrderBy);

            WorkBook wb = WorkBook.CreateNew();
            WorkSheet sheet = wb.Sheets["sheet1"];

            TableDescription tableDesp = new TableDescription("学生课表");

            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("教师姓名", typeof(string))) { PropertyName = "TeacherName" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("学员姓名", typeof(string))) { PropertyName = "CustomerName" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("学员编号", typeof(string))) { PropertyName = "CustomerCode" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("上课日期", typeof(string))) { PropertyName = "StartTime" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("上课时段", typeof(string))) { PropertyName = "CourseSE" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("课时", typeof(string))) { PropertyName = "Amount" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("实际小时", typeof(string))) { PropertyName = "RealTime" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("上课科目", typeof(double))) { PropertyName = "SubjectName" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("上课年级", typeof(string))) { PropertyName = "GradeName" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("学管师", typeof(string))) { PropertyName = "EducatorName" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("咨询师", typeof(string))) { PropertyName = "ConsultantName" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("课时状态", typeof(decimal))) { PropertyName = "AssignStatus" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("课时类型", typeof(decimal))) { PropertyName = "AssignSource" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("订单编号", typeof(int))) { PropertyName = "AssetCode" });

            string[] wname = new string[] { "星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六" };

            sheet.LoadFromCollection(dc.PagedData, tableDesp, (cell, param) =>
            {
                if (param.PropertyValue != null && !string.IsNullOrEmpty(param.PropertyValue.ToString()))
                {
                    switch (param.ColumnDescription.PropertyName)
                    {
                        case "StartTime":
                            var sDate = Convert.ToDateTime(param.PropertyValue);
                            cell.Value = string.Format("{0}({1})", sDate.ToString("yyyy-MM-dd"), wname[(int)sDate.DayOfWeek]);
                            break;
                        case "AssignStatus":
                            ConstantEntity status = ConstantAdapter.Instance.Get("C_CODE_ABBR_Course_AssignStatus", ((int)((AssignStatusDefine)param.PropertyValue)).ToString());
                            cell.Value = status == null ? param.PropertyValue.ToString() : status.Value;
                            break;
                        case "AssignSource":
                            ConstantEntity source = ConstantAdapter.Instance.Get("C_CODE_ABBR_Assign_Source", ((int)((AssignSourceDefine)param.PropertyValue)).ToString());
                            cell.Value = source == null ? param.PropertyValue.ToString() : source.Value;
                            break;
                        default:
                            cell.Value = param.ColumnDescription.FormatValue(param.PropertyValue);
                            break;
                    }
                }
            });
            return wb.ToResponseMessage(string.Format("学生课表_{0}.xlsx", DateTime.Now.ToString("yyyyMMddHHmmss")));
        }


        #endregion

        #region api/studentcourse/deleteAssign

        /// 学员课表，删除课表
        [HttpPost]
        [PPTSJobFunctionAuthorize("PPTS:按钮-录入/删除/确认课时-本校区")]
        public void DeleteAssign(AssignCollection model)
        {
            AssignDeleteExecutor executor = new AssignDeleteExecutor(model);
            executor.Execute();
            AssignTaskService.UpdateCustomerSearchInfo(executor.CustomerIDTask);
        }
        #endregion

        #region api/studentcourse/confirmAssign

        /// 学员课表，确认课表
        [HttpPost]
        [PPTSJobFunctionAuthorize("PPTS:按钮-录入/删除/确认课时-本校区")]
        public void ConfirmAssign(AssignCollection model)
        {
            AssignConfirmExecutor executor = new AssignConfirmExecutor(model);
            executor.Execute();
            AssignTaskService.UpdateCustomerSearchInfo(executor.CustomerIDTask);
        }

        #endregion

        #region api/studentcourse/markupAssign

        ///学员课表 补录课时
        [HttpPost]
        [PPTSJobFunctionAuthorize("PPTS:按钮-录入/删除/确认课时-本校区")]
        public dynamic MarkupAssign(AssignSuperModel asm)
        {
            AssignMarkupExecutor ac = new AssignMarkupExecutor(asm);
            var result = ac.Execute();
            if (string.IsNullOrEmpty(ac.Msg))
                ac.Msg = "ok";
            AssignTaskService.UpdateCustomerSearchInfo(asm.CustomerID);
            return new { Msg = ac.Msg };
        }

        #endregion



    }
}