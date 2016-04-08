using MCS.Library.Core;
using MCS.Library.Data.Builder;
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
    public abstract class VersionedObjectAdapterBase<T> where T : IVersionDataObjectWithoutID
    {
        /// <summary>
        /// 
        /// </summary>
        protected static readonly Dictionary<string, object> _DefaultContext = new Dictionary<string, object>();

        /// <summary>
		/// 将模式对象的修改提交到数据库
		/// </summary>
		/// <param name="obj">对其进行更新的<typeparamref name="T"/>对象。</param>
		public void Update(T obj)
        {
            obj.NullCheck("obj");

            //this.MergeExistsObjectInfo(obj);
            VersionStrategyUpdateSqlBuilder<T> builder = new VersionStrategyUpdateSqlBuilder<T>();

            string sql = builder.ToUpdateSql(obj, this.GetMappingInfo());

            DateTime dt = (DateTime)DbHelper.RunSqlReturnScalar(sql, this.GetConnectionName());

            DBTimePointActionContext.Current.TimePoint.IsMinValue(() => DBTimePointActionContext.Current.TimePoint = dt);
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
		protected virtual ORMappingItemCollection GetMappingInfo()
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
        /// 获取连接的名称
        /// </summary>
        /// <returns>表示连接名称的<see cref="string"/>。</returns>
        protected abstract string GetConnectionName();
    }
}
