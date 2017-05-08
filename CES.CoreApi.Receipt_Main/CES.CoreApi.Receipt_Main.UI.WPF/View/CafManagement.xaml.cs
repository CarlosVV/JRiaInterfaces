﻿using CES.CoreApi.Receipt_Main.UI.WPF.ViewModel;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for CafManagement.xaml
    /// </summary>
    public partial class CafManagement : UserControl
    {
        public CafManagement()
        {
            InitializeComponent();
            DataContext = new CafManagementViewModel();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
        }
    }
}