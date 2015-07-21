using CES.CoreApi.Common.Enumerations;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace CES.CoreApi.Foundation.Data.Interfaces
{
    public interface IDatabaseInstanceProvider
    {
        Database GetDatabase(DatabaseType databaseType, int databaseId = 0);
        Database GetDatabase(string groupName, int databaseId = 0);
    }
}