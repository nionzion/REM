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
using System.Windows.Shapes;

namespace REM
{
    /// <summary>
    /// Interaktionslogik für CreateEditAgentDialog.xaml
    /// </summary>
    public partial class CreateEditAgentDialog : Window
    {
        private bool IsEdit { get; set; } = false;

        private EstateAgent EstateAgent { get; set; } = new EstateAgent();

        public CreateEditAgentDialog(EstateAgent agent = null)
        {
            InitializeComponent();

            if(agent != null)
            {
                IsEdit = true;
                EstateAgent = agent;
                DeleteButton.Visibility = Visibility.Visible;
                LoginBox.Text = agent.Login != null ? agent.Login : string.Empty;
                NameBox.Text = agent.Name != null ? agent.Name : string.Empty;
                AddressBox.Text = agent.Address != null ? agent.Address: string.Empty;
                PasswordBox.Password = agent.Password != null ? agent.Password : string.Empty;
            }  
            else
            {
                DeleteButton.Visibility = Visibility.Collapsed;
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
        private void Continue_Click(object sender, RoutedEventArgs e)
        {
            if (IsEdit)
            {
                var result = DbAccess.Shared.UpdateAgent(new EstateAgent { Address = AddressBox.Text, Login = LoginBox.Text, Name = NameBox.Text, Password = PasswordBox.Password }, EstateAgent.Login);
                if (result)
                {
                    this.DialogResult = true;

                }
            }
            else
            {
                var result = DbAccess.Shared.CreateAgent(new EstateAgent { Address = AddressBox.Text, Login = LoginBox.Text, Name = NameBox.Text, Password = PasswordBox.Password });
                if (result)
                {
                    this.DialogResult = true;

                }
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var result = DbAccess.Shared.DeleteAgent(EstateAgent);
            if (result)
            {
                this.DialogResult = true;
            }
        }
    }
}
