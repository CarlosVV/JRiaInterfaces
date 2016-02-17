using System;
using System.Configuration;

namespace CES.CoreApi.Caching.Configuration
{
    [ConfigurationCollection(typeof(RedisHostElement))]
    public class RedisHostElementCollection : ConfigurationElementCollection
    {
        internal const string PropertyName = "host";

        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.BasicMapAlternate;
            }
        }
        protected override string ElementName
        {
            get
            {
                return PropertyName;
            }
        }
        protected override bool IsElementName(string elementName)
        {
            return elementName.Equals(PropertyName,
              StringComparison.InvariantCultureIgnoreCase);
        }

        public override bool IsReadOnly()
        {
            return false;
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new RedisHostElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((RedisHostElement)(element)).Priority;
        }

        public RedisHostElement this[int idx]
        {
            get { return (RedisHostElement)BaseGet(idx); }
        }
    }
}
