using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Interaktionslogik für ApartmentView.xaml
    /// </summary>
    public partial class EstateView : UserControl
    {

        public EstateView()
        {
            InitializeComponent();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(DataContext is Apartment)
            {
                new CreateEditApartmentDialog(DataContext as Apartment).ShowDialog();
            }
            else
            {
                new CreateEditHouseDialog(DataContext as House).ShowDialog();

            }
        }
    }
}
