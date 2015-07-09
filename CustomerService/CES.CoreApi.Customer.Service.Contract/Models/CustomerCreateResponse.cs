using System.Runtime.Serialization;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Common.Models;
using CES.CoreApi.Customer.Service.Contract.Constants;

namespace CES.CoreApi.Customer.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.CustomerServiceDataContractNamespace)]
    public class CustomerCreateResponse: BaseResponse
    {
        public CustomerCreateResponse(ICurrentDateTimeProvider currentDateTimeProvider)
            : base(currentDateTimeProvider)
        {
        }
    }
}
