using System.Collections.Generic;
using System.ServiceModel;

namespace CES.CoreApi.Foundation.Contract.Interfaces
{
    public interface IClientSecurityContextProvider
    {
        /// <summary>
        /// Provides client applicaiton details as a dictionary
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        IDictionary<string, object> GetDetails(OperationContext context);
    }
}