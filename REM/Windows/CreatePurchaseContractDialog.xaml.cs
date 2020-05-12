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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace REM
{
    /// <summary>
    /// Interaktionslogik für CreateContractDialog.xaml
    /// </summary>
    public partial class CreatePurchaseContractDialog : Window
    {
        public CreatePurchaseContractDialog()
        {
            InitializeComponent();
            Estate.ItemsSource = DbAccess.Shared.FetchEstates().Where(e => e is House);
            Person.ItemsSource = DbAccess.Shared.FetchPersons();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var contract = new PurchaseContract();
            contract.InterestRate = double.Parse(Interest.Text);
            contract.Date = Date.SelectedDate.GetValueOrDefault();
            contract.Installments = Installments.Value;
            contract.Estate = Estate.SelectedItem as IEstate;
            contract.Person = Person.SelectedItem as Person;
            contract.Place = Place.Text;

            var result = DbAccess.Shared.CreatePurchaseContract(contract);

            if (result)
            {
                this.DialogResult = true;

            }
        }
    }
}
