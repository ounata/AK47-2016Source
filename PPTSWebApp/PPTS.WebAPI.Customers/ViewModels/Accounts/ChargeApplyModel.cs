﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.Serialization;
using MCS.Library.Data.Mapping;
using PPTS.Data.Common;
using PPTS.Data.Customers.Entities;
using PPTS.Data.Customers.Adapters;
using PPTS.Data.Customers;
using MCS.Library.OGUPermission;
using PPTS.Data.Common.Security;
using MCS.Library.Net.SNTP;

namespace PPTS.WebAPI.Customers.ViewModels.Accounts
{
    /// <summary>
    /// 缴费申请单模型
    /// </summary>
    [Serializable]
    [DataContract]
    public class ChargeApplyModel : AccountChargeApply
    {
        /// <summary>
        /// 能否编辑申请
        /// </summary>
        [DataMember]
        public bool CanEditApply {
            get
            {
                return this.PayStatus == PayStatusDefine.Unpay
                    && !string.IsNullOrEmpty(this.ApplyNo);
            }
        }

        /// <summary>
        /// 能否编辑付款
        /// </summary>
        [DataMember]
        public bool CanEditPayment
        {
            get
            {
                if (this.PayStatus == PayStatusDefine.Unpay)
                    return true;
                return false;
            }
        }
        
        /// <summary>
        /// 首次充值的最小金额
        /// </summary>
        [DataMember]
        public decimal FirstChargeMinMoney
        {
            set;
            get;
        }
        
        /// <summary>
        /// 业绩分派表
        /// </summary>
        [DataMember]
        public ChargeAllotModel Allot
        {
            get;
            set;
        }

        /// <summary>
        /// 缴费支付表
        /// </summary>
        /// 
        [DataMember]
        public ChargePaymentModel Payment
        {
            get;
            set;
        }

        /// <summary>
        /// 准备保存账户实体
        /// </summary>
        public Account PreparedAccount
        {
            set;
            get;
        }

        /// <summary>
        /// 准备保存的潜客实体
        /// </summary>
        public PotentialCustomer PreparedCustomer
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

        /// <summary>
        /// 为缴费单保存准备数据
        /// </summary>
        /// <param name="jobType"></param>
        public void Prepare4SaveApply(JobTypeDefine jobType)
        {
            CustomerModel customer = CustomerModel.Load(this.CustomerID);
            DiscountModel discount = DiscountModel.LoadByCampusID(this.CampusID);
            this.Init(customer, discount, jobType);
        }

        /// <summary>
        /// 为缴费单收款准备数据
        /// </summary>
        /// <param name="jobType"></param>
        public void Prepare4SavePayment(JobTypeDefine jobType)
        {
            bool isPotential = false;
            PotentialCustomer potential = PotentialCustomerAdapter.Instance.Load(this.CustomerID);
            if (potential != null && potential.Status != CustomerStatus.Formal)
                isPotential = true;
            AccountModel account;
            DiscountModel discount = DiscountModel.LoadByCampusID(this.CampusID);
            this.Caculate(isPotential, discount, jobType, out account);
            this.PreparedAccount = account;
            if (isPotential)
                this.PreparedCustomer = potential;
        }

        public void Init(CustomerModel customer, DiscountModel discount, JobTypeDefine jobType)
        {
            AccountModel account;
            this.Caculate(customer.IsPotential, discount, jobType, out account);
        }
        private void Caculate(bool isPotential, DiscountModel discount, JobTypeDefine jobType, out AccountModel account)
        {
            decimal totalAccountValue;
            ConfigArgs args = ConfigsCache.GetArgs(this.CampusID);
            account = AccountModel.LoadCurrentByCustomerID(this.CustomerID, out totalAccountValue);
            //如果不存在账户或者不是拓路折扣1则新创建账户
            if (account == null || args.DiscountSchema != DiscountSchemaDefine.Schema1)
                account = new AccountModel();

            this.AccountID = account.AccountID;
            this.AccountCode = account.AccountCode;

            this.ThatDiscountID = account.DiscountID;
            this.ThatDiscountCode = account.DiscountCode;
            this.ThatDiscountBase = account.DiscountBase;
            this.ThatDiscountRate = account.DiscountRate;
            this.ThatAccountValue = account.AccountValue;
            this.ThatAccountMoney = account.AccountMoney;

            DiscountResult result = DiscountResult.CalcDiscount(account, discount, this.ChargeMoney);

            this.ThisDiscountID = result.DiscountID;
            this.ThisDiscountCode = result.DiscountCode;
            this.ThisDiscountBase = result.DiscountBase;
            this.ThisDiscountRate = result.DiscountRate;
            this.ThisAccountValue = result.AccountValue;
            this.ThisAccountMoney = result.AccountMoney;

            this.ChargeType = this.BuildChargeType(args, totalAccountValue, isPotential, jobType);
            if (this.ChargeType == ChargeTypeDefine.New)
                this.FirstChargeMinMoney = args.AccountFirstChargeMinMoney;
        }
        
