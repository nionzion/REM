using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REM.Interfaces
{
    public interface IEstate
    {
        string ID { get; set; }
        string City { get; set; }
        string PostalCode { get; set; }
        string Street { get; set; }
        string StreetNumber { get; set; }
        double SquareArea { get; set; }
    }
}
