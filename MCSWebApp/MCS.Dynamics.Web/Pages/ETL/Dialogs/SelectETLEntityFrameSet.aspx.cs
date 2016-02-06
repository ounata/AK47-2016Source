﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCS.Library.SOA.DataObjects.Dynamics.Objects;
using MCS.Library.SOA.DataObjects.Dynamics.Adapters;
using MCS.Web.WebControls;

namespace MCS.Dynamics.Web.Pages.ETL.Dialogs
{
    public partial class SelectETLEntityFrameSet : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DECategory root = CategoryAdapter.Instance.GetRoot();
            DeluxeTreeNode node = new DeluxeTreeNode(root.DisplayName, root.Code);
            node.NodeCloseImg = "../../../Images/wenjianjia.gif";
            node.NodeOpenImg = "../../../Images/wenjianjia.gif";
            //node.ChildNodesLoadingType = ChildNodesLoadingTypeDefine.LazyLoading;
            node.CssClass = "treenodeParent";
            tree.Nodes.Add(node);
            node.Expanded = true;
            tree_GetChildrenData(node, node.Nodes, null);
        }
        protected void ReloadTree(object sender, EventArgs e)
        {
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
        }

        protected void tree_GetChildrenData(DeluxeTreeNode parentNode, DeluxeTreeNodeCollection result, string callBackContext)
        {
            string cssclass = parentNode.CssClass;
            CategoryCollection root = CategoryAdapter.Instance.GetByParentCode(parentNode.Value);
            foreach (var item in root)
            {
                DeluxeTreeNode node = new DeluxeTreeNode(item.DisplayName, item.Code);
                if (IsLastNode(item.Code))
                {
                    node.ChildNodesLoadingType = ChildNodesLoadingTypeDefine.Normal;
                    node.CssClass = "treeNodeThree";
                    node.Expanded = true;
                }
                else
                {
                    node.ChildNodesLoadingType = ChildNodesLoadingTypeDefine.LazyLoading;
                }
                node.NodeCloseImg = "../../../Images/wenjianjia.gif";
                node.NodeOpenImg = "../../../Images/wenjianjia.gif";



                result.Add(node);
            }
        }

        /// <summary>
        /// 判断是不是叶子节点
        /// </summary>
        /// <param name="categoryCode"></param>
        /// <returns></returns>
        protected bool IsLastNode(string categoryCode)
        {
            bool result = true;
            CategoryCollection root = CategoryAdapter.Instance.GetByParentCode(categoryCode);
            if (root.Count > 0)
            {
                result = false;
            }
            return result;
        }
    }
}