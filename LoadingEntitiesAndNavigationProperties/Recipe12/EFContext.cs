using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadingEntitiesAndNavigationProperties.Recipe12
{
    public class EFContext : DbContext
    {
        public EFContext() : base("EFconnectionString") { }
     
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Doctor>().ToTable("Chapter5.Doctors");
            modelBuilder.Entity<Patient>().ToTable("Chapter5.Patient");
            modelBuilder.Entity<Appointment>().ToTable("Chapter5.Appointments");
            base.OnModelCreating(modelBuilder);
        }
    }
}
