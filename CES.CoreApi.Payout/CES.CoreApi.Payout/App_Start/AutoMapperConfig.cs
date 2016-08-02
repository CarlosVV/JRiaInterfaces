using AutoMapper;
using CES.CoreApi.Payout.Models;
using CES.CoreApi.Payout.Providers;
using CES.CoreApi.Payout.ViewModels;
namespace CES.CoreApi.Payout.App_Start
{
	public static class AutoMapperConfig
	{
		public static void RegisterMappings()
		{
			Mapper.CreateMap<TransactionInfoRequest, OrderRequest>();
			Mapper.CreateMap<TransactionInfo, Beneficiary>();
			Mapper.CreateMap<TransactionInfo, Sender>();
			Mapper.CreateMap<TransactionInfo, TransactionInfoResponse>();
			Mapper.CreateMap<PayoutOrderInfo, TransactionInfoResponse>()
					.ForMember(dest => dest.OrderId, opts => opts.MapFrom(src => src.Transaction.OrderId))
					.ForMember(dest => dest.OrderDate, opts => opts.MapFrom(src => src.Transaction.OrderDate))
					.ForMember(dest => dest.OrderStatus, opts => opts.MapFrom(src => src.Transaction.OrderStatus))
					.ForMember(dest => dest.PayoutAmount, opts => opts.MapFrom(src => src.Transaction.PayoutAmount))
					.ForMember(dest => dest.PayoutCurrency, opts => opts.MapFrom(src => src.Transaction.PayoutCurrency))
					.ForMember(dest => dest.CustCountry, opts => opts.MapFrom(src => src.Transaction.CustCountry))
					.ForMember(dest => dest.BenCountry, opts => opts.MapFrom(src => src.Transaction.BenCountry))
					.ForMember(dest => dest.PIN, opts => opts.MapFrom(src => src.Transaction.PIN))
					.ForMember(dest => dest.PASeqID, opts => opts.MapFrom(src => src.Transaction.PASeqID))
					.ForMember(dest => dest.PayAgent, opts => opts.MapFrom(src => src.Transaction.PayAgent))
					.ForMember(dest => dest.PayAgentBranchName, opts => opts.MapFrom(src => src.Transaction.PayAgentBranchName))
					.ForMember(dest => dest.PayAgentBranchNo, opts => opts.MapFrom(src => src.Transaction.PayAgentBranchNo))
					.ForMember(dest => dest.DeliveryMethod, opts => opts.MapFrom(src => src.Transaction.DeliveryMethod))
					.ForMember(dest => dest.BeneficiaryTax, opts => opts.MapFrom(src => src.Transaction.BeneficiaryTax))
					.ForMember(dest => dest.NetAmount, opts => opts.MapFrom(src => src.Transaction.NetAmount));


			Mapper.CreateMap<Tx, TransactionInfo>()
				.ForMember(dest => dest.PIN, opts => opts.MapFrom(src => src.OID))
				.ForMember(dest => dest.OrderDate, opts => opts.MapFrom(src => src.TransferDate))
				.ForMember(dest => dest.OrderDate, opts => opts.MapFrom(src => src.TransferDate))
				
				
				;

		}
	}
}

