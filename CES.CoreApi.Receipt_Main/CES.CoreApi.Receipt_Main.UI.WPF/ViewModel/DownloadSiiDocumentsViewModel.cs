using CES.CoreApi.Receipt_Main.Domain;
using CES.CoreApi.Receipt_Main.Model.Services;
using CES.CoreApi.Receipt_Main.Repository;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
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
        private readonly ObservableCollection<CafResultSelectableViewModel> _documentsToDownload;
        private int currentProgress;
        private object lock_object = new object();
        public DownloadSiiDocumentsViewModel(Func<string, string, bool> confirm, IDocumentService documentService)
        {
            _confirm = confirm;
            _documentService = documentService;
            _searchRangesCommand = new DelegateCommand<object>(SearchRanges, CanSearchRanges);
            //this._searchRangesCommand = new DelegateCommand(o => this.worker.RunWorkerAsync(), o => !this.worker.IsBusy);

            worker = new BackgroundWorker();
            //this.worker.DoWork += this.worker_DoWork;
            //this.worker.ProgressChanged += this.ProgressChanged;
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
                    //this.OnPropertyChanged(() => this.CurrentProgress);
                    NotifyPropertyChanged("CurrentProgress");
                }
            }
        }
        #endregion

        #region Commands
        public DelegateCommand<object> SearchRangesCommand { get { return _searchRangesCommand; } }

        #endregion

        private void SearchRanges(object parameter)
        {
            //var doSearch = _confirm("¿Está seguro?", "Confirmar Buscar");
            /*
                SELECT fDocType, fStart, fStop FROM (
	            SELECT m.Folio + 1 as fStart, m.DocumentTYpe as fDocType,
		            (SELECT min(Folio) - 1 from [systblApp_CoreAPI_Document] (nolock) as x 
	            WHERE x.Folio > m.Folio and x.DocumentType=m.DocumentType) as fStop
	            FROM [systblApp_CoreAPI_Document] (nolock) as m 
	            left outer join [systblApp_CoreAPI_Document] as r on m.Folio = r.Folio - 1 and m.DocumentType=r.DocumentType 
	            WHERE r.Folio is null  and m.DocumentType = 39 and m.IssuedDate >= '2017/01/01 00:00:00'
                ) as x
            */
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
        }
        private void SearchDynamicFilters(BackgroundWorker sender)
        {
            var doctype = "39";
            var startDate = new DateTime(2017, 4, 1);
            var endDate = new DateTime(2017, 5, 1);
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
            var intervalList = GroupIntervals(differenceList);
            iProgress = iProgress + 40;
            sender.ReportProgress(iProgress);
            var resultados = intervalList.OrderBy(m => m.Item1).Select(m => new { DocType = doctype, Start = m.Item1, Stop = m.Item2 }).ToList();

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

            //differenceList.Add(Tuple.Create(Tuple.Create(0, 0), 0));

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

        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
