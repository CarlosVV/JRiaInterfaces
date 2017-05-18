using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.UI.WPF.ViewModel
{
    public class DocumentSearchToDownloadSelectableViewModel : INotifyPropertyChanged
    {
        private bool _isSelected;
        private string _type;
        private string _start;
        private string _end;
        private string _current;
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (_isSelected == value) return;
                _isSelected = value;
                OnPropertyChanged();
            }
        }

        public string DocType
        {
            get { return _type; }
            set
            {
                if (_type == value) return;
                _type = value;
                OnPropertyChanged();
            }
        }

        public string Start
        {
            get { return _start; }
            set
            {
                if (_start == value) return;
                _start = value;
                OnPropertyChanged();
            }
        }

        public string End
        {
            get { return _end; }
            set
            {
                if (_end == value) return;
                _end = value;
                OnPropertyChanged();
            }
        }
        public string Current
        {
            get { return _end; }
            set
            {
                if (_current == value) return;
                _current = value;
                OnPropertyChanged();
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
