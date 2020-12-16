using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Theater_ticket_booking.Extensions
{
    public static partial class DateTimeExtension
    {

        public const long UnixDay = 86400;

        public static DateTime LongToDateTime(this long date)
        {
            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            return dtDateTime.AddSeconds(date);
        }
        public static DateTime? LongToDateTime(this long? date)
        {
            if (date == null) return null;
            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            return dtDateTime.AddSeconds((long)date);
        }

        public static long CurrentTimeLong()
        {
            return DateTime.UtcNow.DateTimeToLong();
        }

        public static long CurrentDayLong()
        {
            return DateTime.Today.DateTimeToLong();
        }

        public static long ApplyTimeZone(this long date, long offset)
        {
            return date - offset;
        }

        public static string CurrentDateString(string format = "dd.MM.yyyy")
        {
            return DateTime.UtcNow.DateTimeToString(format);
        }

        public static string CurrentTimeString(string format = "dd.MM.yyyy hh:mm:ss")
        {
            return DateTime.UtcNow.DateTimeToString(format);
        }

        public static string UnixTimeToString(this long date)
        {
            string format = "dd.MM.yyyy";
            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(date);
            return dtDateTime.DateTimeToString(format);
        }

        public static string ISO2FormatString(this string isoString, string format = "dd.MM.yyyy")
        {
            return isoString.StringToUnixTime().UnixTimeToString(format);
        }

        public static string UnixTimeToISO(this long date)
        {
            string format = "yyyy-MM-dd";
            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(date);
            return dtDateTime.DateTimeToString(format);
        }

        public static string UnixTimeToISOTime(this long date)
        {
            string format = "HH:mm";
            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(date);
            return dtDateTime.DateTimeToString(format);
        }

        public static string UnixTimeToISOWithTime(this long date)
        {
            string format = "yyyy-MM-dd HH:mm";

            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(date);
            return dtDateTime.DateTimeToString(format);
        }

        public static string UnixTimeToISOWithTime(this long? date)
        {
            string format = "yyyy-MM-dd HH:mm";

            if (!date.HasValue) return "";

            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(date.Value);
            return dtDateTime.DateTimeToString(format);
        }

        public static string UnixTimeToString(this long date, string format)
        {
            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(date);
            return dtDateTime.DateTimeToString(format);
        }

        public static string StringToOtherFormat(this string dateIn, string format = "yyyy-MM-dd")
        {
            var dateTime = !dateIn.Contains("/")
                ? DateTime.Parse(dateIn)
                : DateTime.Parse(dateIn, CultureInfo.InvariantCulture);
            return dateTime.DateTimeToString(format);
        }

        public static long StringToUnixTime(this string date)
        {
            var dateTime = !date.Contains("/")
                ? DateTime.Parse(date)
                : DateTime.Parse(date, CultureInfo.InvariantCulture);
            return dateTime.DateTimeToLong();
        }

        public static long StringWithTimeToUnixTime(this string date)
        {
            var dateTime = !date.Contains("/")
                ? DateTime.Parse(date)
                : DateTime.ParseExact(date, "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);
            return dateTime.DateTimeToLong();
        }

        public static long StringToUnixTime(this string date, string format)
        {
            DateTime dateTime = DateTime.Parse(date);
            return dateTime.DateTimeToLong();
        }

        public static long StringWithTimeToUnixTime(string date, string time, string format = "yyyy-MM-dd HH:mm:ss")
        {
            var datetime = $"{date} {time}:00";

            DateTime dateTime = DateTime.ParseExact(datetime, format, CultureInfo.InvariantCulture);
            return dateTime.DateTimeToLong();
        }

        public static long StringWithTimeToUnixTime(long date, string time, string format = "yyyy-MM-dd HH:mm:ss")
        {
            var datetime = $"{date.UnixTimeToISO()} {time}:00";

            DateTime dateTime = DateTime.ParseExact(datetime, format, CultureInfo.InvariantCulture);
            return dateTime.DateTimeToLong();
        }

        public static long DateTimeToLong(this DateTime date)
        {
            return (long)(date.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        }

        public static string DateTimeToString(this DateTime date, string format = "dd.MM.yyyy")
        {
            return format == null ? date.ToString(CultureInfo.InvariantCulture) : date.ToString(format);
        }

        public static int TotalDays(this long dateStart, long dateEnd)
        {
            var time = dateEnd - dateStart;
            return (int)(time / UnixDay);
        }
    }
}