using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;

namespace CES.CoreApi.Receipt_Main.Service.Utilities
{
    public static class SafeCast
    {

        private static DateTime minDate = (DateTime)SqlDateTime.MinValue;
        public static string ToSafeDbString(this string value, string defaultValue = "")
        {
            if (string.IsNullOrEmpty(value))
                return defaultValue;
            return value.Trim();
        }

        public static int ToNumber(this string value, int defaultValue = 0)
        {
            int num;
            if (int.TryParse(value, out num))
                return num;

            return defaultValue;

        }

        public static DateTime ToSafeDateTimeValue(this DateTime value, DateTime? defaultValue)
        {

            if (value == null || value < minDate)
            {
                if (defaultValue != null)
                    return (DateTime)defaultValue;
            }

            return value;

        }

        public static DateTime ToSafeDateTimeValue(this DateTime value)
        {

            if (value == null || value < minDate)
            {
                return minDate;
            }

            return value;

        }

        public static T GetSafeValue<T>(this IDataReader reader, string name)
        {
            try
            {
                return reader[name].GetSafeValue<T>();
            }
            catch
            {

                return default(T);
            }
        }

        public static T GetSafeValue<T>(this object value)
        {
            try
            {
                return (T)Convert.ChangeType(value, typeof(T));
            }
            catch
            {
                return default(T);
            }


        }
    }
}