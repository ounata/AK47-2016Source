using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PPTS.Data.Orders.Entities;
using MCS.Library.Data.Builder;
using MCS.Library.Data.Mapping;
using MCS.Library.Core;

namespace PPTS.Data.Orders.Adapters
{
    public class FinancialAssignMonthlyIncomeAdapter : OrderAdapterBase<FinancialAssignMonthlyIncome,FinancialAssignMonthlyIncomeCollection>
    {
        public static readonly FinancialAssignMonthlyIncomeAdapter Instance = new FinancialAssignMonthlyIncomeAdapter();

        /// <summary>
        /// 根据同步状态查询数据
        /// </summary>
        /// <param name="synStatus"></param>
        public FinancialAssignMonthlyIncomeCollection LoadCollectionBySynStatus(SynchroStatusDefine synStatus)
        {
            return this.Load(builder => builder.AppendItem("IsSyn", Convert.ToInt32(synStatus).ToString()));
        }

        /// <summary>
        /// 更新课时月收入表，未同步的或者无需同步的数据存在更新，不存在的插入
        /// 同步操作不使用此方法
        /// </summary>
        /// <param name="montylyIncomeCollection"></param>
        public void UpdateFinancialAssignMonthlyIncome(FinancialAssignMonthlyIncomeCollection montylyIncomeCollection)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.AppendLine("declare @synStatus as nvarchar(32)");
            montylyIncomeCollection.ForEach(d => {
                sqlBuilder.AppendFormat("set @synStatus = ({0})", GetSynStatusSql(d));
                sqlBuilder.AppendLine("if @synStatus is null");
                sqlBuilder.AppendLine("begin");
                sqlBuilder.AppendLine(InsertSql(d));
                sqlBuilder.AppendLine("end");
                sqlBuilder.AppendLine("else");
                sqlBuilder.AppendLine("begin");
                sqlBuilder.AppendLine("if @synStatus <> '" + Convert.ToInt32(SynchroStatusDefine.Synchronized).ToString() + "'");
                sqlBuilder.AppendLine("begin");
                sqlBuilder.AppendLine(UpdateSQL(d));
                sqlBuilder.AppendLine("end");
                sqlBuilder.AppendLine("end");
            });
            this.GetSqlContext().AppendSqlInContext(TSqlBuilder.Instance, sqlBuilder.ToString());
            this.GetDbContext().ExecuteNonQuerySqlInContext();
        }
        /// <summary>
        /// 向金蝶推送数据成功后，更新状态
        /// </summary>
        /// <param name="montylyIncomeCollection"></param>
        public void UpdateSyn(FinancialAssignMonthlyIncomeCollection montylyIncomeCollection)
        {
            montylyIncomeCollection.NullCheck("需要更新的数据是空");
            (montylyIncomeCollection.Count <= 0).TrueThrow("没有需要更新的数据");

            StringBuilder sqlBuilder = new StringBuilder();
            //将集合中的数据的同步状态置为已同步
            montylyIncomeCollection.ForEach(d =>
            {
                d.IsSyn = Convert.ToInt32(SynchroStatusDefine.Synchronized).ToString();
                sqlBuilder.AppendLine(UpdateSQL(d));
            });
            
            
            this.GetSqlContext().AppendSqlWithSperatorInContext(TSqlBuilder.Instance, sqlBuilder.ToString());
            this.GetDbContext().ExecuteNonQuerySqlInContext();
        }
        /// <summary>
        /// 返回插入SQL
        /// </summary>
        /// <param name="monthlyIncome"></param>
        /// <returns></returns>
        private string InsertSql(FinancialAssignMonthlyIncome monthlyIncome)
        {
            return ORMapping.GetInsertSql<FinancialAssignMonthlyIncome>(monthlyIncome, this.GetQueryMappingInfo(), TSqlBuilder.Instance);
        }
        /// <summary>
        /// 查询同步状态
        /// </summary>
        /// <param name="monthlyIncome"></param>
        /// <returns></returns>
        private string GetSynStatusSql(FinancialAssignMonthlyIncome monthlyIncome)
        {
            WhereSqlClauseBuilder whereBuilder = new WhereSqlClauseBuilder();
            whereBuilder.AppendItem("CheckYear", monthlyIncome.CheckYear)
                .AppendItem("CheckMonth", monthlyIncome.CheckMonth)
                .AppendItem("CampusID", monthlyIncome.CampusID)
                .AppendItem("CategoryType", monthlyIncome.CategoryType)
                .AppendItem("Catalog", monthlyIncome.Catalog);
            string sql = string.Format(@"select IsSyn from {0} where {1}"
                    , this.GetQueryMappingInfo().GetQueryTableName()
                    , whereBuilder.ToSqlString(TSqlBuilder.Instance));
            return sql;
        }
        
