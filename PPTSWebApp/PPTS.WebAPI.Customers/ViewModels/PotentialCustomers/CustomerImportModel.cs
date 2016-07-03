using System;
using System.Data;
using MCS.Library.Data.DataObjects;
using MCS.Library.Office.OpenXml.Excel;
using System.Net.Http;
using MCS.Web.MVC.Library.ApiCore;
using MCS.Library.Core;
using System.Text;
using MCS.Library.Data.Mapping;
using PPTS.WebAPI.Customers.Executors;
using System.Web;
using MCS.Library.SOA.DataObjects;
using PPTS.Data.Customers;
using PPTS.Data.Customers.Adapters;
using PPTS.Data.Customers.Entities;
using PPTS.Data.Common.Adapters;
using PPTS.Data.Common.Entities;
using MCS.Library.Data;

namespace PPTS.WebAPI.Customers.ViewModels.PotentialCustomers
{
    [Serializable]
    public class CustomerImportModel
    {
        #region 导入学员信息
        /// <summary>
        /// 学生姓名
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// 当前所在学校
        /// </summary>
        public string SchoolName { get; set; }
        /// <summary>
        /// 当前所在年级
        /// </summary>
        public string Grade { get; set; }
        /// <summary>
        /// 文理科
        /// </summary>
        public string SubjectType { get; set; }
        /// <summary>
        /// 学生性别
        /// </summary>
        public string CustomerGender { get; set; }
        /// <summary>
        /// 家长姓名
        /// </summary>
        public string ParentName { get; set; }
        /// <summary>
        /// 家长性别
        /// </summary>
        public string ParentGender { get; set; }
        /// <summary>
        /// 家长联系方式
        /// </summary>
        public string PhoneNumber { get; set; }
        /// <summary>
        /// 信息来源一级
        /// </summary>
        public string SourceMainType { get; set; }
        /// <summary>
        /// 信息来源二级
        /// </summary>
        public string SourceSubType { get; set; }
        /// <summary>
        /// 机构ID
        /// </summary>
        public string OrgID { get; set; }
        /// <summary>
        /// 机构名称
        /// </summary>
        public string OrgName { get; set; }
        /// <summary>
        /// 机构类型
        /// </summary>
        public string OrgType { get; set; }
        #endregion

        public static StudentBranchType GetSubjectType(string subjectType)
        {
            StudentBranchType type = StudentBranchType.NoBranch;
            switch (subjectType)
            {
                case "文科":
                    type = StudentBranchType.LiberalArts;
                    break;
                case "理科":
                    type = StudentBranchType.Science;
                    break;
                default:
                    type = StudentBranchType.NoBranch;
                    break;
            }
            return type;
        }

        public static string GetSchoolID(string schoolName)
        {
            School school = SchoolAdapter.Instance.LoadByName(schoolName);
            return school == null ? "" : school.SchoolID;
        }

        public static string GetGradeID(string gradeName)
        {
            ConstantEntityInCategoryCollection grades = ConstantAdapter.Instance.GetByCategory("C_CODE_ABBR_CUSTOMER_GRADE", false);
            ConstantEntity grade = grades.Find(g => g.Value == gradeName);
            return grade == null ? "" : grade.Key;
        }

        public static DataTable CreateTable()
        {
            DataTable table = new DataTable();

            DataColumn column = new DataColumn();
            column.ColumnName = "CustomerName";
            table.Columns.Add(column);

            column = new DataColumn();
            column.ColumnName = "SchoolName";
            table.Columns.Add(column);

            column = new DataColumn();
            column.ColumnName = "Grade";
            table.Columns.Add(column);

            column = new DataColumn();
            column.ColumnName = "SubjectType";
            table.Columns.Add(column);

            column = new DataColumn();
            column.ColumnName = "CustomerGender";
            table.Columns.Add(column);

            column = new DataColumn();
            column.ColumnName = "ParentName";
            table.Columns.Add(column);

            column = new DataColumn();
            column.ColumnName = "ParentGender";
            table.Columns.Add(column);

            column = new DataColumn();
            column.ColumnName = "PhoneNumber";
            table.Columns.Add(column);

            return table;
        }

        public static DataTable ReadContentToDataTable(WorkSheet sheet)
        {
            DataTable table = CreateTable();
            if (sheet != null)
            {
                DataRow row = null;
                Cell cell = null;
                string cellName = string.Empty;
                Cell startCell = sheet.Cells[2, 1];
                for (int r = startCell.Row.Index; r <= sheet.Rows.Count; r++)
                {
                    bool flag = false;
                    row = table.NewRow();
                    for (int c = startCell.Column.Index; c <= 8; c++)
                    {
                        cell = sheet.Cells[r + 0, c + 0];
                        row[c - 1] = cell.Value == null ? "" : cell.Value.ToString();
                        if (cell.Value != null)
                            flag = true;
                    }
                    if (!flag)
                        break;
                    table.Rows.Add(row);
                }
            }
            return table;
        }

