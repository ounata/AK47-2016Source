using System;
using System.Text;
using System.Reflection;
using System.Diagnostics;
using System.Collections.Generic;
using MCS.Library.Data.Mapping;
using MCS.Library.Data.Builder;

namespace MCS.Library.Data.Mapping
{
    /// <summary>
    /// �������ʽ�Ͷ������Ե�ӳ���ϵ
    /// </summary>
    [DebuggerDisplay("PropertyName={propertyName}")]
    public class ConditionMappingItem : ConditionMappingItemBase
    {
        private string operation = SqlClauseBuilderBase.EqualTo;
        private string template = string.Empty;
        private bool escapeLikeString = false;

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

        /// <summary>
        /// �Ӷ�Ӧ���Խ������
        /// </summary>
        /// <param name="attr"></param>
        protected internal override void FillFromAttr(ConditionMappingAttributeBase attr)
        {
            base.FillFromAttr(attr);

            ConditionMappingAttribute cmAttr = (ConditionMappingAttribute)attr;

            this.operation = cmAttr.Operation;
            this.template = cmAttr.Template;
            this.escapeLikeString = cmAttr.EscapeLikeString;
        }

        /// <summary>
        /// �������ݵ�������
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        protected internal override object AdjustValue(object data)
        {
            object result = data;

            if (data is string)
            {
                if (this.EscapeLikeString)
                    result = TSqlBuilder.Instance.EscapeLikeString(data.ToString());
            }

            return base.AdjustValue(result);
        }
    }
}
