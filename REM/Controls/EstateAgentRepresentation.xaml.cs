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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace REM
{
    /// <summary>
    /// Interaktionslogik für EstateAgentRepresentation.xaml
    /// </summary>
    public partial class EstateAgentRepresentation : UserControl
    {
        public EstateAgentRepresentation()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new CreateEditAgentDialog(DataContext as EstateAgent);
            var result = dialog.ShowDialog();
        }
    }
}
