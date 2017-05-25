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
        private int _folioStartNumber;
        private int _folioEndNumber;

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

        public int FolioStartNumber
        {
            get
            {
                return _folioStartNumber;
            }

            set
            {
                _folioStartNumber = value;
                NotifiyPropertyChanged();
            }
        }

        public int FolioEndNumber
        {
            get
            {
                return _folioEndNumber;
            }

            set
            {
                _folioEndNumber = value;
                NotifiyPropertyChanged();
            }
        }
    }
}
