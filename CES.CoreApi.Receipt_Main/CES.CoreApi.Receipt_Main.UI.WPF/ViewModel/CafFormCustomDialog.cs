using CES.CoreApi.Receipt_Main.UI.WPF.View;
using MvvmDialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace CES.CoreApi.Receipt_Main.UI.WPF.ViewModel
{
    public class CafFormCustomDialog : IWindow
    {
        private readonly CafFormViewModel dialog;
        object IWindow.DataContext
        {
            get { return dialog.DataContext; }
            set { dialog.DataContext = value; }
        }

        bool? IWindow.DialogResult
        {
            get { return dialog.DialogResult; }
            set { dialog.DialogResult = value; }
        }

        ContentControl IWindow.Owner
        {
            get { return dialog.Owner; }
            set { dialog.Owner = (Window)value; }
        }

        bool? IWindow.ShowDialog()
        {
            return dialog.ShowDialog();
        }

        void IWindow.Show()
        {
            dialog.Show();
        }
    }
}
