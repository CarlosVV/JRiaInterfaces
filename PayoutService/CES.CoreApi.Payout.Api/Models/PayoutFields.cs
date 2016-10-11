using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CES.CoreApi.Payout.Service.Contract.Constants;
using System.Runtime.Serialization;

namespace CES.CoreApi.Payout.Models
{
    [DataContract(Namespace = Namespaces.PayoutServiceDataContractNamespace)]
    public class PayoutFields
    {
        [DataMember(EmitDefaultValue = false)]
        public int FieldID { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public string FieldName { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public string DisplayName { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public bool FieldRequired { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public bool DataExists { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public bool DataInvalid { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public string NoteOnData { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public string DataErrorCode { get; set; }

    }
}
