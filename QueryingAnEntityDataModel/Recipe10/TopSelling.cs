using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryingAnEntityDataModel.Recipe10
{
    public class TopSelling
    {
        [Key]
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public int? Rating { get; set; }
        public virtual Product Product { get; set; }
    }
}