        /// <summary>
        /// 更新月课时收入汇总表，如果未同步或无需同步更新除同步状态和同步时间的其他字段
        /// 同步只更新同步状态，同步时间和最后更新时间
        /// </summary>
        /// <param name="monthlyIncome"></param>
        /// <returns></returns>
        private string UpdateSQL(FinancialAssignMonthlyIncome monthlyIncome)
        {
            UpdateSqlClauseBuilder updateBuilder = new UpdateSqlClauseBuilder();
            //如果同步状态是未同步或无需同步，更新其他字段
            if (monthlyIncome.IsSyn == Convert.ToInt32(SynchroStatusDefine.NotSynchronized).ToString() || monthlyIncome.IsSyn == Convert.ToInt32(SynchroStatusDefine.NoNeedSynchrozation).ToString())
            {
                updateBuilder.AppendItem("BranchID", monthlyIncome.BranchID)
                    .AppendItem("BranchName", monthlyIncome.BranchName)
                    .AppendItem("CampusID", monthlyIncome.CampusID)
                    .AppendItem("CategoryName", monthlyIncome.CategoryName)
                    .AppendItem("CatalogName", monthlyIncome.CatalogName)
                    .AppendItem("Amount", monthlyIncome.Amount)
                    .AppendItem("TaxAmount", monthlyIncome.TaxAmount)
                    .AppendItem("AllAmount", monthlyIncome.AllAmount)
                    .AppendItem("TaxRate", monthlyIncome.TaxRate)
                    .AppendItem("ModifyTime", "GETUTCDATE()", "=", true);
            }
            else if(monthlyIncome.IsSyn == Convert.ToInt32(SynchroStatusDefine.Synchronized).ToString())
            {
                //已同步，更新同步状态为已同步，更新同步时间
                updateBuilder.AppendItem("IsSyn", monthlyIncome.IsSyn)
                    .AppendItem("SynTime", "GETUTCDATE()", "=", true)
                    .AppendItem("ModifyTime", "GETUTCDATE()", "=", true);

            }

            WhereSqlClauseBuilder whereBuilder = new WhereSqlClauseBuilder();
            whereBuilder.AppendItem("CheckYear", monthlyIncome.CheckYear)
                .AppendItem("CheckMonth", monthlyIncome.CheckMonth)
                .AppendItem("CampusID", monthlyIncome.CampusID)
                .AppendItem("CategoryType", monthlyIncome.CategoryType)
                .AppendItem("Catalog", monthlyIncome.Catalog);

            string sql = string.Format(@"update {0} set {1} where {2}"
                ,this.GetQueryTableName()
                ,updateBuilder.ToSqlString(TSqlBuilder.Instance)
                ,whereBuilder.ToSqlString(TSqlBuilder.Instance));

            return sql;
        }

        /// <summary>
        /// 更新同步状态
        /// </summary>
        /// <param name="monthlyIncome"></param>
        /// <returns></returns>
        private string UpdateSynStatusSql(FinancialAssignMonthlyIncome monthlyIncome)
        {
            UpdateSqlClauseBuilder updateBuilder = new UpdateSqlClauseBuilder();
            updateBuilder.AppendItem("IsSyn", monthlyIncome.IsSyn)
                    .AppendItem("SynTime", "GETUTCDATE()", "=", true)
                    .AppendItem("ModifyTime", "GETUTCDATE()", "=", true);

            WhereSqlClauseBuilder whereBuilder = new WhereSqlClauseBuilder();
            whereBuilder.AppendItem("CheckYear", monthlyIncome.CheckYear)
                .AppendItem("CheckMonth", monthlyIncome.CheckMonth)
                .AppendItem("CampusID", monthlyIncome.CampusID)
                .AppendItem("CategoryType", monthlyIncome.CategoryType)
                .AppendItem("Catalog", monthlyIncome.Catalog);

            string sql = string.Format(@"update {0} set {1} where {2}"
                , this.GetQueryTableName()
                , updateBuilder.ToSqlString(TSqlBuilder.Instance)
                , whereBuilder.ToSqlString(TSqlBuilder.Instance));

            return sql;
        }

    }
}
