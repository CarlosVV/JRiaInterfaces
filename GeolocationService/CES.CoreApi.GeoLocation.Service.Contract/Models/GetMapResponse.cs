using System.Runtime.Serialization;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Common.Models;
using CES.CoreApi.GeoLocation.Service.Contract.Enumerations;
using Namespaces = CES.CoreApi.GeoLocation.Service.Contract.Constants.Namespaces;

namespace CES.CoreApi.GeoLocation.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.GeolocationServiceDataContractNamespace)]
    public class GetMapResponse : BaseResponse
    {
        public GetMapResponse(ICurrentDateTimeProvider currentDateTimeProvider) 
            : base(currentDateTimeProvider)
        {
        }

        [DataMember]
        public byte[]  MapData { get; set; }

        /// <summary>
        /// Returns true if map was received from data provider, otherwise false
        /// </summary>
        [DataMember]
        public bool IsValid { get; set; }

        /// <summary>
        /// Specify data provider used to get map
        /// </summary>
        [DataMember]
        public DataProvider DataProvider { get; set; }
    }
}