        private ChargeTypeDefine BuildChargeType( ConfigArgs args, decimal accountValue, bool isPotential, JobTypeDefine jobType)
        {
            //新签
            if (isPotential)
                return ChargeTypeDefine.New;

            //前期
            if (jobType == JobTypeDefine.Consultant)
            {
                //结课
                if (accountValue >= args.EndingClassMinAccountValue)
                    return ChargeTypeDefine.EarlyEndRenew;
                //非结课
                else
                    return ChargeTypeDefine.EarlyStudyRenew;
            }
            //后期
            else if (jobType == JobTypeDefine.Educator)
            {
                //结课
                if (accountValue >= args.EndingClassMinAccountValue)
                    return ChargeTypeDefine.LaterEndRenew;
                //非结课
                else
                    return ChargeTypeDefine.LaterStudyRenew;
            }
            else
            {
                return ChargeTypeDefine.NaN;
            }
        }

        /// <summary>
        /// 根据学员ID获取缴费单模型
        /// </summary>
        /// <param name="campusID">校区ID</param>
        /// <param name="customerID">学员ID</param>
        /// <param name="jobType">申请人岗位类型</param>
        /// <returns></returns>
        public static ChargeApplyModel LoadByCustomerID(CustomerModel customer)
        {
            ChargeApplyModel model = null;
            AccountChargeApply apply = AccountChargeApplyAdapter.Instance.LoadUnpayByCustomerID(customer.CustomerID);
            if (apply != null)
            {
                model = AutoMapper.Mapper.DynamicMap<ChargeApplyModel>(apply);
                model.Allot = ChargeAllotModel.Load(apply.ApplyID);
            }
            else
            {
                #region 重新构建支付相关信息
                model = new ChargeApplyModel();
                model.ApplyID = Guid.NewGuid().ToString().ToUpper();
                model.CampusID = customer.CampusID;
                model.CampusName = customer.CampusName;
                model.CustomerID = customer.CustomerID;
                model.CustomerCode = customer.CustomerCode;
                model.CustomerName = customer.CustomerName;                
                model.Allot = new ChargeAllotModel();
                #endregion
            }
            return model;
        }

        /// <summary>
        /// 根据申请单ID获取缴费单模型
        /// </summary>
        /// <param name="applyID">申请单ID</param>
        /// <returns></returns>
        public static ChargeApplyModel LoadByApplyID(string applyID)
        {
            AccountChargeApply apply = AccountChargeApplyAdapter.Instance.LoadByApplyID(applyID);
            if (apply != null)
            {
                ChargeApplyModel model = AutoMapper.Mapper.DynamicMap<ChargeApplyModel>(apply);
                model.Allot = ChargeAllotModel.Load(applyID);
                model.Payment = ChargePaymentModel.Load(applyID);
                return model;
            }
            return null;
        }

        /// <summary>
        /// 根据申请单ID获取缴费单模型
        /// </summary>
        /// <param name="applyID">申请单ID</param>
        /// <returns></returns>
        public static ChargeApplyModel LoadByApplyID4Allot(string applyID)
        {
            AccountChargeApply apply = AccountChargeApplyAdapter.Instance.LoadByApplyID(applyID);
            if (apply != null)
            {
                ChargeApplyModel model = AutoMapper.Mapper.DynamicMap<ChargeApplyModel>(apply);
                model.Allot = ChargeAllotModel.Load(applyID);
                return model;
            }
            return null;
        }

        /// <summary>
        /// 根据申请单ID获取缴费单模型
        /// </summary>
        /// <param name="applyID">申请单ID</param>
        /// <returns></returns>
        public static ChargeApplyModel LoadByApplyID4Payment(string applyID)
        {
            AccountChargeApply apply = AccountChargeApplyAdapter.Instance.LoadByApplyID(applyID);
            if (apply != null)
            {
                ChargeApplyModel model = AutoMapper.Mapper.DynamicMap<ChargeApplyModel>(apply);
                model.Payment = ChargePaymentModel.Load(applyID);
                return model;
            }
            return null;
        }
    }
}