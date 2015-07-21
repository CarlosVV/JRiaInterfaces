using System.Collections.Generic;
using CES.CoreApi.Foundation.Data.Models;

namespace CES.CoreApi.Foundation.Data.Interfaces
{
    public interface IDatabaseConfigurationProvider
    {
        ICollection<ConnectionConfiguration> GetConfiguration();
        IEnumerable<string> GetDatabaseNameList();
    }
}