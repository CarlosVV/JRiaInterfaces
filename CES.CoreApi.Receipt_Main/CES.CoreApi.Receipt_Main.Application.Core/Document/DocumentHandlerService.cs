using CES.CoreApi.Receipt_Main.Domain.Core.Documents;
using CES.CoreApi.Receipt_Main.Domain.Core.Security;
using CES.CoreApi.Receipt_Main.Domain.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Application.Core.Document
{
    public class DocumentHandlerService
    {
        private int chunkfolio = 100;
        private static readonly decimal _TaxRate = 0.19m;
        private IDocumentService _documentService;
        private readonly ITaxEntityService _taxEntityService;
        private readonly ITaxAddressService _taxAddressService;
        private static ISequenceService _sequenceService;
        private readonly IStoreService _storeService;
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
                Id = id,
                DocumentId = dbBoleta.Id,
                Document = dbBoleta,
                LineNumber = objBoleta.SetDTE.DTE.Documento.Detalle.NroLinDet,
                Description = objBoleta.SetDTE.DTE.Documento.Detalle.NmbItem,
                QtyProd = objBoleta.SetDTE.DTE.Documento.Detalle.QtyItem,
                Price = objBoleta.SetDTE.DTE.Documento.Detalle.PrcItem,
                Amount = objBoleta.SetDTE.DTE.Documento.Detalle.MontoItem,
                Code = string.Empty,
                DocRefFolio = objBoleta.SetDTE.DTE.Documento.TED.DD.F.ToString(),
                DocRefType = objBoleta.SetDTE.DTE.Documento.TED.DD.TD.ToString(),
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
                Id = id,
                DocumentType = objBoleta.SetDTE.DTE.Documento.TED.DD.TD.ToString(),
                Folio = (int)objBoleta.SetDTE.DTE.Documento.TED.DD.F,
                IssuedDate = objBoleta.SetDTE.DTE.Documento.TED.DD.FE,
                PaymentDate = objBoleta.SetDTE.DTE.Documento.TED.DD.FE,
                StoreName = storename,
                OrderNo = GetOrderNo(objBoleta),
                CashierName = GetCustomFieldFromDocument(objBoleta, "Cajero"),
                CashRegisterNumber = GetCustomFieldFromDocument(objBoleta, "Caja"),
                Amount = amount,
                TaxAmount = tax,
                TotalAmount = totalamount,
                Description = objBoleta.SetDTE.DTE.Documento.Detalle.NmbItem,
                DocumentDetails = new List<systblApp_CoreAPI_DocumentDetail>(),
                TimestampDocument = objBoleta.SetDTE.Caratula.TmstFirmaEnv,
                TimestampSent = objBoleta.SetDTE.DTE.Documento.TmstFirma,
                RecAgent = GetStoreId(storename),
                DownloadedSII = true,
                ExemptAmount = 0,
                SentToSII = true,
                PayAgent = 0,
                ReceiverId = receiverEntity.Id,
                SenderId = senderEntity.Id,
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
                return entity.Id;
            }

            entity = _storeService.GetAllStores().Where(m => m.Name.Equals(storename)).FirstOrDefault();
            if (entity != null)
            {
                _StoreCache.Add(storename, entity);
                return entity.Id;
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
                senderEntity.Id = newsenderEntity.Id;
            }
            else
            {
                senderEntity.Id = idSender.Value;
            }

            if (!TaxEntityExists(rutReceiver, out idReceiver))
            {
                var newreceiverEntity = SaveReceiverEntity(objBoleta);
                receiverEntity.Id = newreceiverEntity.Id;
            }
            else
            {
                receiverEntity.Id = idReceiver.Value;
            }
        }

        private systblApp_CoreAPI_TaxEntity SaveReceiverEntity(EnvioBOLETA objBoleta)
        {

            var newreceiverEntity = MapReceiverEntity(objBoleta);
            _taxEntityService.CreateTaxEntity(newreceiverEntity);

            return newreceiverEntity;
        }

        private systblApp_CoreAPI_TaxEntity MapReceiverEntity(EnvioBOLETA objBoleta)
        {
            return new systblApp_CoreAPI_TaxEntity()
            {
                Id = GetNewTaxEntityId(),
                RUT = objBoleta.SetDTE.DTE.Documento.Encabezado.Receptor.RUTRecep,
                FullName = objBoleta.SetDTE.DTE.Documento.Encabezado.Receptor.RznSocRecep,
                FirstName = objBoleta.SetDTE.DTE.Documento.Encabezado.Receptor.RznSocRecep,
                MiddleName = string.Empty,
                LastName1 = string.Empty,
                LastName2 = string.Empty,
                LineOfBusiness = string.Empty,
                Nationality = "CL",
                CellPhone = string.Empty,
                CountryOfBirth = string.Empty,
                DateOfBirth = null,
                EconomicActivity = 0,
                Email = string.Empty,
                Occupation = string.Empty,
                Phone = string.Empty,
                Gender = string.Empty,
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
                Id = GetNewTaxEntityId(),
                RUT = objBoleta.SetDTE.DTE.Documento.Encabezado.Emisor.RUTEmisor,
                FullName = objBoleta.SetDTE.DTE.Documento.Encabezado.Emisor.RznSocEmisor,
                FirstName = objBoleta.SetDTE.DTE.Documento.Encabezado.Emisor.RznSocEmisor,
                LineOfBusiness = objBoleta.SetDTE.DTE.Documento.Encabezado.Emisor.GiroEmisor,
                TaxAddresses = new List<systblApp_CoreAPI_TaxAddress>(),
                Nationality = "CL",
                CellPhone = string.Empty,
                CountryOfBirth = string.Empty,
                DateOfBirth = null,
                EconomicActivity = 0,
                Email = string.Empty,
                LastName1 = string.Empty,
                LastName2 = string.Empty,
                MiddleName = string.Empty,
                Occupation = string.Empty,
                Gender = string.Empty,
                Phone = string.Empty,
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
            //_sequenceService = App.container.Get<ISequenceService>();
            var entity = _sequenceService.GetAllSequences().Where(m => m.EntityName.Equals(entityName)).FirstOrDefault(); //db1.systblApp_CoreApi_Sequence.Find(entityName);
            var start = entity.CurrentId == null ? entity.StartId : entity.CurrentId.Value + 1;
            var nextId = entity.CurrentId == null ? quantity : entity.CurrentId.Value + quantity;

            entity.CurrentId = nextId;
            _sequenceService.UpdateSequence(entity);
            _sequenceService.SaveChanges();

            return Tuple.Create(start.Value, nextId);

        }
        private systblApp_CoreAPI_TaxEntity SaveSenderEntity(EnvioBOLETA objBoleta)
        {
            var newsenderEntity = MapSenderEntity(objBoleta);
            AddAddressToSenderEntity(objBoleta, newsenderEntity);
            _taxEntityService.CreateTaxEntity(newsenderEntity);
            _taxEntityService.SaveChanges();
            return newsenderEntity;
        }

        private void AddAddressToSenderEntity(EnvioBOLETA objBoleta, systblApp_CoreAPI_TaxEntity senderEntity)
        {
            senderEntity.TaxAddresses.Add(
                new systblApp_CoreAPI_TaxAddress
                {
                    Id = GetNewTaxAddressId(),
                    TaxEntityId = senderEntity.Id,
                    TaxEntity = senderEntity,
                    Address = objBoleta.SetDTE.DTE.Documento.Encabezado.Emisor.DirOrigen,
                    Comuna = objBoleta.SetDTE.DTE.Documento.Encabezado.Emisor.CmnaOrigen,
                    City = objBoleta.SetDTE.DTE.Documento.Encabezado.Emisor.CiudadOrigen,
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
                id = entity.Id;
                return true;
            }


            entity = _taxEntityService.GetAllTaxEntitys().Where(m => m.RUT.Equals(rut)).FirstOrDefault();
            id = null;

            if (entity != null)
            {
                _TaxEntityCache.Add(rut, entity);
                id = entity.Id;
                return true;
            }

            return false;

        }

    }
}
