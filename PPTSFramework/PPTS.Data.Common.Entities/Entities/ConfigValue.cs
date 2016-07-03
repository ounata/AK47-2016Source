using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace PPTS.Data.Common.Entities
{
    /// <summary>
    /// 机构配值
    /// </summary>
    [Serializable()]
    public sealed class ConfigValue : ConfigArgs
    {
        public static string Serialize<T>(T obj)
        {
            if (obj == null)
                throw new ArgumentNullException("obj");

            StringBuilder sb = new StringBuilder();
            XmlSerializer s = new XmlSerializer(typeof(ConfigValue));
            using (TextWriter writer = new StringWriter(sb))
            {
                s.Serialize(writer, obj);
            }
            return sb.ToString();
        }
        public static T Desialize<T>(string xmlString)
        {
            if (string.IsNullOrEmpty(xmlString))
                throw new ArgumentNullException("xmlString");

            XmlSerializer s = new XmlSerializer(typeof(T));
            using (TextReader reader = new StringReader(xmlString))
            {
                return (T)s.Deserialize(reader);
            }
        }

        public ConfigValue()
        {

        }
        public ConfigValue(string xmlString)
            : this()
        {
            ConfigValue corpValue = ConfigValue.Desialize<ConfigValue>(xmlString);
            foreach (PropertyInfo p in typeof(ConfigValue).GetProperties())
            {
                if (p.Name.ToLower().EndsWith("value"))
                {
                    this.SetPropertyValue(p.Name, p.GetValue(corpValue, null));
                }
            }
        }
        public ConfigValue(ConfigPair[] pairs)
            : this()
        {
            if (pairs == null || pairs.Length == 0)
                throw new ArgumentNullException("pairs");

            foreach (ConfigPair pair in pairs)
            {
                this.SetPropertyValue(pair.Name, pair.Value);
            }
        }
        private void SetPropertyValue(string name, object value)
        {
            PropertyInfo p = this.GetType().GetProperty(name);
            if (p != null)
                this.SetPropertyValue(p, value);
            if (name.ToLower().EndsWith("value"))
            {
                PropertyInfo p1 = this.GetType().GetProperty(name.Substring(0, name.Length - "value".Length));
                if (p1 != null)
                    this.SetPropertyValue(p1, value);
            }
        }
        private void SetPropertyValue(PropertyInfo p, object value)
        {
            if (p != null && p.CanWrite)
            {
                if (p.PropertyType == typeof(bool) || p.PropertyType == typeof(bool?))
                {
                    if (value != null)
                    {
                        bool v;
                        if (bool.TryParse(value.ToString(), out v))
                            p.SetValue(this, v, null);
                    }
                }
                else if (p.PropertyType == typeof(int) || p.PropertyType == typeof(int?))
                {
                    if (value != null)
                    {
                        int v;
                        if (int.TryParse(value.ToString(), out v))
                            p.SetValue(this, v, null);
                    }
                }
                else if (p.PropertyType == typeof(decimal) || p.PropertyType == typeof(decimal?))
                {
                    if (value != null)
                    {
                        decimal v;
                        if (decimal.TryParse(value.ToString(), out v))
                            p.SetValue(this, v, null);
                    }
                }
                else
                {
                    p.SetValue(this, value, null);
                }
            }
        }

        public override string ToString()
        {
            return ConfigValue.Serialize<ConfigValue>(this);
        }
        /// <summary>
        /// 获取绑定列表
        /// </summary>
        /// <returns></returns>
        public ConfigPair[] GetBindList()
        {
            Dictionary<string, ConfigPair> dict = new Dictionary<string, ConfigPair>();
            foreach (PropertyInfo p in this.GetType().GetProperties())
            {
                if (p.CanRead && p.CanWrite)
                {
                    if (p.Name.ToLower().EndsWith("value"))
                    {
                        string key = p.Name.ToLower().Substring(0, p.Name.Length - "value".Length);
                        if (!dict.ContainsKey(key))
                            dict.Add(key, new ConfigPair());
                        ConfigPair pair = dict[key];
                        pair.Name = p.Name;
                        pair.Type = p.PropertyType.IsGenericType ? p.PropertyType.GetGenericArguments()[0] : p.PropertyType;
                        pair.Value = p.GetValue(this, null);
                        Attribute descAttr = Attribute.GetCustomAttribute(p, typeof(DescriptionAttribute));
                        if (descAttr != null)
                            pair.Description = (descAttr as DescriptionAttribute).Description;
                        else
                            pair.Description = pair.Name;
                    }
                    else
                    {
                        string key = p.Name.ToLower();
                        if (!dict.ContainsKey(key))
                            dict.Add(key, new ConfigPair());
                        ConfigPair pair = dict[key];
                        pair.Name = p.Name + "Value";
                        pair.Type = p.PropertyType.IsGenericType ? p.PropertyType.GetGenericArguments()[0] : p.PropertyType;
                        pair.RuntimeValue = p.GetValue(this, null);
                    }
                }
            }
            return dict.Values.ToArray();
        }
        /// <summary>
        /// 把指定的配置合并到当前配置中
        /// </summary>
        /// <param name="value"></param>
        public void Merge(ConfigValue value)
        {
            if (value == null)
                return;

            foreach (PropertyInfo p in value.GetType().GetProperties())
            {
                if (p.CanRead && !p.Name.ToLower().EndsWith("value"))
                {
                    object v = p.GetValue(value, null);
                    PropertyInfo p1 = this.GetType().GetProperty(p.Name);
                    if (p1 != null && p1.CanWrite)
                    {
                        PropertyInfo p2 = this.GetType().GetProperty(p.Name + "Value");
                        if (p2 != null && p2.CanRead)
                        {
                            if (p2.GetValue(this, null) == null)
                                p1.SetValue(this, v, null);
                        }
                    }
                }
            }
        }

        [Description("使用拓路折扣2")]
        public bool? IsTulandDiscountSchema2Value
        {
            set;
            get;
        }

        [Description("账户首次充值的最小金额")]
        public decimal? AccountFirstChargeMinMoneyValue
        {
            set;
            get;
        }

        [Description("结课与非结课账户价值阈值")]
        public decimal? EndingClassMinAccountValueValue
        {
            set;
            get;
        }

        [Description("账户充值前期最小天数要求")]
        public int? AccountChargeEarlyMinDaysValue
        {
            set;
            get;
        }

        [Description("账户退费类型判定的天数")]
        public int? AccountRefundTypeJudgeDaysValue
        {
            set;
            get;
        }
    }
}