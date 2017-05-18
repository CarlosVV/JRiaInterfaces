using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfLocalDb.Utilities;
using WpfLocalDb.Xml.Boleta;

namespace RiaDocumentDownloader
{
    public class Program
    {
        private static readonly string rut = "76134934-1";
        private static int foliostart;
        private static int folioend;
        private static string doctype;
        private static string xml_in;
        public static string xml_out;        
        private object obj_lock = new object(); 
        public static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        
        static void Main(string[] args)
        {
            try
            {
                doctype = ConfigurationManager.AppSettings["doctype"] != null ? ConfigurationManager.AppSettings["doctype"] : doctype;
                foliostart = ConfigurationManager.AppSettings["foliostart"] != null ? int.Parse(ConfigurationManager.AppSettings["foliostart"]) : foliostart;
                folioend = ConfigurationManager.AppSettings["folioend"] != null ? int.Parse(ConfigurationManager.AppSettings["folioend"]) : folioend;
                xml_in = ConfigurationManager.AppSettings["xml_in"] != null ? ConfigurationManager.AppSettings["xml_in"] : xml_in;
                xml_out = ConfigurationManager.AppSettings["xml_out"] != null ? ConfigurationManager.AppSettings["xml_out"] : xml_out;

                doctype = (args.Length >= 1) ? args[0] : doctype;
                foliostart = (args.Length >= 2) ? int.Parse(args[1]) : foliostart;
                folioend = (args.Length >= 3) ? int.Parse(args[2]) : folioend;

                Console.WriteLine($"Inicio Descarga...");
                Console.WriteLine($"Folio Inicio: {foliostart}");
                Console.WriteLine($"Folio Final: {folioend}");
                Console.WriteLine($"Tipo Doc: {doctype}");
                Console.WriteLine($"Folder: {xml_in}");

                var objDocumentDownloader = new DocumentDownloader(foliostart, folioend, doctype, xml_in, xml_out, rut, log);
                objDocumentDownloader.DescargarDocumentosFromGDE();
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            Console.WriteLine($"Press <ENTER> to exit");
            Console.ReadLine();
        }

       
    }
}
