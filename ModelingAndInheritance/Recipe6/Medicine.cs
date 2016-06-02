using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelingAndInheritance.Recipe6
{
    public class Medicine:Drug
    {
        public decimal? TargetPrice { get; set; }
        public DateTime AcceptedDate { get; set; }
    }
}
