using CES.CoreApi.Receipt_Main.Domain.Core.Services;
using CES.CoreApi.Receipt_Main.UI.WPF.Model;
using CES.CoreApi.Receipt_Main.UI.WPF.View;
using MvvmDialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.UI.WPF.ViewModel
{
    public class MainWindowViewModel
    {
        public MainWindowViewModel(IDocumentService documentService, ITaxEntityService taxEntityService, ITaxAddressService taxAddressService, ISequenceService sequenceService, IStoreService storeService, IDialogService dialogService)
        {
            MenuElements = new[]
            {
                new MenuElement {Label = "Acciones", MenuStyleType = "Header1", Content = new DownloadSiiDocuments(documentService, taxEntityService, taxAddressService, sequenceService, storeService)},
                new MenuElement {Label = "Buscar Documentos", Content = new SearchDocuments(), MenuStyleType = "Item1"},
                new MenuElement {Label = "Enviar a SII", Content = new SendToEIS(), MenuStyleType = "Item1"},
                new MenuElement {Label = "Cargar Caf", Content = new CafManagement(), MenuStyleType = "Header2"},
                new MenuElement {Label = "Buscar Caf", Content = new CafManagement(), MenuStyleType = "Item2" },
                new MenuElement {Label = "Nuevo Caf", Content = new CafForm(storeService, dialogService), MenuStyleType = "Item2" },
                new MenuElement {Label = "Nuevo Documento", Content = new TaxDocumentForm(), MenuStyleType = "Item1"},
                new MenuElement {Label = "Generar Notas de Crédito", Content = new GenerateCreditNotes(), MenuStyleType = "Item1"},
                new MenuElement {Label = "Descargar Documentos de SII", Content = new DownloadSiiDocuments(documentService, taxEntityService, taxAddressService, sequenceService, storeService), MenuStyleType = "Item1"},
                new MenuElement {Label = "Conciliar SiiVAT", Content = new SiiVatConciliation(), MenuStyleType = "Item1"},
                new MenuElement {Label = "Seguridad", MenuStyleType = "Header1"},
                new MenuElement {Label = "Usuarios y Roles", MenuStyleType = "Header2"},
                new MenuElement {Label = "Nuevo Usuario", Content = new UserForm(), MenuStyleType = "Item2"},
                new MenuElement {Label = "Buscar Usuario", Content = new UserManagement(), MenuStyleType = "Item2"},
                new MenuElement {Label = "Asignar Funciones", Content = new AssignFunctionsToRoles(), MenuStyleType = "Item2"},
                new MenuElement {Label = "Tiendas", MenuStyleType = "Header2"},
                new MenuElement {Label = "Nueva Tienda", Content = new StoreForm(), MenuStyleType = "Item2"},
                new MenuElement {Label = "Buscar Tiendas", Content = new StoreManagement(), MenuStyleType = "Item2"},
                new MenuElement {Label = "Configuraciones", MenuStyleType = "Header1"},
                new MenuElement {Label = "Impresoras", Content = new Printers(), MenuStyleType = "Item1"},
                new MenuElement {Label = "Cajeros", Content = new Cashiers(), MenuStyleType = "Item1"},
                new MenuElement {Label = "Servicios", Content = new Services(), MenuStyleType = "Item1" },
                new MenuElement {Label = "Parámetros", Content = new Parameters(), MenuStyleType = "Item1"},
                new MenuElement {Label = "Verificación", Content = new CheckList(), MenuStyleType = "Item1"},
                new MenuElement {Label = "Reportes", MenuStyleType = "Header1"},
                new MenuElement {Label = "Boletas por hacer", Content = new ToDoDocumentsReport(), MenuStyleType = "Item1"},
                new MenuElement {Label = "Boletas por anular", Content = new UnDoDocumentsReport(), MenuStyleType = "Item1"},
            };
        }
        public MenuElement[] MenuElements { get; }
    }   
}
