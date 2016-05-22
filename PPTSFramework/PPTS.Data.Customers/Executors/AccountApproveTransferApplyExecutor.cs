using MCS.Library.Core;
using MCS.Library.Data;
using MCS.Library.Data.Executors;
using MCS.Library.SOA.DataObjects;
using PPTS.Data.Customers.Adapters;
using PPTS.Data.Customers.Executors;
using PPTS.Data.Customers.Entities;
using PPTS.Data.Customers;
using PPTS.Data.Common.Security;
using PPTS.Data.Common;
using MCS.Library.Principal;
using System.Collections.Generic;
using System;
using MCS.Library.Net.SNTP;

namespace PPTS.WebAPI.Customers.Executors
{
    [DataExecutorDescription("转让审批")]
    public class AccountApproveTransferApplyExecutor : PPTSEditCustomerExecutorBase<AccountApproveTransferModel>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        public AccountApproveTransferApplyExecutor(AccountApproveTransferModel model)
            : base(model, null)
        {
            model.NullCheck("model");
        }

        protected override object DoOperation(DataExecutionContext<UserOperationLogCollection> context)
        {
            base.DoOperation(context);
            return this.Model.Apply;
        }
        
        private void Init(AccountApproveTransferModel model, AccountTransferApply apply, Account account, Account bizAccount)
        {
            apply.ModifierID = model.ApproverID;
            apply.ModifierName = model.ApproverName;
            apply.ModifyTime = SNTPClient.AdjustedTime;

            apply.ApproverID = model.ApproverID;
            apply.ApproverName = model.ApproverName;
            apply.ApproverJobID = model.ApproverJobID;
            apply.ApproverJobName = model.ApproverJobName;
            apply.ApproveTime = model.ApproveTime;
            
            if (model.IsRefused)
                apply.ApplyStatus = ApplyStatusDefine.Refused;
            else if (model.IsFinalApprove)
                apply.ApplyStatus = ApplyStatusDefine.Approved;
            
            account.AccountMoney = apply.ThisAccountMoney;
            account.DiscountID = apply.ThisDiscountID;
            account.DiscountCode = apply.ThisDiscountCode;
            account.DiscountBase = apply.ThisDiscountBase;
            account.DiscountRate = apply.ThisDiscountRate;
            account.ModifierID = apply.ModifierID;
            account.ModifierName = apply.ModifierName;
            account.ModifyTime = apply.ModifyTime;

            if (bizAccount == null)
            {
                bizAccount = new Account();
                bizAccount.CampusID = apply.CampusID;
                bizAccount.CampusName = apply.CampusName;
                bizAccount.AccountID = apply.BizAccountID;
                bizAccount.AccountCode = apply.BizAccountCode;
                bizAccount.AccountType = apply.BizAccountType;
                bizAccount.AccountStatus = AccountStatusDefine.Enabled;
                bizAccount.AccountMoney = apply.BizThisAccountMoney;
                bizAccount.DiscountID = apply.BizThisDiscountID;
                bizAccount.DiscountCode = apply.BizThisDiscountCode;
                bizAccount.DiscountBase = apply.BizThisDiscountBase;
                bizAccount.DiscountRate = apply.BizThisDiscountRate;
                bizAccount.CreatorID = apply.ModifierID;
                bizAccount.CreatorName = apply.ModifierName;
                bizAccount.CreateTime = apply.ModifyTime;

                this.Model.BizAccount = bizAccount;
            }
            bizAccount.ModifierID = apply.ModifierID;
            bizAccount.ModifierName = apply.ModifierName;
            bizAccount.ModifyTime = apply.ModifyTime;
        }

        protected override void PrepareData(DataExecutionContext<UserOperationLogCollection> context)
        {
            base.PrepareData(context);

            if (this.Model.Apply != null && this.Model.Apply.CanApprove)
            {
                this.Init(this.Model, this.Model.Apply, this.Model.Account, this.Model.BizAccount);
                AccountTransferApplyAdapter.Instance.UpdateInContext(this.Model.Apply);
                if (!this.Model.IsRefused)
                {
                    AccountAdapter.Instance.UpdateInContext(this.Model.BizAccount);
                    AccountAdapter.Instance.UpdateInContext(this.Model.Account);
                }
            }
        }

        protected override void ExecuteNonQuerySqlInContext(DbContext dbContext)
        {
            dbContext.ExecuteTimePointSqlInContext();
        }

        /// <summary>
        /// 准备日志信息
        /// </summary>
        /// <param name="context"></param>
        protected override void PrepareOperationLog(DataExecutionContext<UserOperationLogCollection> context)
        {
            base.PrepareOperationLog(context);
        }
    }
}