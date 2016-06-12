using MCS.Library.Core;
using MCS.Library.Data.Builder;
using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace MCS.Library.Data.Mapping
{
    /// <summary>
    /// ����Condition����WhereSqlClauseBuilder��ʱ�򣬶Ը����������ʽ��ֵ���е���
    /// </summary>
    /// <param name="propertyName"></param>
    /// <param name="propertyValue"></param>
    /// <param name="ignored"></param>
    /// <returns></returns>
    public delegate object AdjustConditionValueDelegate(string propertyName, object propertyValue, ref bool ignored);

    /// <summary>
    /// �������ʽ�Ͷ����ӳ���ϵ��
    /// </summary>
    public static class ConditionMapping
    {
        private class RelativeAttributes
        {
            public ConditionMappingAttributeBase FieldMapping = null;
            public NoMappingAttribute NoMapping = null;
            public List<SubConditionMappingAttribute> SubClassFieldMappings = new List<SubConditionMappingAttribute>();
            public SubClassTypeAttribute SubClassType = null;
        }

        #region ���з���
        /// <summary>
        /// �õ�ĳ�����͵��������ʽӳ�䷽ʽ
        /// </summary>
        /// <param name="type">������Ϣ</param>
        /// <returns>ӳ�䷽ʽ</returns>
        public static ConditionMappingItemCollection GetMappingInfo(System.Type type)
        {
            ExceptionHelper.FalseThrow<ArgumentNullException>(type != null, "type");

            ConditionMappingItemCollection result = null;

            if (ConditionMappingCache.Instance.TryGetValue(type, out result) == false)
            {
                result = GetMappingItemCollection(type);
                ConditionMappingCache.Instance[type] = result;
            }

            return result;
        }

        /// <summary>
        /// ����������������WhereSqlClauseBuilder
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="ignoreProperties">��Ҫ���Ե�����</param>
        /// <returns></returns>
        public static WhereSqlClauseBuilder GetWhereSqlClauseBuilder(object condition, params string[] ignoreProperties)
        {
            return GetWhereSqlClauseBuilder(condition, true, ignoreProperties);
        }

        /// <summary>
        /// ����������������WhereSqlClauseBuilder
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="acv"></param>
        /// <param name="ignoreProperties">��Ҫ���Ե�����</param>
        /// <returns></returns>
        public static WhereSqlClauseBuilder GetWhereSqlClauseBuilder(object condition, AdjustConditionValueDelegate acv, params string[] ignoreProperties)
        {
            return GetWhereSqlClauseBuilder(condition, true, acv, ignoreProperties);
        }

        /// <summary>
        /// ����������������WhereSqlClauseBuilder
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="ignoreDefaultValue">������������ֵΪȱʡֵʱ�������뵽WhereSqlClauseBuilder</param>
        /// <param name="ignoreProperties">��Ҫ���Ե�����</param>
        /// <returns></returns>
        public static WhereSqlClauseBuilder GetWhereSqlClauseBuilder(object condition, bool ignoreDefaultValue, params string[] ignoreProperties)
        {
            return GetWhereSqlClauseBuilder(condition, ignoreDefaultValue, null, ignoreProperties);
        }

        /// <summary>
        /// ����������������WhereSqlClauseBuilder
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="ignoreDefaultValue"></param>
        /// <param name="acv"></param>
        /// <param name="ignoreProperties">��Ҫ���Ե�����</param>
        /// <returns></returns>
        public static WhereSqlClauseBuilder GetWhereSqlClauseBuilder(
                object condition,
                bool ignoreDefaultValue,
                AdjustConditionValueDelegate acv,
                params string[] ignoreProperties)
        {
            ExceptionHelper.FalseThrow<ArgumentNullException>(condition != null, "condition");

            ConditionMappingItemCollection mapping = GetMappingInfo(condition.GetType());

            return GetWhereSqlClauseBuilderFromMapping(condition, mapping.FilterByType<ConditionMappingItem>(), ignoreDefaultValue, acv, ignoreProperties);
        }

        /// <summary>
        /// �õ���ϵ���乹������������������Where��IN����Builder
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="ignoreProperties">��Ҫ���Ե�����</param>
        /// <returns></returns>
        public static ConnectiveSqlClauseCollection GetConnectiveClauseBuilder(object condition, params string[] ignoreProperties)
        {
            return GetConnectiveClauseBuilder(condition, true, null, ignoreProperties);
        }

        /// <summary>
        /// �õ���ϵ���乹������������������Where��IN����Builder
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="acv"></param>
        /// <param name="ignoreProperties">��Ҫ���Ե�����</param>
        /// <returns></returns>
        public static ConnectiveSqlClauseCollection GetConnectiveClauseBuilder(object condition, AdjustConditionValueDelegate acv, params string[] ignoreProperties)
        {
            return GetConnectiveClauseBuilder(condition, true, acv, ignoreProperties);
        }

        /// <summary>
        /// �õ���ϵ���乹������������������Where��IN����Builder
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="ignoreDefaultValue"></param>
        /// <param name="ignoreProperties">��Ҫ���Ե�����</param>
        /// <returns></returns>
        public static ConnectiveSqlClauseCollection GetConnectiveClauseBuilder(object condition, bool ignoreDefaultValue, params string[] ignoreProperties)
        {
            return GetConnectiveClauseBuilder(condition, ignoreDefaultValue, null, ignoreProperties);
        }

        /// <summary>
        /// �õ���ϵ���乹������������������Where��IN����Builder
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="ignoreDefaultValue"></param>
        /// <param name="acv"></param>
        /// <param name="ignoreProperties">��Ҫ���Ե�����</param>
        /// <returns></returns>
        public static ConnectiveSqlClauseCollection GetConnectiveClauseBuilder(
                object condition,
                bool ignoreDefaultValue,
                AdjustConditionValueDelegate acv,
                params string[] ignoreProperties)
        {
            ExceptionHelper.FalseThrow<ArgumentNullException>(condition != null, "condition");

            ConditionMappingItemCollection mapping = GetMappingInfo(condition.GetType());

            return GetConnectiveSqlClauseBuilderFromMapping(condition, mapping, ignoreDefaultValue, acv, ignoreProperties);
        }
        #endregion

        #region ˽�з���
        private static WhereSqlClauseBuilder GetWhereSqlClauseBuilderFromMapping(
                object condition,
                IEnumerable<ConditionMappingItem> mapping,
                bool ignoreDefaultValue,
                AdjustConditionValueDelegate acv,
                string[] ignoreProperties)
        {
            WhereSqlClauseBuilder builder = new WhereSqlClauseBuilder();

            foreach (ConditionMappingItem item in mapping)
            {
                if (Array.Exists<string>(ignoreProperties, target => (string.Compare(target, item.PropertyName, true) == 0)) == false)
                {
                    object data = GetValueFromObject(item, condition);

                    if (IsTypeDefaultValue(item, data) && item.DefaultExpression.IsNotEmpty())
                        builder.AppendItem(string.Empty, item.DefaultExpression, string.Empty, "${Data}$", true);
                    else
                        DoAdjustValueAndAppendItem(data, item, ignoreDefaultValue, acv,
                            adjustedData => builder.AppendItem(item.DataFieldName, adjustedData, item.Operation, item.Template, item.IsExpression));
                }
            }

            return builder;
        }

        private static ConnectiveSqlClauseCollection GetConnectiveSqlClauseBuilderFromMapping(
                object condition,
                ConditionMappingItemCollection mapping,
                bool ignoreDefaultValue,
                AdjustConditionValueDelegate acv,
                string[] ignoreProperties)
        {
            ConnectiveSqlClauseCollection connectiveBuilder = new ConnectiveSqlClauseCollection();

            WhereSqlClauseBuilder whereBuilder = GetWhereSqlClauseBuilderFromMapping(condition, mapping.FilterByType<ConditionMappingItem>(), ignoreDefaultValue, acv, ignoreProperties);

            connectiveBuilder.Add(whereBuilder);

            FillInSqlClauseBuilderFromMapping(connectiveBuilder, condition, mapping.FilterByType<InConditionMappingItem>(), ignoreDefaultValue, acv, ignoreProperties);

            return connectiveBuilder;
        }

        /// <summary>
        /// ���IN�Ӿ�
        /// </summary>
        /// <param name="connectiveBuilder"></param>
        /// <param name="condition"></param>
        /// <param name="mapping"></param>
        /// <param name="ignoreDefaultValue"></param>
        /// <param name="acv"></param>
        /// <param name="ignoreProperties">��Ҫ���Ե�����</param>
        private static void FillInSqlClauseBuilderFromMapping(
                ConnectiveSqlClauseCollection connectiveBuilder,
                object condition,
                IEnumerable<InConditionMappingItem> mapping,
                bool ignoreDefaultValue,
                AdjustConditionValueDelegate acv,
                string[] ignoreProperties)
        {
            foreach (InConditionMappingItem item in mapping)
            {
                if (Array.Exists<string>(ignoreProperties, target => (string.Compare(target, item.PropertyName, true) == 0)) == false)
                {
                    object data = GetValueFromObject(item, condition);

                    if (data != null)
                    {
                        InSqlClauseBuilder builder = new InSqlClauseBuilder(item.DataFieldName);

                        if (data is IEnumerable && data.GetType().IsPrimitive == false)
                        {
                            foreach (object dataItem in (IEnumerable)data)
                            {
                                DoAdjustValueAndAppendItem(dataItem, item, ignoreDefaultValue, acv,
                                    (adJustedData) => builder.AppendItem(item.IsExpression, adJustedData));
                            }
                        }
                        else
                        {
                            DoAdjustValueAndAppendItem(data, item, ignoreDefaultValue, acv,
                                    (adJustedData) => builder.AppendItem(item.IsExpression, adJustedData));
                        }

                        connectiveBuilder.Add(builder);
                    }
                }
            }
        }

        /// <summary>
        /// �������ݣ�Ȼ����ӵ�builder��
        /// </summary>
        /// <param name="data"></param>
        /// <param name="item"></param>
        /// <param name="ignoreDefaultValue"></param>
        /// <param name="acv"></param>
        /// <param name="builderAction"></param>
        private static void DoAdjustValueAndAppendItem(object data, ConditionMappingItemBase item, bool ignoreDefaultValue, AdjustConditionValueDelegate acv, Action<object> builderAction)
        {
            if (data != null)
            {
                //��Ĭ�ϱ��ʽ���Ͳ����Զ�����Ĭ��ֵ
                bool needIgnoreDefaultValue = ignoreDefaultValue && item.DefaultExpression.IsNullOrEmpty();

                switch (item.DefaultValueUsage)
                {
                    case DefaultValueUsageType.Ignore:
                        needIgnoreDefaultValue = true;
                        break;
                    case DefaultValueUsageType.UseDefaultValue:
                        needIgnoreDefaultValue = false;
                        break;
                }

                //����Ĭ��ֵ
                if (needIgnoreDefaultValue == true && IsTypeDefaultValue(item, data))
                    return;

                //������Ĭ��ֵ�����߱���Ͳ���Ĭ��ֵ
                object adJustedData = null;

                //��Ĭ�ϱ��ʽ��Ϊ�գ���ΪĬ��ֵʱ
                if (IsTypeDefaultValue(item, data) && item.DefaultExpression.IsNotEmpty())
                    adJustedData = item.DefaultExpression;
                else
                    adJustedData = item.AdjustValue(data);

                bool ignored = false;
                adJustedData = OnAdjustConditionValue(acv, item.PropertyName, adJustedData, ref ignored);

                if (ignored == false && builderAction != null)
                    builderAction(adJustedData);
            }
        }

        private static object OnAdjustConditionValue(AdjustConditionValueDelegate acv, string propertyName, object propertyValue, ref bool ignored)
        {
            object result = propertyValue;

            if (acv != null)
                result = acv(propertyName, propertyValue, ref ignored);

            return result;
        }

        private static bool IsTypeDefaultValue(ConditionMappingItemBase item, object data)
        {
            bool result = false;

            if (data != null)
            {
                Type type = GetMemberInfoType(item.MemberInfo);

                if (type == typeof(object))
                    type = data.GetType();

                if (type.IsEnum)
                {
                    result = false;

                    if (type == typeof(BooleanState))
                        result = ((BooleanState)data) == BooleanState.Unknown;
                    else
                        result = data.Equals((int)TypeCreator.GetTypeDefaultValue(type));
                }
                else
                {
                    if (type == typeof(string))
                        result = string.IsNullOrEmpty((string)data);
                    else
                        result = data.Equals(TypeCreator.GetTypeDefaultValue(type));
                }
            }
            else
                result = true;

            return result;
        }

        private static Type GetMemberInfoType(MemberInfo mi)
        {
            Type result = null;

            switch (mi.MemberType)
            {
                case MemberTypes.Property:
                    result = ((PropertyInfo)mi).PropertyType;
                    break;
                case MemberTypes.Field:
                    result = ((FieldInfo)mi).FieldType;
                    break;
                default:
                    ThrowInvalidMemberInfoTypeException(mi);
                    break;
            }

            return result;
        }

        private static object GetValueFromObject(ConditionMappingItemBase item, object graph)
        {
            object data = null;

            if (item.SubClassPropertyName.IsNullOrEmpty())
                data = GetValueFromObjectDirectly(item, graph);
            else
            {
                if (graph != null)
                {
                    MemberInfo mi = graph.GetType().GetProperty(item.PropertyName,
                        BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

                    if (mi == null)
                        mi = graph.GetType().GetField(item.PropertyName,
                            BindingFlags.Instance | BindingFlags.Public);

                    if (mi != null)
                    {
                        object subGraph = GetMemberValueFromObject(mi, graph);

                        if (subGraph != null)
                            data = GetValueFromObjectDirectly(item, subGraph);
                    }
                }
            }

            return data;
        }

        private static object GetValueFromObjectDirectly(ConditionMappingItemBase item, object graph)
        {
            object data = GetMemberValueFromObject(item.MemberInfo, graph);

            if (data != null)
            {
                System.Type dataType = data.GetType();

                if (dataType.IsEnum)
                {
                    if (item.EnumUsage == EnumUsageTypes.UseEnumValue)
                        data = (int)data;
                    else
                        data = data.ToString();
                }
                else
                {
                    if (dataType == typeof(TimeSpan))
                        data = ((TimeSpan)data).TotalSeconds;
                }
            }

            return data;
        }

        private static object GetMemberValueFromObject(MemberInfo mi, object graph)
        {
            object data = null;

            switch (mi.MemberType)
            {
                case MemberTypes.Property:
                    PropertyInfo pi = (PropertyInfo)mi;
                    if (pi.CanRead)
                        data = pi.GetValue(graph, null);
                    break;
                case MemberTypes.Field:
                    FieldInfo fi = (FieldInfo)mi;
                    data = fi.GetValue(graph);
                    break;
                default:
                    ThrowInvalidMemberInfoTypeException(mi);
                    break;
            }

            return data;
        }

        private static ConditionMappingItemCollection GetMappingItemCollection(System.Type type)
        {
            ConditionMappingItemCollection result = new ConditionMappingItemCollection();

            MemberInfo[] mis = GetTypeMembers(type);

            foreach (MemberInfo mi in mis)
            {
                if (mi.MemberType == MemberTypes.Field || mi.MemberType == MemberTypes.Property)
                {
                    ConditionMappingItemCollection items = CreateMappingItems(mi);

                    MergeMappingItems(result, items);
                }
            }

            return result;
        }

        private static MemberInfo[] GetTypeMembers(System.Type type)
        {
            List<MemberInfo> list = new List<MemberInfo>();

            PropertyInfo[] pis = type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);

            Array.ForEach(pis, delegate (PropertyInfo pi) { list.Add(pi); });

            FieldInfo[] fis = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.FlattenHierarchy);

            Array.ForEach(fis, delegate (FieldInfo fi) { list.Add(fi); });

            return list.ToArray();
        }

        private static ConditionMappingItemCollection CreateMappingItems(MemberInfo mi)
        {
            ConditionMappingItemCollection result = null;
            bool isDoMapping = false;

            RelativeAttributes attrs = null;

            //if (mi.Name != "Item" && GetRealType(mi).GetInterface("ICollection") == null)
            if (mi.Name != "Item")
            {
                attrs = GetRelativeAttributes(mi);

                if (attrs.NoMapping == null)
                    isDoMapping = true;
            }

            if (isDoMapping == true)
            {
                if (attrs != null)
                {
                    if (attrs.SubClassFieldMappings.Count > 0)
                        result = GetMappingItemsBySubClass(attrs, mi);
                    else
                        result = GetMappingItems(attrs, mi);
                }
            }
            else
                result = new ConditionMappingItemCollection();

            return result;
        }

        private static void MergeMappingItems(ConditionMappingItemCollection dest, ConditionMappingItemCollection src)
        {
            foreach (ConditionMappingItemBase item in src)
                dest.Add(item);
        }

        private static RelativeAttributes GetRelativeAttributes(MemberInfo mi)
        {
            RelativeAttributes result = new RelativeAttributes();

            Attribute[] attrs = Attribute.GetCustomAttributes(mi, true);

            foreach (Attribute attr in attrs)
            {
                if (attr is NoMappingAttribute)
                {
                    result.NoMapping = (NoMappingAttribute)attr;
                    break;
                }
                else
                {
                    if (attr is SubConditionMappingAttribute)
                    {
                        result.SubClassFieldMappings.Add((SubConditionMappingAttribute)attr);
                    }
                    else
                    {
                        if (attr is SubClassTypeAttribute)
                        {
                            result.SubClassType = (SubClassTypeAttribute)attr;
                        }
                        else
                        {
                            if (attr is ConditionMappingAttributeBase)
                                result.FieldMapping = (ConditionMappingAttributeBase)attr;
                        }
                    }
                }
            }

            return result;
        }

        private static System.Type GetRealType(MemberInfo mi)
        {
            System.Type type = null;

            switch (mi.MemberType)
            {
                case MemberTypes.Property:
                    type = ((PropertyInfo)mi).PropertyType;
                    break;
                case MemberTypes.Field:
                    type = ((FieldInfo)mi).FieldType;
                    break;
                default:
                    ThrowInvalidMemberInfoTypeException(mi);
                    break;
            }

            if (type.IsGenericType && type.GetGenericTypeDefinition().FullName == "System.Nullable`1")
                type = type.GetGenericArguments()[0];

            return type;
        }

        private static void ThrowInvalidMemberInfoTypeException(MemberInfo mi)
        {
            throw new InvalidOperationException(string.Format("�Ƿ��ĳ�Ա����{0},{1}",
                mi.Name,
                mi.MemberType));
        }

        private static ConditionMappingItemCollection GetMappingItems(RelativeAttributes attrs, MemberInfo mi)
        {
            ConditionMappingItemCollection items = new ConditionMappingItemCollection();

            ConditionMappingItemBase item = CreateConditionMappingItemByAttr(attrs.FieldMapping);
            item.PropertyName = mi.Name;
            item.DataFieldName = mi.Name;

            if (attrs.FieldMapping != null)
                item.FillFromAttr(attrs.FieldMapping);

            item.MemberInfo = mi;

            items.Add(item);

            return items;
        }

        private static ConditionMappingItemCollection GetMappingItemsBySubClass(RelativeAttributes attrs, MemberInfo sourceMI)
        {
            ConditionMappingItemCollection items = new ConditionMappingItemCollection();
            System.Type subType = attrs.SubClassType != null ? attrs.SubClassType.Type : GetRealType(sourceMI);

            MemberInfo[] mis = GetTypeMembers(subType);

            foreach (SubConditionMappingAttribute attr in attrs.SubClassFieldMappings)
            {
                MemberInfo mi = GetMemberInfoByName(attr.SubPropertyName, mis);

                if (mi != null)
                {
                    ConditionMappingItemBase item = CreateConditionMappingItemByAttr(attr);

                    item.PropertyName = sourceMI.Name;
                    item.SubClassPropertyName = attr.SubPropertyName;
                    item.MemberInfo = mi;

                    if (attrs.SubClassType != null)
                        item.SubClassTypeDescription = attrs.SubClassType.TypeDescription;

                    item.FillFromAttr(attr);

                    items.Add(item);
                }
            }

            return items;
        }

        private static MemberInfo GetMemberInfoByName(string name, MemberInfo[] mis)
        {
            MemberInfo result = null;

            foreach (MemberInfo mi in mis)
            {
                if (mi.Name == name)
                {
                    result = mi;
                    break;
                }
            }

            return result;
        }

        private static ConditionMappingItem FindItemBySubClassPropertyName(string subPropertyName, ConditionMappingItemCollection items)
        {
            ConditionMappingItem result = null;

            foreach (ConditionMappingItem item in items)
            {
                if (item.SubClassPropertyName == subPropertyName)
                {
                    result = item;
                    break;
                }
            }

            return result;
        }

        private static ConditionMappingItemBase CreateConditionMappingItemByAttr(ConditionMappingAttributeBase attr)
        {
            ConditionMappingItemBase result = null;

            if (attr is InConditionMappingAttribute)
                result = new InConditionMappingItem();
            else
                result = new ConditionMappingItem();

            return result;
        }
        #endregion
    }
}
