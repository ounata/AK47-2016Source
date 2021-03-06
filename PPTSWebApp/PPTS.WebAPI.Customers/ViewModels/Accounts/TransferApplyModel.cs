﻿using MCS.Library.Core;
using MCS.Library.Net.SNTP;
using MCS.Library.OGUPermission;
using PPTS.Data.Common;
using PPTS.Data.Common.Security;
using PPTS.Data.Customers;
using PPTS.Data.Customers.Adapters;
using PPTS.Data.Customers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PPTS.WebAPI.Customers.ViewModels.Accounts
{
    /// <summary>
    /// 转让申请单模型
    /// </summary>
    [Serializable]
    [DataContract]
    public class TransferApplyModel : AccountTransferApply
    {
        /// <summary>
        /// 初始化提交人信息
        /// </summary>
        public void InitApplier(IUser user)
        {
            this.ApplierID = user.ID;
            this.ApplierName = user.Name;
            this.ApplierJobID = user.GetCurrentJob().ID;
            this.ApplierJobName = user.GetCurrentJob().Name;
            this.ApplyTime = SNTPClient.AdjustedTime;
            this.ApplyStatus = Data.Customers.ApplyStatusDefine.New;
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
            this.SubmitTime = SNTPClient.AdjustedTime;
            this.ApplyStatus = Data.Customers.ApplyStatusDefine.Approving;
        }

        /// <summary>
        /// 初始化审批人
        /// </summary>
        /// <param name="user"></param>
        public void InitApprover(IUser user, ApplyStatusDefine status)
        {
            this.ApproverID = user.ID;
            this.ApproverName = user.Name;
            this.ApproverJobID = user.GetCurrentJob().ID;
            this.ApproverJobName = user.GetCurrentJob().Name;
            this.ApproveTime = SNTPClient.AdjustedTime;
            this.ApplyStatus = status;
        }

        /// <summary>
        /// 准备数据
        /// </summary>
        public void Prepare(IUser user)
        {
            this.ApplyNo = Helper.GetApplyNo("ZR");

            this.BuildAccount();

            //转入学员账户相关信息
            ConfigArgs args = ConfigsCache.GetArgs(this.BizCampusID);
            //拓路2的校区构建新的账户
            if (args.IsTulandDiscountSchema2)
            {
                this.BuildNewBizAccount();
            }
            else if(this.AccountType == AccountTypeDefine.Contract)
            {
                //如果转入方没有老的合同账户，则新建合同账户
                AccountModel bizAccount = AccountModel.LoadContractByCustomerID(this.BizCustomerID);
                if (bizAccount != null)
                    this.BuildOldBizAccount(bizAccount);
                else
                    this.BuildNewBizAccount();
            }
            else
            { 
                //如果转入方没有可充值的账户，则新建账户
                AccountModel bizAccount = AccountModel.LoadChargableByCustomerID(this.BizCustomerID);
                if (bizAccount != null)
                    this.BuildOldBizAccount(bizAccount);
                else
                    this.BuildNewBizAccount();
            }
        }

        private void BuildAccount()
        {
            AccountModel account = AccountModel.LoadByAccountID(this.AccountID);
            if (account == null)
                throw new Exception("转出学员账户不存在");

            //转出学员账户相关信息 
            this.AccountID = account.AccountID;
            this.AccountCode = account.AccountCode;
            this.AccountType = account.AccountType;
            this.ThatDiscountID = account.DiscountID;
            this.ThatDiscountCode = account.DiscountCode;
            this.ThatDiscountBase = account.DiscountBase;
            this.ThatDiscountRate = account.DiscountRate;
            this.ThatAccountMoney = account.AccountMoney;
            this.ThatAccountValue = account.AccountValue;
            this.ThisDiscountID = account.DiscountID;
            this.ThisDiscountCode = account.DiscountCode;
            this.ThisDiscountBase = account.DiscountBase;
            this.ThisDiscountRate = account.DiscountRate;
            this.ThisAccountMoney = account.AccountMoney - this.TransferMoney;
            this.ThisAccountValue = account.AccountValue - this.TransferMoney;

        }
        private void BuildOldBizAccount(AccountModel account)
        {
            this.BizAccountID = account.AccountID;
            this.BizAccountCode = account.AccountCode;
            this.BizAccountType = account.AccountType;
            this.BizThatDiscountID = account.DiscountID;
            this.BizThatDiscountCode = account.DiscountCode;
            this.BizThatDiscountBase = account.DiscountBase;
            this.BizThatDiscountRate = account.DiscountRate;
            this.BizThatAccountMoney = account.AccountMoney;
            this.BizThatAccountValue = account.AccountValue;
            this.BizThisDiscountID = account.DiscountID;
            this.BizThisDiscountCode = account.DiscountCode;
            this.BizThisDiscountBase = account.DiscountBase;
            this.BizThisDiscountRate = account.DiscountRate;
            this.BizThisAccountMoney = account.AccountMoney + this.TransferMoney;
            this.BizThisAccountValue = account.AccountValue + this.TransferMoney;
        }
        private void BuildNewBizAccount()
        {
            DiscountResult result = DiscountResult.CalcDiscount(this.BizCampusID, this.TransferMoney);
            if (string.IsNullOrEmpty(result.DiscountID))
                throw new Exception("转入校区的折扣表不存在");
            this.BizAccountID = this.ApplyID;
            this.BizAccountCode = Helper.GetAccountCode();
            this.BizAccountType = this.AccountType;
            this.BizThisDiscountID = result.DiscountID;
            this.BizThisDiscountCode = result.DiscountCode;
            this.BizThisDiscountBase = result.DiscountBase;
            this.BizThisDiscountRate = result.DiscountRate;
            this.BizThisAccountMoney = result.AccountMoney;
            this.BizThisAccountValue = result.AccountValue;
        }

        /// <summary>
        /// 根据学员ID获取缴费单模型
        /// </summary>
        /// <param name="customer">学员</param>
        /// <returns></returns>
        public static TransferApplyModel LoadByCustomer(CustomerModel customer)
        {
            TransferApplyModel model = new TransferApplyModel();
            model.ApplyID = UuidHelper.NewUuidString();
            model.CampusID = customer.CampusID;
            model.CampusName = customer.CampusName;
            model.CustomerID = customer.CustomerID;
            model.CustomerCode = customer.CustomerCode;
            model.CustomerName = customer.CustomerName;
            model.TransferType = AccountTransferType.TransferOut;
            return model;
        }

        /// <summary>
        /// 根据申请单ID获取缴费单模型
        /// </summary>
        /// <param name="applyID">申请单ID</param>
        /// <returns></returns>
        public static TransferApplyModel LoadByApplyID(string applyID)
        {
            AccountTransferApply apply = AccountTransferApplyAdapter.Instance.LoadByApplyID(applyID);
            if (apply != null)
            {
                TransferApplyModel model = apply.ProjectedAs<TransferApplyModel>();
                return model;
            }
            return null;
        }
    }
}