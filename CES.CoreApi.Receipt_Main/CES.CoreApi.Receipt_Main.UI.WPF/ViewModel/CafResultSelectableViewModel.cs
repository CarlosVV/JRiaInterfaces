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
        private string _store;
        private bool _isViewEditVisible;

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (_isSelected == value) return;
                _isSelected = value;
                NotifiyPropertyChanged();
            }
        }        

        public string Id
        {
            get { return _id; }
            set
            {
                if (_id == value) return;
                _id = value;
                NotifiyPropertyChanged();
            }
        }

        public string Type
        {
            get { return _type; }
            set
            {
                if (_type == value) return;
                _type = value;
                NotifiyPropertyChanged();
            }
        }
        public string Store
        {
            get { return _store; }
            set
            {
                if (_store == value) return;
                _store = value;
                NotifiyPropertyChanged();
            }
        }

        public string Date
        {
            get { return _date; }
            set
            {
                if (_date == value) return;
                _date = value;
                NotifiyPropertyChanged();
            }
        }
        public string Start
        {
            get { return _start; }
            set
            {
                if (_start == value) return;
                _start = value;
                NotifiyPropertyChanged();
            }
        }

        public string End
        {
            get { return _end; }
            set
            {
                if (_end == value) return;
                _end = value;
                NotifiyPropertyChanged();
            }
        }

        public string Current
        {
            get { return _current; }
            set
            {
                if (_current == value) return;
                _current = value;
                NotifiyPropertyChanged();
            }
        }

        public bool IsViewEditVisible
        {
            get { return _isViewEditVisible; }
            set
            {
                if (_isViewEditVisible == value) return;
                _isViewEditVisible = value;
                NotifiyPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void NotifiyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
