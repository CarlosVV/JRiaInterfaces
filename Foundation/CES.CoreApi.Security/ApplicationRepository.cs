using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System;
using CES.CoreApi.Security.Models;
using System.Configuration;

namespace CES.CoreApi.Security
{
	public class ApplicationSecurityRepository
	{
		public async Task<Application> GetApplication(int applicationId)
		{
			//var request = new DatabaseRequest<Application>
			//{
			//	ProcedureName = "coreapi_sp_GetApplicationByID",
			//	IsCacheable = false,
			//	DatabaseType = DatabaseType.Main,
			//	Parameters = new Collection<SqlParameter>
			//	{
			//		new SqlParameter("@applicationID", applicationId)
			//	},
			//	Shaper = reader => GetApplication(reader, applicationId)
			//};

			return await Task.Run(() => GetApplicationById(applicationId));
		}
		//public static Application GetApplicationById(int applicationId)
		//{
			
		//	return GetApplicationInfo(applicationId);
		//}
		public static  Application GetApplicationById(int appId)
		{
			Application app = null;
			var connectionString = ConfigurationManager.ConnectionStrings["Main"].ConnectionString;
			using (var connection = new SqlConnection(connectionString)) 
			{
				var cmd = new SqlCommand("[dbo].[coreapi_sp_GetApplicationByID]", connection)
				{
					CommandType = CommandType.StoredProcedure
				};
				cmd.Parameters.AddWithValue("@applicationID", appId);
				connection.Open();
				using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.Default))
				{
					if (dr.Read())
					{
						app = new Application(appId, Convert.ToString(dr["Name"]),
							Convert.ToBoolean(dr["IsActive"]));
					}
					
				}
			}
			return app;			
		}


		//public Application GetApplicationInfo(int applicationId)
		//{
		//	var request = new DatabaseRequest<Application>
		//	{
		//		ProcedureName = "coreapi_sp_GetApplicationByID",
		//		IsCacheable = false,
		//		DatabaseType = DatabaseType.Main,
		//		Parameters = new Collection<SqlParameter>
		//		{
		//			new SqlParameter("@applicationID", applicationId)
		//		},
		//		Shaper = reader => GetApplication(reader, applicationId)
		//	};
		//	var app = Get(request);
		//	return app;
		//}
	
		
		//private static Application GetApplication(IDataReader reader, int applicationId)
		//{
		//	var application = InitializeApplication(reader, applicationId);
		//	if (application == null)
		//		return null;

		//	reader.NextResult();

		//	InitializeConfigurationList(reader, application);
		//	reader.NextResult();

		//	InitializeOperationList(reader, application);
		//	reader.NextResult();

		//	InitializeAssignedApplicationList(reader, application);
		//	reader.NextResult();

		//	return application;
		//}

		//private static Application InitializeApplication(IDataReader reader, int applicationId)
		//{
		//	return new Application(
		//		applicationId,
		//		reader.ReadValue<string>("Name"),
		//		reader.ReadValue<bool>("IsActive"));
		//}

		//private static void InitializeConfigurationList(IDataReader reader, Application application)
		//{
		//	while (reader.Read())
		//	{
		//		var configurationItem = new ApplicationConfiguration(
		//			reader.ReadValue<string>("ConfigurationName"),
		//			reader.ReadValue<string>("ConfigurationValue"));
		//		application.Configuration.Add(configurationItem);
		//	}
		//}

		//private static void InitializeOperationList(IDataReader reader, Application application)
		//{
		//	while (reader.Read())
		//	{
		//		var operation = new ServiceOperation(
		//			reader.ReadValue<int>("ServiceOperationID"),
		//			reader.ReadValue<string>("MethodName"),
		//			reader.ReadValue<bool>("IsActive"));

		//		application.Operations.Add(operation);
		//	}
		//}

		//private static void InitializeAssignedApplicationList(IDataReader reader, Application application)
		//{
		//	while (reader.Read())
		//	{
		//		var operationId = reader.ReadValue<int>("ServiceOperationID");
		//		var foundOperation = application.Operations.FirstOrDefault(o => o.Id == operationId);
		//		if (foundOperation == null) continue;

		//		var applicationOperation = new ApplicationServiceOperation(
		//			reader.ReadValue<int>("ApplicationID"),
		//			reader.ReadValue<bool>("IsActive"));

		//		foundOperation.AssignedApplications.Add(applicationOperation);
		//	}
		//}
	}
}
