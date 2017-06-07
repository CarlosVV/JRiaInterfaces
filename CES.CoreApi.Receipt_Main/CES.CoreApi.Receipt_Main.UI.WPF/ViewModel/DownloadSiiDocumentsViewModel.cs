using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Data;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using Newtonsoft.Json;
using Prism.Commands;
using CES.CoreApi.Receipt_Main.Application.Core;
using CES.CoreApi.Receipt_Main.Application.Core.Document;
using CES.CoreApi.Receipt_Main.Domain.Core.Documents;
using CES.CoreApi.Receipt_Main.Domain.Core.Security;
using CES.CoreApi.Receipt_Main.Domain.Core.Services;
using CES.CoreApi.Receipt_Main.Infrastructure.Core;
using CES.CoreApi.Receipt_Main.Infrastructure.Data;
using CES.CoreApi.Receipt_Main.Infrastructure.Data.Repository;
using CES.CoreApi.Receipt_Main.Repository.Repository;
using CES.CoreApi.Receipt_Main.UI.WPF.Helpers;
using CES.CoreApi.Receipt_Main.UI.WPF.View;

namespace CES.CoreApi.Receipt_Main.UI.WPF.ViewModel
{
    public class DownloadSiiDocumentsViewModel : INotifyPropertyChanged
    {
        private const string ID_NEW = "(Nuevo)";
        private const string doc_type_bl = "39";
        private const string range_type_perm = "Permanente";
        private const string param_name_ranges = "Download_Ranges";
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
        private IParameterService parameterService;
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
            _cleanNewRangeCommand = new DelegateCommand<object>(CleanNewRange, CanSearchRanges);
            _cancelNewRangeCommand = new DelegateCommand<object>(CancelNewRange, CanSearchRanges);
            _downloadDocumentsCommand = new DelegateCommand<object>(DownloadDocuments, CanSearchRanges);
            _cleanFormCommand = new DelegateCommand<object>(CleanForm, CanSearchRanges);
            SelectAllCheckboxColumnCommand = new RelayCommand(SelectAllCheckboxColumnCommandAction);
            ShowEditRangeCommand = new RelayCommand(ShowEditRangeCommandAction);
            _documentDownloader = new DocumentDownloader();
            _parserBoletas = new XmlDocumentParser<EnvioBOLETA>();

            NewDocumentToDownload = new NewDocumentToDownloadViewModel();
            NewDocumentToDownload.ID = ID_NEW;
            DocumentsToDownload = new ObservableCollection<DocumentSearchToDownloadSelectableViewModel>();
            this.ViewSource = new CollectionViewSource();
            ViewSource.Source = DocumentsToDownload;
            GridStatus = "Número de Registros: 0";

            _documentHelper = new DocumentHandlerService(documentService, taxEntityService, taxAddressService, sequenceService, storeService);

            DocumentTypeList = DocumentTypeHelper.GetDocumenTypes();
            SelectedDocumentTypeValue = DocumentTypeList.FirstOrDefault();

            var datetime = DateTime.Now.AddMonths(-1);
            StartDate = new DateTime(datetime.Year, datetime.Month, 1);
            EndDate = (new DateTime(datetime.Year, datetime.Month, 1)).AddMonths(1);

            parameterService = new ParameterService(new ParameterRepository(new ReceiptDbContext()));

            RetrieveRangesFromDB();
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

        public ICommand ShowDialogCommand { get; }
        public DelegateCommand<object> SearchRangesCommand { get { return _searchRangesCommand; } }
       
        public DelegateCommand<object> CleanNewRangeCommand { get { return _cleanNewRangeCommand; } }
        public DelegateCommand<object> CancelNewRangeCommand { get { return _cancelNewRangeCommand; } }
        public DelegateCommand<object> DownloadDocumentsCommand { get { return _downloadDocumentsCommand; } }
        public DelegateCommand<object> CleanFormCommand { get { return _cleanFormCommand; } }
        public RelayCommand SelectAllCheckboxColumnCommand { get; set; }
        public RelayCommand ShowEditRangeCommand { get; set; }
        private void ShowEditRangeCommandAction(object obj)
        {
            var objRange = obj as DocumentSearchToDownloadSelectableViewModel;

            if (objRange != null)
            {
                NewDocumentToDownload.ID = objRange.ID;
                NewDocumentToDownload.DocType = objRange.DocType;
                NewDocumentToDownload.SelectedDocumentTypeValue = DocumentTypeHelper.GetDocumenTypes().Where(m => m.Description == objRange.DocType).FirstOrDefault();
                NewDocumentToDownload.Start = objRange.Start;
                NewDocumentToDownload.End = objRange.End;
                NewDocumentToDownload.SelectedRangeTypeValue = objRange.RangeType;
            }
            else
            {
                ClearAddRangeDialog();
            }

            var dialog = new AddEditRanges(NewDocumentToDownload);

            DialogHost.Show(dialog, DialogCloseEvenHandler);
        }

