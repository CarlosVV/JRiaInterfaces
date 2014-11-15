using System.Xml.Linq;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Models;

namespace CES.CoreApi.GeoLocation.Service.Business.Contract.Interfaces
{
    public interface IMelissaAddressParser
    {
        AddressModel ParseAddress(XElement addressElement, XNamespace xNamespace = null, string country = null, bool isAutocompleteService = false);
    }
}