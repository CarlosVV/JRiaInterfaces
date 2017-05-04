using CES.CoreApi.Receipt_Main.Model.Services;
using CES.CoreApi.Receipt_Main.UI.WPF.Domain;
using CES.CoreApi.Receipt_Main.UI.WPF.View;
using CES.CoreApi.Receipt_Main.UI.WPF.ViewModel;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CES.CoreApi.Receipt_Main.UI.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IView
    {
        private readonly IUserService _userservice;
        public MainWindow(IUserService userservice)
        {
            _userservice = userservice;
            InitializeComponent();

            DataContext = new MainWindowViewModel();

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

        private async void MenuPopupButton_OnClick(object sender, RoutedEventArgs e)
        {
            var user = _userservice.GetAllUsers().FirstOrDefault();
            var sampleMessageDialog = new SampleMessageDialog
            {
                Message = { Text = $"{((ButtonBase)sender).Content} {user.Name}" }
            };

            await DialogHost.Show(sampleMessageDialog, "RootDialog");
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
