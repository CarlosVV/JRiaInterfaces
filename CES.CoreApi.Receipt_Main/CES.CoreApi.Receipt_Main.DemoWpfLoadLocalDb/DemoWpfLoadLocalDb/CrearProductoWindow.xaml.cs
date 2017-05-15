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
    public partial class CrearProductoWindow : Window
    {
        private MyModel db = new MyModel();
        public CrearProductoWindow()
        {
            InitializeComponent();

            LoadProducts();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var id = int.Parse(IdTextBox.Text);

            var product = db.Products.Where(m => m.Id.Equals(id)).FirstOrDefault();
            if (product != null)
            {
                product = GetProduct(product);
                db.Entry(product).State = EntityState.Modified;
            }
            else
            {
                product = GetProduct();
                db.Products.Add(product);
            }

            db.SaveChanges();
            MessageBox.Show("Grabado en el Sistema");
            LoadProducts();
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            var id = int.Parse(IdTextBox.Text);
            var results = db.Products.Where(m => m.Id.Equals(id));

            if (results != null)
            {
                var producto = results.FirstOrDefault();
                IdTextBox.Text = producto.Id.ToString();
                NameTextBox.Text = producto.Name;
                DescriptionTextBox.Text = producto.Description;
                PriceTextBox.Text = producto.Price.ToString();
            }
            else
            {
                MessageBox.Show("No encontrado");
            }

            //RecuperarXML();
        }
        public void LoadProducts()
        {
            ProductList.ItemsSource = db.Products.ToList();
        }
        private Product GetProduct(Product product)
        {
            product.Id = int.Parse(IdTextBox.Text);
            product.Name = NameTextBox.Text;
            product.Description = DescriptionTextBox.Text;
            product.Price = decimal.Parse(PriceTextBox.Text);
            return product;
        }

        private Product GetProduct()
        {
            Product product;
            product = new Product
            {
                Id = int.Parse(IdTextBox.Text),
                Name = NameTextBox.Text,//"Product A",
                Description = DescriptionTextBox.Text, //"Product A Description"//,
                Price = decimal.Parse(PriceTextBox.Text)
            };
            return product;
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

      
    }
}
