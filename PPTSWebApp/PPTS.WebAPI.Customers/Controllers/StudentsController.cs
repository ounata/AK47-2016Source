using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using MCS.Library.Data;
using MCS.Library.Net.SNTP;
using MCS.Library.Office.OpenXml.Excel;
using MCS.Library.OGUPermission;
using MCS.Library.Principal;
using MCS.Web.MVC.Library.ApiCore;
using MCS.Web.MVC.Library.Filters;
using MCS.Web.MVC.Library.ModelBinder;
using MCS.Web.MVC.Library.Models;
using MCS.Web.MVC.Library.Models.Workflow;
using PPTS.Data.Common;
using PPTS.Data.Common.Adapters;
using PPTS.Data.Common.Entities;
using PPTS.Data.Common.Security;
using PPTS.Data.Customers;
using PPTS.Data.Customers.Adapters;
using PPTS.Data.Customers.Entities;
using PPTS.Data.Customers.Executors;
using PPTS.WebAPI.Customers.DataSources;
using PPTS.WebAPI.Customers.Executors;
using PPTS.WebAPI.Customers.ViewModels.Accounts;
using PPTS.WebAPI.Customers.ViewModels.PotentialCustomers;
using PPTS.WebAPI.Customers.ViewModels.Students;
using MCS.Library.Core;
using PPTS.Web.MVC.Library.Filters;

namespace PPTS.WebAPI.Customers.Controllers
{
    [ApiPassportAuthentication]
    public class StudentsController : ApiController
    {
        #region api/students/getallstudents

        /// <summary>
        /// 学员查询，第一次。第一页，下载字典
        /// </summary>
        /// <param name="criteria">查询条件</param>
        /// <returns>返回带字典的潜客数据列表</returns>
        [HttpPost]
        [PPTSJobFunctionAuthorize("PPTS:学员管理,学员管理-本部门,学员管理-本校区,学员管理-本分公司,学员管理-全国")]
        public StudentQueryResult GetAllStudents(StudentQueryCriteriaModel criteria)
        {
            return new StudentQueryResult
            {
                QueryResult = CustomerSearchDataSource.Instance.LoadCustomerSearch(criteria.PageParams, criteria, criteria.OrderBy),
                Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(CustomerSearch))
            };
        }

        [HttpPost]
        [PPTSJobFunctionAuthorize("PPTS:学员管理,学员管理-本部门,学员管理-本校区,学员管理-本分公司,学员管理-全国")]
        public PagedQueryResult<StudentQueryResultModel, StudentQueryResultModelCollection> GetPagedStudents(StudentQueryCriteriaModel criteria)
        {
            return CustomerSearchDataSource.Instance.LoadCustomerSearch(criteria.PageParams, criteria, criteria.OrderBy);
        }

        #endregion

        #region api/students/getstudentinfo

        [HttpGet]
        [PPTSJobFunctionAuthorize("PPTS:学员视图 - 基本信息（含联系方式）, 学员视图 - 基本信息, 学员视图 - 基本信息 - 本部门, 学员视图 - 基本信息 - 本校区, 学员视图 - 基本信息 - 本分公司, 学员视图 - 基本信息 - 全国")]
        public EditableStudentModel GetStudentInfo(string id)
        {
            return EditableStudentModel.Load(id);
        }

        #endregion

        #region api/students/updatestudent
        [HttpPost]
        [PPTSJobFunctionAuthorize("PPTS:修改家长、学员非关键信息,修改家长、学员关键信息-本分公司")]
        public void UpdateStudent(EditableStudentModel model)
        {
            EditableStudentExecutor executor = new EditableStudentExecutor(model);

            executor.Execute();
        }
        #endregion

        #region api/students/updateparent

        [HttpPost]
        [PPTSJobFunctionAuthorize("PPTS:修改家长、学员非关键信息,修改家长、学员关键信息-本分公司")]
        public void UpdateParent(EditableStudentParentModel model)
        {
            EditableStudentParentExecutor executor = new EditableStudentParentExecutor(model);

            executor.Execute();
        }

        #endregion

        #region api/students/addparent

        [HttpPost]
        [PPTSJobFunctionAuthorize("PPTS:修改家长、学员非关键信息,修改家长、学员关键信息-本分公司")]
        public void AddParent(AddStudentParentModel criteria)
        {
            AddStudentParentExecutor executor = new AddStudentParentExecutor(criteria);
            executor.Execute();
        }

        #endregion

        #region api/students/getstudentparents

        [HttpGet]
        public CustomerParentsQueryResult GetStudentParents(string id)
        {
            return CustomerParentsQueryResult.GetCustomerParents(id);
        }
        #endregion

