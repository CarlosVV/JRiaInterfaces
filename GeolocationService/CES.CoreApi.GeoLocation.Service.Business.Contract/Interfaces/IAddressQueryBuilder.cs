namespace CES.CoreApi.GeoLocation.Service.Business.Contract.Interfaces
{
    public interface IAddressQueryBuilder
    {
        string Build(string address, string administrativeArea, string country);
        string Build(string address1, string address2);
    }
}