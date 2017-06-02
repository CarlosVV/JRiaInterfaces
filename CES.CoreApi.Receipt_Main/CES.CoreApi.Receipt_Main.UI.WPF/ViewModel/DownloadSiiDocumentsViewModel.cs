/*
 Descargar Folios

 -	indicar el número de documentos que existen en el rango desde hasta en la base de datos
-	Antes de descargar ver si existen en la base de datos y no insertar duplicado

-	Al estar la base de datos vacia no permite agregar rangos manuales y el buscar se cae
-	agregar rangos permanentes y temporales y al limpiar o buscar deben quedar los permanentes

-	despues que finaliza la descarga eliminar el check

-	despues de la descarga al sacar el check se cae


-	limpiar no esta funcionando

 */

using CES.CoreApi.Receipt_Main.Application.Core;
using CES.CoreApi.Receipt_Main.Application.Core.Document;
using CES.CoreApi.Receipt_Main.Domain.Core.Documents;
using CES.CoreApi.Receipt_Main.Domain.Core.Services;
using CES.CoreApi.Receipt_Main.Infrastructure.Core;
using CES.CoreApi.Receipt_Main.UI.WPF.Helpers;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using System.Windows.Data;

namespace CES.CoreApi.Receipt_Main.UI.WPF.ViewModel
{
    public class DownloadSiiDocumentsViewModel : INotifyPropertyChanged
    {
        private readonly DocumentDownloader _documentDownloader;
        private readonly XmlDocumentParser<EnvioBOLETA> _parserBoletas;
        private NewDocumentToDownloadViewModel _newDocumentToDownload;
        private BackgroundWorker _worker;
        private readonly IDocumentService _documentService;
        private readonly ITaxEntityService _taxEntityService;
        private readonly ITaxAddressService _taxAddressService;
        private readonly ISequenceService _sequenceService;
        private readonly IStoreService _storeService;
        private readonly DelegateCommand<object> _searchRangesCommand;
        private readonly DelegateCommand<object> _addNewRangeCommand;
        private readonly DelegateCommand<object> _cleanNewRangeCommand;
        private readonly DelegateCommand<object> _cancelNewRangeCommand;
        private readonly DelegateCommand<object> _downloadDocumentsCommand;
        private readonly DelegateCommand<object> _cleanFormCommand;
        private readonly Func<string, string, bool> _confirm;
        private readonly Func<string, string, bool> _msgbox;
        private string _gridStatus;
        private bool? _isAllDocumentResultsSelected;
        private int _currentProgress;
        private DateTime _startDate;
        private DateTime _endDate;
        private List<Tuple<int, int>> _intervalList;
        private DocumentHandlerService _documentHelper;
        private Document_Type _selectedDocumentTypeValue;
        private string _status;
        private string _documentsToDownloadQuantityMessage;

