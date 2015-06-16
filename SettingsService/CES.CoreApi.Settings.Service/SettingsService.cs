using System.Collections.Generic;
using System.ServiceModel;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Exceptions;
using CES.CoreApi.Common.Models;
using CES.CoreApi.Foundation.Contract.Interfaces;
using CES.CoreApi.Settings.Service.Business.Contract.Interfaces;
using CES.CoreApi.Settings.Service.Business.Contract.Models;
using CES.CoreApi.Settings.Service.Contract.Constants;
using CES.CoreApi.Settings.Service.Contract.Interfaces;
using CES.CoreApi.Settings.Service.Contract.Models;
using CES.CoreApi.Settings.Service.Interfaces;

namespace CES.CoreApi.Settings.Service
{
    [ServiceBehavior(Namespace = Namespaces.SettingsServiceContractNamespace, InstanceContextMode = InstanceContextMode.PerCall)]
    public class SettingsService : ICountrySettingsService, IHealthMonitoringService
    {
        #region Core

        private readonly ICountrySettingsProcessor _countrySettingsProcessor;
        private readonly IHealthMonitoringProcessor _healthMonitoringProcessor;
        private readonly IRequestValidator _requestValidator;
        private readonly IMappingHelper _mapper;

        public SettingsService(ICountrySettingsProcessor countrySettingsProcessor, IHealthMonitoringProcessor healthMonitoringProcessor,
            IRequestValidator requestValidator, IMappingHelper mapper)
        {
            if (countrySettingsProcessor == null)
                throw new CoreApiException(TechnicalSubSystem.SettingsService,
                   SubSystemError.GeneralRequiredParameterIsUndefined, "countrySettingsProcessor");
            if (healthMonitoringProcessor == null)
                throw new CoreApiException(TechnicalSubSystem.SettingsService,
                   SubSystemError.GeneralRequiredParameterIsUndefined, "healthMonitoringProcessor");
            if (requestValidator == null)
                throw new CoreApiException(TechnicalSubSystem.SettingsService,
                   SubSystemError.GeneralRequiredParameterIsUndefined, "requestValidator");
            if (mapper == null)
                throw new CoreApiException(TechnicalSubSystem.SettingsService,
                   SubSystemError.GeneralRequiredParameterIsUndefined, "mapper");

            _countrySettingsProcessor = countrySettingsProcessor;
            _healthMonitoringProcessor = healthMonitoringProcessor;
            _requestValidator = requestValidator;
            _mapper = mapper;
        }

        #endregion

        #region ICountrySettingsService implementation

        public GetCountryResponse GetCountry(GetCountryRequest request)
        {
            _requestValidator.Validate(request);
            var responseModel = _countrySettingsProcessor.GetCountry(request.CountryAbbreviation, request.LanguageId);
            var resp = _mapper.ConvertToResponse<CountryModel, GetCountryResponse>(responseModel);
            return resp;
        }

        public GetCountryListResponse GetCountryList(GetCountryListRequest request)
        {
            _requestValidator.Validate(request);
            var responseModel = _countrySettingsProcessor.GetCountries(request.LanguageId, request.Culture, request.CountryAbbreviationList);
            return _mapper.ConvertToResponse<IEnumerable<CountryModel>, GetCountryListResponse>(responseModel);
        }

        public GetCountrySettingsResponse GetCountrySettings(GetCountrySettingsRequest request)
        {
            _requestValidator.Validate(request);
            var responseModel = _countrySettingsProcessor.GetCountrySettings(request.CountryAbbreviation);
            return _mapper.ConvertToResponse<CountrySettingsModel, GetCountrySettingsResponse>(responseModel);
        }

        #endregion

        #region IHealthMonitoringService

        public ClearCacheResponse ClearCache()
        {
            var responseModel = _healthMonitoringProcessor.ClearCache();
            return _mapper.ConvertToResponse<ClearCacheResponseModel, ClearCacheResponse>(responseModel);
        }

        public HealthResponse Ping()
        {
            var responseModel = _healthMonitoringProcessor.Ping();
            return _mapper.ConvertToResponse<HealthResponseModel, HealthResponse>(responseModel);
        }

        #endregion
    }
}