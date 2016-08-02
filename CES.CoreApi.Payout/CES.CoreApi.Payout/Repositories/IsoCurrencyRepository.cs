using CES.CoreApi.Payout.Models.DTO;
using CES.Data.Sql;
using System.Collections.Generic;

namespace CES.CoreApi.Payout.Repositories
{
	public class IsoCurrencyRepository
	{
		private SqlMapper _sqlMapper = DatabaseName.CreateSqlMapper();
		/// <summary>
		/// Get Currency Iso Mapper 
		/// </summary>
		/// <returns></returns>
		public  virtual IEnumerable<Currency> GetCurrencies()
		{
			var currenies = null as IEnumerable<Currency>;
			using (var sql = _sqlMapper.CreateQuery(DatabaseName.Main, "[dbo].[mt_CountryCurrencyIsoCode_Get]"))
			{
				currenies = sql.Query<Currency>();
			}
			return currenies;
		}
	}
}