using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Exceptions;
using CES.CoreApi.Foundation.Contract.Interfaces;
using CES.CoreApi.OrderValidation.Service.Business.Contract.Interfaces;
using CES.CoreApi.Settings.Service.Contract.Interfaces;
using CES.CoreApi.Settings.Service.Contract.Models;

namespace CES.CoreApi.OrderValidation.Service.Business.Logic.Validators
{
    public class CountryCodeValidator : ICountryCodeValidator
    {
        #region Core

        private readonly IServiceHelper _serviceHelper;

        public CountryCodeValidator(IServiceHelper serviceHelper)
        {
            if (serviceHelper == null)
                throw new CoreApiException(TechnicalSubSystem.OrderValidationService,
                   SubSystemError.GeneralRequiredParameterIsUndefined, "serviceHelper");

            _serviceHelper = serviceHelper;
        } 

        #endregion

        #region Public methods

        public bool IsCountryCodeValid(string countryCode)
        {
            var request = new GetCountryRequest { CountryAbbreviation = countryCode };
            var response = _serviceHelper.Execute<ICountrySettingsService, GetCountryResponse>(p => p.GetCountry(request));
            return response.Country != null;
        }  

        #endregion
    }
}
