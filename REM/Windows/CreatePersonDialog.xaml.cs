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
    /// Interaktionslogik für CreatePerson.xaml
    /// </summary>
    public partial class CreatePersonDialog : Window
    {
        public CreatePersonDialog()
        {
            InitializeComponent();
        }

        private void Continue_Click(object sender, RoutedEventArgs e)
        {

            var result = DbAccess.Shared.CreatePerson(new Person {
                FirstName = FirstNameBox.Text,
                LastName = LastNameBox.Text,
                Address = AddressBox.Text
            });

            if (result)
            {
                this.DialogResult = true;
            }
        }
    }
}
