using System.Collections.Generic;
namespace LoadingEntitiesAndNavigationProperties.Recipe9
{
    public class Room
    {
        public Room()
        {
            Reservations = new HashSet<Reservation>();
        }
        public int RoomId { get; set; }
        public decimal Rate { get; set; }
        public int HotelId { get; set; }
        public virtual Hotel Hotel { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}