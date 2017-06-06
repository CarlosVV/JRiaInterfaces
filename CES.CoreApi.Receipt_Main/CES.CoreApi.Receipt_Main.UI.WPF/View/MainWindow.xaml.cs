using CES.CoreApi.Receipt_Main.Domain.Core.Services;
using CES.CoreApi.Receipt_Main.UI.WPF.ViewModel;
using MvvmDialogs;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace CES.CoreApi.Receipt_Main.UI.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IView
    {
        private readonly IUserService _userservice;
        private readonly IDocumentService _documentService;
        public MainWindow(IUserService userservice, IDocumentService documentService, ITaxEntityService taxEntityService, 
            ITaxAddressService taxAddressService, ISequenceService sequenceService, IStoreService storeService, IDialogService dialogService)
        {
            _userservice = userservice;
            _documentService = documentService;

            InitializeComponent();

            DataContext = new MainWindowViewModel(documentService, taxEntityService, taxAddressService, sequenceService, storeService, dialogService);

            Task.Factory.StartNew(() =>
            {
                Thread.Sleep(2500);
            }).ContinueWith(t =>
            {
                //note you can use the message queue from any thread, but just for the demo here we 
                //need to get the message queue from the snackbar, so need to be on the dispatcher
                MainSnackbar.MessageQueue.Enqueue("Bienvenido al Sistema de Gestión de Documentos e Impresiones");
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        public IViewModel ViewModel
        {
            get
            {
                return DataContext as IViewModel;
            }
            set
            {
                DataContext = value;
            }
        }

        private void UIElement_OnPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //until we had a StaysOpen glag to Drawer, this will help with scroll bars
            var dependencyObject = Mouse.Captured as DependencyObject;
            while (dependencyObject != null)
            {
                if (dependencyObject is ScrollBar) return;
                dependencyObject = VisualTreeHelper.GetParent(dependencyObject);
            }

            MenuToggleButton.IsChecked = false;
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}
