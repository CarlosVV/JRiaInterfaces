using CES.CoreApi.Receipt_Main.UI.WPF.Model;
using CES.CoreApi.Receipt_Main.UI.WPF.ViewModel;
using CES.CoreApi.Receipt_Main.UI.WPF.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections;
using CES.CoreApi.Receipt_Main.UI.WPF.Controls.Schedule;

namespace CES.CoreApi.Receipt_Main.UI.WPF.View
{
    /// <summary>
    /// Interaction logic for StaffAssignment.xaml
    /// </summary>
    public partial class StaffAssignment : UserControl
    {
        ListBox dragSource = null;

        public StaffAssignment()
        {
            InitializeComponent();

            List<string> months = new List<string> { "Enero", "Febrero", "Marzp", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Setiembre", "Octubre", "Noviembre", "Diciembre" };
            cboMonth.ItemsSource = months;

            for (int i = -50; i < 50; i++)
            {
                cboYear.Items.Add(DateTime.Today.AddYears(i).Year);
            }

            cboMonth.SelectedItem = months[DateTime.Today.Month - 1];
            cboYear.SelectedItem = DateTime.Today.Year;

            cboMonth.SelectionChanged += (o, e) => RefreshCalendar();
            cboYear.SelectionChanged += (o, e) => RefreshCalendar();

            DataContext = new StaffAssignmentViewModel();
        }

        private void RefreshCalendar()
        {
            if (cboYear.SelectedItem == null) return;
            if (cboMonth.SelectedItem == null) return;

            int year = (int)cboYear.SelectedItem;

            int month = cboMonth.SelectedIndex + 1;

            DateTime targetDate = new DateTime(year, month, 1);

            RiaCalendar.BuildCalendar(targetDate);
        }

        private void Calendar_DayChanged(object sender, DayChangedEventArgs e)
        {
            //save the text edits to persistant storage
            Console.WriteLine("Actualizando Empleado " + e.Day.Notes);
        }

        private void ListBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var parent = (ListBox)sender;
            dragSource = parent;
            object data = GetDataFromListBox(dragSource, e.GetPosition(parent));

            if (data != null)
            {
                DragDrop.DoDragDrop(parent, data, DragDropEffects.Move);
            }
        }

        private void Calendar_Drop(object sender, DragEventArgs e)
        {
            var parent = (ScheduleControl)sender;
            var sourceConfiguracion = e.Data.GetData("CES.CoreApi.Receipt_Main.UI.WPF.Controls.Schedule.ScheduleConfiguration") as ScheduleConfiguration;
            var sourcePersona = e.Data.GetData("System.Windows.Controls.ListBoxItem") as ListBoxItem;
            var cuadroDia = (ArrayList)GetDataFromCalendar(parent, e.GetPosition(parent));

            if (cuadroDia == null) return;

            if (sourceConfiguracion != null)
            {
                var configuracionName = sourceConfiguracion.Name;
                var listViewAsignaciones = ((ListView)(cuadroDia[3]));

                var asignaciones = new ObservableCollection<Assignment>();

                var shifts = sourceConfiguracion.Shifts;
                var spots = sourceConfiguracion.Spots;

                var panel = (StackPanel)cuadroDia[0];
                var day = (Day)panel.DataContext;
                day.Configuration = sourceConfiguracion;
                
                foreach (var sh in shifts)
                {
                    foreach (var sp in spots)
                    {
                        asignaciones.Add(new Assignment { Shift = sh, Spot = sp });
                    }
                }

                day.Assignments = asignaciones;

                var i = 0;
                var grid = new GridView();//((GridView)listViewAsignaciones.FindName("GridViewControl"));
                var column = new GridViewColumn();
                column.Header = "Turno";
                column.DisplayMemberBinding = new Binding($"[{i}]");
                grid.Columns.Add(column);
                
                foreach (var sp in spots)
                {
                    i++;
                    column = new GridViewColumn();
                    column.Header = sp.Name;
                    column.DisplayMemberBinding = new Binding($"[{i}]");
                    grid.Columns.Add(column);
                }

                var list = new List<string[]>();
                foreach(var r in shifts)
                {
                    string[] row = new string[spots.Count + 1];
                    row[0] = r.Name;
                    i = 1;                   
                    foreach(var c in spots)
                    {
                        row[i] = "";
                        i++;
                    }
                    list.Add(row);
                }

                
                listViewAsignaciones.View = grid;
                listViewAsignaciones.ItemsSource = list;
                //listViewAsignaciones.UpdateLayout();
            }

            if (sourcePersona != null)
            {
                var person = sourcePersona.Name;
                ((ListBox)(cuadroDia[2])).Items.Add(new Assignment { Person = person });
            }
            
            //((TextBox)(data2[1])).Text = ((TextBox)(data2[1])).Text  + person + ",";           

        }
        private static object GetDataFromCalendar(ScheduleControl source, Point point)
        {
            UIElement element = source.InputHitTest(point) as UIElement;
            var border = element as Border;

            if (border == null) return null;

            var child = border.Child;
            var dockpanel = child as DockPanel;

            if (dockpanel == null) return null;

            var children = dockpanel.Children as UIElementCollection;
            var stackpanel = children[0] as StackPanel;
            var textbox = children[1] as TextBox;
            var list = children[2] as ListBox;
            var listView = children[3] as ListView;
            return new ArrayList { stackpanel, textbox, list, listView };
        }
        #region GetDataFromListBox(ListBox,Point)
        private static object GetDataFromListBox(ListBox source, Point point)
        {
            UIElement element = source.InputHitTest(point) as UIElement;
            if (element != null)
            {
                object data = DependencyProperty.UnsetValue;
                while (data == DependencyProperty.UnsetValue)
                {
                    data = source.ItemContainerGenerator.ItemFromContainer(element);

                    if (data == DependencyProperty.UnsetValue)
                    {
                        element = VisualTreeHelper.GetParent(element) as UIElement;
                    }

                    if (element == source)
                    {
                        return null;
                    }
                }

                if (data != DependencyProperty.UnsetValue)
                {
                    return data;
                }
            }

            return null;
        }
        #endregion
    }
}
