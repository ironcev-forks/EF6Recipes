using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryingAnEntityDataModel.Recipe14
{
    public class AssociateSalary
    {
        [Key]
        public int SalaryID { get; set; }
        public decimal Salary { get; set; }
        public DateTime SalaryDate { get; set; }
        [ForeignKey("Associate")]
        public int AssociateID { get; set; }
        public Associate Associate { get; set; }
    }
}
