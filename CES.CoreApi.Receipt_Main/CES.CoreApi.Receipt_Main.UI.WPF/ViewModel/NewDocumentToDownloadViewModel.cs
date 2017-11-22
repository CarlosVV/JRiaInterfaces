using CES.CoreApi.Receipt_Main.Application.Core;
using CES.CoreApi.Receipt_Main.Domain.Core.Documents;
using System.Collections.Generic;

namespace CES.CoreApi.Receipt_Main.UI.WPF.ViewModel
{
    public class NewDocumentToDownloadViewModel : ViewModelBase
    {

        private string _id;
        private string _docType;
        private string _start;
        private string _end;
        private DocumentType _selectedDocumentTypeValue;
        private string _selectedRangeTypeValue;
        public NewDocumentToDownloadViewModel()
        {
            DocumentTypeList = DocumentTypeHelper.GetDocumenTypes();
            RangeTypeList = new List<string>();
            RangeTypeList.Add("Permanente");
            RangeTypeList.Add("Temporal");

            SelectedDocumentTypeValue = DocumentTypeList[0];
            SelectedRangeTypeValue = RangeTypeList[0];
        }

        public string ID
        {
            get { return _id; }
            set
            {
                _id = value;
                NotifyPropertyChanged();
            }
        }
        public string DocType
        {
            get { return _docType; }
            set
            {
                _docType = value;
                NotifyPropertyChanged();
            }
        }
        public string Start
        {
            get { return _start; }
            set
            {
                _start = value;
                NotifyPropertyChanged();
            }
        }

        public string End
        {
            get { return _end; }
            set
            {
                _end = value;
                NotifyPropertyChanged();
            }
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

        public string SelectedRangeTypeValue
        {
            get { return _selectedRangeTypeValue; }
            set
            {
                _selectedRangeTypeValue = value;
                NotifyPropertyChanged();
            }
        }

        public IList<DocumentType> DocumentTypeList { get; }
        public IList<string> RangeTypeList { get; }
    }
}
