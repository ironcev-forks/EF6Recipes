using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ModelingAndInheritance.Recipe7
{
    public class EFContext : DbContext
    {
        public EFContext() : base("EFconnectionString") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

        }
      

    }
}
