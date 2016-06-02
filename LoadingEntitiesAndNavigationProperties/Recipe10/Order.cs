using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadingEntitiesAndNavigationProperties.Recipe10
{
    public class Order
    {
        public Order()
        {
            OrderItems = new HashSet<OrderItem>();
        }
        public int OrderId { get; set; }
        public System.DateTime OrderDate { get; set; }
        public string CustomerName { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
