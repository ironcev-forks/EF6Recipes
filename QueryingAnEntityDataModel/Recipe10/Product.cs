using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryingAnEntityDataModel.Recipe10
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public virtual TopSelling TopSelling { get; set; }
    }
}
