﻿using System.Runtime.Serialization;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Models;
using CES.CoreApi.ReferenceData.Service.Contract.Constants;

namespace CES.CoreApi.ReferenceData.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.ReferenceDataServiceDataContractNamespace)]
    public class DatabasePingResponse: ExtensibleObject
    {
        [DataMember]
        public DatabaseType Database { get; set; }
        [DataMember]
        public bool IsOk { get; set; }
    }
}