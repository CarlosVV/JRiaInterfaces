using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using CES.CoreApi.Data.Models;
using Microsoft.Practices.EnterpriseLibrary.Data;
using CES.CoreApi.Data.Enumerations;
using CES.CoreApi.Data.Configuration;

namespace CES.CoreApi.Data.Base
{
	public abstract class BaseGenericRepository
    {
        #region Core

        private static readonly int ApplicationId;
        private static readonly int AppObjectId;
        private static readonly int UserNameId;
       // private readonly ICacheProvider _cacheProvider;
        //private readonly ILogMonitorFactory _monitorFactory;
       // private readonly IIdentityManager _identityManager;
       // private readonly IDatabaseInstanceProvider _instanceProvider;

        static BaseGenericRepository()
        {
            //var configSource = ConfigurationSourceFactory.Create();
            //DatabaseFactory.SetDatabaseProviderFactory(new DatabaseProviderFactory(configSource));

            ApplicationId = int.Parse(ConfigurationManager.AppSettings["ApplicationID"]);
            AppObjectId = int.Parse(ConfigurationManager.AppSettings["AppObjectID"]);
            UserNameId = int.Parse(ConfigurationManager.AppSettings["UserNameID"]);
        }

        //protected BaseGenericRepository(            )
        //{
        //   // if (cacheProvider == null) throw new ArgumentNullException("cacheProvider");
        //   // if (monitorFactory == null) throw new ArgumentNullException("monitorFactory");
        //   // if (identityManager == null) throw new ArgumentNullException("identityManager");
        //    if (instanceProvider == null) throw new ArgumentNullException("instanceProvider");
            
        // //   _cacheProvider = cacheProvider;
        //  //  _monitorFactory = monitorFactory;
        //   // _identityManager = identityManager;
        //    _instanceProvider = instanceProvider;
        //}

        #endregion

        #region Protected methods

        protected IEnumerable<TEntity> GetList<TEntity>(DatabaseRequest<TEntity> request)
        {
            ValidateRequest(request);
            
            var command = GetDbCommand(request);

            //if (!request.IsCacheable) 
                return ExecuteListReaderProcedure(command, request.Shaper);

            //var key = request.ToCacheKey();
            //return _cacheProvider.GetItem(key, () => ExecuteListReaderProcedure(command, request.Shaper), request.CacheDuration);
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

           // if (!request.IsCacheable) 
                return ExecuteReaderProcedure(command, request.Shaper, request.OutputShaper, request.OutputFuncShaper);

           // var key = request.ToCacheKey(request.CacheKeySuffix);
            //return _cacheProvider.GetItem(key,
            //    () => ExecuteReaderProcedure(command, request.Shaper, request.OutputShaper),
            //    request.CacheDuration,
            //    request.CacheInvalidator);
        }

        #endregion
        
        #region private methods

        #region Stored procedure execution related methods

        /// <summary>
        /// Executes stored procedure, return list of entities of type TEntity
        /// </summary>
        /// <typeparam name="TEntity">Type of entity to materialize</typeparam>
        /// <param name="commandContext">Database command context to execute</param>
        /// <param name="shaper">Shaper to materialize entity</param>
        /// <returns>List of entities TEntity</returns>
        private IEnumerable<TEntity> ExecuteListReaderProcedure<TEntity>(CommandContext commandContext, Func<IDataReader, TEntity> shaper)
        {
            var entitList = new Collection<TEntity>();

           // var performanceMonitor = GetPerformanceMonitor(commandContext.Command);

            using (var reader = commandContext.Database.ExecuteReader(commandContext.Command))
            {
                while (reader.Read())
                {
                    var entity = shaper(reader);
                    entitList.Add(entity);
                }
               // performanceMonitor.UpdateConnectionDetails(commandContext.Command);
            }

           // performanceMonitor.Stop();

            return entitList;
        }

