#region
// -------------------------------------------------
// Assembly	：	DeluxeWorks.Library.Data
// FileName	：	SqlCaluseBuilderBase.cs
// Remark	：	Sql子句构造器的抽象基类
// -------------------------------------------------
// VERSION  	AUTHOR		DATE			CONTENT
// 1.0		    张铁军	    20070430		创建
// -------------------------------------------------
#endregion
using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using MCS.Library.Core;
using MCS.Library.Data.DataObjects;
using System.Data;

namespace MCS.Library.Data.Builder
{
    /// <summary>
    /// Sql子句构造器的抽象基类
    /// </summary>
    [Serializable]
    public abstract class SqlClauseBuilderBase : EditableDataObjectCollectionBase<SqlClauseBuilderItemBase>
    {
        internal static List<IConnectiveSqlClause> StaticClauses = new List<IConnectiveSqlClause>();

        #region 条件项的运算符常量定义
        /// <summary>
        /// 等号
        /// </summary>
        internal const string EqualTo = "=";

        /// <summary>
        /// 大于等于
        /// </summary>
        internal const string GreaterThanOrEqualTo = ">=";

        /// <summary>
        /// 大于
        /// </summary>
        internal const string GreaterThan = ">";

        /// <summary>
        /// 小于等于
        /// </summary>
        internal const string LessThanOrEqualTo = "<=";

        /// <summary>
        /// 小于
        /// </summary>
        internal const string LessThan = "<";

        /// <summary>
        /// 不等于
        /// </summary>
        internal const string NotEqualTo = "<>";

        /// <summary>
        /// LIKE运算符
        /// </summary>
        internal const string Like = "LIKE";

        /// <summary>
        /// IS运算符
        /// </summary>
        internal const string Is = "IS";

        /// <summary>
        /// IS NOT运算符
        /// </summary>
        internal const string IsNot = "IS NOT";

        /// <summary>
        /// IN运算符
        /// </summary>
        internal const string In = "IN";
        #endregion 条件项的运算符常量定义

        /// <summary>
        /// 抽象方法，将所有的构造项生成一个SQL
        /// </summary>
        /// <param name="sqlBuilder"></param>
        /// <returns></returns>
        public abstract string ToSqlString(ISqlBuilder sqlBuilder);
    }

    /// <summary>
    /// In操作的Sql语句构造器
    /// </summary>
    [Serializable]
    public class InSqlClauseBuilder : SqlClauseBuilderBase, IConnectiveSqlClause
    {
        private string _DataField = string.Empty;
        private bool _IsNotIn = false;

