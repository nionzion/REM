using System.Collections.Generic;
using System.Windows.Controls;

namespace REM
{
    /// <summary>
    /// Interaktionslogik für SideMenu.xaml
    /// </summary>
    public partial class SideMenu : UserControl
    {
        public SideMenu()
        {
            InitializeComponent();

            DataContext = new SideMenuViewModel
            {
                MenuItemCategories = new List<MenuItemCategoryViewModel>
                {
                    new MenuItemCategoryViewModel
                    {
                        Title = "Main menu",
                        MenuItems = new List<MenuItemViewModel>
                        {
                            new MenuItemViewModel
                            {
                                Icon = IconType.Agent,
                                Text = "Estate Agents",
                                ApplicationPage = ApplicationPage.EstateAgents
                            },
                            new MenuItemViewModel
                            {
                                Icon = IconType.Home,
                                Text = "Estates",
                                ApplicationPage = ApplicationPage.Estates
                            },
                            new MenuItemViewModel
                            {
                                Icon = IconType.Book,
                                Text = "Contracts",
                                ApplicationPage = ApplicationPage.Contracts
                            },
                            new MenuItemViewModel
                            {
                                Icon = IconType.Settings,
                                Text = "Database Configuration",
                                ApplicationPage = ApplicationPage.Configuration
                            }
                        }
                    }
                }
            };
        }
    }
}
