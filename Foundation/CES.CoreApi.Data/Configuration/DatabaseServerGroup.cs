using System;
using System.Configuration;
using System.Globalization;

namespace CES.CoreApi.Foundation.Data.Configuration
{
    [ConfigurationCollection(typeof (DatabaseServer))]
    public class DatabaseServerGroup : ConfigurationElementCollection
    {
        private const string NameElement = "name";
        private const string PropertyName = "databaseServer";

        [ConfigurationProperty(NameElement, IsRequired = true, IsKey = true)]
        public string Name
        {
            get { return Convert.ToString(this[NameElement], CultureInfo.InvariantCulture); }
            set { base[NameElement] = value; }
        }

        public DatabaseServer this[int idx]
        {
            get { return (DatabaseServer)BaseGet(idx); }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new DatabaseServer();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((DatabaseServer) (element)).Name;
        }

        public override bool IsReadOnly()
        {
            return false;
        }

        public override ConfigurationElementCollectionType CollectionType
        {
            get { return ConfigurationElementCollectionType.BasicMapAlternate; }
        }

        protected override string ElementName
        {
            get { return PropertyName; }
        }

        protected override bool IsElementName(string elementName)
        {
            return elementName.Equals(PropertyName, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}