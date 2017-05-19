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
        private readonly BackgroundWorker worker;
        private readonly IDocumentService _documentService;
        private readonly DelegateCommand<object> _searchRangesCommand;
        private readonly Func<string, string, bool> _confirm;
        private ObservableCollection<DocumentSearchToDownloadSelectableViewModel> _documentsToDownload;
        private bool? _isAllDocumentResultsSelected;
        private int currentProgress;
        private object lock_object = new object();
        private string doctype = "39";
        private DateTime startDate = new DateTime(2017, 4, 1);
        private DateTime endDate = new DateTime(2017, 5, 1);
        private List<Tuple<int, int>> intervalList;
        public DownloadSiiDocumentsViewModel(Func<string, string, bool> confirm, IDocumentService documentService)
        {
            _confirm = confirm;
            _documentService = documentService;
            _documentsToDownload = new ObservableCollection<DocumentSearchToDownloadSelectableViewModel>();
            _searchRangesCommand = new DelegateCommand<object>(SearchRanges, CanSearchRanges);
            worker = new BackgroundWorker();
            MyCommand = new RelayCommand(MyCommandAction);
        }

        #region Properties
        public int CurrentProgress
        {
            get { return this.currentProgress; }
            private set
            {
                if (this.currentProgress != value)
                {
                    this.currentProgress = value;
                    NotifyPropertyChanged("CurrentProgress");
                }
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

        public RelayCommand MyCommand { get; set; }
        private void MyCommandAction(object obj)
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
        #endregion

        #region Commands
        public DelegateCommand<object> SearchRangesCommand { get { return _searchRangesCommand; } }

        #endregion

        private void SearchRanges(object parameter)
        {
            worker.WorkerReportsProgress = true;
            worker.DoWork += worker_DoWork;
            worker.ProgressChanged += worker_ProgressChanged;

            worker.RunWorkerAsync();
        }
        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            SearchDynamicFilters(sender as BackgroundWorker);
        }
        private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.CurrentProgress = e.ProgressPercentage;

            if(currentProgress ==  100)
            {
                intervalList.OrderBy(m => m.Item1).ToList().ForEach(m => { DocumentsToDownload.Add(new DocumentSearchToDownloadSelectableViewModel() { DocType = doctype, Start = $"{m.Item1}", End = $"{m.Item2}" }); });
            }
        }
        private void SearchDynamicFilters(BackgroundWorker sender)
        {
            //var documentListGlobal = _documentService.GetAllDocumentsFoliosByType(doctype, null, null);
            var documentList = _documentService.GetAllDocumentsFoliosByType(doctype, startDate, endDate);
            var iProgress = 20;
            sender.ReportProgress(iProgress);

            var folioInicio = documentList.Min();
            var folioFinal = documentList.Max();
            var initialIds = Enumerable.Range(folioInicio, folioFinal - folioInicio + 1).AsParallel().ToArray();
            var finalGapIds = initialIds.Except(documentList).AsParallel().ToArray(); ;//.Except(documentListGlobal).AsParallel().ToArray();

            iProgress = iProgress + 30;
            sender.ReportProgress(iProgress);

            var differenceList = CreateDifferenceList(finalGapIds);
            iProgress = iProgress + 40;
            sender.ReportProgress(iProgress);

            intervalList = GroupIntervals(differenceList);                     
            iProgress = 100;
            sender.ReportProgress(iProgress);
        }

        private List<Tuple<Tuple<int, int>, int>> CreateDifferenceList(int[] finalGapIds)
        {
            List<Tuple<Tuple<int, int>, int>> differenceList;
            var tmpBeforeFoliosList = new int[finalGapIds.Count()];
            var tmpAfterFoliosList = new int[finalGapIds.Count()];

            finalGapIds.Skip(1).ToArray().CopyTo(tmpBeforeFoliosList, 0);

            differenceList = finalGapIds.OrderBy(p => p).AsParallel().AsEnumerable()
                       .Select((folio, index) =>
                            Tuple.Create(
                                Tuple.Create(index, folio),
                                tmpBeforeFoliosList[index] - folio)).AsParallel().ToList();

            return differenceList;
        }

        private List<Tuple<int, int>> GroupIntervals(List<Tuple<Tuple<int, int>, int>> differenceList)
        {
            var intervalFolioList = new List<Tuple<int, int>>();
            var rangeFolio = new List<Tuple<int, int>>();
            var past = differenceList.First();

            foreach (var folio in differenceList)
            {
                var differenceWithUp = Math.Abs(folio.Item1.Item2 - past.Item1.Item2);
                past = Tuple.Create(Tuple.Create(folio.Item1.Item1, folio.Item1.Item2), folio.Item2);

                if (folio.Item2 == 1 || differenceWithUp == 1)
                {
                    rangeFolio.Add(Tuple.Create(folio.Item1.Item1, folio.Item1.Item2));

                    if (differenceWithUp > 1 || (folio.Item2 == 1 && differenceWithUp == 1) || rangeFolio.Count() < 2) continue;
                }

                if (differenceWithUp != 1 && folio.Item2 != 1) intervalFolioList.Add(Tuple.Create(folio.Item1.Item2, folio.Item1.Item2));

                if (rangeFolio.Count() >= 2) intervalFolioList.Add(Tuple.Create(rangeFolio.First().Item2, rangeFolio.Last().Item2));

                rangeFolio = new List<Tuple<int, int>>();
            }

            return intervalFolioList;
        }

        private bool CanSearchRanges(object parameter)
        {
            if (worker.IsBusy)
            {
                return false;
            }

            return true;
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
