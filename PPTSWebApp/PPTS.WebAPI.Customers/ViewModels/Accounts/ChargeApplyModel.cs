using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.Serialization;
using MCS.Library.Data.Mapping;
using PPTS.Data.Common;
using PPTS.Data.Customers.Entities;
using PPTS.Data.Customers.Adapters;
using PPTS.Data.Customers;

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
        /// 初始化提交人信息
        /// </summary>
        public void InitApplier()
        {
            this.ApplyTime = DateTime.UtcNow;
            this.ApplyStatus = ApplyStatusDefine.Approved;
        }

        /// <summary>
        /// 根据申请人初始化提交人信息
        /// </summary>
        public void InitSubmitterFromApplier()
        {
            this.SubmitterID = this.ApplierID;
            this.SubmitterName = this.ApplierName;
            this.SubmitterJobID = this.ApplierJobID;
            this.SubmitterJobName = this.ApplierJobName;
            this.SubmitterJobType = this.ApplierJobType;
            this.SubmitTime = this.ApplyTime;
        }

        /// <summary>
        /// 根据申请人初始化提交人信息
        /// </summary>
        public void InitApproverFromApplier()
        {
            this.ApproverID = this.ApplierID;
            this.ApproverName = this.ApplierName;
            this.ApproverJobID = this.ApplierJobID;
            this.ApproverJobName = this.ApplierJobName;
            this.ApproveTime = this.ApplyTime;
        }
        
        public static ChargeTypeDefine BuildChargeType(CustomerModel customer, JobTypeDefine applierJobType, List<AccountModel> accounts, ConfigArgs args)
        {
            //新签
            if (customer.IsPotential)
                return ChargeTypeDefine.New;
            decimal accountValue = accounts.Sum(x => x.AccountValue);

            //前期
            if (applierJobType == JobTypeDefine.Consultant)
            {
                //结课
                if (accountValue >= args.EndingClassMinAccountValue)
                    return ChargeTypeDefine.EarlyEndRenew;
                //非结课
                else
                    return ChargeTypeDefine.EarlyStudyRenew;
            }
            //后期
            else if (applierJobType == JobTypeDefine.Educator)
            {
                //结课
                if (accountValue >= args.EndingClassMinAccountValue)
                    return ChargeTypeDefine.LaterEndRenew;
                //非结课
                else
                    return ChargeTypeDefine.LaterStudyRenew;
            }
            else
                throw new Exception("不支持其它岗位充值");
        }

        /// <summary>
        /// 根据学员ID获取缴费单模型
        /// </summary>
        /// <param name="campusID">校区ID</param>
        /// <param name="customerID">学员ID</param>
        /// <param name="jobType">申请人岗位类型</param>
        /// <returns></returns>
        public static ChargeApplyModel LoadByCustomerID(CustomerModel customer, JobTypeDefine jobType)
        {
            ChargeApplyModel model = new ChargeApplyModel();
            model.ApplyID = Guid.NewGuid().ToString().ToUpper();
            model.CampusID = customer.CampusID;
            model.CampusName = customer.CampusName;
            model.CustomerID = customer.CustomerID;
            model.CustomerCode = customer.CustomerCode;
            model.CustomerName = customer.CustomerName;

            List<AccountModel> accountModels = new List<AccountModel>();
            foreach (Account account in AccountAdapter.Instance.LoadCollectionByCustomerID(customer.CustomerID))
                accountModels.Add(AccountModel.Load(account));
            ConfigArgs args = ConfigsCache.GetArgs(customer.CampusID);
            if (args.DiscountSchema == DiscountSchemaDefine.Schema1 && accountModels.Count != 0)
            {
                AccountModel accountModel = accountModels.Where(x => x.IsLatest == true).SingleOrDefault();
                if (accountModel != null)
                {
                    model.ThatDiscountID = accountModel.DiscountID;
                    model.ThatDiscountBase = accountModel.DiscountBase;
                    model.ThatDiscountRate = accountModel.DiscountRate;
                    model.ThatAccountValue = accountModel.AccountValue;
                }
            }
            model.FirstChargeMinMoney = args.AccountFirstChargeMinMoney;
            model.ChargeType = BuildChargeType(customer, jobType, accountModels, args);
            model.Allot = new ChargeAllotModel();
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