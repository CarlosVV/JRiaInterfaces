using CES.CoreApi.Receipt_Main.UI.WPF.Model;
using CES.CoreApi.Receipt_Main.UI.WPF.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.UI.WPF.ViewModel
{
    public class MainWindowViewModel
    {
        public MainWindowViewModel()
        {
            MenuElements = new[]
            {
                new MenuElement {Label = "Acciones"},
                new MenuElement {Label = "Buscar Documentos" },
                new MenuElement {Label = "Enviar a SII" },
                new MenuElement {Label = "Cargar Caf"},
                new MenuElement {Label = "Buscar Caf", Content = new CafManagement() },
                new MenuElement {Label = "Nuevo Caf", Content = new CafForm() },
                new MenuElement {Label = "Nuevo Documento", Content = new CafManagement() },
                new MenuElement {Label = "Generar Notas de Crédito", Content = new CafManagement() },
                new MenuElement {Label = "Descargar Documentos de SII", Content = new CafManagement() },
                new MenuElement {Label = "Conciliar SiiVAT", Content = new CafManagement() },
                new MenuElement {Label = "Seguridad"},
                new MenuElement {Label = "Usuarios y Roles"},
                new MenuElement {Label = "Nuevo Usuario", Content = new CafManagement() },
                new MenuElement {Label = "Buscar Usuario", Content = new CafManagement() },
                new MenuElement {Label = "Tiendas"},
                new MenuElement {Label = "Nueva Tienda", Content = new CafManagement() },
                new MenuElement {Label = "Buscar Tiendas", Content = new CafManagement() },
                new MenuElement {Label = "Configuraciones"},
                new MenuElement {Label = "Impresoras", Content = new CafManagement() },
                new MenuElement {Label = "Cajeros", Content = new CafManagement() },
                new MenuElement {Label = "Servicios", Content = new CafManagement() },
                new MenuElement {Label = "Parámetros", Content = new CafManagement() },
                new MenuElement {Label = "Verificación", Content = new CafManagement() },
                new MenuElement {Label = "Reportes"},
                new MenuElement {Label = "Boletas por hacer", Content = new CafManagement() },
                new MenuElement {Label = "Boletas por anular", Content = new CafManagement() },
            };
        }
        public MenuElement[] MenuElements { get; }
    }   
}
