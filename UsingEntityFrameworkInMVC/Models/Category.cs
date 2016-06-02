using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsingEntityFrameworkInMVC.Models
{
    public class Category
    {
        public Category()
        {
            Apps = new HashSet<App>();
        }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<App> Apps { get; set; }
    }
}
