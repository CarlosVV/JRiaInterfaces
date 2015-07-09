﻿using System.Runtime.Serialization;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Common.Models;
using Namespaces = CES.CoreApi.GeoLocation.Service.Contract.Constants.Namespaces;

namespace CES.CoreApi.GeoLocation.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.GeolocationServiceDataContractNamespace)]
    public class GetProviderKeyResponse : BaseResponse
    {
        public GetProviderKeyResponse(ICurrentDateTimeProvider currentDateTimeProvider)
            : base(currentDateTimeProvider)
        {
        }

        [DataMember]
        public string ProviderKey { get; set; }

        [DataMember]
        public bool IsValid { get; set; }
    }
}