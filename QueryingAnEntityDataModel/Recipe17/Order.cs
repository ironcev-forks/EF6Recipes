using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryingAnEntityDataModel.Recipe17
{
    public class Order
    {
        public int OrderId { get; set; }
        public Decimal Amount { get; set; }
        public int AccountID { get; set; }
        public string ShipCity { get; set; }
        public string ShipState { get; set; }
        public virtual Account Account { get; set; }

    }
}
