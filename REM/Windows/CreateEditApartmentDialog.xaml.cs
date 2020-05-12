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
    /// Interaktionslogik für CreateEditApartmentDialog.xaml
    /// </summary>
    public partial class CreateEditApartmentDialog : Window
    {
        private bool IsEdit { get; set; } = false;

        private Apartment Apartment { get; set; }

        public CreateEditApartmentDialog(Apartment apartment = null)
        {
            InitializeComponent();
            
            var agents = DbAccess.Shared.FetchAgents();
            AgentBox.ItemsSource = agents;

            if (apartment != null)
            {
                IsEdit = true;
                Apartment = apartment;
                
                AgentBox.SelectedItem = agents.Where(a => a.ID == apartment.Agent.ID).FirstOrDefault();
                DeleteButton.Visibility = Visibility.Visible;
                StreetBox.Text = apartment.Street != null ? apartment.Street : string.Empty;
                StreetNoBox.Text = apartment.StreetNumber != null ? apartment.StreetNumber : string.Empty;
                CityBox.Text = apartment.City != null ? apartment.City : string.Empty;
                PostalCodeBox.Text = apartment.PostalCode != null ? apartment.PostalCode : string.Empty;
                Area.Text = apartment.SquareArea != null ? apartment.SquareArea.ToString() : string.Empty;
                Floor.Text = apartment.Floor != null ? apartment.Floor : string.Empty;
                Rent.Text = apartment.Rent != null ? apartment.Rent.ToString() : string.Empty;
                Rooms.Text = apartment.Rooms != null ? apartment.Rooms.ToString() : string.Empty;
                BalconyBox.SelectedIndex = apartment.Balcony != null ? Convert.ToInt32(!apartment.Balcony.Value) : -1;
                KitchenBox.SelectedIndex = apartment.Kitchen != null ? Convert.ToInt32(!apartment.Kitchen.Value) : -1;
            }
            else
            {
                DeleteButton.Visibility = Visibility.Collapsed;
            }

        }

        private void Continue_Click(object sender, RoutedEventArgs e)
        {
            if (IsEdit)
            {
                var result = DbAccess.Shared.UpdateApartment(new Apartment
                {
                    City = CityBox.Text,
                    PostalCode = PostalCodeBox.Text,
                    Street = StreetBox.Text,
                    StreetNumber = StreetNoBox.Text,
                    SquareArea = double.Parse(Area.Text),
                    Rent = double.Parse(Rent.Text),
                    Floor = Floor.Text,
                    Rooms = double.Parse(Rooms.Text),
                    Balcony = !Convert.ToBoolean(BalconyBox.SelectedIndex),
                    Kitchen = !Convert.ToBoolean(KitchenBox.SelectedIndex),
                    Agent = AgentBox.SelectedItem as EstateAgent

            }, Apartment.ID) ;
                if (result)
                {
                    this.DialogResult = true;
                }
            }
            else
            {
                var result = DbAccess.Shared.CreateApartment(new Apartment
                {
                    City = CityBox.Text,
                    PostalCode = PostalCodeBox.Text,
                    Street = StreetBox.Text,
                    StreetNumber = StreetNoBox.Text,
                    SquareArea = double.Parse(Area.Text),
                    Rent = double.Parse(Rent.Text),
                    Floor = Floor.Text,
                    Rooms = double.Parse(Rooms.Text),
                    Balcony = !Convert.ToBoolean(BalconyBox.SelectedIndex),
                    Kitchen = !Convert.ToBoolean(KitchenBox.SelectedIndex),
                    Agent = AgentBox.SelectedItem as EstateAgent
            });
                if (result)
                {
                    this.DialogResult = true;
                }
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var result = DbAccess.Shared.DeleteApartment(Apartment);
            if (result)
            {
                this.DialogResult = true;
            }
        }

    }
}
