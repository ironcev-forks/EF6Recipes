using System.Collections.Generic;
namespace LoadingEntitiesAndNavigationProperties.Recipe12
{
    public class Patient
    {
        public Patient()
        {
            Appointments = new HashSet<Appointment>();
        }
        public int PatientId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; }
    }
}