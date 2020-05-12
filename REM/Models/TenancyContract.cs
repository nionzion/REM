using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REM
{
    public class TenancyContract : IContract
    {
        public string ContractNo { get; set; }
        public DateTime Date { get; set; }
        public string Place { get; set; }
        public IEstate Estate { get; set; }
        public Person Person { get; set; }
        public DateTime StartDate { get; set; }
        public int Duration { get; set; }
        public double AdditionalCosts { get; set; }

    }
}
