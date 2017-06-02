using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CES.CoreApi.Receipt_Main.UI.WPF.ViewModel
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public ViewModelBase()
        {
        }
        public virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
