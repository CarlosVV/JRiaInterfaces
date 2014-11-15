namespace CES.CoreApi.Logging.Interfaces
{
    public interface IFullMethodNameFormatter
    {
        /// <summary>
        /// Gets full method name including fully qualified assembly name
        /// </summary>
        /// <param name="fullTypeName">Fully qualified type name</param>
        /// <param name="methodName">Method name</param>
        /// <returns></returns>
        string Format(string fullTypeName, string methodName);
    }
}