        #region api/students/GetStudentParent
        [HttpGet]
        public EditableStudentParentModel GetStudentParent(string id, string customerId)
        {
            EditableStudentParentModel result = new EditableStudentParentModel();

            GenericParentAdapter<ParentModel, List<ParentModel>>.Instance.LoadInContext(id, parent => result.Parent = parent);

            GenericCustomerAdapter<StudentModel, List<StudentModel>>.Instance.LoadInContext(customerId, customer => result.Customer = customer);

            PhoneAdapter.Instance.LoadByOwnerIDInContext(id, phones => result.Parent.FillFromPhones(phones));

            PhoneAdapter.Instance.GetDbContext().DoAction(context => context.ExecuteDataSetSqlInContext());

            result.CustomerParentRelation = CustomerParentRelationAdapter.Instance.Load(customerId, id);

            result.Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(CreatablePortentialCustomerModel), typeof(PotentialCustomer), typeof(Parent));

            return result;
        }
        #endregion

        #region api/students/exportallstudents

        [HttpPost]
        [PPTSJobFunctionAuthorize("PPTS:学员管理导出")]
        public HttpResponseMessage ExportAllStudents([ModelBinder(typeof(FormBinder))] StudentQueryCriteriaModel criteria)
        {
            criteria.PageParams.PageIndex = 1;
            criteria.PageParams.PageSize = 5000;
            PagedQueryResult<StudentQueryResultModel, StudentQueryResultModelCollection> queryResult = CustomerSearchDataSource.Instance.LoadCustomerSearch(criteria.PageParams, criteria, criteria.OrderBy);

            WorkBook wb = WorkBook.CreateNew();
            WorkSheet sheet = wb.Sheets["sheet1"];
            TableDescription tableDesp = new TableDescription("学员管理");
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("学员姓名", typeof(string))) { PropertyName = "CustomerName" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("学员编号", typeof(string))) { PropertyName = "CustomerCode" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("家长姓名", typeof(string))) { PropertyName = "ParentName" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("首次签约日期", typeof(string))) { PropertyName = "FirstSignTime" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("在读学校", typeof(string))) { PropertyName = "SchoolName" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("当前年级", typeof(string))) { PropertyName = "Grade" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("归属咨询师", typeof(string))) { PropertyName = "EducatorName" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("归属学管师", typeof(string))) { PropertyName = "ConsultantName" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("签约课时", typeof(string))) { PropertyName = "AssignedAmount" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("剩余课时", typeof(string))) { PropertyName = "AssetRemainAmount" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("账户价值", typeof(string))) { PropertyName = "AccountMoney" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("订购资金余额", typeof(string))) { PropertyName = "OrderMoney" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("可用金额", typeof(string))) { PropertyName = "AvaiableMoney" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("距最后上课", typeof(string))) { PropertyName = "LastAssignDays" });

            var dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(CustomerSearch));
            sheet.LoadFromCollection(queryResult.PagedData, tableDesp, (cell, param) =>
            {
                if (param.PropertyValue != null && !string.IsNullOrEmpty(param.PropertyValue.ToString()))
                {
                    switch (param.ColumnDescription.PropertyName)
                    {
                        case "Grade":
                            var g = dictionaries["C_CODE_ABBR_CUSTOMER_GRADE"].Where(c => c.Key == Convert.ToString(param.PropertyValue));
                            cell.Value = null != g ? (null == g.FirstOrDefault() ? null : g.FirstOrDefault().Value) : null;
                            break;
                        default:
                            cell.Value = param.PropertyValue;
                            break;
                    }
                }
            });

            return wb.ToResponseMessage(string.Format("学员管理_{0}.xlsx", DateTime.Now.ToString("yyyyMMddHHmmss")));
        }

        #endregion

        #region 充值与转学的判定

        [HttpGet]
        public AssertResult AssertAccountCharge(string customerID)
        {
            return ChargeApplyResult.Validate(customerID, DeluxeIdentity.CurrentUser);
        }

        [HttpGet]
        public AssertResult AssertStudentTransfer(string customerID)
        {
            return StudentTransferApplyResult.Validate(customerID, DeluxeIdentity.CurrentUser);
        }

        /// <summary>
        /// 根据学员ID获取当前转学信息
        /// </summary>
        /// <param name="id">学员ID</param>
        /// <returns></returns>
        [HttpGet]
        public StudentTransferApplyResult GetStudentTransferApplyByCustomerID(string id)
        {
            IUser user = DeluxeIdentity.CurrentUser;
            StudentTransferApplyResult result = StudentTransferApplyResult.LoadByCustomerID(id, user);
            if (result != null)
            {
                result.Apply.InitApplier(DeluxeIdentity.CurrentUser); //初始当前申请人信息
            }
            return result;
        }

        /// <summary>
        /// 根据申请单ID获取转学信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public StudentTransferApplyResult GetStudentTransferApplyByApplyID(string id)
        {
            IUser user = DeluxeIdentity.CurrentUser;
            StudentTransferApplyResult result = StudentTransferApplyResult.LoadByApplyID(id, user);
            return result;
        }

        /// <summary>
        /// 根据工作流参数获取转学信息
        /// </summary>
        /// <param name="wfParams"></param>
        /// <returns></returns>
        [HttpPost]
        public StudentTransferApplyResult GetStudentTransferApplyByWorkflow(dynamic wfParams)
        {
            WfClientSearchParameters p = new WfClientSearchParameters();
            p.ResourceID = wfParams.resourceID;
            p.ActivityID = wfParams.activityID;
            p.ProcessID = wfParams.processID;

            IUser user = DeluxeIdentity.CurrentUser;
            StudentTransferApplyResult result = StudentTransferApplyResult.LoadByApplyID(p.ResourceID, user);
            result.ClientProcess = WfClientProxy.GetClientProcess(p);
            return result;
        }

        /// <summary>
        /// 保存转学申请单。
        /// </summary>
        /// <param name="apply">申请信息</param>
        [HttpPost]
        public void SaveStudentTransferApply(StudentTransferApplyModel apply)
        {
            apply.Prepare(DeluxeIdentity.CurrentUser);
            string wfName = apply.TransferType == StudentTransferType.SameBranch ? WorkflowNames.CustomerTransfer_SameBranch : WorkflowNames.CustomerTransfer_CrossBranch;
            WorkflowHelper wfHelper = new WorkflowHelper(wfName, DeluxeIdentity.CurrentUser);
            if (wfHelper.CheckWorkflow(true))
            {
                MutexLocker mutex = new MutexLocker(
                    new MutexLockParameter()
                    {
                        CustomerID = apply.CustomerID,
                        AccountIDs = apply.AccountIDs,
                        Action = MutexAction.AccountCharge,
                        Description = string.Format("学员 {0} 转学中", apply.CustomerCode),
                        BillID = apply.ApplyID
                    });
                mutex.Lock(
                    delegate ()
                    {
                        new StudentEditTransferApplyExecutor(apply).Execute();
                    },
                    delegate ()
                    {
                        string branchID = OGUExtensions.GetOrganizationByID(apply.ToCampusID).GetParentOrganizationByType(DepartmentType.Branch).ID;
                        Dictionary<string, object> dic = new Dictionary<string, object>();
                        dic.Add("ToBranchID", branchID);
                        wfHelper.StartupWorkflow(
                            new WorkflowStartupParameter
                            {
                                ResourceID = apply.ApplyID,
                                TaskTitle = string.Format("{0}({1})学员申请从{2}转学到{3}", apply.CustomerName, apply.CustomerCode, apply.CampusName, apply.ToCampusName),
                                TaskUrl = "/PPTSWebApp/PPTS.Portal/#/ppts/student/transfer/approve",
                                Parameters = dic
                            });
                    });

                
            }
        }
        #endregion

        #region api/students/createparent

        [HttpGet]
        public EditableStudentParentModel CreateParent(string id)
        {
            EditableStudentParentModel result = new EditableStudentParentModel();
            result.Customer = GenericPotentialCustomerAdapter<StudentModel, List<StudentModel>>.Instance.Load(id);
            result.Parent = new ParentModel { ParentID = UuidHelper.NewUuidString(), IDType = IDTypeDefine.IDCard };
            result.CustomerParentRelation = new CustomerParentRelation() { CustomerID = result.Customer.CustomerID, ParentID = result.Parent.ParentID };
            result.Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(EditableStudentParentModel), typeof(Parent));
            return result;
        }

        [HttpPost]
        public void CreateParent(EditableStudentParentModel model)
        {
            EditableStudentParentExecutor executor = new EditableStudentParentExecutor(model);

            executor.Execute();
        }

        #endregion

        #region api/students/assignTeacher

        [PPTSJobFunctionAuthorize("PPTS:分配教师-本校区")]
        [HttpPost]
        public void AssignTeacher(AssignTeacherModel atm)
        {
            AssignTeacherExecutor executor = new AssignTeacherExecutor(atm);
            executor.Execute();
        }
        #endregion

        #region api/students/getTeachers
        [PPTSJobFunctionAuthorize("PPTS:分配教师-本校区")]
        [HttpGet]
        public TeacherJobViewCollection GetTeachers()
        {
            TeachersQueryCriteriaModel criteria = new TeachersQueryCriteriaModel();
            var result = TeacherJobViewAdapter.Instance.Load(builder => builder.AppendItem("CampusID", criteria.CampusID).AppendItem("JobOrgType", (int)criteria.JobOrgType));
            return result;
        }
        #endregion

        #region api/students/calloutTeacher
        [PPTSJobFunctionAuthorize("PPTS:分配教师-本校区")]
        [HttpPost]
        public void calloutTeacher(CustomerTeacherAssignApply cta)
        {
            CalloutTeacherExecutor executor = new CalloutTeacherExecutor(cta);
            executor.Execute();
        }
        #endregion

        #region api/students/changeTeacher
        [PPTSJobFunctionAuthorize("PPTS:分配教师-本校区")]
        [HttpPost]
        public void changeTeacher(ChangeTeacherModel ct)
        {
            ChangeTeacherExecutor executor = new ChangeTeacherExecutor(ct);
            executor.Execute();
        }
        #endregion

        #region api/students/getAllCustomerTeacherRelations
        [HttpPost]
        public CustomerTeacherRelationQueryResultModel getAllCustomerTeacherRelations(CustomerTeacherRelationQueryCriteriaModel criteria)
        {
            return new CustomerTeacherRelationQueryResultModel
            {
                CustomerTeachers = CustomerTeacherRelationAdapter.Instance.Load(builder => builder.AppendItem("CustomerID", criteria.CustomerID), DateTime.MinValue),
                Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(CustomerTeacherRelation))
            };
        }
        #endregion

        #region 学员解冻

        /// <summary>
        /// 上传附件接口实现
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public MaterialModelCollection UploadMaterial(HttpRequestMessage request)
        {
            return request.ProcessMaterialUpload();
        }

        [HttpPost]
        public HttpResponseMessage DownloadMaterial([ModelBinder(typeof(FormBinder))] MaterialModel material)
        {
            return material.ProcessMaterialDownload();
        }

        /// <summary>
        /// 学员申请解冻
        /// </summary>
        /// <param name="model"></param>
        [HttpPost]
        [PPTSJobFunctionAuthorize("PPTS:申请解冻")]
        public void SaveMaterial(StudentThawModel model)
        {
            model.ApplyName = DeluxeIdentity.CurrentUser.Name;
            Dictionary<string, object> p = new Dictionary<string, object>();
            p["StudentThaw"] = model;
            p["CustomerID"] = model.CustomerID;
            p["ThawReasonType"] = model.ReasonType;
            string wfName = WorkflowNames.CustomerRelease;
            WorkflowHelper wfHelper = new WorkflowHelper(wfName, DeluxeIdentity.CurrentUser);
            if (wfHelper.CheckWorkflow(true))
            {
                //业务数据保存
                MaterialModelHelper helper = MaterialModelHelper.GetInstance(CustomerMeetingAdapter.Instance.ConnectionName);

                if (model.Files != null && model.Files.Count > 0)
                    helper.Update(model.Files);

                #region 修改学员状态

                Customer customer = CustomerAdapter.Instance.Load(model.CustomerID);
                if (customer == null)
                {
                    throw new Exception("Customer为Null！");
                }
                customer.StudentStatus = Data.Customers.StudentStatusDefine.Releasing;
                customer.FillModifier();
                CustomerAdapter.Instance.Update(customer);

                #endregion
                wfHelper.StartupWorkflow(
                new WorkflowStartupParameter
                {
                    Parameters = p,
                    ResourceID = model.Files != null && model.Files.Count > 0 ? model.Files[0].ResourceID : Guid.NewGuid().ToString(),
                    TaskTitle = string.Format("学员解冻审批：{0}[{1}]", model.CustomerName, model.CustomerCode),
                    TaskUrl = "/PPTSWebApp/PPTS.Portal/#/ppts/student/student-thaw-view"
                });
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ProcessID"></param>
        /// <param name="ActivityID"></param>
        /// <param name="ResourceID"></param>
        /// <returns></returns>
        [HttpPost]
        public dynamic GetMaterial(StudentThawQueryCriteriaModel criteria)
        {
            StudentThawModel thawModel = new StudentThawModel();

            //获取当前的job
            PPTSJob job = DeluxeIdentity.CurrentUser.GetCurrentJob();

            WfClientProcess clientProcess = WfClientProxy.GetClientProcess(criteria);

            thawModel = clientProcess.ProcessParameters["StudentThaw"] as StudentThawModel;

            return new
            {
                thaw = thawModel,
                clientProcess = clientProcess
            };
        }
        #endregion
    }
}
