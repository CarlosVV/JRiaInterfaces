using CES.CoreApi.Payout.Service.Business.Logic.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;


namespace CES.CoreApi.Payout.Service.Business.Logic.Repositories
{
	public class ConfigurationRepository
	{
		public virtual List<ServiceConfiguration> GetConfigurations()
		{
			var configs = new List<ServiceConfiguration>();

			var connectionString = ConfigurationManager.ConnectionStrings["Main"].ConnectionString;
			using (var connection = new SqlConnection(connectionString))
			{
				var cmd = new SqlCommand("[dbo].[coreapi_sp_GetProvidersByTypeID]", connection)
				{
					CommandType = CommandType.StoredProcedure
				};
				cmd.Parameters.AddWithValue("@providerTypeID", 1);
				cmd.Parameters.AddWithValue("@bConfigOnly", true);
				connection.Open();
				using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.Default))
				{

					while (dr.Read())
					{
						configs.Add(new ServiceConfiguration
						{
							ProviderId = Convert.ToInt32(dr["ProviderId"]),
							ProviderName = Convert.ToString(dr["ProviderName"]),
							Settings = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ApiSetting>>(Convert.ToString(dr["Settings"]))

						});
					}

				}
			}
			return configs;
		}

	}
}
