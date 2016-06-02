using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadingEntitiesAndNavigationProperties.Recipe1
{
    public class CustomerType
    {
        public CustomerType()
        {
            Customers = new HashSet<Customer>();
        }
        public int CustomerTypeId { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Customer> Customers { get; set; }
    }
}
