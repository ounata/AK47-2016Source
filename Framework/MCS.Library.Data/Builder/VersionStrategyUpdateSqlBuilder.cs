using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MCS.Library.Core;
using MCS.Library.Data.Mapping;

namespace MCS.Library.Data.Builder
{
    /// <summary>
    /// 与时间版本信息处理相关的Update和Insert语句的构造器
    /// </summary>
    public class VersionStrategyUpdateSqlBuilder<T> where T : IVersionDataObjectWithoutID
    {
        //public static readonly VersionStrategyUpdateSqlBuilder<T> LocalTimeInstance = new VersionStrategyUpdateSqlBuilder<T>();

        /// <summary>
        /// 生成Update和Insert混合的子句。先Update，然后通过RowCount判断是需要Insert
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="mapping"></param>
        /// <returns></returns>
		public string ToUpdateSql(T obj, ORMappingItemCollection mapping)
        {
            return this.ToUpdateSql(obj, mapping,
                () => this.PrepareUpdateSql(obj, mapping),
                () => this.PrepareInsertSql(obj, mapping));
        }

        /// <summary>
        /// 生成Update和Insert混合的子句。先Update，然后通过RowCount判断是需要Insert。其中Update子句可以自己构造
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="mapping"></param>
        /// <param name="getUpdateSql"></param>
        /// <param name="getInsertSql"></param>
        /// <returns></returns>
        public string ToUpdateSql(T obj, ORMappingItemCollection mapping, Func<string> getUpdateSql, Func<string> getInsertSql)
        {
            obj.NullCheck("obj");
            mapping.NullCheck("mapping");
            getUpdateSql.NullCheck("getUpdateSql");
            getInsertSql.NullCheck("getInsertSql");

            return VersionStrategyUpdateSqlHelper.ConstructUpdateSql(null, (strB, context) =>
            {
                if (obj.VersionStartTime != DateTime.MinValue)
                {
                    strB.Append(getUpdateSql());

                    strB.Append(TSqlBuilder.Instance.DBStatementSeperator);

                    strB.AppendFormat("IF @@ROWCOUNT > 0\n");
                    strB.AppendFormat("\t{0}\n", this.PrepareInsertSql(obj, mapping));
                    strB.AppendFormat("ELSE\n");
                    strB.AppendFormat("\tRAISERROR ({0}, 16, 1)",
                        TSqlBuilder.Instance.CheckUnicodeQuotationMark(GetErrorInfo(obj)));
                }
                else
                {
                    strB.Append(this.PrepareInsertSql(obj, mapping));
                }
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="mapping"></param>
        /// <returns></returns>
		protected virtual string GetTableName(T obj, ORMappingItemCollection mapping)
        {
            return mapping.TableName;
        }

        /// <summary>
        /// 生成带VersionStartTime和VersionEndTime的InsertBuilder
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="mapping"></param>
        /// <returns></returns>
		protected virtual InsertSqlClauseBuilder PrepareInsertSqlBuilder(T obj, ORMappingItemCollection mapping)
        {
            InsertSqlClauseBuilder builder = ORMapping.GetInsertSqlClauseBuilder(obj, mapping);

            string startTimeFieldName = GetPropertyFieldName("VersionStartTime", mapping);
            string endTimeFieldName = GetPropertyFieldName("VersionEndTime", mapping);

            builder.Remove(b => ((SqlClauseBuilderItemIUW)b).DataField == startTimeFieldName);
            builder.Remove(b => ((SqlClauseBuilderItemIUW)b).DataField == endTimeFieldName);

            builder.AppendItem(startTimeFieldName, "@currentTime", "=", true);
            builder.AppendItem(endTimeFieldName, DBTimePointActionContext.MaxVersionEndTime);

            return builder;
        }

        /// <summary>
        /// 准备更新的Sql Builder。这个方法会在builder中添加VersionEndTime的处理
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="mapping"></param>
        /// <returns></returns>
		protected virtual UpdateSqlClauseBuilder PrepareUpdateSqlBuilder(T obj, ORMappingItemCollection mapping)
        {
            UpdateSqlClauseBuilder updateBuilder = new UpdateSqlClauseBuilder();

            updateBuilder.AppendItem(GetPropertyFieldName("VersionEndTime", mapping), "@currentTime", "=", true);

            return updateBuilder;
        }

        /// <summary>
        /// 准备Where的子句，填写了VersionStartTime。在插入操作时，VST应该是MinValue。否则应该是最后一条记录的时间
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="mapping"></param>
        /// <returns></returns>
		protected virtual WhereSqlClauseBuilder PrepareWhereSqlBuilder(T obj, ORMappingItemCollection mapping)
        {
            WhereSqlClauseBuilder primaryKeyBuilder = ORMapping.GetWhereSqlClauseBuilderByPrimaryKey(obj);

            string startTimeFieldName = GetPropertyFieldName("VersionStartTime", mapping);
            string endTimeFieldName = GetPropertyFieldName("VersionEndTime", mapping);

            if (primaryKeyBuilder.Exists(item => ((SqlClauseBuilderItemIUW)item).DataField == startTimeFieldName) == false)
                primaryKeyBuilder.AppendItem(startTimeFieldName, obj.VersionStartTime);

            primaryKeyBuilder.Remove(b => ((SqlClauseBuilderItemIUW)b).DataField == endTimeFieldName);
            primaryKeyBuilder.AppendItem(endTimeFieldName, DBTimePointActionContext.MaxVersionEndTime);

            return primaryKeyBuilder;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="mapping"></param>
        /// <returns></returns>
		protected virtual string PrepareInsertSql(T obj, ORMappingItemCollection mapping)
        {
            InsertSqlClauseBuilder builder = this.PrepareInsertSqlBuilder(obj, mapping);

            return string.Format("INSERT INTO {0}{1}", this.GetTableName(obj, mapping), builder.ToSqlString(TSqlBuilder.Instance));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="mapping"></param>
        /// <returns></returns>
		protected virtual string PrepareUpdateSql(T obj, ORMappingItemCollection mapping)
        {
            WhereSqlClauseBuilder primaryKeyBuilder = this.PrepareWhereSqlBuilder(obj, mapping);
            UpdateSqlClauseBuilder updateBuilder = this.PrepareUpdateSqlBuilder(obj, mapping);

            return string.Format("UPDATE {0} SET {1} WHERE {2}",
                    GetTableName(obj, mapping),
                    updateBuilder.ToSqlString(TSqlBuilder.Instance),
                    primaryKeyBuilder.ToSqlString(TSqlBuilder.Instance));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="mapping"></param>
        /// <returns></returns>
		protected static string GetPropertyFieldName(string propertyName, ORMappingItemCollection mapping)
        {
            ORMappingItem item = mapping[propertyName];

            (item != null).FalseThrow("不能在{0}的OR Mapping信息中找到属性{1}", mapping.TableName, propertyName);

            return item.DataFieldName;
        }

        private static string GetErrorInfo(T obj)
        {
            string result = "对象的版本不是最新的，不能更新";

            if (obj is IVersionDataObject)
                string.Format("对象\"{0}\"的版本不是最新的，不能更新", ((IVersionDataObject)obj).ID);

            return result;
        }
    }
}
