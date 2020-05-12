using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REM
{
    public class PurchaseContract : IContract
    {
        public string ContractNo { get; set; }
        public DateTime Date { get; set; }
        public string Place { get; set; }
        public IEstate Estate { get; set; }
        public Person Person { get; set; }
        public int Installments { get; set; }
        public double InterestRate { get; set; }
    }
}
