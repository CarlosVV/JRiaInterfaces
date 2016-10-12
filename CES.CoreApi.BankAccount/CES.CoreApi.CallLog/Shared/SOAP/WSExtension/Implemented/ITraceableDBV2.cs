
namespace CES.CoreApi.CallLog.Shared.SOAP.WSExtension.Implemented
{
    /// <summary>
    /// Extends the functionality of ITraceableDB
    /// </summary>
    public interface ITraceableDBV2 : ITraceableDB
    {
        //@@2014-09-10 lb SCR# 2124211 Created (SCR used to allocated this change)
        /// <summary>
        /// Indicates the web extension that carries the functionality to NOT insert log entries in the database for requests and responses, if true
        /// </summary>
        bool SuppressDBLog { get; }
    }
}
