using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using CES.CoreApi.Foundation.Data.Configuration;
using CES.CoreApi.Foundation.Data.Interfaces;
using CES.CoreApi.Foundation.Data.Models;

namespace CES.CoreApi.Foundation.Data.Providers
{
    public class DatabaseConfigurationProvider : IDatabaseConfigurationProvider
    {
        #region Core

        private const string DataAccessConfigurationConfig = "dataAccessConfiguration";
        private ICollection<ConnectionConfiguration> _configurationList; 

        #endregion
        
        #region IDatabaseConfigurationProvider implementation

        public ICollection<ConnectionConfiguration> GetConfiguration()
        {
            return _configurationList ??
                   (_configurationList = PopulateConfiguration());
        }

        public IEnumerable<string> GetDatabaseNameList()
        {
            return GetConfiguration()
                .GroupBy(p => p.GroupName)
                .Select(p => p.First()
                    .GroupName);
        } 

        #endregion

        #region Private methods

        private static ICollection<ConnectionConfiguration> PopulateConfiguration()
        {
            var section = ConfigurationManager.GetSection(DataAccessConfigurationConfig) as DataAccessConfiguration;

            if (section == null)
                throw new ApplicationException("Section 'dataAccessConfiguration' is not found in configuration file.");

            var configurationList = new Collection<ConnectionConfiguration>();

            for (var i = 0; i < section.ServerGroups.Count; i++)
            {
                var group = section.ServerGroups[i];
                for (var j = 0; j < group.Count; j++)
                {
                    var server = group[j];
                    var configurationItem = new ConnectionConfiguration
                    {
                        GroupName = @group.Name,
                        ServerName = server.Name,
                        DatabaseId = server.DatabaseId,
                        IsDefault = server.IsDefault,
                        ConnectionString = server.ConnectionString,
                        ProviderName = server.ProviderName
                    };
                    configurationList.Add(configurationItem);
                }
            }

            return configurationList;
        } 

        #endregion
    }
}