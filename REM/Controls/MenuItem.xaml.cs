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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace REM
{
    /// <summary>
    /// Interaktionslogik für MenuItem.xaml
    /// </summary>
    public partial class MenuItem : UserControl
    {
        
        public MenuItem()
        {
            InitializeComponent();
        }

        private void MenuItem_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
        
             IoC.Get<ApplicationViewModel>().GoToPage((ApplicationPage)Tag);
            
        }
    }
}
