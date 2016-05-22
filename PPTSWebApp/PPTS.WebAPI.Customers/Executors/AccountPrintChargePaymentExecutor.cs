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
using System.Collections.Generic;

namespace PPTS.WebAPI.Customers.Executors
{
    [DataExecutorDescription("打印缴费支付单")]
    public class AccountPrintChargePaymentExecutor : PPTSCustomerExecutorBase
    {
        string[] _payIDs;
        public AccountPrintChargePaymentExecutor(string[] payIDs)
            : base("Print")
        {
            _payIDs = payIDs;
        }

        protected override string GetOperationDescription()
        {
            string description = base.GetOperationDescription();
            if (string.IsNullOrWhiteSpace(description))
            {
                return this.OperationType;
            }
            return description;
        }

        protected override object DoOperation(DataExecutionContext<UserOperationLogCollection> context)
        {
            List<ChargePaymentItemModel> list = new List<ChargePaymentItemModel>();
            foreach (string payID in _payIDs)
            {
                AccountChargePayment payment = AccountChargePaymentAdapter.Instance.LoadByPayID(payID);
                if (payment != null && payment.PayStatus == PayStatusDefine.Paid)
                {
                    ChargePaymentItemModel model = AutoMapper.Mapper.DynamicMap<ChargePaymentItemModel>(payment);
                    model.FillModifier();
                    model.PrintStatus = PrintStatusDefine.Printed;
                    AccountChargePaymentAdapter.Instance.Update(model);
                    list.Add(model);
                }
            }
            return list;
        }
    }
}