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
        public AddEditRangesViewModel(IDocumentService documentService)
        {
            this.documentService = documentService;
        }
    }
}
