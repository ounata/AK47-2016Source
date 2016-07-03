#region
// -------------------------------------------------
// Assembly	��	DeluxeWorks.Library.Data
// FileName	��	SqlCaluseBuilderBase.cs
// Remark	��	Sql�Ӿ乹�����ĳ������
// -------------------------------------------------
// VERSION  	AUTHOR		DATE			CONTENT
// 1.0		    ������	    20070824		����
// -------------------------------------------------
#endregion
using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using MCS.Library.Core;

namespace MCS.Library.Data.Builder
{
    /// <summary>
    /// ����Sql��乹����Ļ���
    /// </summary>
    [Serializable]
    public abstract class SqlClauseBuilderItemBase
    {
        /// <summary>
        /// �õ�Data��Sql�ַ�������
        /// </summary>
        /// <param name="builder">������</param>
        /// <returns>���ؽ�data�����sql���Ľ��</returns>
        public abstract string GetDataDesp(ISqlBuilder builder);
    }

    /// <summary>
    /// �����ݵ�Sql��乹����Ļ���
    /// </summary>
    [Serializable]
    public class SqlCaluseBuilderItemWithData : SqlClauseBuilderItemBase
    {
        private static DataDescriptionGeneratorBase[] _DataDescriptors = new DataDescriptionGeneratorBase[]{
            NullDescriptionGenerator.Instance,
            ExpressionDescriptionGenerator.Instance,
            SqlFullTextDescriptionGenerator.Instance,
            DateTimeDescriptionGenerator.Instance,
            DBNullDescriptionGenerator.Instance,
            BooleanDescriptionGenerator.Instance,
            StringDescriptionGenerator.Instance,
            EnumDescriptionGenerator.Instance,
            GuidDescriptionGenerator.Instance,
            BytesDescriptionGenerator.Instance,
            StreamDescriptionGenerator.Instance
        };

        private object data = null;
        private bool isExpression = false;

        /// <summary>
        /// ����
        /// </summary>
        public object Data
        {
            get { return this.data; }
            set { this.data = value; }
        }

        /// <summary>
        /// �������е�Data�Ƿ���sql���ʽ
        /// </summary>
        public bool IsExpression
        {
            get { return this.isExpression; }
            set { this.isExpression = value; }
        }

        /// <summary>
        /// �õ�Data��Sql�ַ�������
        /// </summary>
        /// <param name="builder">������</param>
        /// <returns>���ؽ�data�����sql���Ľ��</returns>
        public override string GetDataDesp(ISqlBuilder builder)
        {
            string result = string.Empty;

            DataDescriptionGeneratorBase generator = GetDataDescriptor(this);

            if (generator != null)
                result = generator.ToDescription(this, builder);
            else
                result = this.data.ToString();

            return result;
        }

        private static DataDescriptionGeneratorBase GetDataDescriptor(SqlCaluseBuilderItemWithData buiderItem)
        {
            DataDescriptionGeneratorBase result = null;

            foreach (DataDescriptionGeneratorBase generator in _DataDescriptors)
            {
                if (generator.IsMatched(buiderItem))
                {
                    result = generator;
                    break;
                }
            }

            return result;
        }
    }

    /// <summary>
    /// In�����������
    /// </summary>
    [Serializable]
    public class SqlCaluseBuilderItemInOperator : SqlCaluseBuilderItemWithData
    {
    }

    /// <summary>
    /// �ʺ�INSERT��UPDATE��WHERE��ÿһ������������ֶ����ƺ��ֶε�ֵ������
    /// </summary>
    [Serializable]
    public class SqlClauseBuilderItemIUW : SqlCaluseBuilderItemWithData
    {
        private string operation = SqlClauseBuilderBase.EqualTo;

        /// <summary>
        /// ���췽��
        /// </summary>
        public SqlClauseBuilderItemIUW()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        private string dataField = string.Empty;

        /// <summary>
        /// Sql����е��ֶ���
        /// </summary>
        public string DataField
        {
            get { return this.dataField; }
            set { this.dataField = value; }
        }

        /// <summary>
        /// �ֶκ�����֮��Ĳ�����
        /// </summary>
        public string Operation
        {
            get { return this.operation; }
            set { this.operation = value; }
        }
    }

    /// <summary>
    /// �ʺ�UPDATE��WHERE��ÿһ������������ֶ����ƺ��ֶε�ֵ������
    /// </summary>
    [Serializable]
    public class SqlClauseBuilderItemUW : SqlClauseBuilderItemIUW
    {
        private string template = string.Empty;

        /// <summary>
        /// ���ʽģ�塣���û���ṩ����ʹ��Ĭ��ģ��(${DataField}$ ${Operation}$ ${Data}$)��
        /// </summary>
        public string Template
        {
            get { return this.template; }
            set { this.template = value; }
        }

