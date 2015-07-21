using System;
using System.Configuration;
using System.Globalization;

namespace CES.CoreApi.Foundation.Data.Configuration
{
    public class DatabaseServer : ConfigurationElement
    {
        private const string NameElement = "name";
        private const string ConnectionStringElement = "connectionString";
        private const string IsDefaultElement = "isDefault";
        private const string DatabaseIdElement = "databaseId";
        private const string ProviderNameElement = "providerName";

        [ConfigurationProperty(NameElement, IsRequired = true, IsKey = true)]
        public string Name
        {
            get
            {
                return Convert.ToString(this[NameElement], CultureInfo.InvariantCulture);
            }
            set { base[NameElement] = value; }
        }

        [ConfigurationProperty(ConnectionStringElement, IsRequired = true)]
        public string ConnectionString
        {
            get { return Convert.ToString(this[ConnectionStringElement], CultureInfo.InvariantCulture); }
            set { base[ConnectionStringElement] = value; }
        }

        [ConfigurationProperty(IsDefaultElement, IsRequired = false)]
        public bool IsDefault
        {
            get { return Convert.ToBoolean(this[IsDefaultElement], CultureInfo.InvariantCulture); }
            set { base[IsDefaultElement] = value; }
        }

        [ConfigurationProperty(DatabaseIdElement, IsRequired = false)]
        public int DatabaseId
        {
            get { return Convert.ToInt32(this[DatabaseIdElement], CultureInfo.InvariantCulture); }
            set { base[DatabaseIdElement] = value; }
        }

        [ConfigurationProperty(ProviderNameElement, IsRequired = true)]
        public string ProviderName
        {
            get { return Convert.ToString(this[ProviderNameElement], CultureInfo.InvariantCulture); }
            set { base[ProviderNameElement] = value; }
        }
    }
}
