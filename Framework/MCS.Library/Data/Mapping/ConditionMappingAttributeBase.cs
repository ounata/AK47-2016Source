using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Data.Mapping
{
    /// <summary>
    /// 条件对象的映射属性的基类
    /// </summary>
    public abstract class ConditionMappingAttributeBase : System.Attribute
    {
        private string dataFieldName = string.Empty;
        private EnumUsageTypes enumUsage = EnumUsageTypes.UseEnumValue;
        private string prefix = string.Empty;
        private string postfix = string.Empty;
        private double adjustDays = 0;
        private bool isExpression = false;
        private bool utcTimeToLocal = false;
        private DefaultValueUsageType defaultValueUsage = DefaultValueUsageType.ByCaller;
        private string defaultExpression = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public ConditionMappingAttributeBase()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fieldName"></param>
        public ConditionMappingAttributeBase(string fieldName)
        {
            this.dataFieldName = fieldName;
        }

        /// <summary>
		/// 数据字段的类型
		/// </summary>
		public string DataFieldName
        {
            get { return this.dataFieldName; }
            set { this.dataFieldName = value; }
        }

        /// <summary>
        /// 枚举类型的使用方法（值/还是描述）
        /// </summary>
        public EnumUsageTypes EnumUsage
        {
            get { return this.enumUsage; }
            set { this.enumUsage = value; }
        }

        /// <summary>
        /// 生成Value时的前缀
        /// </summary>
        public string Prefix
        {
            get { return this.prefix; }
            set { this.prefix = value; }
        }

        /// <summary>
        /// 生成Value时的后缀
        /// </summary>
        public string Postfix
        {
            get { return this.postfix; }
            set { this.postfix = value; }
        }

        /// <summary>
        /// 如果是日期型，需要调整天数。
        /// </summary>
        public double AdjustDays
        {
            get { return this.adjustDays; }
            set { this.adjustDays = value; }
        }

        /// <summary>
        /// 是否是表达式
        /// </summary>
        public bool IsExpression
        {
            get { return this.isExpression; }
            set { this.isExpression = value; }
        }

        /// <summary>
        /// 默认值的操作方式
        /// </summary>
        public DefaultValueUsageType DefaultValueUsage
        {
            get { return this.defaultValueUsage; }
            set { this.defaultValueUsage = value; }
        }

        /// <summary>
        /// 假设数据库中存放的是UTC time，转换为TimeZoneContext中的时区
        /// </summary>
        public bool UtcTimeToLocal
        {
            get { return this.utcTimeToLocal; }
            set { this.utcTimeToLocal = value; }
        }

        /// <summary>
        /// 对应的属性为空时，所提供的缺省值表达式
        /// </summary>
        public string DefaultExpression
        {
            get
            {
                return this.defaultExpression;
            }
            set
            {
                this.defaultExpression = value;
            }
        }
    }
}
