using MCS.Library.Core;
using MCS.Web.MVC.Library.Models;
using PPTS.WebAPI.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.WebAPI.Common.Test
{
    public static class DataHelper
    {
        public static void Output(this IEnumerable<SelectionItem> items)
        {
            foreach (SelectionItem item in items)
                Console.WriteLine("Key: {0}, Value: {1}", item.Key, item.Value);
        }

        public static void Output(this UserGraphTreeNode treeNode)
        {
            Console.WriteLine("Key: {0}, Name: {1}, IsParent: {2}", treeNode.ID, treeNode.Name, treeNode.IsParent);
        }

        public static void Output(this IEnumerable<UserGraphTreeNode> treeNodes)
        {
            treeNodes.ForEach(treeNode => treeNode.Output());
        }
    }
}
