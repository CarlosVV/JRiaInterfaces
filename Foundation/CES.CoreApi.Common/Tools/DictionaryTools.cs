using System.Collections.Generic;

namespace CES.CoreApi.Common.Tools
{
    public static class DictionaryTools
    {
        public static T GetValue<T>(this IReadOnlyDictionary<string, object> dictionary, string name)
        {
            if (!dictionary.ContainsKey(name))
                return default(T);

            var value = dictionary[name].ToString();
            return value.ConvertValue<T, string>();
        }
    }
}
