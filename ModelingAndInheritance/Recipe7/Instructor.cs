using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelingAndInheritance.Recipe7
{
    public class Instructor:Staff
    {
        public int InstructorId { get; set; }
        public decimal? Salary { get; set; }
    }
}
