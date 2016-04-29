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
    public class EditChargePaymentExecutor : PPTSEditAccountExecutorBase<ChargeApplyModel>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        public EditChargePaymentExecutor(ChargeApplyModel model)
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
                    AccountChargePaymentAdapter.Instance.UpdateInContext(itemModel);
                }
            }         
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