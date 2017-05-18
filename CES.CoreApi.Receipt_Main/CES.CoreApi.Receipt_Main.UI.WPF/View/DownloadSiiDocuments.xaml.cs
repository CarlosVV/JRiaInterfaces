using CES.CoreApi.Receipt_Main.Model.Services;
using CES.CoreApi.Receipt_Main.UI.WPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CES.CoreApi.Receipt_Main.UI.WPF.View
{
    /// <summary>
    /// Interaction logic for DownloadSiiDocuments.xaml
    /// </summary>
    public partial class DownloadSiiDocuments : UserControl
    {
        private readonly IDocumentService _documentService;
        public DownloadSiiDocuments(IDocumentService documentService)
        {
            InitializeComponent();
            _documentService = documentService;
            var confirm = (Func<string, string, bool>)((msg, capt) => MessageBox.Show(msg, capt, MessageBoxButton.YesNo) == MessageBoxResult.Yes);
            var viewModel = new DownloadSiiDocumentsViewModel(confirm, _documentService);
            DataContext = viewModel;
        }
    }
}
