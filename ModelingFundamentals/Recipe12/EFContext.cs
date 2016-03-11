using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelingFundamentals.Recipe12
{
    public class EFContext : DbContext
    {
        public EFContext() : base("EFconnectionString") { }
        public DbSet<Agent> Agents { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Agent>()
            .Map(m => {
                m.Property(e => e.Name.FirstName).HasColumnName("FirstName");
                m.Property(e => e.Name.LastName).HasColumnName("LastName");
                m.Property(e => e.Address.AddressLine1).HasColumnName("AddressLine1");
                m.Property(e => e.Address.AddressLine2).HasColumnName("AddressLine2");
                m.Property(e => e.Address.City).HasColumnName("City");
                m.Property(e => e.Address.State).HasColumnName("State");
                m.Property(e => e.Address.ZIPCode).HasColumnName("ZIPCode");
            });
        }
    }
}
