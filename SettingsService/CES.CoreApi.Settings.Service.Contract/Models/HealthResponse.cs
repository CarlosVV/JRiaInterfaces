using System.Collections.Generic;
using System.Runtime.Serialization;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Common.Models;
using CES.CoreApi.Settings.Service.Contract.Constants;

namespace CES.CoreApi.Settings.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.SettingsServiceDataContractNamespace)]
    public class HealthResponse : BaseResponse
    {
        public HealthResponse(ICurrentDateTimeProvider currentDateTimeProvider)
            : base(currentDateTimeProvider)
        {
        }

        [DataMember]
        public ICollection<DatabasePingModel> Databases { get; set; }
    }
}
