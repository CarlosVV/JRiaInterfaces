using CES.CoreApi.Receipt_Main.UI.WPF.Controls.Calendar;
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

namespace CES.CoreApi.Receipt_Main.UI.WPF.View
{
    /// <summary>
    /// Interaction logic for StaffAssignment.xaml
    /// </summary>
    public partial class StaffAssignment : UserControl
    {
        ListBox dragSource = null;
        public static readonly DependencyProperty DraggedItemProperty =
           DependencyProperty.Register("DraggedItem", typeof(Employee), typeof(MainWindow));
        public Employee DraggedItem
        {
            get { return (Employee)GetValue(DraggedItemProperty); }
            set { SetValue(DraggedItemProperty, value); }
        }

        public StaffAssignment()
        {
            InitializeComponent();
           
            List<string> months = new List<string> { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
            cboMonth.ItemsSource = months;

            for (int i = -50; i < 50; i++)
            {
                cboYear.Items.Add(DateTime.Today.AddYears(i).Year);
            }

            cboMonth.SelectedItem = months.FirstOrDefault(w => w == DateTime.Today.ToString("MMMM"));
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
            var parent = (Controls.Calendar.Calendar)sender;
            object data = e.Data.GetData(typeof(string));
            //((IList)dragSource.ItemsSource).Remove(data);
            //e.poi
            //parent.da .Items.Add(data);
            object data2 = GetDataFromCalendar(parent, e.GetPosition(parent));
        }
        private static object GetDataFromCalendar(Controls.Calendar.Calendar source, Point point)
        {
            UIElement element = source.InputHitTest(point) as UIElement;
            if (element != null)
            {
                object data = DependencyProperty.UnsetValue;
                while (data == DependencyProperty.UnsetValue)
                {
                    //data = source.ItemContainerGenerator.ItemFromContainer(element);

                    //if (data == DependencyProperty.UnsetValue)
                    //{
                    //    element = VisualTreeHelper.GetParent(element) as UIElement;
                    //}

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
