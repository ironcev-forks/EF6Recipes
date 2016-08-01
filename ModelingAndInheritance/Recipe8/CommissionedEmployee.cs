using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelingAndInheritance.Recipe8
{
    public class CommissionedEmployee : SalariedEmployee
    {
        public decimal? Commission { get; set; }
    }
}
