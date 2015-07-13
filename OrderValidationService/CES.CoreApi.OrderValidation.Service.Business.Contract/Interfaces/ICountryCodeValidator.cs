namespace CES.CoreApi.OrderValidation.Service.Business.Contract.Interfaces
{
    public interface ICountryCodeValidator
    {
        bool IsCountryCodeValid(string countryCode);
    }
}