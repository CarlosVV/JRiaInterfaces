using System;
using System.Configuration;

namespace CES.CoreApi.Foundation.Data.Configuration
{
    [ConfigurationCollection(typeof(DatabaseServerGroup))]
    public class DatabaseServerGroups : ConfigurationElementCollection
    {
        private const string PropertyName = "databaseServerGroup";

        protected override ConfigurationElement CreateNewElement()
        {
            return new DatabaseServerGroup();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((DatabaseServerGroup)(element)).Name;
        }

        public override bool IsReadOnly()
        {
            return false;
        }

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
            return elementName.Equals(PropertyName, StringComparison.InvariantCultureIgnoreCase);
        }


        public DatabaseServerGroup this[int idx]
        {
            get { return (DatabaseServerGroup) BaseGet(idx); }
        }

        //[ConfigurationProperty(DatabaseServerGroupsElement)]
        //public DatabaseServerGroups ServerGroups
        //{
        //    get
        //    {
        //        return ((DatabaseServerGroups)(base[DatabaseServerGroupsElement]));
        //    }
        //    set { base[DatabaseServerGroupsElement] = value; }
        //}
    }
}
