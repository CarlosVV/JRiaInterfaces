using System.Runtime.Serialization;
using CES.CoreApi.Common.Constants;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Foundation.Contract.Interfaces;
using CES.CoreApi.Foundation.Contract.Models;
using CES.CoreApi.GeoLocation.Service.Contract.Enumerations;

namespace CES.CoreApi.GeoLocation.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.GeolocationDataContractNamespace)]
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