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

namespace PPTS.WebAPI.Customers.Controllers
{
    [ApiPassportAuthentication]
    public class AccountsController : ApiController
    {
        /// <summary>
        /// 根据OA编码获取教师
        /// </summary>
        /// <param name="oaCode">oa编码</param>
        /// <returns></returns>
        [HttpGet]
        public TeacherModel GetTeacher(string oaCode)
        {
            Teacher teacher = TeacherAdapter.Instance.LoadByTeacherOACode(oaCode);
            if (teacher != null)
                return AutoMapper.Mapper.DynamicMap<TeacherModel>(teacher);
            return null;
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

        #region 缴费相关查询

        /// <summary>
        /// 查询缴费申请单列表
        /// </summary>
        /// <param name="criteria">查询条件</param>
        /// <returns></returns>
        [HttpPost]
        public ChargeApplyQueryResult QueryChargeApplyList(ChargeApplyQueryCriteriaModel criteria)
        {
            ChargeApplyQueryResult result = new ChargeApplyQueryResult();
            result.QueryResult = AccountChargeApplyDataSource.Instance.QueryResult(criteria.PageParams, criteria, criteria.OrderBy); ;
            result.Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(ChargeApplyQueryModel));
            return result;
        }
        [HttpPost]
        public PagedQueryResult<ChargeApplyQueryModel, ChargeApplyQueryModelCollection> QueryPagedChargeApplyList(ChargeApplyQueryCriteriaModel criteria)
        {
            return AccountChargeApplyDataSource.Instance.QueryResult(criteria.PageParams, criteria, criteria.OrderBy);
        }

