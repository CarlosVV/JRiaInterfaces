using AutoMapper;
using CES.CoreApi.Receipt_Main.Service.Models;
using CES.CoreApi.Receipt_Main.Domain.Core.Contracts.ViewModels;
using CES.CoreApi.Receipt_Main.Domain.Core.Documents;
using CES.CoreApi.Receipt_Main.Domain.Core.Contracts.Models;
using LiquidTechnologies.Runtime.Net20;
using System;

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
            Mapper.CreateMap<actblTaxDocument, TaxDocument>();
            Mapper.CreateMap<TaxDocument, actblTaxDocument>();
            Mapper.CreateMap<actblTaxDocument_Detail, TaxDocumentDetail>();
            Mapper.CreateMap<TaxDocumentDetail, actblTaxDocument_Detail>();
            Mapper.CreateMap<actblTaxDocument_Reference, TaxDocumentReference>();
            Mapper.CreateMap<TaxDocumentReference, actblTaxDocument_Reference>();
            Mapper.CreateMap<actblTaxDocument_Entity_Address, TaxAddress>();
            Mapper.CreateMap<TaxAddress, actblTaxDocument_Entity_Address>();
            Mapper.CreateMap<actblTaxDocument_Entity, TaxEntity>();
            Mapper.CreateMap<TaxEntity, actblTaxDocument_Entity>();

            Mapper.CreateMap<actblTaxDocument_Entity, actblTaxDocument_Entity>();
            Mapper.CreateMap<actblTaxDocument_Entity_Address, actblTaxDocument_Entity_Address>();


            //Map CAF
            Mapper.CreateMap<AUTORIZACIONCAF, DocumentoDTE.SiiDte.CAF>();
            Mapper.CreateMap<AUTORIZACIONCAFDA, DocumentoDTE.SiiDte.DA>().
                ForMember(x => x.FA, opt => opt.MapFrom(src => new XmlDateTime(src.FA))).
                ForMember(x => x.TD, opt => opt.MapFrom(src => (DocumentoDTE.SiiDte.Enumerations.DTEType)Enum.Parse(typeof(DocumentoDTE.SiiDte.Enumerations.DTEType), $"n{src.TD}")));
            Mapper.CreateMap<AUTORIZACIONCAFFRMA, DocumentoDTE.SiiDte.FRMA>();
            Mapper.CreateMap<AUTORIZACIONCAFDARNG, DocumentoDTE.SiiDte.RNG>();
            Mapper.CreateMap<AUTORIZACIONCAFDARSAPK, DocumentoDTE.SiiDte.RSAPK>();

            Mapper.CreateMap<AUTORIZACIONCAF, DocumentoBoleta.SiiDte.CAF>();
            Mapper.CreateMap<AUTORIZACIONCAFDA, DocumentoBoleta.SiiDte.DA>().
                ForMember(x => x.FA, opt => opt.MapFrom(src => new XmlDateTime(src.FA))).
                ForMember(x => x.TD, opt => opt.MapFrom(src => (DocumentoBoleta.SiiDte.Enumerations.DTEType)Enum.Parse(typeof(DocumentoBoleta.SiiDte.Enumerations.DTEType), $"n{src.TD}")));
            Mapper.CreateMap<AUTORIZACIONCAFFRMA, DocumentoBoleta.SiiDte.FRMA>();
            Mapper.CreateMap<AUTORIZACIONCAFDARNG, DocumentoBoleta.SiiDte.RNG>();
            Mapper.CreateMap<AUTORIZACIONCAFDARSAPK, DocumentoBoleta.SiiDte.RSAPK>();            
        }
    }
}