using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelingFundamentals.Recipe8
{
    public class EFContext : DbContext
    {
        public EFContext() : base("EFconnectionString") { }
        public DbSet<Business> Businesses { get; set; }
        public object Employees { get; internal set; }
    }
}
