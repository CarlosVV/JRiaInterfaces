using AutoMapper;
using CES.CoreApi.Payout.Service.Business.Contract.Models;
using CES.CoreApi.Payout.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CES.CoreApi.Payout.Api
{
	public class PayoutMapperProfile : Profile
	{
		protected override void Configure()
		{

			CreateMap<string, string>().ConvertUsing(s => (s == string.Empty ? null : s));
			CreateMap<Money, MoneyModel>().ConstructUsingServiceLocator(); ;
			CreateMap<MoneyModel, Money>().ConstructUsingServiceLocator(); ;

			CreateMap<GetTransactionInfoRequest, GetTransactionInfoRequestModel>().ConstructUsingServiceLocator();
			CreateMap<GetTransactionInfoRequestModel, GetTransactionInfoRequest>().ConstructUsingServiceLocator();
			CreateMap<RequesterInfo, RequesterInfoModel>().ConstructUsingServiceLocator();
			CreateMap<ComplianceRun, ComplianceRunModel>().ConstructUsingServiceLocator();

			CreateMap<GetTransactionInfoResponse, GetTransactionInfoResponseModel>().ConstructUsingServiceLocator();
			CreateMap<GetTransactionInfoResponseModel, GetTransactionInfoResponse>().ConstructUsingServiceLocator()
				.ForMember(dest => dest.Message, opts => opts.MapFrom(src => src.ReturnInfo.ErrorMessage));

			CreateMap<BeneficiaryInfoModel, BeneficiaryInfo>().ConstructUsingServiceLocator();
			CreateMap<BeneficiaryInfo, BeneficiaryInfoModel>().ConstructUsingServiceLocator();
			CreateMap<SenderInfoModel, SenderInfo>().ConstructUsingServiceLocator();
			CreateMap<SenderInfo, SenderInfoModel>().ConstructUsingServiceLocator();

			CreateMap<CustomerServiceMessagesModel, CustomerServiceMessages>().ConstructUsingServiceLocator();
			CreateMap<CustomerServiceMessages, CustomerServiceMessagesModel>().ConstructUsingServiceLocator();
			CreateMap<PayoutFieldsModel, PayoutFields>().ConstructUsingServiceLocator();
			CreateMap<PayoutFields, PayoutFieldsModel>().ConstructUsingServiceLocator();
			CreateMap<ReturnInfoModel, ReturnInfo>().ConstructUsingServiceLocator();
			CreateMap<ReturnInfo, ReturnInfoModel>().ConstructUsingServiceLocator();

			CreateMap<PayoutTransactionRequest, PayoutTransactionRequestModel>().ConstructUsingServiceLocator();
			CreateMap<PayoutTransactionRequestModel, PayoutTransactionRequest>().ConstructUsingServiceLocator();
			CreateMap<PayoutTransactionResponse, PayoutTransactionResponseModel>().ConstructUsingServiceLocator();
			CreateMap<PayoutTransactionResponseModel, PayoutTransactionResponse>().ConstructUsingServiceLocator()
					.ForMember(dest => dest.Message, opts => opts.MapFrom(src => src.ReturnInfo.ErrorMessage));

			CreateMap<ProviderInfoModel, ProviderInfo>().ConstructUsingServiceLocator();
			CreateMap<ProviderInfo, ProviderInfoModel>().ConstructUsingServiceLocator();

			CreateMap<RequesterInfoModel, Shared.Persistence.Model.RequesterInfoModel>().ConstructUsingServiceLocator();

		}
	}
}