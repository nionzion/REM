﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REM
{
    public class House : IEstate
    {
        public string ID { get; set ; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Street { get; set; }
        public string StreetNumber { get; set; }
        public double SquareArea { get; set; }
        public int Floors { get; set; }
        public double Price { get; set; }
        public bool Garden { get; set; }

        public EstateAgent Agent { get; set; }
        public string FullStreet => $"{Street} {StreetNumber}";
        public string Type => nameof(House);

    }
}
