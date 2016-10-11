using System;
using System.Configuration;

namespace CES.CoreApi.Payout.Service.Business.Logic.Configuration
{
    [ConfigurationCollection(typeof(ApiProviderConfigElement))]
    public class ApiProviderConfigElementCollection : ConfigurationElementCollection
    {
        internal const string PropertyName = "provider";

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
            return new MethodConfigElement();
        }
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ApiProviderConfigElement)(element)).ProviderName;
        }
      

        public ApiProviderConfigElement this[int idx]
        {
            get { return (ApiProviderConfigElement)BaseGet(idx); }
        }
    }
}
