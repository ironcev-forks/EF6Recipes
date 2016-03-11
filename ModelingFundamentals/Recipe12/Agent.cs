using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelingFundamentals.Recipe12
{
    [Table("Agents", Schema = "Chapter2")]
    public class Agent
    {
        public Agent()
        {
            this.Name = new Name();
            this.Address = new Address();
        }

        public int AgentId { get; set; }

        public Name Name { get; set; }
        public Address Address { get; set; }
    }
}
