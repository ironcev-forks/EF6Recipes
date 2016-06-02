using System;

namespace LoadingEntitiesAndNavigationProperties.Recipe12
{
    public class Appointment
    {
        public int AppointmentId { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public DateTime Date { get; set; }
     
        public decimal Fee { get; set; }
       
        public string Reason { get; set; }
        public Doctor Doctor { get; set; }
        public Patient Patient { get; set; }
    }
}