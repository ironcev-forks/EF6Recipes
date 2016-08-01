using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelingAndInheritance.Recipe8
{
    public class EFContext : DbContext
    {
        public EFContext() : base("EFconnectionString") { }

        public DbSet<Employee> Employees { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Employee>()
                        .HasKey(e => e.EmployeeId)
                        .Property(e => e.EmployeeId)
                        .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<Employee>()
                        .Map<SalariedEmployee>(m => m.Requires("EmployeeType").HasValue("salaried"))
                        .Map<HourlyEmployee>(m => m.Requires("EmployeeType").HasValue("hourly"))
                        .Map<CommissionedEmployee>(m => m.Requires("EmployeeType").HasValue("commissioned"))
                        .ToTable("Employees", "chapter6");

            base.OnModelCreating(modelBuilder);
        }
    }
}
