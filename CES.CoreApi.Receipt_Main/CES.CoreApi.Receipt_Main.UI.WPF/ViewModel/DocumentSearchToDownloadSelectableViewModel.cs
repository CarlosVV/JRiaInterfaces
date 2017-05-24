using CES.CoreApi.Receipt_Main.UI.WPF.Helpers;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.UI.WPF.ViewModel
{
    public class DocumentSearchToDownloadSelectableViewModel : BindableBase
    {
        private bool _isSelected;
        private int _id;
        private string _type;
        private string _start;
        private string _end;
        private string _current;
        private bool _isDynamic;
        private bool _isViewEditVisible;
        private string _downloadStatus;
        private int _numberOfFolios;
        private int _processed;
        private int _pending;
        private string _detail;

        public int ID
        {
            get { return _id; }
            set
            {
                SetProperty(ref _id, value);
            }
        }
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                SetProperty(ref _isSelected, value);
            }
        }

        public string DocType
        {
            get { return _type; }
            set
            {
                SetProperty(ref _type, value);
            }
        }

        public string Start
        {
            get { return _start; }
            set
            {
                SetProperty(ref _start, value);
            }
        }

        public string End
        {
            get { return _end; }
            set
            {
                SetProperty(ref _end, value);
            }
        }
        public string Current
        {
            get { return _current; }
            set
            {
                SetProperty(ref _current, value);
            }
        }

        public bool IsDynamic
        {
            get { return _isDynamic; }
            set
            {
                SetProperty(ref _isDynamic, value);
            }
        }

        public bool IsViewEditVisible
        {
            get { return _isViewEditVisible; }
            set
            {
                SetProperty(ref _isViewEditVisible, value);
            }
        }
        public int NumberOfFolios
        {
            get { return _numberOfFolios; }
            set
            {
                SetProperty(ref _numberOfFolios, value);
            }
        }
        public int Processed
        {
            get { return _processed; }
            set
            {
                SetProperty(ref _processed, value);
            }
        }
        public int Pending
        {
            get { return _pending; }
            set
            {
                SetProperty(ref _pending, value);
            }
        }
        public string DownloadStatus
        {
            get { return _downloadStatus; }
            set
            {
                SetProperty(ref _downloadStatus, value);
            }
        }
        public string Detail
        {
            get { return _detail; }
            set
            {
                SetProperty(ref _detail, value);
            }
        }
    }
}
