using AutoMapper;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Enumerations;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Interfaces;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Models;
using CES.CoreApi.GeoLocation.Service.Contract.Enumerations;
using CES.CoreApi.GeoLocation.Service.Contract.Models;
using System.Web.Http;

namespace CES.CoreApi.GeoLocation.Api.Api
{
	public class AutoCompleteController : ApiController
    {	
		
		private readonly IAddressServiceRequestProcessor addressServiceRequestProcessor;
		private readonly IMapper mapper;
		//private readonly IRequestValidator _validator;
		public AutoCompleteController(IMapper mapper, IAddressServiceRequestProcessor addressServiceRequestProcessor)
		{		
			this.mapper = mapper;
			this.addressServiceRequestProcessor = addressServiceRequestProcessor;
		}
		
		[Route("Ping")]
		[HttpGet]
		public string Ping()
		{
			return "OK";
		}
		[HttpPost]
		[Route("AutoCompleteList")]
		public  AutocompleteAddressResponse GetAutoCompleteList(AutocompleteAddressRequest request)
		{
			//_validator.Validate(request);

			var responseModel = addressServiceRequestProcessor.GetAutocompleteList(
				mapper.Map<AddressRequest, AutocompleteAddressModel>(request.Address),
				request.MaxRecords,
				mapper.Map<Confidence, LevelOfConfidence>(request.MinimumConfidence));

			var x = mapper.Map<AutocompleteAddressResponseModel, AutocompleteAddressResponse>(responseModel);

			return x;
		}
	}
}
