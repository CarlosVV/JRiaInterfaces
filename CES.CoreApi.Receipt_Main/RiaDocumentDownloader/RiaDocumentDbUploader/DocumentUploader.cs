using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfLocalDb.Repository;
using WpfLocalDb.Utilities;
using WpfLocalDb.Xml.Boleta;

namespace RiaDocumentDbUploader
{
    public class DocumentUploader
    {
        private TaxDb _db = new TaxDb();
        private static readonly decimal _TaxRate = 0.19m;
        private static Dictionary<string, systblApp_CoreAPI_TaxEntity> _TaxEntityCache;
        private static Dictionary<string, systblApp_TaxReceipt_Store> _StoreCache;
        private string _doctype;
        private int? _folioend;
        private int? _foliostart;
        private static string _xml_in;
        private static string _xml_out;
        private object _object_lock = new Object();
        private static int _filesToProcess;
        private static int _chunkfolio = 100;

        public DocumentUploader(string doctype, string xml_in, string xml_out, int filesToProcess, int? foliostart, int? folioend)
        {
            _doctype = doctype;
            _xml_in = xml_in;
            _xml_out = xml_out;
            _filesToProcess = filesToProcess;
            _foliostart = foliostart;
            _folioend = folioend;
            _TaxEntityCache = new Dictionary<string, systblApp_CoreAPI_TaxEntity>();
            _StoreCache = new Dictionary<string, systblApp_TaxReceipt_Store>();
        }
        public void CargarDocumentosEnBd()
        {
            var parserBoletas = new XmlDocumentParser<EnvioBOLETA>();
            var respuesta = string.Empty;
            var folio = 0;
            var folioInicio = 0;
            var folioFinal = 0;
            var indexchunk = 1;
            var acumchunk = 0;
            IEnumerable<int> existingdocsInDb = null;
            SortedList<int, EnvioBOLETA> documentsToInsertInDb = null;
            SortedList<int, string> documentsExistingDowloadedByFolio = null;
            SortedList<int, string> documentsExistingInProcessByFolio = null;
            IEnumerable<KeyValuePair<int, string>> documentsExistingDowloadedByFolioToProcess = null;
            IEnumerable<string> existingdocsInFolder = null;
            IEnumerable<string> existingdocsInProcess = null;
            IEnumerable<KeyValuePair<int, string>> docs = null;
            EnvioBOLETA objXmlDocumentToInsert = null;
            List<int> detailids = null;
            List<int> docids = null;
            int numberofchk;
            int restchunk;
            int totalOfDocuments;
            int indexProcessingFiles = 0;

            try
            {
                while (true)
                {
                    respuesta = string.Empty;
                    folio = 0;
                    folioInicio = 0;
                    folioFinal = 0;
                    indexchunk = 1;
                    acumchunk = 0;
                    documentsToInsertInDb = new SortedList<int, EnvioBOLETA>();

                    existingdocsInDb = GetExistingDocumentsInDatabase();

                    if (existingdocsInFolder == null)
                    {
                        existingdocsInFolder = GetExistingXmlDocumentsDownloaded(_xml_in);
                        existingdocsInProcess = GetExistingXmlDocumentsDownloaded(_xml_out);

                        documentsExistingDowloadedByFolio = new SortedList<int, string>();
                        documentsExistingInProcessByFolio = new SortedList<int, string>();

                        GetExistingDocuments(documentsExistingDowloadedByFolio, existingdocsInFolder);
                        GetExistingDocuments(documentsExistingInProcessByFolio, existingdocsInProcess);

                        documentsExistingDowloadedByFolioToProcess = documentsExistingDowloadedByFolio.Except(documentsExistingInProcessByFolio);
                        documentsExistingDowloadedByFolioToProcess = documentsExistingDowloadedByFolioToProcess.Except(existingdocsInDb.Select(p => new KeyValuePair<int, string>(p, null)));
                        documentsExistingDowloadedByFolioToProcess = documentsExistingDowloadedByFolioToProcess.Where(m => (!_foliostart.HasValue || m.Key >= _foliostart) && (!_folioend.HasValue || m.Key <= _folioend));
                    }

                    if (_filesToProcess > documentsExistingDowloadedByFolioToProcess.Count())
                    {
                        _filesToProcess = documentsExistingDowloadedByFolioToProcess.Count();
                    }

                    if (_filesToProcess == 0)
                    {
                        Console.WriteLine("No hay archivos para procesar");
                        break;
                    }

                    Console.WriteLine("Procesando archivos");

                    docs = documentsExistingDowloadedByFolioToProcess.OrderBy(m => m.Key).Skip(_filesToProcess * indexProcessingFiles).Take(_filesToProcess);

                    GetXmlDocumentsParsedToUploadInDatabase(parserBoletas, respuesta, documentsToInsertInDb, docs);

                    totalOfDocuments = documentsToInsertInDb.Count();

                    if (_chunkfolio > totalOfDocuments)
                    {
                        _chunkfolio = totalOfDocuments;
                    }

                    numberofchk = totalOfDocuments / _chunkfolio;
                    restchunk = totalOfDocuments % _chunkfolio;
                    folioInicio = documentsToInsertInDb.First().Key;
                    folioFinal = documentsToInsertInDb.Last().Key;

                    foreach (var docXml in documentsToInsertInDb)
                    {
                        objXmlDocumentToInsert = docXml.Value;
                        folio = docXml.Key;

                        if (objXmlDocumentToInsert == null || folio == 0)
                        {
                            Console.WriteLine($"Documento es NULL o Folio es 0");
                            continue;
                        }

                        if (DocumentExistInDatabase(_doctype, folio))
                        {
                            continue;
                        }

                        SaveDocument(folio, folioFinal, ref indexchunk, ref acumchunk, objXmlDocumentToInsert, ref detailids, ref docids);

                        indexchunk++;
                    }

                    Console.WriteLine($"Descargado todos los docs de {folioInicio} a {folioFinal}");
                    indexProcessingFiles++;
                }

                Console.WriteLine($"Proceso finalizado...");
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var eve in ex.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }

                Console.WriteLine($"Error en folio {folio}: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en folio {folio}: {ex.Message}");

                if (ex.InnerException != null)
                {
                    Console.WriteLine($"{ex.InnerException.Message}");
                }

                try
                {
                    if (detailids != null && docids != null)
                    {
                        RollbackSequenceAssigment(detailids, docids);
                    }
                }
                catch(Exception ex2)
                {
                    Console.WriteLine($"Error rollback ids en folio {folio}: {ex2.Message}");
                }
              
            }
        }

