using CES.CoreApi.Receipt_Main.Domain.Core.Security;
using CES.CoreApi.Receipt_Main.Infrastructure.Core;
using CES.CoreApi.Receipt_Main.Domain.Core.Documents;
using CES.CoreApi.Receipt_Main.Domain.Core.Services;
using CES.CoreApi.Receipt_Main.UI.WPF.Helpers;
using CES.CoreApi.Receipt_Main.UI.WPF.Model;
using MvvmDialogs;
using MvvmDialogs.FrameworkDialogs.OpenFile;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using IOPath = System.IO.Path;
using CES.CoreApi.Receipt_Main.Application.Core;
using System.Windows.Controls;
using CES.CoreApi.Receipt_Main.UI.WPF.View;

namespace CES.CoreApi.Receipt_Main.UI.WPF.ViewModel
{
    public class CafFormViewModel : ViewModelBase, IModalDialogViewModel
    {
        private int _folioCurrentNumber;
        private int _folioStartNumber;
        private int _folioEndNumber;
        private Document_Type _selectedDocumentTypeValue;
        private systblApp_TaxReceipt_Store _selectedStoreValue;
        private CafApiService _cafApiService = null;
        private readonly IDialogService _dialogService;
        private string path;
        private string _xmlContent;
        private int _id;
        private bool _disabled;
        private string _status;
        private bool? dialogResult;
        private systblApp_CoreAPI_Caf cafObjectModel;
        public CafFormViewModel(IStoreService storeService, IDialogService dialogService, systblApp_CoreAPI_Caf cafObjectModel = null)
        {
            _cafApiService = new CafApiService(); 

            DocumentTypeList = DocumentTypeHelper.GetDocumenTypes();
            SelectedDocumentTypeValue = DocumentTypeList.FirstOrDefault();

            var stores = storeService.GetAllStores();
            StoreList = stores.ToList();
            StoreList.Insert(0, new systblApp_TaxReceipt_Store() { Id = 0, Name = "--Seleccione una Tienda --" });
            SelectedStoreValue = StoreList.First();

            _dialogService = dialogService;

            OpenFileCommand = new RelayCommand(OpenFile);
            SaveCommand = new RelayCommand(Save);
            Disabled = false;

            this.cafObjectModel = cafObjectModel;

            if (cafObjectModel != null)
            {
                ID = cafObjectModel.Id;
                FolioStartNumber = cafObjectModel.FolioStartNumber;
                FolioEndNumber = cafObjectModel.FolioEndNumber;
                FolioCurrentNumber = cafObjectModel.FolioCurrentNumber;
                XmlContent = cafObjectModel.FileContent;
                Disabled = cafObjectModel.fDisabled.HasValue  ? cafObjectModel.fDisabled.Value : false;
                SelectedStoreValue = storeService.GetAllStores().Where(s => s.Id == cafObjectModel.RecAgent).FirstOrDefault();
                SelectedDocumentTypeValue = DocumentTypeList.Where(m=> m.Code == cafObjectModel.DocumentType).FirstOrDefault();
            }

        }
        public int ID
        {
            get { return _id; }
            set
            {
                _id = value;
                NotifyPropertyChanged();
            }
        }
        public string Path
        {
            get { return path; }
            set
            {

                path = value;
                NotifyPropertyChanged();
            }
        }
        public string Status
        {
            get { return _status; }
            set
            {
                _status = value;
                NotifyPropertyChanged();
            }
        }
        public string XmlContent
        {
            get { return _xmlContent; }
            set
            {
                _xmlContent = value;
                NotifyPropertyChanged();
            }
        }
        public ICommand OpenFileCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand ClearCommand { get; }
        public ICommand CancelCommand { get; }
        public systblApp_TaxReceipt_Store SelectedStoreValue
        {
            get { return _selectedStoreValue; }
            set
            {
                _selectedStoreValue = value;
                NotifyPropertyChanged();
            }
        }
        private void OpenFile(object obj)
        {
            var settings = new OpenFileDialogSettings
            {
                Title = "Buscar archivo XML ",
                InitialDirectory = IOPath.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                Filter = "XML Documents (*.xml)|*.xml|All Files (*.*)|*.*"
            };

            bool? success = _dialogService.ShowOpenFileDialog(this, settings);
            if (success == true)
            {
                Path = settings.FileName;
                using (var stream = new StreamReader(Path))
                {
                    XmlContent = stream.ReadToEnd();
                }
                var parser = new XmlDocumentParser<AUTORIZACION>();
                var xmlobject = parser.GetDocumentObjectFromString(_xmlContent);
                FolioStartNumber = xmlobject.CAF.DA.RNG.D;
                FolioEndNumber = xmlobject.CAF.DA.RNG.H;
                SelectedDocumentTypeValue = DocumentTypeList.Where(m => m.Code == xmlobject.CAF.DA.TD.ToString()).FirstOrDefault();
                Disabled = false;
            }
        }
        private void Save(object obj)
        {
            var cafService = new CafApiService();

            var storeid = 0;
            var doctype = "39";

            if (FolioCurrentNumber < FolioStartNumber || FolioCurrentNumber > FolioEndNumber)
            {
                Status = "Folio Actual debe ser mayor o igual al folio inicial y menor o igual al folio final";
                return;
            }

            if (SelectedDocumentTypeValue != null)
            {
                doctype = SelectedDocumentTypeValue.Code;
            }

            if (SelectedStoreValue != null)
            {
                storeid = SelectedStoreValue.Id;
            }           

            try
            {
                if(ID <= 0)
                {
                    var result = cafService.CreateCaf(XmlContent, doctype, FolioStartNumber, FolioEndNumber,  FolioCurrentNumber, storeid);
                }
                else
                {
                    var result = cafService.UpdateCaf(ID, XmlContent, doctype, FolioStartNumber, FolioEndNumber, FolioCurrentNumber, storeid, Disabled);
                }
              
                Status = "Grabación exitosa en Base de Datos";
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
                Status = $"Error: {ex.Message}";
            }

        }
        private void Clear(object obj)
        {
            FolioCurrentNumber = 0;
            FolioEndNumber = 0;
            FolioStartNumber = 0;
        }
       public Document_Type SelectedDocumentTypeValue
        {
            get { return _selectedDocumentTypeValue; }
            set
            {
                _selectedDocumentTypeValue = value;
                NotifyPropertyChanged();
            }
        }
        public IList<Document_Type> DocumentTypeList { get; }
        public IList<systblApp_TaxReceipt_Store> StoreList { get; }
        public int FolioCurrentNumber
        {
            get
            {
                return _folioCurrentNumber;
            }

            set
            {
                _folioCurrentNumber = value;
                NotifyPropertyChanged();
            }
        }

        public int FolioStartNumber
        {
            get
            {
                return _folioStartNumber;
            }

            set
            {
                _folioStartNumber = value;
                NotifyPropertyChanged();
            }
        }

        public int FolioEndNumber
        {
            get
            {
                return _folioEndNumber;
            }

            set
            {
                _folioEndNumber = value;
                NotifyPropertyChanged();
            }
        }
        public bool Disabled
        {
            get
            {
                return _disabled;
            }

            set
            {
                _disabled = value;
                NotifyPropertyChanged();
            }
        }
        public ICommand OkCommand { get; }
        public bool? DialogResult
        {
            get { return dialogResult; }
            private set
            {
                dialogResult = value;
                NotifyPropertyChanged();
            }
        }

        private void Ok(object obj)
        {
            DialogResult = true;
        }
    }
}
