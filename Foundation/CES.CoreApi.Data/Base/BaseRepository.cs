using System;
using System.Data;
using System.Data.Common;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Logging.Interfaces;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace CES.CoreApi.Foundation.Data.Base
{
    public class BaseRepository 
    {
        private readonly IDatabasePerformanceLogMonitor _performanceMonitor;

        static BaseRepository()
        {
            var configSource = ConfigurationSourceFactory.Create();
            DatabaseFactory.SetDatabaseProviderFactory(new DatabaseProviderFactory(configSource), false);
        }

        public BaseRepository(ICacheProvider cacheProvider, IDatabasePerformanceLogMonitor performanceMonitor, string connectionStringName)
        {
            if (cacheProvider == null) throw new ArgumentNullException("cacheProvider");
            if (performanceMonitor == null) throw new ArgumentNullException("performanceMonitor");
            if (connectionStringName == null) throw new ArgumentNullException("connectionStringName");

            _performanceMonitor = performanceMonitor;
            CacheProvider = cacheProvider;
            Database = DatabaseFactory.CreateDatabase(connectionStringName);
        }

        protected Database Database { get; private set; }

        protected ICacheProvider CacheProvider { get; private set; }

        public void PingDatabase()
        {
            using (var connection = Database.CreateConnection())
            {
                connection.Open();
                connection.Close();
            }
        }

        public TEntity ExecuteReader<TEntity>(DbCommand command, Func<IDataReader, TEntity> shaper)
        {
            TEntity entity;

            _performanceMonitor.Start(command);

            using (var reader = Database.ExecuteReader(command))
            {
                entity = shaper(reader);
            }

            _performanceMonitor.Stop();

            return entity;
        }
    }
}