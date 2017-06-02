using CES.CoreApi.Receipt_Main.Domain.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.UI.WPF.ViewModel
{
    public class AddEditRangesViewModel : ViewModelBase
    {
        public IDocumentService documentService;
        private NewDocumentToDownloadViewModel _newDocumentToDownload;
        public AddEditRangesViewModel(NewDocumentToDownloadViewModel objectRange)
        {
            //this.documentService = documentService;
            _newDocumentToDownload = objectRange;
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
    }
}