        public static void ImportCustomersHandler(HttpContextBase context, HttpRequestMessage request)
        {
            HttpRequestBase requestInfo = context.Request;
            string fileName = requestInfo.Form["fileName"];
            string sourceMainType = requestInfo.Form["sourceMainType"];
            string sourceSubType = requestInfo.Form["sourceSubType"];
            string orgID = requestInfo.Form["orgID"];
            string orgName = requestInfo.Form["orgName"];
            string orgType = requestInfo.Form["orgType"];

            string log = string.Empty;
            request.ProcessFileUpload(pfArguments =>
            {
                WorkBook workBook = WorkBook.Load(pfArguments.UploadedStream);

                workBook.Sheets.Any.IsNotNull(sheet =>
                {
                    DataTable dt = ReadContentToDataTable(sheet);

                    if (dt.Rows.Count > 300)
                    {
                        throw new Exception("<span style='color:red'>ERROR: 最多导入300条客户数据</span>");
                    }

                    StringBuilder errors = new StringBuilder();

                    int errorCount = 0;

                    pfArguments.Progress.MaxStep = dt.Rows.Count;

                    bool isSuccess = true;
                    string message = string.Empty;
                    StringBuilder errorBuilder = new StringBuilder();
                    foreach (DataRow row in dt.Rows)
                    {
                        isSuccess = true;
                        message = string.Empty;
                        try
                        {
                            //这里可以从DataRow转成对象。ORMapping.DataRowToObject
                            CustomerImportModel importCustomer = new CustomerImportModel();
                            importCustomer = ORMapping.DataRowToObject(row, importCustomer);
                            importCustomer.SourceMainType = sourceMainType;
                            importCustomer.SourceSubType = sourceSubType;
                            importCustomer.OrgID = orgID;
                            importCustomer.OrgName = orgName;
                            importCustomer.OrgType = orgType;

                            if (string.IsNullOrEmpty(importCustomer.CustomerName) || string.IsNullOrEmpty(importCustomer.ParentName) || string.IsNullOrEmpty(importCustomer.PhoneNumber))
                            {
                                throw new Exception("必填信息不完整");
                            }

                            CustomerImportExecutor executor = new CustomerImportExecutor(importCustomer);
                            executor.Execute();

                            //只要有数据处理成功，就标记数据改变了
                            pfArguments.ProcessResult.DataChanged = true;
                        }
                        catch (Exception ex)
                        {
                            isSuccess = false;
                            message = ex.Message;

                            errorCount += 1;

                            //只要有数据错误，就不要关闭窗口，由用户来查看日志
                            pfArguments.ProcessResult.CloseWindow = false;

                            if (errors.Length > 0)
                                errors.Append("\n");

                            errors.Append(ex.Message);
                        }
                        finally
                        {
                            string result = string.Format("第{0}行数据 {1} {2}", pfArguments.Progress.CurrentStep + 1, isSuccess ? "成功" : "失败", message);
                            pfArguments.Progress.CurrentStep += 1;
                            pfArguments.Progress.Response(result);
                            if (!isSuccess)
                                errorBuilder.AppendLine(result);
                        }
                    }
                    log = string.Format("总共处理了{0}条数据 {1}成功 {2}条失败", dt.Rows.Count, dt.Rows.Count - errorCount, errorCount);
                    pfArguments.ProcessResult.ProcessLog = log;
                    // pfArguments.ProcessResult.Error = errors.ToString();

                    UserOperationLog operationLog = UserOperationLog.FromEnvironment();
                    operationLog.ResourceID = "IMPORT-CUSTOMERS-F-CM-AC-002";
                    operationLog.Subject = "批量导入客户资源";
                    operationLog.ActivityName = fileName;
                    operationLog.OperationName = log;
                    operationLog.OperationDescription = errorBuilder.ToString();
                    operationLog.OperationType = OperationType.Add;
                    operationLog.OperationDateTime = TimeZoneContext.Current.ConvertTimeToUtc(DateTime.Now);
                    UserOperationLogAdapter.Instance.InsertData(operationLog);
                    CustomerUserOperationLogAdapter.Instance.InsertData(operationLog);
                });
                return true;
            });
        }
    }

    [Serializable]
    public class CustomerImportModelCollection : EditableDataObjectCollectionBase<CustomerImportModel>
    {
    }

    public class CustomerImportHistoryQueryResult
    {
        public PagedQueryResult<UserOperationLog, UserOperationLogCollection> QueryResult
        {
            get; set;
        }
    }

    public class CustomerImportHistoryCriteriaModel
    {
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
