using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using PPTS.Data.Customers.Entities;
using PPTS.Data.Customers;
using MCS.Library.OGUPermission;
using MCS.Library.Net.SNTP;
using PPTS.Data.Common.Security;
using PPTS.Data.Customers.Adapters;
using MCS.Library.Core;

namespace PPTS.WebAPI.Customers.ViewModels.Accounts
{
    /// <summary>
    /// 退费支付单模型
    /// </summary>
    [Serializable]
    [DataContract]
    public class RefundVerifyingModel : AccountRefundVerifying
    {
        public RefundApplyModel Apply
        {
            set;
            get;
        }

        public Account Account
        {
            set;
            get;
        }

        public void PrepareVerify(string applyID, IUser user, RefundVerifyAction action)
        {
            this.ApplyID = applyID;
            this.FillCreator();
            this.VerifyID = UuidHelper.NewUuidString();
            this.VerifyTime = SNTPClient.AdjustedTime;
            this.VerifierID = user.ID;
            this.VerifierName = user.Name;
            this.VerifierJobID = user.GetCurrentJob().ID;
            this.VerifierJobName = user.GetCurrentJob().Name;
            this.VerifierOrgID = user.GetCurrentJob().Organization().ID;
            this.VerifierOrgName = user.GetCurrentJob().Organization().Name;
            this.VerifyAction = action;

            //只有业务审批完，没有最终确认收款的才可以确认,分区域确认后变更账户
            AccountRefundApply apply = AccountRefundApplyAdapter.Instance.LoadByApplyID(this.ApplyID);
            if (apply != null && apply.ApplyStatus == ApplyStatusDefine.Approved && apply.VerifyStatus != RefundVerifyStatus.Refunded)
            {
                this.Apply = apply.ProjectedAs<RefundApplyModel>();
                if (this.VerifyAction == RefundVerifyAction.RegionVerify)
                {
                    Account account = AccountAdapter.Instance.LoadByAccountID(apply.AccountID);
                    if (account != null)
                    {
                        account.FillModifier();
                        account.DiscountID = apply.ThisDiscountID;
                        account.DiscountCode = apply.ThisDiscountCode;
                        account.DiscountBase = apply.ThisDiscountBase;
                        account.DiscountRate = apply.ThisDiscountRate;
                        account.AccountMoney += apply.ThisAccountMoney - apply.ThatAccountMoney;
                        this.Account = account;
                    }
                }
            }
        }
    }
}