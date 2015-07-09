using CES.CoreApi.Settings.Service.Contract.Models;

namespace CES.CoreApi.Settings.Service.Interfaces
{
    public interface IRequestValidator
    {
        void Validate(GetCountryRequest request);
        void Validate(GetCountryListRequest request);
        void Validate(GetCountrySettingsRequest request);
    }
}
