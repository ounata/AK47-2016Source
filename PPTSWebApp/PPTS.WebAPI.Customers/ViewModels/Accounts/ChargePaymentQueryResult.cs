using MCS.Library.Data;
using PPTS.Data.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PPTS.WebAPI.Customers.ViewModels.Accounts
{
    /// <summary>
    /// 缴费支付列表查询模型
    /// </summary>
    [Serializable]
    public class ChargePaymentQueryResult
    {
        public PagedQueryResult<ChargePaymentQueryModel, ChargePaymentQueryModelCollection> QueryResult
        {
            get;
            set;
        }
        public IDictionary<string, IEnumerable<BaseConstantEntity>> Dictionaries
        {
            get;
            set;
        }
    }
}