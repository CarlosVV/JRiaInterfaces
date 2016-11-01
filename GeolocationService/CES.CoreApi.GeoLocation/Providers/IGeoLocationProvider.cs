using CES.CoreApi.GeoLocation.Models.Requests;
using CES.CoreApi.GeoLocation.Models.Responses;

namespace CES.CoreApi.GeoLocation.Providers
{
	public interface IGeoLocationProvider
	{
		AddressValidationResponse Validation(AddressRequest addressRequest);
	}
}
