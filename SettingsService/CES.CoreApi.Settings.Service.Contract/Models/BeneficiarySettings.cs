using System.Runtime.Serialization;
using CES.CoreApi.Common.Models;
using CES.CoreApi.Settings.Service.Contract.Constants;
using CES.CoreApi.Settings.Service.Contract.Enumerations;

namespace CES.CoreApi.Settings.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.SettingsServiceDataContractNamespace)]
    public class BeneficiarySettings : ExtensibleObject
    {
        [DataMember]
        public RecurrentBeneficiarySetting EnableRecurrent { get; set; }

        [DataMember]
        public RecurrentBeneficiarySetting EnableRecurrentTab { get; set; }

        [DataMember]
        public BeneficiaryConsolidationSetting EnableConsolidation { get; set; }
    }
}
