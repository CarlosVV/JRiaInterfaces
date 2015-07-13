using System.Collections.Generic;
using CES.CoreApi.Settings.Service.Business.Contract.Models;

namespace CES.CoreApi.Settings.Service.Business.Contract.Interfaces
{
    public interface ICountrySettingsProcessor
    {
        CountryModel GetCountry(string countryAbbreviation, int languageId);
        IEnumerable<CountryModel> GetCountries(int languageId, string culture, IEnumerable<string> countryAbbreviationList);
        CountrySettingsModel GetCountrySettings(string countryAbbreviation);
    }
}