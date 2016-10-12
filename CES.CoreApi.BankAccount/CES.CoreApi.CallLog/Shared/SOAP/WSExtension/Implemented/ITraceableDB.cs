
namespace CES.CoreApi.CallLog.Shared.SOAP.WSExtension.Implemented
{
    /// <summary>
    /// Supports the DBExtension SOAP Extension to log every client call and response in database
    /// </summary>
    public interface ITraceableDB: ITraceable
    {
        //@@2012-06-18 lb SCR# 665511 Created
        int TransactionID {get; }
        int ServiceID { get; }
        string Context {get; }
    }
}
