using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.UI.WPF.Controls.Schedule
{
    public class Day : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private DateTime date;
        private string notes;
        private bool enabled;
        private bool isTargetMonth;
        private bool isToday;
        private ObservableCollection<Assignment> assignments;
        private ScheduleConfiguration configuration;

        public bool IsToday
        {
            get { return isToday; }
            set
            {
                isToday = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsToday"));
            }
        }

        public bool IsTargetMonth
        {
            get { return isTargetMonth; }
            set
            {
                isTargetMonth = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsTargetMonth"));
            }
        }

        public bool IsEnabled
        {
            get { return enabled; }
            set
            {
                enabled = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsEnabled"));
            }
        }

        public string Notes
        {
            get { return notes; }
            set
            {
                notes = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Notes"));
            }
        }

        public DateTime Date
        {
            get { return date; }
            set
            {
                date = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Date"));
            }
        }

        public ScheduleConfiguration Configuration
        {
            get { return configuration; }
            set
            {
                configuration = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Configuration"));
            }
        }

        public ObservableCollection<Assignment> Assignments
        {
            get { return assignments; }
            set
            {
                assignments = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Assignments"));
            }
        }
    }
}
