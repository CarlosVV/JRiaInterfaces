namespace CES.CoreApi.Logging.Interfaces
{
    public interface IJsonDataContainerFormatter
    {
        /// <summary>
        /// Gets log entry formatted
        /// </summary>
        /// <param name="dataContainer">Log entry data</param>
        /// <returns></returns>
        string Format(IDataContainer dataContainer);
    }
}