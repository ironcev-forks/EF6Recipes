using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelingFundamentals.Recipe11
{
    public class EFContext : DbContext
    {
        public EFContext() : base("EFconnectionString") { }
        public DbSet<Location> Locations { get; set; }
       
    }
}
