using MCS.Library.Core;
using MCS.Library.Data.Builder;
using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace MCS.Library.Data.Adapters
{
    /// <summary>
    /// 带版本信息Adapter类的基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TCollection"></typeparam>
    public abstract partial class VersionedObjectAdapterBase<T, TCollection>
        where T : IVersionDataObjectWithoutID
        where TCollection : IList<T>, new()
    {
        /// <summary>
        /// 
        /// </summary>
        protected static readonly Dictionary<string, object> _DefaultContext = new Dictionary<string, object>();

        /// <summary>
		/// 将模式对象的修改提交到数据库
		/// </summary>
		/// <param name="data">对其进行更新的<typeparamref name="T"/>对象。</param>
		public void Update(T data)
        {
            data.NullCheck("obj");

            Dictionary<string, object> context = new Dictionary<string, object>();

            this.BeforeInnerUpdate(data, context);

            string sql = VersionStrategyUpdateSqlBuilder<T>.DefaultInstance.ToUpdateSql(data, this.GetMappingInfo());

            using (TransactionScope scope = TransactionScopeFactory.Create())
            {
                DateTime dt = (DateTime)DbHelper.RunSqlReturnScalar(sql, this.GetConnectionName());

                this.AfterInnerUpdate(data, context);

                DBTimePointActionContext.Current.TimePoint.IsMinValue(() => DBTimePointActionContext.Current.TimePoint = dt);

                scope.Complete();
            }
        }

        /// <summary>
        /// 删除数据（实际上是时间封口）
        /// </summary>
        /// <param name="data"></param>
        public void Delete(T data)
        {
            data.NullCheck("obj");

            Dictionary<string, object> context = new Dictionary<string, object>();

            this.BeforeInnerDelete(data, context);

            string sql = VersionStrategyUpdateSqlBuilder<T>.DefaultInstance.ToDeleteSql(data, this.GetMappingInfo());

            using (TransactionScope scope = TransactionScopeFactory.Create())
            {
                DateTime dt = (DateTime)DbHelper.RunSqlReturnScalar(sql, this.GetConnectionName());

                this.AfterInnerDelete(data, context);

                DBTimePointActionContext.Current.TimePoint.IsMinValue(() => DBTimePointActionContext.Current.TimePoint = dt);

                scope.Complete();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ownerKeyBuilder"></param>
        /// <param name="data"></param>
        public void UpdateCollection(IConnectiveSqlClause ownerKeyBuilder, IEnumerable<T> data)
        {
            ownerKeyBuilder.NullCheck("ownerKeyBuilder");
            data.NullCheck("objs");

            Dictionary<string, object> context = new Dictionary<string, object>();

            this.BeforeInnerUpdateCollection(data, context);

            string sql = VersionStrategyUpdateSqlBuilder<T>.DefaultInstance.ToUpdateCollectionSql(ownerKeyBuilder, this.GetMappingInfo(), data);

            using (TransactionScope scope = TransactionScopeFactory.Create())
            {
                DateTime dt = (DateTime)DbHelper.RunSqlReturnScalar(sql, this.GetConnectionName());

                this.AfterInnerUpdateCollection(data, context);

                DBTimePointActionContext.Current.TimePoint.IsMinValue(() => DBTimePointActionContext.Current.TimePoint = dt);

                scope.Complete();
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
		/// 在派生类中重写时， 获取映射信息的集合
		/// </summary>
		/// <returns><see cref="ORMappingItemCollection"/>，表示映射信息</returns>
		public virtual ORMappingItemCollection GetMappingInfo()
        {
            return ORMapping.GetMappingInfo(typeof(T));
        }

        /// <summary>
        /// 得到表名
        /// </summary>
        /// <returns></returns>
        protected virtual string GetTableName()
        {
            return this.GetMappingInfo().TableName;
        }

        /// <summary>
        /// 得到一个静态的上下文
        /// </summary>
        /// <returns></returns>
        protected Dictionary<string, object> GetFixedContext()
        {
            return _DefaultContext;
        }

        /// <summary>
        /// 更新单一对象前
        /// </summary>
        /// <param name="data"></param>
        /// <param name="context"></param>
        protected virtual void BeforeInnerUpdate(T data, Dictionary<string, object> context)
        {
        }

        /// <summary>
        /// 更新单一对象后
        /// </summary>
        /// <param name="data"></param>
        /// <param name="context"></param>
        protected virtual void AfterInnerUpdate(T data, Dictionary<string, object> context)
        {
        }

        /// <summary>
        /// 更新单一对象前
        /// </summary>
        /// <param name="data"></param>
        /// <param name="context"></param>
        protected virtual void BeforeInnerDelete(T data, Dictionary<string, object> context)
        {
        }

        /// <summary>
        /// 更新单一对象后
        /// </summary>
        /// <param name="data"></param>
        /// <param name="context"></param>
        protected virtual void AfterInnerDelete(T data, Dictionary<string, object> context)
        {
        }

        /// <summary>
        /// 更新集合对象前
        /// </summary>
        /// <param name="data"></param>
        /// <param name="context"></param>
        protected virtual void BeforeInnerUpdateCollection(IEnumerable<T> data, Dictionary<string, object> context)
        {
        }

        /// <summary>
        /// 更新集合对象后
        /// </summary>
        /// <param name="data"></param>
        /// <param name="context"></param>
        protected virtual void AfterInnerUpdateCollection(IEnumerable<T> data, Dictionary<string, object> context)
        {
        }

        /// <summary>
        /// 获取连接的名称
        /// </summary>
        /// <returns>表示连接名称的<see cref="string"/>。</returns>
        protected abstract string GetConnectionName();
    }
}
