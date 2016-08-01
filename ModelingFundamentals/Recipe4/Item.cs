using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ModelingFundamentals.Recipe4
{
    [Table("Items", Schema = "Chapter2")]
    public class Item
    {
        public Item()
        {
            OrderItems = new HashSet<OrderItem>();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public int SKU { get; set; }
        public string Description { get; set; }
        public decimal? Price { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}