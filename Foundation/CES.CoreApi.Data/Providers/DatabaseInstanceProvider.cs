using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Globalization;
using System.Linq;
//using CES.CoreApi.Common.Attributes;
//using CES.CoreApi.Common.Enumerations;
//using CES.CoreApi.Common.Tools;
using CES.CoreApi.Foundation.Data.Interfaces;
using CES.CoreApi.Foundation.Data.Models;
using Microsoft.Practices.EnterpriseLibrary.Data;
using CES.CoreApi.Data.Enumerations;
using CES.CoreApi.Data.Attributes;
using CES.CoreApi.Data.Tools;

namespace CES.CoreApi.Foundation.Data.Providers
{
    public class DatabaseInstanceProvider : IDatabaseInstanceProvider
    {
        #region Core

        private readonly ICollection<ConnectionConfiguration> _connectionConfiguration;

        public DatabaseInstanceProvider(IDatabaseConfigurationProvider configurationProvider)
        {
            if (configurationProvider == null)
                throw new ArgumentNullException("configurationProvider");

            _connectionConfiguration = configurationProvider.GetConfiguration();
        } 

        #endregion

        #region IDatabaseInstanceProvider implementation

        public Database GetDatabase(DatabaseType databaseType, int databaseId = 0)
        {
            var groupName = databaseType.GetAttributeValue<ConnectionNameAttribute, string>(x => x.Name);
            return GetDatabase(groupName, databaseId);
        }

        public Database GetDatabase(string groupName, int databaseId = 0)
        {
            var configItems = _connectionConfiguration
                .Where(p => p.GroupName.Equals(groupName, StringComparison.OrdinalIgnoreCase))
                .ToList();

            var errorMessage = string.Format(CultureInfo.InvariantCulture,
                "Configuration is not found for database '{0}' and database ID = '{1}'.", groupName, databaseId);

            if (configItems == null || configItems.Count == 0)
                throw new ApplicationException(errorMessage);

            ConnectionConfiguration configItem = null;

            //Find by database ID
            if (databaseId != 0)
            {
                configItem = configItems.FirstOrDefault(p => p.DatabaseId == databaseId);
            }
            else if (configItems.Count() == 1)
            {
                configItem = configItems[0];
            }
            //Find default connection
            else if (configItems.Count() > 1)
            {
                configItem = configItems.FirstOrDefault(p => p.IsDefault);
            }
            
            if (configItem == null)
                throw new ApplicationException(errorMessage);

            return configItem.Database ??
                   (configItem.Database =
                       new GenericDatabase(configItem.ConnectionString,
                           DbProviderFactories.GetFactory(configItem.ProviderName)));
        } 

        #endregion
    }
}
