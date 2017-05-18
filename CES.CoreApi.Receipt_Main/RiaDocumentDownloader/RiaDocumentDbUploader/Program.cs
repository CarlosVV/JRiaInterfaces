using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiaDocumentDbUploader
{
    public class Program
    {
        private static string doctype;
        public static string dbpath;
        public static string xml_in;
        public static string xml_out;
        private static int foliostart;
        private static int folioend;
        public static int filesToProcess = 30;
        public static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        static void Main(string[] args)
        {
            try
            {
                doctype = ConfigurationManager.AppSettings["doctype"] != null ? ConfigurationManager.AppSettings["doctype"] : doctype;
                foliostart = ConfigurationManager.AppSettings["foliostart"] != null ? int.Parse(ConfigurationManager.AppSettings["foliostart"]) : foliostart;
                folioend = ConfigurationManager.AppSettings["folioend"] != null ? int.Parse(ConfigurationManager.AppSettings["folioend"]) : folioend;
                dbpath = ConfigurationManager.AppSettings["dbpath"] != null ? ConfigurationManager.AppSettings["dbpath"] : AppDomain.CurrentDomain.BaseDirectory;
                AppDomain.CurrentDomain.SetData("DataDirectory", dbpath);
                xml_in = ConfigurationManager.AppSettings["xml_in"] != null ? ConfigurationManager.AppSettings["xml_in"] : xml_in;
                xml_out = ConfigurationManager.AppSettings["xml_out"] != null ? ConfigurationManager.AppSettings["xml_out"] : xml_out;
                filesToProcess = ConfigurationManager.AppSettings["filesToProcess"] != null ? int.Parse(ConfigurationManager.AppSettings["filesToProcess"]) : filesToProcess;

                doctype = (args.Length >= 1) ? args[0] : doctype;
                foliostart = (args.Length >= 2) ? int.Parse(args[1]) : foliostart;
                folioend = (args.Length >= 3) ? int.Parse(args[2]) : folioend;

                var uploader = new DocumentUploader(doctype, xml_in, xml_out, filesToProcess, foliostart, folioend);
                Console.WriteLine($"Inicio Carga a BD...");
                Console.WriteLine($"Folder Origen: {xml_in}");
                Console.WriteLine($"BD: {dbpath}");

                uploader.CargarDocumentosEnBd();
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
