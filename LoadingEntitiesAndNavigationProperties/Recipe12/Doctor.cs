using System.Collections.Generic;
namespace LoadingEntitiesAndNavigationProperties.Recipe12
{
    public class Doctor
    {
        public Doctor()
        {
            Appointments = new HashSet<Appointment>();
        }
        public int DoctorId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; }
    }
}