        /// <summary>
        /// Ĭ�ϵı��ʽģ��
        /// </summary>
        private const string DefaultTemplate = "${DataField}$ ${Operation}$ ${Data}$";

        /// <summary>
        /// ����ģ������SQL�Ӿ�
        /// </summary>
        /// <param name="strB"></param>
        /// <param name="builder"></param>
        internal virtual void ToSqlString(StringBuilder strB, ISqlBuilder builder)
        {
            this.ToSqlString(strB, DefaultTemplate, builder);
        }

        /// <summary>
        /// ����ģ������SQL�Ӿ�
        /// </summary>
        /// <param name="strB"></param>
        /// <param name="defaultTemplate"></param>
        /// <param name="builder"></param>
        internal void ToSqlString(StringBuilder strB, string defaultTemplate, ISqlBuilder builder)
        {
            Regex reg = new Regex(@"\$\{[0-9 a-z A-Z]*?\}\$");

            string t = defaultTemplate;

            if (this.template.IsNotEmpty())
                t = this.template;

            string replacedExp = reg.Replace(t, m =>
            {
                string paramName = m.Value.Substring(2, m.Length - 4);

                return GetParameterValue(paramName, builder);
            });

            strB.Append(replacedExp);
        }

        private string GetParameterValue(string paramName, ISqlBuilder builder)
        {
            string result = string.Empty;

            switch (paramName.ToLower())
            {
                case "datafield":
                    result = this.DataField;
                    break;
                case "operation":
                    result = this.Operation;
                    break;
                case "data":
                    result = this.GetDataDesp(builder);
                    break;
            }

            return result;
        }
    }

    /// <summary>
    /// ����������ʽ�Ĺ�����
    /// </summary>
    [Serializable]
    public class SqlClauseBuilderItemOrd : SqlClauseBuilderItemBase, IOrderByRequestItem
    {
        /// <summary>
        /// 
        /// </summary>
        private FieldSortDirection sortDirection = FieldSortDirection.Ascending;
        /// <summary>
        /// 
        /// </summary>
        private string dataField = string.Empty;

        /// <summary>
        /// ���췽��
        /// </summary>
        public SqlClauseBuilderItemOrd()
        {
        }

        /// <summary>
        /// ��sourceItem����
        /// </summary>
        /// <param name="sourceItem"></param>
        public SqlClauseBuilderItemOrd(IOrderByRequestItem sourceItem)
        {
            sourceItem.NullCheck("sourceItem");

            this.dataField = sourceItem.DataField;
            this.sortDirection = sourceItem.SortDirection;
        }

        /// <summary>
        /// Sql����е��ֶ���
        /// </summary>
        public string DataField
        {
            get
            {
                return this.dataField;
            }
            set
            {
                ExceptionHelper.TrueThrow<ArgumentException>(string.IsNullOrEmpty(value), "DataField���Բ���Ϊ�ջ���ַ���");
                this.dataField = value;
            }
        }

        /// <summary>
        /// ������
        /// </summary>
        public FieldSortDirection SortDirection
        {
            get
            {
                return this.sortDirection;
            }
            set
            {
                this.sortDirection = value;
            }
        }

        /// <summary>
        /// ��Դ����
        /// </summary>
        /// <param name="source"></param>
        public void CopyFrom(IOrderByRequestItem source)
        {
            if (source != null)
            {
                this.DataField = source.DataField;
                this.SortDirection = source.SortDirection;
            }
        }

        /// <summary>
        /// �õ�Data��Sql�ַ�������
        /// </summary>
        /// <param name="builder">������</param>
        /// <returns>���ؽ�data�����sql���Ľ��</returns>
        public override string GetDataDesp(ISqlBuilder builder)
        {
            string result = string.Empty;

            if (this.sortDirection == FieldSortDirection.Descending)
                result = "DESC";

            return result;
        }

        /// <summary>
        /// ����SQL�Ӿ䣨���ֶ� ASC|DESC����
        /// </summary>
        /// <param name="strB"></param>
        /// <param name="builder"></param>
        internal void ToSqlString(StringBuilder strB, ISqlBuilder builder)
        {
            strB.Append(this.DataField);

            string desp = this.GetDataDesp(builder);

            if (false == string.IsNullOrEmpty(desp))
                strB.Append(" " + desp);
        }
    }

    /// <summary>
    /// �߼������
    /// </summary>
    public enum LogicOperatorDefine
    {
        /// <summary>
        /// ���롱����
        /// </summary>
        [EnumItemDescription(Description = "���롱����", ShortName = "AND")]
        And,

        /// <summary>
        /// ���򡱲���
        /// </summary>
        [EnumItemDescription(Description = "���򡱲���", ShortName = "OR")]
        Or
    }
}
