using CES.CoreApi.Receipt_Main.Application.Core;
using CES.CoreApi.Receipt_Main.Domain.Core.Documents;
using CES.CoreApi.Receipt_Main.Domain.Core.Security;
using CES.CoreApi.Receipt_Main.Domain.Core.Services;
using CES.CoreApi.Receipt_Main.Infrastructure.Core;
using CES.CoreApi.Receipt_Main.UI.WPF.Helpers;
using MvvmDialogs;
using MvvmDialogs.FrameworkDialogs.OpenFile;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Input;
using IOPath = System.IO.Path;

namespace CES.CoreApi.Receipt_Main.UI.WPF.ViewModel
{
    public class CafFormViewModel : ViewModelBase, IModalDialogViewModel
    {
        private string _folioCurrentNumber;
        private string _folioStartNumber;
        private string _folioEndNumber;
        private DocumentType _selectedDocumentTypeValue;
        private systblApp_TaxReceipt_Store _selectedStoreValue;
        private CafApiService _cafApiService = null;
        private readonly IDialogService _dialogService;
        private string path;
        private string _xmlContent;
        private int _id;
        private bool _disabled;
        private string _status;
        private bool? dialogResult;
        private actblTaxDocument_AuthCode cafObjectModel;
        private DateTime _authorizationDate;

        public CafFormViewModel(IStoreService storeService, IDialogService dialogService, actblTaxDocument_AuthCode cafObjectModel = null)
        {
            _cafApiService = new CafApiService();

            DocumentTypeList = DocumentTypeHelper.GetDocumenTypes();
            SelectedDocumentTypeValue = DocumentTypeList.FirstOrDefault();

            var stores = storeService.GetAllStores();
            StoreList = stores.ToList();
            StoreList.Insert(0, new systblApp_TaxReceipt_Store() { fStoreId = 0, fName = "--Seleccione una Tienda --" });
            SelectedStoreValue = StoreList.First();

            _dialogService = dialogService;

            OpenFileCommand = new RelayCommand(OpenFile);
            SaveCommand = new RelayCommand(Save);
            ClearCommand = new RelayCommand(Clear);
            Disabled = false;

            this.cafObjectModel = cafObjectModel;

            if (cafObjectModel != null)
            {
                ID = cafObjectModel.fAuthCodeID;
                FolioStartNumber = cafObjectModel.fStartNumber;
                FolioEndNumber = cafObjectModel.fEndNumber;
                FolioCurrentNumber = cafObjectModel.fCurrentNumber;
                XmlContent = cafObjectModel.fFileContent;
                Disabled = cafObjectModel.fDisabled;
                SelectedStoreValue = storeService.GetAllStores().Where(s => s.fStoreId == cafObjectModel.fRecAgentID).FirstOrDefault();
                SelectedDocumentTypeValue = DocumentTypeList.Where(m => m.Code == cafObjectModel.fDocumentTypeID.ToString()).FirstOrDefault();
                AuthorizationDate = cafObjectModel.fAuthorizationDate;
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
                FolioStartNumber = xmlobject.CAF.DA.RNG.D.ToString();
                FolioEndNumber = xmlobject.CAF.DA.RNG.H.ToString();
                SelectedDocumentTypeValue = DocumentTypeList.Where(m => m.Code == xmlobject.CAF.DA.TD.ToString()).FirstOrDefault();
                Disabled = false;
            }
        }
        private void Save(object obj)
        {
            var cafService = new CafApiService();

            var storeid = 0;
            var doctype = "39";
            var currentNumber = int.Parse(FolioCurrentNumber);
            var startNumber = int.Parse(FolioStartNumber);
            var endNumber = int.Parse(FolioEndNumber);

            if (currentNumber < startNumber || currentNumber > endNumber)
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
                storeid = SelectedStoreValue.fStoreId;
            }

            try
            {
                actblTaxDocument_AuthCode caf = null;

                if (ID <= 0)
                {
                    caf = cafService.CreateCaf(XmlContent, doctype, int.Parse(FolioStartNumber), int.Parse(FolioEndNumber),
                        int.Parse(FolioCurrentNumber), storeid);
                    if (caf != null)
                    {
                        this.ID = caf.fAuthCodeID;
                        this.AuthorizationDate = caf.fAuthorizationDate;
                        this.SelectedStoreValue = StoreList.Where(s => s.fStoreId == caf.fRecAgentID).FirstOrDefault();
                        this.SelectedDocumentTypeValue = DocumentTypeList.Where(m => m.Code == caf.fDocumentTypeID.ToString()).FirstOrDefault();
                    }
                }
                else
                {
                    caf = cafService.UpdateCaf(ID, XmlContent, doctype, int.Parse(FolioStartNumber), int.Parse(FolioEndNumber),
                        int.Parse(FolioCurrentNumber), storeid, Disabled);

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
            FolioStartNumber = "0";
            FolioEndNumber = "0";
            FolioCurrentNumber = "0";
            SelectedDocumentTypeValue = new DocumentType() { Code = "0", Description = "--Seleccione Tipo Documento --" };
            SelectedStoreValue = new systblApp_TaxReceipt_Store() { fStoreId = 0, fName = "--Seleccione Tienda --" };
            Disabled = false;
            XmlContent = string.Empty;
        }
        public DocumentType SelectedDocumentTypeValue
        {
            get { return _selectedDocumentTypeValue; }
            set
            {
                _selectedDocumentTypeValue = value;
                NotifyPropertyChanged();
            }
        }
        public IList<DocumentType> DocumentTypeList { get; }
        public IList<systblApp_TaxReceipt_Store> StoreList { get; }
        public string FolioCurrentNumber
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

        public string FolioStartNumber
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

        public string FolioEndNumber
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

        public DateTime AuthorizationDate
        {
            get { return _authorizationDate; }
            set
            {
                if (_authorizationDate == value) return;
                _authorizationDate = value;
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
