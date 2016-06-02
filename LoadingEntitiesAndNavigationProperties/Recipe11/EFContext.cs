using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadingEntitiesAndNavigationProperties.Recipe11
{
    public class EFContext : DbContext
    {
        public EFContext() : base("EFconnectionString") { }
     
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Contractor> Contractors { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Manager>().ToTable("Chapter5.Managers");
            modelBuilder.Entity<Project>().ToTable("Chapter5.Projects");
            modelBuilder.Entity<Contractor>().ToTable("Chapter5.Contractors");
            base.OnModelCreating(modelBuilder);
        }
    }
}
