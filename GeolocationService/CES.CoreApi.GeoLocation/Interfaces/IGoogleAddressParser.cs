using System.Xml.Linq;
using CES.CoreApi.GeoLocation.Models;

namespace CES.CoreApi.GeoLocation.Interfaces
{
    public interface IGoogleAddressParser
    {
        AddressModel ParseAddress(XElement address);
    }
}