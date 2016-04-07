//using CES.CoreApi.Common.Models;

using CES.CoreApi.Data.Models;

namespace CES.CoreApi.Foundation.Data.Interfaces
{
    public interface IDatabasePingProvider
    {
		object PingDatabases();
    }
}