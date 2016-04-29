using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Text;
using System.Transactions;
using MCS.Library.Core;
using MCS.Library.Data.Builder;
using MCS.Library.Data.Mapping;

namespace MCS.Library.Data.Adapters
{
    public abstract partial class UpdatableAdapterBase<T>
    {
        /// <summary>
        /// 在当前的DbContext中记录更新操作，但是不执行。只有整个事务提交时才真正地执行
        /// 会执行BeforeInnerUpdateInContext和AfterUpdateInContext
        /// </summary>
        /// <param name="data"></param>
        /// <param name="ignoreProperties">需要忽略的属性</param>
        public void UpdateInContext(T data, params string[] ignoreProperties)
        {
            this.DoUpdateInContext(data,
                (obj, sqlContext, context) =>
                {
                    this.BeforeInnerUpdateInContext(obj, sqlContext, context);
                    this.InnerUpdateInContext(obj, sqlContext, context, ignoreProperties);
                },
                (obj, sqlContext, context) => this.InnerInsertInContext(data, sqlContext, context, ignoreProperties),
                (obj, sqlContext, context) => this.AfterInnerUpdateInContext(data, sqlContext, context)
            );
        }

        /// <summary>
        /// 在上下文中添加删除对象的脚本
        /// </summary>
        /// <param name="data"></param>
        public virtual void DeleteInContext(T data)
        {
            this.DoDeleteInContext(data,
                (obj, sqlContext, context) =>
                {
                    this.BeforeInnerDeleteInContext(data, sqlContext, context);
                    this.InnerDeleteInContext(data, sqlContext, context);
                },
                (obj, sqlContext, context) => this.AfterInnerDeleteInContext(data, sqlContext, context)
            );
        }

        /// <summary>
        /// 在上下文中添加删除对象的脚本
        /// </summary>
        /// <param name="whereAction"></param>
        public virtual void DeleteInContext(Action<WhereSqlClauseBuilder> whereAction)
        {
            whereAction.NullCheck("whereAction");

            WhereSqlClauseBuilder builder = new WhereSqlClauseBuilder();

            whereAction(builder);

            Dictionary<string, object> context = new Dictionary<string, object>();

            SqlContextItem sqlContext = this.GetSqlContext();

            this.BeforeInnerDeleteInContext(builder, sqlContext, context);

            string sql = this.GetDeleteSql(builder, context);

            if (sql.IsNotEmpty())
            {
                sqlContext.AppendSqlWithSperatorInContext(TSqlBuilder.Instance, sql);

                this.AfterInnerDeleteInContext(builder, context);
            }
        }

        /// <summary>
        /// 执行UpdateInContext的时序，操作由应用定义
        /// </summary>
        /// <param name="data"></param>
        /// <param name="updateAction"></param>
        /// <param name="insertAction"></param>
        /// <param name="afterAction"></param>
        protected void DoUpdateInContext(T data,
            Action<T, SqlContextItem, Dictionary<string, object>> updateAction,
            Action<T, SqlContextItem, Dictionary<string, object>> insertAction,
            Action<T, SqlContextItem, Dictionary<string, object>> afterAction = null)
        {
            ExceptionHelper.FalseThrow<ArgumentNullException>(data != null, "data");

            Dictionary<string, object> context = new Dictionary<string, object>();

            SqlContextItem sqlContext = this.GetSqlContext();

            updateAction.IsNotNull(action => action(data, sqlContext, context));

            sqlContext.AppendSqlWithSperatorInContext(TSqlBuilder.Instance, "IF @@ROWCOUNT = 0");

            sqlContext.AppendSqlInContext(TSqlBuilder.Instance, "\nBEGIN\n");
            insertAction.IsNotNull(action => action(data, sqlContext, context));
            sqlContext.AppendSqlInContext(TSqlBuilder.Instance, "\nEND\n");

            afterAction.IsNotNull(action => action(data, sqlContext, context));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="deletetAction"></param>
        /// <param name="afterAction"></param>
        protected void DoDeleteInContext(T data,
            Action<T, SqlContextItem, Dictionary<string, object>> deletetAction,
            Action<T, SqlContextItem, Dictionary<string, object>> afterAction = null)
        {
            ExceptionHelper.FalseThrow<ArgumentNullException>(data != null, "data");

            SqlContextItem sqlContext = this.GetSqlContext();

            Dictionary<string, object> context = new Dictionary<string, object>();

            deletetAction.IsNotNull(action => action(data, sqlContext, context));

            afterAction.IsNotNull(action => action(data, sqlContext, context));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="sqlContext"></param>
        /// <param name="context"></param>
        protected virtual void BeforeInnerDeleteInContext(WhereSqlClauseBuilder builder, SqlContextItem sqlContext, Dictionary<string, object> context)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="sqlContext"></param>
        /// <param name="context"></param>
        protected virtual void BeforeInnerDeleteInContext(T data, SqlContextItem sqlContext, Dictionary<string, object> context)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="sqlContext"></param>
        /// <param name="context"></param>
        protected virtual void AfterInnerDeleteInContext(T data, SqlContextItem sqlContext, Dictionary<string, object> context)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="context"></param>
        protected virtual void AfterInnerDeleteInContext(WhereSqlClauseBuilder builder, Dictionary<string, object> context)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="sqlContext"></param>
        /// <param name="context"></param>
        protected virtual void AfterInnerDeleteInContext(WhereSqlClauseBuilder builder, SqlContextItem sqlContext, Dictionary<string, object> context)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="sqlContext"></param>
        /// <param name="context"></param>
        protected virtual void BeforeInnerUpdateInContext(T data, SqlContextItem sqlContext, Dictionary<string, object> context)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="sqlContext"></param>
        /// <param name="context"></param>
        protected virtual void AfterInnerUpdateInContext(T data, SqlContextItem sqlContext, Dictionary<string, object> context)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="sqlContext"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        protected virtual string InnerDeleteInContext(T data, SqlContextItem sqlContext, Dictionary<string, object> context)
        {
            ORMappingItemCollection mappings = GetMappingInfo(context);

            string sql = this.GetDeleteSql(data, mappings, context);

            sqlContext.AppendSqlWithSperatorInContext(TSqlBuilder.Instance, sql);

            return sql;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="sqlContext"></param>
        /// <param name="context"></param>
        /// <param name="ignoreProperties">需要忽略的属性</param>
        /// <returns></returns>
        protected virtual string InnerUpdateInContext(T data, SqlContextItem sqlContext, Dictionary<string, object> context, string[] ignoreProperties)
        {
            ORMappingItemCollection mappings = GetMappingInfo(context);

            string sql = this.GetUpdateSql(data, mappings, context, ignoreProperties);

            sqlContext.AppendSqlInContext(TSqlBuilder.Instance, sql);

            return sql;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="sqlContext"></param>
        /// <param name="context"></param>
        /// <param name="ignoreProperties">需要忽略的属性</param>
        /// <returns></returns>
        protected virtual string InnerInsertInContext(T data, SqlContextItem sqlContext, Dictionary<string, object> context, string [] ignoreProperties)
        {
            ORMappingItemCollection mappings = GetMappingInfo(context);

            string sql = this.GetInsertSql(data, mappings, context, ignoreProperties);

            sqlContext.AppendSqlInContext(TSqlBuilder.Instance, sql);

            return sql;
        }
    }
}
