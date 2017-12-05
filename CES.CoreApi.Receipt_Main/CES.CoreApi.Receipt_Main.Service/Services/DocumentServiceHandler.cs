using CES.Caching;
using CES.CoreApi.Receipt_Main.Application.Core;
using CES.CoreApi.Receipt_Main.Application.Core.Document;
using CES.CoreApi.Receipt_Main.CAFUtilities;
using CES.CoreApi.Receipt_Main.Domain.Core.Contracts.Models;
using CES.CoreApi.Receipt_Main.Domain.Core.Documents;
using CES.CoreApi.Receipt_Main.Domain.Core.Security;
using CES.CoreApi.Receipt_Main.Domain.Core.Services;
using CES.CoreApi.Receipt_Main.Infrastructure.Core;
using CES.CoreApi.Receipt_Main.Infrastructure.Data;
using CES.CoreApi.Receipt_Main.Infrastructure.Data.Repository;
using CES.CoreApi.Receipt_Main.Repository.Repository;
using CES.CoreApi.Receipt_Main.Service.Jobs;
using CES.CoreApi.Receipt_Main.Service.Models;
using CES.CoreApi.Receipt_Main.Service.Repositories;
using Hangfire;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.IO;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Xml;
using static LiquidTechnologies.Runtime.Net20.BinaryData;

namespace CES.CoreApi.Receipt_Main.Service.Services
{
    public class DocumentServiceHandler
    {
        private const int TIPO_BOLETA = 39;
        private const int TIPO_FACTURA = 33;
        private IDocumentService _documentService;
        private readonly ITaxEntityService _taxEntityService;
        private readonly ITaxAddressService _taxAddressService;
        private static ISequenceService _sequenceService;
        private readonly IStoreService _storeService;
        private delegate int GetId();
        private object lock_obj = new object();

