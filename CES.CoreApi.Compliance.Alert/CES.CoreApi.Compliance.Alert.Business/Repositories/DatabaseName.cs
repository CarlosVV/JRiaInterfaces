using CES.Data.Sql;
using System.Configuration;

namespace CES.CoreApi.Compliance.Alert.Business.Repositories
{
	public class DatabaseName
	{
		public static readonly string Main = "main";

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
