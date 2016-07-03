using MCS.Library.Data;
using PPTS.Data.Common.Adapters;
using PPTS.Data.Common.Entities;
using PPTS.WebAPI.Customers.DataSources;
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

        public static ChargeApplyQueryResult Query(ChargeApplyQueryCriteriaModel criteria)
        {
            ChargeApplyQueryResult result = new ChargeApplyQueryResult();
            result.QueryResult = AccountChargeApplyDataSource.Instance.QueryResult(criteria.PageParams, criteria, criteria.OrderBy); ;
            result.Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(ChargeApplyQueryModel), typeof(ChargeApplyQueryCriteriaModel));
            return result;
        }
    }         
}