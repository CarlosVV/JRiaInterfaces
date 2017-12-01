﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CES.CoreApi.Receipt_Main.Infrastructure.Core
{
    public class DocumentDownloader
    {
        private string _rut = "76134934-1";
        public bool RetrieveXML(int tipo, int folio, out string respuesta)
        {
            #region Recuperar XML

            #region Llamada al servicio
            var ambiente = DTEBoxCliente.Ambiente.Produccion;
            var tipoDocumento = (DTEBoxCliente.TipoDocumento)tipo;//DTEBoxCliente.TipoDocumento.TIPO_39;
            var apiURL = ConfigurationManager.AppSettings["ApiURL"]; //"http://<DTEBox IP>/api/Core.svc/Core";
            var apiAuth = ConfigurationManager.AppSettings["apiAuth"];//"<Llave de autenticación>";            
            var service = new DTEBoxCliente.Servicio(apiURL, apiAuth);
            var resultado = service.RecuperarXml(ambiente,DTEBoxCliente.GrupoBusqueda.Emitidos, _rut, tipo, folio, false);
            #endregion

            #region Procesamiento de la respuesta
            //Si la respuesta es correcta
            if (resultado.ResultadoServicio.Estado == DTEBoxCliente.EstadoResultado.Ok)
            {
                //Usar los datos que viene en el resultado de la llamada
                var xml = resultado.Datos;

                // Validar si Documento viene con Signature Adicional
                var xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(xml);
                var signatureNodes = xmlDoc.GetElementsByTagName("Signature");
                if(signatureNodes.Count == 2)
                {
                    signatureNodes[1].ParentNode.RemoveChild(signatureNodes[1]);
                    xml = xmlDoc.OuterXml;
                }               

                respuesta = xml;
                return true;
            }
            else //Si la llamada no fue satisfactoria
            {
                //Descripción del error, actuar acorde
                string description = resultado.ResultadoServicio.Descripcion;
                string notfoundmsg = "No se econtró el documento";
                respuesta = description;
                if (!respuesta.Contains(notfoundmsg))
                {
                    throw new Exception($"GDE Error: {respuesta}");
                }

                return false;
            }

            #endregion

            #endregion
        }
    }
}
