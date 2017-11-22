using CES.CoreApi.Receipt_Main.Domain.Core.Documents;
using CES.CoreApi.Receipt_Main.Domain.Core.Security;
using CES.CoreApi.Receipt_Main.Domain.Core.Services;
using CES.CoreApi.Receipt_Main.Infrastructure.Data;
using CES.CoreApi.Receipt_Main.Infrastructure.Data.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
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
        private static Dictionary<string, actblTaxDocument_Entity> _TaxEntityCache;
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

            _TaxEntityCache = new Dictionary<string, actblTaxDocument_Entity>();
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

            _TaxEntityCache = new Dictionary<string, actblTaxDocument_Entity>();
            _StoreCache = new Dictionary<string, systblApp_TaxReceipt_Store>();
        }
        public void SaveDocument(int folio, int folioFinal, ref int indexchunk, ref int acumchunk, string xmlContent, EnvioBOLETA objBoleta, ref List<int> detailids, ref List<int> docids)
        {
            var rutSender = objBoleta.SetDTE.DTE.Documento.Encabezado.Emisor.RUTEmisor;
            var rutReceiver = objBoleta.SetDTE.DTE.Documento.Encabezado.Receptor.RUTRecep;
            int? idSender = null;
            int? idReceiver = null;
            var senderEntity = new actblTaxDocument_Entity();
            var receiverEntity = new actblTaxDocument_Entity();

            //Get o Set Entity and Address
            CreateTaxEntities(objBoleta, rutSender, rutReceiver, out idSender, out idReceiver, senderEntity, receiverEntity);

            //Save Document and Detail
            CreateDocument(indexchunk, xmlContent, objBoleta, ref detailids, ref docids, senderEntity, receiverEntity);

            if (indexchunk >= Chunkfolio || folio == folioFinal)
            {
                _documentService.SaveChanges();
                acumchunk = acumchunk + indexchunk;
                indexchunk = 0;
                docids = null;
                detailids = null;
            }
        }
        private void CreateDocument(int indexchunk, string xmlContent, EnvioBOLETA objBoleta, ref List<int> detailids, ref List<int> docids, actblTaxDocument_Entity senderEntity, actblTaxDocument_Entity receiverEntity)
        {
            if (detailids == null)
            {
                detailids = ReserveNewIds("systblApp_CoreAPI_DocumentDetail", Chunkfolio);
            }

            if (docids == null)
            {
                docids = ReserveNewIds("systblApp_CoreAPI_Document", Chunkfolio);
            }

            var dbBoleta = MapDocumentFromXmlObject(docids[indexchunk - 1], xmlContent, objBoleta, senderEntity, receiverEntity);

            AddDocumentDetailFromXmlObject(detailids[indexchunk - 1], objBoleta, dbBoleta);

            _documentService.CreateDocument(dbBoleta);
        }
        private void AddDocumentDetailFromXmlObject(int id, EnvioBOLETA objBoleta, actblTaxDocument dbBoleta)
        {
            dbBoleta.DocumentDetails.Add(new actblTaxDocument_Detail
            {
                fDetailID = id,
                fDocumentID = dbBoleta.fDocumentID,
                Document = dbBoleta,
                fLineNumber = objBoleta.SetDTE.DTE.Documento.Detalle.NroLinDet,
                fDescription = objBoleta.SetDTE.DTE.Documento.Detalle.NmbItem,
                fItemCount = objBoleta.SetDTE.DTE.Documento.Detalle.QtyItem,
                fPrice = objBoleta.SetDTE.DTE.Documento.Detalle.PrcItem,
                fAmount = objBoleta.SetDTE.DTE.Documento.Detalle.MontoItem,
                fCode = string.Empty,              
                fChanged = false,
                fDelete = false,
                fDisabled = false,
                fModified = DateTime.Now,
                fModifiedID = 69,
                fTime = DateTime.Now,
            });

        }
        private actblTaxDocument MapDocumentFromXmlObject(int id, string xmlContent, EnvioBOLETA objBoleta, actblTaxDocument_Entity senderEntity, actblTaxDocument_Entity receiverEntity)
        {
            var totalamount = objBoleta.SetDTE.DTE.Documento.Encabezado.Totales.MntTotal;
            var amount = Math.Floor(objBoleta.SetDTE.DTE.Documento.Encabezado.Totales.MntTotal / (1 + _TaxRate));
            var tax = totalamount - amount;
            var storename = GetCustomFieldFromDocument(objBoleta, "Tienda");
            var docTypeID = int.Parse(objBoleta.SetDTE.DTE.Documento.TED.DD.TD.ToString());

            return new actblTaxDocument
            {
                fDocumentID = id,
                fDocumentTypeID = docTypeID,
                fAuthorizationNumber =  objBoleta.SetDTE.DTE.Documento.TED.DD.F.ToString(),
                fIssuedDate = objBoleta.SetDTE.DTE.Documento.TED.DD.FE,
                fPaymentDate = objBoleta.SetDTE.DTE.Documento.TED.DD.FE,
                fStoreName = storename,
                fTransactionNo = GetOrderNo(objBoleta),
                fCashierName = GetCustomFieldFromDocument(objBoleta, "Cajero"),
                fCashRegisterNumber = GetCustomFieldFromDocument(objBoleta, "Caja"),
                fAmount = amount,
                fTaxAmount = tax,
                fTotalAmount = totalamount,
                fDescription = objBoleta.SetDTE.DTE.Documento.Detalle.NmbItem,
                DocumentDetails = new List<actblTaxDocument_Detail>(),
                fTimeStampDocument = objBoleta.SetDTE.Caratula.TmstFirmaEnv,
                fTimeStampSent = objBoleta.SetDTE.DTE.Documento.TmstFirma,
                fRecAgentID = GetStoreId(storename),
                fDownloadedIRS = true,
                fExemptAmount = 0,
                fSentToIRS = true,
                fPayAgentID = 0,
                fChanged = false,
                fDelete = false,
                fDisabled = false,
                fModified = DateTime.Now,
                fModifiedID = 69,
                fTime = DateTime.Now,
                fXmlDocumentContent = xmlContent
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

        private void CreateTaxEntities(EnvioBOLETA objBoleta, string rutSender, string rutReceiver, out int? idSender, out int? idReceiver, actblTaxDocument_Entity senderEntity, actblTaxDocument_Entity receiverEntity)
        {
            if (!TaxEntityExists(rutSender, out idSender))
            {
                var newsenderEntity = SaveSenderEntity(objBoleta);
                senderEntity.fEntityID = newsenderEntity.fEntityID;
            }
            else
            {
                senderEntity.fEntityID = idSender.Value;
            }

            if (!TaxEntityExists(rutReceiver, out idReceiver))
            {
                var newreceiverEntity = SaveReceiverEntity(objBoleta);
                receiverEntity.fEntityID = newreceiverEntity.fEntityID;
            }
            else
            {
                receiverEntity.fEntityID = idReceiver.Value;
            }
        }

        private actblTaxDocument_Entity SaveReceiverEntity(EnvioBOLETA objBoleta)
        {
            var newreceiverEntity = MapReceiverEntity(objBoleta);
            _taxEntityService = new TaxEntityService(new TaxEntityRepository(new ReceiptDbContext()));
            _taxEntityService.CreateTaxEntity(newreceiverEntity);
            _taxEntityService.SaveChanges();
            return newreceiverEntity;
        }

        private actblTaxDocument_Entity MapReceiverEntity(EnvioBOLETA objBoleta)
        {
            return new actblTaxDocument_Entity()
            {
                fEntityID = GetNewTaxEntityId(),
                fTaxID = objBoleta.SetDTE.DTE.Documento.Encabezado.Receptor.RUTRecep,
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

        private actblTaxDocument_Entity MapSenderEntity(EnvioBOLETA objBoleta)
        {
            return new actblTaxDocument_Entity()
            {
                fEntityID = GetNewTaxEntityId(),
                fTaxID = objBoleta.SetDTE.DTE.Documento.Encabezado.Emisor.RUTEmisor,
                fFullName = objBoleta.SetDTE.DTE.Documento.Encabezado.Emisor.RznSocEmisor,
                fFirstName = objBoleta.SetDTE.DTE.Documento.Encabezado.Emisor.RznSocEmisor,
                fLineOfBusiness = objBoleta.SetDTE.DTE.Documento.Encabezado.Emisor.GiroEmisor,
                TaxAddresses = new List<actblTaxDocument_Entity_Address>(),
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
            //var sequenceService = new SequenceService(new SequenceRepository(new ReceiptDbContext())); ;//App.container.Get<ISequenceService>();

            var sequenceService = new SequenceService();

            /*var max = sequenceService.GetAllSequences().Count() != 0 ? sequenceService.GetAllSequences().Max(m => m.fSequenceID) : 0;
            var entity = sequenceService.GetAllSequences().Where(m => m.fEntityName.Equals(entityName)).FirstOrDefault(); //db1.systblApp_CoreApi_Sequence.Find(entityName);
            int? start = 1;
            int nextId = 1;
            if (entity == null)
            {
                try
                {
                    var sequenceService2 = new SequenceService(new SequenceRepository(new ReceiptDbContext()));
                    var entity2 = new actblTaxDocument_TableSeq
                    {
                        fSequenceID = max + 1,
                        fEntityName = entityName,
                        fStartId = 1,
                        fCurrentId = null,
                    };
                    sequenceService2.CreateSequence(entity2);
                    sequenceService2.SaveChanges();

                    start = 1;
                    nextId = quantity;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            else
            {
                start = entity.fCurrentId == null ? entity.fStartId : entity.fCurrentId.Value + 1;
                nextId = entity.fCurrentId == null ? quantity : entity.fCurrentId.Value + quantity;
            }

            //var sequenceService3 = new SequenceService(new SequenceRepository(new ReceiptDbContext()));
            var sequenceService3 = new SequenceService();
            var entity3 = sequenceService.GetAllSequences().Where(m => m.fEntityName.Equals(entityName)).FirstOrDefault();
            entity3.fCurrentId = nextId;
            sequenceService3.UpdateSequence(entity3);
            sequenceService3.SaveChanges();

            return Tuple.Create(start.Value, nextId);
            */

            int start = sequenceService.GetSequence();
            int next = sequenceService.GetSequence();

            for (int i = 0; i < quantity; i++)
            {
                next = sequenceService.GetSequence();
            }

            return Tuple.Create(start, next);
        }
        private actblTaxDocument_Entity SaveSenderEntity(EnvioBOLETA objBoleta)
        {
            var newsenderEntity = MapSenderEntity(objBoleta);
            AddAddressToSenderEntity(objBoleta, newsenderEntity);
            _taxEntityService = new TaxEntityService(new TaxEntityRepository(new ReceiptDbContext()));
            _taxEntityService.CreateTaxEntity(newsenderEntity);
            _taxEntityService.SaveChanges();
            return newsenderEntity;
        }

        private void AddAddressToSenderEntity(EnvioBOLETA objBoleta, actblTaxDocument_Entity senderEntity)
        {
            senderEntity.TaxAddresses.Add(
                new actblTaxDocument_Entity_Address
                {
                    fAddressID = GetNewTaxAddressId(),
                    fEntityID = senderEntity.fEntityID,
                    TaxEntity = senderEntity,
                    fAddress = objBoleta.SetDTE.DTE.Documento.Encabezado.Emisor.DirOrigen,
                    fCounty = objBoleta.SetDTE.DTE.Documento.Encabezado.Emisor.CmnaOrigen,
                    fCity = objBoleta.SetDTE.DTE.Documento.Encabezado.Emisor.CiudadOrigen,
                    fChanged = false,
                    fDelete = false,
                    fDisabled = false,
                    fModified = DateTime.Now,
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
            actblTaxDocument_Entity entity;
            if (_TaxEntityCache.TryGetValue(rut, out entity))
            {
                id = entity.fEntityID;
                return true;
            }

            _taxEntityService = new TaxEntityService(new TaxEntityRepository(new ReceiptDbContext()));
            entity = _taxEntityService.GetAllTaxEntitys().Where(m => m.fTaxID.Equals(rut)).FirstOrDefault();
            id = null;

            if (entity != null)
            {
                _TaxEntityCache.Add(rut, entity);
                id = entity.fEntityID;
                return true;
            }

            return false;

        }

        private string ReduceXmlContent(string xmlContent)
        {
            var compressed = Zip(xmlContent);
            var outstring = Convert.ToBase64String(compressed);
            return outstring;
        }

        private string UnReduceXmlContent(string compressed)
        {
            var bytes = Convert.FromBase64String(compressed);
            var result = Unzip(bytes);
            
            return result;
        }

        public static void CopyTo(Stream src, Stream dest)
        {
            byte[] bytes = new byte[4096];

            int cnt;

            while ((cnt = src.Read(bytes, 0, bytes.Length)) != 0)
            {
                dest.Write(bytes, 0, cnt);
            }
        }

        public static byte[] Zip(string str)
        {
            var bytes = Encoding.UTF8.GetBytes(str);

            using (var msi = new MemoryStream(bytes))
            using (var mso = new MemoryStream())
            {
                using (var gs = new GZipStream(mso, CompressionMode.Compress))
                {
                    //msi.CopyTo(gs);
                    CopyTo(msi, gs);
                }

                return mso.ToArray();
            }
        }

        public static string Unzip(byte[] bytes)
        {
            using (var msi = new MemoryStream(bytes))
            using (var mso = new MemoryStream())
            {
                using (var gs = new GZipStream(msi, CompressionMode.Decompress))
                {
                    //gs.CopyTo(mso);
                    CopyTo(gs, mso);
                }

                return Encoding.UTF8.GetString(mso.ToArray());
            }
        }
    }
}
