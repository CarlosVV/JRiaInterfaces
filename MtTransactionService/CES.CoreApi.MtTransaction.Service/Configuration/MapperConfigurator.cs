using AutoMapper;
using CES.CoreApi.Common.Models;
using CES.CoreApi.MtTransaction.Service.Business.Contract.Enumerations;
using CES.CoreApi.MtTransaction.Service.Business.Contract.Models;
using CES.CoreApi.MtTransaction.Service.Contract.Enumerations;
using CES.CoreApi.MtTransaction.Service.Contract.Models;
using CES.CoreApi.Shared.Business.Contract.Enumerations;
using CES.CoreApi.Shared.Business.Contract.Models;
using SimpleInjector;

namespace CES.CoreApi.MtTransaction.Service.Configuration
{
    internal class MapperConfigurator
    {
        public static void Configure(Container container)
        {
            Mapper.CreateMap<AddressModel, Address>();
            Mapper.CreateMap<TelephoneModel, Phone>();
            Mapper.CreateMap<NameModel, Name>();
            Mapper.CreateMap<ContactModel, Contact>();
            Mapper.CreateMap<GeolocationModel, Geolocation>();
            Mapper.CreateMap<CustomerModel, Customer>();
            Mapper.CreateMap<BeneficiaryModel, Beneficiary>();
            Mapper.CreateMap<AgentModel, Agent>();
            Mapper.CreateMap<MoneyTransferDetailsModel, MoneyTransferDetails>();
            Mapper.CreateMap<ProcessingInformationModel, ProcessingInformation>();
            Mapper.CreateMap<ComplianceInformationModel, ComplianceInformation>();
            Mapper.CreateMap<TransactionStatusModel, TransactionStatus>();
            Mapper.CreateMap<IdentificationModel, Identification>();
            Mapper.CreateMap<BirthInformationModel, BirthInformation>();
            Mapper.CreateMap<TaxInformationModel, TaxInformation>();
            Mapper.CreateMap<MonetaryInformationModel, MonetaryInformation>();
            Mapper.CreateMap<BankDepositModel, BankDeposit>();
            Mapper.CreateMap<BankModel, Bank>();
            Mapper.CreateMap<BankAccountModel, BankAccount>();
            Mapper.CreateMap<CommissionDetailsModel, CommissionDetails>();
            Mapper.CreateMap<AmountDetailsModel, AmountDetails>();
            Mapper.CreateMap<RateDetailsModel, RateDetails>();
            Mapper.CreateMap<CurrencyDetailsModel, CurrencyDetails>();
            Mapper.CreateMap<AgentLocationModel, AgentLocation>();
            Mapper.CreateMap<TransactionDetailsModel, TransactionDetails>();

            Mapper.CreateMap<GenderEnum, string>()
                .ConvertUsing(src => src == default(GenderEnum) ? null : src.ToString());
            Mapper.CreateMap<AddressValidationResult, string>()
                .ConvertUsing(src => src.ToString());
            Mapper.CreateMap<AgentTypeEnum, string>()
                .ConvertUsing(src => src == default(AgentTypeEnum) ? null : src.ToString());
            Mapper.CreateMap<DeliveryMethodType, string>()
                .ConvertUsing(src => src == default(DeliveryMethodType) ? null : src.ToString());
            Mapper.CreateMap<PhoneType, string>()
                .ConvertUsing(src => src == default(PhoneType) ? null : src.ToString());
            Mapper.CreateMap<TransactionInformationGroup, InformationGroup>();

            Mapper.CreateMap<TransactionDetailsModel, MtTransactionGetResponse>()
               .ForMember(p => p.Transaction, map => map.MapFrom(dto => Mapper.Map<TransactionDetailsModel, TransactionDetails>(dto)))
               .ConstructUsingServiceLocator();

            Mapper.CreateMap<PingResponseModel, PingResponse>().ConstructUsingServiceLocator();
            Mapper.CreateMap<ClearCacheResponseModel, ClearCacheResponse>().ConstructUsingServiceLocator();
            Mapper.CreateMap<DatabasePingModel, DatabasePingResponse>();

            Mapper.Configuration.AllowNullCollections = true;
            Mapper.Configuration.ConstructServicesUsing(container.GetInstance);
        }
    }
}
