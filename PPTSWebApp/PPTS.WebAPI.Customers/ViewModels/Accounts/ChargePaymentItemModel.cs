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
    /// 缴费单支付明细模型
    /// </summary>
    [Serializable]
    [DataContract]
    public class ChargePaymentItemModel : AccountChargePayment
    {

    }
}