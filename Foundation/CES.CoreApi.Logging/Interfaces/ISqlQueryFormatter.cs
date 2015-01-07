using CES.CoreApi.Logging.Models;

namespace CES.CoreApi.Logging.Interfaces
{
    public interface ISqlQueryFormatter
    {
        string Format(DatabasePerformanceLogDataContainer container);
    }
}