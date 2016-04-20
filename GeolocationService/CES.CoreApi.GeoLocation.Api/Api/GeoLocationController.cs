using AutoMapper;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Exceptions;
using CES.CoreApi.GeoLocation.Api.Models;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Enumerations;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Interfaces;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Models;
using CES.CoreApi.GeoLocation.Service.Contract.Enumerations;
using CES.CoreApi.GeoLocation.Service.Contract.Interfaces;
using CES.CoreApi.GeoLocation.Service.Contract.Models;
using System.Web.Http;

namespace CES.CoreApi.GeoLocation.Api.Api
{
	public class GeoLocationController : ApiController
    {
		
		private readonly IUserRepository repository;
		private readonly IAddressServiceRequestProcessor _addressServiceRequestProcessor;
		private readonly IGeocodeServiceRequestProcessor _geocodeServiceRequestProcessor;
		private readonly IMapServiceRequestProcessor _mapServiceRequestProcessor;
		private readonly IHealthMonitoringProcessor _healthMonitoringProcessor;

		private readonly IMapper _mapper;
		private readonly IRequestValidator _validator;
		public GeoLocationController(IUserRepository repository, IMapper mapper)
		{
			this.repository = repository;
			_mapper = mapper;
		}
		//public GeoLocationController(IAddressServiceRequestProcessor addressServiceRequestProcessor,


		// IMapper mapper)
		//{


		//	_addressServiceRequestProcessor = addressServiceRequestProcessor;


		//	_mapper = mapper;
		//	//_validator = validator;
		//}
		[Route("Ping")]
		[HttpGet]
		public string Ping()
		{
			return "OK";
		}
		[Route("Autocomplete")]
		public  AutocompleteAddressResponse GetAutocompleteList(AutocompleteAddressRequest request)
		{
			_validator.Validate(request);

			var responseModel = _addressServiceRequestProcessor.GetAutocompleteList(
				_mapper.Map<AddressRequest, AutocompleteAddressModel>(request.Address),
				request.MaxRecords,
				_mapper.Map<Confidence, LevelOfConfidence>(request.MinimumConfidence));

			return _mapper.Map<AutocompleteAddressResponseModel, AutocompleteAddressResponse>(responseModel);
		}
	}
}
