﻿using AutoMapper;
using CES.CoreApi.Common.Models;
using CES.CoreApi.Customer.Service.Business.Contract.Enumerations;
using CES.CoreApi.Customer.Service.Business.Contract.Models;
using CES.CoreApi.Customer.Service.Contract.Enumerations;
using CES.CoreApi.Customer.Service.Contract.Models;
using SimpleInjector;

namespace CES.CoreApi.Customer.Service.Configuration
{
    internal class MapperConfigurator
    {
        public static void Configure(Container container)
        {
            Mapper.CreateMap<AddressModel, CustomerAddress>();
            Mapper.CreateMap<TelephoneModel, Telephone>();
            Mapper.CreateMap<NameModel, CustomerName>();
            Mapper.CreateMap<ContactModel, CustomerContact>();
            Mapper.CreateMap<LocationModel, Location>();
            Mapper.CreateMap<AddressValidationResult, AddressValidationStatus>();
            Mapper.CreateMap<TelephoneKind, TelephoneType>();
            Mapper.CreateMap<CustomerModel, CustomerGetResponse>().ConstructUsingServiceLocator();
            Mapper.CreateMap<PingResponseModel, PingResponse>().ConstructUsingServiceLocator();
            Mapper.CreateMap<ClearCacheResponseModel, ClearCacheResponse>().ConstructUsingServiceLocator();
            Mapper.CreateMap<DatabasePingModel, DatabasePingResponse>();

            Mapper.Configuration.ConstructServicesUsing(container.GetInstance);
        }
    }
}
