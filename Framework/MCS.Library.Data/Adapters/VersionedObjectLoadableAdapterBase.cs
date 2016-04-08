using MCS.Library.Core;
using MCS.Library.Data.Builder;
using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Data.Adapters
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TCollection"></typeparam>
    public abstract class VersionedObjectLoadableAdapterBase<T, TCollection> : VersionedObjectAdapterBase<T>
        where T : IVersionDataObjectWithoutID
        where TCollection : IList<T>, IEnumerable<T>, new()
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual T CreateNewData()
        {
            return (T)TypeCreator.CreateInstance(typeof(T));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public virtual T CreateNewData(DataRow row)
        {
            return CreateNewData();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inCondition"></param>
        /// <param name="timePoint"></param>
        /// <param name="mappings"></param>
        /// <returns></returns>
        public TCollection LoadByInBuilder(InLoadingCondition inCondition, DateTime timePoint, ORMappingItemCollection mappings = null)
        {
            inCondition.NullCheck("inCondition");
            inCondition.BuilderAction.NullCheck("BuilderAction");

            if (mappings == null)
                mappings = this.GetQueryMappingInfo();

            TCollection result = default(TCollection);

            PerformanceMonitorHelper.GetDefaultMonitor().WriteExecutionDuration(string.Format("LoadByInBuilder({0})", this.GetType().FullName), () =>
            {
                InSqlClauseBuilder inBuilder = new InSqlClauseBuilder(inCondition.DataField);

                inCondition.BuilderAction(inBuilder);

                string condition = string.Empty;

                if (inBuilder.IsEmpty == false)
                {
                    ConnectiveSqlClauseCollection builder = new ConnectiveSqlClauseCollection(LogicOperatorDefine.And,
                        inBuilder, new WhereSqlClauseBuilder().AppendTenantCode(typeof(T)));

                    condition = builder.ToSqlString(TSqlBuilder.Instance);

                    OrderBySqlClauseBuilder orderByBuilder = null;

                    if (inCondition.OrderByAction != null)
                    {
                        orderByBuilder = new OrderBySqlClauseBuilder();

                        inCondition.OrderByAction(orderByBuilder);
                    }

                    result = this.InnerLoadByBuilder(condition, orderByBuilder, mappings, timePoint);
                }
                else
                    result = new TCollection();
            });

            return result;
        }

        /// <summary>
        /// 得到查询数据时的ORMapping信息
        /// </summary>
        /// <returns></returns>
        protected virtual ORMappingItemCollection GetQueryMappingInfo()
        {
            return base.GetMappingInfo();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="orderByBuilder"></param>
        /// <param name="mappings"></param>
        /// <param name="timePoint"></param>
        /// <returns></returns>
        protected TCollection InnerLoadByBuilder(string condition, OrderBySqlClauseBuilder orderByBuilder, ORMappingItemCollection mappings, DateTime timePoint)
        {
            string sql = GetLoadSqlByBuilder(condition, orderByBuilder, mappings, timePoint);

            TCollection result = QueryData(mappings, sql);

            this.AfterLoad(result);

            return result;
        }

        /// <summary>
        /// 加载数据之后
        /// </summary>
        /// <param name="data"></param>
        protected virtual void AfterLoad(TCollection data)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mapping"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        protected TCollection QueryData(ORMappingItemCollection mapping, string sql)
        {
            DataTable table = DbHelper.RunSqlReturnDS(sql, GetConnectionName()).Tables[0];

            return this.DataTableToCollection(mapping, table);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        /// <typeparam name="TDataCollection"></typeparam>
        /// <param name="mapping"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        protected TDataCollection QueryData<TData, TDataCollection>(ORMappingItemCollection mapping, string sql)
            where TData : IVersionDataObjectWithoutID, new()
            where TDataCollection : IList<TData>, IEnumerable<TData>, new()
        {
            TDataCollection result = new TDataCollection();

            DataTable table = DbHelper.RunSqlReturnDS(sql, GetConnectionName()).Tables[0];

            foreach (DataRow row in table.Rows)
            {
                TData data = new TData();

                ORMapping.DataRowToObject(row, mapping, data);

                result.Add(data);
            }

            return result;
        }

        private static string GetLoadSqlByBuilder(string condition, OrderBySqlClauseBuilder orderByBuilder, ORMappingItemCollection mappings, DateTime timePoint)
        {
            string sql = string.Format("SELECT * FROM {0} WHERE ", mappings.TableName);

            ConnectiveSqlClauseCollection timePointBuilder = VersionStrategyQuerySqlBuilder.Instance.TimePointToBuilder(timePoint);

            if (condition.IsNotEmpty())
                sql = sql + condition;

            sql += " AND " + timePointBuilder.ToSqlString(TSqlBuilder.Instance);

            if (orderByBuilder != null)
                sql = sql + string.Format(" ORDER BY {0}", orderByBuilder.ToSqlString(TSqlBuilder.Instance));

            return sql;
        }

        private TCollection DataTableToCollection(ORMappingItemCollection mapping, DataTable table)
        {
            TCollection result = new TCollection();

            foreach (DataRow row in table.Rows)
            {
                T data = this.CreateNewData(row);

                ORMapping.DataRowToObject(row, mapping, data);

                if (data is ILoadableDataEntity)
                    ((ILoadableDataEntity)data).Loaded = true;

                result.Add(data);
            }

            return result;
        }
    }
}
