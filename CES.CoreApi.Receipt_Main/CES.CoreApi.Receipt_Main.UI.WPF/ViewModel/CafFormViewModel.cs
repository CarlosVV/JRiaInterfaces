using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.UI.WPF.ViewModel
{
    public class CafFormViewModel : ViewModelBase
    {
        private int _folioCurrentNumber;

        public int FolioCurrentNumber
        {
            get
            {
                return _folioCurrentNumber;
            }

            set 
            {
                _folioCurrentNumber = value;
                NotifiyPropertyChanged();
            }
        }
    }
}
