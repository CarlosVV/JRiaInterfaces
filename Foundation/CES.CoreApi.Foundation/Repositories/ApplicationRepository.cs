using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
//using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Exceptions;
using CES.CoreApi.Foundation.Contract.Interfaces;
using CES.CoreApi.Data.Base;
using CES.CoreApi.Foundation.Data.Utility;
using CES.CoreApi.Data.Models;
using CES.CoreApi.Data.Enumerations;
using CES.CoreApi.Foundation.Contract.Models;

namespace CES.CoreApi.Foundation.Repositories
{
	public class ApplicationRepository : BaseGenericRepository, IApplicationRepository
	{
		

		/// <summary>
		/// Gets application instance by ID
		/// </summary>
		/// <param name="applicationId">Application ID</param>
		/// <returns></returns>
		public async Task<Application> GetApplication(int applicationId)
		{
			var request = new DatabaseRequest<Application>
			{
				ProcedureName = "coreapi_sp_GetApplicationByID",
				IsCacheable = false,
				DatabaseType = DatabaseType.Main,
				Parameters = new Collection<SqlParameter>
				{
					new SqlParameter("@applicationID", applicationId)
				},
				Shaper = reader => GetApplication(reader, applicationId)
			};

			return await Task.Run(() => Get(request));
		}

		/// <summary>
		/// Gets application configuration items collection
		/// </summary>
		/// <param name="applicationId">Application identifier</param>
		/// <returns></returns>
		public async Task<ICollection<ApplicationConfiguration>> GetApplicationConfiguration(int applicationId)
		{
			var application = await GetApplication(applicationId);

			if (application == null)
				throw new CoreApiException(Common.Enumerations.TechnicalSubSystem.CoreApiData, Common.Enumerations.SubSystemError.ApplicationNotFoundInDatabase, applicationId);

			return application.Configuration;
		}

		private static Application GetApplication(IDataReader reader, int applicationId)
		{
			var application = InitializeApplication(reader, applicationId);
			if (application == null)
				return null;

			reader.NextResult();

			InitializeConfigurationList(reader, application);
			reader.NextResult();

			InitializeOperationList(reader, application);
			reader.NextResult();

			InitializeAssignedApplicationList(reader, application);
			reader.NextResult();

			return application;
		}

		private static Application InitializeApplication(IDataReader reader, int applicationId)
		{
			return new Application(
				applicationId,
				reader.ReadValue<string>("Name"),
				reader.ReadValue<bool>("IsActive"));
		}

		private static void InitializeConfigurationList(IDataReader reader, Application application)
		{
			while (reader.Read())
			{
				var configurationItem = new ApplicationConfiguration(
					reader.ReadValue<string>("ConfigurationName"),
					reader.ReadValue<string>("ConfigurationValue"));
				application.Configuration.Add(configurationItem);
			}
		}

		private static void InitializeOperationList(IDataReader reader, Application application)
		{
			while (reader.Read())
			{
				var operation = new ServiceOperation(
					reader.ReadValue<int>("ServiceOperationID"),
					reader.ReadValue<string>("MethodName"),
					reader.ReadValue<bool>("IsActive"));

				application.Operations.Add(operation);
			}
		}

		private static void InitializeAssignedApplicationList(IDataReader reader, Application application)
		{
			while (reader.Read())
			{
				var operationId = reader.ReadValue<int>("ServiceOperationID");
				var foundOperation = application.Operations.FirstOrDefault(o => o.Id == operationId);
				if (foundOperation == null) continue;

				var applicationOperation = new ApplicationServiceOperation(
					reader.ReadValue<int>("ApplicationID"),
					reader.ReadValue<bool>("IsActive"));

				foundOperation.AssignedApplications.Add(applicationOperation);
			}
		}
	}
}
