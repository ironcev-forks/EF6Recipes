using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadingEntitiesAndNavigationProperties.Recipe14
{
    public  class Client
    {
        public Client()
        {
            this.Invoices = new HashSet<Invoice>();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ClientId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Invoice> Invoices { get; set; }
    }
}
