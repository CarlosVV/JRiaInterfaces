using System;

namespace CES.CoreApi.Shared.Providers.Helper
{
    public static class TypeTools
    {
        public static T ConvertValue<T, TU>(this TU value) where TU : IConvertible
        {
            return (T) Convert.ChangeType(value, typeof (T));
        }

        public static T ConvertValue<T>(this object value, bool useDefault = false)
        {
            var result = default(T);

            if (value == null || value == DBNull.Value) 
                return result;

            // null checks for 0
            if (value is int && (int) value == 0)
                return result;
                
            if (value is decimal && (decimal) value == 0)
                return result;

            if (value is string)
                value = ((string) value).Trim();

            if (value is string && Equals(value, string.Empty) && useDefault)
                return default(T);

            var type = GetUnderlyingTypeIfNullable(typeof (T));

            if (type.IsEnum)
                return (T) Enum.Parse(type, value.ToString().Replace("-", "").Replace(" ", ""), true);

            if (type != typeof (bool)) 
                return (T) Convert.ChangeType(value, type);
            
            var stringValue = value.ToString().Trim();

            // booleans are stored as "0" or "1" at times in the database
            if (stringValue == "0")
                return (T) Convert.ChangeType(false, type);

            if (stringValue == "1")
                return (T) Convert.ChangeType(true, type);

            return (T) Convert.ChangeType(value, type);
        }

        public static object ConvertValue(this object value, Type targetType, bool useDefault = false)
        {
            var result = GetDefault(targetType);

            if (value == null || value == DBNull.Value)
                return result;

            // null checks for 0
            if (value is int && (int)value == 0)
                return result;

            if (value is decimal && (decimal)value == 0)
                return result;

            if (value is string)
                value = ((string)value).Trim();

            if (value is string && Equals(value, string.Empty) && useDefault)
                return GetDefault(targetType);

            var type = GetUnderlyingTypeIfNullable(targetType);

            if (type.IsEnum)
                return Enum.Parse(type, value.ToString().Replace("-", "").Replace(" ", ""), true);

            if (type != typeof(bool))
                return Convert.ChangeType(value, type);

            var stringValue = value.ToString().Trim();

            // booleans are stored as "0" or "1" at times in the database
            if (stringValue == "0")
                return Convert.ChangeType(false, type);

            if (stringValue == "1")
                return Convert.ChangeType(true, type);

            return Convert.ChangeType(value, type);
        }

        public static bool IsList(this Type type)
        {
            return null != type.GetInterface("IEnumerable`1");
        }

        private static object GetDefault(Type type)
        {
            return type.IsValueType ? Activator.CreateInstance(type) : null;
        }

        private static Type GetUnderlyingTypeIfNullable(Type type)
        {
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                return Nullable.GetUnderlyingType(type);
            }

            return type;
        }
    }
}