        public void DialogCloseEvenHandler(object sender, DialogClosingEventArgs args)
        {
            if (!Equals(args.Parameter, true)) return;

            var dialog = args.Session.Content as AddEditRanges;

            var viewModelResult = dialog.DataContext as AddEditRangesViewModel;

            if (viewModelResult != null)
            {
                NewDocumentToDownload = viewModelResult.NewDocumentToDownload;

                if (string.IsNullOrWhiteSpace(this.NewDocumentToDownload.Start)) return;
                if (string.IsNullOrWhiteSpace(this.NewDocumentToDownload.End)) return;


                var start = this.NewDocumentToDownload.Start;
                var end = this.NewDocumentToDownload.End;
                var numberoffolios = int.Parse(end) - int.Parse(start) + 1;

                if (this.NewDocumentToDownload.ID == ID_NEW)
                {
                    var id = 0;
                    if (DocumentsToDownload.Count > 0)
                    {
                        id = int.Parse(DocumentsToDownload.Last().ID) + 1;
                    }

                    this.DocumentsToDownload.Add(new DocumentSearchToDownloadSelectableViewModel
                    {
                        ID = id.ToString(),
                        DocType = SelectedDocumentTypeValue.Code,
                        RangeType = NewDocumentToDownload.SelectedRangeTypeValue,
                        Start = start,
                        End = end,
                        IsViewEditVisible = true,
                        NumberOfFolios = numberoffolios
                    });

                }
                else
                {
                    var index = 0;
                    if (int.TryParse(NewDocumentToDownload.ID, out index))
                    {
                        DocumentsToDownload[index].NumberOfFolios = numberoffolios;
                        DocumentsToDownload[index].Start = start;
                        DocumentsToDownload[index].End = end;
                    }
                }

                UpdateRangesInDB();

                ViewSource.View.Refresh();
            }

            ClearAddRangeDialog();
        }

        private void ClearAddRangeDialog()
        {
            this.NewDocumentToDownload.ID = ID_NEW;
            this.NewDocumentToDownload.Start = string.Empty;
            this.NewDocumentToDownload.End = string.Empty;
            this.NewDocumentToDownload.DocType = doc_type_bl;
            this.NewDocumentToDownload.SelectedDocumentTypeValue = this.NewDocumentToDownload.DocumentTypeList.FirstOrDefault();
            this.NewDocumentToDownload.SelectedRangeTypeValue = range_type_perm;
        }

        private void UpdateRangesInDB()
        {
            var rangesGrid = this.DocumentsToDownload.Where(m => m.RangeType == range_type_perm);
            var newValue = JsonConvert.SerializeObject(rangesGrid);
            var objRangeDb = parameterService.GetAllParameters().Where(m => m.Name == param_name_ranges).FirstOrDefault();

            if (objRangeDb != null)
            {
                var valRangeDb = objRangeDb.Value;
                objRangeDb.Value = newValue;
                parameterService.SaveChanges();
            }
            else
            {
                var parameters = parameterService.GetAllParameters();
                var id = parameters.Count() == 0 ? 1 : parameters.Last().Id + 1;
                var newObjDb = new systblApp_TaxReceipt_Parameter()
                {
                    Name = param_name_ranges,
                    Description = param_name_ranges,
                    Value = newValue,
                    Id = id
                };
                parameterService.CreateParameter(newObjDb);
                parameterService.SaveChanges();
            }
        }

