using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Core
{
    public static partial class DynamicHelper
    {
        private static readonly Dictionary<Type, Func<PropertyInfo, object, object>> PropertyGetters = new Dictionary<Type, Func<PropertyInfo, object, object>>()
        {
            { typeof(int), (pi, graph) => ((Func<object, int>)pi.GetPropertyGetterDelegate())(graph) },
            { typeof(uint), (pi, graph) => ((Func<object, uint>)pi.GetPropertyGetterDelegate())(graph) },
            { typeof(long), (pi, graph) => ((Func<object, long>)pi.GetPropertyGetterDelegate())(graph) },
            { typeof(ulong), (pi, graph) => ((Func<object, ulong>)pi.GetPropertyGetterDelegate())(graph) },

            { typeof(bool), (pi, graph) => ((Func<object, bool>)pi.GetPropertyGetterDelegate())(graph) },

            { typeof(decimal), (pi, graph) => ((Func<object, decimal>)pi.GetPropertyGetterDelegate())(graph) },
            { typeof(double), (pi, graph) => ((Func<object, double>)pi.GetPropertyGetterDelegate())(graph) },
            { typeof(float), (pi, graph) => ((Func<object, float>)pi.GetPropertyGetterDelegate())(graph) },

            { typeof(TimeSpan), (pi, graph) => ((Func<object, TimeSpan>)pi.GetPropertyGetterDelegate())(graph) },
            { typeof(DateTime), (pi, graph) => ((Func<object, DateTime>)pi.GetPropertyGetterDelegate())(graph) },

            { typeof(string), (pi, graph) => ((Func<object, string>)pi.GetPropertyGetterDelegate())(graph) }
        };

        private static readonly Dictionary<Type, Func<PropertyInfo, object, object, object>> PropertySetters = new Dictionary<Type, Func<PropertyInfo, object, object, object>>()
        {
            { typeof(int), (pi, graph, value) => ((Func<object, int, int>)pi.GetPropertySetterDelegate())(graph, (int)value) },
            { typeof(uint), (pi, graph, value) => ((Func<object, uint, uint>)pi.GetPropertySetterDelegate())(graph, (uint)value) },
            { typeof(long), (pi, graph, value) => ((Func<object, long, long>)pi.GetPropertySetterDelegate())(graph, (long)value) },
            { typeof(ulong), (pi, graph, value) => ((Func<object, ulong, ulong>)pi.GetPropertySetterDelegate())(graph, (ulong)value) },

            { typeof(bool), (pi, graph, value) => ((Func<object, bool, bool>)pi.GetPropertySetterDelegate())(graph, (bool)value) },

            { typeof(decimal), (pi, graph, value) => ((Func<object, decimal, decimal>)pi.GetPropertySetterDelegate())(graph, (decimal)value) },
            { typeof(double), (pi, graph, value) => ((Func<object, double, double>)pi.GetPropertySetterDelegate())(graph, (double)value) },
            { typeof(float), (pi, graph, value) => ((Func<object, float, float>)pi.GetPropertySetterDelegate())(graph, (float)value) },

            { typeof(TimeSpan), (pi, graph, value) => ((Func<object, TimeSpan, TimeSpan>)pi.GetPropertySetterDelegate())(graph, (TimeSpan)value) },
            { typeof(DateTime), (pi, graph, value) => ((Func<object, DateTime, DateTime>)pi.GetPropertySetterDelegate())(graph, (DateTime)value) },

            { typeof(string), (pi, graph, value) => ((Func<object, string, string>)pi.GetPropertySetterDelegate())(graph, (string)value) }
        };

        /// <summary>
        /// 得到属性的值。会根据预定义好的属性delegator来获取。如果没有预定义好的，直接使用反射
        /// </summary>
        /// <param name="pi"></param>
        /// <param name="graph"></param>
        /// <returns></returns>
        public static object GetPropertyValue(this PropertyInfo pi, object graph)
        {
            object result = null;

            Func<PropertyInfo, object, object> getter = null;

            if (PropertyGetters.TryGetValue(pi.PropertyType, out getter) == false)
                getter = DefaultPropertyGetter;

            result = getter(pi, graph);

            return result;
        }

        private static object DefaultPropertyGetter(PropertyInfo pi, object graph)
        {
            return pi.GetValue(graph);
        }

        /// <summary>
        /// 设置属性的值。会根据预定义好的属性delegator来获取。如果没有预定义好的，直接使用反射
        /// </summary>
        /// <param name="pi"></param>
        /// <param name="graph"></param>
        /// <param name="value"></param>
        /// <returns>返回graph对象，用于连续设置属性值</returns>
        public static object SetPropertyValue(this PropertyInfo pi, object graph, object value)
        {
            Func<PropertyInfo, object, object, object> setter = null;

            if (PropertySetters.TryGetValue(pi.PropertyType, out setter) == false)
                setter = DefaultPropertySetter;

            setter(pi, graph, value);

            return graph;
        }

        private static object DefaultPropertySetter(PropertyInfo pi, object graph, object value)
        {
            pi.SetValue(graph, value);

            return graph;
        }
    }
}
