using ModelingFundamentals.Recipe10;
using ModelingFundamentals.Recipe8;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelingFundamentals.Recipe12
{
    /// <summary>
    /// 2-12 创建、修改和映射复合类型
    /// </summary>
    public class Recipe12Program
    {
       
        public static void Run()
        {
            using (var context = new EFContext())
            {
                var name1 = new Name { FirstName = "Robin", LastName = "Rosen" };
                var name2 = new Name { FirstName = "Alex", LastName = "St. James" };
                var address1 = new Address
                {
                    AddressLine1 = "510 N. Grant",
                    AddressLine2 = "Apt. 8",
                    City = "Raytown",
                    State = "MO",
                    ZIPCode = "64133"
                };
                var address2 = new Address
                {
                    AddressLine1 = "222 Baker St.",
                    AddressLine2 = "Apt.22B",
                    City = "Raytown",
                    State = "MO",
                    ZIPCode = "64133"
                };
                context.Agents.Add(new Agent { Name = name1, Address = address1 });
                context.Agents.Add(new Agent { Name = name2, Address = address2 });
                context.SaveChanges();
            }

            using (var context = new EFContext())
            {
                Console.WriteLine("Agents");
                foreach (var agent in context.Agents)
                {
                    Console.WriteLine("{0} {1}", agent.Name.FirstName, agent.Name.LastName);
                    Console.WriteLine("{0}", agent.Address.AddressLine1);
                    Console.WriteLine("{0}", agent.Address.AddressLine2);
                    Console.WriteLine("{0}, {1} {2}", agent.Address.City,
                                       agent.Address.State, agent.Address.ZIPCode);
                    Console.WriteLine();
                }
            }
        }
    }
}
