using Prism.Mvvm;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace CES.CoreApi.Receipt_Main.UI.WPF.ViewModel
{

    public class CafResultSelectableViewModel : BindableBase
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
        private DateTime _authorizationDate;
        private bool _disabled;

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (_isSelected == value) return;
                _isSelected = value;
                SetProperty(ref _isSelected, value);
            }
        }        

        public string Id
        {
            get { return _id; }
            set
            {
                if (_id == value) return;
                _id = value;
                SetProperty(ref _id, value);
            }
        }

        public string Type
        {
            get { return _type; }
            set
            {
                if (_type == value) return;
                _type = value;
                SetProperty(ref _type, value);
            }
        }
        public string Store
        {
            get { return _store; }
            set
            {
                if (_store == value) return;
                _store = value;
                SetProperty(ref _store, value);
            }
        }

        public string Date
        {
            get { return _date; }
            set
            {
                if (_date == value) return;
                _date = value;
                SetProperty(ref _date, value);
            }
        }
        public string Start
        {
            get { return _start; }
            set
            {
                if (_start == value) return;
                _start = value;
                SetProperty(ref _start, value);
            }
        }

        public string End
        {
            get { return _end; }
            set
            {
                if (_end == value) return;
                _end = value;
                SetProperty(ref _end, value);
            }
        }

        public string Current
        {
            get { return _current; }
            set
            {
                if (_current == value) return;
                _current = value;
                SetProperty(ref _current, value);
            }
        }
        public bool Disabled
        {
            get
            {
                return _disabled;
            }

            set
            {
                if (_disabled == value) return;
                _disabled = value;
                SetProperty(ref _disabled, value);
            }
        }

        public DateTime AuthorizationDate
        {
            get { return _authorizationDate; }
            set
            {
                if (_authorizationDate == value) return;
                _authorizationDate = value;
                SetProperty(ref _authorizationDate, value);
            }
        }
        
        public bool IsViewEditVisible
        {
            get { return _isViewEditVisible; }
            set
            {
                if (_isViewEditVisible == value) return;
                _isViewEditVisible = value;
                SetProperty(ref _isViewEditVisible, value);
            }
        }       
    }
}