        public DocumentServiceHandler()
        {
            _documentService = new CES.CoreApi.Receipt_Main.Application.Core.DocumentService(new DocumentRepository(new ReceiptDbContext()));
            _taxEntityService = new CES.CoreApi.Receipt_Main.Application.Core.TaxEntityService(new TaxEntityRepository(new ReceiptDbContext()));
            _taxAddressService = new CES.CoreApi.Receipt_Main.Application.Core.TaxAddressService(new TaxAddressRepository(new ReceiptDbContext()));
            _sequenceService = new CES.CoreApi.Receipt_Main.Application.Core.SequenceService();
            _storeService = new CES.CoreApi.Receipt_Main.Application.Core.StoreService(new StoreRepository(new ReceiptDbContext()));
        }
        public DocumentServiceHandler(IDocumentService documentService, ITaxEntityService taxEntityService, ITaxAddressService taxAddressService, ISequenceService sequenceService, IStoreService storeService)
        {
            _documentService = documentService;
            _taxEntityService = taxEntityService;
            _taxAddressService = taxAddressService;
            _sequenceService = sequenceService;
            _storeService = storeService;
        }
        internal TaxCreateDocumentResponse CreateDocument(TaxCreateDocumentRequest request)
        {
            var response = new TaxCreateDocumentResponse();
            int ErrorCode = 0;

            if (request == null || (request.Document.fTransactionID == 0 && string.IsNullOrWhiteSpace(request.Document.fTransactionNo)))
            {
                var ErrorMessage = $"Either TransactionId or TransactionNo must be sent in request";
                response.ReturnInfo = new ReturnInfo() { ErrorCode = ErrorCode, ErrorMessage = ErrorMessage, ResultProcess = false };
                return response;
            }

            // Get Available Folio             
            var folioNumber = GetFolioNumber(request.Document.fDocumentTypeID, request.Document.fRecAgentID);
            if (folioNumber == 0)
            {
                var code = 77;
                var error = "Unable to generate 'fAuthorizationNumber' (XML File missing or wrong 'fRecAgentID')";
                response.ReturnInfo = new ReturnInfo() { ErrorCode = code, ErrorMessage = error, ResultProcess = false };
                return response;
            }

            var ctx = new ReceiptDbContext();
            var documentSrvc = new DocumentService(new DocumentRepository(ctx));
            var taxDocumentToSave = AutoMapper.Mapper.Map<actblTaxDocument>(request.Document);
            

            if (AppSettings.AppId == 8000 && request.Document.fDocumentTypeID == TIPO_BOLETA)
            {
                var orderInfo = documentSrvc.GetOrderInfo(request.Document.fTransactionID, request.Document.fTransactionNo);

                if (orderInfo == null || (orderInfo.MessageInfoResult != null && orderInfo.MessageInfoResult.ErrorCode > 0))
                {
                    var error = orderInfo.MessageInfoResult;
                    response.ReturnInfo = new ReturnInfo() { ErrorCode = error.ErrorCode, ErrorMessage = error.ErrorMessage, ResultProcess = false };
                    return response;
                }

                if (orderInfo.MessageInfoResult != null && orderInfo.MessageInfoResult.ErrorCode == 0)
                {
                    taxDocumentToSave.fTransactionID = orderInfo.OrderInfoResult.OrderId;
                    taxDocumentToSave.fTransactionNo = orderInfo.OrderInfoResult.OrderNo;
                    taxDocumentToSave.fAmount = orderInfo.OrderInfoResult.OrderAmount;
                    if (string.IsNullOrWhiteSpace(taxDocumentToSave.fDescription))
                    {
                        taxDocumentToSave.fDescription = $"Comision de Giro de Dinero {orderInfo.OrderInfoResult.OrderNo}";
                    }
                    taxDocumentToSave.fRecAgentID = orderInfo.OrderInfoResult.fRecAgentID;
                    taxDocumentToSave.fPayAgentID = orderInfo.OrderInfoResult.fPayAgentID;

                    if (!string.IsNullOrWhiteSpace(orderInfo.OrderInfoResult.CashierName))
                    {
                        taxDocumentToSave.fCashierName = orderInfo.OrderInfoResult.CashierName;
                    }
                    if (!string.IsNullOrWhiteSpace(orderInfo.OrderInfoResult.CashRegisterNumber))
                    {
                        var cashRegisterNumberParts = orderInfo.OrderInfoResult.CashRegisterNumber.Split(' ');
                        var cashRegisterNumber = cashRegisterNumberParts[cashRegisterNumberParts.Length - 1];
                        if (!String.IsNullOrWhiteSpace(cashRegisterNumber))
                        {
                            if (cashRegisterNumber.Length > 10)
                            {
                                cashRegisterNumber = cashRegisterNumber.Substring(cashRegisterNumber.Length - 10);
                            }

                            taxDocumentToSave.fCashRegisterNumber = cashRegisterNumber;
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(orderInfo.OrderInfoResult.StoreName))
                    {
                        taxDocumentToSave.fStoreName = orderInfo.OrderInfoResult.StoreName;
                    }
                }
            }
            else
            {
                if (string.IsNullOrWhiteSpace(taxDocumentToSave.fDescription))
                {
                    taxDocumentToSave.fDescription = $"Comision de Giro de Dinero  {request.Document.fTransactionNo}";
                }
            }

            GetId getNewDocumentIDHandler = GetNewDocumentID;
            taxDocumentToSave.fDocumentID = GetNewID("Document", "fDocumentID", getNewDocumentIDHandler);
            taxDocumentToSave.fAuthorizationNumber = $"{folioNumber}";
            taxDocumentToSave.fTime = DateTime.UtcNow;
            taxDocumentToSave.fModified = DateTime.UtcNow;

            GetId getEntityIDHandler = GetNewEntityID;
            GetId getAddressIDHandler = GetNewAddressID;
            if (!ExistsEntityInDB(taxDocumentToSave.EntityFrom.fTaxID))
            {
                taxDocumentToSave.EntityFrom.fEntityID = GetNewID("Entity", "fEntityID", getEntityIDHandler);
                taxDocumentToSave.EntityFrom.fTime = DateTime.UtcNow;
                taxDocumentToSave.EntityFrom.fModified = DateTime.UtcNow;

                foreach (var item in taxDocumentToSave.EntityFrom.TaxAddresses)
                {
                    item.fAddressID = GetNewID("Address", "fAddressID", getAddressIDHandler);
                    item.fTime = DateTime.UtcNow;
                    item.fModified = DateTime.UtcNow;
                }
            }
            else
            {
                var entity = GetEntityFromDB(taxDocumentToSave.EntityFrom.fTaxID, ctx);
                taxDocumentToSave.fEntityFromID = entity.fEntityID;
                taxDocumentToSave.EntityFrom = AutoMapper.Mapper.Map<actblTaxDocument_Entity>(entity);
                ctx.Entry(taxDocumentToSave.EntityFrom).State = EntityState.Modified;
            }

            if (!ExistsEntityInDB(taxDocumentToSave.EntityTo.fTaxID))
            {
                taxDocumentToSave.EntityTo.fEntityID = GetNewID("Entity", "fEntityID", getEntityIDHandler);
                taxDocumentToSave.EntityTo.fTime = DateTime.UtcNow;
                taxDocumentToSave.EntityTo.fModified = DateTime.UtcNow;
                foreach (var item in taxDocumentToSave.EntityTo.TaxAddresses)
                {
                    item.fAddressID = GetNewID("Address", "fAddressID", getAddressIDHandler);
                    item.fTime = DateTime.UtcNow;
                    item.fModified = DateTime.UtcNow;
                }
            }
            else
            {
                var entity = GetEntityFromDB(taxDocumentToSave.EntityTo.fTaxID, ctx);
                taxDocumentToSave.fEntityToID = entity.fEntityID;
                taxDocumentToSave.EntityTo = AutoMapper.Mapper.Map<actblTaxDocument_Entity>(entity);
                ctx.Entry(taxDocumentToSave.EntityTo).State = EntityState.Modified;
            }

            GetId getNewDocumentDetailIDHandler = GetNewDocumentDetailID;
            foreach (var item in taxDocumentToSave.DocumentDetails)
            {
                item.fDetailID = GetNewID("Detail", "fDetailID", getNewDocumentDetailIDHandler);
                item.fTime = DateTime.UtcNow;
                item.fModified = DateTime.UtcNow;

                if (string.IsNullOrWhiteSpace(item.fDescription))
                {
                    taxDocumentToSave.fDescription = $"Comision de Giro de Dinero {request.Document.fTransactionNo}";
                }
            }

            GetId getNewReferenceIDHandler = GetNewDocumentReferenceID;
            foreach (var item in taxDocumentToSave.DocumentReferences)
            {
                item.fReferenceID = GetNewID("Reference", "fReferenceID", getNewReferenceIDHandler);
                item.fTime = DateTime.UtcNow;
                item.fModified = DateTime.UtcNow;
            }

            // Generar XmlDocument -  Tipo Factura
            if (request.Document.fDocumentTypeID == TIPO_FACTURA)
            {
                DocumentoDTE.SiiDte.DTEDefType factura = CreateInvoice(request, taxDocumentToSave, folioNumber);
                taxDocumentToSave.fXmlDocumentContent = GetFacturaXML(factura);
            }

            // Generar XmlDocument -  Tipo BOLETA
            if (request.Document.fDocumentTypeID == TIPO_BOLETA)
            {
                DocumentoBoleta.SiiDte.BOLETADefType boleta = CreateBoleta(request, taxDocumentToSave, folioNumber);
                taxDocumentToSave.fXmlDocumentContent = GetBoletaXML(boleta);
            }

            //Graba en Base de Datos
            documentSrvc.CreateDocument(taxDocumentToSave);
            documentSrvc.SaveChanges();

            response.ResponseTime = DateTime.Now;
            response.ResponseTimeUTC = DateTime.UtcNow;
            response.ReturnInfo = new ReturnInfo { ErrorCode = 0, Errors = null, ResultProcess = true };
            response.TransferDate = DateTime.Now;
            response.Document = AutoMapper.Mapper.Map<TaxDocument>(taxDocumentToSave);

            return response;
        }
        public static DbContext GetDbContextFromEntity(object entity)
        {
            var object_context = GetObjectContextFromEntity(entity);

            if (object_context == null)
                return null;

            return new DbContext(object_context, dbContextOwnsObjectContext: false);
        }

        private static ObjectContext GetObjectContextFromEntity(object entity)
        {
            var field = entity.GetType().GetField("_entityWrapper");

            if (field == null)
                return null;

            var wrapper = field.GetValue(entity);
            var property = wrapper.GetType().GetProperty("Context");
            var context = (ObjectContext)property.GetValue(wrapper, null);
            return context;
        }

        private actblTaxDocument_Entity GetEntityFromDB(string taxID, DbContext ctx)
        {
            TaxEntityService _docs;
            if (ctx == null)
            {
                ctx = new ReceiptDbContext();
            }

            _docs = new TaxEntityService(new TaxEntityRepository(ctx));
            var obj = _docs.GetAllTaxEntities().Where(m => m.fTaxID == taxID).FirstOrDefault();
            ctx.Entry(obj).State = EntityState.Detached;
            return obj;
        }

        private DocumentoDTE.SiiDte.DTEDefType CreateInvoice(TaxCreateDocumentRequest request, actblTaxDocument taxDocumentToSave, int folioNumber)
        {
            DocumentoDTE.SiiDte.DTEDefType factura = new DocumentoDTE.SiiDte.DTEDefType();
            factura.DTE_Choice = new DocumentoDTE.SiiDte.DTE_Choice();
            //Documento
            factura.DTE_Choice.Documento = new DocumentoDTE.SiiDte.Documento();
            DocumentoDTE.SiiDte.Documento doc = factura.DTE_Choice.Documento;

            //Documento/Encabezado
            doc.Encabezado = new DocumentoDTE.SiiDte.Encabezado();
            doc.Encabezado.IdDoc = new DocumentoDTE.SiiDte.IdDoc();

            //Documento/Encabezado/IdDoc
            doc.Encabezado.IdDoc.TipoDTE = DocumentoDTE.SiiDte.Enumerations.DTEType.n33;
            doc.Encabezado.IdDoc.Folio = folioNumber;
            doc.Encabezado.IdDoc.FchEmis = new LiquidTechnologies.Runtime.Net20.XmlDateTime(DateTime.Today);

            //Documento/Encabezado/Emisor
            doc.Encabezado.Emisor = new DocumentoDTE.SiiDte.Emisor();
            doc.Encabezado.Emisor.RUTEmisor = taxDocumentToSave.EntityFrom.fTaxID;
            doc.Encabezado.Emisor.RznSoc = taxDocumentToSave.EntityFrom.fCompanyLegalName;

            if (taxDocumentToSave.EntityFrom.fLineOfBusiness != null)
            {
                if (taxDocumentToSave.EntityFrom.fLineOfBusiness.Length > 40)
                {
                    doc.Encabezado.Emisor.GiroEmis = taxDocumentToSave.EntityFrom.fLineOfBusiness.Substring(taxDocumentToSave.EntityFrom.fLineOfBusiness.Length - 40);
                }
                else
                {
                    doc.Encabezado.Emisor.GiroEmis = taxDocumentToSave.EntityFrom.fLineOfBusiness;
                }
            }

            if (taxDocumentToSave.EntityFrom.fEconomicActivity != null)
            {
                doc.Encabezado.Emisor.Acteco.Add(taxDocumentToSave.EntityFrom.fEconomicActivity);
            }

            var entityFromAddress = taxDocumentToSave.EntityFrom.TaxAddresses.FirstOrDefault();
            if (entityFromAddress != null)
            {
                doc.Encabezado.Emisor.DirOrigen = entityFromAddress.fAddress;
                doc.Encabezado.Emisor.CmnaOrigen = entityFromAddress.fCounty;
                doc.Encabezado.Emisor.CiudadOrigen = entityFromAddress.fCity;
            }

            //Documento/Encabezado/Receptor
            doc.Encabezado.Receptor = new DocumentoDTE.SiiDte.Receptor();
            doc.Encabezado.Receptor.RUTRecep = taxDocumentToSave.EntityTo.fTaxID;
            doc.Encabezado.Receptor.RznSocRecep = taxDocumentToSave.EntityTo.fCompanyLegalName;
            if (taxDocumentToSave.EntityTo.fLineOfBusiness != null)
            {
                if (taxDocumentToSave.EntityTo.fLineOfBusiness.Length > 40)
                {
                    doc.Encabezado.Receptor.GiroRecep = taxDocumentToSave.EntityTo.fLineOfBusiness.Substring(taxDocumentToSave.EntityTo.fLineOfBusiness.Length - 40);
                }
                else
                {
                    doc.Encabezado.Receptor.GiroRecep = taxDocumentToSave.EntityTo.fLineOfBusiness;
                }
            }

            var entityToAddress = taxDocumentToSave.EntityTo.TaxAddresses.FirstOrDefault();
            if (entityToAddress != null)
            {
                doc.Encabezado.Receptor.DirRecep = entityToAddress.fAddress;
                doc.Encabezado.Receptor.CmnaRecep = entityToAddress.fCounty;
                doc.Encabezado.Receptor.CiudadRecep = entityToAddress.fCity;
            }
            doc.Encabezado.Receptor.CorreoRecep = taxDocumentToSave.EntityTo.fEmail;

            //Documento/Encabezado/Totales
            doc.Encabezado.Totales = new DocumentoDTE.SiiDte.Totales();
            doc.Encabezado.Totales.MntTotal = (long)taxDocumentToSave.fTotalAmount;

            //Documento/Detalle
            foreach (var d in taxDocumentToSave.DocumentDetails)
            {
                DocumentoDTE.SiiDte.Detalle det = new DocumentoDTE.SiiDte.Detalle();
                det.NroLinDet = d.fLineNumber;
                det.NmbItem = d.fDescription;
                det.MontoItem = (long)d.fAmount;
                doc.Detalle.Add(det);
            }

            //Documento/Referencia
            foreach (var r in taxDocumentToSave.DocumentReferences)
            {
                DocumentoDTE.SiiDte.Referencia rf = new DocumentoDTE.SiiDte.Referencia();
                if (!string.IsNullOrWhiteSpace(r.fCodeRef))
                {
                    rf.CodRef = (DocumentoDTE.SiiDte.Enumerations.Referencia_CodRef)Enum.Parse(typeof(DocumentoDTE.SiiDte.Enumerations.Referencia_CodRef), r.fCodeRef);
                }
                if (!string.IsNullOrWhiteSpace(r.fDocRefType))
                {
                    rf.TpoDocRef = r.fDocRefType;
                }

                rf.NroLinRef = r.fLineNumber;
                rf.FolioRef = r.fDocRefFolio;
                rf.FchRef = new LiquidTechnologies.Runtime.Net20.XmlDateTime(r.fDocRefDate.Value);
                doc.Referencia.Add(rf);
            }

            factura.Version = 1;
            doc.ID = $"F{doc.Encabezado.IdDoc.Folio}T33";

            //TED
            doc.TED = new DocumentoDTE.SiiDte.TED();

            doc.TED.Version = "1.0";

            //TED/DD
            doc.TED.DD = new DocumentoDTE.SiiDte.DD();

            doc.TED.DD.RE = doc.Encabezado.Emisor.RUTEmisor;
            doc.TED.DD.TD = doc.Encabezado.IdDoc.TipoDTE;
            doc.TED.DD.F = doc.Encabezado.IdDoc.Folio;

            doc.TED.DD.FE = doc.Encabezado.IdDoc.FchEmis;
            doc.TED.DD.RR = doc.Encabezado.Receptor.RUTRecep;
            doc.TED.DD.RSR = doc.Encabezado.Receptor.RznSocRecep;
            doc.TED.DD.MNT = (ulong)doc.Encabezado.Totales.MntTotal.LongValue();

            var it1 = doc.Detalle[0].NmbItem;
            if (it1.Length > 40)
            {
                it1 = it1.Substring(it1.Length - 40);
            }
            doc.TED.DD.IT1 = it1;

            doc.TED.DD.CAF = new DocumentoDTE.SiiDte.CAF();
            var cafstr = GetCaf(request.Document.fDocumentTypeID, request.Document.fRecAgentID).fFileContent;
            cafstr = new string(cafstr.Where(c => !char.IsControl(c)).ToArray());
            var parser = new CafParser();
            var cafObj = parser.GetCAFObjectFromString(cafstr);
            doc.TED.DD.CAF = AutoMapper.Mapper.Map<DocumentoDTE.SiiDte.CAF>(cafObj.CAF);

            var tsd = DateTime.UtcNow;
            doc.TED.DD.TSTED = new LiquidTechnologies.Runtime.Net20.XmlDateTime((short)tsd.Year, (byte)tsd.Month, (byte)tsd.Day, 0, 0, 0);
            doc.TED.FRMT = new DocumentoDTE.SiiDte.FRMT();
            doc.TED.FRMT.Algoritmo = DocumentoDTE.EnvioDTE_v10Lib.Enumerations.FRMT_Algoritmo.SHA1withRSA;
            doc.TmstFirma = new LiquidTechnologies.Runtime.Net20.XmlDateTime((short)tsd.Year, (byte)tsd.Month, (byte)tsd.Day, 0, 0, 0);

            taxDocumentToSave.fTransactionID = 0;
            taxDocumentToSave.fTransactionNo = "0";
            return factura;
        }

        private DocumentoBoleta.SiiDte.BOLETADefType CreateBoleta(TaxCreateDocumentRequest request, actblTaxDocument taxDocumentToSave, int folioNumber)
        {
            DocumentoBoleta.SiiDte.BOLETADefType boleta = new DocumentoBoleta.SiiDte.BOLETADefType();
            boleta.Documento = new DocumentoBoleta.SiiDte.Documento();
            DocumentoBoleta.SiiDte.Documento doc = boleta.Documento;

            //Boleta/Documento/Encabezado
            doc.Encabezado = new DocumentoBoleta.SiiDte.Encabezado();
            doc.Encabezado.IdDoc = new DocumentoBoleta.SiiDte.IdDoc();

            //Boleta/Documento/EncabezadoIdDoc
            doc.Encabezado.IdDoc.TipoDTE = DocumentoBoleta.SiiDte.Enumerations.DTEType.n39;
            doc.Encabezado.IdDoc.Folio = folioNumber;
            doc.Encabezado.IdDoc.FchEmis = new LiquidTechnologies.Runtime.Net20.XmlDateTime(taxDocumentToSave.fIssuedDate);
            doc.Encabezado.IdDoc.IndServicio = DocumentoBoleta.SiiDte.Enumerations.IdDoc_IndServicio.n1;
            doc.Encabezado.IdDoc.IndMntNeto = DocumentoBoleta.SiiDte.Enumerations.IdDoc_IndMntNeto.n2;
            doc.Encabezado.IdDoc.PeriodoDesde = new LiquidTechnologies.Runtime.Net20.XmlDateTime(new DateTime(2012, 1, 1));
            doc.Encabezado.IdDoc.PeriodoHasta = new LiquidTechnologies.Runtime.Net20.XmlDateTime(new DateTime(2012, 1, 1));
            doc.Encabezado.IdDoc.FchVenc = new LiquidTechnologies.Runtime.Net20.XmlDateTime(new DateTime(2012, 1, 1));

            //Boleta/Documento/Encabezado/Emisor
            doc.Encabezado.Emisor = new DocumentoBoleta.SiiDte.Emisor();
            doc.Encabezado.Emisor.RUTEmisor = taxDocumentToSave.EntityFrom.fTaxID;
            doc.Encabezado.Emisor.RznSocEmisor = taxDocumentToSave.EntityFrom.fCompanyLegalName;
            doc.Encabezado.Emisor.GiroEmisor = taxDocumentToSave.EntityFrom.fLineOfBusiness;

            var siiCode = GetSiiCode(taxDocumentToSave.fRecAgentID);//Verificar el Codigo SII Sucursal
            if (siiCode == 0)
            {
                siiCode = 1;
            }
            doc.Encabezado.Emisor.CdgSIISucur = siiCode;

            var addr = taxDocumentToSave.EntityFrom.TaxAddresses.FirstOrDefault();
            if (addr != null)
            {
                doc.Encabezado.Emisor.DirOrigen = addr.fAddress;
                doc.Encabezado.Emisor.CmnaOrigen = addr.fCounty;
                doc.Encabezado.Emisor.CiudadOrigen = addr.fCity;
            }

            //Boleta/Documento/Encabezado/Receptor
            doc.Encabezado.Receptor = new DocumentoBoleta.SiiDte.Receptor();
            doc.Encabezado.Receptor.RUTRecep = taxDocumentToSave.EntityTo.fTaxID;
            doc.Encabezado.Receptor.RznSocRecep = taxDocumentToSave.EntityTo.fCompanyLegalName;
            var addr2 = taxDocumentToSave.EntityTo.TaxAddresses.FirstOrDefault();
            if (addr2 != null)
            {
                doc.Encabezado.Receptor.DirRecep = addr.fAddress;
                doc.Encabezado.Receptor.CmnaRecep = addr.fCounty;
                doc.Encabezado.Receptor.CiudadRecep = addr.fCity;
            }

            //Boleta/Documento/Encabezado/Totales
            doc.Encabezado.Totales = new DocumentoBoleta.SiiDte.Totales();
            doc.Encabezado.Totales.MntTotal = (long)taxDocumentToSave.fTotalAmount;
            doc.Encabezado.Totales.MntNeto = (long)taxDocumentToSave.fAmount;
            doc.Encabezado.Totales.MntExe = 0;
            doc.Encabezado.Totales.IVA = (long)taxDocumentToSave.fTaxAmount;
            doc.Encabezado.Totales.MontoNF = 0;
            doc.Encabezado.Totales.TotalPeriodo = 0;
            doc.Encabezado.Totales.SaldoAnterior = 0;
            doc.Encabezado.Totales.VlrPagar = (long)taxDocumentToSave.fTotalAmount;

            //Boleta/Documento/Detalle
            foreach (var d in taxDocumentToSave.DocumentDetails)
            {
                DocumentoBoleta.SiiDte.Detalle det = new DocumentoBoleta.SiiDte.Detalle();
                det.NroLinDet = d.fLineNumber;
                det.NmbItem = d.fDescription;
                det.QtyItem = 1;
                det.PrcItem = (long)d.fAmount;
                det.MontoItem = (long)d.fAmount;
                doc.Detalle.Add(det);
            }

            // Personalizados 
            boleta.Personalizados = new DocumentoBoleta.SiiDte.Personalizados();
            boleta.Personalizados.DocPersonalizado = new DocumentoBoleta.SiiDte.DocPersonalizado();

            DocumentoBoleta.SiiDte.CampoString campoString1 = new DocumentoBoleta.SiiDte.CampoString();
            campoString1.Name = "Caja";
            campoString1.PrimitiveValue = taxDocumentToSave.fCashRegisterNumber;
            boleta.Personalizados.DocPersonalizado.CampoString.Add(campoString1);

            DocumentoBoleta.SiiDte.CampoString campoString2 = new DocumentoBoleta.SiiDte.CampoString();
            campoString2.Name = "Cajero";
            campoString2.PrimitiveValue = taxDocumentToSave.fCashierName;
            boleta.Personalizados.DocPersonalizado.CampoString.Add(campoString2);

            DocumentoBoleta.SiiDte.CampoString campoString3 = new DocumentoBoleta.SiiDte.CampoString();
            campoString3.Name = "Tienda";
            campoString3.PrimitiveValue = taxDocumentToSave.fStoreName;
            boleta.Personalizados.DocPersonalizado.CampoString.Add(campoString3);

            DocumentoBoleta.SiiDte.CampoString campoString4 = new DocumentoBoleta.SiiDte.CampoString();
            campoString4.Name = "FechaHora";
            campoString4.PrimitiveValue = taxDocumentToSave.fIssuedDate.ToString("dd-MM-yyyy HH:mm:tt");
            boleta.Personalizados.DocPersonalizado.CampoString.Add(campoString4);

            DocumentoBoleta.SiiDte.CampoString campoString5 = new DocumentoBoleta.SiiDte.CampoString();
            campoString5.Name = "NumeroOrden";
            campoString5.PrimitiveValue = taxDocumentToSave.fTransactionNo;
            boleta.Personalizados.DocPersonalizado.CampoString.Add(campoString5);

            var docBol = boleta.Documento;
            docBol.ID = $"F{doc.Encabezado.IdDoc.Folio}T39";
            docBol.TED = new DocumentoBoleta.SiiDte.TED();
            docBol.TED.Version = "1.0";
            docBol.TED.DD = new DocumentoBoleta.SiiDte.DD();

            docBol.TED.DD.RE = doc.Encabezado.Receptor.RUTRecep;
            docBol.TED.DD.TD = doc.Encabezado.IdDoc.TipoDTE;
            docBol.TED.DD.F = doc.Encabezado.IdDoc.Folio;
            docBol.TED.DD.FE = doc.Encabezado.IdDoc.FchEmis;
            docBol.TED.DD.RR = doc.Encabezado.Receptor.RUTRecep;
            docBol.TED.DD.RSR = doc.Encabezado.Receptor.RznSocRecep;
            docBol.TED.DD.MNT = (ulong)doc.Encabezado.Totales.MntTotal.LongValue();
            var it1 = doc.Detalle[0].NmbItem;
            if (it1.Length > 40)
            {
                it1 = it1.Substring(it1.Length - 40);
            }
            docBol.TED.DD.IT1 = it1;

            docBol.TED.DD.CAF = new DocumentoBoleta.SiiDte.CAF();
            var cafstr = GetCaf(request.Document.fDocumentTypeID, request.Document.fRecAgentID).fFileContent;
            cafstr = new string(cafstr.Where(c => !char.IsControl(c)).ToArray());
            var parser = new CafParser();
            var cafObj = parser.GetCAFObjectFromString(cafstr);
            docBol.TED.DD.CAF = AutoMapper.Mapper.Map<DocumentoBoleta.SiiDte.CAF>(cafObj.CAF);

            var tsd = DateTime.UtcNow;
            docBol.TED.DD.TSTED = new LiquidTechnologies.Runtime.Net20.XmlDateTime((short)tsd.Year, (byte)tsd.Month, (byte)tsd.Day, 0, 0, 0);
            docBol.TED.FRMT = new DocumentoBoleta.SiiDte.FRMT();
            docBol.TED.FRMT.Algoritmo = DocumentoBoleta.EnvioBOLETA_v11Lib.Enumerations.FRMT_Algoritmo.SHA1withRSA;
            docBol.TmstFirma = new LiquidTechnologies.Runtime.Net20.XmlDateTime((short)tsd.Year, (byte)tsd.Month, (byte)tsd.Day, 0, 0, 0);
            return boleta;
        }

        public static string GetBoletaXML(DocumentoBoleta.SiiDte.BOLETADefType boleta)
        {

            var xsc = new LiquidTechnologies.Runtime.Net20.XmlSerializationContext();
            xsc.NamespaceAliases.Add("ABCXYZ", boleta.Namespace);

            var stream = new MemoryStream();
            boleta.ToXmlStream(stream, true, System.Xml.Formatting.Indented, System.Text.Encoding.GetEncoding("ISO-8859-1"), LiquidTechnologies.Runtime.Net20.EOLType.CRLF, xsc);

            var xmlBytes = stream.ToArray();

            var xml = System.Text.Encoding.UTF8.GetString(xmlBytes);
            var xmlStr = xml
                .Replace(":ABCXYZ", "")
                .Replace("ABCXYZ:", "")
                .Replace("<!--Created by Liquid XML Data Binding Libraries (www.liquid-technologies.com) for GDE S.A. (1 * Developer Bundle)-->\r\n", "")
                .Replace("<!--Created by Liquid XML Data Binding Libraries (www.liquid-technologies.com) for SOUTH CONSULTING SIGNATURE S.A. (1 * Developer Edition)-->\r\n", "")
                .Replace("xmlns=\"http://www.sii.cl/SiiDte\" ", "")
                .Replace("BOLETADefType", "DTE");

            return xmlStr;
        }
        public static string GetFacturaXML(DocumentoDTE.SiiDte.DTEDefType dte)
        {
            dte.Version = 1;

            var xsc = new LiquidTechnologies.Runtime.Net20.XmlSerializationContext();
            xsc.NamespaceAliases.Add("ABCXYZ", dte.Namespace);

            var stream = new MemoryStream();
            dte.ToXmlStream(stream, true, System.Xml.Formatting.Indented, System.Text.Encoding.GetEncoding("ISO-8859-1"), LiquidTechnologies.Runtime.Net20.EOLType.CRLF, xsc);

            var xmlBytes = stream.ToArray();

            var xml = System.Text.Encoding.UTF8.GetString(xmlBytes);
            var xmlStr = xml
                .Replace(":ABCXYZ", "")
                .Replace("ABCXYZ:", "")
                .Replace("<!--Created by Liquid XML Data Binding Libraries (www.liquid-technologies.com) for GDE S.A. (1 * Developer Bundle)-->\r\n", "")
                .Replace("<!--Created by Liquid XML Data Binding Libraries (www.liquid-technologies.com) for SOUTH CONSULTING SIGNATURE S.A. (1 * Developer Edition)-->\r\n", "")
                .Replace("xmlns=\"http://www.sii.cl/SiiDte\" ", "")
                .Replace("DTEDefType", "DTE");

            return xmlStr;
        }
        internal int GetFolioNumber(int documentType, int recAgent = 0)
        {
            var folioNumber = 0;
            var caf = GetCaf(documentType, recAgent);
            if (caf != null)
            {
                folioNumber = int.Parse(caf.fCurrentNumber) + 1;

                if (folioNumber <= int.Parse(caf.fEndNumber))
                {
                    UpdateCafFolioCurrentNumber(caf.fAuthCodeID, folioNumber.ToString());
                }
            }

            return folioNumber;
        }
        internal actblTaxDocument_AuthCode GetCaf(int documentType, int recAgent = 0)
        {
            var cafSrv = new CafService(new CafRepository(new ReceiptDbContext()));

            var caf = (from item in cafSrv.GetAllCafs()
                       where (documentType == item.fDocumentTypeID)
                       select item);

            if (caf != null)
            {
                var c2 = (from c in caf
                          where (recAgent == 0 || c.fRecAgentID == recAgent)
                          select c).FirstOrDefault();

                if (c2 != null)
                {
                    return c2;
                }

                return caf.FirstOrDefault();
            }

            return null;
        }
        internal void UpdateCafFolioCurrentNumber(int cafID, string folioNumber)
        {
            var cafSrv = new CafService(new CafRepository(new ReceiptDbContext()));
            var caf = (from item in cafSrv.GetAllCafs()
                       where item.fAuthCodeID == cafID
                       select item).FirstOrDefault();

            if (caf != null)
            {
                caf.fCurrentNumber = folioNumber;
            }

            cafSrv.UpdateCaf(caf);
            cafSrv.SaveChanges();
        }

        private int GetNewID(string tableName, string keyName, GetId getId)
        {
            var key = $"Get{tableName}_AppID_{AppSettings.AppId}_{keyName}";

            lock (lock_obj)
            {
                var folderStorageName = HttpContext.Current.Server.MapPath($"~\\bin\\keys");
                var fileStorageName = Path.Combine(folderStorageName, $"{key}.json");
                var dataStorage = "";
                var dataObj = new tableStorageKey();

                if (File.Exists(fileStorageName))
                {
                    using (var objFile = new StreamReader(fileStorageName))
                    {
                        dataStorage = objFile.ReadToEnd();
                    }

                    dataObj = JsonConvert.DeserializeObject<tableStorageKey>(dataStorage);
                    dataObj.id = dataObj.id + 1;
                }
                else
                {
                    dataObj = new tableStorageKey { table = tableName, key = keyName, id = getId() };
                }

                if (!Directory.Exists(folderStorageName))
                {
                    Directory.CreateDirectory(folderStorageName);
                }

                using (var objFile = new StreamWriter(fileStorageName, false))
                {
                    objFile.Write(JsonConvert.SerializeObject(dataObj));
                }

                return dataObj.id;
            }
        }

        private long GetSiiCode(int recAgentId)
        {
            var folderStorageName = HttpContext.Current.Server.MapPath($"~\\bin\\LocalStorage");
            var fileStorageName = Path.Combine(folderStorageName, $"Stores.json");
            var dataStorage = "";
            IEnumerable<systblApp_TaxReceipt_Store> dataObj = null;

            using (var objFile = new StreamReader(fileStorageName))
            {
                dataStorage = objFile.ReadToEnd();
            }

            dataObj = JsonConvert.DeserializeObject<IEnumerable<systblApp_TaxReceipt_Store>>(dataStorage);

            var query = (from item in dataObj where item.fStoreId == recAgentId select item).FirstOrDefault();
            return query == null ? 0 : long.Parse(query.fSiiCode);
        }

        internal int GetNewDocumentID()
        {
            var max = 0;
            var docSrvc = new DocumentService(new DocumentRepository(new ReceiptDbContext()));
            if (docSrvc.GetAllDocuments().Count > 0)
            {
                max = docSrvc.GetAllDocuments().Max(m => m.fDocumentID);
            }
            return max + 1;
        }

        internal int GetNewDocumentDetailID()
        {
            var max = 0;
            var docSrvc = new DocumentDetailService(new DocumentDetailRepository(new ReceiptDbContext()));
            if (docSrvc.GetAllDocumentDetails().Count > 0)
            {
                max = docSrvc.GetAllDocumentDetails().Max(m => m.fDetailID);
            }
            return max + 1;
        }

        internal int GetNewDocumentReferenceID()
        {
            var max = 0;
            var docSrvc = new DocumentReferenceService(new DocumentReferenceRepository(new ReceiptDbContext()));
            if (docSrvc.GetAllDocumentReferences().Count > 0)
            {
                max = docSrvc.GetAllDocumentReferences().Max(m => m.fReferenceID);
            }
            return max + 1;
        }

        internal int GetNewEntityID()
        {
            var max = 0;
            var docSrvc = new TaxEntityService(new TaxEntityRepository(new ReceiptDbContext()));
            if (docSrvc.GetAllTaxEntities().Count > 0)
            {
                max = docSrvc.GetAllTaxEntities().Max(m => m.fEntityID);
            }
            return max + 1;
        }

        internal int GetNewAddressID()
        {
            var max = 0;
            var docSrvc = new TaxAddressService(new TaxAddressRepository(new ReceiptDbContext()));
            if (docSrvc.GetAllTaxAddresses().Count > 0)
            {
                max = docSrvc.GetAllTaxAddresses().Max(m => m.fAddressID);
            }
            return max + 1;
        }

        internal TaxSearchDocumentResponse SearchDocument(TaxSearchDocumentRequest r)
        {
            var response = new TaxSearchDocumentResponse();

            var results = null as IEnumerable<DocumentSearchResultItem>;
            response.Results = results.ToList();
            response.ResponseTime = DateTime.Now;
            response.TransferDate = DateTime.Now;
            response.ResponseTimeUTC = DateTime.UtcNow;
            response.ReturnInfo = new ReturnInfo { ErrorCode = 0, ErrorMessage = "Process Done", ResultProcess = true };
            return response;
        }

        internal TaxGenerateReceiptResponse GenerateReceipt(TaxGenerateReceiptRequest request)
        {
            var response = new TaxGenerateReceiptResponse();

            // var document = null as List<Document>;// = _documentrepository.GetByOrderNumberAndFolio(request.OrderNumber, request.Folio);

            //response.Document = document.FirstOrDefault();

            //TODO: Populate all depending objects in the model
            //PopulateDocumentModel(response.Document);

            //TODO: Create PDF for Document Stored
            //CreatePDF(response.Document);

            response.ResponseTime = DateTime.Now;
            response.TransferDate = DateTime.Now;
            response.ResponseTimeUTC = DateTime.UtcNow;
            response.ReturnInfo = new ReturnInfo { ErrorCode = 1, ErrorMessage = "Process Done", ResultProcess = true };
            return response;
        }

        internal TaxSIIGetDocumentResponse SIIGetDocument(TaxSIIGetDocumentRequest request)
        {
            var _documentDownloader = new DocumentDownloader();
            var _documentHelper = new DocumentHandlerService(_documentService, _taxEntityService, _taxAddressService, _sequenceService, _storeService, 1);
            var response = new TaxSIIGetDocumentResponse();
            var docType = request.DocumentTypeID.ToString();
            var folio = request.Folio;
            var xmlContent = string.Empty;
            var _parserBoletas = new XmlDocumentParser<EnvioBOLETA>();
            var indexchunk = 1;
            var acumchunk = 0;
            var document = new actblTaxDocument();

            try
            {
                if (!ExistsFolioInDB(folio.ToString()) && _documentDownloader.RetrieveXML(int.Parse(docType), folio, out xmlContent))
                {
                    var documentXmlObject = _parserBoletas.GetDocumentObjectFromString(xmlContent);

                    List<int> detailids = null;
                    List<int> docids = null;
                    _documentHelper.SaveDocument(folio, folio, ref indexchunk, ref acumchunk, xmlContent, documentXmlObject, ref detailids, ref docids);
                }

                var dbDocument = GetDocumentByFolio(folio.ToString());
                var responseDocument = AutoMapper.Mapper.Map<TaxDocument>(dbDocument);
                response.Document = responseDocument;
                Logging.Log.Info("Returning Document");
                response.ReturnInfo = new ReturnInfo { ErrorCode = 1, ErrorMessage = "Process Done", ResultProcess = true };

                return response;
            }
            catch (Exception ex)
            {
                Logging.Log.Error(ex.ToString());
                response.ReturnInfo = new ReturnInfo { ErrorCode = 2, ErrorMessage = ex.Message, ResultProcess = false };
            }
            finally
            {
                response.ResponseTime = DateTime.Now;
                response.TransferDate = DateTime.Now;
                response.ResponseTimeUTC = DateTime.UtcNow;
            }

            return response;
        }
        internal TaxSIIGetDocumentBatchResponse CreateTaskSiiGetDocumentBatch(TaxSIIGetDocumentBatchRequest request)
        {
            var response = new TaxSIIGetDocumentBatchResponse();

            var taskService = new TaskService(new TaskRepository(new ReceiptDbContext()));

            var parameters = new DownloadBatchTaskParameter { FolioStart = request.FolioStart, FolioEnd = request.FolioEnd };
            var jsonParameters = JsonConvert.SerializeObject(parameters);

            var task = taskService.GetAllTasks().Where(m => m.fTypeID == 1 && m.fMethod == "Batch" && m.fRequestObject == jsonParameters).FirstOrDefault();

            if (task != null)
            {
                response.Status = $"Descargados {CountFoliosInDb(request.FolioStart.ToString(), request.FolioEnd.ToString()).ToString()} de {request.FolioEnd - request.FolioStart + 1}";
            }
            else
            {
                var id = taskService.GetAllTasks().Count() + 1;
                var newtask = new Domain.Core.Tasks.systblApp_CoreAPI_Task()
                {
                    fTaskID = id,
                    fTypeID = 1,
                    fStatus = 1,
                    fCountExecution = 0,
                    fStartDateTime = DateTime.Now,
                    fRequestObject = jsonParameters,
                    fMethod = "Batch"
                };

                taskService.CreateTask(newtask);

                taskService.SaveChanges();

                response.Status = $"Descarga iniciada de {request.FolioStart} a {request.FolioEnd}";

                var job = new BatchDownloadJob();
                BackgroundJob.Enqueue(() => job.Execute(id));
            }

            response.ResponseTime = DateTime.Now;
            response.TransferDate = DateTime.Now;
            response.ResponseTimeUTC = DateTime.UtcNow;
            response.ReturnInfo = new ReturnInfo { ErrorCode = 1, ErrorMessage = "Process Done", ResultProcess = true };

            return response;
        }
        private actblTaxDocument GetDocumentByFolio(string folio)
        {
            var _documentServiceToSearch = new DocumentService(new DocumentRepository(new ReceiptDbContext()));
            return _documentServiceToSearch.GetAllDocuments().Where(m => m.fAuthorizationNumber == folio).FirstOrDefault();
        }
        private bool ExistsFolioInDB(string folio)
        {
            var _documentServiceToSearch = new DocumentService(new DocumentRepository(new ReceiptDbContext()));
            return _documentServiceToSearch.GetAllDocuments().Any(m => m.fAuthorizationNumber == folio);
        }
        private bool ExistsEntityInDB(string taxID)
        {
            var _docs = new TaxEntityService(new TaxEntityRepository(new ReceiptDbContext()));
            return _docs.GetAllTaxEntities().Any(m => m.fTaxID == taxID);
        }
        private int CountFoliosInDb(string start, string end)
        {
            var folioStart = int.Parse(start);
            var folioEnd = int.Parse(end);
            var _documentServiceToSearch = new DocumentService(new DocumentRepository(new ReceiptDbContext()));
            return _documentServiceToSearch.GetAllDocuments().Count(m => int.Parse(m.fAuthorizationNumber) >= folioStart && int.Parse(m.fAuthorizationNumber) <= folioEnd);
        }
    }
}