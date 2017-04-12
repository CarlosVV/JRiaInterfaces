using CES.CoreApi.Receipt_Main.Model.Services;
using CES.CoreApi.Receipt_Main.UI.WPF.Domain;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    public partial class MainWindow : Window
    {
        private readonly IUserService _user;
        public MainWindow(IUserService user)
        {
            _user = user;
            InitializeComponent();
        }

        private async void MenuPopupButton_OnClick(object sender, RoutedEventArgs e)
        {
            var user = _user.GetAllUsers().FirstOrDefault();
            var sampleMessageDialog = new SampleMessageDialog
            {
                Message = { Text = $"{((ButtonBase)sender).Content} {user.Name}" }
            };

            await DialogHost.Show(sampleMessageDialog, "RootDialog");
        }
    }
}
