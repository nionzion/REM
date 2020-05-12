using REM.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REM
{
    public class Apartment : IEstate
    {
        public string ID { get; set ; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Street { get; set; }
        public string StreetNumber { get; set; }
        public double SquareArea { get; set; }
        public string Floor { get; set; }
        public double Rent { get; set; }
        public double Rooms { get; set; }
        public bool? Balcony { get; set; }
        public bool? Kitchen { get; set; }

        public EstateAgent Agent { get; set; }

        public string FullStreet => $"{Street} {StreetNumber}"; 

    }
}
