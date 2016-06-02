using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadingEntitiesAndNavigationProperties.Recipe5
{
    public class Course
    {
        public Course()
        {
            Sections = new HashSet<Section>();
        }
        public int CourseId { get; set; }
        public string Title { get; set; }
        public virtual ICollection<Section> Sections { get; set; }
    }
}
