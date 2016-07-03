using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using MCS.Library.Caching;

namespace MCS.Library.Core
{
    internal class DelegationItem
    {
        public Type ValudeType
        {
            get;
            set;
        }

        public Delegate Delegate
        {
            get;
            set;
        }
    }

    internal abstract class PropertyGetMethodDelegateCacheBase // : PortableCacheQueue<PropertyInfo, List<DelegationItem>>
    {
        private static readonly Dictionary<PropertyInfo, List<DelegationItem>> _Cache = new Dictionary<PropertyInfo, List<DelegationItem>>();

        public Delegate GetOrAddNewValue(PropertyInfo propertyInfo, Type valueType, Func<Delegate> func)
        {
            List<DelegationItem> items = null;

            lock (_Cache)
            {
                if (_Cache.TryGetValue(propertyInfo, out items) == false)
                {
                    items = new List<DelegationItem>();

                    _Cache.Add(propertyInfo, items);
                }
            }

            lock (items)
            {
                DelegationItem item = items.Find(i => i.ValudeType == valueType);


                if (item == null)
                {
                    item = new DelegationItem() { ValudeType = valueType, Delegate = func() };
                    items.Add(item);
                }

                return item.Delegate;
            }
        }
    }

    //internal abstract class PropertyGetMethodDelegateCacheBase : PortableCacheQueue<PropertyMethodDelegateCacheKey, Delegate>
    //{
    //    private static readonly Dictionary<PropertyMethodDelegateCacheKey, Delegate> _Cache = new Dictionary<PropertyMethodDelegateCacheKey, Delegate>();

    //    public Delegate GetOrAddNewValue(PropertyInfo propertyInfo, Type valueType, PortableCacheItemNotExistsAction action)
    //    {
    //        PropertyMethodDelegateCacheKey key = new PropertyMethodDelegateCacheKey(propertyInfo, valueType);

    //        return GetOrAddNewValue(key, action);
    //    }

    //    //public Delegate GetOrAddNewValue(PropertyInfo propertyInfo, Type valueType, Func<Dictionary<PropertyMethodDelegateCacheKey, Delegate>, PropertyMethodDelegateCacheKey, Delegate> func)
    //    //{
    //    //    PropertyMethodDelegateCacheKey key = new PropertyMethodDelegateCacheKey(propertyInfo, valueType);

    //    //    Delegate result = null;

    //    //    if (_Cache.TryGetValue(key, out result) == false)
    //    //    {
    //    //        result = func(_Cache, key);
    //    //    }

    //    //    return result;
    //    //}

    //    protected static string CalculateKey(PropertyInfo propertyInfo, Type valueType)
    //    {
    //        return propertyInfo.ReflectedType.FullName + propertyInfo.PropertyType.FullName + valueType.FullName;
    //    }
    //}

    internal sealed class PropertyGetMethodDelegateCache : PropertyGetMethodDelegateCacheBase
    {
        /// <summary>
        /// 获取实例
        /// </summary>
        public static readonly PropertyGetMethodDelegateCache Instance = new PropertyGetMethodDelegateCache();  //CacheManager.GetInstance<PropertyGetMethodDelegateCache>();

        private PropertyGetMethodDelegateCache()
        {
        }
    }

    internal sealed class PropertySetMethodDelegateCache : PropertyGetMethodDelegateCacheBase
    {
        /// <summary>
        /// 获取实例
        /// </summary>
        public static readonly PropertySetMethodDelegateCache Instance = new PropertySetMethodDelegateCache(); //CacheManager.GetInstance<PropertySetMethodDelegateCache>();

        private PropertySetMethodDelegateCache()
        {
        }
    }
}
