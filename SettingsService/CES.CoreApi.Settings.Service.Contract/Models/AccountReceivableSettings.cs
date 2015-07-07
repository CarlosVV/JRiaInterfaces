using System;
using System.Runtime.Serialization;
using CES.CoreApi.Common.Models;
using CES.CoreApi.Settings.Service.Contract.Constants;
using CES.CoreApi.Settings.Service.Contract.Enumerations;

namespace CES.CoreApi.Settings.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.SettingsServiceDataContractNamespace)]
    public class AccountReceivableSettings : ExtensibleObject
    {
        [DataMember]
        public AccountReceivableLimitDisplayOption LimitDisplayOption { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public DateTime LimitDepositTime { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string FaxNo { get; set; }
    }
}
