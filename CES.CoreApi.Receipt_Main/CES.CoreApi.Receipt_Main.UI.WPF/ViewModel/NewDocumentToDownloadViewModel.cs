using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.UI.WPF.ViewModel
{
    public class NewDocumentToDownloadViewModel : INotifyPropertyChanged
    {
        private int _id;
        private string _docType;
        private string _start;
        private string _end;
        public int ID
        {
            get { return _id; }
            set
            {
                _id = value;
                NotifyPropertyChanged();
            }
        }
        public string DocType
        {
            get { return _docType; }
            set
            {
                _docType = value;
                NotifyPropertyChanged();
            }
        }
        public string Start
        {
            get { return _start; }
            set
            {
                _start = value;
                NotifyPropertyChanged();
            }
        }

        public string End
        {
            get { return _end; }
            set
            {
                _end = value;
                NotifyPropertyChanged();
            }
        }       


        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
