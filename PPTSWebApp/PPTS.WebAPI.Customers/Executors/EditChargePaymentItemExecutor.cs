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

namespace PPTS.WebAPI.Customers.Executors
{
    [DataExecutorDescription("编辑缴费支付单")]
    public class EditChargePaymentItemExecutor : PPTSEditAccountExecutorBase<ChargePaymentItemModel>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        public EditChargePaymentItemExecutor(ChargePaymentItemModel model)
            : base(model, null)
        {
            model.NullCheck("model");
        }

        protected override void PrepareData(DataExecutionContext<UserOperationLogCollection> context)
        {
            base.PrepareData(context);
            
            AccountChargePaymentAdapter.Instance.UpdateInContext(this.Model);
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