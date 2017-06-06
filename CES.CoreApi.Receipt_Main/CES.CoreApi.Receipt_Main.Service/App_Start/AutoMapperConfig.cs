using AutoMapper;
using CES.CoreApi.Receipt_Main.Service.Models;
using CES.CoreApi.Receipt_Main.Domain.Core.Contracts.ViewModels;
using CES.CoreApi.Receipt_Main.Domain.Core.Documents;
using CES.CoreApi.Receipt_Main.Domain.Core.Contracts.Models;

namespace CES.CoreApi.Receipt_Main.Service.App_Start
{
    public static class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            //Mapping for CAF XML Document
            Mapper.CreateMap<ServiceTaxCreateCAFRequestViewModel, TaxCreateCafRequest>();
            Mapper.CreateMap<TaxCreateCafFResponse, ServiceTaxCreateCAFResponseViewModel>();
         
            Mapper.CreateMap<ServiceTaxSearchCAFByTypeRequestViewModel, TaxSearchCAFByTypeRequest>();
            Mapper.CreateMap<TaxSearchCAFByTypeResponse, ServiceTaxSearchCAFByTypeResponseViewModel>();

            Mapper.CreateMap<ServiceTaxDeleteCAFRequestViewModel, TaxDeleteCAFRequest>();
            Mapper.CreateMap<TaxDeleteCAFResponse, ServiceTaxDeleteCAFResponseViewModel>();

            Mapper.CreateMap<ServiceTaxUpdateCAFRequestViewModel, TaxUpdateCafRequest>();
            Mapper.CreateMap<TaxUpdateCafResponse, ServiceTaxUpdateCAFResponseViewModel>();

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

            Mapper.CreateMap<ServiceTaxSIIGetDocumentBatchRequestViewModel, TaxSIIGetDocumentBatchRequest>();
            Mapper.CreateMap<TaxSIIGetDocumentBatchResponse, ServiceTaxSIIGetDocumentBatchResponseViewModel>();

            //Map EF Entities to Response Classes
            Mapper.CreateMap<systblApp_CoreAPI_Document, TaxDocument>();
            Mapper.CreateMap<systblApp_CoreAPI_DocumentDetail, TaxDocumentDetail>();
            Mapper.CreateMap<systblApp_CoreAPI_DocumentReference, TaxDocumentReference>();
            Mapper.CreateMap<systblApp_CoreAPI_TaxAddress, TaxAddress>();
            Mapper.CreateMap<systblApp_CoreAPI_TaxEntity, TaxEntity>();
        }
    }
}