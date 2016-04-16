using System;
//using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Foundation.Data.Interfaces;
using CES.CoreApi.Foundation.Data.Factory;
//using CES.CoreApi.Logging.Interfaces;

namespace CES.CoreApi.Foundation.Data.Providers
{
	public class DatabasePingProvider : IDatabasePingProvider
    {
        #region Core


        #endregion

        #region IDatabasePingProvider implementation

        public object PingDatabases()
        {
            return new 
			{
                //Databases = from name in _configurationProvider.GetDatabaseNameList()
                //            select PingDatabase(name)
            };
        }
        
        #endregion
        
        #region Private methods

        private object  PingDatabase(string groupName)
        {
			var pingResult = new  { Database = groupName, IsOk = true
				};

			try
			{
                var database = CoreDatabaseFactory.CreateDb(groupName);
                using (var connection = database.CreateConnection())
                {
                    connection.Open();
                    connection.Close();
                }
				//pingResult.IsOk = true;
			}
            catch (Exception)
            {
				throw;
				//HandleException(ex);
				//pingResult.IsOk = false;
			}

			// 

			return pingResult;
		}

	
		#endregion
	}
}
