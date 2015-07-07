using System.Runtime.Serialization;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Models;
using CES.CoreApi.Customer.Service.Contract.Constants;

namespace CES.CoreApi.Customer.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.CustomerServiceDataContractNamespace)]
    public class DatabasePingResponse: ExtensibleObject
    {
        [DataMember]
        public DatabaseType Database { get; set; }
        [DataMember]
        public bool IsOk { get; set; }
    }
}
