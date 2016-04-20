

using CES.CoreApi.Logging.Enumerations;

namespace CES.CoreApi.Logging.Interfaces
{
    public interface IDataContainer
    {
        string ToString();

        LogType LogType { get; }
    }
}
