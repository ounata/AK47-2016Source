using System;
using System.Text;
using System.ComponentModel;
using System.Collections.Generic;
using MCS.Library.Data.Mapping;

namespace MCS.Library.Data.Mapping
{
    /// <summary>
    /// ���������ӳ������
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class ConditionMappingAttribute : ConditionMappingAttributeBase
    {
        private string operation = "=";
        private string template = string.Empty;
        private bool escapeLikeString = false;

        /// <summary>
        /// 
        /// </summary>
        public ConditionMappingAttribute()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fieldName"></param>
        public ConditionMappingAttribute(string fieldName)
            : base(fieldName)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="op"></param>
        public ConditionMappingAttribute(string fieldName, string op)
            : base(fieldName)
        {
            this.operation = op;
        }

        /// <summary>
        /// ��������ȱʡΪ��=��
        /// </summary>
        public string Operation
        {
            get { return this.operation; }
            set { this.operation = value; }
        }

        /// <summary>
        /// ���ɵ�SQL�Ӿ�ı��ʽģ�塣Ĭ����${DataField}$ ${Operation}$ ${Data}$
        /// </summary>
        public string Template
        {
            get { return this.template; }
            set { this.template = value; }
        }

        /// <summary>
        /// �Ƿ���LIKE�Ӿ�ת���ַ����е�LIKE������
        /// </summary>
        public bool EscapeLikeString
        {
            get { return this.escapeLikeString; }
            set { this.escapeLikeString = value; }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
    public sealed class SubConditionMappingAttribute : ConditionMappingAttribute
    {
        private string subPropertyName = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="subPropertyName"></param>
        /// <param name="fieldName"></param>
        public SubConditionMappingAttribute(string subPropertyName, string fieldName)
            : base(fieldName)
        {
            this.subPropertyName = subPropertyName;
        }

        /// <summary>
        /// Դ�������������
        /// </summary>
        public string SubPropertyName
        {
            get
            {
                return this.subPropertyName;
            }
            set
            {
                this.subPropertyName = value;
            }
        }
    }
}
