using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadingEntitiesAndNavigationProperties.Recipe9
{
    public class Hotel
    {
        public Hotel()
        {
            Rooms = new HashSet<Room>();
        }
        public int HotelId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Room> Rooms { get; set; }
    }
}
