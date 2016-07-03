using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Common.Authorization
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public abstract class ScopeBaseAttribute : Attribute
    {
        private ActionType actionType = ActionType.Read;

        /// <summary>
        /// 授权名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 操作类型
        /// </summary>
        public ActionType ActionType
        {
            get { return actionType; }
            set { actionType = value; }
        }

        /// <summary>
        /// 关联权限点
        /// </summary>
        public string Functions { get; set; }

        /// <summary>
        /// 描述信息
        /// </summary>
        public string Description { get; set; }
    }

    /// <summary>
    /// 范围属性模型
    /// </summary>
    public class ScopeAttributeModel
    {
        /// <summary>
        /// 范围属性
        /// </summary>
        public ScopeBaseAttribute ScopeAttribute
        { get; set; }

        /// <summary>
        /// 权限点
        /// </summary>
        public HashSet<string> Functions
        { get; set; }

        /// <summary>
        /// 权限范围类型
        /// </summary>
        public Type ScopeAttributeType
        { get; set; }
    }
}
