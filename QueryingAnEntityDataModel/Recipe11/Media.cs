using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryingAnEntityDataModel.Recipe11
{
    public class Media
    {
        public int MediaId { get; set; }
        public string Title { get; set; }
    }
    public class Article : Media
    {
    }
    public class Picture : Media
    {
    }
    public class Video : Media
    {
    }
}
