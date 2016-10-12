using AutoMapper;
using CES.CoreApi.BankAccount.Api.Models;
using CES.CoreApi.BankAccount.Api.ViewModels;

namespace CES.CoreApi.BankAccount.Api.App_Start
{
    public static class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.CreateMap<ServiceOfferCurrencyRequest, CountryCurrencyRequest>();
            Mapper.CreateMap<ServiceBankDepositRequest, ValidateBankDepositRequest>();
            //Mapper.Configuration.AllowNullDestinationValues = true;
            //Mapper.Initialize(cfg => { cfg.AllowNullDestinationValues = true; });
        }
    }
}