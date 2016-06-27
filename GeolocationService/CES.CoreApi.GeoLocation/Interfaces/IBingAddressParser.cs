﻿using System.Xml.Linq;
using CES.CoreApi.GeoLocation.Models;

namespace CES.CoreApi.GeoLocation.Service.Business.Contract.Interfaces
{
    public interface IBingAddressParser
    {
        AddressModel ParseAddress(XElement addressElement, XNamespace xNamespace);
    }
}