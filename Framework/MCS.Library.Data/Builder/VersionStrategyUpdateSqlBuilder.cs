using MCS.Library.Core;
using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCS.Library.Data.Builder
{
    /// <summary>
    /// 与时间版本信息处理相关的Update和Insert语句的构造器
    /// </summary>
    public class VersionStrategyUpdateSqlBuilder<T> where T : IVersionDataObjectWithoutID
    {
        /// <summary>
        /// 
        /// </summary>
        public static readonly VersionStrategyUpdateSqlBuilder<T> DefaultInstance = new VersionStrategyUpdateSqlBuilder<T>();

        /// <summary>
        /// 生成Update和Insert混合的子句。先Update，然后通过RowCount判断是需要Insert
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="mapping"></param>
        /// <param name="addCurrentTimeVar">是否添加@currentTime变量</param>
        /// <param name="ignoreProperties">需要忽略的属性</param>
        /// <returns></returns>
		public string ToUpdateSql(T obj, ORMappingItemCollection mapping, bool addCurrentTimeVar = true, params string[] ignoreProperties)
        {
            return this.ToUpdateSql(obj, mapping,
                () => this.PrepareUpdateSql(obj, mapping, ignoreProperties),
                () => this.PrepareInsertSql(obj, mapping, ignoreProperties),
                addCurrentTimeVar,
                ignoreProperties);
        }

        /// <summary>
        /// 生成Update和Insert混合的子句。先Update，然后通过RowCount判断是需要Insert。其中Update子句可以自己构造
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="mapping"></param>
        /// <param name="getUpdateSql"></param>
        /// <param name="getInsertSql"></param>
        /// <param name="addCurrentTimeVar">是否添加@currentTime变量</param>
        /// <param name="ignoreProperties">需要忽略的属性</param>
        /// <returns></returns>
        public string ToUpdateSql(T obj, ORMappingItemCollection mapping, Func<string> getUpdateSql, Func<string> getInsertSql, bool addCurrentTimeVar = true, params string[] ignoreProperties)
        {
            obj.NullCheck("obj");
            mapping.NullCheck("mapping");
            getUpdateSql.NullCheck("getUpdateSql");
            getInsertSql.NullCheck("getInsertSql");

            return VersionStrategyUpdateSqlHelper.ConstructUpdateSql(null, (strB, context) =>
            {
                this.PrepareSingleObjectUpdateSql(strB, obj, mapping, true, getUpdateSql, getInsertSql, ignoreProperties);
            },
            addCurrentTimeVar);
        }

        /// <summary>
        /// 生成删除数据的SQL（时间封口）
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="mapping"></param>
        /// <param name="addCurrentTimeVar"></param>
        /// <param name="ignoreProperties"></param>
        /// <returns></returns>
        public string ToDeleteSql(T obj, ORMappingItemCollection mapping, bool addCurrentTimeVar = true, params string[] ignoreProperties)
        {
            return this.ToDeleteSql(obj, mapping,
                () => this.PrepareDeleteSql(obj, mapping, ignoreProperties),
                addCurrentTimeVar,
                ignoreProperties);
        }

        /// <summary>
        /// 生成删除数据的SQL（时间封口）
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="mapping"></param>
        /// <param name="getDeleteSql"></param>
        /// <param name="addCurrentTimeVar"></param>
        /// <param name="ignoreProperties"></param>
        /// <returns></returns>
        public string ToDeleteSql(T obj, ORMappingItemCollection mapping, Func<string> getDeleteSql, bool addCurrentTimeVar = true, params string[] ignoreProperties)
        {
            obj.NullCheck("obj");
            mapping.NullCheck("mapping");
            getDeleteSql.NullCheck("getDeleteSql");

            return VersionStrategyUpdateSqlHelper.ConstructUpdateSql(null, (strB, context) =>
            {
                strB.Append(getDeleteSql());
            },
            addCurrentTimeVar);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ownerKeyBuilder"></param>
        /// <param name="mapping"></param>
        /// <param name="newObjs"></param>
        /// <param name="addCurrentTimeVar">是否添加@currentTime变量</param>
        /// <param name="ignoreProperties">需要忽略的属性</param>
        /// <returns></returns>
        public string ToUpdateCollectionSql(IConnectiveSqlClause ownerKeyBuilder, ORMappingItemCollection mapping, IEnumerable<T> newObjs, bool addCurrentTimeVar = true, params string[] ignoreProperties)
        {
            return VersionStrategyUpdateSqlHelper.ConstructUpdateSql(null, (strB, context) =>
            {
                ConnectiveSqlClauseCollection existedKeys = new ConnectiveSqlClauseCollection(LogicOperatorDefine.Or);

                foreach (T obj in newObjs)
                {
                    if (obj != null)
                    {
                        if (strB.Length > 0)
                            strB.Append(TSqlBuilder.Instance.DBStatementSeperator);

                        this.PrepareSingleObjectUpdateSql(strB, obj, mapping, false,
                            () => this.PrepareUpdateCollectionItemSql(obj, mapping, ignoreProperties),
                            () => this.PrepareInsertSql(obj, mapping, ignoreProperties),
                            ignoreProperties);

                        WhereSqlClauseBuilder keyBuilder = GetExistedKeysBuilder(ownerKeyBuilder, obj, mapping);

                        existedKeys.Add(keyBuilder);
                    }
                }

                if (strB.Length > 0)
                    strB.Append(TSqlBuilder.Instance.DBStatementSeperator);

                strB.Append(this.PrepareUpdateNotMatchedCollectionItemSql(ownerKeyBuilder, existedKeys, mapping));
            },
            addCurrentTimeVar);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ownerKeyBuilder"></param>
        /// <param name="existedKeys"></param>
        /// <param name="mapping"></param>
        /// <returns></returns>
        protected virtual string PrepareUpdateNotMatchedCollectionItemSql(IConnectiveSqlClause ownerKeyBuilder, IConnectiveSqlClause existedKeys, ORMappingItemCollection mapping)
        {
            string innerSql = this.GetJoinedExistedCountSql(ownerKeyBuilder, existedKeys, mapping);

            string endTimeFieldName = GetPropertyFieldName("VersionEndTime", mapping);

            UpdateSqlClauseBuilder updateBuilder = new UpdateSqlClauseBuilder();

            updateBuilder.AppendItem(endTimeFieldName, DBTimePointActionContext.CurrentTimeTSqlVarName, SqlClauseBuilderBase.EqualTo, true);

            WhereSqlClauseBuilder veBuilder = new WhereSqlClauseBuilder();

            veBuilder.AppendItem(endTimeFieldName, DBTimePointActionContext.MaxVersionEndTime);
            veBuilder.AppendItem(GetPropertyFieldName("VersionStartTime", mapping), DBTimePointActionContext.CurrentTimeTSqlVarName, SqlClauseBuilderBase.NotEqualTo, true);

            ConnectiveSqlClauseCollection connective = new ConnectiveSqlClauseCollection(LogicOperatorDefine.And, ownerKeyBuilder, veBuilder);

            string sql = string.Format("UPDATE {0} SET {1} FROM {0} A WHERE {2} AND ({3}) = 0",
                        this.GetTableName(default(T), mapping),
                        updateBuilder.ToSqlString(TSqlBuilder.Instance),
                        connective.ToSqlString(TSqlBuilder.Instance),
                        innerSql);

            return sql;
        }

        private string GetJoinedExistedCountSql(IConnectiveSqlClause ownerKeyBuilder, IConnectiveSqlClause existedKeys, ORMappingItemCollection mapping)
        {
            HashSet<string> fields = MergeFields(ownerKeyBuilder.GetFields(), existedKeys.GetFields());

            WhereSqlClauseBuilder builder = new WhereSqlClauseBuilder();

            foreach (string field in fields)
                builder.AppendItem("A." + field, "B." + field, SqlClauseBuilderBase.EqualTo, true);

            string innerSql = this.GetInnerExistedObjsSql(ownerKeyBuilder, existedKeys, mapping);
            string sql = string.Format("SELECT COUNT(1) FROM ({0}) B WHERE {1}", innerSql, builder.ToSqlString(TSqlBuilder.Instance));

            return sql;
        }

        /// <summary>
        /// 得到一个已经存在的记录的临时表
        /// </summary>
        /// <param name="ownerKeyBuilder"></param>
        /// <param name="existedKeys"></param>
        /// <param name="mapping"></param>
        /// <returns></returns>
        private string GetInnerExistedObjsSql(IConnectiveSqlClause ownerKeyBuilder, IConnectiveSqlClause existedKeys, ORMappingItemCollection mapping)
        {
            ConnectiveSqlClauseCollection connective = new ConnectiveSqlClauseCollection(LogicOperatorDefine.And);

            connective.Add(ownerKeyBuilder);

            string endTimeFieldName = GetPropertyFieldName("VersionEndTime", mapping);

            WhereSqlClauseBuilder veBuilder = new WhereSqlClauseBuilder();

            veBuilder.AppendItem(endTimeFieldName, DBTimePointActionContext.MaxVersionEndTime);

            connective.Add(veBuilder);
            connective.Add(existedKeys);

            return string.Format("SELECT * FROM {0} WHERE {1}", this.GetTableName(default(T), mapping), connective.ToSqlString(TSqlBuilder.Instance));
        }

        private static WhereSqlClauseBuilder GetExistedKeysBuilder(IConnectiveSqlClause ownerKeyBuilder, T obj, ORMappingItemCollection mapping)
        {
            WhereSqlClauseBuilder keyBuilder = ORMapping.GetWhereSqlClauseBuilderByPrimaryKey(obj, mapping);

            if (obj.VersionStartTime == DateTime.MinValue)
            {
                keyBuilder.IfExists(GetPropertyFieldName("VersionStartTime", mapping),
                    item =>
                    {
                        item.IsExpression = true;
                        item.Data = DBTimePointActionContext.CurrentTimeTSqlVarName;
                    });
            }

            foreach (string ownerKey in ownerKeyBuilder.GetFields())
                keyBuilder.Remove(item => ((SqlClauseBuilderItemUW)item).DataField == ownerKey);

            return keyBuilder;
        }

        /// <summary>
        /// 更新集合对象中某些
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="mapping"></param>
        /// <param name="ignoreProperties">需要忽略的属性</param>
        /// <returns></returns>
        protected virtual string PrepareUpdateCollectionItemSql(T obj, ORMappingItemCollection mapping, string[] ignoreProperties)
        {
            IConnectiveSqlClause primaryKeyBuilder = ModifyTimeFieldsInWhereBuilder(obj, mapping, ORMapping.GetWhereSqlClauseBuilderByPrimaryKey(obj, mapping));

            WhereSqlClauseBuilder changedFieldsBuilder = ORMapping.GetWhereSqlClauseBuilderByChangedFields(obj, mapping, false);

            ConnectiveSqlClauseCollection connective = new ConnectiveSqlClauseCollection(LogicOperatorDefine.And, primaryKeyBuilder, changedFieldsBuilder);

            UpdateSqlClauseBuilder updateBuilder = this.PrepareUpdateSqlBuilder(obj, mapping, ignoreProperties);

            return string.Format("UPDATE {0} SET {1} WHERE {2}",
                    GetTableName(obj, mapping),
                    updateBuilder.ToSqlString(TSqlBuilder.Instance),
                    connective.ToSqlString(TSqlBuilder.Instance));
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
        /// <param name="ignoreProperties">需要忽略的属性</param>
        /// <returns></returns>
		public virtual InsertSqlClauseBuilder PrepareInsertSqlBuilder(T obj, ORMappingItemCollection mapping, string[] ignoreProperties)
        {
            InsertSqlClauseBuilder builder = ORMapping.GetInsertSqlClauseBuilder(obj, mapping, ignoreProperties);

            string startTimeFieldName = GetPropertyFieldName("VersionStartTime", mapping);
            string endTimeFieldName = GetPropertyFieldName("VersionEndTime", mapping);

            builder.Remove(b => ((SqlClauseBuilderItemIUW)b).DataField == startTimeFieldName);
            builder.Remove(b => ((SqlClauseBuilderItemIUW)b).DataField == endTimeFieldName);

            builder.AppendItem(startTimeFieldName, DBTimePointActionContext.CurrentTimeTSqlVarName, "=", true);
            builder.AppendItem(endTimeFieldName, DBTimePointActionContext.MaxVersionEndTime);

            return builder;
        }

        /// <summary>
        /// 准备更新的Sql Builder。这个方法会在builder中添加VersionEndTime的处理
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="mapping"></param>
        /// <param name="ignoreProperties">需要忽略的属性</param>
        /// <returns></returns>
		public virtual UpdateSqlClauseBuilder PrepareUpdateSqlBuilder(T obj, ORMappingItemCollection mapping, string[] ignoreProperties)
        {
            UpdateSqlClauseBuilder updateBuilder = new UpdateSqlClauseBuilder();

            updateBuilder.AppendItem(GetPropertyFieldName("VersionEndTime", mapping), DBTimePointActionContext.CurrentTimeTSqlVarName, "=", true);

            return updateBuilder;
        }

        /// <summary>
        /// 准备Where的子句，填写了VersionStartTime。在插入操作时，VST应该是MinValue。否则应该是最后一条记录的时间
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="mapping"></param>
        /// <param name="ignoreProperties">忽略的属性</param>
        /// <returns></returns>
		public virtual WhereSqlClauseBuilder PrepareWhereSqlBuilder(T obj, ORMappingItemCollection mapping, string[] ignoreProperties)
        {
            obj.NullCheck("obj");
            mapping.NullCheck("mapping");

            WhereSqlClauseBuilder primaryKeyBuilder = ORMapping.GetWhereSqlClauseBuilderByPrimaryKey(obj);

            ModifyTimeFieldsInWhereBuilder(obj, mapping, primaryKeyBuilder);

            return primaryKeyBuilder;
        }

        /// <summary>
        /// 准备Where的子句，填写了VersionStartTime。在插入操作时，VST应该是MinValue。否则应该是最后一条记录的时间
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="mapping"></param>
        /// <param name="builder"></param>
        /// <returns></returns>
        private IConnectiveSqlClause ModifyTimeFieldsInWhereBuilder(T obj, ORMappingItemCollection mapping, WhereSqlClauseBuilder builder)
        {
            string startTimeFieldName = GetPropertyFieldName("VersionStartTime", mapping);
            string endTimeFieldName = GetPropertyFieldName("VersionEndTime", mapping);

            if (builder.Exists(item => ((SqlClauseBuilderItemIUW)item).DataField == startTimeFieldName) == false)
                builder.AppendItem(startTimeFieldName, obj.VersionStartTime);

            builder.Remove(b => ((SqlClauseBuilderItemIUW)b).DataField == endTimeFieldName);
            builder.AppendItem(endTimeFieldName, DBTimePointActionContext.MaxVersionEndTime);

            return builder;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="mapping"></param>
        /// <param name="ignoreProperties">需要忽略的参数</param>
        /// <returns></returns>
		public virtual string PrepareInsertSql(T obj, ORMappingItemCollection mapping, string[] ignoreProperties)
        {
            InsertSqlClauseBuilder builder = this.PrepareInsertSqlBuilder(obj, mapping, ignoreProperties);

            return string.Format("INSERT INTO {0}{1}", this.GetTableName(obj, mapping), builder.ToSqlString(TSqlBuilder.Instance));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="mapping"></param>
        /// <param name="ignoreProperties">需要忽略的参数</param>
        /// <returns></returns>
		public virtual string PrepareUpdateSql(T obj, ORMappingItemCollection mapping, params string[] ignoreProperties)
        {
            IConnectiveSqlClause primaryKeyBuilder = this.PrepareWhereSqlBuilder(obj, mapping, ignoreProperties);
            UpdateSqlClauseBuilder updateBuilder = this.PrepareUpdateSqlBuilder(obj, mapping, ignoreProperties);

            return string.Format("UPDATE {0} SET {1} WHERE {2}",
                    GetTableName(obj, mapping),
                    updateBuilder.ToSqlString(TSqlBuilder.Instance),
                    primaryKeyBuilder.ToSqlString(TSqlBuilder.Instance));
        }

        /// <summary>
        /// 生成删除的Sql语句。默认等同于PrepareUpdateSql
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="mapping"></param>
        /// <param name="ignoreProperties"></param>
        /// <returns></returns>
        public virtual string PrepareDeleteSql(T obj, ORMappingItemCollection mapping, params string[] ignoreProperties)
        {
            return this.PrepareUpdateSql(obj, mapping, ignoreProperties);
        }

        private void PrepareSingleObjectUpdateSql(StringBuilder strB, T obj, ORMappingItemCollection mapping, bool raiseUnmatchedError, Func<string> getUpdateSql, Func<string> getInsertSql, string[] ignoreProperties)
        {
            if (obj.VersionStartTime != DateTime.MinValue)
            {
                strB.Append(getUpdateSql());

                strB.Append(TSqlBuilder.Instance.DBStatementSeperator);

                strB.AppendFormat("IF @@ROWCOUNT > 0\n");
                strB.AppendFormat("\t{0}\n", getInsertSql());

                if (raiseUnmatchedError)
                {
                    strB.AppendFormat("ELSE\n");
                    strB.AppendFormat("\tRAISERROR ({0}, 16, 1)",
                        TSqlBuilder.Instance.CheckUnicodeQuotationMark(GetErrorInfo(obj)));
                }
            }
            else
            {
                strB.Append(getInsertSql());
            }
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

        private static HashSet<string> MergeFields(params HashSet<string>[] fieldsSets)
        {
            HashSet<string> result = new HashSet<string>();

            foreach (HashSet<string> fields in fieldsSets)
            {
                foreach (string field in fields)
                {
                    if (result.Contains(field) == false)
                        result.Add(field);
                }
            }

            return result;
        }
    }
}
