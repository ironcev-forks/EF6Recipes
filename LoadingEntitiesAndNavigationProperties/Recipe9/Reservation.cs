using System;
using System.Collections.Generic;

namespace LoadingEntitiesAndNavigationProperties.Recipe9
{
    public class Reservation
    {
        public int ReservationId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string ContactName { get; set; }
        public int RoomId { get; set; }
        public virtual Room Room { get; set; }
    }
}