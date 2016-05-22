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
            this.BuildAccount();

            //转入学员账户相关信息
            ConfigArgs args = ConfigsCache.GetArgs(this.BizCampusID);
            //不是合同账户并且是拓路1的校区，则获取原来的账户，否则构建新的账户
            if (this.AccountType == AccountTypeDefine.Contract
                || args.DiscountSchema == DiscountSchemaDefine.Schema2)
            {
                this.BuildNewBizAccount();
            }
            else
            {
                AccountModel account = AccountModel.LoadCurrentByAccountID(this.BizCustomerID);
                if (account != null)
                {
                    this.BuildOldBizAccount(account);
                }
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
        /// <param name="campusID">校区ID</param>
        /// <param name="customerID">学员ID</param>
        /// <param name="jobType">申请人岗位类型</param>
        /// <returns></returns>
        public static TransferApplyModel LoadByCustomerID(CustomerModel customer)
        {
            TransferApplyModel model = new TransferApplyModel();
            model.ApplyID = Guid.NewGuid().ToString().ToUpper();
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
                TransferApplyModel model = AutoMapper.Mapper.DynamicMap<TransferApplyModel>(apply);
                return model;
            }
            return null;
        }
    }
}