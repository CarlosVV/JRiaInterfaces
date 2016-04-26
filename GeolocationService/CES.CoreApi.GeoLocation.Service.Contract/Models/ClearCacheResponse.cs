using System.Runtime.Serialization;
using CES.CoreApi.Common.Models;
using Namespaces = CES.CoreApi.GeoLocation.Service.Contract.Constants.Namespaces;

namespace CES.CoreApi.GeoLocation.Service.Contract.Models
{
	[DataContract(Namespace = Namespaces.GeolocationServiceDataContractNamespace)]
    public class ClearCacheResponse : BaseResponse
    {     

       
    }
}
