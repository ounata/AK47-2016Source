using System;
using System.Linq;
using System.Web.Http;
using PPTS.WebAPI.Customers.ViewModels.Accounts;
using PPTS.WebAPI.Customers.Executors;
using PPTS.Data.Customers.Executors;
using PPTS.Data.Customers.Adapters;
using PPTS.Data.Common.Entities;
using PPTS.Data.Common.Adapters;
using PPTS.Data.Common.Security;
using PPTS.Data.Common;
using PPTS.Data.Customers;
using MCS.Library.Principal;
using MCS.Web.MVC.Library.Filters;
using MCS.Library.Data;
using PPTS.Data.Customers.Entities;
using MCS.Library.Net.SNTP;
using MCS.Library.OGUPermission;
using PPTS.WebAPI.Customers.DataSources;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http.ModelBinding;
using MCS.Web.MVC.Library.ModelBinder;
using System.IO;
using MCS.Library.Office.OpenXml.Word;
using MCS.Library.SOA.DocServiceContract;
using System.Net;
using System.Net.Http.Headers;
using System.Web;
using MCS.Web.MVC.Library.ApiCore;
using MCS.Library.Office.OpenXml.Excel;
using System.Data;
using PPTS.Data.Customers.DataSources;
using MCS.Web.MVC.Library.Models;
using MCS.Web.MVC.Library.Models.Workflow;
using PPTS.Web.MVC.Library.Filters;
using PPTS.Data.Common.Authorization;

namespace PPTS.WebAPI.Customers.Controllers
{
    [ApiPassportAuthentication]
    public class AccountsController : ApiController
    {
        private void CheckAuth<T>(string id)
        {
            ScopeAuthorization<T>.GetInstance(ConnectionDefine.PPTSCustomerConnectionName).CheckEditAuth(id);
        }

        /// <summary>
        /// 根据OA编码获取教师
        /// </summary>
        /// <param name="oaCode">oa编码</param>
        /// <returns></returns>
        [HttpGet]
        public TeacherModel GetTeacher(string campusID, string oaCode)
        {
            return TeacherModel.Load(campusID, oaCode);
        }

        /// <summary>
        /// 根据学员编号获取学员信息
        /// </summary>
        /// <param name="customerCode">学员编号</param>
        /// <returns></returns>
        [HttpGet]
        public CustomerModel GetCustomer(string customerCode)
        {
            return CustomerModel.LoadBy(customerCode);
        }

        /// <summary>
        /// 根据学员ID获取账户信息
        /// </summary>
        /// <param name="customerID">学员ID</param>
        /// <returns></returns>
        [HttpGet]
        public AccountListResult GetAccountList(string customerID)
        {
            return AccountListResult.Load(customerID);
        }

        /// <summary>
        /// 查询账户日志列表
        /// </summary>
        /// <param name="criteria">查询条件</param>
        /// <returns></returns>

        /// <summary>
        /// 查询账户日志列表
        /// </summary>
        /// <param name="criteria">查询条件</param>
        /// <returns></returns>

        [PPTSJobFunctionAuthorize("PPTS:学员视图-账户信息/账户日志,学员视图-账户信息/账户日志-本部门,学员视图-账户信息/账户日志-本校区,学员视图-账户信息/账户日志-本分公司,学员视图-账户信息/账户日志-全国")]
        [HttpPost]
        public AccountRecordQueryResult QueryAccountRecordList(AccountRecordQueryCriteriaModel criteria)
        {
            return AccountRecordQueryResult.Query(criteria);
        }

        [PPTSJobFunctionAuthorize("PPTS:学员视图-账户信息/账户日志,学员视图-账户信息/账户日志-本部门,学员视图-账户信息/账户日志-本校区,学员视图-账户信息/账户日志-本分公司,学员视图-账户信息/账户日志-全国")]
        [HttpPost]
        public PagedQueryResult<AccountRecordQueryModel, AccountRecordQueryModelCollection> QueryPagedAccountRecordList(AccountRecordQueryCriteriaModel criteria)
        {
            return AccountRecordDataSource.Instance.QueryResult(criteria.PageParams, criteria, criteria.OrderBy);
        }

        #region 缴费相关查询

        /// <summary>
        /// 查询缴费申请单列表
        /// </summary>
        /// <param name="criteria">查询条件</param>
        /// <returns></returns>
        [PPTSJobFunctionAuthorize("PPTS:缴费单管理（缴费单详情）,缴费单管理（缴费单详情）-本部门,缴费单管理（缴费单详情）-本校区,缴费单管理（缴费单详情）-本分公司,缴费单管理（缴费单详情）-全国")]
        [HttpPost]
        public ChargeApplyQueryResult QueryChargeApplyList(ChargeApplyQueryCriteriaModel criteria)
        {
            if (criteria.QueryDepts != null && criteria.QueryDepts.Length != 0)
            {
                //获取部门ID
                criteria.QueryDeptID = DeluxeIdentity.CurrentUser.GetCurrentJob().GetParentOrganizationByType(DepartmentType.Department).ID;
            }
            return ChargeApplyQueryResult.Query(criteria);
        }
        [PPTSJobFunctionAuthorize("PPTS:缴费单管理（缴费单详情）,缴费单管理（缴费单详情）-本部门,缴费单管理（缴费单详情）-本校区,缴费单管理（缴费单详情）-本分公司,缴费单管理（缴费单详情）-全国")]
        [HttpPost]
        public PagedQueryResult<ChargeApplyQueryModel, ChargeApplyQueryModelCollection> QueryPagedChargeApplyList(ChargeApplyQueryCriteriaModel criteria)
        {
            if (criteria.QueryDepts != null && criteria.QueryDepts.Length != 0)
            {
                //获取部门ID
                criteria.QueryDeptID = DeluxeIdentity.CurrentUser.GetCurrentJob().GetParentOrganizationByType(DepartmentType.Department).ID;
            }
            return AccountChargeApplyDataSource.Instance.QueryResult(criteria.PageParams, criteria, criteria.OrderBy);
        }

