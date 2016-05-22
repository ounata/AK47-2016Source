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
    /// 缴费申请列表查询结果
    /// </summary>
    [Serializable]
    public class ChargeApplyQueryResult
    {
        public PagedQueryResult<ChargeApplyQueryModel, ChargeApplyQueryModelCollection> QueryResult
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