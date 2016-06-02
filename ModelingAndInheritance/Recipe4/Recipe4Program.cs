using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ModelingAndInheritance.Recipe4
{
    /// <summary>
    /// 6-4  使用TPH建模自引用关系
    /// </summary>
    public class Recipe4Program
    {
        public static void Run()
        {
            using (var context = new EFContext())
            {
                var teacher = new Teacher
                {
                    Name = "Susan Smith",
                    School = "Custer Baker Middle School"
                };
                var firefighter = new Firefighter
                {
                    Name = "Joel Clark",
                    FireStation = "Midtown"
                };
                var retired = new Retired
                {
                    Name = "Joan Collins",
                    FullTimeHobby = "Scapbooking"
                };
                context.Persons.Add(teacher);
                context.Persons.Add(firefighter);
                context.Persons.Add(retired);
                context.SaveChanges();
                //我们为每个派生类型创建了一个实例，并构造了一些英雄关系。
                //我们有一位教师，它是消防员心中的英雄，一位退休的职工，他是这位教师心中的英雄。
                //当我们把消防员设为退休职工的英雄时，我们便引入了一个循环。
                //此时，实体框架会产生一个运行时异常(DbUpdatexception),因为它无法确定合适的顺序来将数据插入到数据库中。
                //在代码中，我们采用在设置英雄关系之前调用SaveChanges()方法，来绕开这个问题。
                //一旦数据提交到数据库，实体框架会把数据库中产生的键值带回到对象图中，这样我们就不会为更新关系图而付出什么代价。
                //当然，这些更新最终仍要调用SaveChages()方法来保存。
                firefighter.Hero = teacher;
                teacher.Hero = retired;
                retired.Hero = firefighter;
                context.SaveChanges();
            }

            using (var context = new EFContext())
            {
                foreach (var person in context.Persons)
                {
                    if (person.Hero != null)
                        Console.WriteLine("\n{0}, Hero is: {1}", person.Name,
                                            person.Hero.Name);
                    else
                        Console.WriteLine("{0}", person.Name);
                    if (person is Firefighter)
                        Console.WriteLine("Firefighter at station {0}",
                                           ((Firefighter)person).FireStation);
                    else if (person is Teacher)
                        Console.WriteLine("Teacher at {0}", ((Teacher)person).School);
                    else if (person is Retired)
                        Console.WriteLine("Retired, hobby is {0}",
                                           ((Retired)person).FullTimeHobby);
                    Console.WriteLine("Fans:");
                    foreach (var fan in person.Fans)
                    {
                        Console.WriteLine("\t{0}", fan.Name);
                    }
                }
            }
        }
    }
}
