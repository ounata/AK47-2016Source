﻿using MCS.Library.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Core
{
    /// <summary>
    /// 时区相关的上下文
    /// </summary>
    [ActionContextDescription(Key = "TimeZoneContext")]
    public class TimeZoneContext : ActionContextBase<TimeZoneContext>
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public TimeZoneContext()
        {
            this.CurrentTimeZone = GetDefaultTimeZone();
        }

        /// <summary>
        /// 
        /// </summary>
        public TimeZoneInfo CurrentTimeZone
        {
            get;
            set;
        }

        /// <summary>
        /// 从Utc时间转变为对应时区的本地时间
        /// </summary>
        /// <param name="utcTime"></param>
        /// <returns></returns>
        public DateTime ConvertTimeFromUtc(DateTime utcTime)
        {
            TimeZoneInfo tz = this.CurrentTimeZone;

            if (tz == null)
                tz = TimeZoneInfo.Local;

            DateTime unspecifiedTime = DateTime.SpecifyKind(utcTime, DateTimeKind.Unspecified);

            DateTime result;

            if (unspecifiedTime == DateTime.MinValue)
            {
                result = DateTime.MinValue;
            }
            else
            {
                DateTime convertedTime = DateTime.SpecifyKind(utcTime, DateTimeKind.Utc);
                result = TimeZoneInfo.ConvertTimeFromUtc(convertedTime, tz);
            }

            return result;
        }

        /// <summary>
        /// 将某个时区的时间转变为Utc时间
        /// </summary>
        /// <param name="localTime"></param>
        /// <returns></returns>
        public DateTime ConvertTimeToUtc(DateTime localTime)
        {
            TimeZoneInfo tz = this.CurrentTimeZone;

            if (tz == null)
                tz = TimeZoneInfo.Local;

            DateTime unspecifiedTime = DateTime.SpecifyKind(localTime, DateTimeKind.Unspecified);

            DateTime result;

            if (unspecifiedTime == DateTime.MinValue)
            {
                result = DateTime.MinValue;
            }
            else
            {
                result = TimeZoneInfo.ConvertTimeToUtc(unspecifiedTime, tz);
                result = DateTime.SpecifyKind(result, DateTimeKind.Local);
            }

            return result;
        }

        /// <summary>
        /// 将本机Local的时间转换为当前上下文中的时间
        /// </summary>
        /// <param name="localTime"></param>
        /// <returns></returns>
        public DateTime ConvertLocalTimeToCurrent(DateTime localTime)
        {
            TimeZoneInfo tz = this.CurrentTimeZone;

            if (tz == null)
                tz = TimeZoneInfo.Local;

            DateTime convertedTime = DateTime.SpecifyKind(localTime, DateTimeKind.Unspecified);

            return TimeZoneInfo.ConvertTime(localTime, TimeZoneInfo.Local, tz);
        }

        private static TimeZoneInfo GetDefaultTimeZone()
        {
            TimeZoneInfo result = TimeZoneInfo.Local;

            if (TimeZoneContextSettings.GetConfig().Enabled)
            {
                TimeZoneContextSettings settings = TimeZoneContextSettings.GetConfig();

                result = TimeZoneInfo.CreateCustomTimeZone(settings.TimeZoneID,
                    settings.TimeOffset, settings.TimeZoneName, settings.TimeZoneName);
            }

            return result;
        }
    }
}
