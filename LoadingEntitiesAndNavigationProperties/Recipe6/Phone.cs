using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace LoadingEntitiesAndNavigationProperties.Recipe6
{
    public class Phone
    {
        public Phone()
        {
            Locations = new HashSet<Location>();
        }
        public int PhoneId { get; set; }
        public string Number { get; set; }
        public virtual ICollection<Location> Locations { get; set; }
    }
}