using System.Collections.Generic;
using System.Runtime.Serialization;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Common.Models;
using CES.CoreApi.Settings.Service.Contract.Constants;

namespace CES.CoreApi.Settings.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.SettingsServiceDataContractNamespace)]
    public class PingResponse : BaseResponse
    {
        public PingResponse(ICurrentDateTimeProvider currentDateTimeProvider)
            : base(currentDateTimeProvider)
        {
        }

        [DataMember]
        public ICollection<DatabasePingResponse> Databases { get; set; }
    }
}
