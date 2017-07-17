using CES.CoreApi.Receipt_Main.Domain.Core.Documents;
using CES.CoreApi.Receipt_Main.Domain.Core.Security;
using CES.CoreApi.Receipt_Main.Domain.Core.Services;
using CES.CoreApi.Receipt_Main.Infrastructure.Data;
using CES.CoreApi.Receipt_Main.Infrastructure.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Application.Core.Document
{
    public class DocumentHandlerService
    {
        private int chunkfolio = 100; //TODO: Should be in Parameters
        private static readonly decimal _TaxRate = 0.19m;//TODO: Should be in Parameters
        private IDocumentService _documentService;
        private ITaxEntityService _taxEntityService;
        private ITaxAddressService _taxAddressService;
        private static ISequenceService _sequenceService;
        private IStoreService _storeService;
        private static Dictionary<string, systblApp_CoreAPI_TaxEntity> _TaxEntityCache;
        private static Dictionary<string, systblApp_TaxReceipt_Store> _StoreCache;

        public int Chunkfolio
        {
            get
            {
                return chunkfolio;
            }

            set
            {
                chunkfolio = value;
            }
        }
        public DocumentHandlerService(IDocumentService documentService, ITaxEntityService taxEntityService, ITaxAddressService taxAddressService, ISequenceService sequenceService, IStoreService storeService, int chunkfolio)
        {
            _documentService = documentService;
            _taxEntityService = taxEntityService;
            _taxAddressService = taxAddressService;
            _sequenceService = sequenceService;
            _storeService = storeService;

            _TaxEntityCache = new Dictionary<string, systblApp_CoreAPI_TaxEntity>();
            _StoreCache = new Dictionary<string, systblApp_TaxReceipt_Store>();

            this.chunkfolio = chunkfolio;
        }
        public DocumentHandlerService(IDocumentService documentService, ITaxEntityService taxEntityService, ITaxAddressService taxAddressService, ISequenceService sequenceService, IStoreService storeService)
        {
            _documentService = documentService;
            _taxEntityService = taxEntityService;
            _taxAddressService = taxAddressService;
            _sequenceService = sequenceService;
            _storeService = storeService;

            _TaxEntityCache = new Dictionary<string, systblApp_CoreAPI_TaxEntity>();
            _StoreCache = new Dictionary<string, systblApp_TaxReceipt_Store>();
        }
        public void SaveDocument(int folio, int folioFinal, ref int indexchunk, ref int acumchunk, EnvioBOLETA objBoleta, ref List<int> detailids, ref List<int> docids)
        {
            var rutSender = objBoleta.SetDTE.DTE.Documento.Encabezado.Emisor.RUTEmisor;
            var rutReceiver = objBoleta.SetDTE.DTE.Documento.Encabezado.Receptor.RUTRecep;
            int? idSender = null;
            int? idReceiver = null;
            var senderEntity = new systblApp_CoreAPI_TaxEntity();
            var receiverEntity = new systblApp_CoreAPI_TaxEntity();

            //Get o Set Entity and Address
            CreateTaxEntities(objBoleta, rutSender, rutReceiver, out idSender, out idReceiver, senderEntity, receiverEntity);

            //Save Document and Detail
            CreateDocument(indexchunk, objBoleta, ref detailids, ref docids, senderEntity, receiverEntity);

            if (indexchunk >= Chunkfolio || folio == folioFinal)
            {
                _documentService.SaveChanges();
                acumchunk = acumchunk + indexchunk;
                indexchunk = 0;
                docids = null;
                detailids = null;
            }
        }
        private void CreateDocument(int indexchunk, EnvioBOLETA objBoleta, ref List<int> detailids, ref List<int> docids, systblApp_CoreAPI_TaxEntity senderEntity, systblApp_CoreAPI_TaxEntity receiverEntity)
        {
            if (detailids == null)
            {
                detailids = ReserveNewIds("systblApp_CoreAPI_DocumentDetail", Chunkfolio);
            }

            if (docids == null)
            {
                docids = ReserveNewIds("systblApp_CoreAPI_Document", Chunkfolio);
            }

            var dbBoleta = MapDocumentFromXmlObject(docids[indexchunk - 1], objBoleta, senderEntity, receiverEntity);

            AddDocumentDetailFromXmlObject(detailids[indexchunk - 1], objBoleta, dbBoleta);

            _documentService.CreateDocument(dbBoleta);
        }
        private void AddDocumentDetailFromXmlObject(int id, EnvioBOLETA objBoleta, systblApp_CoreAPI_Document dbBoleta)
        {
            dbBoleta.DocumentDetails.Add(new systblApp_CoreAPI_DocumentDetail
            {
                fDocumentDetailId = id,
                fDocumentId = dbBoleta.fDocumentId,
                Document = dbBoleta,
                fLineNumber = objBoleta.SetDTE.DTE.Documento.Detalle.NroLinDet,
                fDescription = objBoleta.SetDTE.DTE.Documento.Detalle.NmbItem,
                fItemCount = objBoleta.SetDTE.DTE.Documento.Detalle.QtyItem,
                fPrice = objBoleta.SetDTE.DTE.Documento.Detalle.PrcItem,
                fAmount = objBoleta.SetDTE.DTE.Documento.Detalle.MontoItem,
                fCode = string.Empty,
                fDocRefFolio = objBoleta.SetDTE.DTE.Documento.TED.DD.F.ToString(),
                fDocRefType = objBoleta.SetDTE.DTE.Documento.TED.DD.TD.ToString(),
                fChanged = null,
                fDelete = false,
                fDisabled = false,
                fModified = null,
                fModifiedID = 69,
                fTime = DateTime.Now,
            });

        }
        private systblApp_CoreAPI_Document MapDocumentFromXmlObject(int id, EnvioBOLETA objBoleta, systblApp_CoreAPI_TaxEntity senderEntity, systblApp_CoreAPI_TaxEntity receiverEntity)
        {
            var totalamount = objBoleta.SetDTE.DTE.Documento.Encabezado.Totales.MntTotal;
            var amount = Math.Floor(objBoleta.SetDTE.DTE.Documento.Encabezado.Totales.MntTotal / (1 + _TaxRate));
            var tax = totalamount - amount;
            var storename = GetCustomFieldFromDocument(objBoleta, "Tienda");

            return new systblApp_CoreAPI_Document
            {
                fDocumentId = id,
                fDocumentType = objBoleta.SetDTE.DTE.Documento.TED.DD.TD.ToString(),
                fFolio = (int)objBoleta.SetDTE.DTE.Documento.TED.DD.F,
                fIssuedDate = objBoleta.SetDTE.DTE.Documento.TED.DD.FE,
                fPaymentDate = objBoleta.SetDTE.DTE.Documento.TED.DD.FE,
                fStoreName = storename,
                fOrderNo = GetOrderNo(objBoleta),
                fCashierName = GetCustomFieldFromDocument(objBoleta, "Cajero"),
                fCashRegisterNumber = GetCustomFieldFromDocument(objBoleta, "Caja"),
                fAmount = amount,
                fTaxAmount = tax,
                fTotalAmount = totalamount,
                fDescription = objBoleta.SetDTE.DTE.Documento.Detalle.NmbItem,
                DocumentDetails = new List<systblApp_CoreAPI_DocumentDetail>(),
                fTimestampDocument = objBoleta.SetDTE.Caratula.TmstFirmaEnv,
                fTimestampSent = objBoleta.SetDTE.DTE.Documento.TmstFirma,
                fRecAgent = GetStoreId(storename),
                fDownloadedSII = true,
                fExemptAmount = 0,
                fSentToSII = true,
                fPayAgent = 0,
                fReceiverId = receiverEntity.fTaxEntityId,
                fSenderId = senderEntity.fTaxEntityId,
                fChanged = null,
                fDelete = false,
                fDisabled = false,
                fModified = null,
                fModifiedID = 69,
                fTime = DateTime.Now,
            };
        }
        public int GetStoreId(string storename)
        {
            systblApp_TaxReceipt_Store entity;
            if (_StoreCache.TryGetValue(storename, out entity))
            {
                return entity.fStoreId;
            }

            entity = _storeService.GetAllStores().Where(m => m.fName.Equals(storename)).FirstOrDefault();
            if (entity != null)
            {
                _StoreCache.Add(storename, entity);
                return entity.fStoreId;
            }

            return 0;
        }
        private string GetOrderNo(EnvioBOLETA objBoleta)
        {
            var orderNo = GetCustomFieldFromDocument(objBoleta, "NumeroOrden");

            if (string.IsNullOrEmpty(orderNo))
            {
                var item = objBoleta.SetDTE.DTE.Documento.Detalle.NmbItem;
                orderNo = item.Split(' ').Select(m => m.Trim()).Where(p => p.Contains("CL")).FirstOrDefault();
            }

            return orderNo;
        }
        private string GetCustomFieldFromDocument(EnvioBOLETA objBoleta, string fieldName)
        {
            var result = objBoleta.
                 Personalizados.
                 DocPersonalizado.
                 campoString.Where(m => m.name.Equals(fieldName)).FirstOrDefault();
            if (result == null) return string.Empty;
            return result.Value;
        }
        private List<int> ReserveNewIds(string entityName, int quantity)
        {
            var ids = new List<int>();
            var rangeid = GetNewId(entityName, quantity);
            ids = Enumerable.Range(rangeid.Item1, rangeid.Item2 - rangeid.Item1 + 1).ToList();
            return ids;
        }

        private void CreateTaxEntities(EnvioBOLETA objBoleta, string rutSender, string rutReceiver, out int? idSender, out int? idReceiver, systblApp_CoreAPI_TaxEntity senderEntity, systblApp_CoreAPI_TaxEntity receiverEntity)
        {
            if (!TaxEntityExists(rutSender, out idSender))
            {
                var newsenderEntity = SaveSenderEntity(objBoleta);
                senderEntity.fTaxEntityId = newsenderEntity.fTaxEntityId;
            }
            else
            {
                senderEntity.fTaxEntityId = idSender.Value;
            }

            if (!TaxEntityExists(rutReceiver, out idReceiver))
            {
                var newreceiverEntity = SaveReceiverEntity(objBoleta);
                receiverEntity.fTaxEntityId = newreceiverEntity.fTaxEntityId;
            }
            else
            {
                receiverEntity.fTaxEntityId = idReceiver.Value;
            }
        }

        private systblApp_CoreAPI_TaxEntity SaveReceiverEntity(EnvioBOLETA objBoleta)
        {
            var newreceiverEntity = MapReceiverEntity(objBoleta);
            _taxEntityService = new TaxEntityService(new TaxEntityRepository(new ReceiptDbContext()));
            _taxEntityService.CreateTaxEntity(newreceiverEntity);
            _taxEntityService.SaveChanges();
            return newreceiverEntity;
        }

        private systblApp_CoreAPI_TaxEntity MapReceiverEntity(EnvioBOLETA objBoleta)
        {
            return new systblApp_CoreAPI_TaxEntity()
            {
                fTaxEntityId = GetNewTaxEntityId(),
                fRUT = objBoleta.SetDTE.DTE.Documento.Encabezado.Receptor.RUTRecep,
                fFullName = objBoleta.SetDTE.DTE.Documento.Encabezado.Receptor.RznSocRecep,
                fFirstName = objBoleta.SetDTE.DTE.Documento.Encabezado.Receptor.RznSocRecep,
                fMiddleName = string.Empty,
                fLastName1 = string.Empty,
                fLastName2 = string.Empty,
                fLineOfBusiness = string.Empty,
                fNationality = "CL",
                fCellPhone = string.Empty,
                fCountryOfBirth = string.Empty,
                fDateOfBirth = null,
                fEconomicActivity = 0,
                fEmail = string.Empty,
                fOccupation = string.Empty,
                fPhone = string.Empty,
                fGender = string.Empty,
                fChanged = null,
                fDelete = false,
                fDisabled = false,
                fModified = null,
                fModifiedID = 69,
                fTime = DateTime.Now,
            };
        }

        private systblApp_CoreAPI_TaxEntity MapSenderEntity(EnvioBOLETA objBoleta)
        {
            return new systblApp_CoreAPI_TaxEntity()
            {
                fTaxEntityId = GetNewTaxEntityId(),
                fRUT = objBoleta.SetDTE.DTE.Documento.Encabezado.Emisor.RUTEmisor,
                fFullName = objBoleta.SetDTE.DTE.Documento.Encabezado.Emisor.RznSocEmisor,
                fFirstName = objBoleta.SetDTE.DTE.Documento.Encabezado.Emisor.RznSocEmisor,
                fLineOfBusiness = objBoleta.SetDTE.DTE.Documento.Encabezado.Emisor.GiroEmisor,
                TaxAddresses = new List<systblApp_CoreAPI_TaxAddress>(),
                fNationality = "CL",
                fCellPhone = string.Empty,
                fCountryOfBirth = string.Empty,
                fDateOfBirth = null,
                fEconomicActivity = 0,
                fEmail = string.Empty,
                fLastName1 = string.Empty,
                fLastName2 = string.Empty,
                fMiddleName = string.Empty,
                fOccupation = string.Empty,
                fGender = string.Empty,
                fPhone = string.Empty,
                fChanged = null,
                fDelete = false,
                fDisabled = false,
                fModified = null,
                fModifiedID = 69,
                fTime = DateTime.Now,
            };
        }

        private int GetNewTaxEntityId()
        {
            return GetNewId("systblApp_CoreAPI_TaxEntity");
        }
        private int GetNewId(string entityName)
        {
            return GetNewId(entityName, 1).Item2;
        }

        private Tuple<int, int> GetNewId(string entityName, int quantity)
        {
            var sequenceService = new SequenceService(new SequenceRepository(new ReceiptDbContext())); ;//App.container.Get<ISequenceService>();
            var max = sequenceService.GetAllSequences().Count() != 0 ? sequenceService.GetAllSequences().Max(m => m.fSequenceId) : 0;
            var entity = sequenceService.GetAllSequences().Where(m => m.fEntityName.Equals(entityName)).FirstOrDefault(); //db1.systblApp_CoreApi_Sequence.Find(entityName);
            int? start = 1;
            int nextId = 1;
            if (entity == null)
            {
                try
                {
                    var sequenceService2 = new SequenceService(new SequenceRepository(new ReceiptDbContext()));
                    var entity2 = new systblApp_CoreApi_Sequence
                    {
                        fSequenceId = max + 1,
                        fEntityName = entityName,
                        fStartId = 1,
                        fCurrentId = null,
                    };
                    sequenceService2.CreateSequence(entity2);
                    sequenceService2.SaveChanges();

                    start = 1;
                    nextId = quantity;
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }              
            }
            else
            {
                start = entity.fCurrentId == null ? entity.fStartId : entity.fCurrentId.Value + 1;
                nextId = entity.fCurrentId == null ? quantity : entity.fCurrentId.Value + quantity;
            }

            var sequenceService3 = new SequenceService(new SequenceRepository(new ReceiptDbContext()));
            var entity3 = sequenceService.GetAllSequences().Where(m => m.fEntityName.Equals(entityName)).FirstOrDefault();
            entity3.fCurrentId = nextId;
            sequenceService3.UpdateSequence(entity3);
            sequenceService3.SaveChanges();

            return Tuple.Create(start.Value, nextId);
        }
        private systblApp_CoreAPI_TaxEntity SaveSenderEntity(EnvioBOLETA objBoleta)
        {
            var newsenderEntity = MapSenderEntity(objBoleta);
            AddAddressToSenderEntity(objBoleta, newsenderEntity);
            _taxEntityService = new TaxEntityService(new TaxEntityRepository(new ReceiptDbContext()));
            _taxEntityService.CreateTaxEntity(newsenderEntity);
            _taxEntityService.SaveChanges();
            return newsenderEntity;
        }

        private void AddAddressToSenderEntity(EnvioBOLETA objBoleta, systblApp_CoreAPI_TaxEntity senderEntity)
        {
            senderEntity.TaxAddresses.Add(
                new systblApp_CoreAPI_TaxAddress
                {
                    fTaxAddressId = GetNewTaxAddressId(),
                    fTaxEntityId = senderEntity.fTaxEntityId,
                    TaxEntity = senderEntity,
                    fAddress = objBoleta.SetDTE.DTE.Documento.Encabezado.Emisor.DirOrigen,
                    fComuna = objBoleta.SetDTE.DTE.Documento.Encabezado.Emisor.CmnaOrigen,
                    fCity = objBoleta.SetDTE.DTE.Documento.Encabezado.Emisor.CiudadOrigen,
                    fChanged = null,
                    fDelete = false,
                    fDisabled = false,
                    fModified = null,
                    fModifiedID = 69,
                    fTime = DateTime.Now,
                });
        }
        private int GetNewTaxAddressId()
        {
            return GetNewId("systblApp_CoreAPI_TaxAddress");
        }
        private bool TaxEntityExists(string rut, out int? id)
        {
            systblApp_CoreAPI_TaxEntity entity;
            if (_TaxEntityCache.TryGetValue(rut, out entity))
            {
                id = entity.fTaxEntityId;
                return true;
            }

            _taxEntityService = new TaxEntityService(new TaxEntityRepository(new ReceiptDbContext()));
            entity = _taxEntityService.GetAllTaxEntitys().Where(m => m.fRUT.Equals(rut)).FirstOrDefault();
            id = null;

            if (entity != null)
            {
                _TaxEntityCache.Add(rut, entity);
                id = entity.fTaxEntityId;
                return true;
            }

            return false;

        }

    }
}
