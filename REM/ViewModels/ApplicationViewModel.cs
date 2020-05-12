using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REM
{
    public class ApplicationViewModel : BaseViewModel
    {
        public ApplicationPage CurrentPage { get; private set; } = ApplicationPage.Configuration;

        public bool SideMenuVisible { get; set; } = true;

        public void GoToPage(ApplicationPage page)
        {
            CurrentPage = page;

        }
    }
}
