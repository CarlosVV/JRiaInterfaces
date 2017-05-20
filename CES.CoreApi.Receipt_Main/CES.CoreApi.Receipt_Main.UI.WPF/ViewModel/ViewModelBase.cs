using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace CES.CoreApi.Receipt_Main.UI.WPF.ViewModel
{
    class ViewModelBase : INotifyPropertyChanged
    {
        public ViewModelBase()
        {
        }
        public void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
