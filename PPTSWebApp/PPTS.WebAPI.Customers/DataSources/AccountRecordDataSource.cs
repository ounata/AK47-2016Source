using System.Collections.Generic;
using MCS.Library.Data;
using PPTS.Data.Customers.DataSources;
using PPTS.WebAPI.Customers.ViewModels.CustomerFollows;
using PPTS.Data.Customers.Entities;
using PPTS.Data.Customers.Adapters;
using PPTS.WebAPI.Customers.ViewModels.Accounts;
using MCS.Library.Data.Mapping;
using MCS.Library.Data.Builder;
using System;
using MCS.Library.Core;

namespace PPTS.WebAPI.Customers.DataSources
{
    /// <summary>
    /// 缴费单数据源类
    /// </summary>
    public class AccountRecordDataSource : GenericCustomerDataSource<AccountRecordQueryModel, AccountRecordQueryModelCollection>
    {
        public static readonly new AccountRecordDataSource Instance = new AccountRecordDataSource();

        private AccountRecordDataSource()
        {
        }
        
        public PagedQueryResult<AccountRecordQueryModel, AccountRecordQueryModelCollection> QueryResult(IPageRequestParams prp, object condition, IEnumerable<IOrderByRequestItem> orderByBuilder)
        {
            var select = @"*";
            var from = @" CM.v_AccountRecords";
                        
            return this.Query(prp, select, from, condition, orderByBuilder);
        }
    }
}
