using MCS.Library.Data;
using MCS.Library.Data.Builder;
using MCS.Library.Data.DataObjects;
using MCS.Library.SOA.DataObjects;
using PPTS.Data.Products.DataSources;
using PPTS.Data.Products.Entities;
using PPTS.WebAPI.Products.ViewModels.ServiceFees;
using System.Collections.Generic;

namespace PPTS.WebAPI.Products.DataSources
{
    public class ExpensesDataSource : GenericProductDataSource<ExpensesQueryModel, ExpensesQueryCollection>
    {
        public static readonly new ExpensesDataSource Instance = new ExpensesDataSource();
        private ExpensesDataSource() { }
        /// <summary>
        /// 综合服务费会分页查询
        /// </summary>
        /// <param name="prp">分页参数</param>
        /// <param name="condition">查询条件</param>
        /// <param name="orderByBuilder">排序条件</param>
        /// <returns></returns>
        public PagedQueryResult<ExpensesQueryModel, ExpensesQueryCollection> GetServiceFeesList(IPageRequestParams prp, object condition, IEnumerable<IOrderByRequestItem> orderByBuilder)
        {
            string select = @"*";
            string from = @" PM.Expenses";

            PagedQueryResult<ExpensesQueryModel, ExpensesQueryCollection> result = Query(prp, select, from, condition, orderByBuilder);
            return result;
        }
        protected override void OnBuildQueryCondition(QueryCondition qc)
        {
            qc.SelectFields = @" Expenses.* ";
            qc.FromClause = @" PM.Expenses Expenses";

            #region 数据权限加工
            qc.WhereClause = PPTS.Data.Common.Authorization.ScopeAuthorization<Expense>
                .GetInstance(ConnectionDefine.DBConnectionName)
                .ReadAuthExistsBuilder("Expenses", qc.WhereClause).ToSqlString(TSqlBuilder.Instance);
            #endregion

            base.OnBuildQueryCondition(qc);
        }
    }
}