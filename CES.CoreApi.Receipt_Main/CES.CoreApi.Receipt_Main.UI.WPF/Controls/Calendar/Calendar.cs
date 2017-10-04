using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace CES.CoreApi.Receipt_Main.UI.WPF.Controls.Calendar
{
    public class Calendar : Control
    {
        public ObservableCollection<Day> Days { get; set; }
        public ObservableCollection<string> DayNames { get; set; }
        public static readonly DependencyProperty CurrentDateProperty = DependencyProperty.Register("CurrentDate", typeof(DateTime), typeof(Calendar));

        public event EventHandler<DayChangedEventArgs> DayChanged;

        public DateTime CurrentDate
        {
            get { return (DateTime)GetValue(CurrentDateProperty); }
            set { SetValue(CurrentDateProperty, value); }
        }

        static Calendar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Calendar), new FrameworkPropertyMetadata(typeof(Calendar)));
        }

        public Calendar()
        {
            DataContext = this;
            CurrentDate = DateTime.Today;
            DayNames = new ObservableCollection<string> { "Lunes", "Martes", "Miercoles", "Jueves", "Viernes", "Sabado", "Domingo"};

            Days = new ObservableCollection<Day>();
            BuildCalendar(DateTime.Today);
        }

        public void BuildCalendar(DateTime targetDate)
        {
            Days.Clear();

            //Calculate when the first day of the month is and work out an 
            //offset so we can fill in any boxes before that.
            DateTime d = new DateTime(targetDate.Year, targetDate.Month, 1);
            int offset = DayOfWeekNumber(d.DayOfWeek) - 1;
            if (offset != 1) d = d.AddDays(-offset);

            //Show 6 weeks each with 7 days = 42
            for (int box = 1; box <= 42; box++)
            {
                Day day = new Day { Date = d, Enabled = true, IsTargetMonth = targetDate.Month == d.Month };
                day.PropertyChanged += Day_Changed;
                day.IsToday = d == DateTime.Today;
                Days.Add(day);
                d = d.AddDays(1);
            }
        }

        public void AddEmployee( Day day, string name)
        {
            day.Notes = name;
            Console.WriteLine("adding " + name);
            DayChanged(this, new DayChangedEventArgs(day));
        }

        private void Day_Changed(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != "Notes") return;
            if (DayChanged == null) return;

            DayChanged(this, new DayChangedEventArgs(sender as Day));
        }

        private static int DayOfWeekNumber(DayOfWeek dow)
        {
            var day = (int)dow;
            if (day == 0) day = 7;
            return int.Parse( day.ToString("D"));
        }
    }

}
