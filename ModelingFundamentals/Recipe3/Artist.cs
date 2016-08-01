using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelingFundamentals.Recipe3
{
    [Table("Artists", Schema = "Chapter2")]
    public class Artist
    {
        public Artist()
        {
            this.Albums = new HashSet<Album>();
        }
        public int ArtistId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public virtual ICollection<Album> Albums { get; set; }
    }
}
