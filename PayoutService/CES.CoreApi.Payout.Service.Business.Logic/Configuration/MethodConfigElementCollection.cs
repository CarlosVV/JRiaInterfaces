using System;
using System.Configuration;

namespace CES.CoreApi.Payout.Service.Business.Logic.Configuration
{
    [ConfigurationCollection(typeof(MethodConfigElement))]
    public class MethodConfigElementCollection : ConfigurationElementCollection
    {
        internal const string PropertyName = "method";

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
            return ((MethodConfigElement)(element)).MethodName;
        }

        public MethodConfigElement this[int idx]
        {
            get { return (MethodConfigElement)BaseGet(idx); }
        }
    }
}
