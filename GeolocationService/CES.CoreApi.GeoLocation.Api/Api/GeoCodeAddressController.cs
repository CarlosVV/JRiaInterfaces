using AutoMapper;
using CES.CoreApi.GeoLocation.Enumerations;
using CES.CoreApi.GeoLocation.Models;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Interfaces;
using CES.CoreApi.GeoLocation.Service.Contract.Enumerations;
using CES.CoreApi.GeoLocation.Service.Contract.Models;
using System.Web.Http;

namespace CES.CoreApi.GeoLocation.Api.Api
{
	[RoutePrefix("geolocation")]
	public class GeoCodeAddressController : ApiController
    {
		private readonly IMapper mapper;
		private readonly IGeocodeServiceRequestProcessor geocodeServiceRequestProcessor;
		public GeoCodeAddressController(IMapper mapper, IGeocodeServiceRequestProcessor geocodeServiceRequestProcessor)
		{
			this.mapper = mapper;
			this.geocodeServiceRequestProcessor = geocodeServiceRequestProcessor;
		}

		[HttpPost]
		[Route("Geocode/Address")]
		public  GeocodeAddressResponse GeocodeAddress(GeocodeAddressRequest request)
		{
			//_validator.Validate(request);

			var responseModel = geocodeServiceRequestProcessor.GeocodeAddress(
				mapper.Map<AddressRequest, AddressModel>(request.Address),
				mapper.Map<Confidence, LevelOfConfidence>(request.MinimumConfidence));

			return mapper.Map<GeocodeAddressResponseModel, GeocodeAddressResponse>(responseModel);
		}
		[HttpPost]
		[Route("Geocode/FormatAddress")]
		public  GeocodeAddressResponse GeocodeAddress(GeocodeFormattedAddressRequest request)
		{
			//_validator.Validate(request);

			var responseModel = geocodeServiceRequestProcessor.GeocodeAddress(
				request.FormattedAddress,
				request.Country,
				mapper.Map<Confidence, LevelOfConfidence>(request.MinimumConfidence));

			return mapper.Map<GeocodeAddressResponseModel, GeocodeAddressResponse>(responseModel);
		}
		[HttpPost]
		[Route("Geocode/ReverseGeocodePoint")]
		public  GeocodeAddressResponse ReverseGeocodePoint(ReverseGeocodePointRequest request)
		{
			//_validator.Validate(request);

			var responseModel = geocodeServiceRequestProcessor.ReverseGeocodePoint(
				mapper.Map<Location, LocationModel>(request.Location),
				request.Country,
				mapper.Map<Confidence, LevelOfConfidence>(request.MinimumConfidence));

			return mapper.Map<GeocodeAddressResponseModel, GeocodeAddressResponse>(responseModel);
		}
	}
}
