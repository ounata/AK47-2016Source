using MCS.Library.Core;
using MCS.Library.Data.Builder;
using System;
using System.Collections.Generic;
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
        /// <param name="data"></param>
        /// <param name="ignoreProperties">需要忽略的属性</param>
        public void UpdateInContext(T data, params string[] ignoreProperties)
        {
            data.NullCheck("data");

            Dictionary<string, object> context = new Dictionary<string, object>();

            SqlContextItem sqlContext = this.GetSqlContext();

            this.BeforeInnerUpdateInContext(data, sqlContext, context);

            string sql = VersionStrategyUpdateSqlBuilder<T>.DefaultInstance.ToUpdateSql(data, this.GetMappingInfo(), false, ignoreProperties);

            sqlContext.AppendSqlWithSperatorInContext(TSqlBuilder.Instance, sql);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ownerKeyBuilder"></param>
        /// <param name="data"></param>
        /// <param name="ignoreProperties">需要忽略的属性</param>
        public void UpdateCollectionInContext(IConnectiveSqlClause ownerKeyBuilder, IEnumerable<T> data, params string[] ignoreProperties)
        {
            ownerKeyBuilder.NullCheck("ownerKeyBuilder");
            data.NullCheck("objs");

            Dictionary<string, object> context = new Dictionary<string, object>();

            SqlContextItem sqlContext = this.GetSqlContext();

            this.BeforeInnerUpdateCollectionInContext(data, sqlContext, context);

            string sql = VersionStrategyUpdateSqlBuilder<T>.DefaultInstance.ToUpdateCollectionSql(ownerKeyBuilder, this.GetMappingInfo(), data, false, ignoreProperties);

            sqlContext.AppendSqlWithSperatorInContext(TSqlBuilder.Instance, sql);
        }

        /// <summary>
        /// 在上下文中更新单一对象前
        /// </summary>
        /// <param name="data"></param>
        /// <param name="sqlContext"></param>
        /// <param name="context"></param>
        protected virtual void BeforeInnerUpdateInContext(T data, SqlContextItem sqlContext, Dictionary<string, object> context)
        {
        }

        /// <summary>
        /// 在上下文中更新集合对象前
        /// </summary>
        /// <param name="data"></param>
        /// <param name="sqlContext"></param>
        /// <param name="context"></param>
        protected virtual void BeforeInnerUpdateCollectionInContext(IEnumerable<T> data, SqlContextItem sqlContext, Dictionary<string, object> context)
        {
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
        /// 从连接名称得到DbContext
        /// </summary>
        /// <returns></returns>
        public DbContext GetDbContext()
        {
            return DbContext.GetContext(this.GetConnectionName());
        }
    }
}
