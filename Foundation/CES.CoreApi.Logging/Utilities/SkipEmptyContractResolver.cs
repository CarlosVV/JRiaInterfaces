using System;
using System.Collections;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CES.CoreApi.Logging.Utilities
{
    public class SkipEmptyContractResolver : DefaultContractResolver
    {
        public SkipEmptyContractResolver(bool shareCache = false) : base(shareCache)
        {
        }

        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var property = base.CreateProperty(member, memberSerialization);
            var isDefaultValueIgnored = ((property.DefaultValueHandling ?? DefaultValueHandling.Ignore) &
                                         DefaultValueHandling.Ignore) != 0;
            if (isDefaultValueIgnored && !typeof (string).IsAssignableFrom(property.PropertyType) &&
                typeof (IEnumerable).IsAssignableFrom(property.PropertyType))
            {
                Predicate<object> shouldSerialize = obj =>
                {
                    var collection = property.ValueProvider.GetValue(obj) as ICollection;
                    return collection == null || collection.Count != 0;
                };

                var oldShouldSerialize = property.ShouldSerialize;
                property.ShouldSerialize = oldShouldSerialize != null
                    ? o => oldShouldSerialize(o) && shouldSerialize(o)
                    : shouldSerialize;
            }
            return property;
        }
    }
}
