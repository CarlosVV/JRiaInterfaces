using System.Collections.Generic;

namespace CES.CoreApi.Common.Models
{
    public class PingResponseModel
    {
        public IEnumerable<DatabasePingModel> Databases { get; set; }
    }
}
