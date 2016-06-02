using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadingEntitiesAndNavigationProperties.Recipe5
{
    public class Section
    {
        public Section()
        {
            Students = new HashSet<Student>();
        }
        public int SectionId { get; set; }
        public int CourseId { get; set; }
        public virtual Course Course { get; set; }
        public int InstructorId { get; set; }
        public virtual Instructor Instructor { get; set; }
        public virtual ICollection<Student> Students { get; set; }
    }
}
