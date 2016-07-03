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

    internal static class DateTimeUtils
    {
        internal static readonly long InitialJavaScriptDateTicks = 621355968000000000;
        private const string IsoDateFormat = "yyyy-MM-ddTHH:mm:ss.FFFFFFFK";

        private const int DaysPer100Years = 36524;
        private const int DaysPer400Years = 146097;
        private const int DaysPer4Years = 1461;
        private const int DaysPerYear = 365;
        private const long TicksPerDay = 864000000000L;
        private static readonly int[] DaysToMonth365;
        private static readonly int[] DaysToMonth366;

        static DateTimeUtils()
        {
            DaysToMonth365 = new[] { 0, 31, 59, 90, 120, 151, 181, 212, 243, 273, 304, 334, 365 };
            DaysToMonth366 = new[] { 0, 31, 60, 91, 121, 152, 182, 213, 244, 274, 305, 335, 366 };
        }

        public static TimeSpan GetUtcOffset(this DateTime d)
        {
#if NET20
            return TimeZone.CurrentTimeZone.GetUtcOffset(d);
#else
            return TimeZoneInfo.Local.GetUtcOffset(d);
#endif
        }

        internal static long ConvertDateTimeToJavaScriptTicks(DateTime dateTime)
        {
            return ConvertDateTimeToJavaScriptTicks(dateTime, true);
        }

        internal static long ConvertDateTimeToJavaScriptTicks(DateTime dateTime, bool convertToUtc)
        {
            long ticks = (convertToUtc) ? ToUniversalTicks(dateTime) : dateTime.Ticks;

            return UniversialTicksToJavaScriptTicks(ticks);
        }


        private static long ToUniversalTicks(DateTime dateTime, TimeSpan offset)
        {
            // special case min and max value
            // they never have a timezone appended to avoid issues
            if (dateTime.Kind == DateTimeKind.Utc || dateTime == DateTime.MaxValue || dateTime == DateTime.MinValue)
            {
                return dateTime.Ticks;
            }

            long ticks = dateTime.Ticks - offset.Ticks;
            if (ticks > 3155378975999999999L)
            {
                return 3155378975999999999L;
            }

            if (ticks < 0L)
            {
                return 0L;
            }

            return ticks;
        }

        private static long ToUniversalTicks(DateTime dateTime)
        {
            if (dateTime.Kind == DateTimeKind.Utc)
            {
                return dateTime.Ticks;
            }

            return ToUniversalTicks(dateTime, dateTime.GetUtcOffset());
        }

        private static long UniversialTicksToJavaScriptTicks(long universialTicks)
        {
            long javaScriptTicks = (universialTicks - InitialJavaScriptDateTicks) / 10000;

            return javaScriptTicks;
        }

    }


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

        public DateTime ConvertToUTCDatetime(DateTime dt)
        {

            DateTime ChangedDT = DateTime.SpecifyKind(dt, DateTimeKind.Local);
            DateTime ConvertedDT = TimeZoneContext.Current.ConvertTimeToUtc(ChangedDT);
            return DateTime.SpecifyKind(ConvertedDT, DateTimeKind.Utc);



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



        /// <summary>
        /// Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer">The <see cref="JsonWriter"/> to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            long ticks;

            if (value is DateTime)
            {

                DateTime dateTime = (DateTime)value;
                DateTime utcDateTime = ConvertToUTCDatetime(dateTime);
                ticks = DateTimeUtils.ConvertDateTimeToJavaScriptTicks(utcDateTime);
            }
#if !NET20
            else if (value is DateTimeOffset)
            {
                DateTimeOffset dateTimeOffset = (DateTimeOffset)value;
                DateTimeOffset utcDateTimeOffset = dateTimeOffset.ToUniversalTime();
                ticks = DateTimeUtils.ConvertDateTimeToJavaScriptTicks(utcDateTimeOffset.UtcDateTime);
            }
#endif
            else
            {
                throw new JsonSerializationException("Expected date object value.");
            }

            writer.WriteStartConstructor("Date");
            writer.WriteValue(ticks);
            writer.WriteEndConstructor();
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
                    //throw new Exception("Cannot convert null value");
                    DateTime date = DateTime.MinValue;
                    return ConvertToLocalDateTime(date);
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
