﻿using CES.CoreApi.GeoLocation.Service.Business.Contract.Enumerations;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Models;

namespace CES.CoreApi.GeoLocation.Service.Business.Contract.Interfaces
{
    public interface IAddressAutocompleteDataProvider
    {
        AutocompleteAddressResponseModel GetAddressHintList(string country, string administrativeArea, string address, int maxRecords, DataProviderType providerType);
    }
}