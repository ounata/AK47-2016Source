using MCS.Library.Core;
using MCS.Library.Net.SNTP;
using MCS.Library.OGUPermission;
using PPTS.Data.Common.Security;
using PPTS.Data.Customers;
using PPTS.Data.Customers.Adapters;
using PPTS.Data.Customers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPTS.WebAPI.Customers.ViewModels.Accounts
{
    /// <summary>
    /// 返还申请单模型
    /// </summary>
    public class ReturnApplyModel : AccountReturnApply
    {
        public Account PreparedAccount
        {
            set;
            get;
        }

        /// <summary>
        /// 初始化提交人信息
        /// </summary>
        public void InitApplier(IUser user)
        {
            this.ApplierID = user.ID;
            this.ApplierName = user.Name;
            this.ApplierJobID = user.GetCurrentJob().ID;
            this.ApplierJobName = user.GetCurrentJob().Name;
            this.ApplierJobType = user.GetCurrentJob().JobType;
            this.ApplyTime = SNTPClient.AdjustedTime;
            this.ApplyStatus = ApplyStatusDefine.New;
        }

        /// <summary>
        /// 根据申请人初始化提交人信息
        /// </summary>
        public void InitSubmitter(IUser user)
        {
            this.SubmitterID = user.ID;
            this.SubmitterName = user.Name;
            this.SubmitterJobID = user.GetCurrentJob().ID;
            this.SubmitterJobName = user.GetCurrentJob().Name;
            this.SubmitterJobType = user.GetCurrentJob().JobType;
            this.SubmitTime = SNTPClient.AdjustedTime;
            this.ApplyStatus = ApplyStatusDefine.Approving;
        }

        /// <summary>
        /// 根据申请人初始化提交人信息
        /// </summary>
        public void InitApprover(IUser user, ApplyStatusDefine status)
        {
            this.ApproverID = user.ID;
            this.ApproverName = user.Name;
            this.ApproverJobID = user.GetCurrentJob().ID;
            this.ApproverJobName = user.GetCurrentJob().Name;
            this.ApproveTime = SNTPClient.AdjustedTime;
            this.ApplyStatus = status;
        }

        public void Prepare(IUser user)
        {
            CustomerExpenseRelation expense = CustomerExpenseRelationAdapter.Instance.Load(this.CustomerID, this.ExpenseID);
            if (expense == null)
                throw new Exception("当前综合服务费记录不存在");

            this.ExpenseType = expense.ExpenseType;
            this.ExpenseMoney = expense.ExpenseMoney;

            AccountModel account = AccountModel.LoadByAccountID(expense.AccountID);
            if (account == null)
                throw new Exception("当前返还的服务费对应的账户不存在");

            this.ApplyNo = Helper.GetApplyNo("FH");
            this.AccountID = account.AccountID;
            this.AccountCode = account.AccountCode;

            this.ThatDiscountID = account.DiscountID;
            this.ThatDiscountCode = account.DiscountCode;
            this.ThatDiscountBase = account.DiscountBase;
            this.ThatDiscountRate = account.DiscountRate;
            this.ThatAccountMoney = account.AccountMoney;

            account.AccountMoney += this.ExpenseMoney;

            this.ThisAccountValue = account.AccountValue;
            this.ThisDiscountID = account.DiscountID;
            this.ThisDiscountCode = account.DiscountCode;
            this.ThisDiscountBase = account.DiscountBase;
            this.ThisDiscountRate = account.DiscountRate;
            this.ThisAccountMoney = account.AccountMoney;
            this.ThisAccountValue = account.AccountValue;

            this.PreparedAccount = account;
        }

        public static ReturnApplyModel Load(CustomerModel customer)
        {
            ReturnApplyModel model = new ReturnApplyModel();
            model.ApplyID = UuidHelper.NewUuidString();
            model.CampusID = customer.CampusID;
            model.CampusName = customer.CampusName;
            model.CustomerID = customer.CustomerID;
            model.CustomerCode = customer.CustomerCode;
            model.CustomerName = customer.CustomerName;
            return model;
        }
    }
}