using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelingFundamentals.Recipe4
{
    public class OrderItem
    {
        [Key,Column(Order=0)]
        public int OrderId { get; set; }
        [Key, Column(Order = 1)]
        public int SKU { get; set; }
        public int Count { get; set; }
        public virtual Order Order { get; set; }
        public virtual  Item Item { get; set; }
    }
}