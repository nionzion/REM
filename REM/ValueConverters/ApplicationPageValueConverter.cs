using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REM
{
    public class ApplicationPageValueConverter : BaseValueConverter<ApplicationPageValueConverter>
    {
        public override object Convert(object value, Type targetType = null, object parameter = null, CultureInfo culture = null)
        {

            switch ((ApplicationPage)value)
            {
                case ApplicationPage.EstateAgents:
                    return new EstateAgentsPage();
                case ApplicationPage.Configuration:
                    return new ConfigurationPage();
                case ApplicationPage.Estates:
                    return new EstatesPage();
                case ApplicationPage.Contracts:
                    return new ContractsPage();

                default:
                    Debugger.Break();
                    return null;
            }
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
