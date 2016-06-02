using MCS.Library.Core;
using MCS.Library.OGUPermission;
using PPTS.Data.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PPTS.WebAPI.Common.Models
{
    public static class ConverterExtentions
    {
        public static SelectionItemCollection ToSelectionItems(this IEnumerable<IOguObject> objs)
        {
            SelectionItemCollection items = new SelectionItemCollection();

            foreach (IOguObject obj in objs)
                items.Add(obj.ToSelectionItem());

            return items;
        }

        public static SelectionItem ToSelectionItem(this IOguObject obj)
        {
            obj.NullCheck("obj");

            SelectionItem item = new SelectionItem();

            item.Key = obj.ID;
            item.ParentKey = "0";
            item.Value = obj.DisplayName;

            return item;
        }

        public static SelectionItemCollection ToSelectionItems(this IEnumerable<BaseConstantEntity> objs)
        {
            SelectionItemCollection items = new SelectionItemCollection();

            foreach (BaseConstantEntity obj in objs)
                items.Add(obj.ToSelectionItem());

            return items;
        }

        public static SelectionItemCollection ToSelectionItems(this IEnumerable<UserAndJob> objs)
        {
            SelectionItemCollection items = new SelectionItemCollection();

            foreach (UserAndJob obj in objs)
            {
                if(!items.ContainsKey(obj.UserID))
                    items.Add(obj.ToSelectionItem());
            }

            return items;
        }

        public static SelectionItem ToSelectionItem(this BaseConstantEntity obj)
        {
            obj.NullCheck("obj");

            SelectionItem item = new SelectionItem();

            item.Key = obj.Key;
            item.ParentKey = obj.ParentKey;
            item.Value = obj.Value;

            return item;
        }

        public static SelectionItem ToSelectionItem(this UserAndJob obj)
        {
            obj.NullCheck("obj");

            SelectionItem item = new SelectionItem();

            item.Key = obj.UserID;
            item.Value = obj.UserName;
            item.SelectItem = obj;

            return item;
        }
    }
}