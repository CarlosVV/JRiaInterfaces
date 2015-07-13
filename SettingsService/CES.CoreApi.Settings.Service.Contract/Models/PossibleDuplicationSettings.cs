using System.Runtime.Serialization;
using CES.CoreApi.Common.Models;
using CES.CoreApi.Settings.Service.Contract.Constants;
using CES.CoreApi.Settings.Service.Contract.Enumerations;

namespace CES.CoreApi.Settings.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.SettingsServiceDataContractNamespace)]
    public class PossibleDuplicationSettings : ExtensibleObject
    {
        [DataMember]
        public PossibleDuplicateSetting ByNames { get; set; }

        [DataMember]
        public PossibleDuplicateSetting ByNamesDob { get; set; }

        [DataMember]
        public PossibleDuplicateSetting ByNamesDobId { get; set; }

        [DataMember]
        public PossibleDuplicateSetting ByNamesId { get; set; }

        [DataMember]
        public PossibleDuplicateSetting ById { get; set; }

        [DataMember]
        public PossibleDuplicateCustomerCreationSetting CustomerCreateByNames { get; set; }

        [DataMember]
        public PossibleDuplicateCustomerCreationSetting CustomerCreateByNamesDob { get; set; }

        [DataMember]
        public PossibleDuplicateCustomerCreationSetting CustomerCreateByNamesDobId { get; set; }

        [DataMember]
        public PossibleDuplicateCustomerCreationSetting CustomerCreateByNamesId { get; set; }

        [DataMember]
        public PossibleDuplicateCustomerCreationSetting CustomerCreateById { get; set; }

        [DataMember]
        public PossibleDuplicateCustomerActionSetting CustomerActionByNames { get; set; }

        [DataMember]
        public PossibleDuplicateCustomerActionSetting CustomerActionByNamesDob { get; set; }

        [DataMember]
        public PossibleDuplicateCustomerActionSetting CustomerActionByNamesDobId { get; set; }

        [DataMember]
        public PossibleDuplicateCustomerActionSetting CustomerActionByNamesId { get; set; }

        [DataMember]
        public PossibleDuplicateCustomerActionSetting CustomerActionById { get; set; }
    }
}