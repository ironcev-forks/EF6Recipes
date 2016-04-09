using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryingAnEntityDataModel.Recipe7
{
    public class EFContext : DbContext
    {
        public EFContext() : base("EFconnectionString") { }
     
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Bid> Bids { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Job>().ToTable("Chapter3.Jobs");
            modelBuilder.Entity<Bid>().ToTable("Chapter3.Bids");
            base.OnModelCreating(modelBuilder);
        }
    }
}
