using System.Collections.Generic;

namespace CES.CoreApi.Foundation.Contract.Interfaces
{
    public interface IRequestHeadersProvider
    {
        Dictionary<string, object> GetHeaders(string bindingName);
        Dictionary<string, object> GetHeaders();
    }
}