using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using WpfLocalDb.Utilities;

namespace WpfLocalDb.Xml.Validator
{
    public class XmlFormatValidator
    {
        public string Error { get; set; }
        private readonly string _dsigncore = "DSIG-CORE";
        public bool Validate(string xml, string documenttype)
        {
            var settings = new XmlReaderSettings();
            settings.DtdProcessing = DtdProcessing.Parse;
            settings.MaxCharactersFromEntities = 1024;
            settings.ValidationType = ValidationType.Schema;
            settings.ValidationFlags |= XmlSchemaValidationFlags.ProcessInlineSchema;
            settings.ValidationFlags |= XmlSchemaValidationFlags.ProcessSchemaLocation;
            settings.ValidationFlags |= XmlSchemaValidationFlags.ReportValidationWarnings;
            settings.ValidationEventHandler += new ValidationEventHandler(ValidationCallBack);

            var streamXSD2 = XmlHelper.GenerateStreamFromString(GetXmlSchema(_dsigncore));
            var streamXSD = XmlHelper.GenerateStreamFromString(GetXmlSchema(documenttype));

            settings.Schemas.Add(@"http://www.w3.org/2000/09/xmldsig#", XmlReader.Create(streamXSD2, settings));
            settings.Schemas.Add(null, XmlReader.Create(streamXSD, settings));
           

            var streamXML = XmlHelper.GenerateStreamFromString(xml);
            var reader = XmlReader.Create(streamXML, settings);

            while (reader.Read()) ;

            return true;
        }

        private void ValidationCallBack(object sender, ValidationEventArgs args)
        {
            if (args.Severity == XmlSeverityType.Warning)
            {
                Error = $"Warning: Matching schema not found.  No validation occurred. {args.Message}";
            }

            else
            {
                Error = $"Validation error: {args.Message}";
            }
        }

        private string  GetXmlSchema(string documenttype)
        {
            var xml = string.Empty;
            switch (documenttype)
            {
                case "BOLETA":
                    using(var reader = new StreamReader("Xml\\Boleta\\Boleta.xsd"))
                    {
                        xml = reader.ReadToEnd();
                    }
                    break;
                case "DSIG-CORE":
                    using (var reader = new StreamReader("Xml\\Boleta\\xmldsig-core-schema.xsd"))
                    {
                        xml = reader.ReadToEnd();
                    }
                    break;
                default:
                    break;
            }
            return xml;
        }
    }
}
