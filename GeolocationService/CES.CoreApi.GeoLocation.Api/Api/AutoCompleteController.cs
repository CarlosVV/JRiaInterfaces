using AutoMapper;
using CES.CoreApi.GeoLocation.Api.Attributes;
using CES.CoreApi.GeoLocation.Enumerations;
using CES.CoreApi.GeoLocation.Models;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Interfaces;
using CES.CoreApi.GeoLocation.Service.Contract.Enumerations;
using CES.CoreApi.GeoLocation.Service.Contract.Models;

using System.Web.Http;

namespace CES.CoreApi.GeoLocation.Api.Api
{  
	[CoreApiExceptionFilter]
	[RoutePrefix("geolocation")]
	public class AutoCompleteController : ApiController
    {	
		
		private readonly IAddressServiceRequestProcessor addressServiceRequestProcessor;
		private readonly IMapper mapper;		
		public AutoCompleteController(IMapper mapper, IAddressServiceRequestProcessor addressServiceRequestProcessor)
		{		
			this.mapper = mapper;
			this.addressServiceRequestProcessor = addressServiceRequestProcessor;
		}
		
		[HttpGet]
		[HttpPost]
		[Route("address/autoComplete")]
		[Route("v1/address/autoComplete")]
		[Route("v1.0/address/autoComplete")]
		[Route("1/address/autoComplete")]
		public AutocompleteAddressResponse GetAutoCompleteList(AutocompleteAddressRequest request)
		{		
		
			var result =Utilities.RequestValidator.Validate(request);
			if (result != null)
				return result;

			var responseModel = addressServiceRequestProcessor.GetAutocompleteList(
				mapper.Map<AddressRequest, AutocompleteAddressModel>(request.Address),
				request.MaxRecords,
				mapper.Map<Confidence, LevelOfConfidence>(request.MinimumConfidence));

			result = mapper.Map<AutocompleteAddressResponseModel, AutocompleteAddressResponse>(responseModel);
		
			return result;
						
		}

		
		[HttpPost]
		[Route("address/validate")]
		public virtual ValidateAddressResponse ValidateAddress(ValidateAddressRequest request)
		{
			var result = Utilities.RequestValidator.Validate(request);
			if (result != null)
				return result;

			var responseModel = addressServiceRequestProcessor.ValidateAddress(
			mapper.Map<AddressRequest, AddressModel>(request.Address),
			mapper.Map<Confidence, LevelOfConfidence>(request.MinimumConfidence));

			return mapper.Map<ValidateAddressResponseModel, ValidateAddressResponse>(responseModel);
		}
		[Route("address/formatted/validate")]
		public virtual ValidateAddressResponse ValidateAddress(ValidateFormattedAddressRequest request)
		{
			//validator.Validate(request);

			var responseModel = addressServiceRequestProcessor.ValidateAddress(
				request.FormattedAddress,
				request.Country,
				mapper.Map<Confidence, LevelOfConfidence>(request.MinimumConfidence));

			return mapper.Map<ValidateAddressResponseModel, ValidateAddressResponse>(responseModel);
		}
	}
}
