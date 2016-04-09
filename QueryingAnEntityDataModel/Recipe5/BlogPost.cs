using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryingAnEntityDataModel.Recipe5
{
    public class BlogPost
    {
        public BlogPost()
        {
            Comments = new HashSet<Comment>();
        }
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
