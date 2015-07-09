using System;
using System.Collections.Generic;
using System.Linq;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Exceptions;
using CES.CoreApi.Common.Tools;
using CES.CoreApi.Settings.Service.Business.Contract.Interfaces;
using CES.CoreApi.Settings.Service.Business.Contract.Models;

namespace CES.CoreApi.Settings.Service.Business.Logic.Processors
{
    public class CountrySettingsProcessor : ICountrySettingsProcessor
    {
        #region Core

        private readonly ICountryRepository _countryRepository;
        private readonly ICountrySettingsRepository _settingsRepository;

        public CountrySettingsProcessor(ICountryRepository countryRepository, ICountrySettingsRepository settingsRepository)
        {
            if (countryRepository == null)
                throw new CoreApiException(TechnicalSubSystem.SettingsService,
                   SubSystemError.GeneralRequiredParameterIsUndefined, "countryRepository");
            if (settingsRepository == null)
                throw new CoreApiException(TechnicalSubSystem.SettingsService,
                   SubSystemError.GeneralRequiredParameterIsUndefined, "settingsRepository");

            _countryRepository = countryRepository;
            _settingsRepository = settingsRepository;
        }

        #endregion

        #region Public methods

        public CountryModel GetCountry(string countryAbbreviation, int languageId)
        {
            var countries = GetCountries(languageId);
            return countries.FirstOrDefault(
                p => p.Abbreviation.Equals(countryAbbreviation, StringComparison.OrdinalIgnoreCase));
        }

        public IEnumerable<CountryModel> GetCountries(int languageId = 0, string culture = null, IEnumerable<string> countryAbbreviationList = null)
        {
            var countryAbbreviations = countryAbbreviationList != null
                ? countryAbbreviationList.ToDelimitedString()
                : string.Empty;

            return !string.IsNullOrEmpty(culture) 
                ? _countryRepository.GetAll(culture, countryAbbreviations) 
                : _countryRepository.GetAll(languageId, countryAbbreviations);
        }

        public CountrySettingsModel GetCountrySettings(string countryAbbreviation)
        {
            var countryModel = GetCountry(countryAbbreviation, 0);
            return countryModel == null
                ? null
                : _settingsRepository.GetCountyrSettings(countryModel.Id);
        }

        #endregion
    }
}