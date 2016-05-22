using MCS.Library.OGUPermission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MCS.Web.MVC.Library.Models
{
    /// <summary>
    /// 组织机构树的节点定义
    /// </summary>
    public class UserGraphTreeNode : TreeNode<UserGraphTreeNode>
    {
        //public WrappedOguObject Data
        //{
        //    get;
        //    set;
        //}
        public IOguObject Data
        {
            get;
            set;
        }
        ///// <summary>
        ///// 对象全路径
        ///// </summary>
        //public string FullPath
        //{
        //    get;
        //    set;
        //}

        ///// <summary>
        ///// 对象代码名称（对于用户是登录名）
        ///// </summary>
        //public string CodeName
        //{
        //    get;
        //    set;
        //}

        ///// <summary>
        ///// 对象的类型
        ///// </summary>
        //public SchemaType ObjectType
        //{
        //    get;
        //    set;
        //}
    }
}