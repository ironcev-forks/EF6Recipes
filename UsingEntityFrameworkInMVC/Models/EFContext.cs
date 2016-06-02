using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsingEntityFrameworkInMVC.Models
{
    public class EFContext:DbContext
    {
        public EFContext() : base("name=EFconnectionString") { }
        public DbSet<Category> Categories { get; set; }
        public DbSet<App> Apps { get; set; }
        public DbSet<Developer> Developers { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().ToTable("Chapter4.Categories");
            modelBuilder.Entity<Developer>().ToTable("Chapter4.Developers");
            modelBuilder.Entity<App>().ToTable("Chapter4.Apps");
            base.OnModelCreating(modelBuilder);
        }
    }
}
