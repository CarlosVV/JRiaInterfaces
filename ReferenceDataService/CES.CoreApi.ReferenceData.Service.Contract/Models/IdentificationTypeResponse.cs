using System.Runtime.Serialization;
using CES.CoreApi.Common.Models;
using CES.CoreApi.ReferenceData.Service.Contract.Constants;

namespace CES.CoreApi.ReferenceData.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.ReferenceDataServiceDataContractNamespace)]
    public class IdentificationTypeResponse: ExtensibleObject
    {
        [DataMember(EmitDefaultValue = false)]
        public string Name { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public string Country { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public string Category { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public int Id { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public int LocationDepartmentId { get; set; }
        [DataMember]
        public bool IsExpirationNotRequired { get; set; }
    }
}