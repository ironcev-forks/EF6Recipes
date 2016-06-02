using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ModelingAndInheritance.Recipe1
{
    public class Organizer
    {
        public Organizer()
        {
            Events = new HashSet<Event>();
        }
        public int OrganizerId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Event> Events { get; set; }
    }
}