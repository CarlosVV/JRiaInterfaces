using CES.CoreApi.Common.Enumerations;

namespace CES.CoreApi.Logging.Interfaces
{
    public interface IDataContainer
    {
        string ToString();

        LogType LogType { get; }
    }
}
