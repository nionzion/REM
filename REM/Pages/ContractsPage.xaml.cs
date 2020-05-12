using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net.Sockets;
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
    /// Interaktionslogik für ContractsPage.xaml
    /// </summary>
    public partial class ContractsPage : BasePage<ContractsPageViewModel>
    {
        public ContractsPage()
        {
            InitializeComponent();

            GuiState.OnPersonsChanged += GuiState_OnPersonsChanged;

            Persons.ItemsSource = DbAccess.Shared.FetchPersons();

        }

        private void GuiState_OnPersonsChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Persons.ItemsSource = DbAccess.Shared.FetchPersons();
        }

        private void Continue_Click(object sender, RoutedEventArgs e)
        {
            new CreatePersonDialog().ShowDialog();
        }
    }
}
