using AutoMapper;
using CES.CoreApi.GeoLocation.Enumerations;
using CES.CoreApi.GeoLocation.Models;
using CES.CoreApi.GeoLocation.Interfaces;
using CES.CoreApi.GeoLocation.Api.ViewModels;


using System.Web.Http;
using System.Net;

namespace CES.CoreApi.GeoLocation.Api
{  

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
		[Route("v2/address/autoComplete")]
		public IHttpActionResult GetAutoCompleteVersionTwo([FromUri]AddressRequest request)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);
			AutocompleteAddressRequest query = new AutocompleteAddressRequest()
			{
				Address = request,
				MaxRecords = request.MaxRecords,
				MinimumConfidence = request.MinimumConfidence
			};
			return  Content(HttpStatusCode.OK, GetAutoCompleteList(query));
		}


		[HttpGet]
		[HttpPost]
		[Route("address/autoComplete")]
		public IHttpActionResult GetAutoCompleteVersionOne(AutocompleteAddressRequest request)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			return Content(HttpStatusCode.OK, GetAutoCompleteList(request));
		}
		private AutocompleteAddressResponse GetAutoCompleteList(AutocompleteAddressRequest request)
		{

			//var result =Utilities.RequestValidator.Validate(request);
			//if (result != null)
			//	return result;

			var address = mapper.Map<AddressRequest, AutocompleteAddressModel>(request.Address);
			var confidence = mapper.Map<Confidence, LevelOfConfidence>(request.MinimumConfidence);

			var responseModel = addressServiceRequestProcessor.GetAutocompleteList(
				address,
				request.MaxRecords,
				confidence
				);

			var result = mapper.Map<AutocompleteAddressResponseModel, AutocompleteAddressResponse>(responseModel);
			return result;
						
		}



		[HttpPost]
		[Route("v2/address/validate")]
		public virtual ValidateAddressResponse DoValidateAddress(AddressRequest request)
		{
			//var result = Utilities.RequestValidator.Validate(request);
			//if (result != null)
			//	return result;
			var address = mapper.Map<AddressRequest, AddressModel>(request);

			//var level = mapper.Map<Confidence, LevelOfConfidence>(request.MinimumConfidence);

			var responseModel = addressServiceRequestProcessor.ValidateAddress(address, LevelOfConfidence.High);


			return mapper.Map<ValidateAddressResponseModel, ValidateAddressResponse>(responseModel);
		}


		[HttpPost]
		[Route("address/validate")]
		public virtual ValidateAddressResponse ValidateAddress(ValidateAddressRequest request)
		{
			var result = Utilities.RequestValidator.Validate(request);
			if (result != null)
				return result;
			var address = mapper.Map<AddressRequest, AddressModel>(request.Address);

			var level = mapper.Map<Confidence, LevelOfConfidence>(request.MinimumConfidence);

			var responseModel = addressServiceRequestProcessor.ValidateAddress(address, level);
		

			return mapper.Map<ValidateAddressResponseModel, ValidateAddressResponse>(responseModel);
		}


		[Route("address/formatted/validate")]
		public virtual ValidateAddressResponse ValidateAddress(ValidateFormattedAddressRequest request)
		{
			

			var responseModel = addressServiceRequestProcessor.ValidateAddress(
				request.FormattedAddress,
				request.Country,
				mapper.Map<Confidence, LevelOfConfidence>(request.MinimumConfidence));

			return mapper.Map<ValidateAddressResponseModel, ValidateAddressResponse>(responseModel);
		}
	}
}
