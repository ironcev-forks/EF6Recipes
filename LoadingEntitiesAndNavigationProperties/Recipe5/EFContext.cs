using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadingEntitiesAndNavigationProperties.Recipe5
{
    public class EFContext : DbContext
    {
        public EFContext() : base("EFconnectionString") { }
     
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Section> Sections { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().ToTable("Chapter5.Students");
            modelBuilder.Entity<Course>().ToTable("Chapter5.Courses");
            modelBuilder.Entity<Instructor>().ToTable("Chapter5.Instructors");
            modelBuilder.Entity<Section>().ToTable("Chapter5.Sections");
            base.OnModelCreating(modelBuilder);
        }
    }
}
