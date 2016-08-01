using CES.Data.Sql;
using System;
using System.Data;

namespace CES.CoreApi.Payout.Repositories
{
	public class PersistenceRepository
	{
		private SqlMapper sqlMapper = DatabaseName.CreateSqlMapper();
	
		internal int GetPersistenceId(int userId)
		{
			int persistenceId = 0;
			using (var sql = sqlMapper.CreateCommand(DatabaseName.Main, "coreApi_sp_PresistenceCreate"))
			{
				sql.AddParam("@fDisabled", false);
				sql.AddParam("@fDelete", false);
				sql.AddParam("@fChanged", false);
				sql.AddParam("@fTime", DateTime.UtcNow);
				sql.AddParam("@fModified", false);
				sql.AddParam("@fModifiedID", userId);
				var persistenceIdData = sql.AddOutputParam("@fPersistenceID", SqlDbType.Int);
				sql.Execute();
				persistenceId = persistenceIdData.GetSafeValue<int>();

			}
			return persistenceId;
		}
	}
}