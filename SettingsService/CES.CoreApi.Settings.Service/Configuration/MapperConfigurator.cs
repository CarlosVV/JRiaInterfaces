using System.Collections.Generic;
using AutoMapper;
using CES.CoreApi.Common.Models;
using CES.CoreApi.Settings.Service.Business.Contract.Enumerations;
using CES.CoreApi.Settings.Service.Business.Contract.Models;
using CES.CoreApi.Settings.Service.Contract.Enumerations;
using CES.CoreApi.Settings.Service.Contract.Models;
using SimpleInjector;

namespace CES.CoreApi.Settings.Service.Configuration
{
    internal class MapperConfigurator
    {
        public static void Configure(Container container)
        {
            Mapper.CreateMap<CountryModel, GetCountryResponse>()
                .ForMember(p => p.Country, map => map.MapFrom(dto => Mapper.Map<CountryModel, CountryResponse>(dto)))
                .ConstructUsingServiceLocator();

            Mapper.CreateMap<IEnumerable<CountryModel>, GetCountryListResponse>()
                .ForMember(p => p.CountryList, map => map.MapFrom(dto => Mapper.Map<IEnumerable<CountryModel>, IEnumerable<CountryResponse>>(dto)))
                .ConstructUsingServiceLocator();

            Mapper.CreateMap<CountrySettingsModel, GetCountrySettingsResponse>()
                .ConstructUsingServiceLocator();

            Mapper.CreateMap<CountryModel, CountryResponse>();
            
            Mapper.CreateMap<AccountReceivableSettingsModel, AccountReceivableSettings>();
            Mapper.CreateMap<BeneficiarySettingsModel, BeneficiarySettings>();
            Mapper.CreateMap<CorporateSettingsModel, CorporateSettings>();
            Mapper.CreateMap<MoneyTransferSettingsModel, MoneyTransferSettings>();
            Mapper.CreateMap<AddressValidationSettingsModel, AddressValidationSettings>();
            Mapper.CreateMap<PossibleDuplicationSettingsModel, PossibleDuplicationSettings>();
            Mapper.CreateMap<DigitalReceiptSettingsModel, DigitalReceiptSettings>();
            Mapper.CreateMap<ScannerSettingsModel, ScannerSettings>();
            Mapper.CreateMap<CustomerConfidentialSettingsModel, CustomerConfidentialSettings>();

            Mapper.CreateMap<PingResponseModel, PingResponse>().ConstructUsingServiceLocator();
            Mapper.CreateMap<ClearCacheResponseModel, ClearCacheResponse>().ConstructUsingServiceLocator();
            Mapper.CreateMap<DatabasePingModel, DatabasePingResponse>();
            
            Mapper.CreateMap<AccountReceivableLimitDisplayOptionType, AccountReceivableLimitDisplayOption>();
            Mapper.CreateMap<BeneficiaryConsolidationSettingType, BeneficiaryConsolidationSetting>();
            Mapper.CreateMap<RecurrentBeneficiarySettingType, RecurrentBeneficiarySetting>();
            Mapper.CreateMap<PossibleDuplicateSettingType, PossibleDuplicateSetting>();
            Mapper.CreateMap<PossibleDuplicateCustomerCreationSettingType, PossibleDuplicateCustomerCreationSetting>();
            Mapper.CreateMap<PossibleDuplicateCustomerActionSettingType, PossibleDuplicateCustomerActionSetting>();
            Mapper.CreateMap<WatchListPayoutSettingType, WatchListPayoutSetting>();
            Mapper.CreateMap<ComplianceWarningPeriodicityType, ComplianceWarningPeriodicity>();
      

            Mapper.Configuration.ConstructServicesUsing(container.GetInstance);
        }
    }
}
