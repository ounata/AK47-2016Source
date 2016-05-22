using MCS.Library.Net.SNTP;
using MCS.Library.OGUPermission;
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
            //是否是制度外退费
            this.IsExtraRefund = (this.ExtraRefundMoney != 0);

            //保存当时的实退费金额，以便验证后面规则是否正确
            decimal clientRealRefundMoney = this.RealRefundMoney;

            AccountModel account = AccountModel.LoadByAccountID(this.AccountID, true);
            if (account == null)
                throw new Exception(string.Format("账户ID为[{0}]的信息不存在", this.AccountID));
            DiscountModel discount = DiscountModel.LoadByCampusID(this.CampusID);

            //新的账户余额=以前的账户余额-申退金额
            this.ThisAccountMoney = this.ThatAccountMoney - this.ApplyRefundMoney;
            //新的账户价值=以前的账户价值-申退金额
            this.ThisAccountValue = this.ThatAccountValue - this.ApplyRefundMoney;

            //当前发生的课时价值(confirmedValue>=thatDiscountBase)当前折扣基数
            if (account.OccurenceValue >= this.ThatDiscountBase)
            {
                this.ConsumptionValue = 0;
                this.ReallowanceMoney = 0;

                this.ThisDiscountID = this.ThatDiscountID;
                this.ThisDiscountCode = this.ThatDiscountCode;
                this.ThisDiscountBase = this.ThatDiscountBase;
                this.ThisDiscountRate = this.ThatDiscountRate;
            }
            else {
                //计算折扣相关数值
                //新的账户价值=以前的账户价值                            
                this.ThisDiscountBase = this.ThatDiscountBase - this.ApplyRefundMoney;
                //新的折扣率计算
                this.ThisDiscountRate = 1;
                if (discount != null && discount.Items.Count != 0)
                {
                    this.ThisDiscountID = discount.DiscountID;
                    this.ThisDiscountCode = discount.DiscountCode;
                    this.ThisDiscountRate = discount.Items[0].DiscountValue;
                    for (var i = 0; i < discount.Items.Count; i++)
                    {
                        var item = discount.Items[i];
                        if (this.ThisDiscountBase >= item.DiscountStandard * 10000)
                        {
                            this.ThisDiscountRate = item.DiscountValue;
                            break;
                        }
                    }
                }

                //已消耗课时价值不变
                //折扣返还金额 = 已消耗课时价值/退费前折扣率*（退费后折扣率-退费前折扣率）
                if (this.ThatDiscountRate > 0)
                {
                    this.ReallowanceMoney = this.ConsumptionValue / this.ThatDiscountRate * (this.ThisDiscountRate - this.ThatDiscountRate);
                }
            }
            //应退金额 = 申退金额-折扣返还
            this.OughtRefundMoney = this.ApplyRefundMoney - this.ReallowanceMoney;
            //实退金额 = 应退金额-差价补偿+制度外退款
            this.RealRefundMoney = this.OughtRefundMoney - this.CompensateMoney + this.ExtraRefundMoney;

            if (clientRealRefundMoney != this.RealRefundMoney)
                throw new Exception("数据校验不合法，请重新再操作一次");
        }

        private void BuildRefundType()
        {
            this.RefundType = RefundTypeDefine.Regular;
            this.IsPeriodRefund = false;
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
            model.ApplyID = Guid.NewGuid().ToString().ToUpper();
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
                RefundApplyModel model = AutoMapper.Mapper.DynamicMap<RefundApplyModel>(apply);
                model.Allot = RefundAllotModel.Load(apply.ApplyID);
                return model;
            }
            return null;
        }
    }
}