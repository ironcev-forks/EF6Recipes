using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadingEntitiesAndNavigationProperties.Recipe8
{
    public class EFContext : DbContext
    {
        public EFContext() : base("EFconnectionString") { }
     
        public DbSet<Company> Company { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>().ToTable("Chapter5.Departments");
            modelBuilder.Entity<Company>().ToTable("Chapter5.Companys");
            modelBuilder.Entity<Employee>().ToTable("Chapter5.Employees");
            base.OnModelCreating(modelBuilder);
        }
    }
}
