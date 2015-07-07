using System.Collections.Generic;
using System.Runtime.Serialization;
using CES.CoreApi.Accounting.Service.Contract.Constants;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Common.Models;

namespace CES.CoreApi.Accounting.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.AccountingServiceDataContractNamespace)]
    public class HealthMonitoringResponse: BaseResponse
    {
        public HealthMonitoringResponse(ICurrentDateTimeProvider currentDateTimeProvider) 
            : base(currentDateTimeProvider)
        {
        }

        [DataMember]
        public ICollection<DatabasePingModel> Databases { get; set; }
    }
}
