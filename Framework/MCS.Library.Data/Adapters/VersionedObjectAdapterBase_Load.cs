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
    public abstract partial class VersionedObjectAdapterBase<T, TCollection>
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
        /// <param name="whereAction"></param>
        /// <param name="timePoint">时间点</param>
        /// <param name="mappings"></param>
        /// <returns></returns>
        public bool Exists(Action<WhereSqlClauseBuilder> whereAction, DateTime timePoint, ORMappingItemCollection mappings = null)
        {
            whereAction.NullCheck("whereAction");

            if (mappings == null)
                mappings = this.GetQueryMappingInfo();

            WhereSqlClauseBuilder builder = new WhereSqlClauseBuilder();

            whereAction(builder);
            builder.AppendTenantCode(typeof(T));

            ConnectiveSqlClauseCollection timePointBuilder = VersionStrategyQuerySqlBuilder.Instance.TimePointToBuilder(timePoint);

            timePointBuilder.Add(builder);

            string sql = string.Format("SELECT TOP 1 * FROM {0}", GetQueryTableName(mappings, timePoint));

            if (builder.Count > 0)
                sql = sql + string.Format(" WHERE {0}", timePointBuilder.ToSqlString(TSqlBuilder.Instance));

            return (int)DbHelper.RunSqlReturnDS(sql, GetConnectionName()).Tables[0].Rows.Count > 0;
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

                    result = this.InnerLoadByBuilder(condition, orderByBuilder, timePoint, mappings);
                }
                else
                    result = new TCollection();
            });

            return result;
        }

        /// <summary>
        /// 根据外界的builder加载数据
        /// </summary>
        /// <param name="connectiveCondition"></param>
        /// <param name="timePoint">时间点</param>
        /// <returns></returns>
        public virtual TCollection LoadByBuilder(ConnectiveLoadingCondition connectiveCondition, DateTime timePoint)
        {
            connectiveCondition.NullCheck("connectiveCondition");
            connectiveCondition.ConnectiveBuilder.NullCheck("ConnectiveBuilder");

            TCollection result = default(TCollection);

            PerformanceMonitorHelper.GetDefaultMonitor().WriteExecutionDuration(string.Format("LoadByBuilder({0})", this.GetType().FullName), () =>
            {
                result = this.InnerLoadByBuilder(connectiveCondition.ConnectiveBuilder.ToSqlString(TSqlBuilder.Instance),
                    connectiveCondition.OrderByBuilder,
                    timePoint,
                    this.GetQueryMappingInfo());
            });

            return result;
        }

        /// <summary>
        /// 按照条件加载对象
        /// </summary>
        /// <param name="whereAction">筛选条件</param>
        /// <param name="timePoint">时间点</param>
        /// <param name="orderByAction">排序条件</param>
        /// <returns>对象集合</returns>
        public TCollection Load(Action<WhereSqlClauseBuilder> whereAction, DateTime timePoint, Action<OrderBySqlClauseBuilder> orderByAction = null)
        {
            return this.Load(new WhereLoadingCondition(whereAction, orderByAction), timePoint);
        }

        /// <summary>
        /// 按照条件加载对象
        /// </summary>
        /// <param name="loadingCondition">筛选和排序条件</param>
        /// <param name="timePoint">时间点</param>
        /// <param name="mappings"></param>
        /// <returns>对象集合</returns>
        public TCollection Load(WhereLoadingCondition loadingCondition, DateTime timePoint, ORMappingItemCollection mappings = null)
        {
            loadingCondition.NullCheck("loadingCondition");

            if (mappings == null)
                mappings = this.GetQueryMappingInfo();

            TCollection result = default(TCollection);

            PerformanceMonitorHelper.GetDefaultMonitor().WriteExecutionDuration(string.Format("Load({0})", this.GetType().FullName), () =>
            {
                loadingCondition.BuilderAction.NullCheck("whereAction");

                WhereSqlClauseBuilder builder = new WhereSqlClauseBuilder();

                loadingCondition.BuilderAction(builder);

                builder.AppendTenantCode(typeof(T));

                OrderBySqlClauseBuilder orderByBuilder = null;

                if (loadingCondition.OrderByAction != null)
                {
                    orderByBuilder = new OrderBySqlClauseBuilder();

                    loadingCondition.OrderByAction(orderByBuilder);
                }

                result = this.InnerLoadByBuilder(builder.ToSqlString(TSqlBuilder.Instance), orderByBuilder, timePoint, mappings);
            });

            return result;
        }

        /// <summary>
        /// 得到查询数据时的ORMapping信息
        /// </summary>
        /// <returns></returns>
        protected virtual ORMappingItemCollection GetQueryMappingInfo()
        {
            return this.GetMappingInfo();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="orderByBuilder"></param>
        /// <param name="timePoint"></param>
        /// <param name="mappings"></param>
        /// <returns></returns>
        protected TCollection InnerLoadByBuilder(string condition, OrderBySqlClauseBuilder orderByBuilder, DateTime timePoint, ORMappingItemCollection mappings)
        {
            string sql = GetLoadSqlByBuilder(condition, orderByBuilder, timePoint, mappings);

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

        private static string GetQueryTableName(ORMappingItemCollection mappings, DateTime timePoint)
        {
            string result = mappings.GetQueryTableName();

            if (timePoint != DateTime.MinValue)
                result = mappings.TableName;

            return result;
        }
    }
}
