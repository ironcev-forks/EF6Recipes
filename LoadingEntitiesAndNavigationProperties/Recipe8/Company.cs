using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadingEntitiesAndNavigationProperties.Recipe8
{
    public class Company
    {
        public Company()
        {
            Departments = new HashSet<Department>();
        }
        public int CompanyId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Department> Departments { get; set; }
    }
}
