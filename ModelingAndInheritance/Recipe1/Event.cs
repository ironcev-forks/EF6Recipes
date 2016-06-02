using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelingAndInheritance.Recipe1
{
    public class Event
    {
        public Event()
        {
            Organizers = new HashSet<Organizer>();
        }
        public int EventId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Organizer> Organizers { get; set; }
    }
}