        private void RollbackSequenceAssigment(List<int> detailids, List<int> docids)
        {
            using (var _db1 = new TaxDb())
            {
                var documentSequence = _db1.systblApp_CoreApi_Sequence.Where(m => m.EntityName == "systblApp_CoreAPI_Document").FirstOrDefault();
                var documentDetailSequence = _db1.systblApp_CoreApi_Sequence.Where(m => m.EntityName == "systblApp_CoreAPI_DocumentDetail").FirstOrDefault();
                documentSequence.CurrentId = docids.FirstOrDefault() - 1;
                documentDetailSequence.CurrentId = detailids.FirstOrDefault() - 1;
                _db1.Entry(documentSequence).State = EntityState.Modified;
                _db1.Entry(documentDetailSequence).State = EntityState.Modified;
                _db1.SaveChanges();
            }
        }

        private bool DocumentExistInDatabase(string _tipo_documento, int folio)
        {
            using (var ctx = new TaxDb())
            {
                return ctx.systblApp_CoreAPI_Document.Any(t => t.DocumentType == _tipo_documento && t.Folio == folio);
            }
        }

        private void GetXmlDocumentsParsedToUploadInDatabase(XmlDocumentParser<EnvioBOLETA> parserBoletas, string respuesta, SortedList<int, EnvioBOLETA> documentsToInsertInDb, IEnumerable<KeyValuePair<int, string>> docs)
        {
            Parallel.ForEach(docs, (docfile) =>
            {
                lock (_object_lock)
                {
                    if (System.IO.File.Exists(docfile.Value))
                    {
                        using (var reader = new StreamReader(docfile.Value))
                        {
                            respuesta = reader.ReadToEnd();
                            reader.Close();

                            if (!string.IsNullOrWhiteSpace(respuesta))
                            {
                                try
                                {
                                    if (docfile.Key != 0 && docfile.Value != null)
                                    {
                                        var objParseXmlDoc = parserBoletas.GetDocumentObjectFromString(respuesta);
                                        documentsToInsertInDb.Add(docfile.Key, objParseXmlDoc);
                                        File.Move(docfile.Value, $"{_xml_out}{Path.GetFileName(docfile.Value)}");
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine($"Error parsing folio: [{docfile.Key}]");
                                    Console.WriteLine($"Detalle: {docfile.Value} -> [{ex}]");
                                    Program.log.Error($"Detalle: {docfile.Value} -> [{ex}]");
                                }
                            }
                            else
                            {
                                Console.WriteLine($"[{docfile.Value}] Archivo XML no tiene datos");
                            }
                        }
                    }
                }
            });
        }

        private void GetExistingDocuments(SortedList<int, string> documentsExistingByFolio, IEnumerable<string> existingdocsInFolder)
        {
            Parallel.ForEach(existingdocsInFolder, (docfile) =>
            {
                lock (_object_lock)
                {
                    var existingfolio = int.Parse(Path.GetFileNameWithoutExtension(docfile).Substring(2));
                    documentsExistingByFolio.Add(existingfolio, docfile);
                }
            });
        }

        private IEnumerable<string> GetExistingXmlDocumentsDownloaded(string folder)
        {
            return Directory.GetFiles($"{folder}", "*.xml");
        }

        private IEnumerable<int> GetExistingDocumentsInDatabase()
        {
            using (var ctx = new TaxDb())
            {
                return ctx.systblApp_CoreAPI_Document.Where(t => t.DocumentType == _doctype).Select(m => m.Folio).ToArray();
            }
        }

        private void SaveDocument(int folio, int folioFinal, ref int indexchunk, ref int acumchunk, EnvioBOLETA objBoleta, ref List<int> detailids, ref List<int> docids)
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

            if (indexchunk >= _chunkfolio || folio == folioFinal)
            {
                _db.SaveChanges();
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
                detailids = ReserveNewIds("systblApp_CoreAPI_DocumentDetail", _chunkfolio);
            }

            if (docids == null)
            {
                docids = ReserveNewIds("systblApp_CoreAPI_Document", _chunkfolio);
            }

            var dbBoleta = MapDocumentFromXmlObject(docids[indexchunk - 1], objBoleta, senderEntity, receiverEntity);

            AddDocumentDetailFromXmlObject(detailids[indexchunk - 1], objBoleta, dbBoleta);

            _db.systblApp_CoreAPI_Document.Add(dbBoleta);
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
            using (var db1 = new TaxDb())
            {
                var newreceiverEntity = MapReceiverEntity(objBoleta);
                db1.systblApp_CoreAPI_TaxEntity.Add(newreceiverEntity);
                db1.SaveChanges();
                return newreceiverEntity;
            }
        }

