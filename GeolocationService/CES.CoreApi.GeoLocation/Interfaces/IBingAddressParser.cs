using System.Xml.Linq;
using CES.CoreApi.GeoLocation.Models;

namespace CES.CoreApi.GeoLocation.Interfaces
{
    public interface IBingAddressParser
    {
        AddressModel ParseAddress(XElement addressElement, XNamespace xNamespace);
    }
}