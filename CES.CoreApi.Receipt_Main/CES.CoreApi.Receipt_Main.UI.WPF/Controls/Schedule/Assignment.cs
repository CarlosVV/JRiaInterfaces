using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.UI.WPF.Controls.Schedule
{
    public class Assignment : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string person;
        private Shift shift;
        private Spot spot;
        public string Person
        {
            get { return person; }
            set
            {
                person = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Person"));
            }
        }

        public Shift Shift
        {
            get { return shift; }
            set
            {
                shift = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Shift"));
            }
        }

        public Spot Spot
        {
            get { return spot; }
            set
            {
                spot = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Spot"));
            }
        }
    }
}
