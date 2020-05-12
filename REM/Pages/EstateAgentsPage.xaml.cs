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
    /// Interaktionslogik für EstateAgentsPage.xaml
    /// </summary>
    public partial class EstateAgentsPage : BasePage<EstateAgentsPageViewModel>
    {
        public EstateAgentsPage()
        {
            InitializeComponent();
            Items.ItemsSource = DbAccess.Shared.FetchAgents();
            GuiState.OnAgentsChanged += GuiState_OnAgentsChanged;
            PasswordBox.Password = "{G=Vn}&-Bc6p8GD{";

        }

        private void GuiState_OnAgentsChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Items.ItemsSource = DbAccess.Shared.FetchAgents();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new CreateEditAgentDialog();
            dialog.ShowDialog();
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if(PasswordBox.Password == "{G=Vn}&-Bc6p8GD{")
            {
                VerificationBox.Visibility = Visibility.Collapsed;
                ScrollView.Visibility = Visibility.Visible;
                CreateButton.Visibility = Visibility.Visible;
            }
            else
            {
                MessageBox.Show("Wrong password!");
            }
        }


        private void PasswordBox_GotFocus(object sender, RoutedEventArgs e)
        {
            PasswordTitleTextBlock.Foreground = new SolidColorBrush(Colors.Black);
            PasswordBottomBorder.Background = new SolidColorBrush(Colors.Black);

            var animation = new DoubleAnimation
            {
                Duration = new Duration(TimeSpan.FromSeconds(0.1)),
                From = 12,
                To = 12.5,
                DecelerationRatio = 0.4f
            };

            PasswordTitleTextBlock.BeginAnimation(TextBlock.FontSizeProperty, animation);
        }

        private void PasswordBox_LostFocus(object sender, RoutedEventArgs e)
        {
            PasswordTitleTextBlock.Foreground = new SolidColorBrush(Colors.DarkGray);
            PasswordBottomBorder.Background = new SolidColorBrush(Colors.Gray);

            var animation = new DoubleAnimation
            {
                Duration = new Duration(TimeSpan.FromSeconds(0.1)),
                From = 12.5,
                To = 12,
                DecelerationRatio = 0.4f
            };

            PasswordTitleTextBlock.BeginAnimation(TextBlock.FontSizeProperty, animation);
        }
    }
}
