using CES.CoreApi.Receipt_Main.Domain.Core.Security;
using CES.CoreApi.Receipt_Main.Infrastructure.Core;
using CES.CoreApi.Receipt_Main.Domain.Core.Documents;
using CES.CoreApi.Receipt_Main.Domain.Core.Security;
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

namespace CES.CoreApi.Receipt_Main.UI.WPF.ViewModel
{
    public class CafFormViewModel : ViewModelBase
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

        public CafFormViewModel(IStoreService storeService, IDialogService dialogService)
        {
            _cafApiService = new CafApiService();
            DocumentTypeList = DocumentTypeHelper.LoadDocumenTypes();
            SelectedDocumentTypeValue = DocumentTypeList.FirstOrDefault();
            var stores = storeService.GetAllStores();
            StoreList = stores.ToList();
            SelectedStoreValue = StoreList.First();
            _dialogService = dialogService;
            OpenFileCommand = new RelayCommand(OpenFile);
            SaveCommand = new RelayCommand(Save);
            ClearCommand = new RelayCommand(Clear);
            CancelCommand = new RelayCommand(Cancel);

        }
        public int ID
        {
            get { return _id; }
            set
            {
                _id = value;
                NotifiyPropertyChanged();
            }
        }
        public string Path
        {
            get { return path; }
            set
            {

                path = value;
                NotifiyPropertyChanged();
            }
        }
        public string Status
        {
            get { return _status; }
            set
            {
                _status = value;
                NotifiyPropertyChanged();
            }
        }
        public string XmlContent
        {
            get { return _xmlContent; }
            set
            {
                _xmlContent = value;
                NotifiyPropertyChanged();
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
                NotifiyPropertyChanged();
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
                _selectedDocumentTypeValue = DocumentTypeList.Where(m => m.Code == xmlobject.CAF.DA.TD.ToString()).FirstOrDefault();
            }
        }
        private void Save(object obj)
        {
            var cafService = new CafApiService();
            try
            {
                var result = cafService.CreateCaf(XmlContent).ConfigureAwait(false);
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
        }
        private void Cancel(object obj)
        {
        }
        public Document_Type SelectedDocumentTypeValue
        {
            get { return _selectedDocumentTypeValue; }
            set
            {
                _selectedDocumentTypeValue = value;
                NotifiyPropertyChanged();
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
                NotifiyPropertyChanged();
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
                NotifiyPropertyChanged();
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
                NotifiyPropertyChanged();
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
                NotifiyPropertyChanged();
            }
        }
    }
}
