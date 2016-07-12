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
		
		public  string GetClientSetting(int applicationId)
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
									.Equals("DataProviderServiceConfiguration",StringComparison.OrdinalIgnoreCase))

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
