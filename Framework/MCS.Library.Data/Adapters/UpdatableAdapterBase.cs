﻿using System;
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
    /// <summary>
    /// 带数据更新功能的DataAdapter的基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class UpdatableAdapterBase<T>
    {
        /// <summary>
        /// 默认的上下文对象
        /// </summary>
        protected static readonly Dictionary<string, object> _DefaultContext = new Dictionary<string, object>();

        /// <summary>
        /// 得到连接串的名称
        /// </summary>
        public string ConnectionName
        {
            get
            {
                return GetConnectionName();
            }
        }

        /// <summary>
        /// 从连接名称得到DbContext
        /// </summary>
        /// <returns></returns>
        public DbContext GetDbContext()
        {
            return DbContext.GetContext(this.GetConnectionName());
        }

        /// <summary>
        /// 得到SQL语句的上下文
        /// </summary>
        /// <returns></returns>
        public SqlContextItem GetSqlContext()
        {
            return SqlContext.GetContext(this.GetConnectionName());
        }

        /// <summary>
        /// 更新对象
        /// </summary>
        /// <param name="data"></param>
        public void Update(T data)
        {
            ExceptionHelper.FalseThrow<ArgumentNullException>(data != null, "data");

            PerformanceMonitorHelper.GetDefaultMonitor().WriteExecutionDuration(string.Format("Update({0})", this.GetType().FullName), () =>
            {
                Dictionary<string, object> context = new Dictionary<string, object>();

                this.BeforeInnerUpdate(data, context);

                using (TransactionScope scope = TransactionScopeFactory.Create())
                {
                    if (this.InnerUpdate(data, context) == 0)
                        this.InnerInsert(data, context);

                    this.AfterInnerUpdate(data, context);

                    scope.Complete();
                }
            });
        }

        /// <summary>
        /// 在当前的DbContext中记录更新操作，但是不执行。只有整个事务提交时才真正地执行
        /// 会执行BeforeInnerUpdateInContext和AfterUpdateInContext
        /// </summary>
        /// <param name="data"></param>
        public void UpdateInContext(T data)
        {
            ExceptionHelper.FalseThrow<ArgumentNullException>(data != null, "data");

            Dictionary<string, object> context = new Dictionary<string, object>();

            SqlContextItem sqlContext = this.GetSqlContext();

            this.BeforeInnerUpdateInContext(data, sqlContext, context);

            this.InnerUpdateInContext(data, sqlContext, context);

            sqlContext.AppendSqlWithSperatorInContext(TSqlBuilder.Instance, "IF @@ROWCOUNT = 0");

            sqlContext.AppendSqlInContext(TSqlBuilder.Instance, "\nBEGIN\n");
            this.InnerInsertInContext(data, sqlContext, context);
            sqlContext.AppendSqlInContext(TSqlBuilder.Instance, "\nEND\n");

            this.AfterInnerUpdateInContext(data, sqlContext, context);
        }

        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="data"></param>
        public virtual void Delete(T data)
        {
            ExceptionHelper.FalseThrow<ArgumentNullException>(data != null, "data");

            PerformanceMonitorHelper.GetDefaultMonitor().WriteExecutionDuration(string.Format("Delete In Context({0})", this.GetType().FullName), () =>
            {
                Dictionary<string, object> context = new Dictionary<string, object>();

                BeforeInnerDelete(data, context);

                using (TransactionScope scope = TransactionScopeFactory.Create())
                {
                    InnerDelete(data, context);

                    AfterInnerDelete(data, context);

                    scope.Complete();
                }
            });
        }

        /// <summary>
        /// 在上下文中添加删除对象的脚本
        /// </summary>
        /// <param name="data"></param>
        public virtual void DeleteInContext(T data)
        {
            ExceptionHelper.FalseThrow<ArgumentNullException>(data != null, "data");

            SqlContextItem sqlContext = this.GetSqlContext();

            using (DbContext dbContext = this.GetDbContext())
            {
                Dictionary<string, object> context = new Dictionary<string, object>();

                this.BeforeInnerDeleteInContext(data, sqlContext, context);

                this.InnerDeleteInContext(data, sqlContext, context);

                this.AfterInnerDeleteInContext(data, sqlContext, context);
            }
        }

        /// <summary>
        /// 按照条件删除
        /// </summary>
        /// <param name="whereAction"></param>
        public virtual void Delete(Action<WhereSqlClauseBuilder> whereAction)
        {
            whereAction.NullCheck("whereAction");

            PerformanceMonitorHelper.GetDefaultMonitor().WriteExecutionDuration(string.Format("Delete(whereAction-{0})", this.GetType().FullName), () =>
            {
                WhereSqlClauseBuilder builder = new WhereSqlClauseBuilder();

                whereAction(builder);

                Dictionary<string, object> context = new Dictionary<string, object>();

                this.BeforeInnerDelete(builder, context);

                string sql = this.GetDeleteSql(builder, context);

                if (sql.IsNotEmpty())
                {
                    using (TransactionScope scope = TransactionScopeFactory.Create())
                    {
                        DbHelper.RunSql(db => db.ExecuteNonQuery(CommandType.Text, sql), this.GetConnectionName());

                        this.AfterInnerDelete(builder, context);

                        scope.Complete();
                    }
                }
            });
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
        /// 删除所有数据，用于测试
        /// </summary>
        [Conditional("DEBUG")]
        public virtual void ClearAll()
        {
            string sql = "DELETE FROM " + this.GetTableName();
            DbHelper.RunSql(sql, GetConnectionName());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="context"></param>
        protected virtual void BeforeInnerDelete(T data, Dictionary<string, object> context)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="context"></param>
        protected virtual void BeforeInnerDelete(WhereSqlClauseBuilder builder, Dictionary<string, object> context)
        {
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
        /// <param name="context"></param>
        protected virtual void AfterInnerDelete(T data, Dictionary<string, object> context)
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
        /// <param name="sqlContext"></param>
        /// <param name="context"></param>
        protected virtual void AfterInnerDeleteInContext(WhereSqlClauseBuilder builder, SqlContextItem sqlContext, Dictionary<string, object> context)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="context"></param>
        protected virtual void AfterInnerDelete(WhereSqlClauseBuilder builder, Dictionary<string, object> context)
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
        /// <returns></returns>
        protected abstract string GetConnectionName();


        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="context"></param>
        protected virtual void BeforeInnerUpdate(T data, Dictionary<string, object> context)
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
        /// <param name="context"></param>
        protected virtual void AfterInnerUpdate(T data, Dictionary<string, object> context)
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
        /// <param name="context"></param>
        /// <returns></returns>
        protected virtual int InnerDelete(T data, Dictionary<string, object> context)
        {
            ORMappingItemCollection mappings = GetMappingInfo(context);

            string sql = this.GetDeleteSql(data, mappings, context);

            int result = 0;

            DbHelper.RunSql(db => result = db.ExecuteNonQuery(CommandType.Text, sql), this.GetConnectionName());

            return result;
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
        /// <param name="context"></param>
        /// <returns></returns>
        protected virtual ORMappingItemCollection GetMappingInfo(Dictionary<string, object> context)
        {
            return ORMapping.GetMappingInfo<T>();
        }

        /// <summary>
        /// 得到表名
        /// </summary>
        /// <returns></returns>
        protected virtual string GetTableName()
        {
            return this.GetMappingInfo(_DefaultContext).TableName;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        protected virtual int InnerUpdate(T data, Dictionary<string, object> context)
        {
            ORMappingItemCollection mappings = GetMappingInfo(context);

            string sql = this.GetUpdateSql(data, mappings, context);

            int result = 0;

            DbHelper.RunSql(db => result = db.ExecuteNonQuery(CommandType.Text, sql), this.GetConnectionName());

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="sqlContext"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        protected virtual string InnerUpdateInContext(T data, SqlContextItem sqlContext, Dictionary<string, object> context)
        {
            ORMappingItemCollection mappings = GetMappingInfo(context);

            string sql = this.GetUpdateSql(data, mappings, context);

            sqlContext.AppendSqlInContext(TSqlBuilder.Instance, sql);

            return sql;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="sqlContext"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        protected virtual string InnerInsertInContext(T data, SqlContextItem sqlContext, Dictionary<string, object> context)
        {
            ORMappingItemCollection mappings = GetMappingInfo(context);

            string sql = this.GetInsertSql(data, mappings, context);

            sqlContext.AppendSqlInContext(TSqlBuilder.Instance, sql);

            return sql;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="mappings"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        protected virtual string GetUpdateSql(T data, ORMappingItemCollection mappings, Dictionary<string, object> context)
        {
            return ORMapping.GetUpdateSql(data, mappings, TSqlBuilder.Instance);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="context"></param>
        protected virtual void InnerInsert(T data, Dictionary<string, object> context)
        {
            ORMappingItemCollection mappings = GetMappingInfo(context);

            string sql = this.GetInsertSql(data, mappings, context);

            DbHelper.RunSql(db => db.ExecuteNonQuery(CommandType.Text, sql), this.GetConnectionName());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="mappings"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        protected virtual string GetInsertSql(T data, ORMappingItemCollection mappings, Dictionary<string, object> context)
        {
            return ORMapping.GetInsertSql(data, mappings, TSqlBuilder.Instance);
        }

        /// <summary>
        /// 得到对象默认的删除语句
        /// </summary>
        /// <param name="data"></param>
        /// <param name="mappings"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        protected virtual string GetDeleteSql(T data, ORMappingItemCollection mappings, Dictionary<string, object> context)
        {
            WhereSqlClauseBuilder builder = ORMapping.GetWhereSqlClauseBuilderByPrimaryKey(data, mappings);

            ExceptionHelper.FalseThrow(builder.Count > 0, "必须为对象{0}指定关键字", typeof(T));

            return string.Format("DELETE {0} WHERE {1}", this.GetTableName(), builder.ToSqlString(TSqlBuilder.Instance));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        protected virtual string GetDeleteSql(WhereSqlClauseBuilder builder, Dictionary<string, object> context)
        {
            string sql = string.Empty;

            if (builder.Count > 0)
            {
                sql = string.Format("DELETE {0} WHERE {1}",
                        this.GetTableName(),
                        builder.ToSqlString(TSqlBuilder.Instance));
            }

            return sql;
        }
    }
}
