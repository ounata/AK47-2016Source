using MCS.Library.Data.DataObjects;
using System;
using System.Runtime.Serialization;

namespace PPTS.WebAPI.Common.Models
{
    [Serializable]
    public class SelectionItem
    {
        /// <summary>
        /// 当前项的Key
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 当前项的父级Key，第一级为0
        /// </summary>
        public string ParentKey { get; set; }

        /// <summary>
        /// 当前项显示的值
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// 当前项
        /// </summary>
        public object SelectItem { get; set; }
    }

    [Serializable]
    public class SelectionItemCollection : SerializableEditableKeyedDataObjectCollectionBase<string, SelectionItem>
    {
        public SelectionItemCollection()
        {
        }

        protected SelectionItemCollection(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }

        protected override string GetKeyForItem(SelectionItem item)
        {
            return item.Key;
        }
    }
}