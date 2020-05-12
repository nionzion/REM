using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REM
{
    public class MenuItemViewModel : BaseViewModel
    {
        public IconType Icon { get; set; }
        public string Text { get; set; }
        public bool IsSelected { get; set; }
        public ApplicationPage ApplicationPage { get; set; }
    }
}