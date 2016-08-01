using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelingFundamentals.Recipe1 { 

    /// <summary>
    /// 2-1 创建一个简单模型
    /// </summary>
public class Recipe1Program
    {
        public static void Run()
        {
            using (var context = new EF6RecipesContext())
            {
                var person = new Person
                {
                    FirstName = "Robert",
                    MiddleName = "Allen",
                    LastName = "Doe",
                    PhoneNumber = "867-5309"
                };
                context.People.Add(person);
                person = new Person
                {
                    FirstName = "John",
                    MiddleName = "K.",
                    LastName = "Smith",
                    PhoneNumber = "824-3031"
                };
                context.People.Add(person);
                person = new Person
                {
                    FirstName = "Billy",
                    MiddleName = "Albert",
                    LastName = "Minor",
                    PhoneNumber = "907-2212"
                };
                context.People.Add(person);
                person = new Person
                {
                    FirstName = "Kathy",
                    MiddleName = "Anne",
                    LastName = "Ryan",
                    PhoneNumber = "722-0038"
                };
                context.People.Add(person);

                context.SaveChanges();
            }

            using (var context = new EF6RecipesContext())
            {
                foreach (var person in context.People)
                {
                    System.Console.WriteLine("{0} {1} {2}, Phone: {3}",
                                              person.FirstName, person.MiddleName,
                                              person.LastName, person.PhoneNumber);
                }
            }

        }
    }
}