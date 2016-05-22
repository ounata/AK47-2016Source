using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using PPTS.Data.Customers.Entities;
using MCS.Library.Data.DataObjects;
using PPTS.Data.Customers;

namespace PPTS.WebAPI.Customers.ViewModels.Accounts
{
    /// <summary>
    /// 退费支付单模型
    /// </summary>
    [Serializable]
    public class RefundApplyQueryModel : AccountRefundApply
    {
    }

    [Serializable]
    public class RefundApplyQueryModelCollection : EditableDataObjectCollectionBase<RefundApplyQueryModel>
    {

    }
}