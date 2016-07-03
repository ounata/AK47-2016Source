using MCS.Library.Core;
using MCS.Library.Net.SNTP;
using MCS.Library.OGUPermission;
using MCS.Web.MVC.Library.Models;
using PPTS.Contracts.Orders.Models;
using PPTS.Contracts.Proxies;
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
    /// 退费申请单模型
    /// </summary>
    [Serializable]
    [DataContract]
    public class RefundApplyModel : AccountRefundApply
    {

        /// <summary>
        /// 附件集合
        /// </summary>
        [DataMember]
        public MaterialModelCollection Files { get; set; }


        /// <summary>
        /// 业绩分派表
        /// </summary>
        [DataMember]
        public RefundAllotModel Allot
        {
            get;
            set;
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
        /// 初始化确认人信息
        /// </summary>
        public void InitVerifier(IUser user, RefundVerifyStatus status)
        {
            this.VerifierID = user.ID;
            this.VerifierName = user.Name;
            this.VerifierJobID = user.GetCurrentJob().ID;
            this.VerifierJobName = user.GetCurrentJob().Name;
            this.VerifyTime = SNTPClient.AdjustedTime;
            this.VerifyStatus = status;
        }

        /// <summary>
        /// 初始化对账人信息
        /// </summary>
        public void InitChecker(IUser user)
        {
            this.CheckerID = user.ID;
            this.CheckerName = user.Name;
            this.CheckerJobID = user.GetCurrentJob().ID;
            this.CheckerJobName = user.GetCurrentJob().Name;
            this.CheckTime = SNTPClient.AdjustedTime;
            this.CheckStatus = CheckStatusDefine.Checked;
        }

        /// <summary>
        /// 为保存申请准备数据
        /// </summary>
        public void Prepare4SaveApply()
        {
            this.BuildRefundType();

            //保存当时的实退费金额，以便验证后面规则是否正确
            decimal clientRealRefundMoney = this.RealRefundMoney;

            AccountModel account = AccountModel.LoadByAccountID(this.AccountID, true);
            if (account == null)
                throw new Exception(string.Format("账户ID为[{0}]的信息不存在", this.AccountID));
#if !DEBUG
            if (account.AssetMoney != 0)
                throw new Exception("订购资金余额不为0，无法退费");
#endif
            this.ApplyNo = Helper.GetApplyNo("TF");

            this.AccountID = account.AccountID;
            this.AccountCode = account.AccountCode;
            this.ThatDiscountID = account.DiscountID;
            this.ThatDiscountCode = account.DiscountCode;
            this.ThatDiscountBase = account.DiscountBase;
            this.ThatDiscountRate = account.DiscountRate;
            this.ThatAccountValue = account.AccountValue;
            this.ThatAccountMoney = account.AccountMoney;

            //课时消耗价值
            this.ConsumptionValue = account.ConsumptionValue;
            //新的账户余额=以前的账户余额-申退金额
            this.ThisAccountMoney = this.ThatAccountMoney - this.ApplyRefundMoney;
            //新的账户价值=以前的账户价值-申退金额
            this.ThisAccountValue = this.ThatAccountValue - this.ApplyRefundMoney;

            //消耗的课时价值(consumptionValue>=thatDiscountBase)当前折扣基数
            if (this.ConsumptionValue >= this.ThatDiscountBase)
            {
                this.ThisDiscountID = this.ThatDiscountID;
                this.ThisDiscountCode = this.ThatDiscountCode;
                this.ThisDiscountBase = this.ThatDiscountBase;
                this.ThisDiscountRate = this.ThatDiscountRate;
                this.ReallowanceMoney = 0;
            }
            else
            {
                if(string.IsNullOrEmpty(account.RefundDiscountID))
                    throw new Exception("数据存在问题，存在充值记录没有指定折扣表");

                decimal discountBase = this.ThatDiscountBase - this.ApplyRefundMoney;
                RefundReallowanceResult result = RefundReallowanceResult.GetReallowance(account.AccountID, account.RefundDiscountID, discountBase, account.ReallowanceStartTime);
                if (string.IsNullOrEmpty(result.DiscountID))
                    throw new Exception(string.Format("折扣ID{0}不存在", account.RefundDiscountID));

                this.ThisDiscountID = result.DiscountID;
                this.ThisDiscountCode = result.DiscountCode;
                this.ThisDiscountBase = discountBase;
                this.ThisDiscountRate = result.DiscountRate;
                this.ReallowanceMoney = result.ReallowanceMoney;
            }
            //应退金额 = 申退金额-折扣返还
            this.OughtRefundMoney = this.ApplyRefundMoney - this.ReallowanceMoney;
            //实退金额 = 应退金额-差价补偿+制度外退款
            this.RealRefundMoney = this.OughtRefundMoney - this.CompensateMoney + this.ExtraRefundMoney;

            //如果没有制度外退款，制度外退款类型为空
            if (this.ExtraRefundMoney <= 0)
                this.ExtraRefundType = string.Empty;

            if (clientRealRefundMoney != this.RealRefundMoney)
                throw new Exception("数据校验不合法，请重新再操作一次");
        }

        private void BuildRefundType()
        {
            ConfigArgs args = ConfigsCache.GetArgs(this.CampusID);
            AccountChargeApply charge = AccountChargeApplyAdapter.Instance.LoadNewSignByCustomerID(this.CustomerID);
            //对于老合同学员没有新签记录，默认是正常退费
            if (charge == null)
                this.RefundType = RefundTypeDefine.Regular;
            //查找到当前有无上课记录
            AssetStatisticQueryResult result = PPTSAssetQueryServiceProxy.Instance.QueryAssetStatisticByCustomerID(this.CustomerID);
            if (charge != null)
            {
                DateTime currentDate = SNTPClient.AdjustedTime.Date; //当前日期
                DateTime newSignDate = charge.PayTime.Date;          //新签日期
                                                                     //新签后?天内未上课是坏账退费
                if (newSignDate.AddDays(args.AccountRefundTypeJudgeDays) >= currentDate && result.ConfirmedAmount == 0)
                    this.RefundType = RefundTypeDefine.Irregular;
                else
                    this.RefundType = RefundTypeDefine.Regular;
            }
            //是否有课时
            this.IsPeriodRefund = (result.ConfirmedAmount != 0);
            //是否是制度外退费
            this.IsExtraRefund = (this.ExtraRefundMoney != 0);
        }

        /// <summary>
        /// 根据学员ID获取缴费单模型
        /// </summary>
        /// <param name="campusID">校区ID</param>
        /// <param name="customerID">学员ID</param>
        /// <param name="jobType">申请人岗位类型</param>
        /// <returns></returns>
        public static RefundApplyModel LoadByCustomerID(CustomerModel customer)
        {
            RefundApplyModel model = new RefundApplyModel();
            model.ApplyID = UuidHelper.NewUuidString();
            model.CampusID = customer.CampusID;
            model.CampusName = customer.CampusName;
            model.CustomerID = customer.CustomerID;
            model.CustomerCode = customer.CustomerCode;
            model.CustomerName = customer.CustomerName;
            model.Drawer = customer.ParentName;
            model.Allot = new RefundAllotModel();
            return model;
        }

        /// <summary>
        /// 根据申请单ID获取缴费单模型
        /// </summary>
        /// <param name="applyID">申请单ID</param>
        /// <returns></returns>
        public static RefundApplyModel LoadByApplyID(string applyID)
        {
            AccountRefundApply apply = AccountRefundApplyAdapter.Instance.LoadByApplyID(applyID);
            if (apply != null)
            {
                RefundApplyModel model = apply.ProjectedAs<RefundApplyModel>();
                model.Allot = RefundAllotModel.Load(model);
                return model;
            }
            return null;
        }
    }
}