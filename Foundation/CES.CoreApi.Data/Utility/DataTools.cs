using System;
using System.Data;
using CES.CoreApi.Common.Tools;

namespace CES.CoreApi.Foundation.Data.Utility
{
    public static class DataTools
    {
        public static T ReadValue<T>(this IDataReader reader, string name)
        {
            var value = reader.GetValue(reader.GetOrdinal(name));
            return value.ConvertValue<T>();
        }

        public static T ReadEnumValueFromString<T>(this IDataReader reader, string name)
        {
            var rawValue = ReadValue<string>(reader, name);
            return Enum.IsDefined(typeof(T), rawValue)
                ? (T)Enum.Parse(typeof(T), rawValue, true)
                : default(T);
        }

        public static T ReadEnumValueFromInt<T>(this IDataReader reader, string name)
        {
            var rawValue = ReadValue<int>(reader, name);
            return Enum.IsDefined(typeof (T), rawValue)
                ? (T) (object) rawValue
                : default(T);
        }
    }
}
