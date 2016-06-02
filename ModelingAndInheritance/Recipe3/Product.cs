using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelingAndInheritance.Recipe3
{
    [Table("Products", Schema = "Chapter6")]
    public class Product
    {
        public Product()
        {
            RelatedProducts = new HashSet<Product>();
            OtherRelatedProducts = new HashSet<Product>();
        }
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        //自己(本product)的关联Products
        public virtual ICollection<Product> RelatedProducts { get; set; }
        //与自己(本product)关联的Products
        public virtual ICollection<Product> OtherRelatedProducts { get; set; }


    }
}
