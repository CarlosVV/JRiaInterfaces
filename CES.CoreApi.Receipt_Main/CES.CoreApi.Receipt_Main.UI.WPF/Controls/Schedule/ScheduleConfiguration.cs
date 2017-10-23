using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.UI.WPF.Controls.Schedule
{
    public class ScheduleConfiguration : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string name;
        private ObservableCollection<Shift> shifts;
        private ObservableCollection<Spot> spots;

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Name"));
            }
        }
        public ObservableCollection<Shift> Shifts
        {
            get { return shifts; }
            set
            {
                shifts = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Shifts"));
            }
        }
        public ObservableCollection<Spot> Spots
        {
            get { return spots; }
            set
            {
                spots = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Spots"));
            }
        }
    }
}
