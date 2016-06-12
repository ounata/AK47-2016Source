using MCS.Library.Core;
using MCS.Library.Data;
using MCS.Library.Office.OpenXml.Excel;
using MCS.Library.OGUPermission;
using MCS.Library.Principal;
using MCS.Web.MVC.Library.ApiCore;
using MCS.Web.MVC.Library.Filters;
using MCS.Web.MVC.Library.ModelBinder;
using PPTS.Data.Common.Adapters;
using PPTS.Data.Common.Entities;
using PPTS.Data.Common.Security;
using PPTS.Data.Orders;
using PPTS.Data.Orders.Adapters;
using PPTS.Data.Orders.DataSources;
using PPTS.Data.Orders.Entities;
using PPTS.WebAPI.Orders.Executors;
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
        /// <summary>
        /// 获取学员课表列表数据
        /// </summary>
        /// <param name="criteriaQCM"></param>
        /// <returns></returns>
        [HttpPost]
        public AssignQCR GetStuCourse(AssignQCM criteriaQCM)
        {
            if (criteriaQCM.EndTime != DateTime.MinValue)
            {
                criteriaQCM.EndTime = criteriaQCM.EndTime.AddDays(1);
            }
            return new AssignQCR()
            {
                QueryResult = GenericOrderDataSource<Data.Orders.Entities.Assign, AssignCollection>.Instance.Query(criteriaQCM.PageParams, criteriaQCM, criteriaQCM.OrderBy),
                Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(Data.Orders.Entities.Assign))
            };
        }

        [HttpPost]
        public PagedQueryResult<Data.Orders.Entities.Assign, AssignCollection> GetStuCoursePaged(AssignQCM criteriaQCM)
        {
            return GenericOrderDataSource<Data.Orders.Entities.Assign, AssignCollection>.Instance.Query(criteriaQCM.PageParams, criteriaQCM, criteriaQCM.OrderBy);
        }

        [HttpPost]
        public HttpResponseMessage ExportPageStuCourse([ModelBinder(typeof(FormBinder))] AssignQCM criteriaQCM)
        {
            PagedQueryResult<Data.Orders.Entities.Assign, AssignCollection> dc = GenericOrderDataSource<Data.Orders.Entities.Assign, AssignCollection>.Instance.Query(criteriaQCM.PageParams, criteriaQCM, criteriaQCM.OrderBy);

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
                            cell.Value = string.Format("{0}({1})",sDate.ToString("yyyy-MM-dd"), wname[(int)sDate.DayOfWeek]);
                            break;
                        case "AssignStatus":
                            ConstantEntity status = ConstantAdapter.Instance.Get("C_CODE_ABBR_Course_AssignStatus",  ((int)((AssignStatusDefine)param.PropertyValue)).ToString());
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
        public void DeleteAssign(AssignCollection model)
        {
            AssignDeleteExecutor executor = new AssignDeleteExecutor(model);
            executor.Execute();
        }
        #endregion

        #region api/studentcourse/confirmAssign

        /// 学员课表，确认课表
        [HttpPost]
        public void ConfirmAssign(AssignCollection model)
        {
            AssignConfirmExecutor executor = new AssignConfirmExecutor(model);
            executor.Execute();
        }

        #endregion

        #region api/studentcourse/markupAssign

        ///保存补录的课时
        [HttpPost]
        public dynamic MarkupAssign(AssignSuperModel asm)
        {
            AssignMarkupExecutor ac = new AssignMarkupExecutor(asm);
            var result = ac.Execute();
            if (string.IsNullOrEmpty(ac.Msg))
                ac.Msg = "ok";
            return new { Msg = ac.Msg };
        }

        #endregion 

      

    }
}