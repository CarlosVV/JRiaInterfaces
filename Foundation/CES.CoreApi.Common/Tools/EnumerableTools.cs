using System.Collections.Generic;
using System.Linq;

namespace CES.CoreApi.Common.Tools
{
    public static class EnumerableTools
    {
        public static string ToDelimitedString<T>(this IEnumerable<T> itemList, string delimiter = ",")
        {
            if (itemList == null)
                return string.Empty;

            var enumerable = itemList as IList<T> ?? itemList.ToList();

            if (enumerable.Count == 0)
                return string.Empty;

            var items = (from item in enumerable
                where item != null
                let itemString = item.ToString()
                where itemString.Length > 0
                select itemString).ToArray();

            return items == null 
                ? string.Empty 
                : string.Join(delimiter, items);
        }
    }
}