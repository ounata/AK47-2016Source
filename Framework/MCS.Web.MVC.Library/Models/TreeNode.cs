using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MCS.Web.MVC.Library.Models
{
    /// <summary>
    /// 树节点
    /// </summary>
    public class TreeNode<T> where T : TreeNode<T>
    {
        private List<T> _Children = null;

        public string ID
        {
            get;
            set;
        }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// 是否是展开的节点
        /// </summary>
        public bool Open
        {
            get;
            set;
        }

        /// <summary>
        /// 是否是父级节点（不是叶子节点）
        /// </summary>
        public bool IsParent
        {
            get;
            set;
        }

        /// <summary>
        /// 节点图标
        /// </summary>
        public string IconSkin
        {
            get;
            set;
        }

        /// <summary>
        /// 子节点
        /// </summary>
        public List<T> Children
        {
            get
            {
                if (this._Children == null)
                    this._Children = new List<T>();

                return this._Children;
            }
        }
    }
}