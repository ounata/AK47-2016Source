﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCS.Web.Library.Script;
using System.Threading;

namespace MCS.Web.WebControls.Test.DeluxeTree
{
	public partial class cloneDeluxeTreeTest : System.Web.UI.Page
	{
		private class ExtendedData
		{
			public string Data = "Hello world";
		}

		protected void tree_GetChildrenData(DeluxeTreeNode parentNode, DeluxeTreeNodeCollection result, string callBackContext)
		{
			if (parentNode.Text == "加载子节点会出现异常")
				throw new System.ApplicationException("加载子节点出现异常");
			else
				if (parentNode.Text == "很多子节点，小心打开！")
					CreateSubTreeNodes(result, 300);
				else
				{
					Thread.Sleep(1000);
					Random rnd = new Random();
					CreateSubTreeNodes(result, rnd.Next(5));
				}
		}

		private static void CreateSubTreeNodes(DeluxeTreeNodeCollection parent, int subNodesCount)
		{
			for (int i = 0; i < subNodesCount; i++)
			{
				string text = "动态子节点" + i;
				DeluxeTreeNode node = new DeluxeTreeNode(text, text);

				node.ExtendedData = new ExtendedData();
				node.ShowCheckBox = true;

				if (i != 1)
					node.ChildNodesLoadingType = ChildNodesLoadingTypeDefine.LazyLoading;

				parent.Add(node);

				if (i == 1)
				{
					DeluxeTreeNode subNode = new DeluxeTreeNode("固定孙节点", text);
					node.Nodes.Add(subNode);
				}
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{

		}
	}
}