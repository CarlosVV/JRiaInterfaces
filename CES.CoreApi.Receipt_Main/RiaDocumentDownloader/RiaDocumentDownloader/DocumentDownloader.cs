using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WpfLocalDb.Repository;
using WpfLocalDb.Utilities;
using WpfLocalDb.Xml.Boleta;

namespace RiaDocumentDownloader
{
    public class DocumentDownloader
    {
        private int _foliostart = 483148;
        private int _folioend = 2000000;
        private string _doctype = "39";//Boletas
        private readonly string _xml_in = "C:\\Users\\cvalderrama\\xml_in\\";
        private readonly string _xml_out = "C:\\Users\\cvalderrama\\xml_out\\";
        private readonly string _xml_not = "C:\\Users\\cvalderrama\\xml_not\\";
        private string _rut = "76134934-1";
        private object _obj_lock = new object();
        private readonly log4net.ILog _log;

        public DocumentDownloader(int foliostart, int folioend, string doctype, string xml_in, string xml_out, string rut, log4net.ILog log)
        {
            _foliostart = foliostart;
            _folioend = folioend;
            _doctype = doctype;
            _xml_in = xml_in;
            _xml_out = xml_out;
            _rut = rut;
            _log = log;
        }

        public void DescargarDocumentosFromGDE()
        {
            var parserBoletas = new XmlDocumentParser<EnvioBOLETA>();
            IEnumerable<int> existingdocsInDb = null;
            int[] initialFolioListToDownload = null;
            string[] xmlDocumentListDownloaded;
            string[] xmlDocumentListInProcess;
            string[] xmlDocumentListNotExisting;
            SortedList<int, int> existingFolioDownloaded;
            SortedList<int, int> existingFolioInProcess;
            SortedList<int, int> notFoundFoliosInService;
            int[] foliosToDownloadFromService;
            try
            {
                initialFolioListToDownload = Enumerable.Range(_foliostart, _folioend - _foliostart + 1).ToArray();
                xmlDocumentListDownloaded = GetXmlDocsDownloaded(_xml_in);
                xmlDocumentListInProcess = GetXmlDocsDownloaded(_xml_out);
                xmlDocumentListNotExisting = GetXmlDocsNotExistingInService();
                existingFolioDownloaded = new SortedList<int, int>();
                existingFolioInProcess = new SortedList<int, int>();
                notFoundFoliosInService = new SortedList<int, int>();

                Console.WriteLine($"Documentos iniciales a descargar: {initialFolioListToDownload.Length}");
                Console.WriteLine($"Buscando documentos descargados...");

                GetExistFolioListDownloaded(xmlDocumentListDownloaded, existingFolioDownloaded);
                GetExistFolioListDownloaded(xmlDocumentListInProcess, existingFolioInProcess);
                GetNotFoundFoliosInService(xmlDocumentListNotExisting, notFoundFoliosInService);

                existingdocsInDb = GetExistingDocumentsInDatabase();

                Console.WriteLine($"Documentos encontrados en carpeta: {existingFolioDownloaded.Count()}");
                Console.WriteLine($"Documentos encontrados en proceso: {existingFolioInProcess.Count()}");
                Console.WriteLine($"Documentos no encontrados en servicio: {notFoundFoliosInService.Count()}");
                Console.WriteLine($"Documentos en Base de Datos: {existingdocsInDb.Count()}");

                foliosToDownloadFromService = initialFolioListToDownload.Except(existingFolioDownloaded.Select(p => p.Key).ToArray()).ToArray();
                foliosToDownloadFromService = foliosToDownloadFromService.Except(existingFolioInProcess.Select(p => p.Key).ToArray()).ToArray();
                foliosToDownloadFromService = foliosToDownloadFromService.Except(notFoundFoliosInService.Select(p => p.Key).ToArray()).ToArray();
                foliosToDownloadFromService = foliosToDownloadFromService.Except(existingdocsInDb).ToArray();

                if (foliosToDownloadFromService.Length == 0)
                {
                    Console.WriteLine("No hay documentos para descargar con los parámetros suministrados");
                }
                else
                {
                    Console.WriteLine($"Documentos finales a descargar: {foliosToDownloadFromService.Length}");
                    Console.WriteLine($"Folio Inicio: {foliosToDownloadFromService.First()}");
                    Console.WriteLine($"Folio Final: {foliosToDownloadFromService.Last()}");
                    Console.WriteLine($"Descargando...");

                    var cancelationToken = new CancellationTokenSource();
                    var parallelOptions = new ParallelOptions();
                    parallelOptions.CancellationToken = cancelationToken.Token;
                    parallelOptions.MaxDegreeOfParallelism = System.Environment.ProcessorCount;

                    try
                    {
                        Parallel.ForEach(foliosToDownloadFromService, parallelOptions, (folio) =>
                        {
                            try
                            {
                                lock (_obj_lock)
                                {
                                    SaveXmlDocument(folio);
                                }

                                parallelOptions.CancellationToken.ThrowIfCancellationRequested();
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"[{folio}] {ex.Message} ");
                                cancelationToken.Cancel();
                            }
                        });
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    finally
                    {
                        cancelationToken.Dispose();
                    }

                    Console.WriteLine($"Descarga Finalizada: De {_foliostart} hasta {_folioend} ");
                }

                Console.WriteLine($"Documentos descargados: {foliosToDownloadFromService.Length}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Descarga con Error: {ex}");
            }
        }

