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

namespace LoadingEntitiesAndNavigationProperties.Recipe5
{
    /// <summary>
    /// 5-5 加载完整的对象图
    /// </summary>
    public class Recipe5Program
    {
        public static void Run()
        {
            using (var context = new EFContext())
            {
                var course = new Course { Title = "Biology 101" };
                var fred = new Instructor { Name = "Fred Jones" };
                var julia = new Instructor { Name = "Julia Canfield" };
                var section1 = new Section { Course = course, Instructor = fred };
                var section2 = new Section { Course = course, Instructor = julia };

                var jim = new Student { Name = "Jim Roberts" };
                jim.Sections.Add(section1);

                var jerry = new Student { Name = "Jerry Jones" };
                jerry.Sections.Add(section2);

                var susan = new Student { Name = "Susan O'Reilly" };
                susan.Sections.Add(section1);

                var cathy = new Student { Name = "Cathy Ryan" };
                cathy.Sections.Add(section2);

                course.Sections.Add(section1);
                course.Sections.Add(section2);

                context.Students.Add(jim);
                context.Students.Add(jerry);
                context.Students.Add(susan);
                context.Students.Add(cathy);

                context.Courses.Add(course);
                context.SaveChanges();
            }

            //调用多个Includ()方法预先加载数据，会迅速地增加生成的SQL语句的复杂性，
            //同时，从数据库中返回的数据量也急剧上升。复杂的查询语句会导致产生低性能的查询计划，大数据量的返回，
            //也会让实体框架花费大量的时间来移除重复的数据。所以你应该明智地使用Include()方法，确保它不会给你的应用带来潜在的性能问题。

            //字符串查询路径参数
            using (var context = new EFContext())
            {
                var graph = context.Courses
                                   .Include("Sections.Instructor")
                                   .Include("Sections.Students");
                Console.WriteLine("Courses");
                Console.WriteLine("=======");

                foreach (var course in graph)
                {
                    Console.WriteLine("{0}", course.Title);
                    foreach (var section in course.Sections)
                    {
                        Console.WriteLine("\tSection: {0}, Instrutor: {1}", section.SectionId, section.Instructor.Name);
                        Console.WriteLine("\tStudents:");
                        foreach (var student in section.Students)
                        {
                            Console.WriteLine("\t\t{0}", student.Name);
                        }
                        Console.WriteLine("\n");
                    }
                }
            }

            // 强类型查询路径参数
            using (var context = new EFContext())
            {
                var graph = context.Courses
                                   .Include(x => x.Sections.Select(y => y.Instructor))
                                   .Include(x => x.Sections.Select(z => z.Students));

                Console.WriteLine("Courses");
                Console.WriteLine("=======");

                foreach (var course in graph)
                {
                    Console.WriteLine("{0}", course.Title);
                    foreach (var section in course.Sections)
                    {
                        Console.WriteLine("\tSection: {0}, Instrutor: {1}", section.SectionId, section.Instructor.Name);
                        Console.WriteLine("\tStudents:");
                        foreach (var student in section.Students)
                        {
                            Console.WriteLine("\t\t{0}", student.Name);
                        }
                        Console.WriteLine("\n");
                    }
                }
            }

            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }
    }
}