        /// <summary>
        /// 构造方法
        /// </summary>
        public InSqlClauseBuilder()
        {
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="isNotIn">是否组合成NOT IN子句</param>
        public InSqlClauseBuilder(bool isNotIn)
        {
            this._IsNotIn = isNotIn;
        }

        /// <summary>
        /// 数据字段
        /// </summary>
        /// <param name="dataField"></param>
        public InSqlClauseBuilder(string dataField)
        {
            this._DataField = dataField;
        }

        /// <summary>
        /// 是否组合成NOT IN子句
        /// </summary>
        public bool IsNotIn
        {
            get
            {
                return this._IsNotIn;
            }
            set
            {
                this._IsNotIn = value;
            }
        }

        /// <summary>
        /// 对应的数据字段，如果不为空，那么构造的时候，会自动带上字段名
        /// </summary>
        public string DataField
        {
            get { return this._DataField; }
            set { this._DataField = value; }
        }

        /// <summary>
        /// 添加一个构造项
        /// </summary>SqlCaluseBuilderBase
        /// <typeparam name="T">数据的类型</typeparam>
        /// <param name="data">In操作的数据</param>
        public InSqlClauseBuilder AppendItem<T>(params T[] data)
        {
            return AppendItem<T>(false, data);
        }

        /// <summary>
        /// 添加一个构造项
        /// </summary>
        /// <typeparam name="T">数据的类型</typeparam>
        /// <param name="data">In操作的数据</param>
        /// <param name="isExpression">是否是表达式</param>
        public InSqlClauseBuilder AppendItem<T>(bool isExpression, params T[] data)
        {
            ExceptionHelper.FalseThrow<ArgumentNullException>(data != null, "data");

            for (int i = 0; i < data.Length; i++)
            {
                SqlCaluseBuilderItemInOperator item = new SqlCaluseBuilderItemInOperator();

                item.IsExpression = isExpression;
                item.Data = data[i];

                List.Add(item);
            }

            return this;
        }

        /// <summary>
        /// 生成Sql语句（格式为：数据1,数据2，...）
        /// </summary>
        /// <param name="builder">Sql语句构造器</param>
        /// <returns>生成Sql语句（格式为：数据1,数据2，...）</returns>
        public override string ToSqlString(ISqlBuilder builder)
        {
            string result = string.Empty;

            if (this._DataField.IsNotEmpty())
                result = ToSqlStringWithInOperator(builder);
            else
                result = InnerToSqlString(builder);

            return result;
        }

        /// <summary>
        /// 生成Sql语句，加上In操作符。如果没有数据，In操作符也不生成
        /// </summary>
        /// <param name="builder">Sql语句构造器</param>
        /// <returns>构造的In语句</returns>
        public string ToSqlStringWithInOperator(ISqlBuilder builder)
        {
            string result = string.Empty;
            string inClause = InnerToSqlString(builder);

            if (string.IsNullOrEmpty(inClause) == false)
            {
                string prefix = string.Empty;

                if (this.IsNotIn)
                    prefix = "NOT ";

                if (this._DataField.IsNotEmpty())
                    result = string.Format("{0} {1}IN ({2})", this._DataField, prefix, inClause);
                else
                    result = string.Format("{0}IN ({1})", prefix, inClause);
            }

            return result;
        }

        private string InnerToSqlString(ISqlBuilder builder)
        {
            StringBuilder strB = new StringBuilder(256);

            for (int i = 0; i < List.Count; i++)
            {
                if (strB.Length > 0)
                    strB.Append(", ");

                strB.Append(((SqlCaluseBuilderItemInOperator)List[i]).GetDataDesp(builder));
            }

            return strB.ToString();
        }

        /// <summary>
        /// 得到相关的字段
        /// </summary>
        /// <returns></returns>
        public HashSet<string> GetFields()
        {
            HashSet<string> result = new HashSet<string>();

            if (this.DataField.IsNotEmpty())
                result.Add(this.DataField);

            return result;
        }

        /// <summary>
        /// 得到子构造对象
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IConnectiveSqlClause> GetSubConnectiveSqlClause()
        {
            return SqlClauseBuilderBase.StaticClauses;
        }

        /// <summary>
        /// 是否为空
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                return this.Count == 0;
            }
        }
    }

    /// <summary>
    /// Insert、Update、Where语句构造器的基类
    /// </summary>
    [Serializable]
    public abstract class SqlClauseBuilderIUW<TBuilder> : SqlClauseBuilderBase where TBuilder : SqlClauseBuilderIUW<TBuilder>
    {
        /// <summary>
        /// 添加一个构造项
        /// </summary>SqlCaluseBuilderBase
        /// <typeparam name="T">数据的类型</typeparam>
        /// <returns>返回这个Builder自己，便于编写连续AppendItem语句，例如b.AppendItem(...).AppendItem(...)</returns>
        /// <param name="dataField">Sql语句中的字段名</param>
        /// <param name="data">操作的数据</param>
        public virtual TBuilder AppendItem<T>(string dataField, T data)
        {
            return AppendItem<T>(dataField, data, SqlClauseBuilderBase.EqualTo);
        }

        /// <summary>
        /// 添加一个构造项
        /// </summary>
        /// <typeparam name="T">数据的类型</typeparam>
        /// <returns>返回这个Builder自己，便于编写连续AppendItem语句，例如b.AppendItem(...).AppendItem(...)</returns>
        /// <param name="dataField">Sql语句中的字段名</param>
        /// <param name="data">操作的数据</param>
        /// <param name="op">操作运算符</param>
        public virtual TBuilder AppendItem<T>(string dataField, T data, string op)
        {
            return AppendItem<T>(dataField, data, op, false);
        }

        /// <summary>
        /// 添加一个构造项
        /// </summary>
        /// <typeparam name="T">数据的类型</typeparam>
        /// <returns>返回这个Builder自己，便于编写连续AppendItem语句，例如b.AppendItem(...).AppendItem(...)</returns>
        /// <param name="dataField">Sql语句中的字段名</param>
        /// <param name="data">操作的数据</param>
        /// <param name="op">操作运算符</param>
        /// <param name="isExpression">操作的数据是否是表达式</param>
        public virtual TBuilder AppendItem<T>(string dataField, T data, string op, bool isExpression)
        {
            SqlClauseBuilderItemIUW item = (SqlClauseBuilderItemIUW)CreateBuilderItem();

            item.DataField = dataField;
            item.IsExpression = isExpression;
            item.Operation = op;
            item.Data = data;

            List.Add(item);

            return (TBuilder)this;
        }

        /// <summary>
        /// 从DataRow生成一组Items
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public TBuilder AppendItems(DataRow row)
        {
            if (row != null)
            {
                foreach (DataColumn column in row.Table.Columns)
                    this.AppendItem(column.ColumnName, row[column.ColumnName]);
            }

            return (TBuilder)this;
        }

        /// <summary>
        /// 判断是否已经包含了某个数据字段（大小写相关）
        /// </summary>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public bool ContainsDataField(string fieldName)
        {
            fieldName.CheckStringIsNullOrEmpty("fieldName");

            return this.Exists(item => ((SqlClauseBuilderItemIUW)item).DataField == fieldName);
        }

        /// <summary>
        /// 创建一个BuilderItem。派生类可以重载，创建自己的BuilderItem。
        /// </summary>
        /// <returns></returns>
        protected virtual SqlClauseBuilderItemBase CreateBuilderItem()
        {
            return new SqlClauseBuilderItemIUW();
        }

        /// <summary>
        /// 得到相关的字段
        /// </summary>
        /// <returns></returns>
        public HashSet<string> GetFields()
        {
            HashSet<string> result = new HashSet<string>();

            foreach (SqlClauseBuilderItemIUW item in this)
            {
                if (result.Contains(item.DataField) == false)
                    result.Add(item.DataField);
            }

            return result;
        }
    }

    /// <summary>
    /// UPDATE和WHERE语句构造器的基类
    /// </summary>
    [Serializable]
    public abstract class SqlClauseBuilderUW<TBuilder> : SqlClauseBuilderIUW<TBuilder> where TBuilder : SqlClauseBuilderUW<TBuilder>
    {
        /// <summary>
        /// 创建SqlClauseBuilderItemUW
        /// </summary>
        /// <returns></returns>
        protected override SqlClauseBuilderItemBase CreateBuilderItem()
        {
            return new SqlClauseBuilderItemUW();
        }

        /// <summary>
        /// 添加一个构造项
        /// </summary>
        /// <typeparam name="T">数据的类型</typeparam>
        /// <returns>返回这个Builder自己，便于编写连续AppendItem语句，例如b.AppendItem(...).AppendItem(...)</returns>
        /// <param name="dataField">Sql语句中的字段名</param>
        /// <param name="data">操作的数据</param>
        /// <param name="op">操作运算符</param>
        /// <param name="template">模板的样式</param>
        public virtual TBuilder AppendItem<T>(string dataField, T data, string op, string template)
        {
            return AppendItem<T>(dataField, data, op, template, false);
        }

        /// <summary>
        /// 添加一个构造项
        /// </summary>
        /// <typeparam name="T">数据的类型</typeparam>
        /// <returns>返回这个Builder自己，便于编写连续AppendItem语句，例如b.AppendItem(...).AppendItem(...)</returns>
        /// <param name="dataField">Sql语句中的字段名</param>
        /// <param name="data">操作的数据</param>
        /// <param name="op">操作运算符</param>
        /// <param name="template">模板的样式</param>
        /// <param name="isExpression">操作的数据是否是表达式</param>
        public virtual TBuilder AppendItem<T>(string dataField, T data, string op, string template, bool isExpression)
        {
            SqlClauseBuilderItemUW item = (SqlClauseBuilderItemUW)CreateBuilderItem();

            item.DataField = dataField;
            item.IsExpression = isExpression;
            item.Operation = op;
            item.Data = data;
            item.Template = template;

            List.Add(item);

            return (TBuilder)this;
        }
    }

    /// <summary>
    /// 提供一组字段和值的集合，帮助生成UPDATE语句的SET部分
    /// </summary>
    [Serializable]
    public class UpdateSqlClauseBuilder : SqlClauseBuilderUW<UpdateSqlClauseBuilder>
    {
        /// <summary>
        /// 生成Update语句的SET部分（不包括SET）
        /// </summary>
        /// <param name="sqlBuilder">Sql语句构造器</param>
        /// <returns>构造的Update子句(不含update部分)</returns>
        public override string ToSqlString(ISqlBuilder sqlBuilder)
        {
            ExceptionHelper.FalseThrow<ArgumentNullException>(sqlBuilder != null, "sqlBuilder");

            StringBuilder strB = new StringBuilder(256);

            foreach (SqlClauseBuilderItemUW item in List)
            {
                if (strB.Length > 0)
                    strB.Append(", ");

                item.ToSqlString(strB, sqlBuilder);
            }

            return strB.ToString();
        }
    }

    /// <summary>
    /// 提供一组字段和值的集合，帮助生成INSERT语句的字段名称和VALUES部分
    /// </summary>
    [Serializable]
    public class InsertSqlClauseBuilder : SqlClauseBuilderIUW<InsertSqlClauseBuilder>
    {
        /// <summary>
        /// 生成INSERT语句的字段名称和VALUES部分
        /// </summary>
        /// <param name="sqlBuilder">Sql语句构造器</param>
        /// <returns>构造的Insert子句(不含insert部分)</returns>
        public override string ToSqlString(ISqlBuilder sqlBuilder)
        {
            ExceptionHelper.FalseThrow<ArgumentNullException>(sqlBuilder != null, "sqlBuilder");

            StringBuilder strBFields = new StringBuilder(256);
            StringBuilder strBValues = new StringBuilder(256);

            foreach (SqlClauseBuilderItemIUW item in List)
            {
                if (item.Data != null && item.Data != DBNull.Value)
                {
                    if (strBFields.Length > 0)
                        strBFields.Append(", ");

                    strBFields.Append(item.DataField);

                    if (strBValues.Length > 0)
                        strBValues.Append(", ");

                    strBValues.Append(item.GetDataDesp(sqlBuilder));
                }
            }

            string result = string.Empty;

            if (strBValues.Length > 0)
                result = string.Format("({0}) VALUES({1})", strBFields.ToString(), strBValues.ToString());

            return result;
        }
    }

    /// <summary>
    /// 可连接的Sql子句的接口
    /// </summary>
    public interface IConnectiveSqlClause
    {
        /// <summary>
        /// 子句是否为空
        /// </summary>
        bool IsEmpty
        {
            get;
        }

        /// <summary>
        /// 得到相关的字段
        /// </summary>
        /// <returns></returns>
        HashSet<string> GetFields();

        /// <summary>
        /// 得到子构造对象
        /// </summary>
        /// <returns></returns>
        IEnumerable<IConnectiveSqlClause> GetSubConnectiveSqlClause();

        /// <summary>
        /// 生成Sql子句
        /// </summary>
        /// <param name="sqlBuilder"></param>
        /// <returns></returns>
        string ToSqlString(ISqlBuilder sqlBuilder);
    }


    /// <summary>
    /// 提供一组字段和值的集合，帮助生成WHERE语句
    /// </summary>
    [Serializable]
    public class WhereSqlClauseBuilder : SqlClauseBuilderUW<WhereSqlClauseBuilder>, IConnectiveSqlClause
    {
        private LogicOperatorDefine logicOperator = LogicOperatorDefine.And;

        /// <summary>
        /// 构造方法
        /// </summary>
        public WhereSqlClauseBuilder()
            : base()
        {
        }

        /// <summary>
        /// 构造方法，可以指定生成条件表达式时的逻辑运算符
        /// </summary>
        /// <param name="lod">逻辑运算符</param>
        public WhereSqlClauseBuilder(LogicOperatorDefine lod)
            : base()
        {
            this.logicOperator = lod;
        }

        /// <summary>
        /// 条件表达式之间的逻辑运算符
        /// </summary>
        public LogicOperatorDefine LogicOperator
        {
            get
            {
                return this.logicOperator;
            }
            set
            {
                this.logicOperator = value;
            }
        }

        /// <summary>
        /// 判断是否不存在任何条件表达式
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                return this.Count == 0;
            }
        }

        /// <summary>
        /// 帮助生成WHERE语句
        /// </summary>
        /// <param name="sqlBuilder">语句构造器</param>
        /// <returns>构造的Where子句(不含where部分)</returns>
        public override string ToSqlString(ISqlBuilder sqlBuilder)
        {
            ExceptionHelper.FalseThrow<ArgumentNullException>(sqlBuilder != null, "sqlBuilder");

            StringBuilder strB = new StringBuilder(256);

            foreach (SqlClauseBuilderItemUW item in List)
            {
                if (strB.Length > 0)
                    strB.AppendFormat(" {0} ", EnumItemDescriptionAttribute.GetAttribute(this.logicOperator).ShortName);

                item.ToSqlString(strB, sqlBuilder);
            }
            return strB.ToString();
        }

        /// <summary>
        /// 得到子构造对象
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IConnectiveSqlClause> GetSubConnectiveSqlClause()
        {
            return SqlClauseBuilderBase.StaticClauses;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataField"></param>
        /// <param name="action"></param>
        public void IfExists(string dataField, Action<SqlClauseBuilderItemIUW> action)
        {
            if (action != null)
            {
                SqlClauseBuilderItemIUW builderItem = (SqlClauseBuilderItemIUW)this.Find(item => ((SqlClauseBuilderItemIUW)item).DataField == dataField);

                if (builderItem != null)
                    action(builderItem);
            }
        }
    }

    /// <summary>
    /// 提供一组字段和值的集合，帮助生成ORDER BY语句的字段排序部分
    /// </summary>
    [Serializable]
    public class OrderBySqlClauseBuilder : SqlClauseBuilderBase, IEnumerable<IOrderByRequestItem>
    {
        /// <summary>
        /// 添加一个构造项
        /// </summary>
        /// <returns>返回这个Builder自己，便于编写连续AppendItem语句，例如b.AppendItem(...).AppendItem(...)</returns>
        /// <param name="dataField">操作的数据</param>
        /// <param name="sortDirection">排序方式</param>
        public OrderBySqlClauseBuilder AppendItem(string dataField, FieldSortDirection sortDirection)
        {
            SqlClauseBuilderItemOrd item = new SqlClauseBuilderItemOrd();

            item.DataField = dataField;
            item.SortDirection = sortDirection;
            List.Add(item);

            return this;
        }

        /// <summary>
        /// 帮助生成ORDER BY语句的字段排序部分
        /// </summary>
        /// <param name="sqlBuilder">Sql语句构造器</param>
        /// <returns>构造出的Order By子句</returns>
        public override string ToSqlString(ISqlBuilder sqlBuilder)
        {
            ExceptionHelper.FalseThrow<ArgumentNullException>(sqlBuilder != null, "sqlBuilder");

            StringBuilder strB = new StringBuilder(256);

            foreach (SqlClauseBuilderItemOrd item in List)
            {
                if (strB.Length > 0)
                    strB.Append(", ");

                item.ToSqlString(strB, sqlBuilder);
            }

            return strB.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public OrderByRequestItem[] ToOrderByRequestItems()
        {
            List<OrderByRequestItem> result = new List<OrderByRequestItem>();

            foreach (SqlClauseBuilderItemOrd item in this.List)
            {
                OrderByRequestItem resultItem = new OrderByRequestItem();

                resultItem.CopyFrom(item);

                result.Add(resultItem);
            }

            return result.ToArray();
        }

        /// <summary>
        /// 创建一个包装好的OrderBySqlClauseBuilder
        /// </summary>
        /// <param name="sourceBuilder"></param>
        /// <returns></returns>
        public static OrderBySqlClauseBuilder CreateWrapperBuilder(IEnumerable<IOrderByRequestItem> sourceBuilder)
        {
            OrderBySqlClauseBuilder result = sourceBuilder as OrderBySqlClauseBuilder;

            if (result == null)
            {
                result = new OrderBySqlClauseBuilder();

                foreach (IOrderByRequestItem sourceItem in sourceBuilder)
                {
                    result.Add(new SqlClauseBuilderItemOrd(sourceItem));
                }
            }

            return result;
        }

        IEnumerator<IOrderByRequestItem> IEnumerable<IOrderByRequestItem>.GetEnumerator()
        {
            foreach (IOrderByRequestItem item in this)
                yield return item;
        }
    }

    /// <summary>
    /// 可连接的Sql子句的集合，可以统一生成Sql语句，各语句之间使用括号隔离
    /// </summary>
    [Serializable]
    public class ConnectiveSqlClauseCollection : CollectionBase, IConnectiveSqlClause
    {
        private LogicOperatorDefine logicOperator = LogicOperatorDefine.And;

        /// <summary>
        /// 构造方法
        /// </summary>
        public ConnectiveSqlClauseCollection()
            : base()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sqlClause"></param>
        public ConnectiveSqlClauseCollection(params IConnectiveSqlClause[] sqlClause)
            : base()
        {
            if (sqlClause != null)
            {
                for (int i = 0; i < sqlClause.Length; i++)
                    this.Add(sqlClause[i]);
            }
        }

        /// <summary>
        /// 构造方法，可以指定生成条件表达式时的逻辑运算符
        /// </summary>
        /// <param name="lo">逻辑运算符</param>
        public ConnectiveSqlClauseCollection(LogicOperatorDefine lo)
            : base()
        {
            this.logicOperator = lo;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lo"></param>
        /// <param name="sqlClause"></param>
        public ConnectiveSqlClauseCollection(LogicOperatorDefine lo, params IConnectiveSqlClause[] sqlClause)
            : base()
        {
            this.logicOperator = lo;

            if (sqlClause != null)
            {
                for (int i = 0; i < sqlClause.Length; i++)
                    this.Add(sqlClause[i]);
            }
        }

        /// <summary>
        /// 增加一个可连接的Sql子句
        /// </summary>
        /// <returns>返回自身的对象，便于连续地执行Add，例如c.Add(...).Add(...)</returns>
        /// <param name="clause">Sql子句</param>
        public ConnectiveSqlClauseCollection Add(IConnectiveSqlClause clause)
        {
            List.Add(clause);

            return this;
        }

        /// <summary>
        /// 获取或设置一个可连接的Sql子句
        /// </summary>
        /// <param name="index">Sql子句</param>
        /// <returns>可连接的Sql子句</returns>
        public IConnectiveSqlClause this[int index]
        {
            get
            {
                return (IConnectiveSqlClause)List[index];
            }
            set
            {
                List[index] = value;
            }
        }

        /// <summary>
        /// 将所有的构造项生成一个SQL
        /// </summary>
        /// <param name="sqlBuilder">Sql语句的构造器</param>
        /// <returns></returns>
        public string ToSqlString(ISqlBuilder sqlBuilder)
        {
            StringBuilder strB = new StringBuilder(256);

            for (int i = 0; i < this.Count; i++)
            {
                IConnectiveSqlClause clause = this[i];

                if (clause.IsEmpty == false)
                {
                    if (strB.Length > 0)
                        strB.AppendFormat(" {0} ", EnumItemDescriptionAttribute.GetAttribute(this.logicOperator).ShortName);

                    strB.AppendFormat("({0})", clause.ToSqlString(sqlBuilder));
                }
            }

            return strB.ToString();
        }

        #region Protected
        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="value"></param>
        protected override void OnInsert(int index, object value)
        {
            ExceptionHelper.FalseThrow<ArgumentNullException>(value != null, "value");
            base.OnInsert(index, value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        protected override void OnSet(int index, object oldValue, object newValue)
        {
            ExceptionHelper.FalseThrow<ArgumentNullException>(newValue != null, "newValue");
            base.OnSet(index, oldValue, newValue);
        }
        #endregion

        #region IConnectiveSqlClause 成员

        /// <summary>
        /// Sql子句之间的逻辑运算符
        /// </summary>
        public LogicOperatorDefine LogicOperator
        {
            get
            {
                return this.logicOperator;
            }
            set
            {
                this.logicOperator = value;
            }
        }

        /// <summary>
        /// 判断条件表达式是否为空
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                bool result = true;

                foreach (IConnectiveSqlClause clause in List)
                {
                    if (clause.IsEmpty == false)
                    {
                        result = false;
                        break;
                    }
                }

                return result;
            }
        }

        /// <summary>
        /// 得到所有的字段
        /// </summary>
        /// <returns></returns>
        public HashSet<string> GetFields()
        {
            HashSet<string> allFields = new HashSet<string>();

            foreach (IConnectiveSqlClause connective in this)
                InnerGetFields(connective, allFields);

            return allFields;
        }

        /// <summary>
        /// 得到子构造对象
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IConnectiveSqlClause> GetSubConnectiveSqlClause()
        {
            foreach (IConnectiveSqlClause connective in this)
                yield return connective;
        }
        #endregion

        private static void InnerGetFields(IConnectiveSqlClause connective, HashSet<string> fields)
        {
            HashSet<string> innerFields = connective.GetFields();

            foreach (string field in innerFields)
            {
                if (fields.Contains(field) == false)
                    fields.Add(field);
            }

            foreach (IConnectiveSqlClause subConnective in connective.GetSubConnectiveSqlClause())
                InnerGetFields(subConnective, fields);
        }
    }
}
