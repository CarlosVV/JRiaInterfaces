using System;
using System.Runtime.Serialization;
using CES.CoreApi.Common.Models;
using CES.CoreApi.CustomerVerification.Service.Contract.Constants;

namespace CES.CoreApi.CustomerVerification.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.CustomerVerificationServiceDataContractNamespace)]
    public class VerifyCustomerIdentityRequest: BaseRequest
    {
        [DataMember(IsRequired = true)]
        public CustomerName Name { get; set; }

        [DataMember(IsRequired = true)]
        public string Country { get; set; }

        [DataMember(IsRequired = true)]
        public DateTime DateOfBirth { get; set; }

        [DataMember(IsRequired = true)]
        public string CurrentAddress { get; set; }
    }
}