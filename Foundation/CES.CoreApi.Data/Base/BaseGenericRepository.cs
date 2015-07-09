using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using CES.CoreApi.Common.Attributes;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Common.Models;
using CES.CoreApi.Common.Tools;
using CES.CoreApi.Foundation.Data.Utility;
using CES.CoreApi.Logging.Interfaces;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace CES.CoreApi.Foundation.Data.Base
{
    public abstract class BaseGenericRepository
    {
        #region Core

        private static readonly int ApplicationId;
        private static readonly int AppObjectId;
        private static readonly int UserNameId;
        private readonly Database _database;
        private readonly ICacheProvider _cacheProvider;
        private readonly ILogMonitorFactory _monitorFactory;
        private readonly IIdentityManager _identityManager;

        static BaseGenericRepository()
        {
            var configSource = ConfigurationSourceFactory.Create();
            DatabaseFactory.SetDatabaseProviderFactory(new DatabaseProviderFactory(configSource));

            ApplicationId = int.Parse(ConfigurationManager.AppSettings["ApplicationID"]);
            AppObjectId = int.Parse(ConfigurationManager.AppSettings["AppObjectID"]);
            UserNameId = int.Parse(ConfigurationManager.AppSettings["UserNameID"]);
        }

        protected BaseGenericRepository(ICacheProvider cacheProvider, ILogMonitorFactory monitorFactory, IIdentityManager identityManager, DatabaseType databaseType)
        {
            if (cacheProvider == null) throw new ArgumentNullException("cacheProvider");
            if (monitorFactory == null) throw new ArgumentNullException("monitorFactory");
            if (identityManager == null) throw new ArgumentNullException("identityManager");
            if (databaseType == DatabaseType.Undefined) throw new InvalidEnumArgumentException("databaseType", (int)databaseType, typeof(DatabaseType));

            _cacheProvider = cacheProvider;
            _monitorFactory = monitorFactory;
            _identityManager = identityManager;
            DatabaseType = databaseType;
            var connectionName = databaseType.GetAttributeValue<ConnectionNameAttribute, string>(x => x.Name);
            _database = DatabaseFactory.CreateDatabase(connectionName);
        }

        #endregion

        #region Protected methods

        protected IEnumerable<TEntity> GetList<TEntity>(DatabaseRequest<TEntity> request)
        {
            ValidateRequest(request);
            
            var command = GetDbCommand(request);

            if (!request.IsCacheable) 
                return ExecuteListReaderProcedure(command, request.Shaper);

            var key = request.ToCacheKey();
            return _cacheProvider.GetItem(key, () => ExecuteListReaderProcedure(command, request.Shaper), request.CacheDuration);
        }

        protected int Add<TEntity>(DatabaseRequest<TEntity> request)
        {
            ValidateRequest(request);

            var command = GetDbCommand(request);

            var result = ExecuteScalarProcedure(command);

            return result == DBNull.Value
                ? default(int)
                : int.Parse(result.ToString());
        }

        protected void Delete<TEntity>(DatabaseRequest<TEntity> request)
        {
            ValidateRequest(request);

            var command = GetDbCommand(request);

            ExecuteNonQueryProcedure(command);
        }

        protected void Update<TEntity>(DatabaseRequest<TEntity> request)
        {
            ValidateRequest(request);

            var command = GetDbCommand(request);

            ExecuteNonQueryProcedure(command);
        }

        protected TEntity Get<TEntity>(DatabaseRequest<TEntity> request)// where TEntity : class
        {
            ValidateRequest(request);

            var command = GetDbCommand(request);

            if (!request.IsCacheable) 
                return ExecuteReaderProcedure(command, request.Shaper, request.OutputShaper);

            var key = request.ToCacheKey();
            return _cacheProvider.GetItem(key, () => ExecuteReaderProcedure(command, request.Shaper, request.OutputShaper), request.CacheDuration);
        }

        protected DatabasePingModel PingDatabase()
        {
            var databasePing = new DatabasePingModel
            {
                Database = DatabaseType
            };

            try
            {
                using (var connection = _database.CreateConnection())
                {
                    connection.Open();
                    connection.Close();
                }
                databasePing.IsOk = true;
            }
            catch (Exception ex)
            {
                HandleException(ex);
                databasePing.IsOk = false;
            }
            
            return databasePing;
        }

        #endregion

        #region Public methods

        public DatabaseType DatabaseType { get; private set; }

        #endregion
        
        #region private methods

        #region Stored procedure execution related methods

        /// <summary>
        /// Executes stored procedure, return list of entities of type TEntity
        /// </summary>
        /// <typeparam name="TEntity">Type of entity to materialize</typeparam>
        /// <param name="command">Database command to execute</param>
        /// <param name="shaper">Shaper to materialize entity</param>
        /// <returns>List of entities TEntity</returns>
        private IEnumerable<TEntity> ExecuteListReaderProcedure<TEntity>(DbCommand command, Func<IDataReader, TEntity> shaper)
        {
            var entitList = new Collection<TEntity>();

            var performanceMonitor = GetPerformanceMonitor(command);

            using (var reader = _database.ExecuteReader(command))
            {
                while (reader.Read())
                {
                    var entity = shaper(reader);
                    entitList.Add(entity);
                }
            }

            performanceMonitor.Stop();

            return entitList;
        }

        /// <summary>
        /// Executes stored procedure, return entity of type T
        /// </summary>
        /// <typeparam name="TEntity">Type of entity to materialize</typeparam>
        /// <param name="command">Database command to execute</param>
        /// <param name="shaper">Shaper to materialize entity</param>
        /// <param name="outputShaper">Output shaper, uses output SP parameters to update entity</param>
        /// <returns>Entity TEntity</returns>
        private TEntity ExecuteReaderProcedure<TEntity>(DbCommand command, Func<IDataReader, TEntity> shaper, Action<DbParameterCollection, TEntity> outputShaper)
        {
            TEntity entity;

            var performanceMonitor = GetPerformanceMonitor(command);

            using (var reader = _database.ExecuteReader(command))
            {
                entity = reader.Read() 
                    ? shaper(reader) 
                    : default (TEntity);
            }

            if (outputShaper != null)
                outputShaper(command.Parameters, entity);

            performanceMonitor.Stop();

            return entity;
        }

        /// <summary>
        /// Executes stored procedure
        /// </summary>
        /// <param name="command">Database command to execute</param>
        private void ExecuteNonQueryProcedure(DbCommand command)
        {
            var performanceMonitor = GetPerformanceMonitor(command);

            _database.ExecuteNonQuery(command);

            performanceMonitor.Stop();
        }

        /// <summary>
        /// Executes stored procedure and returns scalar value
        /// </summary>
        /// <param name="command">Database command to execute</param>
        private object ExecuteScalarProcedure(DbCommand command)
        {
            var performanceMonitor = GetPerformanceMonitor(command);

            var result = _database.ExecuteScalar(command);

            performanceMonitor.Stop();

            return result;
        }
        
        #endregion //Stored procedure execution related methods

        private DbCommand GetDbCommand<TEntity>(DatabaseRequest<TEntity> databaseRequest)
        {
            using (var command = _database.GetStoredProcCommand(databaseRequest.ProcedureName))
            {
                AddConventionParameters(databaseRequest, command);
                
                if (databaseRequest.Parameters == null)
                    return command;

                foreach (var parameter in databaseRequest.Parameters)
                {
                    command.Parameters.Add(parameter);
                }
                return command;
            }
        }

        private static void AddConventionParameters<TEntity>(DatabaseRequest<TEntity> databaseRequest, DbCommand command)
        {
            if (!databaseRequest.IncludeConventionParameters) 
                return;

            command.Parameters.Add(new SqlParameter("@fAppID", ApplicationId));
            command.Parameters.Add(new SqlParameter("@fAppObjectID", AppObjectId));
            command.Parameters.Add(new SqlParameter("@lUserNameID", UserNameId));
        }

        private static void ValidateRequest<TEntity>(DatabaseRequest<TEntity> databaseRequest)
        {
            if (databaseRequest == null) 
                throw new ArgumentNullException("databaseRequest");
            if (string.IsNullOrEmpty(databaseRequest.ProcedureName))
                throw new ApplicationException("databaseRequest.ProcedureName is empty.");
        }
        
        private IDatabasePerformanceLogMonitor GetPerformanceMonitor(DbCommand command)
        {
            var performanceMonitor = _monitorFactory.CreateNew<IDatabasePerformanceLogMonitor>();
            performanceMonitor.DataContainer.ApplicationContext = _identityManager.GetClientApplicationIdentity();
            performanceMonitor.Start(command);
            return performanceMonitor;
        }

        private void HandleException(Exception ex)
        {
            var exceptionLogMonitor = _monitorFactory.CreateNew<IExceptionLogMonitor>();
            exceptionLogMonitor.DataContainer.ApplicationContext = _identityManager.GetClientApplicationIdentity();
            exceptionLogMonitor.Publish(ex);
        }
        
        #endregion
    }
}
