using REM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REM
{
    public interface IContract
    {
        string ContractNo { get; set; }
        DateTime Date { get; set; }
        string Place { get; set; }
        IEstate Estate { get; set; }
        Person Person { get; set; }
    }
}
