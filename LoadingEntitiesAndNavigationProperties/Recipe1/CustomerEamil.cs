using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadingEntitiesAndNavigationProperties.Recipe1
{
    public class CustomerEmail
    {
        public int CustomerEmailId { get; set; }
        public int CustomerId { get; set; }
        public string Email { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
