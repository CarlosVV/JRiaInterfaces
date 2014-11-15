using CES.CoreApi.GeoLocation.Service.Contract.Models;

namespace CES.CoreApi.GeoLocation.Service.Contract.Interfaces
{
    public interface IRequestValidator
    {
        void Validate(ValidateAddressRequest request);
        void Validate(ValidateFormattedAddressRequest request);
        void Validate(AutocompleteAddressRequest request);
        void Validate(GeocodeAddressRequest request);
        void Validate(GeocodeFormattedAddressRequest request);
        void Validate(ReverseGeocodePointRequest request);
        void Validate(GetMapRequest request);
    }
}