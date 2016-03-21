using System;
using System.Text;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections;

using MCS.Library.Core;
using MCS.Library.Data.DataObjects;

namespace MCS.Library.Data.Mapping
{
    /// <summary>
    /// �������ʽ�Ͷ���ӳ����Ŀ����
    /// </summary>
    public class ConditionMappingItemCollection : DataObjectCollectionBase<ConditionMappingItemBase>
    {
        /// <summary>
        /// ���һ��������
        /// </summary>
        /// <param name="item"></param>
        public void Add(ConditionMappingItemBase item)
        {
            InnerAdd(item);
        }

        /// <summary>
        /// ����������ӻ�����һ��������
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public ConditionMappingItemBase this[int index]
        {
            get
            {
                return (ConditionMappingItemBase)List[index];
            }
            set
            {
                List[index] = value;
            }
        }

        /// <summary>
        /// ɾ��һ��������
        /// </summary>
        /// <param name="item"></param>
        public void Remove(ConditionMappingItemBase item)
        {
            List.Remove(item);
        }

        /// <summary>
        /// �������ͽ���ɸѡ
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IEnumerable<T> FilterByType<T>() where T: ConditionMappingItemBase
        {
            foreach(ConditionMappingItemBase item in this)
            {
                if (item is T)
                {
                    yield return (T)item;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        protected override void OnValidate(object value)
        {
            ExceptionHelper.FalseThrow<ArgumentNullException>(value != null, "value");
        }
    }
}
