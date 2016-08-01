using System.ComponentModel.DataAnnotations.Schema;

namespace ModelingFundamentals.Recipe8
{
    [Table("Businesses", Schema ="Chapter2")]
    public class Business
    {
        public int BusinessID { get;protected set; }
        public string Name { get; set; }
        public string LicenseNumber { get; set; }
    }
}