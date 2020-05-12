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
    /// Interaktionslogik für CreateEditHouseDialog.xaml
    /// </summary>
    public partial class CreateEditHouseDialog : Window
    {
        private bool IsEdit { get; set; } = false;

        private House House { get; set; }

        public CreateEditHouseDialog(House house = null)
        {
            InitializeComponent();
            
            var agents = DbAccess.Shared.FetchAgents();
            AgentBox.ItemsSource = agents;

            if (house != null)
            {
                IsEdit = true;
                House = house;
                AgentBox.SelectedItem = agents.Where(a => a.ID == house.Agent.ID).FirstOrDefault();
                DeleteButton.Visibility = Visibility.Visible;
                StreetBox.Text = house.Street != null ? house.Street : string.Empty;
                StreetNoBox.Text = house.StreetNumber != null ? house.StreetNumber : string.Empty;
                CityBox.Text = house.City != null ? house.City : string.Empty;
                PostalCodeBox.Text = house.PostalCode != null ? house.PostalCode : string.Empty;
                Area.Text = house.SquareArea != null ? house.SquareArea.ToString() : string.Empty;
                Floors.Text = house.Floors != null ? house.Floors.ToString() : string.Empty;
                Price.Text = house.Price != null ? house.Price.ToString() : string.Empty;
                GardenBox.SelectedIndex = house.Garden != null ? Convert.ToInt32(!house.Garden) : -1;
               
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
                var result = DbAccess.Shared.UpdateHouse(new House
                {
                    City = CityBox.Text,
                    PostalCode = PostalCodeBox.Text,
                    Street = StreetBox.Text,
                    StreetNumber = StreetNoBox.Text,
                    SquareArea = double.Parse(Area.Text),
                    Price = double.Parse(Price.Text),
                    Floors = int.Parse(Floors.Text),
                    Garden = !Convert.ToBoolean(GardenBox.SelectedIndex),
                    Agent = AgentBox.SelectedItem as EstateAgent

                }, House.ID);
                if (result)
                {
                    this.DialogResult = true;
                }
            }
            else
            {
                var result = DbAccess.Shared.CreateHouse(new House
                {
                    City = CityBox.Text,
                    PostalCode = PostalCodeBox.Text,
                    Street = StreetBox.Text,
                    StreetNumber = StreetNoBox.Text,
                    SquareArea = double.Parse(Area.Text),
                    Price = double.Parse(Price.Text),
                    Floors = int.Parse(Floors.Text),
                    Garden = !Convert.ToBoolean(GardenBox.SelectedIndex),
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
            var result = DbAccess.Shared.DeleteHouse(House);
            if (result)
            {
                this.DialogResult = true;
            }
        }
    }
}
