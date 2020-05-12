using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace REM
{
    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Dispatcher.UnhandledException += OnDispatcherUnhandledException;

            IoC.Setup();

            if(!File.Exists(Environment.CurrentDirectory + "/database.json"))
            {
                Current.MainWindow = new ConfigurationWizard();
            }
            else
            {
                using (StreamReader file = new StreamReader(Environment.CurrentDirectory + "/database.json"))
                {
                    string json = file.ReadToEnd();
                    var parameters = JsonConvert.DeserializeObject<DatabaseParameters>(json);
                    if (string.IsNullOrEmpty(parameters.Server) || string.IsNullOrEmpty(parameters.Port) || string.IsNullOrEmpty(parameters.Database) || string.IsNullOrEmpty(parameters.User) || string.IsNullOrEmpty(parameters.Password))
                    {
                        Current.MainWindow = new ConfigurationWizard();
                    }
                    else
                    {
                        Current.MainWindow = new MainWindow();
                    }
                }
            }

            Current.MainWindow.Show();
        }

        void OnDispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            string errorMessage = string.Format("An unhandled exception occurred: {0}", e.Exception.Message);
            MessageBox.Show(errorMessage);
            e.Handled = true;
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            DbAccess.Shared.Connection.Close();
        }
    }
}