        public DownloadSiiDocumentsViewModel(Func<string, string, bool> msgbox, Func<string, string, bool> confirm, IDocumentService documentService, ITaxEntityService taxEntityService, ITaxAddressService taxAddressService, ISequenceService sequenceService, IStoreService storeService)
        {
            _confirm = confirm;
            _msgbox = msgbox;
            _documentService = documentService;
            _taxEntityService = taxEntityService;
            _taxAddressService = taxAddressService;
            _sequenceService = sequenceService;
            _storeService = storeService;
            _worker = new BackgroundWorker();
            _searchRangesCommand = new DelegateCommand<object>(SearchRanges, CanSearchRanges);
            _addNewRangeCommand = new DelegateCommand<object>(AddNewRange, CanSearchRanges);
            _cleanNewRangeCommand = new DelegateCommand<object>(CleanNewRange, CanSearchRanges);
            _cancelNewRangeCommand = new DelegateCommand<object>(CancelNewRange, CanSearchRanges);
            _downloadDocumentsCommand = new DelegateCommand<object>(DownloadDocuments, CanSearchRanges);
            _cleanFormCommand = new DelegateCommand<object>(CleanForm, CanSearchRanges);
            SelectAllCheckboxColumnCommand = new RelayCommand(SelectAllCheckboxColumnCommandAction);
            ShowEditRangeCommand = new RelayCommand(ShowEditRangeCommandAction);
            _documentDownloader = new DocumentDownloader();
            _parserBoletas = new XmlDocumentParser<EnvioBOLETA>();

            NewDocumentToDownload = new NewDocumentToDownloadViewModel();
            NewDocumentToDownload.ID = -1;
            DocumentsToDownload = new ObservableCollection<DocumentSearchToDownloadSelectableViewModel>();
            this.ViewSource = new CollectionViewSource();
            ViewSource.Source = DocumentsToDownload;
            GridStatus = "Número de Registros: 0";

            _documentHelper = new DocumentHandlerService(documentService, taxEntityService, taxAddressService, sequenceService, storeService);

            DocumentTypeList = DocumentTypeHelper.LoadDocumenTypes();
            SelectedDocumentTypeValue = DocumentTypeList.FirstOrDefault();

            var datetime = DateTime.Now.AddMonths(-1);

            EndDate = new DateTime(datetime.Year, datetime.Month, 1);
            StartDate = (new DateTime(datetime.Year, datetime.Month, 1)).AddMonths(1);

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

                this._gridStatus = value;
                NotifyPropertyChanged();
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

        public ObservableCollection<DocumentSearchToDownloadSelectableViewModel> DocumentsToDownload { get; set; }
        public CollectionViewSource ViewSource { get; set; }
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
        public DateTime StartDate
        {
            get { return _startDate; }
            set
            {
                _startDate = value;
                NotifyPropertyChanged();
            }
        }
        public DateTime EndDate
        {
            get { return _endDate; }
            set
            {
                _endDate = value;
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

        public string DocumentsToDownloadQuantityMessage
        {
            get { return _documentsToDownloadQuantityMessage; }
            set
            {
                _documentsToDownloadQuantityMessage = value;
                NotifyPropertyChanged();
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
            if (!_worker.IsBusy)
            {
                foreach (var item in DocumentsToDownload)
                {
                    item.IsSelected = (bool)obj;
                }
            }
           
        }
        public bool? IsAllDocumentResultsSelected
        {
            get { return _isAllDocumentResultsSelected; }
            set
            {
                if (!_worker.IsBusy)
                {

                    if (_isAllDocumentResultsSelected == value) return;

                    _isAllDocumentResultsSelected = value;

                    if (_isAllDocumentResultsSelected.HasValue)
                        SelectAll(_isAllDocumentResultsSelected.Value, DocumentsToDownload);

                    NotifyPropertyChanged();
                }
             
            }
        }
        private void SearchRanges(object parameter)
        {
            DocumentsToDownload.Clear();
            _worker = new BackgroundWorker();
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
            var start = this.NewDocumentToDownload.Start;
            var end = this.NewDocumentToDownload.End;
            var numberoffolios = int.Parse(end) - int.Parse(start) + 1;

            if (this.NewDocumentToDownload.ID == -1)
            {
                this.DocumentsToDownload.Add(new DocumentSearchToDownloadSelectableViewModel
                {
                    ID = DocumentsToDownload.Last().ID + 1,
                    DocType = SelectedDocumentTypeValue.Code,
                    Start = start,
                    End = end,
                    IsViewEditVisible = true,
                    NumberOfFolios = numberoffolios
                });
            }
            else
            {
                DocumentsToDownload[NewDocumentToDownload.ID - 1].NumberOfFolios = numberoffolios;
                DocumentsToDownload[NewDocumentToDownload.ID - 1].Start = start;
                DocumentsToDownload[NewDocumentToDownload.ID - 1].End = end;
            }

            this.NewDocumentToDownload.ID = -1;
            this.NewDocumentToDownload.Start = string.Empty;
            this.NewDocumentToDownload.End = string.Empty;
            this.NewDocumentToDownload.DocType = string.Empty;

            ViewSource.View.Refresh();
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
            this.CurrentProgress = 0;
            _worker = new BackgroundWorker();
            _worker.WorkerReportsProgress = true;
            _worker.DoWork += worker_DoWorkDownload;
            _worker.ProgressChanged += workerDownload_ProgressChanged;

            _worker.RunWorkerAsync();
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

        private void worker_DoWorkDownload(object sender, DoWorkEventArgs e)
        {
            var bgw = sender as BackgroundWorker;
            List<int> detailids = null;
            List<int> docids = null;
            var indexchunk = 1;
            var acumchunk = 0;
            int iprogress = 0;
            var documentsSelected = DocumentsToDownload.Where(m => m.IsSelected);
            var documentsToDownloadQuantity = 0;
            var numberOfDocs = documentsSelected.Count();
            int i = 1;

            documentsToDownloadQuantity = documentsSelected.Sum(m => m.NumberOfFolios);
            bgw.ReportProgress(1, documentsToDownloadQuantity);

            try
            {
                foreach (var rangeFolio in documentsSelected)
                {
                    var indexRange = rangeFolio.ID - 1;
                    DocumentsToDownload[indexRange].DownloadStatus = "Descargando...";
                    iprogress = (int)Math.Ceiling((i / (double)numberOfDocs) * 100);
                    bgw.ReportProgress(iprogress);

                    if (iprogress > 100) iprogress = 100;

                    detailids = null;
                    docids = null;
                    indexchunk = 1;
                    acumchunk = 0;
                    iprogress = 0;

                    var folioinicio = int.Parse(rangeFolio.Start);
                    var foliofin = int.Parse(rangeFolio.End);
                    var numfolios = foliofin - folioinicio + 1;

                    if (numfolios < _documentHelper.Chunkfolio)
                    {
                        _documentHelper.Chunkfolio = numfolios;
                    }

                    DocumentsToDownload[indexRange].Pending = numfolios;
                    DocumentsToDownload[indexRange].Processed = 0;
                    bgw.ReportProgress(iprogress);

                    try
                    {
                        var notFoundDocumentsFollowedQty = 0;
                        for (int folio = folioinicio; folio <= foliofin; folio++)
                        {
                            var respuesta = string.Empty;

                            if(_documentDownloader.RetrieveXML(int.Parse(SelectedDocumentTypeValue.Code), folio, out respuesta))
                            {
                                var documentXmlObject = _parserBoletas.GetDocumentObjectFromString(respuesta);

                                _documentHelper.SaveDocument(folio, foliofin, ref indexchunk, ref acumchunk, documentXmlObject, ref detailids, ref docids);

                                DocumentsToDownload[indexRange].Pending = DocumentsToDownload[indexRange].Pending - 1;
                                DocumentsToDownload[indexRange].Processed = DocumentsToDownload[indexRange].Processed + 1;
                                bgw.ReportProgress(iprogress);
                                indexchunk++;
                                notFoundDocumentsFollowedQty = 0;
                            }
                            else
                            {
                                notFoundDocumentsFollowedQty++;

                                if(notFoundDocumentsFollowedQty == 50)
                                {
                                    DocumentsToDownload[indexRange].DownloadStatus = "Descarga cancelada al no encontrar 50 documentos seguidos";
                                    DocumentsToDownload[indexRange].Pending = DocumentsToDownload[indexRange].Pending - 1;
                                    DocumentsToDownload[indexRange].Processed = DocumentsToDownload[indexRange].Processed + 1;
                                    bgw.ReportProgress(iprogress);
                                    break;
                                }
                            }                          
                        }

                        DocumentsToDownload[indexRange].DownloadStatus = "Descarga Finalizada";
                    }
                    catch (Exception ex)
                    {
                        DocumentsToDownload[indexRange].DownloadStatus = "Error en descarga";
                        DocumentsToDownload[indexRange].Detail = ex.ToString();
                    }
                    finally
                    {
                        bgw.ReportProgress(iprogress);
                    }

                    i++;
                }
            }
            catch (Exception ex)
            {
                //_msgbox(ex.ToString(), "Error");
                Trace.Write(ex);
                Status = ex.Message;
            }
            finally
            {
                bgw.ReportProgress(99);
            }
        }

        private void workerDownload_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.CurrentProgress = e.ProgressPercentage;
            ViewSource.View.Refresh();

            var quantityDocumentsToDownload = 0;

            if(e.UserState != null && int.TryParse(e.UserState.ToString() , out quantityDocumentsToDownload))
            {
                _documentsToDownloadQuantityMessage = $"Cantidad de documentos a descargar: {quantityDocumentsToDownload}";
            }         


            if (CurrentProgress == 100)
            {
                //_msgbox("Descarga Finalizada", "Descarga");
                Trace.Write("Descarga Finalizada");
                Status = "Descarga Finalizada";
            }
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
                            Start = $"{m.Item1}",
                            End = $"{m.Item2}",
                            DocType = SelectedDocumentTypeValue.Code,
                            IsDynamic = true,
                            IsViewEditVisible = false,
                            NumberOfFolios = m.Item2 - m.Item1 + 1
                        });
                    });

                ViewSource.View.Refresh();
                GridStatus = $"Número de Registros: {DocumentsToDownload.Count()}";
                //_msgbox("Busqueda Finalizada", "Busqueda");
                Trace.Write("Busqueda Finalizada");
                Status = "Busqueda Finalizada";
            }
        }
        private void SearchDynamicFilters(BackgroundWorker sender)
        {
            var iProgress = 5;
            sender.ReportProgress(iProgress);
            var documentListGlobal = _documentService.GetAllDocumentsFoliosByType(SelectedDocumentTypeValue.Code, null, null).AsParallel();
            iProgress = 10;
            sender.ReportProgress(iProgress);
            var documentList = _documentService.GetAllDocumentsFoliosByType(SelectedDocumentTypeValue.Code, _startDate, _endDate).AsParallel();
            iProgress = 20;
            sender.ReportProgress(iProgress);

            if (documentList.Count() == 0) return;

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
