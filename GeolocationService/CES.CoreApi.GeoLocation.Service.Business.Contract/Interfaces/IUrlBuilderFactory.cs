﻿using CES.CoreApi.GeoLocation.Service.Business.Contract.Enumerations;

namespace CES.CoreApi.GeoLocation.Service.Business.Contract.Interfaces
{
    public interface IUrlBuilderFactory
    {
        T GetInstance<T>(DataProviderType providerType, FactoryEntity urlBuilder) where T : class;
    }
}