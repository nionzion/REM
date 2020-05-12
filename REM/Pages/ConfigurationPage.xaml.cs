using Newtonsoft.Json;
using Npgsql;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaktionslogik für ConfigurationPage.xaml
    /// </summary>
    public partial class ConfigurationPage : BasePage<ConfigurationPageViewModel>
    {
        public ConfigurationPage()
        {
            InitializeComponent();
            using (StreamReader file = new StreamReader(Environment.CurrentDirectory + "/database.json"))
            {
                string json = file.ReadToEnd();
                var parameters = JsonConvert.DeserializeObject<DatabaseParameters>(json);
                ServerBox.Text = parameters.Server;
                PortBox.Text = parameters.Port;
                DatabaseBox.Text = parameters.Database;
                UserBox.Text = parameters.User;
                PasswordBox.Password = parameters.Password;
            }
            
        }

        private void ConfigurationWizard_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                var inputControls = WpfHelpers.FindVisualChildren<InputTextBox>(this);
                foreach (var ctrl in inputControls)
                    ctrl.Unfocus();
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

            var parameters = new DatabaseParameters();
            parameters.Server = ServerBox.Text;
            parameters.Port = PortBox.Text;
            parameters.Database = DatabaseBox.Text;
            parameters.User = UserBox.Text;
            parameters.Password = PasswordBox.Password;

            using (StreamWriter file = File.CreateText(Environment.CurrentDirectory + "/database.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, parameters);
            }

            MessageBox.Show("Settings were saved.");
        }

        private void TestConnection_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string connstring = $"Server={ServerBox.Text};Port={PortBox.Text};User Id={UserBox.Text};Password={PasswordBox.Password};Database={DatabaseBox.Text};";
                NpgsqlConnection conn = new NpgsqlConnection(connstring);
                conn.Open();
                conn.Close();
                MessageBox.Show("Successfully established connection!");
            }
            catch (Exception xcpt)
            {
                MessageBox.Show(xcpt.Message);
            }
        }
    }
}
