using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PPTS.Data.Common.Entities
{
    /// <summary>
    /// 配置键值对
    /// </summary>
    public class ConfigPair
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            set;
            get;
        }

        /// <summary>
        /// 类型
        /// </summary>
        public Type Type
        {
            set;
            get;
        }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description
        {
            set;
            get;
        }

        /// <summary>
        /// 配置时值
        /// </summary>
        public object Value
        {
            set;
            get;
        }

        /// <summary>
        /// 运行时值
        /// </summary>
        public object RuntimeValue
        {
            set;
            get;
        }

        /// <summary>
        /// 运行值显示文本
        /// </summary>
        public string RuntimeValueText
        {
            get
            {
                if (this.RuntimeValue == null)
                    return null;
                if (this.RuntimeValue is bool)
                    return ((bool)this.RuntimeValue) ? "是" : "否";
                return this.RuntimeValue.ToString();
            }
        }
    }
}