        private void GetNotFoundFoliosInService(string[] xmlDocumentListNotExisting, SortedList<int, int> notFoundFoliosInService)
        {
            Parallel.ForEach(xmlDocumentListNotExisting, (doc) =>
            {
                lock (_obj_lock)
                {
                    var notfoundfolio = int.Parse(Path.GetFileNameWithoutExtension(doc).Substring(2));
                    notFoundFoliosInService.Add(notfoundfolio, notfoundfolio);
                }
            });
        }

        private void GetExistFolioListDownloaded(string[] xmlDocumentListDownloaded, SortedList<int, int> existingFolioDownloaded)
        {
            Parallel.ForEach(xmlDocumentListDownloaded, (doc) =>
            {
                lock (_obj_lock)
                {
                    var existingfolio = int.Parse(Path.GetFileNameWithoutExtension(doc).Substring(2));
                    existingFolioDownloaded.Add(existingfolio, existingfolio);
                }
            });
        }

        private void SaveXmlDocument(int folio)
        {
            string respuesta;
            var result = RetrieveXML(int.Parse(_doctype), folio, out respuesta);
            SaveXmlDocumentToRepository(folio, respuesta, result);
        }

        private void SaveXmlDocumentToRepository(int folio, string respuesta, bool result)
        {
            var name = result ? $"{_xml_in}BO{folio}.XML" : $"{_xml_not}BO{folio}.NOT";
            if (!File.Exists(name))
            {
                using (var writer = new StreamWriter(name, false))
                {
                    writer.Write(respuesta);
                    writer.Close();
                }
            }            
        }

        private string[] GetXmlDocsNotExistingInService()
        {
            return Directory.GetFiles($"{_xml_not}", "*.not");
        }

        private string[] GetXmlDocsDownloaded(string folder)
        {
            return Directory.GetFiles($"{folder}", "*.xml");
        }

        private bool RetrieveXML(int tipo, int folio, out string respuesta)
        {
            #region Recuperar XML

            #region Llamada al servicio
            var ambiente = DTEBoxCliente.Ambiente.Produccion;
            var tipoDocumento = (DTEBoxCliente.TipoDocumento)tipo;//DTEBoxCliente.TipoDocumento.TIPO_39;
            var apiURL = ConfigurationManager.AppSettings["ApiURL"]; //"http://<DTEBox IP>/api/Core.svc/Core";
            var apiAuth = ConfigurationManager.AppSettings["apiAuth"];//"<Llave de autenticación>";            
            var service = new DTEBoxCliente.Servicio(apiURL, apiAuth);
            var resultado = service.RecuperarXml(ambiente, _rut, tipoDocumento, folio);
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
                string notfoundmsg = "No se econtró el documento";//No se econtró el documento 2000000 del tipo 39 para el suscriptor 76134934-1
                respuesta = description;
                if (!respuesta.Contains(notfoundmsg))
                {
                    //Console.WriteLine($"Error: {respuesta}");
                    throw new Exception($"GDE Error: {respuesta}");
                }
                //Console.WriteLine($"Error: {respuesta}");
                return false;
            }

            #endregion

            #endregion
        }

        private IEnumerable<int> GetExistingDocumentsInDatabase()
        {
            using (var db = new TaxDb())
            {
                db.Configuration.AutoDetectChangesEnabled = false;
                db.Configuration.LazyLoadingEnabled = true;
                db.Configuration.ProxyCreationEnabled = false;
                db.Configuration.ValidateOnSaveEnabled = false;

                return db.systblApp_CoreAPI_Document.Where(t => t.DocumentType == _doctype).Select(m => m.Folio).ToArray();
            }
        }
    }
}