        private void RetrieveRangesFromDB()
        {
            var rangesGrid = this.DocumentsToDownload.Where(m => m.IsViewEditVisible);
            var newValue = JsonConvert.SerializeObject(rangesGrid);
            var objRangeDb = parameterService.GetAllParameters().Where(m => m.Name == param_name_ranges).FirstOrDefault();

            if (objRangeDb != null)
            {
                var valRangeDb = objRangeDb.Value;
                var ranges = JsonConvert.DeserializeObject<List<DocumentSearchToDownloadSelectableViewModel>>(valRangeDb);
                var idInit = DocumentsToDownload.Count == 0 ? 0 : int.Parse(DocumentsToDownload.Last().ID) + 1;
                foreach (var r in ranges)
                {
                    DocumentsToDownload.Add(new DocumentSearchToDownloadSelectableViewModel
                    {
                        ID = idInit.ToString(),
                        Current = r.Current,
                        DocType = r.DocType,
                        Start = r.Start,
                        End = r.End,
                        NumberOfFolios = r.NumberOfFolios,
                        RangeType = r.RangeType,
                        IsDynamic = false,
                        IsSelected = false,
                        IsViewEditVisible = true,
                    });
                    idInit++;
                }

            }
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
            _worker = new BackgroundWorker();
            _worker.WorkerReportsProgress = true;
            _worker.DoWork += worker_SearchDynamicRanges;
            _worker.ProgressChanged += workerSearchRanges_ProgressChanged;
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
            _worker.DoWork += worker_DownloadDocuments;
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
            ClearRanges();
            GridStatus = "Número de Registros: 0";
        }
        private void ShowDialogAction(object obj)
        {
            var rangeObject = obj as NewDocumentToDownloadViewModel;


            var dialog = new AddEditRanges(rangeObject)
            {
            };

            DialogHost.Show(dialog, "RootDialog");
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

        private void worker_SearchDynamicRanges(object sender, DoWorkEventArgs e)
        {
            var backgroundWorker = sender as BackgroundWorker;

            var iProgress = 5;
            backgroundWorker.ReportProgress(iProgress);
            var documentListGlobal = _documentService.GetAllDocumentsFoliosByType(SelectedDocumentTypeValue.Code, null, null).AsParallel();
            iProgress = 10;
            backgroundWorker.ReportProgress(iProgress);
            var documentList = _documentService.GetAllDocumentsFoliosByType(SelectedDocumentTypeValue.Code, _startDate, _endDate).AsParallel();
            iProgress = 20;
            backgroundWorker.ReportProgress(iProgress);

            if (documentList.Count() == 0)
            {
                iProgress = 0;
                backgroundWorker.ReportProgress(iProgress, "No hay documentos en el rango de fecha y con las condiciones de busqueda.");
                return;
            }

            var folioInicio = documentList.Min();
            var folioFinal = documentList.Max();
            var initialIds = Enumerable.Range(folioInicio, folioFinal - folioInicio + 1).AsParallel().ToArray();
            var finalGapIds = initialIds.Except(documentList).AsParallel().ToArray();

            iProgress = iProgress + 30;
            backgroundWorker.ReportProgress(iProgress);

            var differenceList = CreateDifferenceList(finalGapIds);

            if (differenceList.Count == 0)
            {
                iProgress = 100;
                backgroundWorker.ReportProgress(iProgress, "No hubo rangos encontrados para los parámetros suministrados");
                return;
            }

            iProgress = iProgress + 40;
            backgroundWorker.ReportProgress(iProgress);

            _intervalList = GroupIntervals(differenceList);
            _intervalList = FixFolioRangesWhenExistInDatabase(_intervalList, documentListGlobal);

            iProgress = 100;
            backgroundWorker.ReportProgress(iProgress);
        }

        private void worker_DownloadDocuments(object sender, DoWorkEventArgs e)
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
                    var indexRange = int.Parse(rangeFolio.ID);
                    DocumentsToDownload[indexRange].DownloadStatus = $"Descargando Folios...";
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

                    var numberInDb = GetNumberOfDocsInDb(folioinicio, foliofin);
                    DocumentsToDownload[indexRange].DownloadStatus = $"Número de Documentos en DB: {numberInDb}";

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
                            if (!ExistFolioInDb(folio))
                            {
                                var respuesta = string.Empty;

                                if (_documentDownloader.RetrieveXML(int.Parse(SelectedDocumentTypeValue.Code), folio, out respuesta))
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

                                    if (notFoundDocumentsFollowedQty == 50)
                                    {
                                        DocumentsToDownload[indexRange].DownloadStatus = "Descarga cancelada al no encontrar 50 documentos seguidos";
                                        DocumentsToDownload[indexRange].Pending = DocumentsToDownload[indexRange].Pending - 1;
                                        DocumentsToDownload[indexRange].Processed = DocumentsToDownload[indexRange].Processed + 1;

                                        bgw.ReportProgress(iprogress);
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                DocumentsToDownload[indexRange].Pending = DocumentsToDownload[indexRange].Pending - 1;
                                DocumentsToDownload[indexRange].Processed = DocumentsToDownload[indexRange].Processed + 1;

                                bgw.ReportProgress(iprogress);
                                indexchunk++;
                                notFoundDocumentsFollowedQty = 0;
                            }
                        }

                        DocumentsToDownload[indexRange].DownloadStatus = "Descarga Finalizada";
                        DocumentsToDownload[indexRange].IsSelected = false;
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
                Trace.Write(ex);
                Status = ex.Message;
            }
            finally
            {
                bgw.ReportProgress(99);
            }
        }

