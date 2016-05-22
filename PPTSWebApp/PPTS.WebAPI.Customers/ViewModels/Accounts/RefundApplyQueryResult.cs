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
    /// 退费支付列表查询结果
    /// </summary>
    [Serializable]
    public class RefundApplyQueryResult
    {
        public PagedQueryResult<RefundApplyQueryModel, RefundApplyQueryModelCollection> QueryResult
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