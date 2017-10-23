using CES.CoreApi.Receipt_Main.UI.WPF.Controls.Schedule;
using CES.CoreApi.Receipt_Main.UI.WPF.Model;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace CES.CoreApi.Receipt_Main.UI.WPF.ViewModel
{
    public class StaffAssignmentViewModel : ViewModelBase
    {
        public ObservableCollection<ScheduleConfiguration> Configurations { get; set; }
        public StaffAssignmentViewModel()
        {
            CreateConfigurations();
        }

        void CreateConfigurations()
        {
            var shifts = new ObservableCollection<Shift>
            {
                new Shift {Name="T1" },
                new Shift {Name="T2" },
                new Shift {Name="T3" },
            };

            var spots = new ObservableCollection<Spot>
            {
                new Spot {Name = "P1" },
                new Spot {Name = "P2" },
                new Spot {Name = "P3" },
            };

            Configurations = new ObservableCollection<ScheduleConfiguration>() {
                new ScheduleConfiguration { Name = "Configuracion 1", Shifts = shifts, Spots = spots },
                new ScheduleConfiguration { Name = "Configuracion 2", Shifts = shifts, Spots = spots },
                new ScheduleConfiguration { Name = "Configuracion 3", Shifts = shifts, Spots = spots },
                new ScheduleConfiguration { Name = "Configuracion 4", Shifts = shifts, Spots = spots },
                new ScheduleConfiguration { Name = "Configuracion 5", Shifts = shifts, Spots = spots },
            };
        }
    }
}
