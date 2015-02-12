using System;
using System.Runtime.Serialization;
using CES.CoreApi.Common.Constants;

namespace CES.CoreApi.Common.Models
{
    [DataContract(Name = "CoreApiServiceFault", Namespace = Namespaces.ServiceFaultContractAction)]
    public class CoreApiServiceFault
    {
        #region Public properties

        [DataMember(Name = "ErrorMessage")]
        public string ErrorMessage { get; set; }

        [DataMember(Name = "ErrorCode")]
        public string ErrorCode { get; set; }

        [DataMember(Name = "ErrorTime")]
        public DateTime ErrorTime { get; set; }

        [DataMember(Name = "ErrorId")]
        public Guid ErrorIdentifier { get; set; }

        #endregion 
    }
}
