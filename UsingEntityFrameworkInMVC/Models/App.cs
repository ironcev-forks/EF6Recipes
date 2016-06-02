using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsingEntityFrameworkInMVC.Models
{
    public class App
    {
        public int AppId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public int DeveloperId { get; set; }
        public virtual Developer Developer { get; set; }
    }
}