        /// <summary>
        /// Executes stored procedure, return entity of type T
        /// </summary>
        /// <typeparam name="TEntity">Type of entity to materialize</typeparam>
        /// <param name="commandContext">Database command context to execute</param>
        /// <param name="shaper">Shaper to materialize entity</param>
        /// <param name="outputShaper">Output shaper, uses output SP parameters to update entity</param>
        /// <returns>Entity TEntity</returns>
        private TEntity ExecuteReaderProcedure<TEntity>(CommandContext commandContext, Func<IDataReader, TEntity> shaper, Action<DbParameterCollection, TEntity> outputShaper, Func<DbParameterCollection, TEntity> outputFuncShaper = null)
        {
            TEntity entity;

           // var performanceMonitor = GetPerformanceMonitor(commandContext.Command);

            using (var reader = commandContext.Database.ExecuteReader(commandContext.Command))
            {
                entity = reader.Read() 
                    ? shaper(reader) 
                    : default (TEntity);

                //performanceMonitor.UpdateConnectionDetails(commandContext.Command);
            }

            if (outputShaper != null)
                outputShaper(commandContext.Command.Parameters, entity);

            if (outputFuncShaper != null)
                entity = outputFuncShaper(commandContext.Command.Parameters);

            //  performanceMonitor.Stop();

            return entity;
        }

        /// <summary>
        /// Executes stored procedure
        /// </summary>
        /// <param name="commandContext">Database command context to execute</param>
        private void ExecuteNonQueryProcedure(CommandContext commandContext)
        {
           // var performanceMonitor = GetPerformanceMonitor(commandContext.Command);

            commandContext.Database.ExecuteNonQuery(commandContext.Command);

          //  performanceMonitor.Stop();
        }

        /// <summary>
        /// Executes stored procedure and returns scalar value
        /// </summary>
        /// <param name="commandContext">Database command context to execute</param>
        private object ExecuteScalarProcedure(CommandContext commandContext)
        {
            //var performanceMonitor = GetPerformanceMonitor(commandContext.Command);

            var result = commandContext.Database.ExecuteScalar(commandContext.Command);

           // performanceMonitor.Stop();

            return result;
        }
        
        #endregion //Stored procedure execution related methods

        #region Helper methods

        private CommandContext GetDbCommand<TEntity>(DatabaseRequest<TEntity> databaseRequest)
        {
			
			var databaseCommand = new CommandContext
			{
				Database = new GenericDatabase(GetDbString(databaseRequest.DatabaseType)
				, DbProviderFactories.GetFactory("System.Data.SqlClient"))
				
            };

            using (databaseCommand.Command = databaseCommand.Database.GetStoredProcCommand(databaseRequest.ProcedureName))
            {
                AddConventionParameters(databaseRequest, databaseCommand.Command);
                
                if (databaseRequest.Parameters == null)
                    return databaseCommand;

                foreach (var parameter in databaseRequest.Parameters)
                {
                    databaseCommand.Command.Parameters.Add(parameter);
                }
                return databaseCommand;
            }
        }

		private string GetDbString(DatabaseType type)
		{
			switch (type)
			{			
			
				case DatabaseType.ReadOnly:
					return DbConnectionString.Readonly;
				case DatabaseType.FrontEnd:
					return DbConnectionString.FrontEnd;
				case DatabaseType.Image:
					return DbConnectionString.Image;
				default:
					return DbConnectionString.Main;
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
            if (databaseRequest.DatabaseType == DatabaseType.Undefined)
                // ReSharper disable NotResolvedInText
                throw new InvalidEnumArgumentException("databaseRequest.DatabaseType", (int)databaseRequest.DatabaseType, typeof(DatabaseType));
                // ReSharper restore NotResolvedInText
            if (string.IsNullOrEmpty(databaseRequest.ProcedureName))
                throw new ApplicationException("databaseRequest.ProcedureName is empty.");
        }
        
        //private IDatabasePerformanceLogMonitor GetPerformanceMonitor(DbCommand command)
        //{
        //    var performanceMonitor = _monitorFactory.CreateNew<IDatabasePerformanceLogMonitor>();
        //    performanceMonitor.DataContainer.ApplicationContext = _identityManager.GetClientApplicationIdentity();
        //    performanceMonitor.Start(command);
        //    return performanceMonitor;
        //}

        #endregion

        #endregion
    }
}
