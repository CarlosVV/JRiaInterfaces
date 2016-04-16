using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Foundation.Data.Factory
{
	public class CoreDatabaseFactory
	{

		public static DbConnection CreateDbConnection(string connectionString, string provider = "System.Data.SqlClient")
		{
		

			var dbConnection =		   new GenericDatabase(connectionString,
						   DbProviderFactories.GetFactory(provider));

			return dbConnection.CreateConnection(); 
		}

		public static Database CreateDb(string connectionString, string provider = "System.Data.SqlClient")
		{
			//var df = DatabaseFactory.CreateDatabase();

			var db = new GenericDatabase(connectionString,
						   DbProviderFactories.GetFactory(provider));

			return db;
		}
	}
}
