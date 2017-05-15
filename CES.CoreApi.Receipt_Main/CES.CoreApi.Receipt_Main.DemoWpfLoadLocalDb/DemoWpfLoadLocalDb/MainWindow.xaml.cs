using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Threading;
using System.Windows;
using WpfLocalDb.Repository;
using WpfLocalDb.Utilities;
using WpfLocalDb.Xml.Boleta;
using WpfLocalDb.Xml.Validator;

namespace WpfLocalDb
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private TaxDb db = new TaxDb();
        private static readonly decimal TaxRate = 0.19m;
        private static Dictionary<string, systblApp_CoreAPI_TaxEntity> TaxEntityCache;
        private static Dictionary<string, systblApp_TaxReceipt_Store> StoreCache;
        public MainWindow()
        {
            InitializeComponent();
            TaxEntityCache = new Dictionary<string, systblApp_CoreAPI_TaxEntity>();
            StoreCache = new Dictionary<string, systblApp_TaxReceipt_Store>();
        }

        private bool RetrieveXML(int tipo, int folio, out string respuesta)
        {
            //
            // ﻿Recuperar el XML de un documento
            //
            #region Recuperar XML
            #region Dependencias
            // - DTEBOXClienteNET20.dll
            #endregion
            #region Llamada al servicio
            DTEBoxCliente.Ambiente ambiente = DTEBoxCliente.Ambiente.Produccion;
            string rut = "76134934-1";
            DTEBoxCliente.TipoDocumento tipoDocumento = (DTEBoxCliente.TipoDocumento)tipo;//DTEBoxCliente.TipoDocumento.TIPO_39;
            //GrupoBusqueda grupo = GrupoBusqueda.Emitidos;
            //long folio = 768983;
            //bool esParaDistribucion = true;
            //key TESTING "6f8bbbf2-d48d-4750-8333-611be4569738";
            //key PRODUCCCION "6f8bbbf2-d48d-4750-8333-611be4569738";
            string apiURL = ConfigurationManager.AppSettings["ApiURL"]; //"http://<DTEBox IP>/api/Core.svc/Core";
            string apiAuth = "6f8bbbf2-d48d-4750-8333-611be4569738";//"<Llave de autenticación>";            
            DTEBoxCliente.Servicio service = new DTEBoxCliente.Servicio(apiURL, apiAuth);
            /*DTEBoxCliente.ResultadoRecuperarXml resultado = service.RecuperarXml(
               ambiente,
               grupo,
               rut,
               (int)tipoDocumento,
               folio,
               esParaDistribucion);*/
            DTEBoxCliente.ResultadoRecuperarXml resultado = service.RecuperarXml(
                ambiente,
                rut,
                tipoDocumento,
                folio);
            #endregion
            #region Procesamiento de la respuesta
            //Si la respuesta es correcta
            if (resultado.ResultadoServicio.Estado == DTEBoxCliente.EstadoResultado.Ok)
            {
                //Usar los datos que viene en el resultado de la llamada
                string xml = resultado.Datos;
                //Código de usuario a partir de aquí
                respuesta = xml;
                return true;
            }
            else //Si la llamada no fue satisfactoria
            {
                //Descripción del error, actuar acorde
                string description = resultado.ResultadoServicio.Descripcion;
                respuesta = description;
                return false;
            }
            #endregion
            #endregion
        }

        private void DescargarDocumentos_Click(object sender, RoutedEventArgs e)
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.DoWork += worker_DoWork;
            worker.ProgressChanged += worker_ProgressChanged;

            worker.RunWorkerAsync();
        }

        private void DescargarDocumentosFromGDE(BackgroundWorker sender)
        {
            var folioInicio = 483148;
            var folioFinal = 1559333;
            var chunkfolio = 100;
            var tipo_documento = 39;//Boletas
            var parserBoletas = new XmlDocumentParser<EnvioBOLETA>();
            var folio = folioInicio;
            var respuesta = string.Empty;
            var totalOfDocuments = folioFinal - folioInicio + 1;
            var numberofchk = totalOfDocuments / chunkfolio;
            var restchunk = totalOfDocuments % chunkfolio;
            var indexchunk = 1;
            var acumchunk = 0;
            var iProgress = 0;
            var validator = new XmlFormatValidator();

            try
            {
                var documentsToInsert = new List<EnvioBOLETA>();
                var documentsErrorsRetrieve = new Dictionary<int, String>();
                var documentsErrorsParse = new Dictionary<int, Tuple<string, string>>();

                for (folio = folioInicio; folio <= folioFinal; folio++)
                {
                    respuesta = string.Empty;

                    if (!RetrieveXML(tipo_documento, folio, out respuesta))
                    {
                        documentsErrorsRetrieve.Add(folio, respuesta);
                        continue;
                    }

                    if (!validator.Validate(respuesta, "BOLETA"))
                    {

                        documentsErrorsParse.Add(folio, Tuple.Create(validator.Error, respuesta));
                        continue;
                    }

                    var objBoleta = parserBoletas.GetDocumentObjectFromString(respuesta);
                    documentsToInsert.Add(objBoleta);

                    //iProgress = (int)Math.Ceiling((decimal)acumchunk / totalOfDocuments);
                    //sender.ReportProgress(iProgress);
                }

                iProgress = 0;
                List<int> detailids = null;
                List<int> docids = null;
                foreach (var objBoleta in documentsToInsert)
                {
                    //var objBoleta = parserBoletas.GetDocumentObjectFromString(respuesta);
                    var rutSender = objBoleta.SetDTE.DTE.Documento.Encabezado.Emisor.RUTEmisor;
                    var rutReceiver = objBoleta.SetDTE.DTE.Documento.Encabezado.Receptor.RUTRecep;
                    int? idSender = null;
                    int? idReceiver = null;
                    var senderEntity = new systblApp_CoreAPI_TaxEntity();
                    var receiverEntity = new systblApp_CoreAPI_TaxEntity();

                    //Get o Set Entity and Address
                    if (!TaxEntityExists(rutSender, out idSender))
                    {
                        systblApp_CoreAPI_TaxEntity newsenderEntity = SaveSenderEntity(objBoleta);
                        //senderEntity = db.systblApp_CoreAPI_TaxEntity.Find(newsenderEntity.Id);
                        senderEntity.Id = newsenderEntity.Id;
                    }
                    else
                    {
                        //senderEntity = db.systblApp_CoreAPI_TaxEntity.Find(idSender.Value);
                        senderEntity.Id = idSender.Value;
                    }

                    if (!TaxEntityExists(rutReceiver, out idReceiver))
                    {
                        systblApp_CoreAPI_TaxEntity newreceiverEntity = SaveReceiverEntity(objBoleta);
                        //receiverEntity = db.systblApp_CoreAPI_TaxEntity.Find(newreceiverEntity.Id);
                        receiverEntity.Id = newreceiverEntity.Id;
                    }
                    else
                    {
                        //receiverEntity = db.systblApp_CoreAPI_TaxEntity.Find(idReceiver.Value);
                        receiverEntity.Id = idReceiver.Value;
                    }

                    //Save Document and Detail

                    if(detailids == null)
                    {
                        detailids = ReserveNewIds("systblApp_CoreAPI_DocumentDetail", chunkfolio);
                    }

                    if (docids == null)
                    {
                        docids = ReserveNewIds("systblApp_CoreAPI_Document", chunkfolio);
                    }
                   

                    var dbBoleta = MapDocumentFromXmlObject(docids[indexchunk], objBoleta, senderEntity, receiverEntity);

                    AddDocumentDetailFromXmlObject(detailids[indexchunk], objBoleta, dbBoleta);

                    db.systblApp_CoreAPI_Document.Add(dbBoleta);

                    if (indexchunk >= chunkfolio || folio == folioFinal)
                    {
                        db.SaveChanges();
                        acumchunk = acumchunk + indexchunk;
                        indexchunk = 0;
                        docids = null;
                        detailids = null;
                    }

                    indexchunk++;
                    iProgress = (int)Math.Ceiling((decimal)acumchunk / totalOfDocuments);
                    sender.ReportProgress(iProgress);
                }
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

                MessageBox.Show($"Error en Folio {folio}: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en Folio {folio}: {ex}");
                MessageBox.Show($"Error en Folio {folio}: {ex.Message}");
            }

            MessageBox.Show($"Descargado todos los de {folioInicio} a {folioFinal}");
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
            if (StoreCache.TryGetValue(storename, out entity))
            {
                return entity.Id;
            }

            using (var db1 = new TaxDb())
            {
                entity = db1.systblApp_TaxReceipt_Store.Where(m => m.Name.Equals(storename)).FirstOrDefault();
                if (entity != null)
                {
                    StoreCache.Add(storename, entity);
                    return entity.Id;
                }
                return 0;
            }

        }
        private bool TaxEntityExists(string rut, out int? id)
        {
            systblApp_CoreAPI_TaxEntity entity;
            if (TaxEntityCache.TryGetValue(rut, out entity))
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
                    TaxEntityCache.Add(rut, entity);
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
            var amount = Math.Floor(objBoleta.SetDTE.DTE.Documento.Encabezado.Totales.MntTotal / TaxRate);
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
                //Receiver = receiverEntity,
                ReceiverId = receiverEntity.Id,
                //Sender = senderEntity,
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
            var i = rangeid.Item1;
            ids.ForEach(m => m = ++i);
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
                var nextId = entity.CurrentId == null ? entity.StartId + quantity - 1 : entity.CurrentId.Value + quantity - 1;

                entity.CurrentId = nextId;
                db1.Entry(entity).State = EntityState.Modified;
                db1.SaveChanges();

                return Tuple.Create(start.Value, nextId.Value);
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

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            DescargarDocumentosFromGDE(sender as BackgroundWorker);
        }

        private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            pbStatus.Value = e.ProgressPercentage;
        }
    }
}
