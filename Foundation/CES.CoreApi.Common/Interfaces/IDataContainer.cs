using CES.CoreApi.Common.Enumerations;

namespace CES.CoreApi.Common.Interfaces
{
    public interface IDataContainer
    {
        string ToString();

        LogType LogType { get; }
    }
}
