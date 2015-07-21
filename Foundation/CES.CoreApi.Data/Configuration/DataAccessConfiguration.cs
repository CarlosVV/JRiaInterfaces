using System.Configuration;

namespace CES.CoreApi.Foundation.Data.Configuration
{
    public class DataAccessConfiguration : ConfigurationSection
    {
        private const string DatabaseServerGroupsElement = "databaseServerGroups";

        [ConfigurationProperty(DatabaseServerGroupsElement)]
        public DatabaseServerGroups ServerGroups
        {
            get
            {
                return ((DatabaseServerGroups) (base[DatabaseServerGroupsElement]));
            }
            set { base[DatabaseServerGroupsElement] = value; }
        }
    }
}
