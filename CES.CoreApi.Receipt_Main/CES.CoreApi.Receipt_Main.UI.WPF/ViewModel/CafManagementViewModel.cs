using CES.CoreApi.Receipt_Main.Application.Core;
using CES.CoreApi.Receipt_Main.Domain.Core.Documents;
using CES.CoreApi.Receipt_Main.Domain.Core.Security;
using CES.CoreApi.Receipt_Main.Domain.Core.Services;
using CES.CoreApi.Receipt_Main.UI.WPF.Helpers;
using CES.CoreApi.Receipt_Main.UI.WPF.Model;
using CES.CoreApi.Receipt_Main.UI.WPF.View;
using MaterialDesignThemes.Wpf;
using MvvmDialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace CES.CoreApi.Receipt_Main.UI.WPF.ViewModel
{
    public class CafManagementViewModel : ViewModelBase
    {
        private Document_Type _selectedDocumentTypeValue;
        private systblApp_TaxReceipt_Store _selectedStoreValue;
        private CafApiService _cafApiService = null;
        private readonly IDialogService dialogService;
        private readonly IStoreService storeService;
        private string _status;
        private int _folioCurrentNumber;
        private int _folioStartNumber;
        private int _folioEndNumber;
        private bool? _isAllCafResultsSelected;

        public CafManagementViewModel(Func<string, string, bool> msgbox, Func<string, string, bool> confirm, IStoreService storeService, IDialogService dialogService)
        {
            _cafApiService = new CafApiService();

            DocumentTypeList = DocumentTypeHelper.GetDocumenTypes();
            DocumentTypeList.Insert(0, new Document_Type() { Code = "0", Description = "--Seleccione una Tipo --" });
            SelectedDocumentTypeValue = DocumentTypeList.FirstOrDefault();

            this.storeService = storeService;

            var stores = storeService.GetAllStores();
            StoreList = stores.ToList();
            StoreList.Insert(0, new systblApp_TaxReceipt_Store() { Id = 0, Name = "--Seleccione una Tienda --" });
            SelectedStoreValue = StoreList.First();

            SearchCommand = new RelayCommand(Search);
            ClearCommand = new RelayCommand(Clear);
            DeleteCommand = new RelayCommand(Delete);

            CafResults = new ObservableCollection<CafResultSelectableViewModel>();

            SelectAllCheckboxColumnCommand = new RelayCommand(SelectAllCheckboxColumnCommandAction);

            this.dialogService = dialogService;

            ShowDialogCommand = new RelayCommand(ShowDialogAction);

            this.ViewSource = new CollectionViewSource();
            ViewSource.Source = CafResults;

        }
        private void ShowDialogAction(object obj)
        {
            var cafObject = new systblApp_CoreAPI_Caf();
            if (obj != null)
            {
                var cafGridItem = obj as CafResultSelectableViewModel;
                var recAgent = string.IsNullOrWhiteSpace(cafGridItem.Store) ? 0 : storeService.GetAllStores().Where(s => s.Name == cafGridItem.Store).FirstOrDefault().Id;
                var docType = string.IsNullOrWhiteSpace(cafGridItem.Type) ? string.Empty : DocumentTypeList.Where(s => s.Description == cafGridItem.Type).FirstOrDefault().Code;

                cafObject = _cafApiService.SearchCafs(int.Parse(cafGridItem.Id), docType, int.Parse(cafGridItem.Start), int.Parse(cafGridItem.End), int.Parse(cafGridItem.Current), recAgent).FirstOrDefault();
            }

            var dialog = new CafForm(storeService, dialogService, cafObject);

            DialogHost.Show(dialog, DialogCloseEvenHandler);
        }

        public void DialogCloseEvenHandler(object sender, DialogClosingEventArgs args)
        {
            if (!Equals(args.Parameter, true)) return;

            var objCafForm = args.Session.Content as CafForm;

            if (objCafForm == null) return;

            var objCafFormVieModel = objCafForm.DataContext as CafFormViewModel;

            if (objCafFormVieModel == null) return;

            var caf = new CafResultSelectableViewModel
            {
                Id = $"{objCafFormVieModel.ID}",
                Start = $"{objCafFormVieModel.FolioStartNumber}",
                End = $"{objCafFormVieModel.FolioEndNumber}",
                Current = $"{objCafFormVieModel.FolioCurrentNumber}",
                Date = $"{objCafFormVieModel.AuthorizationDate.ToShortDateString()}",
                Type = objCafFormVieModel.SelectedDocumentTypeValue.Description,
                Store = objCafFormVieModel.SelectedStoreValue != null && objCafFormVieModel.SelectedStoreValue.Id != 0 ?  $"{objCafFormVieModel.SelectedStoreValue.Name}" : string.Empty,
                Disabled = objCafFormVieModel.Disabled,
                AuthorizationDate = objCafFormVieModel.AuthorizationDate,
                IsViewEditVisible = true
            };

            var lastIndex = -1;
            var index = 0;

            foreach (var it in CafResults)
            {
                if (it.Id == caf.Id)
                {
                    lastIndex = index;
                    break;
                }

                index++;
            }

            if(lastIndex >= 0)
            {
                CafResults[lastIndex] = caf;
            }
            else
            {
                CafResults.Add(caf);
            }

            ViewSource.View.Refresh();

        }

        public ICommand ClearCommand { get; }
        public ICommand SearchCommand { get; }
        public ICommand DeleteCommand { get; }
        public RelayCommand SelectAllCheckboxColumnCommand { get; set; }
        public ICommand ShowDialogCommand { get; }

        public CollectionViewSource ViewSource { get; set; }
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

        public Document_Type SelectedDocumentTypeValue
        {
            get { return _selectedDocumentTypeValue; }
            set
            {
                _selectedDocumentTypeValue = value;
                this.NotifyPropertyChanged();
            }
        }

        public systblApp_TaxReceipt_Store SelectedStoreValue
        {
            get { return _selectedStoreValue; }
            set
            {
                _selectedStoreValue = value;
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
        public IList<Document_Type> DocumentTypeList { get; }
        public IList<systblApp_TaxReceipt_Store> StoreList { get; }
        public ObservableCollection<CafResultSelectableViewModel> CafResults { get; set; }

        private void Clear(object obj)
        {
            FolioStartNumber = 0;
            FolioEndNumber = 0;
            FolioCurrentNumber = 0;
            SelectedDocumentTypeValue = new Document_Type() { Code = "0", Description = "--Seleccione Tipo Documento --" };
            SelectedStoreValue = new systblApp_TaxReceipt_Store() { Id = 0, Name = "--Seleccione Tienda --" };
        }

        private void Delete(object obj)
        {
            var itemsToDelete = CafResults.Where(m => m.IsSelected);

            if (itemsToDelete != null && itemsToDelete.Count() > 0)
            {
                foreach (var item in itemsToDelete)
                {
                    var cafService = new CafApiService();

                    cafService.DeleteCaf(int.Parse(item.Id));

                    Status = "CAF Eliminados";
                }

                var itemsToPreserve = CafResults.Where(m => !m.IsSelected).ToList();
                CafResults.Clear();

                foreach (var item in itemsToPreserve)
                {
                    CafResults.Add(item);
                }

                ViewSource.View.Refresh();
            }
            else
            {
                Status = "Seleccione los CAF a eliminar";
            }
        }

        private void Search(object obj)
        {
            var cafService = new CafApiService();
            try
            {
                var results = cafService.SearchCafs(0, SelectedDocumentTypeValue.Code, FolioStartNumber, FolioEndNumber, FolioCurrentNumber, SelectedStoreValue.Id);
                CafResults.Clear();
                foreach (var item in results)
                {
                    CafResults.Add(new CafResultSelectableViewModel
                    {
                        Id = $"{item.Id}",
                        Start = $"{item.FolioStartNumber}",
                        End = $"{item.FolioEndNumber}",
                        Current = $"{item.FolioCurrentNumber}",
                        Date = $"{item.AuthorizationDate.ToShortDateString()}",
                        Type = DocumentTypeList.Where(m => m.Code == item.DocumentType).FirstOrDefault().Description,
                        Store = item.RecAgent == 0 ? string.Empty : StoreList.Where(m => m.Id == item.RecAgent).FirstOrDefault().Name,
                        Disabled = item.fDisabled.HasValue ? item.fDisabled.Value : false,
                        AuthorizationDate = item.AuthorizationDate,
                        IsViewEditVisible = true
                    });
                }

                Status = "Busqueda exitosa en Base de Datos";
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
                Status = $"{ex.Message}";
            }
        }

        private void SelectAllCheckboxColumnCommandAction(object obj)
        {

            foreach (var item in CafResults)
            {
                item.IsSelected = (bool)obj;
            }

        }
        public bool? IsAllCafResultsSelected
        {
            get { return _isAllCafResultsSelected; }
            set
            {
                if (_isAllCafResultsSelected == value) return;

                _isAllCafResultsSelected = value;

                if (_isAllCafResultsSelected.HasValue)
                    SelectAll(_isAllCafResultsSelected.Value, CafResults);

                NotifyPropertyChanged();
            }
        }

        private void SelectAll(bool select, IEnumerable<CafResultSelectableViewModel> models)
        {
            foreach (var model in models)
            {
                model.IsSelected = select;
            }
        }
    }
}
