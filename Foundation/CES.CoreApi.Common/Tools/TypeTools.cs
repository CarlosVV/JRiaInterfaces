using System;

namespace CES.CoreApi.Common.Tools
{
    public static class TypeTools
    {
        public static T ConvertValue<T, TU>(this TU value) where TU : IConvertible
        {
            return (T) Convert.ChangeType(value, typeof (T));
        }

        public static T ConvertValue<T>(this object value)
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
