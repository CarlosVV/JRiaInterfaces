using AutoMapper;
using CES.CoreApi.Receipt_Main.Models;
using CES.CoreApi.Receipt_Main.Models.DTOs;
using CES.CoreApi.Receipt_Main.ViewModels;

namespace CES.CoreApi.Receipt_Main.App_Start
{
    public static class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            //Mapping for CAF XML Document
            Mapper.CreateMap<ServiceTaxCreateCAFRequestViewModel, TaxCreateCAFRequest>();
            Mapper.CreateMap<TaxCreateCAFResponse, ServiceTaxCreateCAFResponseViewModel>();
         
            Mapper.CreateMap<ServiceTaxSearchCAFByTypeRequestViewModel, TaxSearchCAFByTypeRequest>();
            Mapper.CreateMap<TaxSearchCAFByTypeResponse, ServiceTaxSearchCAFByTypeResponseViewModel>();

            Mapper.CreateMap<ServiceTaxDeleteCAFRequestViewModel, TaxDeleteCAFRequest>();
            Mapper.CreateMap<TaxDeleteCAFResponse, ServiceTaxDeleteCAFResponseViewModel>();

            Mapper.CreateMap<ServiceTaxUpdateCAFRequestViewModel, TaxUpdateCAFRequest>();
            Mapper.CreateMap<TaxUpdateCAFResponse, ServiceTaxUpdateCAFResponseViewModel>();

            Mapper.CreateMap<ServiceTaxGetFolioRequestViewModel, TaxGetFolioRequest>();
            Mapper.CreateMap<TaxGetFolioResponse, ServiceTaxGetFolioResponseViewModel>();

            Mapper.CreateMap<ServiceTaxUpdateFolioRequestViewModel, TaxUpdateFolioRequest>();
            Mapper.CreateMap<TaxUpdateFolioResponse, ServiceTaxUpdateFolioResponseViewModel>();

            //Mapping for Receipt Document 
            Mapper.CreateMap<ServiceTaxCreateDocumentRequestViewModel, TaxCreateDocumentRequest>();
            Mapper.CreateMap<TaxCreateDocumentResponse, ServiceTaxCreateDocumentResponseViewModel>();

            Mapper.CreateMap<ServiceTaxSearchDocumentRequestViewModel, TaxSearchDocumentRequest>();
            Mapper.CreateMap<TaxSearchDocumentResponse, ServiceTaxSearchDocumentResponseViewModel>();

            Mapper.CreateMap<ServiceTaxGenerateReceiptRequestViewModel, TaxGenerateReceiptRequest>();
            Mapper.CreateMap<TaxGenerateReceiptResponse, ServiceTaxGenerateReceiptResponseViewModel>();

            //Mapping for SII Document 
            Mapper.CreateMap<ServiceTaxSIISendDocumentRequestViewModel, TaxSIISendDocumentRequest>();
            Mapper.CreateMap<TaxSIISendDocumentResponse, ServiceTaxSIISendDocumentResponseViewModel>();

            Mapper.CreateMap<ServiceTaxSIIGetDocumentRequestViewModel, TaxSIIGetDocumentRequest>();
            Mapper.CreateMap<TaxSIIGetDocumentResponse, ServiceTaxSIIGetDocumentResponseViewModel>();
        }
    }
}