        private systblApp_CoreAPI_TaxEntity SaveSenderEntity(EnvioBOLETA objBoleta)
        {
            using (var db1 = new TaxDb())
            {
                var newsenderEntity = MapSenderEntity(objBoleta);
                AddAddressToSenderEntity(objBoleta, newsenderEntity);
                db1.systblApp_CoreAPI_TaxEntity.Add(newsenderEntity);
                db1.SaveChanges();
                return newsenderEntity;
            }
        }

        public int GetStoreId(string storename)
        {
            systblApp_TaxReceipt_Store entity;
            if (_StoreCache.TryGetValue(storename, out entity))
            {
                return entity.Id;
            }

            using (var db1 = new TaxDb())
            {
                entity = db1.systblApp_TaxReceipt_Store.Where(m => m.Name.Equals(storename)).FirstOrDefault();
                if (entity != null)
                {
                    _StoreCache.Add(storename, entity);
                    return entity.Id;
                }
                return 0;
            }

        }
        private bool TaxEntityExists(string rut, out int? id)
        {
            systblApp_CoreAPI_TaxEntity entity;
            if (_TaxEntityCache.TryGetValue(rut, out entity))
            {
                id = entity.Id;
                return true;
            }

            using (var db1 = new TaxDb())
            {
                entity = db1.systblApp_CoreAPI_TaxEntity.Where(m => m.RUT.Equals(rut)).FirstOrDefault();
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

        private string GetCustomFieldFromDocument(EnvioBOLETA objBoleta, string fieldName)
        {
            var result = objBoleta.
                 Personalizados.
                 DocPersonalizado.
                 campoString.Where(m => m.name.Equals(fieldName)).FirstOrDefault();
            if (result == null) return string.Empty;
            return result.Value;
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

        private List<int> ReserveNewIds(string entityName, int quantity)
        {
            var ids = new List<int>();
            var rangeid = GetNewId(entityName, quantity);
            ids = Enumerable.Range(rangeid.Item1, rangeid.Item2 - rangeid.Item1 + 1).ToList();
            return ids;
        }

        private int GetNewId(string entityName)
        {
            return GetNewId(entityName, 1).Item2;
        }

        private Tuple<int, int> GetNewId(string entityName, int quantity)
        {
            using (var db1 = new TaxDb())
            {
                var entity = db1.systblApp_CoreApi_Sequence.Find(entityName);
                var start = entity.CurrentId == null ? entity.StartId : entity.CurrentId.Value + 1;
                var nextId = entity.CurrentId == null ? quantity : entity.CurrentId.Value + quantity;

                entity.CurrentId = nextId;
                db1.Entry(entity).State = EntityState.Modified;
                db1.SaveChanges();

                return Tuple.Create(start.Value, nextId);
            }
        }
        private int GetNewDocumentId()
        {
            return GetNewId("systblApp_CoreAPI_Document");
        }

        private int GetNewDocumentDetailId()
        {
            return GetNewId("systblApp_CoreAPI_DocumentDetail");
        }

        private int GetNewTaxEntityId()
        {
            return GetNewId("systblApp_CoreAPI_TaxEntity");
        }

        private int GetNewTaxAddressId()
        {
            return GetNewId("systblApp_CoreAPI_TaxAddress");
        }
    }
}
