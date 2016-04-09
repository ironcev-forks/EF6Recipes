using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryingAnEntityDataModel.Recipe9
{
    public class EFContext : DbContext
    {
        public EFContext() : base("EFconnectionString") { }
     
        public DbSet<Worker> Workers { get; set; }
        public DbSet<Accident> Accidents { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Worker>().ToTable("Chapter3.Workers");
            modelBuilder.Entity<Accident>().ToTable("Chapter3.Accidents");
            base.OnModelCreating(modelBuilder);
        }
    }
}
