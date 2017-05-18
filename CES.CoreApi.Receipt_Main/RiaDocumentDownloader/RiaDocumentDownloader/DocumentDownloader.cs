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
        private int _folioInicio = 483148;
        private int _folioFinal = 2000000;
        private string _tipo_documento = "39";//Boletas
        private readonly string _folder = "C:\\Users\\cvalderrama\\OneDrive for Business\\XML\\";
        private readonly string _xml_out = "C:\\Users\\cvalderrama\\xml_out\\";
        private string _rut = "76134934-1";
        private object _obj_lock = new object();
        private readonly log4net.ILog _log;

        public DocumentDownloader(int folioInicio, int folioFinal, string tipo_documento, string folder, string xml_out, string rut, log4net.ILog log)
        {
            _folioInicio = folioInicio;
            _folioFinal = folioFinal;
            _tipo_documento = tipo_documento;
            _folder = folder;
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
                initialFolioListToDownload = Enumerable.Range(_folioInicio, _folioFinal - _folioInicio + 1).ToArray();
                xmlDocumentListDownloaded = GetXmlDocsDownloaded(_folder);
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

                    Console.WriteLine($"Descarga Finalizada: De {_folioInicio} hasta {_folioFinal} ");
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
            var result = RetrieveXML(int.Parse(_tipo_documento), folio, out respuesta);
            SaveXmlDocumentToRepository(folio, respuesta, result);
        }

        private void SaveXmlDocumentToRepository(int folio, string respuesta, bool result)
        {
            var extname = result ? "XML" : "NOT";
            using (var writer = new StreamWriter($"{_folder}BO{folio}.{extname}", false))
            {
                writer.Write(respuesta);
                writer.Close();
            }
        }

        private string[] GetXmlDocsNotExistingInService()
        {
            return Directory.GetFiles($"{_folder}", "*.not");
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

                return db.systblApp_CoreAPI_Document.Where(t => t.DocumentType == _tipo_documento).Select(m => m.Folio).ToArray();
            }
        }
    }
}
