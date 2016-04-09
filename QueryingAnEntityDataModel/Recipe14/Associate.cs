using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryingAnEntityDataModel.Recipe14
{
    public class Associate
    {
        public Associate()
        {
            AssociateSalaries = new HashSet<AssociateSalary>();
        }
        public int AssociateID { get; set; }
        public string Name { get; set; }
        public virtual ICollection<AssociateSalary> AssociateSalaries { get; set; }
    }
}