        /// <summary>
        /// 查询缴费支付单列表
        /// </summary>
        /// <param name="criteria">查询条件</param>
        /// <returns></returns>
        [HttpPost]
        public ChargePaymentQueryResult QueryChargePaymentList(ChargePaymentQueryCriteriaModel criteria)
        {
            ChargePaymentQueryResult result = new ChargePaymentQueryResult();
            result.QueryResult = AccountChargePaymentDataSource.Instance.QueryResult(criteria.PageParams, criteria, criteria.OrderBy); ;
            result.Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(ChargePaymentQueryModel));
            return result;
        }
        [HttpPost]
        public PagedQueryResult<ChargePaymentQueryModel, ChargePaymentQueryModelCollection> QueryPagedChargePaymentList(ChargePaymentQueryCriteriaModel criteria)
        {
            return AccountChargePaymentDataSource.Instance.QueryResult(criteria.PageParams, criteria, criteria.OrderBy);
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
        #endregion

        #region 退费相关查询

        /// <summary>
        /// 查询退费申请列表
        /// </summary>
        /// <param name="criteria">查询条件</param>
        /// <returns></returns>
        [HttpPost]
        public RefundApplyQueryResult QueryRefundApplyList(RefundApplyQueryCriteriaModel criteria)
        {
            RefundApplyQueryResult result = new RefundApplyQueryResult();
            result.QueryResult = AccountRefundApplyDataSource.Instance.QueryResult(criteria.PageParams, criteria, criteria.OrderBy); ;
            result.Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(RefundApplyQueryModel));
            return result;
        }
        [HttpPost]
        public PagedQueryResult<RefundApplyQueryModel, RefundApplyQueryModelCollection> QueryPagedChargeApplyList(RefundApplyQueryCriteriaModel criteria)
        {
            return AccountRefundApplyDataSource.Instance.QueryResult(criteria.PageParams, criteria, criteria.OrderBy);
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

        #endregion

        #region 转让相关查询
        /// <summary>
        /// 根据学员ID获取转让列表
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
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
        #endregion

        #region 返还相关查询
        [HttpGet]
        public ExpenseResult GetCustomerExpenses(string customerID)
        {
            return ExpenseResult.Load(customerID);
        }

        #endregion

        #region 缴费相关操作
        /// <summary>
        /// 保存（新增/修改）缴费申请单。
        /// </summary>
        /// <param name="apply">申请信息</param>
        [HttpPost]
        public void SaveChargeApply(ChargeApplyModel apply)
        {
            apply.Prepare4SaveApply(DeluxeIdentity.CurrentUser.GetCurrentJob().JobType);
            new AccountEditChargeApplyExecutor(apply).Execute();
        }

        /// <summary>
        /// 删除缴费申请单。
        /// </summary>
        /// <param name="applyID">申请ID</param>
        [HttpPost]
        public void DeleteChargeApply(dynamic apply)
        {
            string applyID = apply.applyID;
            new AccountDeleteChargeApplyExecutor(applyID).Execute();
        }

        /// <summary>
        /// 保存支付记录
        /// </summary>
        /// <param name="payment"></param>
        [HttpPost]
        public void SaveChargePayment(ChargeApplyModel apply)
        {
            apply.Prepare4SavePayment(DeluxeIdentity.CurrentUser.GetCurrentJob().JobType);
            new AccountEditChargePaymentExecutor(apply).Execute();
        }

        /// <summary>
        /// 对账收款单
        /// </summary>
        /// <param name="payIDs"></param>
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
        #endregion

        #region 退费相关操作
        /// <summary>
        /// 保存退费申请单
        /// </summary>
        /// <param name="apply"></param>
        [HttpPost]
        public void SaveRefundApply(RefundApplyModel apply)
        {
            apply.Prepare4SaveApply();
            new AccountEditRefundApplyExecutor(apply).Execute();
        }

        /// <summary>
        /// 审批退款单
        /// </summary>
        /// <param name="apply"></param>
        /// <returns></returns>
        [HttpPost]
        public RefundApplyModel ApproveRefundApply(dynamic apply)
        {
            string opinion = apply.opinion;
            string applyID = apply.applyID;

            IUser user = DeluxeIdentity.CurrentUser;
            AccountApproveRefundModel model = new AccountApproveRefundModel();
            model.BillID = applyID;
            model.ApproverID = user.ID;
            model.ApproverName = user.Name;
            model.ApproverJobID = user.GetCurrentJob().ID;
            model.ApproverJobName = user.GetCurrentJob().Name;
            model.ApproveTime = SNTPClient.AdjustedTime;
            model.IsFinalApprove = true;
            model.IsRefused = opinion == "refuse";
            model.PrepareApprove();
            if (model.Apply.ApplyStatus == ApplyStatusDefine.Approving)
            {
                object result = new AccountApproveRefundApplyExecutor(model).Execute();
                return AutoMapper.Mapper.DynamicMap<RefundApplyModel>(result);
            }
            return null;
        }

        /// <summary>
        /// 确认退款单
        /// </summary>
        /// <param name="verifying"></param>
        [HttpPost]
        public RefundApplyModel VerifyRefundApply(dynamic apply)
        {
            string action = apply.action;
            string applyID = apply.applyID;
            var actionEnum = (RefundVerifyAction)Enum.Parse(typeof(RefundVerifyAction), action, true);

            RefundVerifyingModel model = new RefundVerifyingModel();
            model.PrepareVerify(applyID, DeluxeIdentity.CurrentUser, actionEnum);
            object result = new AccountVerifyRefundApplyExecutor(model).Execute();
            return result as RefundApplyModel;
        }

        /// <summary>
        /// 对账退费单
        /// </summary>
        /// <param name="applyIDs"></param>
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
        public void SaveTransferApply(TransferApplyModel apply)
        {
            IOrganization org1 = OguMechanismFactory.GetMechanism().GetObjects<IOrganization>(SearchOUIDType.Guid, apply.CampusID).SingleOrDefault().GetUpperDataScope();
            IOrganization org2 = OguMechanismFactory.GetMechanism().GetObjects<IOrganization>(SearchOUIDType.Guid, apply.CampusID).SingleOrDefault().GetUpperDataScope();
            if (org1.ID != org2.ID)
                throw new Exception("转出学员与转入学员不是相同分公司，不可转让");
            apply.Prepare(DeluxeIdentity.CurrentUser);
            new AccountEditTransferApplyExecutor(apply).Execute();
        }

        /// <summary>
        /// 审批转让申请单
        /// </summary>
        /// <param name="apply"></param>
        /// <returns></returns>
        [HttpPost]
        public TransferApplyModel ApproveTransferApply(dynamic apply)
        {
            string opinion = apply.opinion;
            string applyID = apply.applyID;

            IUser user = DeluxeIdentity.CurrentUser;
            AccountApproveTransferModel model = new AccountApproveTransferModel();
            model.BillID = applyID;
            model.ApproverID = user.ID;
            model.ApproverName = user.Name;
            model.ApproverJobID = user.GetCurrentJob().ID;
            model.ApproverJobName = user.GetCurrentJob().Name;
            model.ApproveTime = SNTPClient.AdjustedTime;
            model.IsFinalApprove = true;
            model.IsRefused = opinion == "refuse";
            model.PrepareApprove();
            if (model.Apply.ApplyStatus == ApplyStatusDefine.Approving)
            {
                object result = new AccountApproveTransferApplyExecutor(model).Execute();
                return AutoMapper.Mapper.DynamicMap<TransferApplyModel>(result);
            }
            return null;
        }
        #endregion
    }
}