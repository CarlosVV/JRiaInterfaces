using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CES.CoreApi.Logging.Utilities
{
    internal static class TypeExtensions
    {
        /// <summary>
        /// Gets public property list from the type. Works for classes or interfaces
        /// </summary>
        /// <param name="type">Type to examine</param>
        /// <param name="excludeTypeList">List of type to filter properties out</param>
        /// <returns></returns>
        public static PropertyInfo[] GetPublicProperties(this Type type, List<Type> excludeTypeList = null)
        {
            PropertyInfo[] properties;

            if (type.IsInterface)
            {
                var propertyInfos = new List<PropertyInfo>();
                var considered = new List<Type>();
                var queue = new Queue<Type>();

                considered.Add(type);
                queue.Enqueue(type);

                while (queue.Count > 0)
                {
                    var subType = queue.Dequeue();
                    foreach (var subInterface in subType.GetInterfaces()
                        .Where(subInterface => !considered.Contains(subInterface)))
                    {
                        considered.Add(subInterface);
                        queue.Enqueue(subInterface);
                    }

                    var typeProperties = subType.GetProperties(BindingFlags.FlattenHierarchy |
                                                               BindingFlags.Public |
                                                               BindingFlags.Instance);

                    var newPropertyInfos = typeProperties.Where(x => !propertyInfos.Contains(x));

                    propertyInfos.InsertRange(0, newPropertyInfos);
                }

                properties = propertyInfos.ToArray();
            }
            else
            {
                properties = type.GetProperties(BindingFlags.FlattenHierarchy |
                                                BindingFlags.Public |
                                                BindingFlags.Instance);
            }

            //Filter properties
            if (excludeTypeList != null && excludeTypeList.Count > 0)
            {
                properties = (from p in properties
                              where !excludeTypeList.Contains(p.PropertyType)
                              select p).ToArray();
            }
            return properties;
        }
    }
}