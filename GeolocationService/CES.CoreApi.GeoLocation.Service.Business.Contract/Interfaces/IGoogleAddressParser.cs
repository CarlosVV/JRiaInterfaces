using System.Xml.Linq;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Models;

namespace CES.CoreApi.GeoLocation.Service.Business.Contract.Interfaces
{
    public interface IGoogleAddressParser
    {
        AddressModel ParseAddress(XElement address);
    }
}