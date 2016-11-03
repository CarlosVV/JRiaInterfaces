using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace CES.CoreApi.GeoLocation.Repositories
{
	public class ClientSettingRepository
	{

		public async Task<string> GetGetClientSettingAsync(int applicationId)
		{
				return await Task.Run(() => GetClientSetting(applicationId));
		}

		public virtual string GetStateName(int applicationId, string stateCode, string country)
		{
			if (string.IsNullOrEmpty(stateCode) || string.IsNullOrEmpty(country))
				return stateCode;

			string stateName = stateCode;

			var connectionString = ConfigurationManager.ConnectionStrings["Main"].ConnectionString;
			using (var connection = new SqlConnection(connectionString))
			{
				var cmd = new SqlCommand("[dbo].[sys_sp_StatesByCountry_List]", connection)
				{
					CommandType = CommandType.StoredProcedure
				};			
				cmd.Parameters.AddWithValue("@sState", stateCode);
				cmd.Parameters.AddWithValue("@sCountry", country);
				connection.Open();
				using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.Default))
				{
					
					if (dr.Read())
					{
						stateName = dr["fState"].ToString();
					}

				}
			}
			return stateName;
		}


		public  virtual string GetClientSetting(int applicationId)
		{
			
			var connectionString = ConfigurationManager.ConnectionStrings["Main"].ConnectionString;
			using (var connection = new SqlConnection(connectionString))
			{
				var cmd = new SqlCommand("[dbo].[coreapi_sp_GetApplicationByID]", connection)
				{
					CommandType = CommandType.StoredProcedure
				};
				cmd.Parameters.AddWithValue("@applicationID", applicationId);
				connection.Open();
				using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.Default))
				{
					if (dr.Read()){}
					if(dr.NextResult())
					{			

						while (dr.Read())
						{ 
							if(Convert.ToString(dr["ConfigurationName"])
									.Equals("DataProviderClientConfiguration", StringComparison.OrdinalIgnoreCase))

							{
								return Convert.ToString(dr["ConfigurationValue"]);
							}
						}
					}

				}
			}
			return null;
		}
	}
}
