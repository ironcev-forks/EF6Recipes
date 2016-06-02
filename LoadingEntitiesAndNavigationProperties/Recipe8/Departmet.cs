using System.Collections;
using System.Collections.Generic;

namespace LoadingEntitiesAndNavigationProperties.Recipe8
{
    public class Department
    {
        public Department()
        {
            Employees = new HashSet<Employee>();
        }
        public int DepartmentId { get; set; }
        public string Name { get; set; }
        public int CompanyId { get; set; }
        public virtual Company Company { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }
}