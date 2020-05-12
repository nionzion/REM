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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace REM
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public object Page;
        private bool isOpen = true;
        public MainWindow()
        {

            var resizer = new WindowResizer(this);
            InitializeComponent();
            Page = PageHost.CurrentPage;
        }

        private void Minimize(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Maximize(object sender, RoutedEventArgs e)
        {
            WindowState ^= WindowState.Maximized;
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            Close();
        }

        public void ToggleButtonClick(object sender, RoutedEventArgs e)
        {
            if (isOpen)
            {
                CloseMenu();

            }
            else
            {
                OpenMenu();
            }
        }

        private void OpenMenu()
        {
            var sb = new Storyboard();
            var animation = new ThicknessAnimation
            {
                Duration = new Duration(TimeSpan.FromSeconds(0.4)),
                From = new Thickness(-ToggleMenu.ActualWidth, 0, 0, 0),
                To = new Thickness(0),
                DecelerationRatio = 0.2f
            };

            Storyboard.SetTargetProperty(animation, new PropertyPath("Margin"));
            sb.Children.Add(animation);
            sb.Begin(ToggleMenu);
            isOpen = true;
            //Overlay.Visibility = Visibility.Visible;
        }
        private void CloseMenu()
        {
            var sb = new Storyboard();
            var animation = new ThicknessAnimation
            {
                Duration = new Duration(TimeSpan.FromSeconds(0.4)),
                From = new Thickness(0),
                To = new Thickness(-ToggleMenu.ActualWidth, 0, 0, 0),
                DecelerationRatio = 0.2f
            };

            Storyboard.SetTargetProperty(animation, new PropertyPath("Margin"));
            sb.Children.Add(animation);
            sb.Begin(ToggleMenu);
            isOpen = false;
            //Overlay.Visibility = Visibility.Collapsed;

        }
        private void MainWindow_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape && isOpen)
            {
                CloseMenu();
            }
        }



    }
}

