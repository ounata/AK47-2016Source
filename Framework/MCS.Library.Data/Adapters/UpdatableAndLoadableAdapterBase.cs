﻿using MCS.Library.Core;
using MCS.Library.Data.Builder;
using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace MCS.Library.Data.Adapters
{
    /// <summary>
    /// 带读取和更新功能Adapter的基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TCollection"></typeparam>
    public abstract partial class UpdatableAndLoadableAdapterBase<T, TCollection> : UpdatableAdapterBase<T>
        where TCollection : IList<T>, new()
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
        /// <param name="mappings"></param>
        /// <returns></returns>
        public bool Exists(Action<WhereSqlClauseBuilder> whereAction, ORMappingItemCollection mappings = null)
        {
            whereAction.NullCheck("whereAction");

            if (mappings == null)
                mappings = this.GetQueryMappingInfo();

            WhereSqlClauseBuilder builder = new WhereSqlClauseBuilder();

            whereAction(builder);
            builder.AppendTenantCode(typeof(T));

            string sql = string.Format("SELECT TOP 1 * FROM {0}", mappings.GetQueryTableName());

            if (builder.Count > 0)
                sql = sql + string.Format(" WHERE {0}", builder.ToSqlString(TSqlBuilder.Instance));

            return (int)DbHelper.RunSqlReturnDS(sql, GetConnectionName()).Tables[0].Rows.Count > 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inCondition"></param>
        /// <param name="mappings"></param>
        /// <returns></returns>
        public TCollection LoadByInBuilder(InLoadingCondition inCondition, ORMappingItemCollection mappings = null)
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

                    result = InnerLoadByBuilder(condition, orderByBuilder, mappings);
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
        /// <returns></returns>
        public virtual TCollection LoadByBuilder(ConnectiveLoadingCondition connectiveCondition)
        {
            connectiveCondition.NullCheck("connectiveCondition");
            connectiveCondition.ConnectiveBuilder.NullCheck("ConnectiveBuilder");

            TCollection result = default(TCollection);

            PerformanceMonitorHelper.GetDefaultMonitor().WriteExecutionDuration(string.Format("LoadByBuilder({0})", this.GetType().FullName), () =>
            {
                result = this.InnerLoadByBuilder(connectiveCondition.ConnectiveBuilder.ToSqlString(TSqlBuilder.Instance),
                    connectiveCondition.OrderByBuilder,
                    this.GetQueryMappingInfo());
            });

            return result;
        }

        /// <summary>
        /// 按照条件加载对象
        /// </summary>
        /// <param name="whereAction">筛选条件</param>
        /// <param name="orderByAction">排序条件</param>
        /// <returns>对象集合</returns>
        public TCollection Load(Action<WhereSqlClauseBuilder> whereAction, Action<OrderBySqlClauseBuilder> orderByAction = null)
        {
            return Load(new WhereLoadingCondition(whereAction, orderByAction));
        }

        /// <summary>
        /// 按照条件加载对象
        /// </summary>
        /// <param name="loadingCondition">筛选和排序条件</param>
        /// <param name="mappings"></param>
        /// <returns>对象集合</returns>
        public TCollection Load(WhereLoadingCondition loadingCondition, ORMappingItemCollection mappings = null)
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

                result = this.InnerLoadByBuilder(builder.ToSqlString(TSqlBuilder.Instance), orderByBuilder, mappings);
            });

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="orderByBuilder"></param>
        /// <param name="mappings"></param>
        /// <returns></returns>
        protected TCollection InnerLoadByBuilder(string condition, OrderBySqlClauseBuilder orderByBuilder, ORMappingItemCollection mappings)
        {
            string sql = QueryInContextBuilder<T, TCollection>.Instance.GetLoadSqlByBuilder(condition, orderByBuilder, mappings.GetQueryTableName());

            TCollection result = QueryData(sql);

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
        /// <typeparam name="TData"></typeparam>
        /// <typeparam name="TDataCollection"></typeparam>
        /// <param name="mapping"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        protected TDataCollection QueryData<TData, TDataCollection>(ORMappingItemCollection mapping, string sql)
            where TData : new()
            where TDataCollection : IList<TData>, new()
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        protected TCollection QueryData(string sql)
        {
            ORMappingItemCollection mapping = this.GetQueryMappingInfo();

            return QueryData(mapping, sql);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mapping"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        protected TCollection QueryData(ORMappingItemCollection mapping, string sql)
        {
            return QueryInContextBuilder<T, TCollection>.Instance.QueryData(mapping, sql, this.GetConnectionName(), (row) => this.CreateNewData(row));
        }

        /// <summary>
        /// 得到查询数据时的ORMapping信息
        /// </summary>
        /// <returns></returns>
        public virtual ORMappingItemCollection GetQueryMappingInfo()
        {
            return base.GetMappingInfo(this.GetFixedContext());
        }

        /// <summary>
        /// 得到查询所使用的表名
        /// </summary>
        /// <returns></returns>
        protected virtual string GetQueryTableName()
        {
            return this.GetQueryMappingInfo().GetQueryTableName();
        }

        /// <summary>
        /// 得到一个静态的上下文
        /// </summary>
        /// <returns></returns>
        protected Dictionary<string, object> GetFixedContext()
        {
            return _DefaultContext;
        }
    }
}
