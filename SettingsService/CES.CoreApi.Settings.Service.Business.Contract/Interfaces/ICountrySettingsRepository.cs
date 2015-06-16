using CES.CoreApi.Settings.Service.Business.Contract.Models;

namespace CES.CoreApi.Settings.Service.Business.Contract.Interfaces
{
    public interface ICountrySettingsRepository
    {
        CountrySettingsModel GetCountyrSettings(int countryId);
    }
}