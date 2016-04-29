using System;
using System.Linq;
using System.Web.Http;
using PPTS.WebAPI.Customers.ViewModels.Accounts;
using PPTS.WebAPI.Customers.Executors;
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
        /// 根据学员ID获取账户信息
        /// </summary>
        /// <param name="customerID">学员ID</param>
        /// <returns></returns>
        [HttpGet]
        public AccountQueryResult GetAccountQueryResult(string customerID)
        {
            return AccountQueryResult.Load(customerID);
        }

        /// <summary>
        /// 根据学员ID获取当前缴费信息
        /// </summary>
        /// <param name="id">学员ID</param>
        /// <returns></returns>
        [HttpGet]
        public ChargeDisplayResult GetChargeDisplayResultByCustomerID(string id)
        {
            JobTypeDefine jobType = JobTypeDefine.Consultant;
            ChargeDisplayResult result = ChargeDisplayResult.LoadByCustomerID(id, jobType);
            if (result != null)
            {
                //获取当前申请人信息
                result.Apply.ApplierID = DeluxeIdentity.CurrentUser.ID;
                result.Apply.ApplierName = DeluxeIdentity.CurrentUser.DisplayName;
                result.Apply.ApplierJobID = DeluxeIdentity.CurrentUser.GetCurrentJob().ID;
                result.Apply.ApplierJobName = DeluxeIdentity.CurrentUser.GetCurrentJob().Name;
                result.Apply.ApplierJobType = jobType;
            }
            return result;
        }

        /// <summary>
        /// 根据申请单ID获取充值信息（所有信息）
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ChargeDisplayResult GetChargeDisplayResultByApplyID(string id)
        {
            ChargeDisplayResult result = ChargeDisplayResult.LoadByApplyID(id);
            return result;
        }

        /// <summary>
        /// 根据申请单ID获取充值信息（针对业绩分配）
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ChargeDisplayResult GetChargeDisplayResultByApplyID4Allot(string id)
        {
            ChargeDisplayResult result = ChargeDisplayResult.LoadByApplyID4Allot(id);
            return result;
        }
        /// <summary>
        /// 根据申请单ID获取充值信息（针对缴费支付）
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ChargeDisplayResult GetChargeDisplayResultByApplyID4Payment(string id)
        {
            ChargeDisplayResult result = ChargeDisplayResult.LoadByApplyID4Payment(id);
            result.Apply.Payment.Payer = result.Customer.CustomerName;
            result.Apply.Payment.PayeeID = DeluxeIdentity.CurrentUser.ID;
            result.Apply.Payment.PayeeName = DeluxeIdentity.CurrentUser.DisplayName;
            result.Apply.Payment.PayeeJobID = DeluxeIdentity.CurrentUser.GetCurrentJob().ID;
            result.Apply.Payment.PayeeJobName = DeluxeIdentity.CurrentUser.GetCurrentJob().Name;
            result.Apply.Payment.PayTime = SNTPClient.AdjustedTime;
            return result;
        }

        /// <summary>
        /// 根据支付单ID获取支付信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ChargeDisplayResult GetChargeDisplayResultByPayID(string id)
        {
            AccountChargePayment payment =  AccountChargePaymentAdapter.Instance.LoadByPayID(id);
            if (payment != null)
                return ChargeDisplayResult.LoadByApplyID4Payment(payment.ApplyID);
            return null;
        }

        /// <summary>
        /// 保存（新增/修改）缴费申请单。
        /// </summary>
        /// <param name="apply">申请信息</param>
        [HttpPost]
        public void SaveChargeApply(ChargeApplyModel apply)
        {
            new EditChargeApplyExecutor(apply).Execute();
        }

        /// <summary>
        /// 删除缴费申请单。
        /// </summary>
        /// <param name="applyID">申请ID</param>
        [HttpPost]
        public void DeleteChargeApply(string applyID)
        {
            new DeleteChargeApplyExecutor(applyID).Execute();
        }

        /// <summary>
        /// 保存支付记录
        /// </summary>
        /// <param name="payment"></param>
        [HttpPost]
        public void SaveChargePayment(ChargeApplyModel apply)
        {
            new EditChargePaymentExecutor(apply).Execute();
        }

        /// <summary>
        /// 保存支付收据打印状态
        /// </summary>
        /// <param name="print"></param>
        [HttpPost]
        public ChargePaymentItemModel SaveChargePaymentPrint(ChargePaymentPrintModel print)
        {
            AccountChargePayment payment = AccountChargePaymentAdapter.Instance.LoadByPayID(print.PayID);
            if (payment != null && payment.PayStatus == PayStatusDefine.Paid)
            {
                ChargePaymentItemModel model = AutoMapper.Mapper.DynamicMap<ChargePaymentItemModel>(payment);
                model.FillModifier();
                model.PrintStatus = PrintStatusDefine.Printed;
                new EditChargePaymentItemExecutor(model).Execute();
                return model;
            }
            return null;
        }
    }
}