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

namespace LoadingEntitiesAndNavigationProperties.Recipe11
{
    /// <summary>
    ///5-11  测试实体引用或实体集合是否加载
    /// </summary>
    public class Recipe11Program
    {
        public static void Run()
        {
            using (var context = new EFContext())
            {
                var man1 = new Manager { Name = "Jill Stevens" };
                var proj = new Project { Name = "City Riverfront Park", Manager = man1 };
                var con1 = new Contractor { Name = "Robert Alvert", Project = proj };
                var con2 = new Contractor { Name = "Alan Jones", Project = proj };
                var con3 = new Contractor { Name = "Nancy Roberts", Project = proj };
                context.Projects.Add(proj);
                context.SaveChanges();
            }

            using (var context = new EFContext())
            {
                var project = context.Projects.Include("Manager").First();

                if (context.Entry(project).Reference(x => x.Manager).IsLoaded)
                    Console.WriteLine("Manager entity is loaded.");
                else
                    Console.WriteLine("Manager entity is NOT loaded.");

                if (context.Entry(project).Collection(x => x.Contractors).IsLoaded)
                    Console.WriteLine("Contractors are loaded.");
                else
                    Console.WriteLine("Contractors are NOT loaded.");
                Console.WriteLine("Calling project.Contractors.Load()...");

                context.Entry(project).Collection(x => x.Contractors).Load();

                if (context.Entry(project).Collection(x => x.Contractors).IsLoaded)
                    Console.WriteLine("Contractors are now loaded.");
                else
                    Console.WriteLine("Contractors failed to load.");
            }

            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }
    }
}
