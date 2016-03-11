using System.ComponentModel.DataAnnotations.Schema;

namespace ModelingFundamentals.Recipe11
{
    [Table("Parks", Schema = "Chapter2")]
    public class Park:Location
    {
        public string Name { get; set; }
        public int OfficeLocationId { get; set; }

        public virtual Location Office { get; set; }
    }
}