        private int GetNumberOfDocsInDb(int folioinicio, int foliofin)
        {
            var _documentServiceForSearch = new DocumentService(new DocumentRepository(new ReceiptDbContext()));
            return _documentServiceForSearch.GetAllDocuments().Where(f => f.Folio >= folioinicio && f.Folio <= foliofin).Count();
        }

        private bool ExistFolioInDb(int folio)
        {
            var _documentServiceForSearch = new DocumentService(new DocumentRepository(new ReceiptDbContext()));
            return _documentServiceForSearch.GetAllDocuments().Where(f => f.Folio == folio).Any();
        }

        private void workerDownload_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.CurrentProgress = e.ProgressPercentage;
            try
            {
                ViewSource.View.Refresh();

            }
            catch (Exception ex)
            {
                Trace.Write(ex.ToString());
            }

            var quantityDocumentsToDownload = 0;

            if (e.UserState != null && int.TryParse(e.UserState.ToString(), out quantityDocumentsToDownload))
            {
                _documentsToDownloadQuantityMessage = $"Cantidad de documentos a descargar: {quantityDocumentsToDownload}";
            }


            if (CurrentProgress == 100)
            {
                Trace.Write("Descarga Finalizada");
                Status = "Descarga Finalizada";
            }
        }
        private void workerSearchRanges_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.CurrentProgress = e.ProgressPercentage;

            if (e.UserState != null)
            {
                Status = e.UserState.ToString();
            }

            if (_currentProgress == 100)
            {
                var index = 0;

                ClearRanges();

                if (_intervalList != null)
                {
                    index = DocumentsToDownload.Count();
                    _intervalList.OrderBy(m => m.Item1).ToList().
                   ForEach(m =>
                   {
                       DocumentsToDownload.Add(new DocumentSearchToDownloadSelectableViewModel()
                       {
                           ID = (index).ToString(),
                           Start = $"{m.Item1}",
                           End = $"{m.Item2}",
                           DocType = SelectedDocumentTypeValue.Code,
                           IsDynamic = true,
                           IsViewEditVisible = false,
                           NumberOfFolios = m.Item2 - m.Item1 + 1
                       });
                       index++;
                   });
                }

                ViewSource.View.Refresh();
                GridStatus = $"Número de Registros: {DocumentsToDownload.Count()}";
                Trace.Write("Busqueda Finalizada");
                Status = "Busqueda Finalizada";
            }
        }

        private void ClearRanges()
        {
            DocumentsToDownload.Clear();
            RetrieveRangesFromDB();
            ViewSource.View.Refresh();
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

                if (rangeFoundInDb.Length == 0)
                {
                    newIntervalFolioList.Add(Tuple.Create(currentStartFolio, currentEndFolio));
                    continue;
                }

                var firstFolioInDb = rangeFoundInDb.FirstOrDefault();
                var lastFolioInDb = rangeFoundInDb.LastOrDefault();

                if (firstFolioInDb == currentStartFolio && lastFolioInDb == currentEndFolio) continue;

                if (rangeFoundInDb.Length != 0 && rangeFoundInDb.Length < currentNumberOfFoliosInRange && currentStartFolio >= firstFolioInDb) currentStartFolio = lastFolioInDb + 1;

                newIntervalFolioList.Add(Tuple.Create(currentStartFolio, currentEndFolio));
            }

            var lastRangeFolio = newIntervalFolioList.Last();
            var lastRangeFolioList = Enumerable.Range(lastRangeFolio.Item1, lastRangeFolio.Item2 - lastRangeFolio.Item1 + 1);


            var intersectionWithDbFolios = lastRangeFolioList.Intersect(documentListGlobal.OrderBy(m => m)).AsParallel();

            if (intersectionWithDbFolios.Count() > 0)
            {
                var firstIntersectedFolioFound = intersectionWithDbFolios.FirstOrDefault();
                newIntervalFolioList.RemoveAt(newIntervalFolioList.Count() - 1);

                if (firstIntersectedFolioFound > lastRangeFolio.Item1) newIntervalFolioList.Add(Tuple.Create(lastRangeFolio.Item1, firstIntersectedFolioFound - 1));
            }

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
