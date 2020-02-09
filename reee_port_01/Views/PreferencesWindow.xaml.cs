﻿using System;
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
using System.Windows.Shapes;

namespace reee_port_01
{
    /// <summary>
    /// Interaction logic for PreferencesWindow.xaml
    /// </summary>
    public partial class PreferencesWindow : Window
    {

        public PreferencesWindow()
        {
            InitializeComponent();
            DataContext = (ReeeportSettings)Application.Current.MainWindow.DataContext;

            PreferencesWindow pw = this;
            pw.Topmost = true;
        }

    }
}
