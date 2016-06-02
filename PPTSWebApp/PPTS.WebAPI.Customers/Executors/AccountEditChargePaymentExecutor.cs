using System;
using MCS.Library.Core;
using MCS.Library.Data;
using MCS.Library.Data.Executors;
using MCS.Library.SOA.DataObjects;
using PPTS.Data.Common.Executors;
using PPTS.Data.Customers.Adapters;
using PPTS.Data.Customers.Entities;
using PPTS.Data.Customers;
using PPTS.WebAPI.Customers.ViewModels.Accounts;
using MCS.Library.Net.SNTP;
using PPTS.Data.Common.Security;
using PPTS.Data.Customers.Executors;

namespace PPTS.WebAPI.Customers.Executors
{
    [DataExecutorDescription("编辑缴费收款单")]
    public class AccountEditChargePaymentExecutor : PPTSEditCustomerExecutorBase<ChargeApplyModel>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        public AccountEditChargePaymentExecutor(ChargeApplyModel model)
            : base(model, null)
        {
            model.NullCheck("model");            
        }

        protected override void PrepareData(DataExecutionContext<UserOperationLogCollection> context)
        {
            base.PrepareData(context);

            this.Model.PayStatus = PayStatusDefine.Paid;
            this.Model.PayTime = SNTPClient.AdjustedTime;
            this.Model.PaidMoney = this.Model.Payment.PaidMoney;
            AccountChargeApplyAdapter.Instance.UpdateInContext(this.Model);
            AccountChargePaymentAdapter.Instance.DeleteInContext(builder => builder.AppendItem("ApplyID", this.Model.ApplyID));
            foreach (ChargePaymentItemModel itemModel in this.Model.Payment.Items)
            {
                if (itemModel.PayMoney != 0)
                {
                    itemModel.FillCreator();
                    itemModel.FillModifier();
                    itemModel.PayID = Guid.NewGuid().ToString().ToUpper();
                    itemModel.PayTime = this.Model.PayTime;
                    itemModel.PayStatus = this.Model.PayStatus;
                    AccountChargePaymentAdapter.Instance.UpdateInContext(itemModel);
                }
            }
            if (this.Model.PayStatus == PayStatusDefine.Paid)
            {
                Account account = this.Model.PreparedAccount;
                if (account == null)
                    account = new Account();
                account.CustomerID = this.Model.CustomerID;
                account.AccountID = this.Model.AccountID;
                account.AccountCode = this.Model.AccountCode;
                account.AccountType = AccountTypeDefine.Tunland;
                account.AccountMoney = this.Model.ThisAccountMoney;
                account.DiscountID = this.Model.ThisDiscountID;
                account.DiscountCode = this.Model.ThisDiscountCode;
                account.DiscountBase = this.Model.ThisDiscountBase;
                account.DiscountRate = this.Model.ThisDiscountRate;
                account.ChargeApplyID = this.Model.ApplyID;
                account.ChargePayTime = this.Model.PayTime;
                if(this.Model.ChargeType == ChargeTypeDefine.New)
                {
                    account.FirstChargeApplyID = this.Model.ApplyID;
                    account.FirstChargePayTime = this.Model.PayTime;
                }
                account.FillCreator();
                account.FillModifier();
                AccountAdapter.Instance.UpdateInContext(account);

                if (this.Model.PreparedCustomer != null)
                {
                    PotentialCustomer potential = this.Model.PreparedCustomer;
                    potential.Status = CustomerStatus.Formal;
                    potential.FillModifier();
                    Customer customer = AutoMapper.Mapper.DynamicMap<Customer>(potential);
                    customer.FillCreator();
                    customer.FillModifier();
                    customer.VersionEndTime = DateTime.MinValue;
                    customer.VersionStartTime = DateTime.MinValue;
                    PotentialCustomerAdapter.Instance.UpdateInContext(potential);
                    CustomerAdapter.Instance.UpdateInContext(customer);
                }
                if (this.Model.PreparedVerify != null)
                {
                    CustomerVerify verify = this.Model.PreparedVerify;
                    verify.FillCreator();
                    CustomerVerifyAdapter.Instance.UpdateInContext(verify);
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