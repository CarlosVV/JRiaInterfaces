using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace CES.CoreApi.Receipt_Main.UI.WPF.ViewModel
{

    public class CafResultSelectableViewModel : INotifyPropertyChanged
    {
        private bool _isSelected;       
        private string _id;
        private string _type;
        private string _date;
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

        public string Id
        {
            get { return _id; }
            set
            {
                if (_id == value) return;
                _id = value;
                OnPropertyChanged();
            }
        }

        public string Type
        {
            get { return _type; }
            set
            {
                if (_type == value) return;
                _type = value;
                OnPropertyChanged();
            }
        }

        public string Date
        {
            get { return _date; }
            set
            {
                if (_date == value) return;
                _date = value;
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
