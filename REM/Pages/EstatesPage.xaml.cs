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
    /// Interaktionslogik für EstatesPage.xaml
    /// </summary>
    public partial class EstatesPage : BasePage<EstatesPageViewModel>
    {


        public EstatesPage()
        {
            InitializeComponent();
            GuiState.OnEstatesChanged += GuiState_OnEstatesChanged;
            
            Items.ItemsSource = DbAccess.Shared.FetchEstates();
        }

        private void GuiState_OnEstatesChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Items.ItemsSource = DbAccess.Shared.FetchEstates();
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

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var isValid = DbAccess.Shared.ValidateAgent(UserBox.Text, PasswordBox.Password);
            if (isValid)
            {
                VerificationBox.Visibility = Visibility.Collapsed;
                ScrollView.Visibility = Visibility.Visible;
                CreateApartmentButton.Visibility = Visibility.Visible;
                CreateHouseButton.Visibility = Visibility.Visible;

            }
            else
            {
                MessageBox.Show("Wrong credentials. :(");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new CreateEditApartmentDialog().ShowDialog();
        }

        private void CreateHouseButton_Click(object sender, RoutedEventArgs e)
        {
            new CreateEditHouseDialog().ShowDialog();

        }
    }
}
