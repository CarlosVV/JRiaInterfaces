using System.Collections.Generic;
using System.Runtime.Serialization;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Common.Models;
using CES.CoreApi.Customer.Service.Contract.Constants;

namespace CES.CoreApi.Customer.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.CustomerServiceDataContractNamespace)]
    public class HealthResponse: BaseResponse
    {
        public HealthResponse(ICurrentDateTimeProvider currentDateTimeProvider) 
            : base(currentDateTimeProvider)
        {
        }

        [DataMember]
        public ICollection<DatabasePingModel> Databases { get; set; }
    }
}
