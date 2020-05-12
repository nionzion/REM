using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REM
{
    public class MenuItemCategoryViewModel : BaseViewModel
    {
        public List<MenuItemViewModel> MenuItems { get; set; }
        public string Title { get; set; }

    }
}
