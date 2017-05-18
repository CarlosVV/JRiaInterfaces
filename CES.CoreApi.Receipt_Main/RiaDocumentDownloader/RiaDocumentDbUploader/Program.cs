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
        public static string folder;
        public static string xml_out;
        public static int filesToProcess = 30;
        public static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        static void Main(string[] args)
        {
            try
            {
                doctype = ConfigurationManager.AppSettings["doctype"] != null ? ConfigurationManager.AppSettings["doctype"] : doctype;
                dbpath = ConfigurationManager.AppSettings["dbpath"] != null ? ConfigurationManager.AppSettings["dbpath"] : AppDomain.CurrentDomain.BaseDirectory;
                AppDomain.CurrentDomain.SetData("DataDirectory", dbpath);
                folder = ConfigurationManager.AppSettings["folder"] != null ? ConfigurationManager.AppSettings["folder"] : folder;
                xml_out = ConfigurationManager.AppSettings["xml_out"] != null ? ConfigurationManager.AppSettings["xml_out"] : xml_out;

                var uploader = new DocumentUploader(folder, filesToProcess, xml_out, doctype);
                Console.WriteLine($"Inicio Carga a BD...");
                Console.WriteLine($"Folder Origen: {folder}");
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
