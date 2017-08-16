using CES.CoreApi.Receipt_Main.UI.WPF.Model;
using CES.CoreApi.Receipt_Main.UI.WPF.ViewModel;
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

namespace CES.CoreApi.Receipt_Main.UI.WPF.View
{
    /// <summary>
    /// Interaction logic for StaffAssignment.xaml
    /// </summary>
    public partial class StaffAssignment : UserControl
    {
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

            DataContext = new StaffAssignmentViewModel();
        }
        public bool IsEditing { get; set; }
        public bool IsDragging { get; set; }
        private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (IsEditing) return;

            //var row = UIHelpers.TryFindFromPoint<DataGridRow>((UIElement)sender, e.GetPosition(shareGrid));
            //if (row == null || row.IsEditing) return;

            //set flag that indicates we're capturing mouse movements
            IsDragging = true;
            //DraggedItem = (Employee)row.Item;
        }

        private void OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (!IsDragging || IsEditing)
            {
                return;
            }

            //get the target item
            var targetItem = (Employee)dataGrid.SelectedItem;

            if (targetItem == null || !ReferenceEquals(DraggedItem, targetItem))
            {
                //remove the source from the list
                //ShareList.Remove(DraggedItem);

                //get target index
                //var targetIndex = ShareList.IndexOf(targetItem);

                //move source at the target's location
                //ShareList.Insert(targetIndex, DraggedItem);

                //select the dropped item
                //shareGrid.SelectedItem = DraggedItem;
            }

            //reset
            ResetDragDrop();
        }

        private void ResetDragDrop()
        {
            IsDragging = false;
            popup1.IsOpen = false;
            //shareGrid.IsReadOnly = false;
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (!IsDragging || e.LeftButton != MouseButtonState.Pressed) return;

            //display the popup if it hasn't been opened yet
            if (!popup1.IsOpen)
            {
                //switch to read-only mode
                //shareGrid.IsReadOnly = true;

                //make sure the popup is visible
                popup1.IsOpen = true;
            }

            Size popupSize = new Size(popup1.ActualWidth, popup1.ActualHeight);
            popup1.PlacementRectangle = new Rect(e.GetPosition(this), popupSize);

            //make sure the row under the grid is being selected
            //Point position = e.GetPosition(shareGrid);
            //var row = UIHelpers.TryFindFromPoint<DataGridRow>(shareGrid, position);
            //if (row != null) shareGrid.SelectedItem = row.Item;
        }
    }
}
