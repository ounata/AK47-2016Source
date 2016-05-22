using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCS.Library.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MCS.Web.MVC.Library.Converters
{
    /// <summary>
    /// 日期类型的Converter。序列化时，序列化为new Data(...)，反序列化时需要进行日期时区校对
    /// </summary>
    public class JavascriptUpwardDatetimeConverter : JavaScriptDateTimeConverter
    {
        public bool IsNullable(Type t)
        {
            if (t.IsValueType)
            {
                return (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>));
            }

            return true;
        }

        public DateTime ConvertToLocalDateTime(DateTime dt)
        {
            DateTime ChangedDT = DateTime.SpecifyKind(dt, DateTimeKind.Utc);
            DateTime ConvertedDT = TimeZoneContext.Current.ConvertTimeFromUtc(ChangedDT);
            return DateTime.SpecifyKind(ConvertedDT, DateTimeKind.Local);

        }

        private DateTimeStyles _dateTimeStyles = DateTimeStyles.RoundtripKind;
        private string _dateTimeFormat;
        private CultureInfo _culture;

        /// <summary>
        /// Gets or sets the date time styles used when converting a date to and from JSON.
        /// </summary>
        /// <value>The date time styles used when converting a date to and from JSON.</value>
        public DateTimeStyles DateTimeStyles
        {
            get { return _dateTimeStyles; }
            set { _dateTimeStyles = value; }
        }

        /// <summary>
        /// Gets or sets the date time format used when converting a date to and from JSON.
        /// </summary>
        /// <value>The date time format used when converting a date to and from JSON.</value>
        public string DateTimeFormat
        {
            get { return _dateTimeFormat ?? string.Empty; }
            set { _dateTimeFormat = string.IsNullOrEmpty(value) ? null : value; }
        }

        /// <summary>
        /// Gets or sets the culture used when converting a date to and from JSON.
        /// </summary>
        /// <value>The culture used when converting a date to and from JSON.</value>
        public CultureInfo Culture
        {
            get { return _culture ?? CultureInfo.CurrentCulture; }
            set { _culture = value; }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {

            bool nullable = (objectType.IsGenericType && objectType.GetGenericTypeDefinition() == typeof(Nullable<>));
#if !NET20
            Type t = (nullable)
                ? Nullable.GetUnderlyingType(objectType)
                : objectType;
#endif

            if (reader.TokenType == JsonToken.Null)
            {
                if (!nullable)
                {
                    throw new Exception("Cannot convert null value");
                }

                return null;
            }

            if (reader.TokenType == JsonToken.Date)
            {
#if !NET20
                if (t == typeof(DateTimeOffset))
                {
                    return (reader.Value is DateTimeOffset) ? reader.Value : new DateTimeOffset((DateTime)reader.Value);
                }

                // converter is expected to return a DateTime
                if (reader.Value is DateTimeOffset)
                {
                    return ((DateTimeOffset)reader.Value).DateTime;
                }
#endif

                return ConvertToLocalDateTime((DateTime)reader.Value);
            }

            if (reader.TokenType != JsonToken.String)
            {
                throw new Exception("Unexpected token parsing date. Expected String");
            }

            string dateText = reader.Value.ToString();

            if (string.IsNullOrEmpty(dateText) && nullable)
            {
                return null;
            }

#if !NET20
            if (t == typeof(DateTimeOffset))
            {
                if (!string.IsNullOrEmpty(_dateTimeFormat))
                {
                    return DateTimeOffset.ParseExact(dateText, _dateTimeFormat, Culture, _dateTimeStyles);
                }
                else
                {
                    return DateTimeOffset.Parse(dateText, Culture, _dateTimeStyles);
                }
            }
#endif

        

      if (!string.IsNullOrEmpty(_dateTimeFormat))
      {
          DateTime date = string.IsNullOrEmpty(dateText)
              ? DateTime.MinValue
              : DateTime.ParseExact(dateText, _dateTimeFormat, Culture, _dateTimeStyles);



                return ConvertToLocalDateTime(date);
      }
      else
      {
          DateTime date = string.IsNullOrEmpty(dateText)
              ? DateTime.MinValue
              : DateTime.Parse(dateText, Culture, _dateTimeStyles);
        return ConvertToLocalDateTime(date);
      }
    }
  }
}
