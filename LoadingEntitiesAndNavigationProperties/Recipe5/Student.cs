using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadingEntitiesAndNavigationProperties.Recipe5
{
    public class Student
    {
        public Student()
        {
            Sections = new HashSet<Section>();
        }
        public int StudentId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Section> Sections { get; set; }
    }
}
