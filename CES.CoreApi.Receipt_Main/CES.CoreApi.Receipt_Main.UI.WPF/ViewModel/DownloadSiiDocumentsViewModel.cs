using CES.CoreApi.Receipt_Main.Domain;
using CES.CoreApi.Receipt_Main.Model.Services;
using CES.CoreApi.Receipt_Main.Repository;
using CES.CoreApi.Receipt_Main.UI.WPF.Helpers;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.UI.WPF.ViewModel
{
    public class DownloadSiiDocumentsViewModel : INotifyPropertyChanged
    {
        private NewDocumentToDownloadViewModel _newDocumentToDownload;
        private readonly BackgroundWorker _worker;
        private readonly IDocumentService _documentService;
        private readonly DelegateCommand<object> _searchRangesCommand;
        private readonly DelegateCommand<object> _addNewRangeCommand;
        private readonly DelegateCommand<object> _cleanNewRangeCommand;
        private readonly DelegateCommand<object> _cancelNewRangeCommand;
        private readonly DelegateCommand<object> _downloadDocumentsCommand;
        private readonly DelegateCommand<object> _cleanFormCommand;
        private readonly Func<string, string, bool> _confirm;
        private ObservableCollection<DocumentSearchToDownloadSelectableViewModel> _documentsToDownload;
        private string _gridStatus;
        private bool? _isAllDocumentResultsSelected;
        private int _currentProgress;
        private object _lock_object = new object();
        private string _doctype = "39";
        private readonly static DateTime _startDate = new DateTime(DateTime.Now.Year, 2, 1); //new DateTime(DateTime.Now.Year, DateTime.Now.AddMonths(-1).Month, 1);
        private readonly static DateTime _endDate = new DateTime(DateTime.Now.Year, 3, 1); // new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        private List<Tuple<int, int>> _intervalList;
        public DownloadSiiDocumentsViewModel(Func<string, string, bool> confirm, IDocumentService documentService)
        {
            _confirm = confirm;
            _documentService = documentService;
            _worker = new BackgroundWorker();
            _documentsToDownload = new ObservableCollection<DocumentSearchToDownloadSelectableViewModel>();
            SelectAllCheckboxColumnCommand = new RelayCommand(SelectAllCheckboxColumnCommandAction);
            ShowEditRangeCommand = new RelayCommand(ShowEditRangeCommandAction);
            _searchRangesCommand = new DelegateCommand<object>(SearchRanges, CanSearchRanges);
            _addNewRangeCommand = new DelegateCommand<object>(AddNewRange, CanSearchRanges);
            _cleanNewRangeCommand = new DelegateCommand<object>(CleanNewRange, CanSearchRanges);
            _cancelNewRangeCommand = new DelegateCommand<object>(CancelNewRange, CanSearchRanges);
            _downloadDocumentsCommand = new DelegateCommand<object>(DownloadDocuments, CanSearchRanges);
            _cleanFormCommand = new DelegateCommand<object>(CleanForm, CanSearchRanges);


            NewDocumentToDownload = new NewDocumentToDownloadViewModel();
            GridStatus = "Número de Registros: 0";
        }

        #region Properties
        public int CurrentProgress
        {
            get { return this._currentProgress; }
            private set
            {
                if (this._currentProgress != value)
                {
                    this._currentProgress = value;
                    NotifyPropertyChanged("CurrentProgress");
                }
            }
        }

        public string GridStatus
        {
            get { return this._gridStatus; }
            private set
            {
                if (this.DocumentsToDownload.Count() != 0)
                {
                    this._gridStatus = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public NewDocumentToDownloadViewModel NewDocumentToDownload
        {
            get { return this._newDocumentToDownload; }
            private set
            {

                this._newDocumentToDownload = value;
                NotifyPropertyChanged();

            }
        }

        public ObservableCollection<DocumentSearchToDownloadSelectableViewModel> DocumentsToDownload
        {
            get { return _documentsToDownload; }
            set
            {
                _documentsToDownload = value;
                NotifyPropertyChanged("DocumentsToDownload");
            }
        }


        #endregion

        #region Commands
        public DelegateCommand<object> SearchRangesCommand { get { return _searchRangesCommand; } }
        public DelegateCommand<object> AddNewRangeCommand { get { return _addNewRangeCommand; } }
        public DelegateCommand<object> CleanNewRangeCommand { get { return _cleanNewRangeCommand; } }
        public DelegateCommand<object> CancelNewRangeCommand { get { return _cancelNewRangeCommand; } }
        public DelegateCommand<object> DownloadDocumentsCommand { get { return _downloadDocumentsCommand; } }
        public DelegateCommand<object> CleanFormCommand { get { return _cleanFormCommand; } }
        public RelayCommand SelectAllCheckboxColumnCommand { get; set; }
        public RelayCommand ShowEditRangeCommand { get; set; }
        private void ShowEditRangeCommandAction(object obj)
        {
            var edit = (DocumentSearchToDownloadSelectableViewModel)obj;
            NewDocumentToDownload.ID = edit.ID;
            NewDocumentToDownload.DocType = edit.DocType;
            NewDocumentToDownload.Start = edit.Start;
            NewDocumentToDownload.End = edit.End;
        }
        private void SelectAllCheckboxColumnCommandAction(object obj)
        {
            foreach (var item in DocumentsToDownload)
            {
                item.IsSelected = (bool)obj;
            }
        }
        public bool? IsAllDocumentResultsSelected
        {
            get { return _isAllDocumentResultsSelected; }
            set
            {
                if (_isAllDocumentResultsSelected == value) return;

                _isAllDocumentResultsSelected = value;

                if (_isAllDocumentResultsSelected.HasValue)
                    SelectAll(_isAllDocumentResultsSelected.Value, DocumentsToDownload);

                NotifyPropertyChanged();
            }
        }
        private void SearchRanges(object parameter)
        {
            DocumentsToDownload.Clear();

            _worker.WorkerReportsProgress = true;
            _worker.DoWork += worker_DoWork;
            _worker.ProgressChanged += worker_ProgressChanged;

            _worker.RunWorkerAsync();
        }

        private bool CanSearchRanges(object parameter)
        {
            if (_worker.IsBusy)
            {
                return false;
            }

            return true;
        }

        private void AddNewRange(object parameter)
        {
            if (this.NewDocumentToDownload.ID == 0)
            {
                var start = this.NewDocumentToDownload.Start;
                var end = this.NewDocumentToDownload.End;
                this.DocumentsToDownload.Add(new DocumentSearchToDownloadSelectableViewModel {ID = DocumentsToDownload.Last().ID + 1,  DocType = _doctype, Start = start, End = end });
            }
            else
            {
                NewDocumentToDownload.ID = NewDocumentToDownload.ID;
                DocumentsToDownload[NewDocumentToDownload.ID].Start = NewDocumentToDownload.Start;
                DocumentsToDownload[NewDocumentToDownload.ID].End = NewDocumentToDownload.End;
                NotifyPropertyChanged("DocumentsToDownload");
            }

            this.NewDocumentToDownload.ID = 0;
            this.NewDocumentToDownload.DocType = string.Empty;
            this.NewDocumentToDownload.Start = string.Empty;
            this.NewDocumentToDownload.End = string.Empty;
        }

        private bool CanAddNewRanges(object parameter)
        {
            if (_worker.IsBusy)
            {
                return false;
            }

            return true;
        }

        private void CleanNewRange(object parameter)
        {
        }

        private bool CanCleanNewRange(object parameter)
        {
            if (_worker.IsBusy)
            {
                return false;
            }

            return true;
        }

        private void CancelNewRange(object parameter)
        {
        }
        private bool CanCancelNewRange(object parameter)
        {
            if (_worker.IsBusy)
            {
                return false;
            }

            return true;
        }

        private void DownloadDocuments(object parameter)
        {

        }

        private bool CanDownloadDocuments(object parameter)
        {
            if (_worker.IsBusy)
            {
                return false;
            }

            return true;
        }

        private void CleanForm(object parameter)
        {
        }

        private bool CanCleanForm(object parameter)
        {
            if (_worker.IsBusy)
            {
                return false;
            }

            return true;
        }

        #endregion

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            SearchDynamicFilters(sender as BackgroundWorker);
        }
        private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.CurrentProgress = e.ProgressPercentage;

            if (_currentProgress == 100)
            {
                var index = 0;
                DocumentsToDownload.Clear();
                _intervalList.OrderBy(m => m.Item1).ToList().
                    ForEach(m =>
                    {
                        DocumentsToDownload.Add(new DocumentSearchToDownloadSelectableViewModel()
                        {
                            ID = ++index,
                            DocType = _doctype,
                            Start = $"{m.Item1}",
                            End = $"{m.Item2}"
                        });
                    });

                GridStatus = $"Número de Registros: {DocumentsToDownload.Count()}";
            }
        }
        private void SearchDynamicFilters(BackgroundWorker sender)
        {
            var iProgress = 2;
            sender.ReportProgress(iProgress);
            var documentListGlobal = _documentService.GetAllDocumentsFoliosByType(_doctype, null, null).AsParallel();
            iProgress = 10;
            sender.ReportProgress(iProgress);
            var documentList = _documentService.GetAllDocumentsFoliosByType(_doctype, _startDate, _endDate).AsParallel();
            iProgress = 20;
            sender.ReportProgress(iProgress);

            var folioInicio = documentList.Min();
            var folioFinal = documentList.Max();
            var initialIds = Enumerable.Range(folioInicio, folioFinal - folioInicio + 1).AsParallel().ToArray();
            var finalGapIds = initialIds.Except(documentList).AsParallel().ToArray();

            iProgress = iProgress + 30;
            sender.ReportProgress(iProgress);

            var differenceList = CreateDifferenceList(finalGapIds);
            iProgress = iProgress + 40;
            sender.ReportProgress(iProgress);

            _intervalList = GroupIntervals(differenceList);
            _intervalList = FixFolioRangesWhenExistInDatabase(_intervalList, documentListGlobal);

            iProgress = 100;
            sender.ReportProgress(iProgress);
        }
        private List<Tuple<Tuple<int, int>, int>> CreateDifferenceList(int[] finalGapIds)
        {
            List<Tuple<Tuple<int, int>, int>> differenceList;
            var tmpBeforeFoliosList = new int[finalGapIds.Count()];

            finalGapIds.Skip(1).ToArray().CopyTo(tmpBeforeFoliosList, 0);
            differenceList = finalGapIds.OrderBy(p => p).AsParallel().AsEnumerable()
                    .Select((folio, index) =>
                        Tuple.Create
                            (Tuple.Create(index, folio),
                                tmpBeforeFoliosList[index] - folio)).AsParallel().ToList();

            return differenceList;
        }
        private List<Tuple<int, int>> GroupIntervals(List<Tuple<Tuple<int, int>, int>> differenceList)
        {
            var intervalList = new List<Tuple<int, int>>();
            var rangeFolio = new List<Tuple<int, int>>();
            var previousFolio = differenceList.First();

            foreach (var folio in differenceList)
            {
                var differenceWithPrevious = Math.Abs(folio.Item1.Item2 - previousFolio.Item1.Item2);
                previousFolio = Tuple.Create(Tuple.Create(folio.Item1.Item1, folio.Item1.Item2), folio.Item2);

                if (folio.Item2 == 1 || differenceWithPrevious == 1)
                {
                    rangeFolio.Add(Tuple.Create(folio.Item1.Item1, folio.Item1.Item2));

                    if (differenceWithPrevious > 1 || (folio.Item2 == 1 && differenceWithPrevious == 1) || rangeFolio.Count() < 2) continue;
                }

                if (differenceWithPrevious != 1 && folio.Item2 != 1) intervalList.Add(Tuple.Create(folio.Item1.Item2, folio.Item1.Item2));

                if (rangeFolio.Count() >= 2) intervalList.Add(Tuple.Create(rangeFolio.First().Item2, rangeFolio.Last().Item2));

                rangeFolio = new List<Tuple<int, int>>();
            }

            return intervalList;
        }

        private List<Tuple<int, int>> FixFolioRangesWhenExistInDatabase(List<Tuple<int, int>> intervalFolioList, IEnumerable<int> documentListGlobal)
        {
            var newIntervalFolioList = new List<Tuple<int, int>>();

            foreach (var r in intervalFolioList)
            {
                var currentNumberOfFoliosInRange = r.Item2 - r.Item1 + 1;
                var currentStartFolio = r.Item1;
                var currentEndFolio = r.Item2;

                var rangeFoundInDb = documentListGlobal.Where(m => m >= currentStartFolio && m <= currentEndFolio).OrderBy(m => m).AsParallel().ToArray();
                var firstFolioInDb = rangeFoundInDb.FirstOrDefault();
                var lastFolioInDb = rangeFoundInDb.LastOrDefault();

                if (firstFolioInDb == currentStartFolio && lastFolioInDb == currentEndFolio) continue;

                if (rangeFoundInDb.Length != 0 && rangeFoundInDb.Length < currentNumberOfFoliosInRange && currentStartFolio >= firstFolioInDb) currentStartFolio = lastFolioInDb + 1;

                newIntervalFolioList.Add(Tuple.Create(currentStartFolio, currentEndFolio));
            }

            var lastRangeFolio = newIntervalFolioList.Last();
            var lastRangeFolioList = Enumerable.Range(lastRangeFolio.Item1, lastRangeFolio.Item2 - lastRangeFolio.Item1 + 1);
            var intersectionWithDbFolios = lastRangeFolioList.Intersect(documentListGlobal.OrderBy(m => m)).AsParallel();
            var firstIntersectedFolioFound = intersectionWithDbFolios.FirstOrDefault();
            newIntervalFolioList.RemoveAt(newIntervalFolioList.Count() - 1);

            if (firstIntersectedFolioFound > lastRangeFolio.Item1) newIntervalFolioList.Add(Tuple.Create(lastRangeFolio.Item1, firstIntersectedFolioFound - 1));

            return newIntervalFolioList;
        }

        private void SelectAll(bool select, IEnumerable<DocumentSearchToDownloadSelectableViewModel> models)
        {
            foreach (var model in models)
            {
                model.IsSelected = select;
            }
        }
        private Action<PropertyChangedEventArgs> RaisePropertyChanged()
        {
            return args => PropertyChanged?.Invoke(this, args);
        }

        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
