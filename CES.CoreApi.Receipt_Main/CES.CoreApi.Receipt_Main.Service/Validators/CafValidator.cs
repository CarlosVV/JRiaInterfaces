using CES.CoreApi.Receipt_Main.Service.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;
using System.Xml.Schema;

namespace CES.CoreApi.Receipt_Main.Service.Validators
{
    public class CafValidator
    {
        public string Error { get; set; }
        public bool Validate(string xml)
        {
            var settings = new XmlReaderSettings();
            settings.ValidationType = ValidationType.Schema;
            settings.ValidationFlags |= XmlSchemaValidationFlags.ProcessInlineSchema;
            settings.ValidationFlags |= XmlSchemaValidationFlags.ProcessSchemaLocation;
            settings.ValidationFlags |= XmlSchemaValidationFlags.ReportValidationWarnings;
            settings.ValidationEventHandler += new ValidationEventHandler(ValidationCallBack);

            var streamXSD = XmlHelper.GenerateStreamFromString(GetXmlSchema());
            settings.Schemas.Add(null, XmlReader.Create(streamXSD));

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
        
        private string GetXmlSchema()
        {
            return
                @"<?xml version=""1.0"" encoding=""utf-8""?>
                    <xs:schema attributeFormDefault=""unqualified"" elementFormDefault=""qualified"" xmlns:xs=""http://www.w3.org/2001/XMLSchema"">
                      <xs:element name=""AUTORIZACION"">
                        <xs:complexType>
                          <xs:sequence>
                            <xs:element name=""CAF"">
                              <xs:complexType>
                                <xs:sequence>
                                  <xs:element name=""DA"">
                                    <xs:complexType>
                                      <xs:sequence>
                                        <xs:element name=""RE"" type=""xs:string"" />
                                        <xs:element name=""RS"" type=""xs:string"" />
                                        <xs:element name=""TD"" type=""xs:unsignedByte"" />
                                        <xs:element name=""RNG"">
                                          <xs:complexType>
                                            <xs:sequence>
                                              <xs:element name=""D"" type=""xs:unsignedByte"" />
                                              <xs:element name=""H"" type=""xs:unsignedByte"" />
                                            </xs:sequence>
                                          </xs:complexType>
                                        </xs:element>
                                        <xs:element name=""FA"" type=""xs:date"" />
                                        <xs:element name=""RSAPK"">
                                          <xs:complexType>
                                            <xs:sequence>
                                              <xs:element name=""M"" type=""xs:string"" />
                                              <xs:element name=""E"" type=""xs:string"" />
                                            </xs:sequence>
                                          </xs:complexType>
                                        </xs:element>
                                        <xs:element name=""IDK"" type=""xs:unsignedShort"" />
                                      </xs:sequence>
                                    </xs:complexType>
                                  </xs:element>
                                  <xs:element name=""FRMA"">
                                    <xs:complexType>
                                      <xs:simpleContent>
                                        <xs:extension base=""xs:string"">
                                          <xs:attribute name=""algoritmo"" type=""xs:string"" use=""required"" />
                                        </xs:extension>
                                      </xs:simpleContent>
                                    </xs:complexType>
                                  </xs:element>
                                </xs:sequence>
                                <xs:attribute name=""version"" type=""xs:decimal"" use=""required"" />
                              </xs:complexType>
                            </xs:element>
                            <xs:element name=""RSASK"" type=""xs:string"" />
                            <xs:element name=""RSAPUBK"" type=""xs:string"" />
                          </xs:sequence>
                        </xs:complexType>
                      </xs:element>
                    </xs:schema>";
        }
    }
}