        [PPTSJobFunctionAuthorize("PPTS:缴费单管理（缴费单详情）,缴费单管理（缴费单详情）-本部门,缴费单管理（缴费单详情）-本校区,缴费单管理（缴费单详情）-本分公司,缴费单管理（缴费单详情）-全国,缴费单管理导出")]
        [HttpPost]
        public HttpResponseMessage ExportAllCharges([ModelBinder(typeof(FormBinder))]ChargeApplyQueryCriteriaModel criteria)
        {
            criteria.PageParams.PageIndex = 0;
            criteria.PageParams.PageSize = 0;
            PagedQueryResult<ChargeApplyQueryModel, ChargeApplyQueryModelCollection> pageData = AccountChargeApplyDataSource.Instance.QueryResult(criteria.PageParams, criteria, criteria.OrderBy);
            WorkBook wb = WorkBook.CreateNew();
            WorkSheet sheet = wb.Sheets["sheet1"];
            TableDescription tableDesp = new TableDescription("缴费单");
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("学员姓名", typeof(string))) { PropertyName = "CustomerName" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("学员编号", typeof(string))) { PropertyName = "CustomerCode" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("家长姓名", typeof(string))) { PropertyName = "ParentName" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("缴费单号", typeof(string))) { PropertyName = "ApplyNo" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("充值类型", typeof(string))) { PropertyName = "ChargeType" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("充值状态", typeof(string))) { PropertyName = "PayStatus" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("审核状态", typeof(string))) { PropertyName = "AuditStatus" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("充值金额", typeof(string))) { PropertyName = "ChargeMoney" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("充值日期", typeof(string))) { PropertyName = "PayTime", Format = "yyyy-MM-dd" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("折扣基数", typeof(string))) { PropertyName = "ThisDiscountBase" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("折扣率", typeof(string))) { PropertyName = "ThisDiscountRate" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("申请人所在地", typeof(string))) { PropertyName = "CampusName" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("申请人", typeof(string))) { PropertyName = "SubmitterName" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("申请人岗位", typeof(string))) { PropertyName = "SubmitterJobName" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("当时年级", typeof(string))) { PropertyName = "CustomerGrade" });

