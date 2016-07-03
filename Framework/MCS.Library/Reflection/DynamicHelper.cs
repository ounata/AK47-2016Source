using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MCS.Library.Core
{
    using System.Linq.Expressions;

    /// <summary>
    /// 方法或属性动态调用的帮助类
    /// </summary>
    public static partial class DynamicHelper
    {
        /// <summary>
        /// 得到创建方法动态调用的委托。已经创建过的，会放在缓存中
        /// </summary>
        /// <param name="methodInfo"></param>
        /// <returns></returns>
        public static Delegate GetMethodInvokeDelegate(this MethodInfo methodInfo)
        {
            return InvokeMethodDelegateCache.Instance.GetOrAddNewValue(methodInfo, (cache, key) =>
            {
                Delegate result = BuildMethodInvokeDelegate(key);

                cache.Add(key, result);

                return result;
            });
        }

        /// <summary>
        /// 创建方法动态调用的委托
        /// </summary>
        /// <param name="methodInfo"></param>
        /// <returns></returns>
        public static Delegate BuildMethodInvokeDelegate(this MethodInfo methodInfo)
        {
            methodInfo.NullCheck("methodInfo");

            var paramExpressions = methodInfo.GetParameters().Select((p, i) =>
            {
                var name = "param" + (i + 1).ToString(CultureInfo.InvariantCulture);

                return Expression.Parameter(p.ParameterType, name);

            }).ToList();

            MethodCallExpression callExpression;

            if (methodInfo.IsStatic)
            {
                callExpression = Expression.Call(methodInfo, paramExpressions);
            }
            else
            {
                var instanceExpression = Expression.Parameter(typeof(object), "instance");

                var castExpression = Expression.Convert(instanceExpression, methodInfo.ReflectedType);

                callExpression = Expression.Call(castExpression, methodInfo, paramExpressions);

                paramExpressions.Insert(0, instanceExpression);
            }

            var lambdaExpression = Expression.Lambda(callExpression, paramExpressions);

            return lambdaExpression.Compile();
        }

        /// <summary>
        /// 从Cache中得到设置对象属性的委托
        /// </summary>
        /// <param name="propertyInfo"></param>
        /// <returns></returns>
        public static Delegate GetPropertySetterDelegate(this PropertyInfo propertyInfo)
        {
            propertyInfo.NullCheck("propertyInfo");

            return GetPropertySetterDelegate(propertyInfo, propertyInfo.PropertyType);
        }

        /// <summary>
        /// 从Cache中得到设置对象属性的委托
        /// </summary>
        /// <param name="propertyInfo"></param>
        /// <param name="valueType"></param>
        /// <returns></returns>
        public static Delegate GetPropertySetterDelegate(PropertyInfo propertyInfo, Type valueType)
        {
            propertyInfo.NullCheck("propertyInfo");
            valueType.NullCheck("valueType");

            //return PropertySetMethodDelegateCache.Instance.GetOrAddNewValue(propertyInfo, valueType, (cache, key) =>
            //{
            //    Delegate result = BuildPropertySetterDelegate(propertyInfo, valueType);

            //    cache.Add(key, result);

            //    return result;
            //});
            return PropertySetMethodDelegateCache.Instance.GetOrAddNewValue(propertyInfo, valueType,
                () => BuildPropertySetterDelegate(propertyInfo, valueType));
        }

        /// <summary>
        /// 创建设置对象属性委托
        /// </summary>
        /// <param name="propertyInfo"></param>
        /// <param name="valueType"></param>
        /// <returns></returns>
        public static Delegate BuildPropertySetterDelegate(this PropertyInfo propertyInfo, Type valueType)
        {
            propertyInfo.NullCheck("propertyInfo");

            var instanceParam = Expression.Parameter(typeof(object), "instance");

            //((T)instance)
            var castExpression = Expression.Convert(instanceParam, propertyInfo.ReflectedType);

            var valueParam = Expression.Parameter(propertyInfo.PropertyType, "value");
            var castValueExpression = Expression.Convert(valueParam, valueType);

            //((T)instance).Property
            var propertyProperty = Expression.Property(castExpression, propertyInfo);

            //((T)instance).Property = value
            var assignExpression = Expression.Assign(propertyProperty, castValueExpression);

            var lambdaExpression = Expression.Lambda(assignExpression, instanceParam, valueParam);

            return lambdaExpression.Compile();
        }

        /// <summary>
        /// 从Cache中读取对象属性的委托
        /// </summary>
        /// <param name="propertyInfo"></param>
        /// <returns></returns>
        public static Delegate GetPropertyGetterDelegate(this PropertyInfo propertyInfo)
        {
            propertyInfo.NullCheck("propertyInfo");

            return GetPropertyGetterDelegate(propertyInfo, propertyInfo.PropertyType);
        }

        /// <summary>
        /// 从Cache中读取对象属性的委托
        /// </summary>
        /// <param name="propertyInfo"></param>
        /// <param name="valueType"></param>
        /// <returns></returns>
        public static Delegate GetPropertyGetterDelegate(this PropertyInfo propertyInfo, Type valueType)
        {
            propertyInfo.NullCheck("propertyInfo");
            valueType.NullCheck("targetType");

            //return PropertyGetMethodDelegateCache.Instance.GetOrAddNewValue(propertyInfo, valueType, (cache, key) =>
            //{
            //    Delegate result = BuildPropertyGetterDelegate(propertyInfo, valueType);

            //    cache.Add(key, result);

            //    return result;
            //});
            return PropertyGetMethodDelegateCache.Instance.GetOrAddNewValue(propertyInfo, valueType,
                () => BuildPropertyGetterDelegate(propertyInfo, valueType));
        }

        /// <summary>
        /// 创建读取对象属性的委托
        /// </summary>
        /// <param name="propertyInfo"></param>
        /// <param name="valueType"></param>
        /// <returns></returns>
        public static Delegate BuildPropertyGetterDelegate(this PropertyInfo propertyInfo, Type valueType)
        {
            propertyInfo.NullCheck("propertyInfo");
            valueType.NullCheck("targetType");

            var instanceParam = Expression.Parameter(typeof(object), "instance");

            //((T)instance)
            var castExpression = Expression.Convert(instanceParam, propertyInfo.ReflectedType);

            //((T)instance).Property
            var propertyProperty = Expression.Property(castExpression, propertyInfo);

            Expression castValueExpression = propertyProperty;

            if (valueType != propertyInfo.PropertyType)
                castValueExpression = Expression.Convert(propertyProperty, valueType);

            var lambdaExpression = Expression.Lambda(castValueExpression, instanceParam);

            return lambdaExpression.Compile();
        }

        /// <summary>
        /// 从Cache中读取对象字段的委托
        /// </summary>
        /// <param name="fieldInfo"></param>
        /// <returns></returns>
        public static Delegate GetFiledGetterDelegate(this FieldInfo fieldInfo)
        {
            fieldInfo.NullCheck("fieldInfo");

            return GetFiledGetterDelegate(fieldInfo, fieldInfo.FieldType);
        }

        /// <summary>
        /// 从Cache中读取对象字段的委托
        /// </summary>
        /// <param name="fieldInfo"></param>
        /// <param name="valueType"></param>
        /// <returns></returns>
        public static Delegate GetFiledGetterDelegate(this FieldInfo fieldInfo, Type valueType)
        {
            fieldInfo.NullCheck("fieldInfo");
            valueType.NullCheck("valueType");

            return FieldGetMethodDelegateCache.Instance.GetOrAddNewValue(fieldInfo, valueType, (cache, key) =>
            {
                Delegate result = BuildFieldGetterDelegate(fieldInfo, valueType);

                cache.Add(key, result);

                return result;
            });
        }

        /// <summary>
        /// 创建读取对象字段的委托
        /// </summary>
        /// <param name="fieldInfo"></param>
        /// <param name="valueType"></param>
        /// <returns></returns>
        public static Delegate BuildFieldGetterDelegate(this FieldInfo fieldInfo, Type valueType)
        {
            fieldInfo.NullCheck("fieldInfo");
            valueType.NullCheck("valueType");

            var instanceParam = Expression.Parameter(typeof(object), "instance");

            //((T)instance)
            var castExpression = Expression.Convert(instanceParam, fieldInfo.ReflectedType);

            //((T)instance).Property
            var propertyProperty = Expression.Field(castExpression, fieldInfo);

            Expression castValueExpression = propertyProperty;

            if (valueType != fieldInfo.FieldType)
                castValueExpression = Expression.Convert(propertyProperty, valueType);

            var lambdaExpression = Expression.Lambda(castValueExpression, instanceParam);

            return lambdaExpression.Compile();
        }

        /// <summary>
        /// 从Cache中得到设置对象字段的委托
        /// </summary>
        /// <param name="fieldInfo"></param>
        /// <returns></returns>
        public static Delegate GetFieldSetterDelegate(this FieldInfo fieldInfo)
        {
            return GetFieldSetterDelegate(fieldInfo, fieldInfo.FieldType);
        }

        /// <summary>
        /// 从Cache中得到设置对象字段的委托
        /// </summary>
        /// <param name="fieldInfo"></param>
        /// <param name="valueType"></param>
        /// <returns></returns>
        public static Delegate GetFieldSetterDelegate(this FieldInfo fieldInfo, Type valueType)
        {
            return FieldSetMethodDelegateCache.Instance.GetOrAddNewValue(fieldInfo, valueType, (cache, key) =>
            {
                Delegate result = BuildFieldSetterDelegate(fieldInfo, valueType);

                cache.Add(key, result);

                return result;
            });
        }

        /// <summary>
        /// 创建设置对象字段委托
        /// </summary>
        /// <param name="fieldInfo"></param>
        /// <param name="valueType">值类型</param>
        /// <returns></returns>
        public static Delegate BuildFieldSetterDelegate(this FieldInfo fieldInfo, Type valueType)
        {
            fieldInfo.NullCheck("propertyInfo");

            var instanceParam = Expression.Parameter(typeof(object), "instance");

            //((T)instance)
            var castExpression = Expression.Convert(instanceParam, fieldInfo.ReflectedType);

            var valueParam = Expression.Parameter(fieldInfo.FieldType, "value");
            var castValueExpression = Expression.Convert(valueParam, valueType);

            //((T)instance).Property
            var propertyProperty = Expression.Field(castExpression, fieldInfo);

            //((T)instance).Property = value
            var assignExpression = Expression.Assign(propertyProperty, castValueExpression);

            var lambdaExpression = Expression.Lambda(assignExpression, instanceParam, valueParam);

            return lambdaExpression.Compile();
        }
    }
}
