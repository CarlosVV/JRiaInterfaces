using System;
using System.Data;
using System.Globalization;
using CES.CoreApi.Common.Tools;

namespace CES.CoreApi.Foundation.Data.Utility
{
    public static class DataReaderExtensions
    {
        private const string ReadValueException = "An exception happened during reading field with name = '{0}' from the data reader. Target type = '{1}'.";

        public static T ReadValue<T>(this IDataReader reader, string name, bool useDefault = false)
        {
            try
            {
                var value = reader.GetValue(reader.GetOrdinal(name));
                return value.ConvertValue<T>(useDefault);
            }
            catch (Exception)
            {
                throw new ApplicationException(string.Format(CultureInfo.InvariantCulture, ReadValueException, name, typeof(T).Name));
            }
        }

        public static T ReadEnumValueFromString<T>(this IDataReader reader, string name)
        {
            try
            {
                var rawValue = ReadValue<string>(reader, name);
                return Enum.IsDefined(typeof(T), rawValue)
                    ? (T)Enum.Parse(typeof(T), rawValue, true)
                    : default(T);
            }
            catch (Exception)
            {
                throw new ApplicationException(string.Format(CultureInfo.InvariantCulture, ReadValueException, name, typeof(T).Name));
            }
        }

        public static T ReadEnumValueFromInt<T>(this IDataReader reader, string name)
        {
            try
            {
                var rawValue = ReadValue<int>(reader, name);
                return Enum.IsDefined(typeof(T), rawValue)
                    ? (T)(object)rawValue
                    : default(T);
            }
            catch (Exception)
            {
                throw new ApplicationException(string.Format(CultureInfo.InvariantCulture, ReadValueException, name, typeof(T).Name));
            }
        }
    }
}
