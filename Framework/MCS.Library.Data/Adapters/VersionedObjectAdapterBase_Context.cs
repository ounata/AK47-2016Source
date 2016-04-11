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
        public void UpdateInContext(T data)
        {
            data.NullCheck("data");

            VersionStrategyUpdateSqlBuilder<T> builder = new VersionStrategyUpdateSqlBuilder<T>();

            string sql = builder.ToUpdateSql(data, this.GetMappingInfo(), false);

            this.GetSqlContext().AppendSqlWithSperatorInContext(TSqlBuilder.Instance, sql);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ownerKeyBuilder"></param>
        /// <param name="objs"></param>
        public void UpdateCollectionInContext(IConnectiveSqlClause ownerKeyBuilder, IEnumerable<T> objs)
        {
            ownerKeyBuilder.NullCheck("ownerKeyBuilder");
            objs.NullCheck("objs");

            VersionStrategyUpdateSqlBuilder<T> builder = new VersionStrategyUpdateSqlBuilder<T>();

            string sql = builder.ToUpdateCollectionSql(ownerKeyBuilder, this.GetMappingInfo(), objs, false);

            this.GetSqlContext().AppendSqlWithSperatorInContext(TSqlBuilder.Instance, sql);
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
