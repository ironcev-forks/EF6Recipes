using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadingEntitiesAndNavigationProperties.Recipe1
{
    public class Customer
    {
        public Customer()
        {
            CustomerEmails = new HashSet<CustomerEmail>();
        }
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public int CustomerTypeId { get; set; }
        public virtual CustomerType CustomerType { get; set; }
        public virtual ICollection<CustomerEmail> CustomerEmails { get; set; }
    }
}
