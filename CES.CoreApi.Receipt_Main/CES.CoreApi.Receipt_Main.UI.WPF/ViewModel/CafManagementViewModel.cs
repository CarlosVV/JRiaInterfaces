using CES.CoreApi.Receipt_Main.Model;
using CES.CoreApi.Receipt_Main.Model.Documents;
using CES.CoreApi.Receipt_Main.UI.WPF.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.UI.WPF.ViewModel
{
    public class CafManagementViewModel : INotifyPropertyChanged
    {
        private string _foliostart;
        private string _folioend;
        private Document_Type _selectedDocumentTypeValue;
        private Store _selectedStoreValue;
        private readonly ObservableCollection<CafResultSelectableViewModel> _cafResults;
        private bool? _isAllCafResultsSelected;
        public event PropertyChangedEventHandler PropertyChanged;

        public CafManagementViewModel()
        {
            DocumentTypeList = new List<Document_Type>();
            //TODO: Get Document Type from DB
            DocumentTypeList.Add(new Document_Type { Code = "39", Description = "Boleta" });
            DocumentTypeList.Add(new Document_Type { Code = "56", Description = "Factura" });
            DocumentTypeList.Add(new Document_Type { Code = "57", Description = "Nota de Credito" });
            DocumentTypeList.Add(new Document_Type { Code = "58", Description = "Nota de Debio" });

            SelectedDocumentTypeValue = DocumentTypeList.FirstOrDefault();
            //TODO: Get Stores from DB
            StoreList = new List<Store>();
            StoreList.Add(new Store { Id = Guid.NewGuid(), Name = "Banderas" });
            StoreList.Add(new Store { Id = Guid.NewGuid(), Name = "Catedral" });
            SelectedStoreValue = StoreList.First();

            //TODO: Load Serch Result
            _cafResults = CreateData();

        }

        public string FolioStart
        {
            get { return _foliostart; }
            set
            {
                this.MutateVerbose(ref _foliostart, value, RaisePropertyChanged());
            }
        }

        public string FolioEnd
        {
            get { return _folioend; }
            set
            {
                this.MutateVerbose(ref _folioend, value, RaisePropertyChanged());
            }
        }

        public Document_Type SelectedDocumentTypeValue
        {
            get { return _selectedDocumentTypeValue; }
            set
            {
                this.MutateVerbose(ref _selectedDocumentTypeValue, value, RaisePropertyChanged());
            }
        }

        public Store SelectedStoreValue
        {
            get { return _selectedStoreValue; }
            set
            {
                this.MutateVerbose(ref _selectedStoreValue, value, RaisePropertyChanged());
            }
        }

        public IList<Document_Type> DocumentTypeList { get; }
        public IList<Store> StoreList { get; }
        public ObservableCollection<CafResultSelectableViewModel> CafResults
        {
            get { return _cafResults; }
        }

        private Action<PropertyChangedEventArgs> RaisePropertyChanged()
        {
            return args => PropertyChanged?.Invoke(this, args);
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

                OnPropertyChanged();
            }
        }

        private void SelectAll(bool select, IEnumerable<CafResultSelectableViewModel> models)
        {
            foreach (var model in models)
            {
                model.IsSelected = select;
            }
        }
        private static ObservableCollection<CafResultSelectableViewModel> CreateData()
        {
            return new ObservableCollection<CafResultSelectableViewModel>
            {
                new CafResultSelectableViewModel
                {
                    Id = "1",
                    Type = "Boleta",
                    Date = "28-04-2017",
                    Start = "1",
                    End = "100",
                    Current = "2"
                },
                new CafResultSelectableViewModel
                {
                    Id = "2",
                    Type = "Boleta",
                    Date = "28-04-2017",
                    Start = "1",
                    End = "100",
                    Current = "2"
                },
                new CafResultSelectableViewModel
                {
                    Id = "3",
                    Type = "Boleta",
                    Date = "28-04-2017",
                    Start = "1",
                    End = "100",
                    Current = "2"
                }
            };
        }
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }       
    }
}
