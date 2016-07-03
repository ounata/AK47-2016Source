using MCS.Library.Core;
using MCS.Library.Data;
using MCS.Library.Data.Executors;
using MCS.Library.SOA.DataObjects;
using PPTS.Data.Customers.Adapters;
using PPTS.Data.Customers.Executors;
using PPTS.Data.Customers.Entities;
using PPTS.Data.Customers;
using PPTS.WebAPI.Customers.ViewModels.Accounts;
using PPTS.Data.Common.Security;
using PPTS.Data.Common;
using MCS.Library.Principal;
using System.Collections.Generic;
using System;
using PPTS.Contracts.Search.Models;

namespace PPTS.WebAPI.Customers.Executors
{
    [DataExecutorDescription("确认退费单")]
    public class AccountVerifyRefundApplyExecutor : PPTSEditCustomerExecutorBase<RefundVerifyingModel>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        public AccountVerifyRefundApplyExecutor(RefundVerifyingModel model)
            : base(model, null)
        {
            model.NullCheck("model");
        }

        protected override object DoOperation(DataExecutionContext<UserOperationLogCollection> context)
        {
            #region 把数据保存到队列去更新CustomerSearch
            if (this.Model.Apply.VerifyStatus == RefundVerifyStatus.Refunded)
            {
                AccountAdapter.Instance.GetSqlContext().AfterActions.Add(() => UpdateCustomerSearchByCustomerTask.Instance.UpdateByCustomerInfoByTask(new CustomerSearchUpdateModel()
                {
                    CustomerID = this.Model.Apply.CustomerID,
                    Type = CustomerSearchUpdateType.AccountRefundApply
                }));
            }
            #endregion

            base.DoOperation(context);
            return this.Model.Apply;
        }

        private void Init(RefundVerifyingModel model, RefundApplyModel apply)
        {
            apply.FillModifier();
            switch (model.VerifyAction)
            {
                case RefundVerifyAction.CashierVerify:
                    if (apply.VerifyStatus == RefundVerifyStatus.WaitCashierVerify)
                        apply.InitVerifier(DeluxeIdentity.CurrentUser, RefundVerifyStatus.WaitFinanceVerify);
                    break;
                case RefundVerifyAction.FinanceVerify:
                    if (apply.VerifyStatus == RefundVerifyStatus.WaitFinanceVerify)
                        apply.InitVerifier(DeluxeIdentity.CurrentUser, RefundVerifyStatus.WaitRegionVerify);
                    break;
                case RefundVerifyAction.RegionVerify:
                    if (apply.VerifyStatus == RefundVerifyStatus.WaitRegionVerify)
                        apply.InitVerifier(DeluxeIdentity.CurrentUser, RefundVerifyStatus.Refunded);
                    break;
                default:
                    break;
            }
        }

        protected override void PrepareData(DataExecutionContext<UserOperationLogCollection> context)
        {
            base.PrepareData(context);

            if (this.Model.Apply != null)
            {
                this.Init(this.Model, this.Model.Apply);
                AccountRefundApplyAdapter.Instance.UpdateInContext(this.Model.Apply);
                AccountRefundVerifyingAdapter.Instance.UpdateInContext(this.Model);
                if (this.Model.Account != null)
                    AccountAdapter.Instance.UpdateInContext(this.Model.Account);
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