            Dictionary<string, IEnumerable<BaseConstantEntity>> Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(ChargeApplyQueryModel));

            sheet.LoadFromCollection(pageData.PagedData, tableDesp, (cell, param) =>
            {
                switch (param.ColumnDescription.PropertyName)
                {
                    case "CustomerName":
                    case "CustomerCode":
                    case "ParentName":
                    case "ApplyNo":
                    case "ChargeType":
                    case "ChargeMoney":
                    case "ThisDiscountBase":
                    case "ThisDiscountRate":
                    case "CampusName":
                    case "SubmitterName":
                    case "SubmitterJobName":                    
                        cell.Value = param.ColumnDescription.FormatValue(param.PropertyValue);
                        break;
                    case "CustomerGrade":
                        cell.Value = Dictionaries["C_CODE_ABBR_CUSTOMER_GRADE"].Where(c => c.Key == param.PropertyValue.ToString()).FirstOrDefault() == null ? "" : Dictionaries["C_CODE_ABBR_CUSTOMER_GRADE"].Where(c => c.Key == param.PropertyValue.ToString()).FirstOrDefault().Value;
                        break;
                    case "PayStatus":
                        cell.Value = Dictionaries["Account_PayStatus"].Where(c => c.Key == param.PropertyValue.GetHashCode().ToString()).FirstOrDefault().Value;
                        break;
                    case "AuditStatus":
                        cell.Value = Dictionaries["Account_ChargeAuditStatus"].Count() == 0 ? "" : Dictionaries["Account_ChargeAuditStatus"].Where(c => c.Key == param.PropertyValue.GetHashCode().ToString()).FirstOrDefault().Value;
                        break;
                    case "PayTime":
                        cell.Value = param.ColumnDescription.FormatValue(param.PropertyValue);
                        break;
                    default:
                        cell.Value = param.PropertyValue;
                        break;
                }
            });

            return wb.ToResponseMessage("缴费单.xlsx");
        }

        /// <summary>
        /// 查询缴费支付单列表
        /// </summary>
        /// <param name="criteria">查询条件</param>
        /// <returns></returns>
        [PPTSJobFunctionAuthorize("PPTS:退费管理,退费管理-本部门,退费管理-本校区,退费管理-本分公司,退费管理-全国")]
        [HttpPost]
        public ChargePaymentQueryResult QueryChargePaymentList(ChargePaymentQueryCriteriaModel criteria)
        {
            return ChargePaymentQueryResult.Query(criteria);
        }

        [PPTSJobFunctionAuthorize("PPTS:退费管理,退费管理-本部门,退费管理-本校区,退费管理-本分公司,退费管理-全国")]
        [HttpPost]
        public PagedQueryResult<ChargePaymentQueryModel, ChargePaymentQueryModelCollection> QueryPagedChargePaymentList(ChargePaymentQueryCriteriaModel criteria)
        {
            return AccountChargePaymentDataSource.Instance.QueryResult(criteria.PageParams, criteria, criteria.OrderBy);
        }

        [PPTSJobFunctionAuthorize("PPTS:退费管理导出")]
        [HttpPost]
        public HttpResponseMessage ExportAllChargePayments([ModelBinder(typeof(FormBinder))]ChargePaymentQueryCriteriaModel criteria)
        {
            criteria.PageParams.PageIndex = 0;
            criteria.PageParams.PageSize = 0;
            PagedQueryResult<ChargePaymentQueryModel, ChargePaymentQueryModelCollection> pageData = AccountChargePaymentDataSource.Instance.QueryResult(criteria.PageParams, criteria, criteria.OrderBy);
            WorkBook wb = WorkBook.CreateNew();
            WorkSheet sheet = wb.Sheets["sheet1"];
            TableDescription tableDesp = new TableDescription("收款");
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("学员姓名", typeof(string))) { PropertyName = "CustomerName" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("学员编号", typeof(string))) { PropertyName = "CustomerCode" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("缴费单号", typeof(string))) { PropertyName = "ApplyNo" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("充值金额", typeof(string))) { PropertyName = "ChargeMoney" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("收款人", typeof(string))) { PropertyName = "PayeeName" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("收款金额", typeof(string))) { PropertyName = "PayMoney" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("收款类型", typeof(string))) { PropertyName = "PayType" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("收款时间", typeof(string))) { PropertyName = "PayTime", Format = "yyyy-MM-dd" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("校区", typeof(string))) { PropertyName = "CampusName" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("对账状态", typeof(string))) { PropertyName = "CheckStatus" });


            Dictionary<string, IEnumerable<BaseConstantEntity>> Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(ChargePaymentQueryModel));

            sheet.LoadFromCollection(pageData.PagedData, tableDesp, (cell, param) =>
            {
                switch (param.ColumnDescription.PropertyName)
                {
                    case "CustomerName":
                    case "CustomerCode":
                    case "ApplyNo":
                    case "ChargeMoney":
                    case "PayeeName":
                    case "PayMoney":
                    case "CampusName":
                        cell.Value = param.ColumnDescription.FormatValue(param.PropertyValue);
                        break;
                    case "PayType":
                        cell.Value = Dictionaries["Account_PayType"].Where(c => c.Key == param.PropertyValue.ToString()).FirstOrDefault() == null ? "" : Dictionaries["Account_PayType"].Where(c => c.Key == param.PropertyValue.ToString()).FirstOrDefault().Value;
                        break;
                    case "PayTime":
                        cell.Value = param.ColumnDescription.FormatValue(param.PropertyValue);
                        break;
                    case "CheckStatus":
                        cell.Value = Dictionaries["Common_CheckStatus"].Count() == 0 ? "" : Dictionaries["Common_CheckStatus"].Where(c => c.Key == param.PropertyValue.GetHashCode().ToString()).FirstOrDefault().Value;
                        break;
                    default:
                        cell.Value = param.PropertyValue;
                        break;
                }
            });

            return wb.ToResponseMessage("收款.xlsx");
        }

        /// <summary>
        /// 根据学员ID获取充值列表
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        [HttpGet]
        public ChargeApplyListResult GetChargeApplyList(string customerID)
        {
            return ChargeApplyListResult.Load(customerID);
        }

        /// <summary>
        /// 根据学员ID获取当前缴费信息
        /// </summary>
        /// <param name="id">学员ID</param>
        /// <returns></returns>
        [HttpGet]
        public ChargeApplyResult GetChargeApplyByCustomerID(string id)
        {
            IUser user = DeluxeIdentity.CurrentUser;
            ChargeApplyResult result = ChargeApplyResult.LoadByCustomerID(id, user);
            if (result != null)
            {
                result.Apply.InitApplier(DeluxeIdentity.CurrentUser); //初始当前申请人信息
            }
            return result;
        }

        /// <summary>
        /// 根据申请单ID获取充值信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ChargeApplyResult GetChargeApplyByApplyID(string id)
        {
            IUser user = DeluxeIdentity.CurrentUser;
            ChargeApplyResult result = ChargeApplyResult.LoadByApplyID(id, user);
            return result;
        }

        /// <summary>
        /// 根据申请单ID获取充值信息（针对业绩分配）
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ChargeApplyResult GetChargeApplyByApplyID4Allot(string id)
        {
            ChargeApplyResult result = ChargeApplyResult.LoadByApplyID4Allot(id);
            return result;
        }

        /// <summary>
        /// 根据申请单ID获取充值信息（针对缴费支付）
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ChargeApplyResult GetChargeApplyByApplyID4Payment(string id)
        {
            ChargeApplyResult result = ChargeApplyResult.LoadByApplyID4Payment(id);
            result.Apply.Payment.Payer = result.Customer.CustomerName;
            result.Apply.Payment.InitPayee(DeluxeIdentity.CurrentUser);
            return result;
        }

        /// <summary>
        /// 根据支付单ID获取支付信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ChargeApplyResult GetChargeApplyByPayID(string id)
        {
            AccountChargePayment payment = AccountChargePaymentAdapter.Instance.LoadByPayID(id);
            if (payment != null)
                return ChargeApplyResult.LoadByApplyID4Payment(payment.ApplyID);
            return null;
        }

        /// <summary>
        /// 获取刷卡记录信息。
        /// </summary>
        /// <param name="campusID">校区ID</param>
        /// <param name="payTicket">交易流水号</param>
        /// <param name="payType">收款类型</param>
        /// <returns></returns>
        public PosRecordResult GetPosRecord(string campusID, string payTicket, string payType)
        {
            return PosRecordResult.Load(campusID, payTicket, payType);
        }
        #endregion

        #region  登记发票信息

        ///登记发票
        [HttpPost]
        public dynamic GetAccountChargeInvoiceCollection(AccountChargeInvoiceQCM qcm)
        {
            ChargeApplyResult ca = ChargeApplyResult.LoadByApplyID4Payment(qcm.ApplyID);
            PagedQueryResult<AccountChargeInvoice, AccountChargeInvoiceCollection> queryResult = GenericCustomerDataSource<AccountChargeInvoice, AccountChargeInvoiceCollection>.Instance.Query(qcm.PageParams, qcm, qcm.OrderBy);
            return new {
                ChargeApply = ca,
                QueryResult = queryResult,
                Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(Data.Customers.Entities.AccountChargeInvoice))
        };
        }

        ///登记发票
        [HttpPost]
        public PagedQueryResult<AccountChargeInvoice, AccountChargeInvoiceCollection> GetPagedAccountChargeInvoiceCollection(AccountChargeInvoiceQCM qcm)
        {
            return GenericCustomerDataSource<AccountChargeInvoice, AccountChargeInvoiceCollection>.Instance.Query(qcm.PageParams, qcm, qcm.OrderBy);
        }

        [HttpPost]
        public AccountChargeInvoiceModel InitAccountChargeInvoice()
        {
            AccountChargeInvoiceModel result = new AccountChargeInvoiceModel();
            result.InitModel();
            return result;
        }

        [HttpGet]
        public AccountChargeInvoiceModel GetAccountChargeInvoice(string invoiceID)
        {
            AccountChargeInvoiceModel result = new AccountChargeInvoiceModel();
            result.LoadData(invoiceID);
            return result;
        }

        [HttpPost]
        [PPTSJobFunctionAuthorize("PPTS:按钮-登记发票,按钮-登记发票-本校区")]
        public dynamic SaveAccountChargeInvoice(AccountChargeInvoice  model)
        {
            AccountEditChargeInvoiceExecutor executor = new AccountEditChargeInvoiceExecutor(model);
            executor.Execute();
            return new { Msg = executor.Msg };
        }

        #endregion

        #region 退费相关查询

        /// <summary>
        /// 查询退费申请列表
        /// </summary>
        /// <param name="criteria">查询条件</param>
        /// <returns></returns>
        [PPTSJobFunctionAuthorize("PPTS:退费管理,退费管理-本部门,退费管理-本校区,退费管理-本分公司,退费管理-全国")]
        [HttpPost]
        public RefundApplyQueryResult QueryRefundApplyList(RefundApplyQueryCriteriaModel criteria)
        {
            return RefundApplyQueryResult.Query(criteria);
        }

        [PPTSJobFunctionAuthorize("PPTS:退费管理,退费管理-本部门,退费管理-本校区,退费管理-本分公司,退费管理-全国")]
        [HttpPost]
        public PagedQueryResult<RefundApplyQueryModel, RefundApplyQueryModelCollection> QueryPagedRefundApplyList(RefundApplyQueryCriteriaModel criteria)
        {
            return AccountRefundApplyDataSource.Instance.QueryResult(criteria.PageParams, criteria, criteria.OrderBy);
        }

        [PPTSJobFunctionAuthorize("PPTS:退费管理导出")]
        [HttpPost]
        public HttpResponseMessage ExportAllRefundApply([ModelBinder(typeof(FormBinder))]RefundApplyQueryCriteriaModel criteria)
        {
            criteria.PageParams.PageIndex = 0;
            criteria.PageParams.PageSize = 0;
            PagedQueryResult<RefundApplyQueryModel, RefundApplyQueryModelCollection> pageData = AccountRefundApplyDataSource.Instance.QueryResult(criteria.PageParams, criteria, criteria.OrderBy);
            WorkBook wb = WorkBook.CreateNew();
            WorkSheet sheet = wb.Sheets["sheet1"];
            TableDescription tableDesp = new TableDescription("退费");
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("学员姓名", typeof(string))) { PropertyName = "CustomerName" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("学员编号", typeof(string))) { PropertyName = "CustomerCode" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("账户总额", typeof(string))) { PropertyName = "ThisAccountMoney" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("应退金额", typeof(string))) { PropertyName = "OughtRefundMoney" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("实退金额", typeof(string))) { PropertyName = "RealRefundMoney" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("领款人", typeof(string))) { PropertyName = "Drawer" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("申请人", typeof(string))) { PropertyName = "ApplierName" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("申请人岗位", typeof(string))) { PropertyName = "ApplierJobName" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("申请校区", typeof(string))) { PropertyName = "CampusName" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("业务终审日期", typeof(string))) { PropertyName = "ApproveTime", Format = "yyyy-MM-dd" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("财务终审日期", typeof(string))) { PropertyName = "VerifyTime", Format = "yyyy-MM-dd" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("对账状态", typeof(string))) { PropertyName = "CheckStatus" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("退款状态", typeof(string))) { PropertyName = "VerifyStatus" });

            Dictionary<string, IEnumerable<BaseConstantEntity>> Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(RefundApplyQueryModel));

            sheet.LoadFromCollection(pageData.PagedData, tableDesp, (cell, param) =>
            {
                switch (param.ColumnDescription.PropertyName)
                {
                    case "CustomerName":
                    case "CustomerCode":
                    case "ThisAccountMoney":
                    case "OughtRefundMoney":
                    case "RealRefundMoney":
                    case "Drawer":
                    case "ApplierName":
                    case "ApplierJobName":
                    case "CampusName":
                        cell.Value = param.ColumnDescription.FormatValue(param.PropertyValue);
                        break;
                    case "VerifyStatus":
                        cell.Value = Dictionaries["Account_RefundVerifyStatus"].Where(c => c.Key == param.PropertyValue.GetHashCode().ToString()).FirstOrDefault() == null ? "" : Dictionaries["Account_RefundVerifyStatus"].Where(c => c.Key == param.PropertyValue.GetHashCode().ToString()).FirstOrDefault().Value;
                        break;
                    case "ApproveTime":
                    case "VerifyTime":
                        cell.Value = param.ColumnDescription.FormatValue(param.PropertyValue);
                        break;
                    case "CheckStatus":
                        cell.Value = Dictionaries["Common_CheckStatus"].Count() == 0 ? "" : Dictionaries["Common_CheckStatus"].Where(c => c.Key == param.PropertyValue.GetHashCode().ToString()).FirstOrDefault().Value;
                        break;
                    default:
                        cell.Value = param.PropertyValue;
                        break;
                }
            });

            return wb.ToResponseMessage("退费.xlsx");
        }

        /// <summary>
        /// 根据学员ID获取当前退费信息
        /// </summary>
        /// <param name="id">学员ID</param>
        /// <returns></returns>
        [HttpGet]
        public RefundApplyResult GetRefundApplyByCustomerID(string id)
        {
            IUser user = DeluxeIdentity.CurrentUser;
            RefundApplyResult result = RefundApplyResult.LoadByCustomerID(id, user);
            if (result != null)
            {
                result.Apply.InitApplier(DeluxeIdentity.CurrentUser); //初始当前申请人信息
            }
            return result;
        }

        /// <summary>
        /// 根据申请单ID获取退费信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public RefundApplyResult GetRefundApplyByApplyID(string id)
        {
            IUser user = DeluxeIdentity.CurrentUser;
            RefundApplyResult result = RefundApplyResult.LoadByApplyID(id, user);
            return result;
        }

        /// <summary>
        /// 根据工作流参数获取退费信息
        /// </summary>
        /// <param name="wfParams"></param>
        /// <returns></returns>
        [HttpPost]
        public RefundApplyResult GetRefundApplyByWorkflow(dynamic wfParams)
        {
            WfClientSearchParameters p = new WfClientSearchParameters();
            p.ResourceID = wfParams.resourceID;
            p.ActivityID = wfParams.activityID;
            p.ProcessID = wfParams.processID;

            IUser user = DeluxeIdentity.CurrentUser;
            RefundApplyResult result = RefundApplyResult.LoadByApplyID(p.ResourceID, user);
            result.ClientProcess = WfClientProxy.GetClientProcess(p);
            return result;
        }

        /// <summary>
        /// 获取退费折扣返还数据
        /// </summary>
        /// <param name="accountID">账户ID</param>
        /// <param name="discountID">折扣ID</param>
        /// <param name="discountBase">折扣基数</param>
        /// <param name="reallowanceStartTime">折扣返还时间计算时间点</param>
        /// <returns></returns>
        [HttpGet]
        public RefundReallowanceResult GetRefundReallowance(string accountID, string discountID, decimal discountBase, DateTime reallowanceStartTime)
        {
            return RefundReallowanceResult.GetReallowance(accountID, discountID, discountBase, reallowanceStartTime);
        }

        #endregion

        #region 转让相关查询
        /// <summary>
        /// 根据学员ID获取转让列表
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        [PPTSJobFunctionAuthorize("PPTS:转让")]
        [HttpGet]
        public TransferApplyListResult GetTransferApplyList(string customerID)
        {
            return TransferApplyListResult.Load(customerID);
        }

        /// <summary>
        /// 根据学员ID获取当前转让信息
        /// </summary>
        /// <param name="id">学员ID</param>
        /// <returns></returns>
        [HttpGet]
        public TransferApplyResult GetTransferApplyByCustomerID(string id)
        {
            IUser user = DeluxeIdentity.CurrentUser;
            TransferApplyResult result = TransferApplyResult.LoadByCustomerID(id, user);
            if (result != null)
            {
                result.Apply.InitApplier(DeluxeIdentity.CurrentUser); //初始当前申请人信息
            }
            return result;
        }

        /// <summary>
        /// 根据申请单ID获取转让信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public TransferApplyResult GetTransferApplyByApplyID(string id)
        {
            IUser user = DeluxeIdentity.CurrentUser;
            TransferApplyResult result = TransferApplyResult.LoadByApplyID(id, user);
            return result;
        }
        /// <summary>
        /// 根据工作流信息获取转让信息
        /// </summary>
        /// <param name="wfParams"></param>
        /// <returns></returns>
        [HttpPost]
        public TransferApplyResult GetTransferApplyByWorkflow(dynamic wfParams)
        {
            WfClientSearchParameters p = new WfClientSearchParameters();
            p.ResourceID = wfParams.resourceID;
            p.ActivityID = wfParams.activityID;
            p.ProcessID = wfParams.processID;


            IUser user = DeluxeIdentity.CurrentUser;
            TransferApplyResult result = TransferApplyResult.LoadByApplyID(p.ResourceID, user);
            result.ClientProcess = WfClientProxy.GetClientProcess(p);
            return result;
        }
        #endregion

        #region 缴费相关操作
        /// <summary>
        /// 保存（新增/修改）缴费申请单。
        /// </summary>
        /// <param name="apply">申请信息</param>
        [PPTSJobFunctionAuthorize("PPTS:按钮-充值/删除/编辑缴费单")]
        [HttpPost]
        public void SaveChargeApply(ChargeApplyModel apply)
        {
            apply.Prepare4SaveApply(DeluxeIdentity.CurrentUser.GetCurrentJob().JobType);
            MutexLocker mutex = new MutexLocker(
                new MutexLockParameter()
                {
                    CustomerID = apply.CustomerID,
                    AccountID = apply.AccountID,
                    Action = MutexAction.AccountCharge,
                    Description = string.Format("学员 {0} ，账号 {1} 充值中", apply.CustomerCode, apply.AccountCode),
                    BillID = apply.ApplyID
                });
            mutex.Lock(
                delegate ()
                {
                    new AccountEditChargeApplyExecutor(apply).Execute();
                });
        }

        /// <summary>
        /// 删除缴费申请单。
        /// </summary>
        /// <param name="applyID">申请ID</param>
        [PPTSJobFunctionAuthorize("PPTS:按钮-充值/删除/编辑缴费单")]
        [HttpPost]
        public void DeleteChargeApply(dynamic apply)
        {
            string applyID = apply.applyID;
            ChargeApplyModel model = ChargeApplyModel.LoadByApplyID(applyID);
            MutexReleaser mutex = new MutexReleaser(
                new MutexReleaseParameter()
                {
                    BillID = model.ApplyID
                });
            mutex.Release(
                delegate ()
                {
                    new AccountDeleteChargeApplyExecutor(applyID).Execute();
                });
        }

        /// <summary>
        /// 保存业绩归属。
        /// </summary>
        /// <param name="allot">归属信息</param>
        [PPTSJobFunctionAuthorize("PPTS:按钮-编辑业绩分配,按钮-编辑业绩分配-本校区,按钮-编辑业绩分配-本分公司")]
        [HttpPost]
        public void SaveChargeAllot(ChargeAllotModel allot)
        {
            new AccountEditChargeAllotExecutor(allot).Execute();
        }

        /// <summary>
        /// 保存支付记录
        /// </summary>
        /// <param name="payment"></param>
        [PPTSJobFunctionAuthorize("PPTS:按钮-添加付款-本校区")]
        [HttpPost]
        public void SaveChargePayment(ChargeApplyModel apply)
        {
            apply.Prepare4SavePayment(DeluxeIdentity.CurrentUser);
            MutexReleaser mutex = new MutexReleaser(
                new MutexReleaseParameter()
                {
                    BillID = apply.ApplyID
                });
            mutex.Release(
                delegate ()
                {
                    new AccountEditChargePaymentExecutor(apply).Execute();
                });
        }

        /// <summary>
        /// 对账收款单
        /// </summary>
        /// <param name="payIDs"></param>
        [PPTSJobFunctionAuthorize("PPTS:收款对账-本分公司")]
        [HttpPost]
        public void CheckChargePayment(string[] payIDs)
        {
            new AccountCheckChargePaymentExecutor(payIDs).Execute();
        }

        /// <summary>
        /// 保存支付收据打印状态
        /// </summary>
        /// <param name="payIDs"></param>
        [HttpPost]
        public ChargePaymentItemModel PrintChargePayment(dynamic print)
        {
            var payIDs = new string[] { print.payID };
            object result = new AccountPrintChargePaymentExecutor(payIDs).Execute();
            List<ChargePaymentItemModel> list = result as List<ChargePaymentItemModel>;
            if (list.Count != 0)
                return list[0];
            return null;
        }

        ///导出服务协议
        [HttpPost]
        public HttpResponseMessage ExportServiceAgreement([ModelBinder(typeof(FormBinder))] Paramter Para)
        {
            ChargeApplyResult retValue = ChargeApplyResult.LoadByApplyID4Payment(Para.Id);

            IOrganization org1 = OguMechanismFactory.GetMechanism().GetObjects<IOrganization>(SearchOUIDType.Guid, retValue.Customer.CampusID).SingleOrDefault().GetUpperDataScope();

            string companyName = string.Empty;
            string companyAddress = string.Empty;
            if (org1 != null)
            {
                companyName = org1.GetLegalOwner();
                companyAddress = org1.GetOfficeAddress();
            }

            ConstantEntity entityGender = ConstantAdapter.Instance.Get(ConstantCategoryConsts.GenderCategory, ((int)retValue.Customer.Gender).ToString());

            ConstantEntity entityParentRole = ConstantAdapter.Instance.Get(ConstantCategoryConsts.ParentRoleCategory, retValue.Customer.ParentRole);
            ConstantEntity entityGrade = ConstantAdapter.Instance.Get(ConstantCategoryConsts.GradeCategory, retValue.Customer.Grade);

            CustomerGuardian cg = new CustomerGuardian(retValue.Customer.CustomerID);

            DCTWordDataObject wdo = new DCTWordDataObject();
            ///甲方 单位名称
            wdo.PropertyCollection.Add(new DCTSimpleProperty() { TagID = "tgFPName", Value = companyName, IsReadOnly = true });
            ///甲方 经营地址
            wdo.PropertyCollection.Add(new DCTSimpleProperty() { TagID = "tgFPAddress", Value = companyAddress, IsReadOnly = true });

            ///乙方 学员编号
            wdo.PropertyCollection.Add(new DCTSimpleProperty() { TagID = "tgSFCustomerNumber", Value = retValue.Customer.CustomerCode, IsReadOnly = true });
            ///乙方 学员姓名
            wdo.PropertyCollection.Add(new DCTSimpleProperty() { TagID = "tgSFCustomerName", Value = retValue.Customer.CustomerName, IsReadOnly = true });
            ///乙方 性别
            wdo.PropertyCollection.Add(new DCTSimpleProperty() { TagID = "tgSPSex", Value = (entityGender != null ? entityGender.Value : ""), IsReadOnly = true });
            ///乙方 出生日期
            wdo.PropertyCollection.Add(new DCTSimpleProperty() { TagID = "tgSPBirthday", Value = retValue.Customer.Birthday == DateTime.MinValue ? "" : retValue.Customer.Birthday.ToString("yyyy-MM-dd"), IsReadOnly = true });
            ///乙方 学校
            wdo.PropertyCollection.Add(new DCTSimpleProperty() { TagID = "tgSPSchool", Value = retValue.Customer.SchoolName == null ? "" : retValue.Customer.SchoolName, IsReadOnly = true });
            ///乙方 年级
            wdo.PropertyCollection.Add(new DCTSimpleProperty() { TagID = "tgSPGrade", Value = entityGrade == null ? "" : entityGrade.Value, IsReadOnly = true });

            ///乙方 监护人姓名
            wdo.PropertyCollection.Add(new DCTSimpleProperty() { TagID = "tgGuardianName", Value = retValue.Customer.ParentName, IsReadOnly = true });
            ///乙方 监护人与乙方关系
            wdo.PropertyCollection.Add(new DCTSimpleProperty() { TagID = "tgGuardianSPRela", Value = entityParentRole == null ? "" : entityParentRole.Value, IsReadOnly = true });

            if (cg != null)
            {
                ///乙方 监护人宅电
                wdo.PropertyCollection.Add(new DCTSimpleProperty() { TagID = "tgSPPhone", Value = cg.HomePhone, IsReadOnly = true });
                ///乙方 监护人手机
                wdo.PropertyCollection.Add(new DCTSimpleProperty() { TagID = "tgGuardianMobile", Value = cg.MobilePhone, IsReadOnly = true });
                ///乙方 监护人家庭地址
                wdo.PropertyCollection.Add(new DCTSimpleProperty() { TagID = "tgSPHomeAddress", Value = cg.AddressDetail, IsReadOnly = true });
                ///乙方 监护人证件类型
                wdo.PropertyCollection.Add(new DCTSimpleProperty() { TagID = "tgGuardianCertificateType", Value = cg.IDType, IsReadOnly = true });
                ///乙方 监护人证件号码
                wdo.PropertyCollection.Add(new DCTSimpleProperty() { TagID = "tgGuardianCertificateNumber", Value = cg.IDNumber, IsReadOnly = true });
                ///乙方 监护人EMAIL
                wdo.PropertyCollection.Add(new DCTSimpleProperty() { TagID = "tgSPEMail", Value = cg.Email, IsReadOnly = true });
            }

            string path = string.Format("{0}\\TemplateDocument", Path.Combine(AppDomain.CurrentDomain.BaseDirectory));

            byte[] templateBinary = File.ReadAllBytes(Path.Combine(path, "学大教育服务协议V2.0.docx"));

            byte[] byteContent = WordEntry.GenerateDocument(templateBinary, wdo);

            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);

            MemoryStream stream = new MemoryStream(byteContent);

            result.Content = new StreamContent(stream);

            result.Content.Headers.ContentType =
                new MediaTypeHeaderValue("application/vnd.ms-word");

            string fileName = "学大教育服务协议V2.0.docx";

            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("Attachment")
            {
                FileName = HttpContext.Current.EncodeFileNameByBrowser(fileName)
            };

            return result;
        }

        public class Paramter
        {
            public string Id { get; set; }
        }
        #endregion

        #region 退费相关操作

        /// <summary>
        /// 上传附件接口实现
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [PPTSJobFunctionAuthorize("PPTS:退费")]
        [HttpPost]
        public MaterialModelCollection UploadMaterial(HttpRequestMessage request)
        {
            return request.ProcessMaterialUpload();
        }

        /// <summary>
        /// 保存退费申请单
        /// </summary>
        /// <param name="apply"></param>
        [PPTSJobFunctionAuthorize("PPTS:退费")]
        [HttpPost]
        public void SaveRefundApply(RefundApplyModel apply)
        {
            apply.Prepare4SaveApply();
            string wfName = apply.IsExtraRefund ? WorkflowNames.Refund_Outer : WorkflowNames.Refund_Inner;
            WorkflowHelper wfHelper = new WorkflowHelper(wfName, DeluxeIdentity.CurrentUser);
            if (wfHelper.CheckWorkflow(true))
            {
                MutexLocker mutex = new MutexLocker(
                    new MutexLockParameter()
                    {
                        CustomerID = apply.CustomerID,
                        AccountID = apply.AccountID,
                        Action = MutexAction.AccountRefund,
                        Description = string.Format("学员 {0} ，账号 {1} 退费中", apply.CustomerCode, apply.AccountCode),
                        BillID = apply.ApplyID
                    });
                mutex.Lock(
                    delegate ()
                    {
                        new AccountEditRefundApplyExecutor(apply).Execute();
                    },
                    delegate ()
                    {
                        wfHelper.StartupWorkflow(
                            new WorkflowStartupParameter
                            {
                                ResourceID = apply.ApplyID,
                                TaskTitle = string.Format("{0}({1})的账户 {2} 申请退费 ￥{3}", apply.CustomerName, apply.CustomerCode, apply.AccountCode, apply.RealRefundMoney.ToString("0.00")),
                                TaskUrl = "/PPTSWebApp/PPTS.Portal/#/ppts/account/refund/approve"
                            });
                    });
            }
        }

        /// <summary>
        /// 确认退款单
        /// </summary>
        /// <param name="verifying"></param>
        [PPTSJobFunctionAuthorize("PPTS:待分出纳确认-本分公司,待分财务确认-本分公司,待分区域财务确认-本分公司")]
        [HttpPost]
        public RefundApplyModel VerifyRefundApply(dynamic apply)
        {
            string action = apply.action;
            string applyID = apply.applyID;
            var actionEnum = (RefundVerifyAction)Enum.Parse(typeof(RefundVerifyAction), action, true);

            RefundApplyModel result = null;
            RefundVerifyingModel model = new RefundVerifyingModel();
            model.PrepareVerify(applyID, DeluxeIdentity.CurrentUser, actionEnum);
            if (actionEnum != RefundVerifyAction.RegionVerify)
            {
                object obj = new AccountVerifyRefundApplyExecutor(model).Execute();
                result = obj as RefundApplyModel;
                return result;
            }
            else
            {
                MutexReleaser mutex = new MutexReleaser(
                    new MutexReleaseParameter()
                    {
                        BillID = apply.ApplyID
                    });
                mutex.Release(
                    delegate ()
                    {
                        object obj = new AccountVerifyRefundApplyExecutor(model).Execute();
                        result = obj as RefundApplyModel;
                    });
                return result;
            }
        }

        /// <summary>
        /// 对账退费单
        /// </summary>
        /// <param name="applyIDs"></param>
        [PPTSJobFunctionAuthorize("PPTS:退款对账-本分公司")]
        [HttpPost]
        public void CheckRefundApply(string[] applyIDs)
        {
            new AccountCheckRefundApplyExecutor(applyIDs).Execute();
        }

        #endregion

        #region 转让相关操作
        /// <summary>
        /// 保存转让申请单。
        /// </summary>
        /// <param name="apply">申请信息</param>
        [HttpPost]
        [PPTSJobFunctionAuthorize("PPTS:转让")]
        public void SaveTransferApply(TransferApplyModel apply)
        {
            IOrganization org1 = OguMechanismFactory.GetMechanism().GetObjects<IOrganization>(SearchOUIDType.Guid, apply.CampusID).SingleOrDefault();
            IOrganization org2 = OguMechanismFactory.GetMechanism().GetObjects<IOrganization>(SearchOUIDType.Guid, apply.CampusID).SingleOrDefault();
            if (org1 == null || org1.Parent == null || org2 == null || org2.Parent == null)
                throw new Exception("没有找到校区和相应的分公司信息");
            if (org1.Parent.ID != org2.Parent.ID)
                throw new Exception("转出学员与转入学员不是相同分公司，不可转让");

            string wfName = WorkflowNames.AccountTransfer;
            WorkflowHelper wfHelper = new WorkflowHelper(wfName, DeluxeIdentity.CurrentUser);
            if (wfHelper.CheckWorkflow(true))
            {
                apply.Prepare(DeluxeIdentity.CurrentUser);
                MutexLocker mutex = new MutexLocker(
                    new MutexLockParameter()
                    {
                        CustomerID = apply.CustomerID,
                        AccountID = apply.AccountID,
                        Action = MutexAction.AccountTransferOut,
                        Description = string.Format("学员 {0} ，账号 {1} 转出中", apply.CustomerCode, apply.AccountCode),
                        BillID = apply.ApplyID
                    },
                    new MutexLockParameter()
                    {
                        CustomerID = apply.BizCustomerID,
                        AccountID = apply.BizAccountID,
                        Action = MutexAction.AccountTransferIn,
                        Description = string.Format("学员 {0} ，账号 {1} 转入中", apply.BizCustomerCode, apply.BizAccountCode),
                        BillID = apply.ApplyID
                    });
                mutex.Lock(
                    delegate ()
                    {
                        new AccountEditTransferApplyExecutor(apply).Execute();
                    },
                    delegate ()
                    {
                        wfHelper.StartupWorkflow(
                            new WorkflowStartupParameter
                            {
                                ResourceID = apply.ApplyID,
                                TaskTitle = string.Format("{0}({1})的账户 {2} 申请转让 ￥{3}", apply.CustomerName, apply.CustomerCode, apply.AccountCode, apply.TransferMoney.ToString("0.00")),
                                TaskUrl = "/PPTSWebApp/PPTS.Portal/#/ppts/account/transfer/approve"
                            });
                    });
            }
        }
        
        #endregion

        #region 返还相关操作

        /// <summary>
        /// 根据学员ID获取当前服务费返还信息
        /// </summary>
        /// <param name="id">学员ID</param>
        /// <returns></returns>
        [HttpGet]
        public ReturnApplyResult GetReturnApplyByCustomerID(string id)
        {
            IUser user = DeluxeIdentity.CurrentUser;
            ReturnApplyResult result = ReturnApplyResult.LoadByCustomerID(id, user);
            if (result != null)
            {
                result.Apply.InitApplier(DeluxeIdentity.CurrentUser); //初始当前申请人信息
            }
            return result;
        }

        /// <summary>
        /// 保存服务费返还。
        /// </summary>
        /// <param name="apply">申请信息</param>
        [HttpPost]
        [PPTSJobFunctionAuthorize("PPTS:返还综合服务费")]
        public void SaveReturnApply(ReturnApplyModel apply)
        {
            apply.Prepare(DeluxeIdentity.CurrentUser);
            new AccountEditReturnApplyExecutor(apply).Execute();
        }
        #endregion

    }
}