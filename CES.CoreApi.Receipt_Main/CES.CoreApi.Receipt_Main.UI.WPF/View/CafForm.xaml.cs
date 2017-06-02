using CES.CoreApi.Receipt_Main.Domain.Core.Documents;
using CES.CoreApi.Receipt_Main.Domain.Core.Services;
using CES.CoreApi.Receipt_Main.UI.WPF.ViewModel;
using MvvmDialogs;
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
    /// Interaction logic for CafForm.xaml
    /// </summary>
    public partial class CafForm : UserControl
    {
        public CafForm(IStoreService storeService, IDialogService dialogService, systblApp_CoreAPI_Caf obj = null)
        {
            InitializeComponent();
            DataContext = new CafFormViewModel(storeService, dialogService, obj);
        }        
    }
}
