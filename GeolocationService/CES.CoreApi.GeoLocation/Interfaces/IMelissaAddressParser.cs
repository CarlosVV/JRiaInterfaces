using System.Xml.Linq;
using CES.CoreApi.GeoLocation.Models;

namespace CES.CoreApi.GeoLocation.Interfaces
{
    public interface IMelissaAddressParser
    {
        AddressModel ParseAddress(XElement addressElement, XNamespace xNamespace = null, string country = null, bool isAutocompleteService = false);
    }
}