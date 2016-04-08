using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Data.Executors
{
    /// <summary>
    /// Executor的描述信息，可以出现在日志中
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class DataExecutorDescriptionAttribute : Attribute
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public DataExecutorDescriptionAttribute()
        {
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="description">Executor的描述信息</param>
        public DataExecutorDescriptionAttribute(string description)
        {
            this.Description = description;
        }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description
        {
            get;
            set;
        }
    }
}
