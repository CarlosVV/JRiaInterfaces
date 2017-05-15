using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Windows;
using WpfLocalDb.Repository;
using WpfLocalDb.Utilities;
using WpfLocalDb.Xml.Boleta;

namespace WpfLocalDb
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class BuscarFolioWindow : Window
    {
        private MyModel db = new MyModel();
        public BuscarFolioWindow()
        {
            InitializeComponent();
        }
     
        private bool RecuperarXML(int tipo, int folio, out string respuesta)
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

        private void BuscarFolio_Click(object sender, RoutedEventArgs e)
        {
            var tipo = int.Parse(Tipo.Text.Split('.')[0]);
            var respuesta = string.Empty;
            if (RecuperarXML(tipo, int.Parse(Folio.Text), out respuesta))
            {
                MessageBox.Show($"Respuesta obtenida exitosamente");
            }
            else
            {
                MessageBox.Show($"Error: {respuesta}");
            }

            XML.Text = respuesta;
        }

      
    }
}
