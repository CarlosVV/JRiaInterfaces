using CES.Caching;
using CES.CoreApi.Payout.Models.DTO;
using CES.Data.Sql;
using System.Collections.Generic;

namespace CES.CoreApi.Payout.Repositories
{
	public class IsoCurrencyRepository
	{
		private SqlMapper sqlMapper = DatabaseName.CreateSqlMapper();

		public  virtual IEnumerable<Currency> GetCurrencies()
		{
			var currenies = null as IEnumerable<Currency>;
			using (var sql = sqlMapper.CreateQuery(DatabaseName.Main, "[dbo].[mt_CountryCurrencyIsoCode_Get]"))
			{
				currenies = sql.Query<Currency>();
			}
			return currenies;
		}
	}
}