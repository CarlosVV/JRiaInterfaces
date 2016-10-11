using CES.Data.Sql;
using System.Configuration;

namespace CES.CoreApi.PushNotifications.Repositories
{
	/// <summary>
	///This is to register connection strings.
	///If you don’t use main and  ReadOnlyTransactional please remove them or replace them
	/// </summary>
	public class DatabaseName
	{
		public static readonly string Main = "main";
		public static readonly string ReadOnlyTransactional = "readonlyTransactional";


		internal static SqlMapper CreateSqlMapper()
		{
			var sqlMapper = new SqlMapper();
			foreach (ConnectionStringSettings item in ConfigurationManager.ConnectionStrings)
			{
				sqlMapper.AddConnectionSetting(item.Name, item.ConnectionString, 30);
			}
			return sqlMapper;
		}
	}
}