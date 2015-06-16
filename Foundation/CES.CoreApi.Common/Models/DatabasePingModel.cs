using CES.CoreApi.Common.Enumerations;

namespace CES.CoreApi.Common.Models
{
    public class DatabasePingModel
    {
        public DatabaseType Database { get; set; }
        public bool IsOk { get; set; }
    }
}