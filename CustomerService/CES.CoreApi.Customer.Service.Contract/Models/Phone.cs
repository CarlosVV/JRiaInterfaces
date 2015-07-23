﻿using System.Runtime.Serialization;
using CES.CoreApi.Common.Models;
using CES.CoreApi.Customer.Service.Contract.Constants;

namespace CES.CoreApi.Customer.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.CustomerServiceDataContractNamespace)]
    public class Phone : ExtensibleObject
    {
        [DataMember(EmitDefaultValue = false)]
        public string CountryCode { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string AreaCode { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string Number { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string PhoneType { get; set; }
    }
}